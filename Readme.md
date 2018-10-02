# MASIC (MS/MS Automated Selected Ion Chromatogram Generator)

MASIC generates selected ion chromatograms (SICs) for all of the parent ions 
chosen for fragmentation in an LC-MS/MS analysis.  The SICs are generated 
using the LC-MS data, then each SIC is processed using a peak finding 
algorithm to characterize the chromatographic peaks, providing peak statistics 
including elution time of the peak apex, peak area, and peak signal/noise.
The program can read Finnigan .Raw files, mzXML files, mzData files, or 
.cdf/.mgf combo files.  Results are saved both as flat files (.txt) and 
in an XML file that can be read using the accompanying graphical results 
browser.  The browser provides a fast, graphical method for browsing the SICs 
identified by MASIC, allowing the user to sort and filter the SIC list as desired.

The application note describing MASIC is Monroe ME et. al.
Comput Biol Chem. 2008 Jun;32(3):215-217.  More info at
https://www.ncbi.nlm.nih.gov/pubmed/?term=18440872

## Installation

Double click the MASIC_installer.msi file to install.\
The program shortcut can be found at Start Menu -> Programs -> PAST Toolkit -> MASIC\
The MASIC browser shortcut can be found at PAST Toolkit -> MASIC Browser

### MSFileReader

In order to process Thermo .Raw files, you must download and install the 
MSFileReader by creating an account at https://thermo.flexnetoperations.com/control/thmo/login
Next, login and choose "Utility Software" and look for MSFileReader 3.1 SP4 (or newer)

When installing enable both features:
* MSFileReader for 64 bit
* MSFileReader for 32 bit

## Custom SIC Values

You can optionally define custom values to instruct MASIC to create SICs of
specific m/z values centered around a certain time point in the input file.
You can define the custom values via three methods:
1. Using the Custom SIC Options tab in the GUI
2. Using a tab-delimited text file, which you select in the GUI 
   or via the "CustomMZFile" setting in the "CustomSICValues" section of a MASIC XML parameter file
3. Using the "MZList", "ScanCenterList", and "ScanCommentList" settings in the "CustomSICValues" section of a MASIC XML parameter file

For all three methods, you can define three types of time modes:
* Absolute: absolute scan number
* Relative: relative time ranging from 0 (the first scan) to 1 (the last scan)
* AcquisitionTime: scan acquisition time, aka elution time (in minutes)

When defining custom SIC values, you must provide, at a minimum, the m/z value
to search for.  If no other values are provided, the default m/z search
tolerance will be used (defined on the SIC Options tab in the GUI), and the 
entire dataset will be searched for the m/z value.  You can optionally also define:
1. A custom m/z tolerance to use for each m/z
  * If 0, the default SIC tolerance will be used
2. The central scan number or scan acquisition time you want the search to be centered around
  * If 0, will be set to the mid-point of the dataset
3. The scan or time tolerance to search forward or backward from the central scan or time
  * If 0,  will use the default scan or time tolerance
4. A comment to associate with each custom m/z search value

If you use the GUI to define the Custom SIC values, then the column titles shown in the 
Custom SIC Values table will change based on the Tolerance Type defined.

If you define a tab-delimited text file from which to read the Custom SIC 
search details, the column headers present in the text file define the 
search tolerances to use.  Files CustomMZList.txt and CustomMZList_AcqTime.txt,
which are installed with MASIC, show the proper format for a Custom SIC Values
file.  At a minimum, the file must have a column titled "MZ" which specifies
the custom m/z values to search for.  If no other columns are present,
a default tolerance is used for the m/z tolerance and the entire file will be
searched for each custom m/z file.  If you add other columns of data to the
file, additional tolerances can be defined.  Allowable column names are:
MZToleranceDa, ScanCenter, ScanTolerance, and Comment

The data in columns ScanCenter and ScanTolerance can be absolute scan numbers, 
relative scan numbers, or acquisition time values.  The mode to use can be
defined in the GUI using the "Tolerance Type" option on the Custom SIC Options
tab.  Alternatively, if running MASIC from the Command Line, the mode to
use must be defined using the "ScanType" setting in section "CustomSICValues"
of an XML parameter file ("ScanType" can be "Absolute", "Relative", or 
"AcquisitionTime").

