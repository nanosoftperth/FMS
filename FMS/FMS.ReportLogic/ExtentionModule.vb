' Declarations will typically be in a separate module.
Imports System.Runtime.CompilerServices
Imports FMS.Business.DataObjects.FeatureListConstants

Module ExtentionModule


    <Extension()>
    Public Sub PrintAndPunctuate(ByVal aString As String,
                                 ByVal punc As String)
        Console.WriteLine(aString & punc)
    End Sub

#Region "dates & nullables"


    <Extension()>
    Public Function StartOfWeek(dt As Date, startDayOfWeek As DayOfWeek)

        Dim diff As Integer = dt.DayOfWeek - startDayOfWeek

        If diff < 0 Then diff += 7

        Return dt.AddDays(-1 * diff).Date

    End Function

    <Extension()>
    Public Function timezoneToPerth(ByVal d As Date?) As Date?

        Dim thisd As New Date?

        If d.HasValue Then thisd = d.Value.timezoneToPerth
        Return thisd

    End Function

    <Extension()>
    Public Function timezoneToPerth(ByVal d As Date) As Date
        Return d.AddHours(Business.SingletonAccess.ClientSelected_TimeZone.Offset_FromHQToPerth)
    End Function


    <Extension()>
    Public Function timezoneToClient(ByVal d As Date?) As Date?

        Dim thisd As New Date?

        If d.HasValue Then thisd = d.Value.timezoneToClient
        Return thisd

    End Function

    <Extension()>
    Public Function timezoneToClient(ByVal d As Date) As Date
        Return d.AddHours(Business.SingletonAccess.ClientSelected_TimeZone.Offset_FromPerthToHQ)
    End Function

#End Region

End Module
