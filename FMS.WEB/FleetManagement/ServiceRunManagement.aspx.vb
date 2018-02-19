Imports System.Reflection
Imports System.Reflection.Emit
Imports DevExpress.Web
Imports DevExpress.Web.ASPxGridView
Imports System.Web.Services

Public Class ServiceRunManagement
    Inherits System.Web.UI.Page

#Region "Events"
    Private priRundate As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack = False) Then
            Dim dtServRun As DataTable

            dtServRun = DirectCast(Session("ServiceRunTable"), DataTable)

            If (IsNothing(dtServRun) = False) Then
                Me.gvServiceRun.DataSource = dtServRun
                Me.gvServiceRun.DataBind()
            End If

            'Me.dteStart.Value = DateSerial(Now.Year, Now.Month, 1)
            'Me.dteEnd.Value = DateSerial(Now.Year, Now.Month + 1, 0)

        End If
    End Sub

    Protected Sub dteStart_Init(sender As Object, e As EventArgs)
        dteStart.Date = DateSerial(Now.Year, Now.Month, 1)
    End Sub

    Protected Sub dteEnd_Init(sender As Object, e As EventArgs)
        dteEnd.Date = DateSerial(Now.Year, Now.Month + 1, 0)
    End Sub
    Protected Sub dteStart_ValueChanged(sender As Object, e As EventArgs)

    End Sub
    Protected Sub dteEnd_ValueChanged(sender As Object, e As EventArgs)

    End Sub
    Protected Sub btnLoad_Click(sender As Object, e As EventArgs)
        Dim blnValidate As Boolean = False

        If (IsDate(Me.dteStart.Value) = True) Then
            If (IsDate(Me.dteEnd.Value) = True) Then
                If (Me.dteStart.Value <= Me.dteEnd.Value) Then
                    blnValidate = True
                Else
                    ClientScript.RegisterStartupScript(Me.[GetType](), "srvrunalert", "alert('Start date date should be earlier date or same date of end date.');", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "srvrunalert", "alert('End date date should be a valid date.');", True)
                blnValidate = False
            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "srvrunalert", "alert('Start date should be a valid date.');", True)
        End If

        If blnValidate = True Then
            PopulateServiceRunGrid()
        End If


    End Sub

    Protected Sub LoadGrid()
        Dim blnValidate As Boolean = False

        If (IsDate(Me.dteStart.Value) = True) Then
            If (IsDate(Me.dteEnd.Value) = True) Then
                If (Me.dteStart.Value <= Me.dteEnd.Value) Then
                    blnValidate = True
                Else
                    ClientScript.RegisterStartupScript(Me.[GetType](), "srvrunalert", "alert('Start date date should be earlier date or same date of end date.');", True)
                End If
            Else
                ClientScript.RegisterStartupScript(Me.[GetType](), "srvrunalert", "alert('End date date should be a valid date.');", True)
                blnValidate = False
            End If
        Else
            ClientScript.RegisterStartupScript(Me.[GetType](), "srvrunalert", "alert('Start date should be a valid date.');", True)
        End If

        If blnValidate = True Then
            PopulateServiceRunGrid()
        End If


    End Sub


    Protected Sub gvServiceRun_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)
        Dim Rundate As String = ""
        Dim TmpRundate As String = ""

        Dim ndxTech = e.DataColumn.FieldName.IndexOf("Tech_")
        If (ndxTech > -1) Then
            Dim cellVal = e.CellValue
            If (cellVal.ToString().Length) > 0 Then
                e.Cell.BackColor = System.Drawing.Color.Beige
            End If
        End If

        Dim ndxDvr = e.DataColumn.FieldName.IndexOf("Driver_")
        If (ndxDvr > -1) Then
            Dim cellVal = e.CellValue
            If (cellVal.ToString().Length) > 0 Then
                e.Cell.BackColor = System.Drawing.Color.Aqua
            End If
        End If

        If (IsDBNull(e.Cell) = False) Then
            e.Cell.Attributes.Add("onclick", "ShowPopup('" + e.DataColumn.FieldName + "');")

        End If

        Dim ndxRundate = e.DataColumn.FieldName.IndexOf("RunDate")
        If (ndxRundate > -1) Then
            Rundate = e.CellValue

        End If

        If (Rundate <> "") Then
            priRundate = Rundate
        End If

        e.Cell.Attributes("data-CI") = String.Format("{0}_{1}_{2}", e.VisibleIndex, gvServiceRun.DataColumns.IndexOf(e.DataColumn), priRundate) ' cell info

    End Sub
    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        puUnassignedRun.ShowOnPageLoad = False
    End Sub
    Protected Sub gvServiceRun_CustomJSProperties(sender As Object, e As ASPxGridViewClientJSPropertiesEventArgs)
        e.Properties("cpDataColumnMap") = gvServiceRun.DataColumns.ToDictionary(Function(c) gvServiceRun.DataColumns.IndexOf(c), Function(c) c.FieldName)
    End Sub
    Protected Sub btnSelectServiceRun_Click(sender As Object, e As EventArgs)

        '--- Set proerty value for tblRunDates
        Dim DriverID As Integer = 0
        Dim TransDate As Date = Now

        If Not Request.Cookies("DriverID") Is Nothing Then
            DriverID = Server.HtmlEncode(Request.Cookies("DriverID").Value)
        End If

        If Not Request.Cookies("RepDate") Is Nothing Then
            TransDate = Server.HtmlEncode(Request.Cookies("RepDate").Value)
        End If

        Dim objRuns = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplication(TransDate, TransDate).Where(Function(x) _
                                                                                                                           x.Driver = DriverID).ToList()
        If (objRuns.Count > 0) Then
            Session("ListRun") = objRuns
            Session("ShowDiagFrom") = "SELECTSERVICERUN"

            Me.lblDialog.Text = "Run exist on this date for this driver. Update it?"
            Me.puDialog.ShowOnPageLoad = True

        Else

            If (Me.cboRun.Value <> 0) Then
                Dim rowRundate = New FMS.Business.DataObjects.tblRunDates

                rowRundate.ApplicationID = FMS.Business.ThisSession.ApplicationID
                rowRundate.Rid = DriverID
                rowRundate.DateOfRun = TransDate

                '--- Set property values for tblRuns
                Dim blnSun = False
                Dim blnMon = False
                Dim blnTue = False
                Dim blnWed = False
                Dim blnThu = False
                Dim blnFri = False
                Dim blnSat = False

                Dim DayOfWeek = TransDate.DayOfWeek.ToString()

                Select Case DayOfWeek
                    Case "Sunday"
                        blnSun = True
                    Case "Monday"
                        blnMon = True
                    Case "Tuesday"
                        blnTue = True
                    Case "Wednesday"
                        blnWed = True
                    Case "Thursday"
                        blnThu = True
                    Case "Friday"
                        blnFri = True
                    Case "Saturday"
                        blnSat = True
                End Select

                Dim rowRuns = New FMS.Business.DataObjects.tblRuns

                'rowRuns.Rid = ndxRID
                rowRuns.RunNUmber = Convert.ToInt32(Me.cboRun.Value)
                rowRuns.RunDescription = Me.cboRun.Text
                rowRuns.RunDriver = DriverID
                rowRuns.SundayRun = blnSun
                rowRuns.MondayRun = blnMon
                rowRuns.TuesdayRun = blnTue
                rowRuns.WednesdayRun = blnWed
                rowRuns.ThursdayRun = blnThu
                rowRuns.FridayRun = blnFri
                rowRuns.SaturdayRun = blnSat
                rowRuns.InactiveRun = False

                FMS.Business.DataObjects.tblRunDates.Create(rowRundate)
                FMS.Business.DataObjects.tblRuns.Create(rowRuns)

            End If

        End If

        puUnassignedRun.ShowOnPageLoad = False
        btnLoad_Click(sender, e)
    End Sub
    Protected Sub btnDialogOK_Click(sender As Object, e As EventArgs)

        Dim DiagType = Session("ShowDiagFrom")

        Select Case DiagType.ToString().ToUpper()
            Case "SELECTSERVICERUN"
                Dim DriverID As Integer = 0
                Dim TransDate As Date = Now
                Dim runNum As Integer = 0

                Dim ListRun = Session("ListRun")

                If Not Request.Cookies("DriverID") Is Nothing Then
                    DriverID = Server.HtmlEncode(Request.Cookies("DriverID").Value)
                End If

                If (ListRun IsNot Nothing) Then

                    If (Me.cboRun.Value = 0) Then
                        If Not Request.Cookies("RepDate") Is Nothing Then
                            TransDate = Server.HtmlEncode(Request.Cookies("RepDate").Value)
                        End If
                        Dim objRunDate = New FMS.Business.DataObjects.tblRunDates

                        objRunDate.DateOfRun = TransDate
                        objRunDate.Rid = DriverID

                        Dim objRun = New FMS.Business.DataObjects.tblRuns
                        For Each item In ListRun
                            runNum = DirectCast(item, FMS.Business.DataObjects.usp_GetServiceRunDates).RunNUmber
                        Next
                        objRun.RunNUmber = runNum
                        objRun.RunDriver = DriverID

                        FMS.Business.DataObjects.tblRuns.DeleteRun(objRun)
                        FMS.Business.DataObjects.tblRunDates.DeleteRunDate(objRunDate)

                    Else
                        Dim objRun = New FMS.Business.DataObjects.tblRuns
                        objRun.RunNUmber = Convert.ToInt32(Me.cboRun.Value)
                        objRun.RunDescription = Me.cboRun.Text

                        For Each item In ListRun
                            runNum = DirectCast(item, FMS.Business.DataObjects.usp_GetServiceRunDates).RunNUmber
                        Next

                        FMS.Business.DataObjects.tblRuns.ChangeRun(DriverID, runNum, objRun)
                    End If

                    Me.puDialog.ShowOnPageLoad = False

                    btnLoad_Click(sender, e)

                End If

            Case "RUNCOMPLETED"
                Dim DriverID As Integer = 0
                Dim TransDate As Date = Now
                Dim RunNumber As Integer = 0

                If Not Request.Cookies("DriverID") Is Nothing Then
                    DriverID = Server.HtmlEncode(Request.Cookies("DriverID").Value)
                End If

                If Not Request.Cookies("RepDate") Is Nothing Then
                    TransDate = Server.HtmlEncode(Request.Cookies("RepDate").Value)
                End If

                If Not Request.Cookies("RunNumber") Is Nothing Then
                    RunNumber = Server.HtmlEncode(Request.Cookies("RunNumber").Value)
                End If

                Dim objRuns = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplication(TransDate, TransDate).Where(Function(x) _
                                                                                            x.Driver = DriverID).FirstOrDefault()
                If (objRuns IsNot Nothing) Then
                    Dim objFleetRunCompletion = FMS.Business.DataObjects.FleetRunCompletion.GetAll().Where(Function(f) _
                                                                                                            f.RunID = objRuns.RunID).ToList()
                    If (objFleetRunCompletion.Count > 0) Then

                        If (cbxCompleteRun.Checked = True) Then

                            For Each run In objFleetRunCompletion
                                Dim ListFleetRunCompletion = New FMS.Business.DataObjects.FleetRunCompletion
                                ListFleetRunCompletion.RunCompletionID = run.RunCompletionID
                                ListFleetRunCompletion.RunID = run.RunID
                                ListFleetRunCompletion.DriverID = Guid.Parse(Me.cboDriverCompleted.Value.ToString())
                                ListFleetRunCompletion.RunDate = Me.dteCompleted.Value

                                FMS.Business.DataObjects.FleetRunCompletion.Update(ListFleetRunCompletion)

                            Next

                        Else

                            For Each run In objFleetRunCompletion
                                Dim ListFleetRunCompletion = New FMS.Business.DataObjects.FleetRunCompletion
                                ListFleetRunCompletion.RunCompletionID = run.RunCompletionID
                                ListFleetRunCompletion.RunID = run.RunID
                                ListFleetRunCompletion.DriverID = Guid.Parse(Me.cboDriverCompleted.Value.ToString())
                                ListFleetRunCompletion.RunDate = Me.dteCompleted.Value

                                FMS.Business.DataObjects.FleetRunCompletion.Delete(ListFleetRunCompletion)

                            Next

                        End If

                    End If

                End If

        End Select
    End Sub
    Protected Sub btnDialogCancel_Click(sender As Object, e As EventArgs)
        Session("DialogAns") = False
        Me.puDialog.ShowOnPageLoad = False
    End Sub
    Protected Sub btnCompleteRun_Click(sender As Object, e As EventArgs)
        Dim DriverID As Integer = 0
        Dim TransDate As Date = Now
        Dim RunNumber As Integer = 0

        If Not Request.Cookies("DriverID") Is Nothing Then
            DriverID = Server.HtmlEncode(Request.Cookies("DriverID").Value)
        End If

        If Not Request.Cookies("RepDate") Is Nothing Then
            TransDate = Server.HtmlEncode(Request.Cookies("RepDate").Value)
        End If

        If Not Request.Cookies("RunNumber") Is Nothing Then
            RunNumber = Server.HtmlEncode(Request.Cookies("RunNumber").Value)
        End If

        Dim objRuns = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplication(TransDate, TransDate).Where(Function(x) _
                                                                                            x.Driver = DriverID).FirstOrDefault()
        If (objRuns IsNot Nothing) Then

            Dim objFleetRunCompletion = FMS.Business.DataObjects.FleetRunCompletion.GetAll().Where(Function(f) f.RunID = objRuns.RunID)

            If (objFleetRunCompletion.Count <= 0) Then

                Dim frc = New FMS.Business.DataObjects.FleetRunCompletion

                frc.RunID = objRuns.RunID
                frc.DriverID = Guid.Parse(Me.cboDriverCompleted.Value.ToString())
                frc.RunDate = Me.dteCompleted.Value
                frc.Notes = Me.txtCompletedNotes.Text
                Dim o As Object = ""

                FMS.Business.DataObjects.FleetRunCompletion.Create(frc)

            Else

                If (cbxCompleteRun.Checked = False) Then
                    Dim ListFleetRunCompletion = New FMS.Business.DataObjects.FleetRunCompletion
                    ListFleetRunCompletion.RunCompletionID = objFleetRunCompletion.FirstOrDefault.RunCompletionID
                    FMS.Business.DataObjects.FleetRunCompletion.Delete(ListFleetRunCompletion)
                Else
                    Session("ShowDiagFrom") = "RUNCOMPLETED"

                    Me.lblDialog.Text = "Run already completed. Update it?"
                    Me.puDialog.ShowOnPageLoad = True


                End If

            End If

            'For Each run In objRuns
            '    Dim RundID = run.RunID
            '    Dim DvrID = run.DriverID

            'Next
        End If

        puCompleteRun.ShowOnPageLoad = False

    End Sub
    Protected Sub btnCancelComplete_Click(sender As Object, e As EventArgs)
        puCompleteRun.ShowOnPageLoad = False
    End Sub

