Imports System.Reflection
Imports System.Reflection.Emit
Imports DevExpress.Web
Imports DevExpress.Web.ASPxGridView

Public Class ServiceRunManagement
    Inherits System.Web.UI.Page

#Region "Events"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If (Not IsPostBack = False) Then
            Dim dtServRun As DataTable

            dtServRun = DirectCast(Session("ServiceRunTable"), DataTable)

            If (IsNothing(dtServRun) = False) Then
                Me.gvServiceRun.DataSource = dtServRun
                Me.gvServiceRun.DataBind()
            End If


        End If
    End Sub
    Protected Sub dteStart_ValueChanged(sender As Object, e As EventArgs)
        'Dim ListDates = GetRunDates(Me.dteStart.Value, Me.dteEnd.Value)
        'Me.gvServiceRun.DataBind()

        'PopulateServiceRunGrid(Me.dteStart.Value, Me.dteEnd.Value)

    End Sub

    Protected Sub dteEnd_ValueChanged(sender As Object, e As EventArgs)
        'Dim ListDates = GetRunDates(Me.dteStart.Value, Me.dteEnd.Value)
        ' Me.gvServiceRun.DataBind()

        'PopulateServiceRunGrid(Me.dteStart.Value, Me.dteEnd.Value)

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

    Protected Sub gvServiceRun_HtmlDataCellPrepared(sender As Object, e As ASPxGridViewTableDataCellEventArgs)

        Dim htmlId As String = String.Format("cell_{0}_{1}", e.VisibleIndex, e.DataColumn.FieldName)
        Dim cellClickHandler As String = String.Format("onCellClick(""{0}"", ""{1}"", ""{2}"")", e.DataColumn.FieldName, e.GetValue(e.DataColumn.FieldName), htmlId)
        e.Cell.Attributes.Add("onclick", cellClickHandler)
        e.Cell.Attributes.Add("id", htmlId)

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs)
        puUnassignedRun.ShowOnPageLoad = False
    End Sub


#End Region

