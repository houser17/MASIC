﻿Option Strict On

Public Class clsMSSpectrum

    ' Used to track the MZ and Intensity values of a given mass spectrum

    ' Written by Matthew Monroe for the Department of Energy (PNNL, Richland, WA)
    ' Copyright 2009, Battelle Memorial Institute.  All Rights Reserved.
    ' Started October 26, 2009
    '
    ' Last modified March 21, 2005

    Public Const DEFAULT_SPECTRUM_ION_COUNT As Integer = 500

    Public ScanNumber As Integer                        ' 0 if not in use
    Public IonCount As Integer
    Public IonsMZ() As Double                           ' 0-based array, ranging from 0 to IonCount-1; note that IonsMZ.Length could be > IonCount, so do not use .Length to determine the data count
    Public IonsIntensity() As Single                    ' 0-based array, ranging from 0 to IonCount-1; note that IonsIntensity.Length could be > IonCount, so do not use .Length to determine the data count


    Public Sub New()

        ScanNumber = 0
        IonCount = 0

        ReDim IonsMZ(DEFAULT_SPECTRUM_ION_COUNT - 1)
        ReDim IonsIntensity(DEFAULT_SPECTRUM_ION_COUNT - 1)
    End Sub

    Public Sub CopyTo(ByRef objTarget As clsMSSpectrum)
        Me.Copy(Me, objTarget)
    End Sub

    Public Sub Copy(ByRef objSource As clsMSSpectrum, ByRef objTarget As clsMSSpectrum)

        objTarget.ScanNumber = objSource.ScanNumber
        objTarget.IonCount = objSource.IonCount

        ReDim objTarget.IonsMZ(objSource.IonCount - 1)
        ReDim objTarget.IonsIntensity(objSource.IonCount - 1)

        Array.Copy(objSource.IonsMZ, objTarget.IonsMZ, objSource.IonCount)
        Array.Copy(objSource.IonsIntensity, objTarget.IonsIntensity, objSource.IonCount)
    End Sub

End Class
