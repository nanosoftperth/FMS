' Declarations will typically be in a separate module.
Imports System.Runtime.CompilerServices
Imports FMS.Business.DataObjects.FeatureListConstants

Module ExtentionModule


    <Extension()>
    Public Sub PrintAndPunctuate(ByVal aString As String,
                                 ByVal punc As String)
        Console.WriteLine(aString & punc)
    End Sub

    '<Extension()>
    'Public Function ToStringForReports(t As System.TimeSpan) As String
    '    Return String.Format("{0:00}:{1:00}:{2:00}", t.TotalHours, t.Minutes, t.Seconds)
    'End Function

    <Extension()>
    Public Function UserAuthorisationCheck(thisPage As System.Web.UI.Page,
                                           fml As FeatureListAccess) As Boolean

        If Membership.GetUser Is Nothing Then
            FormsAuthentication.RedirectToLoginPage()
            Return False
        End If

        If thisPage.IsPostBack Then Exit Function

        'check if the user has access to the page
        If Not FMS.Business.ThisSession.User.GetIfAccessToFeature(fml) Then _
                        thisPage.Response.Redirect("NoAccessPage.aspx")

        Return True

    End Function

    <Extension()>
    Public Sub RedirectAsNoAccess(thisPage As System.Web.UI.Page, Optional additionalMessage As String = "")

        'redirect to the "no access" page with the message as to why
        thisPage.Response.Redirect("~/NoAccessPage.aspx?additionalMessage=" & additionalMessage, False)

    End Sub

    <Extension()>
    Public Function timezoneToPerth(ByVal d As Date?) As Date
        If d.HasValue Then Return d.Value.timezoneToPerth Else Return Nothing
    End Function

    <Extension()>
    Public Function timezoneToPerth(ByVal d As Date) As Date
        Return d.AddHours(Business.SingletonAccess.ClientSelected_TimeZone.Offset_FromHQToPerth)
    End Function


    <Extension()>
    Public Function timezoneToClient(ByVal d As Date?) As Date
        If d.HasValue Then Return d.Value.timezoneToClient Else Return Nothing
    End Function

    <Extension()>
    Public Function timezoneToClient(ByVal d As Date) As Date
        Return d.AddHours(Business.SingletonAccess.ClientSelected_TimeZone.Offset_FromPerthToHQ)
    End Function

    <Extension()>
    Public Function getDateFromJavascriptFormat(dateStr As String) As Date


        Dim browserDate As Date = dateStr.Substring(0, dateStr.IndexOf("GMT"))
        'we dont use "timezoneoffset", however, this is left for in future incase we would eventually like to use it.
        Dim timeZoneOffset As Decimal = (dateStr.Substring(dateStr.IndexOf("GMT"), dateStr.Length() - dateStr.IndexOf("GMT")).Split("(")(0).Replace("GMT", "")) / 100

        Return CDate(browserDate)

    End Function


End Module
