﻿Public Class clsSICPeakFinderOptions

#Region "Properties"

    ''' <summary>
    ''' Intensity Threshold Fraction Max
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Value between 0 and 1; default: 0.01</remarks>
    Public Property IntensityThresholdFractionMax As Single
        Get
            Return mIntensityThresholdFractionMax
        End Get
        Set(value As Single)
            If value < 0 Or value > 1 Then value = 0.01
            mIntensityThresholdFractionMax = value
        End Set
    End Property

    ''' <summary>
    ''' Intensity Threshold Absolute Minimum
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Default: 0</remarks>
    Public Property IntensityThresholdAbsoluteMinimum As Single

    Public Property SICBaselineNoiseOptions As clsBaselineNoiseOptions

    ''' <summary>
    ''' Maximum distance that the edge of an identified peak can be away from the scan number that the parent ion was observed in if the identified peak does not contain the parent ion
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Default: 0</remarks>
    Public Property MaxDistanceScansNoOverlap As Integer
        Get
            Return mMaxDistanceScansNoOverlap
        End Get
        Set(value As Integer)
            If value < 0 Or value > 10000 Then value = 0
            mMaxDistanceScansNoOverlap = value
        End Set
    End Property
    ''' <summary>
    ''' Maximum fraction of the peak maximum that an upward spike can be to be included in the peak
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Default: 0.20</remarks>
    Public Property MaxAllowedUpwardSpikeFractionMax As Single
        Get
            Return mMaxAllowedUpwardSpikeFractionMax
        End Get
        Set(value As Single)
            If value < 0 Or value > 1 Then value = 0.2
            mMaxAllowedUpwardSpikeFractionMax = value
        End Set
    End Property

    ''' <summary>
    ''' Multiplied by scaled S/N for the given spectrum to determine the initial minimum peak width (in scans) to try.  Scaled "S/N" = Math.Log10(Math.Floor("S/N")) * 10
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Default: 0.5</remarks>
    Public Property InitialPeakWidthScansScaler As Single
        Get
            Return mInitialPeakWidthScansScaler
        End Get
        Set(value As Single)
            If value < 0.001 Or value > 1000 Then value = 0.5
            mInitialPeakWidthScansScaler = value
        End Set
    End Property
    ''' <summary>
    ''' Maximum initial peak width to allow
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Default: 30</remarks>
    Public Property InitialPeakWidthScansMaximum As Integer
        Get
            Return mInitialPeakWidthScansMaximum
        End Get
        Set(value As Integer)
            If value < 3 Or value > 1000 Then value = 6
            mInitialPeakWidthScansMaximum = value
        End Set
    End Property
    Public Property FindPeaksOnSmoothedData As Boolean

    Public Property SmoothDataRegardlessOfMinimumPeakWidth As Boolean

    ''' <summary>
    ''' Use Butterworth smoothing
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>UseButterworthSmooth takes precedence over UseSavitzkyGolaySmooth</remarks>
    Public Property UseButterworthSmooth As Boolean

    Public Property ButterworthSamplingFrequency As Single
    Public Property ButterworthSamplingFrequencyDoubledForSIMData As Boolean

    ''' <summary>
    ''' Use Savitzky Golay smoothing
    ''' </summary>
    ''' <returns></returns>
    Public Property UseSavitzkyGolaySmooth As Boolean

    ''' <summary>
    ''' Even number, 0 or greater; 0 means a moving average filter, 2 means a 2nd order Savitzky Golay filter
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>Default: 0</remarks>
    Public Property SavitzkyGolayFilterOrder As Short
        Get
            Return mSavitzkyGolayFilterOrder
        End Get
        Set(value As Short)

            ' Polynomial order should be between 0 and 6
            If value < 0 Or value > 6 Then value = 0

            ' Polynomial order should be even
            If value Mod 2 <> 0 Then value -= CShort(1)
            If value < 0 Then value = 0

            mSavitzkyGolayFilterOrder = value
        End Set
    End Property

    Public Property MassSpectraNoiseThresholdOptions As clsBaselineNoiseOptions

#End Region

#Region "Classwide variables"
    Private mInitialPeakWidthScansMaximum As Integer = 30
    Private mInitialPeakWidthScansScaler As Single = 0.5
    Private mIntensityThresholdFractionMax As Single = 0.01
    Private mMaxAllowedUpwardSpikeFractionMax As Single = 0.2
    Private mMaxDistanceScansNoOverlap As Integer
    Private mSavitzkyGolayFilterOrder As Short
#End Region

End Class