#Region "Methods and Functions for Service Run"

    Protected Sub PopulateServiceRunGrid()

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
                Dim blnDrvr As Boolean = False

                If dataColumn.DataType.FullName = "System.Int32" Or dataColumn.DataType.FullName = "System.String" Then

                    Dim techNdx = dataColumn.ColumnName.IndexOf("TechID")
                    If (techNdx > -1) Then
                        blnTech = True
                    End If

                    Dim drvNdx = dataColumn.ColumnName.IndexOf("DriverID")
                    If (drvNdx > -1) Then
                        blnDrvr = True
                    End If

                    Dim column As New GridViewDataTextColumn()
                    column.FieldName = dataColumn.ColumnName

                    'set additional column properties
                    If dataColumn.ColumnName = "RunDate" Then
                        column.Caption = " "
                    Else
                        column.Caption = dataColumn.ColumnName
                    End If


                    If (blnTech = True Or blnDrvr = True Or dataColumn.ColumnName = "ID") Then
                        column.Visible = False
                    End If

                    'column.HeaderStyle.Border.BorderStyle = WebControls.BorderStyle.None

                    Me.gvServiceRun.Columns.Add(column)

                End If

                If dataColumn.DataType.FullName = "System.DateTime" Then

                    Dim column As New GridViewDataDateColumn()
                    column.FieldName = dataColumn.ColumnName

                    'set additional column properties
                    column.Caption = dataColumn.ColumnName
                    'column.HeaderStyle.Border.BorderStyle = WebControls.BorderStyle.None

                    Me.gvServiceRun.Columns.Add(column)

                End If

            Next dataColumn

        End If

        '----- Create Rows
        '--- Create Run Dates

        Dim listSrvRun = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplication(Me.dteStart.Value, Me.dteEnd.Value)

        Dim dates = GetServiceRunDates(Me.dteStart.Value, Me.dteEnd.Value)

        If (dates.Count > 0) Then

            Dim row As DataRow
            Dim id As Integer = 1
            Dim currDvrID = 0
            Dim prevDvrID = 0
            Dim dayname As String = ""
            Dim dvrID As Integer = 0
            Dim objRun As New List(Of FMS.Business.DataObjects.usp_GetServiceRunDates)

            For Each dte In dates

                row = dtService.NewRow()

                For Each col In dtService.Columns

                    Dim colName = DirectCast(col, System.Data.DataColumn).ColumnName

                    Dim techNdx = colName.IndexOf("TechID")
                    If (techNdx > -1) Then
                        row(colName) = 0
                    Else

                        Dim drvNdx = colName.IndexOf("DriverID")
                        If (drvNdx > -1) Then

                            dayname = dte.RunDate.ToString("dddd")
                            Dim dNdx = colName.IndexOf("_") + 1
                            dvrID = Convert.ToInt32(colName.Substring(dNdx, colName.Length - dNdx))

                            currDvrID = dvrID

                            row(colName) = dvrID

                        Else

                            If (colName.ToUpper() = currDvrID.ToString().ToUpper()) Then

                                Select Case dayname.ToUpper()
                                    Case "MONDAY"
                                        Dim run = listSrvRun.Where(Function(r) r.MondayRun = True And r.Did = currDvrID).FirstOrDefault()
                                        row(colName) = "Run " + run.RunNUmber.ToString()

                                    Case "TUESDAY"
                                        Dim run = listSrvRun.Where(Function(r) r.TuesdayRun = True And r.Did = currDvrID).FirstOrDefault()
                                        row(colName) = "Run " + run.RunNUmber.ToString()

                                    Case "WEDNESDAY"
                                        Dim run = listSrvRun.Where(Function(r) r.WednesdayRun = True And r.Did = currDvrID).FirstOrDefault()
                                        row(colName) = "Run " + run.RunNUmber.ToString()

                                    Case "THURSDAY"
                                        Dim run = listSrvRun.Where(Function(r) r.ThursdayRun = True And r.Did = currDvrID).FirstOrDefault()
                                        row(colName) = "Run " + run.RunNUmber.ToString()

                                    Case "FRIDAY"
                                        Dim run = listSrvRun.Where(Function(r) r.FridayRun = True And r.Did = currDvrID).FirstOrDefault()
                                        row(colName) = "Run " + run.RunNUmber.ToString()

                                    Case "SATURDAY"
                                        Dim run = listSrvRun.Where(Function(r) r.SaturdayRun = True And r.Did = currDvrID).FirstOrDefault()
                                        row(colName) = "Run " + run.RunNUmber.ToString()

                                    Case "SUNDAY"
                                        Dim run = listSrvRun.Where(Function(r) r.SundayRun = True And r.Did = currDvrID).FirstOrDefault()
                                        row(colName) = "Run " + run.RunNUmber.ToString()


                                End Select

                            Else

                                Select Case colName.ToUpper()
                                    Case "ID"
                                        row(colName) = id
                                    Case "RUNDATE"
                                        row(colName) = dte.RunDate.ToString("dd MMM")

                                End Select


                            End If

                        End If

                    End If

                Next

                dtService.Rows.Add(row)

                '--- increament ID
                id = id + 1

            Next

            Session("ServiceRunTable") = dtService

            Me.gvServiceRun.DataSource = dtService
            Me.gvServiceRun.DataBind()

        End If

    End Sub

    Protected Sub PopulateServiceRunTable()

    End Sub

    Public Shared Function ServiceRunTable(StartDate As Date, EndDate As Date) As DataTable


        Dim fields = CreateServiceRunFieldList(StartDate, EndDate)

        Dim table As DataTable = New DataTable()
        table.TableName = "tblServiceRun"
        Dim column As DataColumn

        For Each fld In fields
            Dim strType As String = fld.FieldProperty.ToString()

            column = New DataColumn()
            column.ColumnName = fld.FieldName
            column.DataType = System.Type.GetType(strType)

            table.Columns.Add(column)

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
        Dim oTechs = GetTechnician()

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
        Dim oDrivers = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplicationAndDistinctDriverName(StartDate, EndDate)

        If (oDrivers.Count > 0) Then

            For Each d In oDrivers

                Dim fldDvrID As New ServiceRunFields

                fldDvrID.FieldName = "DriverID_" + d.id.ToString()
                fldDvrID.FieldProperty = GetType(Integer)
                fields.Add(fldDvrID)

                Dim fldDriver As New ServiceRunFields

                fldDriver.FieldName = d.Name
                fldDriver.FieldProperty = GetType(String)
                fields.Add(fldDriver)

            Next

        End If

        Return fields

    End Function

    ''' <summary>
    ''' This is just for testing
    ''' the content of the function will be change as soon as we know how to get
    ''' the technicians
    ''' </summary>
    ''' <returns></returns>
    Public Shared Function GetTechnician() As List(Of Technician)
        Dim Technicians As New List(Of Technician)
        Dim strTech As String = "Technician"

        For nRow = 1 To 3
            Dim Tech As New Technician

            Dim techname = strTech + nRow.ToString()

            Tech.TechID = "TechID_" + nRow.ToString()
            Tech.Techname = techname

            Technicians.Add(Tech)

        Next

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

    'Public Shared Function GetServiceRunDates(StartDate As Date, EndDate As Date) As List(Of RunDates)

    'End Function

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


#Region "Test Code/Garbage Codes"

#End Region


End Class