Instead of using column names ScanCenter and ScanTolerance you can alternatively
use the column names ScanTime and TimeTolerance.  When these columns are present,
then the values in those columns will be treated as AcquisitionTime values,
regardless of the global custom SIC Tolerance Type setting defined in the GUI
or in the XML parameter file.

## Reporter Ions

Enable the "Generate Reporter Ion Stats" option to instruct MASIC to look 
for standard reporter ion masses and to save their observed intensities in
file _ReporterIons.txt.  Supported reporter ion modes are:
* Mode 1, iTraq ions 114.1112, 115.1083, 116.1116, and 117.115 m/z
* Mode 2, ITraq ETD: 101.107, 102.104, and 104.1107 m/z
* Mode 3, TMT 2: 126.1283 and 127.1316
* Mode 4, TMT 6: 126.1283, 127.1316, 128.135, 129.1383, 130.1417, and 131.1387
* Mode 5, ITraq 8 (high res): 113.107873, 114.111228, 115.108263, 116.111618, 117.114973, 118.112008, 119.115363, and 121.122072
* Mode 6, ITraq 8 (low res): same ions as Mode 5, but corrects for contamination from the immonium ion at 120.08131
* Mode 7, PCGalnaz: 300.13 and 503.21 m/z
* Mode 8, HemeCFragment: 616.1767 and 617.1845
* Mode 9, LycAcetFragment: 126.09134 and 127.094695
* Mode 10, TMT 10: 126.127726, 127.124761, 127.131081, 128.128116, 128.134436, 129.131471, 129.137790, 130.134825, 130.141145, 131.138180
* Mode 11, OGlcNAc: 204.0872, 300.13079, and 503.21017
* Mode 12, Fracking Amine 20160217: 157.089, 170.097, and 234.059
* Mode 13, FSFACustomCarbonyl
* Mode 14, FSFACustomCarboxylic
* Mode 15, FSFACustomHydroxyl
* Mode 16, TMT 11: 126.127726, 127.124761, 127.131081, 128.128116, 128.134436, 129.131471, 129.137790, 130.134825, 130.141145, 131.138180, 131.144499

## MRM Data

When processing Thermo-Finnigan MRM data files, a file named _MRMSettings.txt 
will be created listing the parent and daughter m/z values monitored via 
selected reaction monitoring.  You can optionally export detailed MRM 
intensity data using the "MRM Data List" and/or "MRM Intensity Crosstab" 
options.  The MRM Data List file is a long, narrow file with only a few
columns: Scan, MRM_Parent_MZ, MRM_Daughter_MZ, and MRM_Daughter_Intensity.
In contrast, the MRM Intensity Crosstab file is a wide, rectangular file
that presents the data in a crosstab format (aka PivotTable).  Here, each
column in the crosstab corresponds to the intensity values over time for a 
given parent m/z to daughter m/z transition being monitored.

## IonCountRaw vs. IonCount

In the ScanStats file, IonCountRaw is the number MASIC m/z values present in 
each spectrum.  IonCount is the number of points that MASIC keeps in memory and 
examines when creating SICs.  For LTQ-FT data with scans containing lots of high 
resolution data, MASIC compresses the data a bit to combine m/z values that are 
within ~0.05 m/z (thus, combine 1000.05 and 1000.052 as simply 1000.051).
Additionally, for scans with a lot of low quality, low intensity data, MASIC 
discards the low intensity data.  The IonCount value would let you see which 
scans MASIC is discarding some data, for whatever reason.

## Contacts

Written by Matthew Monroe for the Department of Energy (PNNL, Richland, WA)\
Copyright 2005, Battelle Memorial Institute.  All Rights Reserved.\
E-mail: matthew.monroe@pnnl.gov or proteomics@pnnl.gov\
Website: https://omics.pnl.gov/ or https://panomics.pnnl.gov/\

## License

MASIC is licensed under the 2-Clause BSD License; you may not use this file except 
in compliance with the License.  You may obtain a copy of the License at 
https://opensource.org/licenses/BSD-2-Clause

Copyright 2018 Battelle Memorial Institute
