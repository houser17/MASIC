﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Keep for reference", Scope = "member", Target = "~M:MASIC.DataOutput.clsBPIWriter.SaveICRToolsChromatogramByScan(MASIC.Options.MASICOptions,System.Collections.Generic.IList{MASIC.clsScanInfo},System.Int32,System.String,System.Boolean,System.Boolean,System.String)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Keep for reference", Scope = "member", Target = "~M:MASIC.DataOutput.clsSICStatsWriter.PopulateScanListPointerArray(System.Collections.Generic.IList{MASIC.clsScanInfo},System.Int32,System.Int32[]@)")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Keep for reference", Scope = "member", Target = "~M:MASIC.Plots.PlotContainer.PointSizeToEm(System.Int32)~System.Double")]
[assembly: SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Keep for reference", Scope = "member", Target = "~M:MASIC.Plots.PlotContainer.PointSizeToPixels(System.Int32)~System.Int32")]
[assembly: SuppressMessage("Design", "RCS1075:Avoid empty catch clause that catches System.Exception.", Justification = "This design pattern is used to silently handle several types of non-fatal errors", Scope = "module")]
[assembly: SuppressMessage("General", "RCS1118:Mark local variable as const.", Justification = "Entirely unnecessary", Scope = "member", Target = "~M:MASIC.Centroider.EstimateResolution(System.Double,System.Double,System.Boolean)~System.Double")]
[assembly: SuppressMessage("Performance", "RCS1197:Optimize StringBuilder.Append/AppendLine call.", Justification = "Unnecessary optimization", Scope = "module")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses are correct", Scope = "member", Target = "~M:MASIC.DataInput.clsDataImportMSXml.ExtractFragmentationScan(MASIC.clsScanList,MASIC.clsSpectraCache,MASIC.DataOutput.clsDataOutput,MSDataFileReader.clsSpectrumInfo,MASIC.clsMSSpectrum,MASIC.Options.SICOptions,System.Boolean,MSDataFileReader.clsSpectrumInfoMzXML,PSI_Interface.MSData.SimpleMzMLReader.SimpleSpectrum)~System.Boolean")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsCorrelation.BetaCF(System.Double,System.Double,System.Double)~System.Double")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsCorrelation.BetaI(System.Double,System.Double,System.Double)~System.Double")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsCorrelation.BinData(System.Collections.Generic.List{System.Single},System.Collections.Generic.List{System.Single},System.Collections.Generic.List{System.Single},System.Collections.Generic.List{System.Single})~System.Boolean")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsCorrelation.CorrelateKendall(System.Collections.Generic.IReadOnlyList{System.Single},System.Collections.Generic.IReadOnlyList{System.Single},System.Single@,System.Single@,System.Single@)")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsCorrelation.CorrelatePearson(System.Collections.Generic.IReadOnlyList{System.Single},System.Collections.Generic.IReadOnlyList{System.Single},System.Single@,System.Single@,System.Single@)")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsCorrelation.CorrelateSpearman(System.Collections.Generic.IReadOnlyCollection{System.Single},System.Collections.Generic.IReadOnlyCollection{System.Single},System.Single@,System.Single@,System.Single@,System.Single@,System.Single@)")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsCorrelation.CRank(System.Int32,System.Collections.Generic.IList{System.Single},System.Single@)")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsCorrelation.ErfCC(System.Double)~System.Double")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsFilterDataArrayMaxCount.FilterDataByMaxDataCountToKeep")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsITraqIntensityCorrection.DefineIsotopeContribution(System.Single,System.Single,System.Single,System.Single,System.Single)~MASIC.clsITraqIntensityCorrection.IsotopeContributionType")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsMRMProcessing.ExportMRMDataToDisk(MASIC.clsScanList,MASIC.clsSpectraCache,System.Collections.Generic.IReadOnlyList{MASIC.clsMRMScanInfo},System.Collections.Generic.IReadOnlyList{MASIC.clsMRMProcessing.SRMListType},System.String,System.String)~System.Boolean")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsParentIonProcessing.AppendParentIonToUniqueMZEntry(MASIC.clsScanList,System.Int32,MASIC.clsUniqueMZListItem,System.Double)")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsReporterIons.UpdateMZIntensityFilterIgnoreRange")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsScanNumScanTimeConversion.ScanOrAcqTimeToAbsolute(MASIC.clsScanList,System.Single,MASIC.clsCustomSICList.CustomSICScanTypeConstants,System.Boolean)~System.Int32")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsScanNumScanTimeConversion.ScanOrAcqTimeToScanTime(MASIC.clsScanList,System.Single,MASIC.clsCustomSICList.CustomSICScanTypeConstants,System.Boolean)~System.Single")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsSICProcessing.ComputeMzSearchChunkProgress(System.Int32,System.Int32,System.Int32,System.Int32,System.Double)~System.Int16")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsSICProcessing.ExtractSICDetailsFromFullSIC(System.Int32,System.Collections.Generic.List{MASICPeakFinder.clsBaselineNoiseStatsSegment},System.Int32,System.Int32[,],System.Double[,],System.Double[,],MASIC.clsScanList,System.Int32,MASIC.clsSICDetails,MASIC.Options.MASICOptions,MASIC.clsScanNumScanTimeConversion,System.Boolean,System.Single)~MASICPeakFinder.clsSICStatsPeak")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsUtilities.ConvoluteMass(System.Double,System.Int16,System.Int16,System.Double)~System.Double")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.DataInput.clsDataImportMGFandCDF.ExtractScanInfoFromMGFandCDF(System.String,MASIC.clsScanList,MASIC.clsSpectraCache,MASIC.DataOutput.clsDataOutput,System.Boolean,System.Boolean)~System.Boolean")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.DataInput.clsDataImportMGFandCDF.InterpolateRTandFragScanNumber(System.Collections.Generic.IList{MASIC.clsScanInfo},System.Int32,System.Int32,System.Int32@)~System.Single")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.DataInput.clsDataImportMSXml.ExtractScanInfoCheckRange(MASIC.clsMSSpectrum,MSDataFileReader.clsSpectrumInfo,PSI_Interface.MSData.SimpleMzMLReader.SimpleSpectrum,MASIC.clsScanList,MASIC.clsSpectraCache,MASIC.DataOutput.clsDataOutput,System.Double,System.Int32)~System.Boolean")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.DataInput.clsDataImportThermoRaw.ExtractScanInfoCheckRange(ThermoRawFileReader.XRawFileIO,ThermoRawFileReader.clsScanInfo,MASIC.clsScanList,MASIC.clsSpectraCache,MASIC.DataOutput.clsDataOutput,System.Double)~System.Boolean")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.Plots.AxisInfo.SetRange(System.Double,System.Double)")]
[assembly: SuppressMessage("Redundancy", "RCS1213:Remove unused member declaration.", Justification = "Keep for reference", Scope = "member", Target = "~M:MASIC.Plots.PlotContainer.PointSizeToEm(System.Int32)~System.Double")]
[assembly: SuppressMessage("Redundancy", "RCS1213:Remove unused member declaration.", Justification = "Keep for reference", Scope = "member", Target = "~M:MASIC.Plots.PlotContainer.PointSizeToPixels(System.Int32)~System.Int32")]
[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "Keep for debugging purposes", Scope = "member", Target = "~M:MASIC.clsSICProcessing.ProcessMzSearchChunk(MASIC.Options.MASICOptions,MASIC.clsScanList,MASIC.clsDataAggregation,MASIC.DataOutput.clsDataOutput,MASIC.DataOutput.clsXMLResultsWriter,MASIC.clsSpectraCache,MASIC.clsScanNumScanTimeConversion,System.Collections.Generic.IReadOnlyList{MASIC.clsMzSearchInfo},System.Double,System.Collections.Generic.IList{System.Int32},System.Boolean,System.Int32,System.Collections.Generic.IList{System.Boolean},System.Int32@)~System.Boolean")]
[assembly: SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Keep parameter", Scope = "member", Target = "~M:MASIC.clsParentIonProcessing.CompareFragSpectraForParentIons(MASIC.clsScanList,MASIC.clsSpectraCache,System.Int32,System.Int32,MASIC.Options.BinningOptions,MASICPeakFinder.clsBaselineNoiseOptions,MASIC.DataInput.clsDataImport)~System.Single")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsBinnedData")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsCorrelation")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsCorrelation.cmCorrelationMethodConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsCustomMZSearchSpec")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsCustomSICList")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsCustomSICList.CustomSICScanTypeConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsDataAggregation")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsDatabaseAccess")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsFilterDataArrayMaxCount")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsFragScanInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsITraqIntensityCorrection")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsITraqIntensityCorrection.CorrectionFactorsiTRAQ4Plex")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMASIC")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMASIC.MasicErrorCodes")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMASIC.ProcessingStepConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMasicEventNotifier")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMRMProcessing")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMRMScanInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMSSpectrum")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMzBinInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsMzSearchInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsParentIonInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsParentIonProcessing")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsProcessingStats")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIonInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIonProcessor")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIonProcessor.clsReportIonInfoComparer")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIons")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIons.ReporterIonMassModeConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsScanInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsScanList")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsScanList.ScanOrderPointerType")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsScanList.ScanTypeConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsScanNumScanTimeConversion")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsScanTracking")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsSearchRange")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsSearchRange.eDataTypeToUse")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsSICDetails")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsSICProcessing")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsSICStats")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsSimilarParentIonsData")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsSpectraCache")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsSpectraCache.eCacheStateConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsUniqueMZListItem")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.clsUtilities")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsCustomSICListReader")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsDataImport")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsDataImportMGFandCDF")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsDataImportMSXml")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsDataImportThermoRaw")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsBPIWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsDataOutput")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsDataOutput.OutputFileTypeConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsExtendedStatsWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsOutputFileHandles")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsSICStatsWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsSpectrumDataWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsThermoMetadataWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsXMLResultsWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.DatasetStats.clsDatasetStatsSummarizer")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Allowed legacy name", Scope = "type", Target = "~T:MASIC.Options.RawDataExportOptions.eExportRawDataFileFormatConstants")]
[assembly: SuppressMessage("Usage", "RCS1146:Use conditional access.", Justification = "Leave for readability", Scope = "member", Target = "~M:MASIC.Plots.AxisInfo.GetOptions(System.Collections.Generic.List{System.String})~System.String")]
[assembly: SuppressMessage("Usage", "RCS1246:Use element access.", Justification = "Prefer to use .First() instead of [0]", Scope = "module")]
