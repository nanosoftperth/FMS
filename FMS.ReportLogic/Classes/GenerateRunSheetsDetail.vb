Public Class CacheGenerateRunSheetsDetail
    Public Property LineValues As List(Of GenerateRunSheetsDetail)
    Public Property ParamDate As String
    Public Property ParamDay As String
    Public Property ParamSignature As String
    Public Property ParamMessage As String
End Class
Public Class GenerateRunSheetsDetail
    Public Property SortOrder As String
    Public Property Cid As System.Nullable(Of Integer)
    Public Property SiteName As String
    Public Property Add As String
    Public Property Suburb As String
    Public Property SiteContactName As String
    Public Property SiteContactPhone As String
    Public Property SiteContactMobile As String
    Public Property RunDriver As System.Nullable(Of Integer)
    Public Property GeneralSiteServiceComments As String
    Public Property RunNumber As System.Nullable(Of Integer)
    Public Property DriverName As String
    Public Property RunDescription As String
    Public Property Notes As String
    Public Property ApplicationID As System.Nullable(Of System.Guid)

    Public SkipThisOne As Boolean = False
    Public strTemp As String
    Public Function GenerateRunSheets(ByVal RunSheets As RunSheets) As String
        Dim strStatus As String = ""
        Try
            FMS.Business.DataObjects.tblzGenerateRunSheets.DeleteAllGenerateRunSheets()
            strStatus = ProcessRecords(RunSheets)
        Catch ex As Exception
            strStatus = "Failed"
        End Try

        Return strStatus
    End Function

    Public Function ProcessRecords(RunSheets As RunSheets) As String
        Dim Run = FMS.Business.DataObjects.usp_GetTblRuns.GetTblRuns(RunSheets.SpecificRun)
        If Run.Count > 0 Then
            For Each rsin In Run
                ProcessWeekly(RunSheets, rsin)
                SpecificDates(RunSheets, rsin)
            Next
            Return "No runs selected for processing"
        Else
            Return ""
        End If
    End Function
    Public Sub ProcessWeekly(RunSheets As RunSheets, rsin As FMS.Business.DataObjects.usp_GetTblRuns)
        strTemp = RunSheets.DayOfRun
        Select Case strTemp
            Case "Monday"
                If rsin.MondayRun.Equals(True) Then
                    UpdateRecords(RunSheets, rsin)
                End If
            Case "Tuesday"
                If rsin.TuesdayRun.Equals(True) Then
                    UpdateRecords(RunSheets, rsin)
                End If
            Case "Wednesday"
                If rsin.WednesdayRun.Equals(True) Then
                    UpdateRecords(RunSheets, rsin)
                End If
            Case "Thursday"
                If rsin.ThursdayRun.Equals(True) Then
                    UpdateRecords(RunSheets, rsin)
                End If
            Case "Friday"
                If rsin.FridayRun.Equals(True) Then
                    UpdateRecords(RunSheets, rsin)
                End If
            Case "Saturday"
                If rsin.SaturdayRun.Equals(True) Then
                    UpdateRecords(RunSheets, rsin)
                End If
            Case "Sunday"
                If rsin.SundayRun.Equals(True) Then
                    UpdateRecords(RunSheets, rsin)
                End If
        End Select
    End Sub
    Public Sub UpdateRecords(RunSheets As RunSheets, rsin As FMS.Business.DataObjects.usp_GetTblRuns)
        Dim SqlSent2 = FMS.Business.DataObjects.usp_GetSitesAndCustomerServices.GetSitesAndCustomerServices(rsin.Rid)
        For Each rsIn2 In SqlSent2
            SkipThisOne = False
            Dim tblzGenerateRunSheet As New FMS.Business.DataObjects.tblzGenerateRunSheets()
            CheckPeriodical(RunSheets, rsIn2)
            If SkipThisOne = False Then
                tblzGenerateRunSheet.RunNumber = rsin.RunNUmber
                tblzGenerateRunSheet.RunDriver = rsin.RunDriver
                tblzGenerateRunSheet.RunDescription = rsin.RunDescription
                tblzGenerateRunSheet.Cid = rsIn2.CId
                tblzGenerateRunSheet.CSid = rsIn2.CSid
                tblzGenerateRunSheet.ServiceUnits = rsIn2.ServiceUnits
                tblzGenerateRunSheet.ProductId = rsIn2.CSid
                tblzGenerateRunSheet.ServiceComments = rsIn2.ServiceComments
                If rsIn2.ServiceSortOrderCode Is Nothing Or rsIn2.ServiceSortOrderCode = "" Then
                    tblzGenerateRunSheet.SortOrder = "A100"
                Else
                    tblzGenerateRunSheet.SortOrder = rsIn2.ServiceSortOrderCode
                End If
                AddNewTblzGenerateRunSheets(tblzGenerateRunSheet)
            End If
        Next
    End Sub
    Public Sub AddNewTblzGenerateRunSheets(GenerateRunSheet As FMS.Business.DataObjects.tblzGenerateRunSheets)
        Try
            FMS.Business.DataObjects.tblzGenerateRunSheets.Create(GenerateRunSheet)
        Catch ex As Exception
        End Try
    End Sub
    Public Sub CheckPeriodical(RunSheets As RunSheets, rsIn2 As FMS.Business.DataObjects.usp_GetSitesAndCustomerServices)
        Dim varPeriodical = FMS.Business.DataObjects.tblServiceFrequency.GetServiceFrequencyByFID(rsIn2.ServiceSortOrderCode).FirstOrDefault
        If Not varPeriodical Is Nothing AndAlso varPeriodical.Periodical Then
            SkipThisOne = True
            If rsIn2.ServiceFrequency1 = CInt(RunSheets.Month) Then
                SkipThisOne = False
            End If
            If rsIn2.ServiceFrequency2 = CInt(RunSheets.Month) Then
                SkipThisOne = False
            End If
            If rsIn2.ServiceFrequency3 = CInt(RunSheets.Month) Then
                SkipThisOne = False
            End If
            If rsIn2.ServiceFrequency4 = CInt(RunSheets.Month) Then
                SkipThisOne = False
            End If
            If rsIn2.ServiceFrequency5 = CInt(RunSheets.Month) Then
                SkipThisOne = False
            End If
            If rsIn2.ServiceFrequency6 = CInt(RunSheets.Month) Then
                SkipThisOne = False
            End If
            If rsIn2.ServiceFrequency7 = CInt(RunSheets.Month) Then
                SkipThisOne = False
            End If
            If rsIn2.ServiceFrequency8 = CInt(RunSheets.Month) Then
                SkipThisOne = False
            End If
        End If
    End Sub
    Public Sub SpecificDates(RunSheets As RunSheets, rsin As FMS.Business.DataObjects.usp_GetTblRuns)
        Dim SqlSent2 = FMS.Business.DataObjects.usp_GetSpecificDates.GetSpecificDates(RunSheets.SqlDate, rsin.Rid)
        For Each x In SqlSent2
            UpdateRecords(RunSheets, rsin)
        Next
    End Sub
End Class
Public Class RunSheets
    Public Property DateOfRun As String
    Public Property DayOfRun As String
    Public Property Month As String
    Public Property SqlDate As String
    Public Property SpecificRun As String
    Public Property PrintCustomerSignature As String
End Class
