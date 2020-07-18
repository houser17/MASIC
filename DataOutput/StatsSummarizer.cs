﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using MASIC.Options;
using MASICPeakFinder;
using PRISM;
using DbUtils = PRISMDatabaseUtils.DataTableUtils;

namespace MASIC.DataOutput
{
    public class StatsSummarizer : EventNotifier
    {

        #region "Constants and Enums"

        private enum ReporterIonsColumns
        {
            DatasetID = 0,
            ScanNumber = 1,
            CollisionMode = 2,
            ParentIonMZ = 3,
            BasePeakIntensity = 4,
            BasePeakMZ = 5,
            ReporterIonIntensityMax = 6,
            WeightedAvgPctIntensityCorrection = 7
        }

        private enum ScanStatsColumns
        {
            DatasetID = 0,
            ScanNumber = 1,
            ScanTime = 2,
            ScanType = 3,
            TotalIonIntensity = 4,
            BasePeakIntensity = 5,
            BasePeakMZ = 6,
            BasePeakSignalToNoiseRatio = 7,
            IonCount = 8,
            IonCountRaw = 9,
            ScanTypeName = 10
        }

        private enum SICStatsColumns
        {
            DatasetID = 0,
            ParentIonIndex = 1,
            Mz = 2,
            SurveyScanNumber = 3,
            FragScanNumber = 4,
            OptimalPeakApexScanNumber = 5,
            PeakSignalToNoiseRatio = 6,
            FWHMinScans = 7,
            PeakArea = 8,
            ParentIonIntensity = 9
        }

        #endregion

        #region "Member variables"

        /// <summary>
        /// Parent ions loaded from the SICStats file
        /// </summary>
        /// <remarks>Keys are parent ion index, values are parent ion info</remarks>
        private readonly Dictionary<int, clsParentIonInfo> mParentIons;

        /// <summary>
        /// Scan data loaded from the ScanStats file
        /// </summary>
        /// <remarks>Keys are scan number, values are scan info</remarks>
        private readonly Dictionary<int, clsScanInfo> mScanList;

        /// <summary>
        /// Reporter ion columns in the ReporterIons file
        /// </summary>
        /// <remarks>Keys are column index, values are reporter ion info</remarks>
        private readonly Dictionary<int, clsReporterIonInfo> mReporterIonInfo;

        /// <summary>
        /// Reporter ion abundance data in the ReporterIons file
        /// </summary>
        /// <remarks>
        /// Keys are column index (corresponding to keys in <see cref="mReporterIonInfo"/>)
        /// Values are a dictionary of scan number and abundance
        /// </remarks>
        private readonly Dictionary<int, Dictionary<int, double>> mReporterIonAbundances;

        #endregion

        #region "Properties"

        /// <summary>
        /// MASIC Options
        /// </summary>
        public MASICOptions Options { get; }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options">MASIC Options</param>
        public StatsSummarizer(MASICOptions options)
        {
            Options = options;

            mScanList = new Dictionary<int, clsScanInfo>();
            mParentIons = new Dictionary<int, clsParentIonInfo>();

            mReporterIonInfo = new Dictionary<int, clsReporterIonInfo>();
            mReporterIonAbundances = new Dictionary<int, Dictionary<int, double>>();

        }

        private void AddHeaderColumn<T>(Dictionary<T, SortedSet<string>> columnNamesByIdentifier, T columnId, string columnName)
        {
            DbUtils.AddColumnNamesForIdentifier(columnNamesByIdentifier, columnId, columnName);
        }

