﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace MASIC
{
    public class clsParentIonProcessing : clsMasicEventNotifier
    {
        #region "Classwide Variables"

        private readonly clsReporterIons mReporterIons;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reporterIons"></param>
        public clsParentIonProcessing(clsReporterIons reporterIons)
        {
            mReporterIons = reporterIons;
        }

        public void AddUpdateParentIons(
            clsScanList scanList,
            int surveyScanIndex,
            double parentIonMZ,
            int fragScanIndex,
            clsSpectraCache spectraCache,
            clsSICOptions sicOptions)
        {
            AddUpdateParentIons(scanList, surveyScanIndex, parentIonMZ, 0, 0, fragScanIndex, spectraCache, sicOptions);
        }

        public void AddUpdateParentIons(
            clsScanList scanList,
            int surveyScanIndex,
            double parentIonMZ,
            clsMRMScanInfo mrmInfo,
            clsSpectraCache spectraCache,
            clsSICOptions sicOptions)
        {
            for (var mrmIndex = 0; mrmIndex <= mrmInfo.MRMMassCount - 1; mrmIndex++)
            {
                var mrmDaughterMZ = mrmInfo.MRMMassList[mrmIndex].CentralMass;
                var mrmToleranceHalfWidth = Math.Round((mrmInfo.MRMMassList[mrmIndex].EndMass - mrmInfo.MRMMassList[mrmIndex].StartMass) / 2, 6);
                if (mrmToleranceHalfWidth < 0.001)
                {
                    mrmToleranceHalfWidth = 0.001;
                }

                AddUpdateParentIons(scanList, surveyScanIndex, parentIonMZ, mrmDaughterMZ, mrmToleranceHalfWidth, scanList.FragScans.Count - 1, spectraCache, sicOptions);
            }
        }

        private void AddUpdateParentIons(
            clsScanList scanList,
            int surveyScanIndex,
            double parentIonMZ,
            double mrmDaughterMZ,
            double mrmToleranceHalfWidth,
            int fragScanIndex,
            clsSpectraCache spectraCache,
            clsSICOptions sicOptions)
        {
            const double MINIMUM_TOLERANCE_PPM = 0.01;
            const double MINIMUM_TOLERANCE_DA = 0.0001;

            // Checks to see if the parent ion specified by surveyScanIndex and parentIonMZ exists in .ParentIons()
            // If mrmDaughterMZ is > 0, also considers that value when determining uniqueness
            //
            // If the parent ion entry already exists, adds an entry to .FragScanIndices()
            // If it does not exist, adds a new entry to .ParentIons()
            // Note that typically fragScanIndex will equal scanList.FragScans.Count - 1

            // If surveyScanIndex < 0, the first scan(s) in the file occurred before we encountered a survey scan
            // In this case, we cannot properly associate the fragmentation scan with a survey scan

            var parentIonIndex = 0;

            double parentIonTolerance;

            if (sicOptions.SICToleranceIsPPM)
            {
                parentIonTolerance = sicOptions.SICTolerance / sicOptions.CompressToleranceDivisorForPPM;
                if (parentIonTolerance < MINIMUM_TOLERANCE_PPM)
                {
                    parentIonTolerance = MINIMUM_TOLERANCE_PPM;
                }
            }
            else
            {
                parentIonTolerance = sicOptions.SICTolerance / sicOptions.CompressToleranceDivisorForDa;
                if (parentIonTolerance < MINIMUM_TOLERANCE_DA)
                {
                    parentIonTolerance = MINIMUM_TOLERANCE_DA;
                }
            }

            // See if an entry exists yet in .ParentIons for the parent ion for this fragmentation scan
            var matchFound = false;

            if (mrmDaughterMZ > 0)
            {
                if (sicOptions.SICToleranceIsPPM)
                {
                    // Force the tolerances to 0.01 m/z units
                    parentIonTolerance = MINIMUM_TOLERANCE_PPM;
                }
                else
                {
                    // Force the tolerances to 0.01 m/z units
                    parentIonTolerance = MINIMUM_TOLERANCE_DA;
                }
            }

            if (parentIonMZ > 0)
            {
                var parentIonToleranceDa = GetParentIonToleranceDa(sicOptions, parentIonMZ, parentIonTolerance);

                for (parentIonIndex = scanList.ParentIons.Count - 1; parentIonIndex >= 0; parentIonIndex += -1)
                {
                    if (scanList.ParentIons[parentIonIndex].SurveyScanIndex >= surveyScanIndex)
                    {
                        if (Math.Abs(scanList.ParentIons[parentIonIndex].MZ - parentIonMZ) <= parentIonToleranceDa)
                        {
                            if (mrmDaughterMZ < double.Epsilon ||
                                Math.Abs(scanList.ParentIons[parentIonIndex].MRMDaughterMZ - mrmDaughterMZ) <= parentIonToleranceDa)
                            {
                                matchFound = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (!matchFound)
            {
                // Add a new parent ion entry to .ParentIons(), but only if surveyScanIndex >= 0

                if (surveyScanIndex >= 0)
                {
                    var newParentIon = new clsParentIonInfo(parentIonMZ)
                    {
                        SurveyScanIndex = surveyScanIndex,
                        CustomSICPeak = false,
                        MRMDaughterMZ = mrmDaughterMZ,
                        MRMToleranceHalfWidth = mrmToleranceHalfWidth
                    };

                    newParentIon.FragScanIndices.Add(fragScanIndex);

                    newParentIon.OptimalPeakApexScanNumber = scanList.SurveyScans[surveyScanIndex].ScanNumber;        // Was: .FragScans(fragScanIndex).ScanNumber
                    newParentIon.PeakApexOverrideParentIonIndex = -1;
                    scanList.FragScans[fragScanIndex].FragScanInfo.ParentIonInfoIndex = scanList.ParentIons.Count;

                    // Look for .MZ in the survey scan, using a tolerance of parentIonTolerance
                    // If found, then update the mass to the matched ion
                    // This is done to determine the parent ion mass more precisely
                    if (sicOptions.RefineReportedParentIonMZ)
                    {
                        if (FindClosestMZ(spectraCache, scanList.SurveyScans, surveyScanIndex, parentIonMZ, parentIonTolerance, out var parentIonMZMatch))
                        {
                            newParentIon.UpdateMz(parentIonMZMatch);
                        }
                    }

                    scanList.ParentIons.Add(newParentIon);
                }
            }
            // Add a new entry to .FragScanIndices() for the matching parent ion
            // However, do not add a new entry if this is an MRM scan

            else if (mrmDaughterMZ < double.Epsilon)
            {
                scanList.ParentIons[parentIonIndex].FragScanIndices.Add(fragScanIndex);
                scanList.FragScans[fragScanIndex].FragScanInfo.ParentIonInfoIndex = parentIonIndex;
            }
        }

        private void AppendParentIonToUniqueMZEntry(
            clsScanList scanList,
            int parentIonIndex,
            clsUniqueMZListItem mzListEntry,
            double searchMZOffset)
        {
            var parentIon = scanList.ParentIons[parentIonIndex];
            if (mzListEntry.MatchCount == 0)
            {
                mzListEntry.MZAvg = parentIon.MZ - searchMZOffset;
                mzListEntry.MatchIndices.Add(parentIonIndex);
            }
            else
            {
                // Update the average MZ: NewAvg = (OldAvg * OldCount + NewValue) / NewCount
                mzListEntry.MZAvg = (mzListEntry.MZAvg * mzListEntry.MatchCount + (parentIon.MZ - searchMZOffset)) / (mzListEntry.MatchCount + 1);
                mzListEntry.MatchIndices.Add(parentIonIndex);
            }

            var sicStats = parentIon.SICStats;
            if (sicStats.Peak.MaxIntensityValue > mzListEntry.MaxIntensity || mzListEntry.MatchCount == 1)
            {
                mzListEntry.MaxIntensity = sicStats.Peak.MaxIntensityValue;
                if (sicStats.ScanTypeForPeakIndices == clsScanList.eScanTypeConstants.FragScan)
                {
                    mzListEntry.ScanNumberMaxIntensity = scanList.FragScans[sicStats.PeakScanIndexMax].ScanNumber;
                    mzListEntry.ScanTimeMaxIntensity = scanList.FragScans[sicStats.PeakScanIndexMax].ScanTime;
                }
                else
                {
                    mzListEntry.ScanNumberMaxIntensity = scanList.SurveyScans[sicStats.PeakScanIndexMax].ScanNumber;
                    mzListEntry.ScanTimeMaxIntensity = scanList.SurveyScans[sicStats.PeakScanIndexMax].ScanTime;
                }

                mzListEntry.ParentIonIndexMaxIntensity = parentIonIndex;
            }

            if (sicStats.Peak.Area > mzListEntry.MaxPeakArea || mzListEntry.MatchCount == 1)
            {
                mzListEntry.MaxPeakArea = sicStats.Peak.Area;
                mzListEntry.ParentIonIndexMaxPeakArea = parentIonIndex;
            }
        }

        private float CompareFragSpectraForParentIons(
            clsScanList scanList,
            clsSpectraCache spectraCache,
            int parentIonIndex1,
            int parentIonIndex2,
            clsBinningOptions binningOptions,
            MASICPeakFinder.clsBaselineNoiseOptions noiseThresholdOptions,
            DataInput.clsDataImport dataImportUtilities)
        {
            // Compare the fragmentation spectra for the two parent ions
            // Returns the highest similarity score (ranging from 0 to 1)
            // Returns 0 if no similarity or no spectra to compare
            // Returns -1 if an error

            float highestSimilarityScore;

            try
            {
                if (scanList.ParentIons[parentIonIndex1].CustomSICPeak || scanList.ParentIons[parentIonIndex2].CustomSICPeak)
                {
                    // Custom SIC values do not have fragmentation spectra; nothing to compare
                    highestSimilarityScore = 0;
                }
                else if (scanList.ParentIons[parentIonIndex1].MRMDaughterMZ > 0 || scanList.ParentIons[parentIonIndex2].MRMDaughterMZ > 0)
                {
                    // MRM Spectra should not be compared
                    highestSimilarityScore = 0;
                }
                else
                {
                    highestSimilarityScore = 0;
                    for (var fragIndex1 = 0; fragIndex1 <= scanList.ParentIons[parentIonIndex1].FragScanIndices.Count - 1; fragIndex1++)
                    {
                        var fragSpectrumIndex1 = scanList.ParentIons[parentIonIndex1].FragScanIndices[fragIndex1];

                        if (!spectraCache.ValidateSpectrumInPool(scanList.FragScans[fragSpectrumIndex1].ScanNumber, out var poolIndex1))
                        {
                            SetLocalErrorCode(clsMASIC.eMasicErrorCodes.ErrorUncachingSpectrum);
                            return -1;
                        }

                        if (!clsMASIC.DISCARD_LOW_INTENSITY_MSMS_DATA_ON_LOAD)
                        {
                            dataImportUtilities.DiscardDataBelowNoiseThreshold(spectraCache.SpectraPool[poolIndex1], scanList.FragScans[fragSpectrumIndex1].BaselineNoiseStats.NoiseLevel, 0, 0, noiseThresholdOptions);
                        }

                        for (var fragIndex2 = 0; fragIndex2 <= scanList.ParentIons[parentIonIndex2].FragScanIndices.Count - 1; fragIndex2++)
                        {
                            var fragSpectrumIndex2 = scanList.ParentIons[parentIonIndex2].FragScanIndices[fragIndex2];

                            if (!spectraCache.ValidateSpectrumInPool(scanList.FragScans[fragSpectrumIndex2].ScanNumber, out var poolIndex2))
                            {
                                SetLocalErrorCode(clsMASIC.eMasicErrorCodes.ErrorUncachingSpectrum);
                                return -1;
                            }

                            if (!clsMASIC.DISCARD_LOW_INTENSITY_MSMS_DATA_ON_LOAD)
                            {
                                dataImportUtilities.DiscardDataBelowNoiseThreshold(spectraCache.SpectraPool[poolIndex2], scanList.FragScans[fragSpectrumIndex2].BaselineNoiseStats.NoiseLevel, 0, 0, noiseThresholdOptions);
                            }

                            var similarityScore = CompareSpectra(spectraCache.SpectraPool[poolIndex1], spectraCache.SpectraPool[poolIndex2], binningOptions);

                            if (similarityScore > highestSimilarityScore)
                            {
                                highestSimilarityScore = similarityScore;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError("Error in CompareFragSpectraForParentIons", ex);
                return -1;
            }

            return highestSimilarityScore;
        }

        private float CompareSpectra(
            clsMSSpectrum fragSpectrum1,
            clsMSSpectrum fragSpectrum2,
            clsBinningOptions binningOptions,
            bool considerOffsetBinnedData = true)
        {
            // Compares the two spectra and returns a similarity score (ranging from 0 to 1)
            // Perfect match is 1; no similarity is 0
            // Note that both the standard binned data and the offset binned data are compared
            // If considerOffsetBinnedData = True, then the larger of the two scores is returned
            // similarity scores is returned
            //
            // If an error, returns -1

            var binnedSpectrum1 = new clsBinnedData();
            var binnedSpectrum2 = new clsBinnedData();

            try
            {
                var objCorrelate = new clsCorrelation(binningOptions);
                RegisterEvents(objCorrelate);

                const clsCorrelation.cmCorrelationMethodConstants eCorrelationMethod = clsCorrelation.cmCorrelationMethodConstants.Pearson;

                // Bin the data in the first spectrum
                var success = CompareSpectraBinData(objCorrelate, fragSpectrum1, binnedSpectrum1);
                if (!success)
                    return -1;

                // Bin the data in the second spectrum
                success = CompareSpectraBinData(objCorrelate, fragSpectrum2, binnedSpectrum2);
                if (!success)
                    return -1;

                // Now compare the binned spectra
                // Similarity will be 0 if either instance of BinnedIntensities has fewer than 5 data points
                var similarity1 = objCorrelate.Correlate(binnedSpectrum1.BinnedIntensities, binnedSpectrum2.BinnedIntensities, eCorrelationMethod);

                if (!considerOffsetBinnedData)
                {
                    return similarity1;
                }

                var similarity2 = objCorrelate.Correlate(binnedSpectrum1.BinnedIntensitiesOffset, binnedSpectrum2.BinnedIntensitiesOffset, eCorrelationMethod);
                return Math.Max(similarity1, similarity2);
            }
            catch (Exception ex)
            {
                ReportError("CompareSpectra: " + ex.Message, ex);
                return -1;
            }
        }

        private bool CompareSpectraBinData(
            clsCorrelation objCorrelate,
            clsMSSpectrum fragSpectrum,
            clsBinnedData binnedSpectrum)
        {
            var xData = new List<float>();
            var yData = new List<float>();

            // Make a copy of the data, excluding any Reporter Ion data

            for (var index = 0; index <= fragSpectrum.IonCount - 1; index++)
            {
                if (!clsUtilities.CheckPointInMZIgnoreRange(fragSpectrum.IonsMZ[index],
                                                            mReporterIons.MZIntensityFilterIgnoreRangeStart,
                                                            mReporterIons.MZIntensityFilterIgnoreRangeEnd))
                {
                    xData.Add((float)(fragSpectrum.IonsMZ[index]));
                    yData.Add((float)(fragSpectrum.IonsIntensity[index]));
                }
            }

            binnedSpectrum.BinnedDataStartX = objCorrelate.BinStartX;
            binnedSpectrum.BinSize = objCorrelate.BinSize;

            // Note that the data in xData and yData should have already been filtered to discard data points below the noise threshold intensity
            var success = objCorrelate.BinData(xData, yData, binnedSpectrum.BinnedIntensities, binnedSpectrum.BinnedIntensitiesOffset);

            return success;
        }

        private bool FindClosestMZ(
            clsSpectraCache spectraCache,
            IList<clsScanInfo> scanList,
            int spectrumIndex,
            double searchMZ,
            double toleranceMZ,
            out double bestMatchMZ)
        {
            bool success;

            bestMatchMZ = 0;
            try
            {
                if (scanList[spectrumIndex].IonCount == 0 && scanList[spectrumIndex].IonCountRaw == 0)
                {
                    // No data in this spectrum
                    success = false;
                }
                else if (!spectraCache.ValidateSpectrumInPool(scanList[spectrumIndex].ScanNumber, out var poolIndex))
                {
                    SetLocalErrorCode(clsMASIC.eMasicErrorCodes.ErrorUncachingSpectrum);
                    success = false;
                }
                else
                {
                    var spectraPool = spectraCache.SpectraPool[poolIndex];
                    success = FindClosestMZ(spectraPool.IonsMZ, spectraPool.IonCount, searchMZ, toleranceMZ, out bestMatchMZ);
                }
            }
            catch (Exception ex)
            {
                ReportError("Error in FindClosestMZ", ex);
                success = false;
            }

            return success;
        }

        private bool FindClosestMZ(
            IList<double> mzList,
            int ionCount,
            double searchMZ,
            double toleranceMZ,
            out double bestMatchMZ)
        {
            // Searches mzList for the closest match to searchMZ within tolerance bestMatchMZ
            // If a match is found, then updates bestMatchMZ to the m/z of the match and returns True

            int closestMatchIndex;
            var bestMassDifferenceAbs = 0.0;

            try
            {
                closestMatchIndex = -1;
                for (var dataIndex = 0; dataIndex <= ionCount - 1; dataIndex++)
                {
                    var massDifferenceAbs = Math.Abs(mzList[dataIndex] - searchMZ);
                    if (massDifferenceAbs <= toleranceMZ)
                    {
                        if (closestMatchIndex < 0 || massDifferenceAbs < bestMassDifferenceAbs)
                        {
                            closestMatchIndex = dataIndex;
                            bestMassDifferenceAbs = massDifferenceAbs;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ReportError("Error in FindClosestMZ", ex);
                closestMatchIndex = -1;
            }

            if (closestMatchIndex >= 0)
            {
                bestMatchMZ = mzList[closestMatchIndex];
                return true;
            }

            bestMatchMZ = 0;
            return false;
        }

        public bool FindSimilarParentIons(
            clsScanList scanList,
            clsSpectraCache spectraCache,
            clsMASICOptions masicOptions,
            DataInput.clsDataImport dataImportUtilities,
            ref int ionUpdateCount)
        {
            // Look for parent ions that have similar m/z values and are nearby one another in time
            // For the groups of similar ions, assign the scan number of the highest intensity parent ion to the other similar parent ions

            try
            {
                ionUpdateCount = 0;

                if (scanList.ParentIons.Count <= 0)
                {
                    return masicOptions.SuppressNoParentIonsError;
                }

                Console.Write("Finding similar parent ions ");
                ReportMessage("Finding similar parent ions");
                UpdateProgress(0, "Finding similar parent ions");

                var similarParentIonsData = new clsSimilarParentIonsData(scanList.ParentIons.Count);

                similarParentIonsData.UniqueMZList.Clear();

                // Original m/z values, rounded to 2 decimal places

                // Initialize the arrays

                var mzList = new double[scanList.ParentIons.Count];
                var intensityPointerArray = new int[scanList.ParentIons.Count];
                var intensityList = new double[scanList.ParentIons.Count];

                var findSimilarIonsDataCount = 0;
                for (var index = 0; index <= scanList.ParentIons.Count - 1; index++)
                {
                    bool includeParentIon;

                    if (scanList.ParentIons[index].MRMDaughterMZ > 0)
                    {
                        includeParentIon = false;
                    }
                    else if (masicOptions.CustomSICList.LimitSearchToCustomMZList)
                    {
                        includeParentIon = scanList.ParentIons[index].CustomSICPeak;
                    }
                    else
                    {
                        includeParentIon = true;
                    }

                    if (includeParentIon)
                    {
                        similarParentIonsData.MZPointerArray[findSimilarIonsDataCount] = index;
                        mzList[findSimilarIonsDataCount] = Math.Round(scanList.ParentIons[index].MZ, 2);

                        intensityPointerArray[findSimilarIonsDataCount] = index;
                        intensityList[findSimilarIonsDataCount] = scanList.ParentIons[index].SICStats.Peak.MaxIntensityValue;
                        findSimilarIonsDataCount += 1;
                    }
                }

                if (similarParentIonsData.MZPointerArray.Length != findSimilarIonsDataCount && findSimilarIonsDataCount > 0)
                {
                    var oldMZPointerArray = similarParentIonsData.MZPointerArray;
                    similarParentIonsData.MZPointerArray = new int[findSimilarIonsDataCount];
                    Array.Copy(oldMZPointerArray, similarParentIonsData.MZPointerArray, Math.Min(findSimilarIonsDataCount, oldMZPointerArray.Length));
                    var oldMzList = mzList;
                    mzList = new double[findSimilarIonsDataCount];
                    Array.Copy(oldMzList, mzList, Math.Min(findSimilarIonsDataCount, oldMzList.Length));
                    var oldIntensityPointerArray = intensityPointerArray;
                    intensityPointerArray = new int[findSimilarIonsDataCount];
                    Array.Copy(oldIntensityPointerArray, intensityPointerArray, Math.Min(findSimilarIonsDataCount, oldIntensityPointerArray.Length));
                    var oldIntensityList = intensityList;
                    intensityList = new double[findSimilarIonsDataCount];
                    Array.Copy(oldIntensityList, intensityList, Math.Min(findSimilarIonsDataCount, oldIntensityList.Length));
                }

                if (findSimilarIonsDataCount == 0)
                {
                    if (masicOptions.SuppressNoParentIonsError)
                    {
                        return true;
                    }

                    return scanList.MRMDataPresent;
                }

                ReportMessage("FindSimilarParentIons: Sorting the mz arrays");

                // Sort the MZ arrays
                Array.Sort(mzList, similarParentIonsData.MZPointerArray);

                ReportMessage("FindSimilarParentIons: Populate objSearchRange");

                // Populate objSearchRange
                // Set UsePointerIndexArray to false to prevent .FillWithData trying to sort mzList
                // (the data was already sorted above)
                var objSearchRange = new clsSearchRange()
                {
                    UsePointerIndexArray = false
                };

                var success = objSearchRange.FillWithData(ref mzList);

                ReportMessage("FindSimilarParentIons: Sort the intensity arrays");

                // Sort the Intensity arrays
                Array.Sort(intensityList, intensityPointerArray);

                // Reverse the order of intensityPointerArray so that it is ordered from the most intense to the least intense ion
                // Note: We don't really need to reverse intensityList since we're done using it, but
                // it doesn't take long, it won't hurt, and it will keep intensityList synchronized with intensityPointerArray
                Array.Reverse(intensityPointerArray);
                Array.Reverse(intensityList);

                ReportMessage("FindSimilarParentIons: Look for similar parent ions by using m/z and scan");
                var lastLogTime = DateTime.UtcNow;

                // Look for similar parent ions by using m/z and scan
                // Step through the ions by decreasing intensity
                var parentIonIndex = 0;
                do
                {
                    var originalIndex = intensityPointerArray[parentIonIndex];
                    if (similarParentIonsData.IonUsed[originalIndex])
                    {
                        // Parent ion was already used; move onto the next one
                        parentIonIndex += 1;
                    }
                    else
                    {
                        var mzListEntry = new clsUniqueMZListItem();
                        similarParentIonsData.UniqueMZList.Add(mzListEntry);

                        AppendParentIonToUniqueMZEntry(scanList, originalIndex, mzListEntry, 0);

                        similarParentIonsData.IonUsed[originalIndex] = true;
                        similarParentIonsData.IonInUseCount = 1;

                        // Look for other parent ions with m/z values in tolerance (must be within mass tolerance and scan tolerance)
                        // If new values are added, then repeat the search using the updated udtUniqueMZList().MZAvg value
                        int ionInUseCountOriginal;
                        do
                        {
                            ionInUseCountOriginal = similarParentIonsData.IonInUseCount;
                            var currentMZ = mzListEntry.MZAvg;

                            if (currentMZ <= 0)
                                continue;

                            FindSimilarParentIonsWork(spectraCache, currentMZ, 0, originalIndex,
                                                      scanList, similarParentIonsData,
                                                      masicOptions, dataImportUtilities, objSearchRange);

                            // Look for similar 1+ spaced m/z values
                            FindSimilarParentIonsWork(spectraCache, currentMZ, 1, originalIndex,
                                                      scanList, similarParentIonsData,
                                                      masicOptions, dataImportUtilities, objSearchRange);

                            FindSimilarParentIonsWork(spectraCache, currentMZ, -1, originalIndex,
                                                      scanList, similarParentIonsData,
                                                      masicOptions, dataImportUtilities, objSearchRange);

                            // Look for similar 2+ spaced m/z values
                            FindSimilarParentIonsWork(spectraCache, currentMZ, 0.5, originalIndex,
                                                      scanList, similarParentIonsData,
                                                      masicOptions, dataImportUtilities, objSearchRange);

                            FindSimilarParentIonsWork(spectraCache, currentMZ, -0.5, originalIndex,
                                                      scanList, similarParentIonsData,
                                                      masicOptions, dataImportUtilities, objSearchRange);

                            var parentIonToleranceDa = GetParentIonToleranceDa(masicOptions.SICOptions, currentMZ);

                            if (parentIonToleranceDa <= 0.25 && masicOptions.SICOptions.SimilarIonMZToleranceHalfWidth <= 0.15)
                            {
                                // Also look for similar 3+ spaced m/z values
                                FindSimilarParentIonsWork(spectraCache, currentMZ, 0.666, originalIndex,
                                                          scanList, similarParentIonsData,
                                                          masicOptions, dataImportUtilities, objSearchRange);

                                FindSimilarParentIonsWork(spectraCache, currentMZ, 0.333, originalIndex,
                                                          scanList, similarParentIonsData,
                                                          masicOptions, dataImportUtilities, objSearchRange);

                                FindSimilarParentIonsWork(spectraCache, currentMZ, -0.333, originalIndex,
                                                          scanList, similarParentIonsData,
                                                          masicOptions, dataImportUtilities, objSearchRange);

                                FindSimilarParentIonsWork(spectraCache, currentMZ, -0.666, originalIndex,
                                                          scanList, similarParentIonsData,
                                                          masicOptions, dataImportUtilities, objSearchRange);
                            }
                        }
                        while (similarParentIonsData.IonInUseCount > ionInUseCountOriginal);

                        parentIonIndex += 1;
                    }

                    if (findSimilarIonsDataCount > 1)
                    {
                        if (parentIonIndex % 100 == 0)
                        {
                            UpdateProgress((short)(parentIonIndex / (double)(findSimilarIonsDataCount - 1) * 100));
                        }
                    }
                    else
                    {
                        UpdateProgress(1);
                    }

                    UpdateCacheStats(spectraCache);
                    if (masicOptions.AbortProcessing)
                    {
                        scanList.ProcessingIncomplete = true;
                        break;
                    }

                    if (parentIonIndex % 100 == 0)
                    {
                        if (DateTime.UtcNow.Subtract(lastLogTime).TotalSeconds >= 10 || parentIonIndex % 500 == 0)
                        {
                            ReportMessage("Parent Ion Index: " + parentIonIndex.ToString());
                            Console.Write(".");
                            lastLogTime = DateTime.UtcNow;
                        }
                    }
                }
                while (parentIonIndex < findSimilarIonsDataCount);

                Console.WriteLine();

                ReportMessage("FindSimilarParentIons: Update the scan numbers for the unique ions");

                // Update the optimal peak apex scan numbers for the unique ions
                ionUpdateCount = 0;
                foreach (var uniqueMzListItem in similarParentIonsData.UniqueMZList)
                {
                    for (var matchIndex = 0; matchIndex <= uniqueMzListItem.MatchCount - 1; matchIndex++)
                    {
                        var parentIonMatchIndex = uniqueMzListItem.MatchIndices[matchIndex];

                        if (scanList.ParentIons[parentIonMatchIndex].MZ > 0)
                        {
                            if (scanList.ParentIons[parentIonMatchIndex].OptimalPeakApexScanNumber != uniqueMzListItem.ScanNumberMaxIntensity)
                            {
                                ionUpdateCount += 1;
                                scanList.ParentIons[parentIonMatchIndex].OptimalPeakApexScanNumber = uniqueMzListItem.ScanNumberMaxIntensity;
                                scanList.ParentIons[parentIonMatchIndex].PeakApexOverrideParentIonIndex = uniqueMzListItem.ParentIonIndexMaxIntensity;
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                ReportError("Error in FindSimilarParentIons", ex, clsMASIC.eMasicErrorCodes.FindSimilarParentIonsError);
                return false;
            }
        }

        private void FindSimilarParentIonsWork(
            clsSpectraCache spectraCache,
            double searchMZ,
            double searchMZOffset,
            int originalIndex,
            clsScanList scanList,
            clsSimilarParentIonsData similarParentIonsData,
            clsMASICOptions masicOptions,
            DataInput.clsDataImport dataImportUtilities,
            clsSearchRange objSearchRange)
        {
            var sicOptions = masicOptions.SICOptions;
            var binningOptions = masicOptions.BinningOptions;

            if (objSearchRange.FindValueRange(searchMZ + searchMZOffset, sicOptions.SimilarIonMZToleranceHalfWidth, out var indexFirst, out var indexLast))
            {
                for (var matchIndex = indexFirst; matchIndex <= indexLast; matchIndex++)
                {
                    // See if the matches are unused and within the scan tolerance
                    var matchOriginalIndex = similarParentIonsData.MZPointerArray[matchIndex];

                    if (similarParentIonsData.IonUsed[matchOriginalIndex])
                        continue;

                    float timeDiff;

                    var uniqueMzListItem = similarParentIonsData.UniqueMZList.Last();
                    var sicStats = scanList.ParentIons[matchOriginalIndex].SICStats;

                    if (sicStats.ScanTypeForPeakIndices == clsScanList.eScanTypeConstants.FragScan)
                    {
                        if (scanList.FragScans[sicStats.PeakScanIndexMax].ScanTime < float.Epsilon &&
                            uniqueMzListItem.ScanTimeMaxIntensity < float.Epsilon)
                        {
                            // Both elution times are 0; instead of computing the difference in scan time, compute the difference in scan number, then convert to minutes assuming the acquisition rate is 1 Hz (which is obviously a big assumption)
                            timeDiff = (float)(Math.Abs(scanList.FragScans[sicStats.PeakScanIndexMax].ScanNumber - uniqueMzListItem.ScanNumberMaxIntensity) / 60.0);
                        }
                        else
                        {
                            timeDiff = Math.Abs(scanList.FragScans[sicStats.PeakScanIndexMax].ScanTime - uniqueMzListItem.ScanTimeMaxIntensity);
                        }
                    }
                    else if (scanList.SurveyScans[sicStats.PeakScanIndexMax].ScanTime < float.Epsilon && uniqueMzListItem.ScanTimeMaxIntensity < float.Epsilon)
                    {
                        // Both elution times are 0; instead of computing the difference in scan time, compute the difference in scan number, then convert to minutes assuming the acquisition rate is 1 Hz (which is obviously a big assumption)
                        timeDiff = (float)(Math.Abs(scanList.SurveyScans[sicStats.PeakScanIndexMax].ScanNumber - uniqueMzListItem.ScanNumberMaxIntensity) / 60.0);
                    }
                    else
                    {
                        timeDiff = Math.Abs(scanList.SurveyScans[sicStats.PeakScanIndexMax].ScanTime - uniqueMzListItem.ScanTimeMaxIntensity);
                    }

                    if (timeDiff <= sicOptions.SimilarIonToleranceHalfWidthMinutes)
                    {
                        // Match is within m/z and time difference; see if the fragmentation spectra patterns are similar

                        var similarityScore = CompareFragSpectraForParentIons(scanList, spectraCache, originalIndex, matchOriginalIndex, binningOptions, sicOptions.SICPeakFinderOptions.MassSpectraNoiseThresholdOptions, dataImportUtilities);

                        if (similarityScore > sicOptions.SpectrumSimilarityMinimum)
                        {
                            // Yes, the spectra are similar
                            // Add this parent ion to uniqueMzListItem
                            AppendParentIonToUniqueMZEntry(scanList, matchOriginalIndex, uniqueMzListItem, searchMZOffset);

                            similarParentIonsData.IonUsed[matchOriginalIndex] = true;
                            similarParentIonsData.IonInUseCount += 1;
                        }
                    }
                }
            }
        }

        public static double GetParentIonToleranceDa(clsSICOptions sicOptions, double parentIonMZ)
        {
            return GetParentIonToleranceDa(sicOptions, parentIonMZ, sicOptions.SICTolerance);
        }

        public static double GetParentIonToleranceDa(clsSICOptions sicOptions, double parentIonMZ, double parentIonTolerance)
        {
            if (sicOptions.SICToleranceIsPPM)
            {
                return clsUtilities.PPMToMass(parentIonTolerance, parentIonMZ);
            }

            return parentIonTolerance;
        }
    }
}