#End Region

#Region "Methods and Functions for Service Run"

    <WebMethod>
    Public Shared Function RemoveFleetRunCompletion(RunDate As Date, DriverID As Integer, RunNumber As Integer) As Boolean

        Dim RunID As Guid
        Dim DvrID As Guid

        Dim listSrvRun = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplication(RunDate, RunDate).Where(
            Function(r) r.Driver = DriverID And r.RunNUmber = RunNumber).FirstOrDefault()

        If (listSrvRun IsNot Nothing) Then
            RunID = listSrvRun.RunID
            DvrID = listSrvRun.DriverID

        End If

        Dim objList = FMS.Business.DataObjects.FleetRunCompletion.GetAll().Where(Function(r) r.RunID = RunID _
                                                                                     And r.DriverID = DvrID).ToList()
        If (objList.Count > 0) Then
            Dim row = New FMS.Business.DataObjects.FleetRunCompletion

        End If

    End Function
    <WebMethod>
    Public Shared Function IsFleetRunCompleted(RunDate As Date, DriverID As Integer, RunNumber As Integer) As List(Of FMS.Business.DataObjects.FleetRunCompletion)
        Dim RunID As Guid
        Dim DvrID As Guid

        Dim listSrvRun = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplication(RunDate, RunDate).Where(
            Function(r) r.Driver = DriverID And r.RunNUmber = RunNumber).FirstOrDefault()

        If (listSrvRun IsNot Nothing) Then
            RunID = listSrvRun.RunID
            DvrID = listSrvRun.DriverID

        End If

        Dim objListCompletion As New List(Of FMS.Business.DataObjects.FleetRunCompletion)
        Dim objList = FMS.Business.DataObjects.FleetRunCompletion.GetAll().Where(Function(r) r.RunID = RunID).ToList()

        If (objList.Count > 0) Then
            For Each run In objList
                Dim rowrun As New FMS.Business.DataObjects.FleetRunCompletion

                rowrun.DriverID = run.DriverID
                rowrun.RunCompletionID = run.RunCompletionID
                rowrun.RunDate = run.RunDate
                rowrun.RunID = run.RunID
                rowrun.Notes = run.Notes

                objListCompletion.Add(rowrun)

            Next

        End If

        Return objListCompletion

    End Function
    <WebMethod>
    Public Shared Function GetUnAssignedRuns() As List(Of FMS.Business.DataObjects.tblRuns)
        Dim ListRuns = New List(Of FMS.Business.DataObjects.tblRuns)

        Dim objList = FMS.Business.DataObjects.tblRuns.GetAll().GroupBy(Function(g) g.RunDescription).Select(Function(s) s.First)

        If (objList.Count > 0) Then

            For Each item In objList
                Dim row = New FMS.Business.DataObjects.tblRuns
                row.ApplicationID = item.ApplicationID
                row.RunID = item.RunID
                row.Rid = item.Rid
                row.RunNUmber = item.RunNUmber
                row.RunDescription = item.RunDescription
                row.RunDriver = item.RunDriver
                row.InactiveRun = item.InactiveRun
                'row.DateOfRun = item.DateOfRun
                'row.Rid = item.Rid
                'row.RunDescription = item.RunDescription
                'row.RunNUmber = item.RunNUmber
                ListRuns.Add(row)


            Next

        End If

        'Dim objList = FMS.Business.DataObjects.usp_GetUnAssignedRuns.GetAllPerApplication(DateRun, DateRun).ToList()

        'If (objList.Count > 0) Then

        '    For Each item In objList
        '        Dim row = New FMS.Business.DataObjects.usp_GetUnAssignedRuns

        '        row.ApplicationId = item.ApplicationId
        '        row.DateOfRun = item.DateOfRun
        '        row.Rid = item.Rid
        '        row.RunDescription = item.RunDescription
        '        row.RunNUmber = item.RunNUmber
        '        ListRuns.Add(row)
        '    Next

        'End If
        Return ListRuns
    End Function
    'Public Shared Function GetUnAssignedRuns(DateRun As Date) As List(Of FMS.Business.DataObjects.usp_GetUnAssignedRuns)
    '    Dim ListRuns = New List(Of FMS.Business.DataObjects.usp_GetUnAssignedRuns)

    '    Dim objList = FMS.Business.DataObjects.usp_GetUnAssignedRuns.GetAllPerApplication(DateRun, DateRun).ToList()

    '    If (objList.Count > 0) Then

    '        For Each item In objList
    '            Dim row = New FMS.Business.DataObjects.usp_GetUnAssignedRuns

    '            row.ApplicationId = item.ApplicationId
    '            row.DateOfRun = item.DateOfRun
    '            row.Rid = item.Rid
    '            row.RunDescription = item.RunDescription
    '            row.RunNUmber = item.RunNUmber
    '            ListRuns.Add(row)
    '        Next

    '    End If
    '    Return ListRuns
    'End Function
    Protected Sub PopulateServiceRunGrid()
        '--- Get
        Dim dtService = ServiceRunTable(Me.dteStart.Value, Me.dteEnd.Value)

        If (dtService.Columns.Count > 0) Then

            '--- Enable search panel
            Me.gvServiceRun.SettingsSearchPanel.Visible = True

            '--- Create Grid Columns
            Me.gvServiceRun.Columns.Clear()

            Dim table As DataTable = ServiceRunTable(Me.dteStart.Value, Me.dteEnd.Value)
            For Each dataColumn As DataColumn In table.Columns

                Dim dtype = dataColumn.DataType.FullName
                Dim blnTech As Boolean = False
                Dim blnTechName As Boolean = False
                Dim blnDrvr As Boolean = False
                Dim blnDrvrName As Boolean = False

                If dataColumn.DataType.FullName = "System.Int32" Or dataColumn.DataType.FullName = "System.String" Then

                    Dim techNdx = dataColumn.ColumnName.IndexOf("TechID")
                    If (techNdx > -1) Then
                        blnTech = True
                    End If

                    Dim drvNdx = dataColumn.ColumnName.IndexOf("DriverID")
                    If (drvNdx > -1) Then
                        blnDrvr = True
                    End If

                    Dim technameNdx = dataColumn.ColumnName.IndexOf("Tech_")
                    If (technameNdx > -1) Then
                        blnTechName = True
                    End If

                    Dim drivernameNdx = dataColumn.ColumnName.IndexOf("Driver_")
                    If (drivernameNdx > -1) Then
                        blnDrvrName = True
                    End If

                    Dim column As New GridViewDataTextColumn()
                    column.FieldName = dataColumn.ColumnName

                    'set additional column properties
                    If dataColumn.ColumnName = "RunDate" Then
                        column.Caption = " "
                    Else
                        column.Caption = dataColumn.ColumnName
                    End If

                    If blnTechName = True Then
                        column.HeaderStyle.BackColor = Drawing.Color.Orange
                        Dim tNdx = dataColumn.ColumnName.IndexOf("-") + 1
                        Dim techname = dataColumn.ColumnName.Substring(tNdx, dataColumn.ColumnName.Length - tNdx)

                        If (IsNumeric(techname) = True) Then
                            techname = "Technician " + techname
                        End If

                        column.Caption = techname
                    End If

                    If blnDrvrName = True Then
                        column.HeaderStyle.BackColor = Drawing.Color.Green
                        Dim dvrNdx = dataColumn.ColumnName.IndexOf("-") + 1
                        Dim drivername = dataColumn.ColumnName.Substring(dvrNdx, dataColumn.ColumnName.Length - dvrNdx)

                        If (IsNumeric(drivername) = True) Then
                            drivername = "Driver " + drivername
                        End If

                        column.Caption = drivername
                    End If

                    '--- hide column(s)
                    If (blnTech = True Or blnDrvr = True Or dataColumn.ColumnName = "ID") Then
                        column.Visible = False
                    End If

                    Me.gvServiceRun.Columns.Add(column)

                End If

                If dataColumn.DataType.FullName = "System.DateTime" Then

                    Dim column As New GridViewDataDateColumn()
                    column.FieldName = dataColumn.ColumnName

                    'set additional column properties
                    column.Caption = dataColumn.ColumnName

                    Me.gvServiceRun.Columns.Add(column)

                End If

            Next dataColumn

        End If

        '----- Create Rows Values

        Dim listSrvRun = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplication(Me.dteStart.Value, Me.dteEnd.Value)

        Dim dates = GetServiceRunDates(Me.dteStart.Value, Me.dteEnd.Value)

        If (dates.Count > 0) Then
            Dim row As DataRow
            Dim id As Integer = 1
            Dim techID As Integer = 0
            Dim dvrID As Integer = 0

            Dim objRun As New List(Of FMS.Business.DataObjects.usp_GetServiceRunDates)

            For Each dte In dates
                Dim strTechRun As String = ""
                Dim strRunNum As String = ""

                row = dtService.NewRow()

                For Each col In dtService.Columns

                    Dim strColName As String = ""
                    Dim colName = DirectCast(col, System.Data.DataColumn).ColumnName

                    If (colName.ToUpper() = "ID") Or (colName.ToUpper() = "RUNDATE") Then
                        strColName = colName
                    Else

                        '--- technician cols
                        Dim idxTech = colName.IndexOf("Tech_")
                        If (idxTech > -1) Then
                            strColName = "TECH_"
                        End If

                        Dim idxTID = colName.IndexOf("TechID_")
                        If (idxTID > -1) Then
                            Dim locNdx = colName.IndexOf("_") + 1
                            techID = Convert.ToInt32(colName.Substring(locNdx, colName.Length - locNdx))

                            'Dim val = listSrvRun.Where(Function(x) x.Did = techID _
                            '                            And x.RunDescription.Substring(0, 2) = "WR" _
                            '                            And x.DateOfRun = dte.RunDate).ToList()
                            Dim val = listSrvRun.Where(Function(x) x.Driver = techID _
                                                        And x.DateOfRun = dte.RunDate).ToList()

                            If (val.Count > 0) Then
                                strTechRun = val.FirstOrDefault.RunDescription.Substring(3, val.FirstOrDefault.RunDescription.Length - 3)
                            End If

                        End If

                        '--- Drivers cols
                        Dim idxDvr = colName.IndexOf("Driver_")
                        If (idxDvr > -1) Then
                            strColName = "Driver_"
                        End If

                        Dim idxDID = colName.IndexOf("DriverID_")
                        If (idxDID > -1) Then
                            Dim locNdx = colName.IndexOf("_") + 1
                            dvrID = Convert.ToInt32(colName.Substring(locNdx, colName.Length - locNdx))

                            Dim run = listSrvRun.Where(Function(r) r.Driver = dvrID _
                                                           And r.DateOfRun = dte.RunDate).ToList()

                            If (run.Count > 0) Then
                                strRunNum = "Run " + run.FirstOrDefault.Rid.ToString()
                            End If

                        End If


                    End If

                    Select Case strColName.ToUpper()
                        Case "ID"
                            row(colName) = id
                        Case "RUNDATE"
                            row(colName) = dte.RunDate.ToString("dd MMM")
                        Case "TECH_"
                            row(colName) = strTechRun
                            strTechRun = ""
                        Case "DRIVER_"
                            row(colName) = strRunNum
                            strRunNum = ""
                    End Select

                Next

                dtService.Rows.Add(row)

                '--- increament ID
                id = id + 1

            Next

            Session("ServiceRunTable") = dtService

            Me.gvServiceRun.DataSource = dtService
            Me.gvServiceRun.DataBind()

        End If


        'If (dates.Count > 0) Then

        '    Dim row As DataRow
        '    Dim id As Integer = 1
        '    Dim currDvrID = 0
        '    Dim prevDvrID = 0
        '    Dim dayname As String = ""
        '    Dim dvrID As Integer = 0
        '    Dim techID As Integer = 0

        '    Dim objRun As New List(Of FMS.Business.DataObjects.usp_GetServiceRunDates)

        '    For Each dte In dates
        '        Dim strTechRun As String = ""
        '        Dim strDvrRun As String = ""
        '        Dim strRunNum As String = ""

        '        dayname = dte.RunDate.ToString("dddd")

        '        row = dtService.NewRow()

        '        For Each col In dtService.Columns

        '            Dim strColName As String = ""

        '            Dim colName = DirectCast(col, System.Data.DataColumn).ColumnName

        '            If (colName.ToUpper() = "ID") Or (colName.ToUpper() = "RUNDATE") Then
        '                strColName = colName
        '            Else
        '                '--- technician cols
        '                Dim idxTech = colName.IndexOf("Tech_")
        '                If (idxTech > -1) Then
        '                    strColName = "TECH_"
        '                End If

        '                Dim idxTID = colName.IndexOf("TechID_")
        '                If (idxTID > -1) Then
        '                    Dim locNdx = colName.IndexOf("_") + 1
        '                    techID = Convert.ToInt32(colName.Substring(locNdx, colName.Length - locNdx))

        '                    Dim val = listSrvRun.Where(Function(x) x.Did = techID _
        '                                                And x.RunDescription.Substring(0, 2) = "WR" _
        '                                                And x.DateOfRun = dte.RunDate).ToList()

        '                    If (val.Count > 0) Then
        '                        strTechRun = val.FirstOrDefault.RunDescription.Substring(3, val.FirstOrDefault.RunDescription.Length - 3)
        '                    End If

        '                End If

        '                '--- Drivers cols
        '                Dim idxDvr = colName.IndexOf("Driver_")
        '                If (idxDvr > -1) Then
        '                    strColName = "Driver_"
        '                End If

        '                Dim idxDID = colName.IndexOf("DriverID_")
        '                If (idxDID > -1) Then
        '                    Dim locNdx = colName.IndexOf("_") + 1
        '                    dvrID = Convert.ToInt32(colName.Substring(locNdx, colName.Length - locNdx))

        '                    '--- test driver 45
        '                    If (dvrID = 45) Then
        '                        Dim obj = ""
        '                    End If
        '                    '---- end test

        '                    Select Case dayname.ToUpper()
        '                        Case "MONDAY"
        '                            Dim run = listSrvRun.Where(Function(r) r.MondayRun = True And r.Did = dvrID _
        '                                                           And r.RunDescription.Substring(0, 2) IsNot "WR").ToList()
        '                            If (run.Count > 0) Then
        '                                strRunNum = "Run " + run.FirstOrDefault.RunNUmber.ToString()
        '                            End If

        '                        Case "TUESDAY"
        '                            Dim run = listSrvRun.Where(Function(r) r.TuesdayRun = True And r.Did = dvrID _
        '                                                           And r.RunDescription.Substring(0, 2) IsNot "WR").ToList()
        '                            If (run.Count > 0) Then
        '                                strRunNum = "Run " + run.FirstOrDefault.RunNUmber.ToString()
        '                            End If

        '                        Case "WEDNESDAY"
        '                            Dim run = listSrvRun.Where(Function(r) r.WednesdayRun = True And r.Did = dvrID _
        '                                                           And r.RunDescription.Substring(0, 2) IsNot "WR").ToList()
        '                            If (run.Count > 0) Then
        '                                strRunNum = "Run " + run.FirstOrDefault.RunNUmber.ToString()
        '                            End If

        '                        Case "THURSDAY"
        '                            Dim run = listSrvRun.Where(Function(r) r.ThursdayRun = True And r.Did = dvrID _
        '                                                           And r.RunDescription.Substring(0, 2) IsNot "WR").ToList()
        '                            If (run.Count > 0) Then
        '                                strRunNum = "Run " + run.FirstOrDefault.RunNUmber.ToString()
        '                            End If

        '                        Case "FRIDAY"
        '                            Dim run = listSrvRun.Where(Function(r) r.FridayRun = True And r.Did = dvrID _
        '                                                           And r.RunDescription.Substring(0, 2) IsNot "WR").ToList()
        '                            If (run.Count > 0) Then
        '                                strRunNum = "Run " + run.FirstOrDefault.RunNUmber.ToString()
        '                            End If

        '                        Case "SATURDAY"
        '                            Dim run = listSrvRun.Where(Function(r) r.SaturdayRun = True And r.Did = dvrID _
        '                                                           And r.RunDescription.Substring(0, 2) IsNot "WR").ToList()
        '                            If (run.Count > 0) Then
        '                                strRunNum = "Run " + run.FirstOrDefault.RunNUmber.ToString()
        '                            End If

        '                        Case "SUNDAY"
        '                            Dim run = listSrvRun.Where(Function(r) r.SundayRun = True And r.Did = dvrID _
        '                                                           And r.RunDescription.Substring(0, 2) IsNot "WR").ToList()
        '                            If (run.Count > 0) Then
        '                                strRunNum = "Run " + run.FirstOrDefault.RunNUmber.ToString()
        '                            End If
        '                    End Select

        '                End If

        '            End If

        '            Select Case strColName.ToUpper()
        '                Case "ID"
        '                    row(colName) = id
        '                Case "RUNDATE"
        '                    row(colName) = dte.RunDate.ToString("dd MMM")
        '                Case "TECH_"
        '                    row(colName) = strTechRun
        '                Case "DRIVER_"
        '                    row(colName) = strRunNum
        '                    strRunNum = ""
        '            End Select

        '        Next

        '        dtService.Rows.Add(row)

        '        '--- increament ID
        '        id = id + 1

        '    Next

        '    Session("ServiceRunTable") = dtService

        '    Me.gvServiceRun.DataSource = dtService
        '    Me.gvServiceRun.DataBind()

        'End If

    End Sub

    Public Shared Function ServiceRunTable(StartDate As Date, EndDate As Date) As DataTable

        '--- Create Table Fields or Columns
        Dim fields = CreateServiceRunFieldList(StartDate, EndDate)

        Dim table As DataTable = New DataTable()
        table.TableName = "tblServiceRun"
        Dim column As DataColumn

        For Each fld In fields
            Dim strType As String = fld.FieldProperty.ToString()

            '--- check if column already exist
            Dim colExist = False

            Dim strFld As String = ""

            Dim ndxDID = fld.FieldName.IndexOf("DriverID_")
            Dim ndxDNM = fld.FieldName.IndexOf("Driver_")
            If (ndxDID > -1) Or (ndxDNM > -1) Then
                Dim ndx = fld.FieldName.IndexOf("_") + 1
                strFld = "TechID_" + fld.FieldName.Substring(ndx, fld.FieldName.Length - ndx)

            End If

            For colNdx = 0 To table.Columns.Count - 1

                If (table.Columns(colNdx).ColumnName = strFld) Then
                    colExist = True

                    Exit For
                End If
            Next

            '--- add column if not exist
            If (colExist = False) Then
                column = New DataColumn()
                column.ColumnName = fld.FieldName
                column.DataType = System.Type.GetType(strType)

                table.Columns.Add(column)
            End If

        Next

        Return table

    End Function

    Public Shared Function CreateServiceRunFieldList(StartDate As Date, EndDate As Date) As List(Of ServiceRunFields)

        Dim fields As New List(Of ServiceRunFields)

        '--- Create ID field Column (as PK)
        Dim fldID As New ServiceRunFields

        fldID.FieldName = "ID"
        fldID.FieldProperty = GetType(Integer)

        fields.Add(fldID)

        '--- Create Run Date Column
        Dim fldDate As New ServiceRunFields

        fldDate.FieldName = "RunDate"
        fldDate.FieldProperty = GetType(String)

        fields.Add(fldDate)

        '--- Create Technician Column(s)
        Dim oTechs = GetTechnician(StartDate, EndDate)

        If (oTechs.Count > 0) Then

            For Each t In oTechs

                Dim fldTechID As New ServiceRunFields

                fldTechID.FieldName = t.TechID
                fldTechID.FieldProperty = GetType(Integer)
                fields.Add(fldTechID)

                Dim fldTechnician As New ServiceRunFields



                fldTechnician.FieldName = t.Techname
                fldTechnician.FieldProperty = GetType(String)
                fields.Add(fldTechnician)

            Next

        End If

        '--- Create Driver's Column(s)
        'Dim oDrivers = FMS.Business.DataObjects.tblDrivers.GetAllPerApplication().Where(Function(d) _
        '                                                                                 d.Inactive = False _
        '                                                                                 And (d.Technician <> vbNull _
        '                                                                                 And d.Technician = False)).ToList()
        Dim oDrivers = FMS.Business.DataObjects.tblDrivers.GetAllPerApplication().Where(Function(d) _
                                                                                         d.Inactive = False _
                                                                                         And (d.Technician Is Nothing _
                                                                                         Or d.Technician = False)).ToList()

        If (oDrivers.Count > 0) Then
            For Each d In oDrivers
                Dim fldDvrID As New ServiceRunFields

                fldDvrID.FieldName = "DriverID_" + d.Did.ToString()
                fldDvrID.FieldProperty = GetType(Integer)
                fields.Add(fldDvrID)

                Dim fldDriver As New ServiceRunFields

                fldDriver.FieldName = "Driver_" + d.Did.ToString() + "-" + d.DriverName
                fldDriver.FieldProperty = GetType(String)
                fields.Add(fldDriver)

            Next
        End If

        Return fields

    End Function

    Public Shared Function GetTechnician(StartDate As Date, EndDate As Date) As List(Of Technician)
        Dim Technicians As New List(Of Technician)

        Dim techs = FMS.Business.DataObjects.tblDrivers.GetTechnicianPerApplicationMinusInActive()

        If (techs.Count > 0) Then

            For Each t In techs
                Dim Tech As New Technician

                Tech.TechID = "TechID_" + t.Did.ToString()
                Tech.Techname = "Tech_" + t.Did.ToString() + "-" + t.DriverName

                Technicians.Add(Tech)

            Next

        End If

        Return Technicians

    End Function

    Public Shared Function GetServiceRunDates(StartDate As Date, EndDate As Date) As List(Of ServiceRunDates)
        Dim dateCtr = DateDiff(DateInterval.Day, StartDate, EndDate.AddDays(1))

        Dim listDates As New List(Of ServiceRunDates)

        If (dateCtr > 0) Then

            For nRow = 0 To dateCtr - 1
                Dim row As New ServiceRunDates

                If (nRow = 0) Then
                    row.RunDate = StartDate
                Else
                    row.RunDate = StartDate.AddDays(nRow)
                End If

                listDates.Add(row)

            Next

        End If

        Return listDates
    End Function

#End Region

#Region "List for Service Run Date"
    Public Class ServiceRunFields
        Public Property FieldName As String
        Public Property FieldProperty As Object

    End Class

    Public Class ServiceRunDates
        Public Property RunDate As Date

    End Class

    Public Class Technician
        Public Property TechID As String
        Public Property Techname As String


    End Class





#End Region



End Class