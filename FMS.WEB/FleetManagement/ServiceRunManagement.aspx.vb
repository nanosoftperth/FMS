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

#End Region

#Region "Methods and Functions for Service Run"

    Protected Sub PopulateServiceRunGrid()

        Dim dtService = ServiceRunTable()

        If (dtService.Columns.Count > 0) Then

            '--- Create Grid Columns
            Me.gvServiceRun.Columns.Clear()

            Dim table As DataTable = ServiceRunTable()
            For Each dataColumn As DataColumn In table.Columns

                Dim dtype = dataColumn.DataType.FullName

                If dataColumn.DataType.FullName = "System.Int32" Or dataColumn.DataType.FullName = "System.String" Then
                    Dim column As New GridViewDataTextColumn()
                    column.FieldName = dataColumn.ColumnName

                    'set additional column properties
                    column.Caption = dataColumn.ColumnName
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

        '----- Create Rows
        '--- Create Run Dates
        Dim dates = GetServiceRunDates(Me.dteStart.Value, Me.dteEnd.Value)

        If (dates.Count > 0) Then

            Dim row As DataRow
            Dim id As Integer = 1

            For Each dte In dates

                row = dtService.NewRow()

                row("id") = id
                row("RunDate") = dte.RunDate.ToString("dd MMM")
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

    Public Shared Function ServiceRunTable() As DataTable

        Dim fields = CreateServiceRunFieldList()

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

    Public Shared Function CreateServiceRunFieldList() As List(Of ServiceRunFields)

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
        Dim oDrivers = FMS.Business.DataObjects.usp_GetServiceRunDates.GetAllPerApplicationAndDistinctDriverName()

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




    Public Shared Function CreateClass(ByVal className As String, ByVal properties As Dictionary(Of String, Type)) As Type

        Dim myDomain As AppDomain = AppDomain.CurrentDomain
        Dim myAsmName As New AssemblyName("MyAssembly")
        Dim myAssembly As AssemblyBuilder = myDomain.DefineDynamicAssembly(myAsmName, AssemblyBuilderAccess.Run)

        Dim myModule As ModuleBuilder = myAssembly.DefineDynamicModule("MyModule")

        Dim myType As TypeBuilder = myModule.DefineType(className, TypeAttributes.Public)

        myType.DefineDefaultConstructor(MethodAttributes.Public)

        For Each o In properties

            Dim prop As PropertyBuilder = myType.DefineProperty(o.Key, PropertyAttributes.HasDefault, o.Value, Nothing)

            Dim field As FieldBuilder = myType.DefineField("_" + o.Key, GetType(Integer), FieldAttributes.[Private])

            Dim getter As MethodBuilder = myType.DefineMethod("get_" + o.Key, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig, o.Value, Type.EmptyTypes)
            Dim getterIL As ILGenerator = getter.GetILGenerator()
            getterIL.Emit(OpCodes.Ldarg_0)
            getterIL.Emit(OpCodes.Ldfld, field)
            getterIL.Emit(OpCodes.Ret)

            Dim setter As MethodBuilder = myType.DefineMethod("set_" + o.Key, MethodAttributes.[Public] Or MethodAttributes.SpecialName Or MethodAttributes.HideBySig, Nothing, New Type() {o.Value})
            Dim setterIL As ILGenerator = setter.GetILGenerator()
            setterIL.Emit(OpCodes.Ldarg_0)
            setterIL.Emit(OpCodes.Ldarg_1)
            setterIL.Emit(OpCodes.Stfld, field)
            setterIL.Emit(OpCodes.Ret)

            prop.SetGetMethod(getter)
            prop.SetSetMethod(setter)

        Next

        Return myType.CreateType()

    End Function

    Protected Sub Button1_Click(sender As Object, e As EventArgs)


        PopulateServiceRunGrid()

        'Dim dateko As New Dictionary(Of String, Type)

        'Dim cls = CreateClass("sample", dateko)

        'Dim obj As New cls

        'Dim o As Object = ""




    End Sub



#End Region


End Class