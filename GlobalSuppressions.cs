﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Design", "RCS1075:Avoid empty catch clause that catches System.Exception.",
    Justification = "This design pattern is used to silently handle several types of non-fatal errors", Scope = "module")]

[assembly: SuppressMessage("Performance", "RCS1197:Optimize StringBuilder.Append/AppendLine call.",
    Justification = "Unnecessary optimization", Scope = "module")]

[assembly: SuppressMessage("Usage", "RCS1246:Use element access.", Justification = "Prefer to use .First() instead of [0]", Scope = "module")]

[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsSICDetails")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsSICProcessing")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsDataOutput")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsDataOutput.OutputFileTypeConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsCorrelation")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsCorrelation.cmCorrelationMethodConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsCustomMZSearchSpec")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsCustomSICList")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsCustomSICList.CustomSICScanTypeConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsDataAggregation")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsDatabaseAccess")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsFilterDataArrayMaxCount")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsFragScanInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsITraqIntensityCorrection")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsITraqIntensityCorrection.CorrectionFactorsiTRAQ4Plex")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsITraqIntensityCorrection.udtIsotopeContributionType")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMASIC")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMASIC.MasicErrorCodes")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMASIC.ProcessingStepConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMasicEventNotifier")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMRMProcessing")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMRMScanInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMSSpectrum")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMzBinInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsMzSearchInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsParentIonInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsParentIonProcessing")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsProcessingStats")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIonInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIonProcessor")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIonProcessor.clsReportIonInfoComparer")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIons")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsReporterIons.ReporterIonMassModeConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsScanInfo")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsScanList")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsScanList.ScanTypeConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsScanList.ScanOrderPointerType")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsScanNumScanTimeConversion")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsScanTracking")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsSearchRange")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsSearchRange.eDataTypeToUse")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsSICStats")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsSpectraCache")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsSpectraCache.eCacheStateConstants")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.clsUtilities")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsCustomSICListReader")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsDataImport")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsDataImportMGFandCDF")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsDataImportMSXml")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataInput.clsDataImportThermoRaw")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsBPIWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsExtendedStatsWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsOutputFileHandles")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsSICStatsWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsSpectrumDataWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsThermoMetadataWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DataOutput.clsXMLResultsWriter")]
[assembly: SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Legacy name", Scope = "type", Target = "~T:MASIC.DatasetStats.clsDatasetStatsSummarizer")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.clsReporterIons.UpdateMZIntensityFilterIgnoreRange")]
[assembly: SuppressMessage("General", "RCS1118:Mark local variable as const.", Justification = "Entirely unnecessary", Scope = "member", Target = "~M:MASIC.Centroider.EstimateResolution(System.Double,System.Double,System.Boolean)~System.Double")]
[assembly: SuppressMessage("Readability", "RCS1123:Add parentheses when necessary.", Justification = "Parentheses not needed", Scope = "member", Target = "~M:MASIC.DataInput.clsDataImportThermoRaw.ExtractScanInfoCheckRange(ThermoRawFileReader.XRawFileIO,ThermoRawFileReader.clsScanInfo,MASIC.clsScanList,MASIC.clsSpectraCache,MASIC.DataOutput.clsDataOutput,System.Double)~System.Boolean")]