        private void ClearResults()
        {
            mScanList.Clear();
            mParentIons.Clear();

            mReporterIonInfo.Clear();
            mReporterIonAbundances.Clear();


        private bool LoadReporterIons(string reporterIonsFilePath)
        {
            try
            {
                mReporterIonInfo.Clear();
                mReporterIonAbundances.Clear();

                var reporterIonsFile = new FileInfo(reporterIonsFilePath);
                if (!reporterIonsFile.Exists)
                {
                    OnWarningEvent("ReporterIons file not found, cannot convert FWHM in scans to minutes; file path:" + reporterIonsFile.FullName);
                    return false;
                }

                OnDebugEvent("Reading reporter ions from " + PathUtils.CompactPathString(reporterIonsFile.FullName, 110));

                // Keys in this dictionary are column identifier
                // Values are the index of this column in the tab-delimited text file (-1 if not present)
                var columnMap = new Dictionary<ReporterIonsColumns, int>();

                var columnNamesByIdentifier = new Dictionary<ReporterIonsColumns, SortedSet<string>>();
                AddHeaderColumn(columnNamesByIdentifier, ReporterIonsColumns.DatasetID, "Dataset");
                AddHeaderColumn(columnNamesByIdentifier, ReporterIonsColumns.ScanNumber, "ScanNumber");
                AddHeaderColumn(columnNamesByIdentifier, ReporterIonsColumns.CollisionMode, "Collision Mode");
                AddHeaderColumn(columnNamesByIdentifier, ReporterIonsColumns.ParentIonMZ, "ParentIonMZ");
                AddHeaderColumn(columnNamesByIdentifier, ReporterIonsColumns.BasePeakIntensity, "BasePeakIntensity");
                AddHeaderColumn(columnNamesByIdentifier, ReporterIonsColumns.BasePeakMZ, "BasePeakMZ");
                AddHeaderColumn(columnNamesByIdentifier, ReporterIonsColumns.ReporterIonIntensityMax, "ReporterIonIntensityMax");
                AddHeaderColumn(columnNamesByIdentifier, ReporterIonsColumns.WeightedAvgPctIntensityCorrection, "Weighted Avg Pct Intensity Correction");

                var requiredColumns = new List<ReporterIonsColumns>
                {
                    ReporterIonsColumns.ScanNumber
                };

                // This RegEx matches reporter ion abundance columns
                // Typically columns will have names like Ion_126.128 or Ion_116
                // However, the name could be followed by an underscore then an integer, thus the ?<ReporterIonIndex> group

                var reporterIonMzMatcher = new Regex(@"^Ion_(?<Mz>[0-9.]+)(?<ReporterIonIndex>_\d+)?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                using (var reader = new StreamReader(new FileStream(reporterIonsFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    var linesRead = 0;

                    while (!reader.EndOfStream)
                    {
                        var dataLine = reader.ReadLine();
                        if (dataLine == null)
                            continue;

                        var dataValues = dataLine.Split('\t');

                        if (dataValues.Length <= 0)
                            continue;

                        linesRead += 1;

                        if (linesRead == 1)
                        {
                            // This is the first non-blank line; parse the headers
                            DbUtils.GetColumnMappingFromHeaderLine(columnMap, dataLine, columnNamesByIdentifier);

                            // Assure that required columns are present
                            foreach (var columnId in requiredColumns)
                            {
                                if (DbUtils.GetColumnIndex(columnMap, columnId) < 0)
                                {
                                    OnWarningEvent(string.Format("File {0} is missing required column {1}", reporterIonsFile.FullName, columnId));
                                    return false;
                                }
                            }

                            // Determine the column indices of the reporter ion abundance columns

                            // Reporter ion column names are of the form:
                            // Ion_126.128 or Ion_116

                            // If there is a name conflict due to two reporter ions having the same rounded mass,
                            // a uniquifier will have been appended, e.g. Ion_116_2

                            for (var columnIndex = 0; columnIndex < dataValues.Length; columnIndex++)
                            {
                                var columnName = dataValues[columnIndex];

                                if (columnMap.ContainsValue(columnIndex) || !columnName.StartsWith(clsReporterIonProcessor.REPORTER_ION_COLUMN_PREFIX))
                                    continue;

                                var reporterIonMatch = reporterIonMzMatcher.Match(columnName);

                                if (!reporterIonMatch.Success)
                                {
                                    // Although this column starts with Ion_, it is not a reporter ion intensity column
                                    // Example columns that are skipped:
                                    // Ion_126.128_ObsMZ
                                    // Ion_126.128_OriginalIntensity
                                    // Ion_126.128_SignalToNoise
                                    // Ion_126.128_Resolution
                                    continue;
                                }

                                var reporterIonMz = double.Parse(reporterIonMatch.Groups["Mz"].Value);
                                var reporterIon = new clsReporterIonInfo(reporterIonMz);

                                mReporterIonInfo.Add(columnIndex, reporterIon);
                                mReporterIonAbundances.Add(columnIndex, new Dictionary<int, double>());

                                ReporterIonNames.Add(columnIndex, columnName);
                            }

                            continue;
                        }

                        var scanNumber = DbUtils.GetColumnValue(dataValues, columnMap, ReporterIonsColumns.ScanNumber, 0);
                        // Skip: var collisionMode = DbUtils.GetColumnValue(dataValues, columnMap, ReporterIonsColumns.CollisionMode);
                        // Skip: var parentIonMZ = DbUtils.GetColumnValue(dataValues, columnMap, ReporterIonsColumns.ParentIonMZ, 0.0);
                        // Skip: var basePeakIntensity = DbUtils.GetColumnValue(dataValues, columnMap, ReporterIonsColumns.BasePeakIntensity, 0.0);
                        // Skip: var basePeakMZ = DbUtils.GetColumnValue(dataValues, columnMap, ReporterIonsColumns.BasePeakMZ, 0.0);
                        // Skip: var reporterIonIntensityMax = DbUtils.GetColumnValue(dataValues, columnMap, ReporterIonsColumns.ReporterIonIntensityMax, 0.0);
                        // Skip: var weightedAvgPctIntensityCorrection = DbUtils.GetColumnValue(dataValues, columnMap, ReporterIonsColumns.WeightedAvgPctIntensityCorrection, 0.0);

                        foreach (var columnIndex in mReporterIonInfo.Keys)
                        {
                            if (double.TryParse(dataValues[columnIndex], out var reporterIonAbundance))
                            {
                                mReporterIonAbundances[columnIndex].Add(scanNumber, reporterIonAbundance);
                            }
                            else
                            {
                                mReporterIonAbundances[columnIndex].Add(scanNumber, 0);
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                OnErrorEvent("Exception in LoadReporterIons", ex);
                return false;
            }
        }

        private bool LoadScanStats(string scanStatsFilePath)
        {
            try
            {
                mScanList.Clear();

                var scanStatsFile = new FileInfo(scanStatsFilePath);
                if (!scanStatsFile.Exists)
                {
                    OnWarningEvent("ScanStats file not found, cannot convert FWHM in scans to minutes; file path:" + scanStatsFile.FullName);
                    return false;
                }

                OnDebugEvent("Reading scan info from " + PathUtils.CompactPathString(scanStatsFile.FullName, 110));

                // Keys in this dictionary are column identifier
                // Values are the index of this column in the tab-delimited text file (-1 if not present)
                var columnMap = new Dictionary<ScanStatsColumns, int>();

                var columnNamesByIdentifier = new Dictionary<ScanStatsColumns, SortedSet<string>>();
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.DatasetID, "Dataset");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.ScanNumber, "ScanNumber");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.ScanTime, "ScanTime");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.ScanType, "ScanType");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.TotalIonIntensity, "TotalIonIntensity");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.BasePeakIntensity, "BasePeakIntensity");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.BasePeakMZ, "BasePeakMZ");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.BasePeakSignalToNoiseRatio, "BasePeakSignalToNoiseRatio");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.IonCount, "IonCount");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.IonCountRaw, "IonCountRaw");
                AddHeaderColumn(columnNamesByIdentifier, ScanStatsColumns.ScanTypeName, "ScanTypeName");

                var requiredColumns = new List<ScanStatsColumns>
                {
                    ScanStatsColumns.ScanNumber,
                    ScanStatsColumns.ScanTime,
                };

                using (var reader = new StreamReader(new FileStream(scanStatsFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    var linesRead = 0;

                    while (!reader.EndOfStream)
                    {
                        var dataLine = reader.ReadLine();
                        if (dataLine == null)
                            continue;

                        var dataValues = dataLine.Split('\t');

                        if (dataValues.Length <= 0)
                            continue;

                        linesRead += 1;

                        if (linesRead == 1)
                        {
                            // This is the first non-blank line; parse the headers
                            DbUtils.GetColumnMappingFromHeaderLine(columnMap, dataLine, columnNamesByIdentifier);

                            // Assure that required columns are present
                            foreach (var columnId in requiredColumns)
                            {
                                if (DbUtils.GetColumnIndex(columnMap, columnId) < 0)
                                {
                                    OnWarningEvent(string.Format("File {0} is missing required column {1}", scanStatsFile.FullName, columnId));
                                    return false;
                                }
                            }
                            continue;
                        }

                        var scanNumber = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.ScanNumber, 0);
                        var scanTime = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.ScanTime, 0.0);
                        // Skip: var scanType = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.ScanType, 0);
                        var totalIonIntensity = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.TotalIonIntensity, 0.0);
                        var basePeakIntensity = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.BasePeakIntensity, 0.0);
                        var basePeakMZ = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.BasePeakMZ, 0.0);
                        // Skip: var basePeakSignalToNoiseRatio = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.BasePeakSignalToNoiseRatio, 0.0);
                        var ionCount = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.IonCount, 0);
                        var ionCountRaw = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.IonCountRaw, 0);
                        var scanTypeName = DbUtils.GetColumnValue(dataValues, columnMap, ScanStatsColumns.ScanTypeName);

                        var scanInfo = new clsScanInfo()
                        {
                            ScanNumber = scanNumber,
                            ScanTime = (float)scanTime,
                            ScanTypeName = scanTypeName,
                            TotalIonIntensity = totalIonIntensity,
                            BasePeakIonIntensity = basePeakIntensity,
                            BasePeakIonMZ = basePeakMZ,
                            IonCount = ionCount,
                            IonCountRaw = ionCountRaw
                        };

                        if (mScanList.ContainsKey(scanNumber))
                        {
                            OnWarningEvent(string.Format("Ignoring duplicate scan {0} found in {1}", scanNumber, scanStatsFile.Name));
                            continue;
                        }

                        mScanList.Add(scanNumber, scanInfo);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                OnErrorEvent("Exception in LoadScanStats", ex);
                return false;
            }
        }

        private bool LoadSICStats(string sicStatsFilePath)
        {
            try
            {
                mParentIons.Clear();

                var sicStatsFile = new FileInfo(sicStatsFilePath);
                if (!sicStatsFile.Exists)
                {
                    OnWarningEvent("File not found: " + sicStatsFile.FullName);
                    return false;
                }

                OnDebugEvent("Reading SIC data from " + PathUtils.CompactPathString(sicStatsFile.FullName, 110));

                // Keys in this dictionary are column identifier
                // Values are the index of this column in the tab-delimited text file (-1 if not present)
                var columnMap = new Dictionary<SICStatsColumns, int>();

                var columnNamesByIdentifier = new Dictionary<SICStatsColumns, SortedSet<string>>();
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.DatasetID, "Dataset");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.ParentIonIndex, "ParentIonIndex");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.Mz, "MZ");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.SurveyScanNumber, "SurveyScanNumber");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.FragScanNumber, "FragScanNumber");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.OptimalPeakApexScanNumber, "OptimalPeakApexScanNumber");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.PeakSignalToNoiseRatio, "PeakSignalToNoiseRatio");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.FWHMinScans, "FWHMInScans");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.PeakArea, "PeakArea");
                AddHeaderColumn(columnNamesByIdentifier, SICStatsColumns.ParentIonIntensity, "ParentIonIntensity");

                var requiredColumns = new List<SICStatsColumns>
                {
                    SICStatsColumns.OptimalPeakApexScanNumber,
                    SICStatsColumns.FWHMinScans,
                    SICStatsColumns.PeakArea
                };

                using (var reader = new StreamReader(new FileStream(sicStatsFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    var linesRead = 0;

                    while (!reader.EndOfStream)
                    {
                        var dataLine = reader.ReadLine();
                        if (dataLine == null)
                            continue;

                        var dataValues = dataLine.Split('\t');

                        if (dataValues.Length <= 0)
                            continue;

                        linesRead += 1;

                        if (linesRead == 1)
                        {
                            // This is the first non-blank line; parse the headers
                            DbUtils.GetColumnMappingFromHeaderLine(columnMap, dataLine, columnNamesByIdentifier);

                            // Assure that required columns are present
                            foreach (var columnId in requiredColumns)
                            {
                                if (DbUtils.GetColumnIndex(columnMap, columnId) < 0)
                                {
                                    OnWarningEvent(string.Format("File {0} is missing required column {1}", sicStatsFile.FullName, columnId));
                                    return false;
                                }
                            }
                            continue;
                        }

                        var parentIonIndex = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.ParentIonIndex, 0);
                        var parentIonMz = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.Mz, 0.0);
                        var surveyScanNumber = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.SurveyScanNumber, 0);
                        var fragScanNumber = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.FragScanNumber, 0);
                        var optimalPeakApexScanNumber = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.OptimalPeakApexScanNumber, 0);
                        var peakSignalToNoiseRatio = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.PeakSignalToNoiseRatio, 0.0);
                        var fwhmInScans = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.FWHMinScans, 0);
                        var peakArea = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.PeakArea, 0.0);
                        var parentIonIntensity = DbUtils.GetColumnValue(dataValues, columnMap, SICStatsColumns.ParentIonIntensity, 0.0);

                        var parentIon = new clsParentIonInfo(parentIonMz)
                        {
                            OptimalPeakApexScanNumber = optimalPeakApexScanNumber
                        };

                        // MASIC typically tracks scans using ScanIndex values
                        // Instead, we're storing scan numbers here
                        parentIon.FragScanIndices.Add(fragScanNumber);
                        parentIon.SurveyScanIndex = surveyScanNumber;

                        parentIon.SICStats.Peak.ParentIonIntensity = parentIonIntensity;
                        parentIon.SICStats.Peak.SignalToNoiseRatio = peakSignalToNoiseRatio;
                        parentIon.SICStats.Peak.FWHMScanWidth = fwhmInScans;
                        parentIon.SICStats.Peak.Area = peakArea;

                        if (mParentIons.ContainsKey(parentIonIndex))
                        {
                            if (parentIonIndex > 0)
                            {
                                OnWarningEvent(string.Format(
                                    "Ignoring duplicate parent ion index {0} found in {1}", parentIonIndex, sicStatsFile.Name));
                                continue;
                            }

                            var alternateIndex = -linesRead;

                            if (mParentIons.ContainsKey(alternateIndex))
                            {
                                OnWarningEvent(string.Format(
                                    "Ignoring conflicting parent ion index {0} for parent ion {1} found in {2}",
                                    alternateIndex, parentIonIndex, sicStatsFile.Name));
                                continue;
                            }

                            mParentIons.Add(alternateIndex, parentIon);
                        }
                        else
                        {
                            mParentIons.Add(parentIonIndex, parentIon);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                OnErrorEvent("Exception in LoadSICStats", ex);
                return false;
            }
        }

        public bool SummarizeSICStats(string sicStatsFilePath)
        {
            try
            {

                ClearResults();

                var sicStatsLoaded = LoadSICStats(sicStatsFilePath);

                var scanStatsFilePath = clsUtilities.ReplaceSuffix(sicStatsFilePath, clsDataOutput.SIC_STATS_FILE_SUFFIX, clsDataOutput.SCAN_STATS_FILE_SUFFIX);

                // ReSharper disable once UnusedVariable
                var scanStatsLoaded = LoadScanStats(scanStatsFilePath);

                var reporterIonsFilePath = clsUtilities.ReplaceSuffix(sicStatsFilePath, clsDataOutput.SIC_STATS_FILE_SUFFIX, clsDataOutput.REPORTER_IONS_FILE_SUFFIX);
                var reporterIonsLoaded = LoadReporterIons(reporterIonsFilePath);

            }
            catch (Exception ex)
            {
                OnErrorEvent("Exception in SummarizeSICStats", ex);
                return false;
            }
        }
    }
}
