﻿Public Class clsDataObjects

    Friend Structure udtMZSearchInfoType
        Public SearchMZ As Double

        Public MZIndexStart As Integer
        Public MZIndexEnd As Integer
        Public MZIndexMidpoint As Integer

        Public MZTolerance As Double
        Public MZToleranceIsPPM As Boolean

        Public MaximumIntensity As Single
        Public ScanIndexMax As Integer

        Public BaselineNoiseStatSegments() As MASICPeakFinder.clsMASICPeakFinder.udtBaselineNoiseStatSegmentsType

        Public Overrides Function ToString() As String
            Return "m/z: " & SearchMZ.ToString("0.0") & ", Intensity: " & MaximumIntensity.ToString("0.0")
        End Function
    End Structure

    Friend Structure udtMZBinListType
        Public MZ As Double
        Public MZTolerance As Double
        Public MZToleranceIsPPM As Boolean

        Public Overrides Function ToString() As String
            If MZToleranceIsPPM Then
                Return "m/z: " & MZ.ToString("0.0") & ", MZTolerance: " & MZTolerance.ToString("0.0") & " ppm"
            Else
                Return "m/z: " & MZ.ToString("0.0") & ", MZTolerance: " & MZTolerance.ToString("0.000") & " Da"
            End If

        End Function
    End Structure

End Class