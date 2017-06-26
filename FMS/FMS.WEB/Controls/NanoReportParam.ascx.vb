﻿Imports FMS.Business
Imports System.Globalization
Imports DevExpress.Web
Imports DevExpress.XtraReports.Parameters

Public Class NanoReportParam
    Inherits System.Web.UI.UserControl

    Private _UniqueClientID As String
    Private _ISDisplay As String
    Protected IsVehicle As Boolean

    Public ReadOnly Property UniqueClientID As String
        Get
            If String.IsNullOrEmpty(_UniqueClientID) Then _UniqueClientID = Guid.NewGuid.ToString.Replace("-"c, "")
            Return _UniqueClientID
        End Get
    End Property
    Public Property ISDisplay As String
        Get
            Return _ISDisplay
        End Get
        Set(value As String)
            _ISDisplay = value

        End Set
    End Property

    Public Property ReportParameter As DevExpress.XtraReports.Parameters.Parameter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        lblParameterName.Text = ReportParameter.Description

        'populate the comboDateSelected, object data sources do not seem to work for custom controls in markup
        '' for Start Date on 20170417  
        If ReportParameter.Description.Contains("Start Date") Then
            Business.DataObjects.ReportSchedule.GetDateTimeOptions.ForEach(Function(x) comboDateSelected.Items.Add(x))
        ElseIf ReportParameter.Description.Contains("End Date") Then
            Business.DataObjects.ReportSchedule.GetEndDateTimeOptions.ForEach(Function(x) comboDateSelected.Items.Add(x))
        End If

        'add the value changed event to javascript here as we need to know the new client ID at runtime
        comboDateSelected.ClientSideEvents.ValueChanged = _
                 " function(s,e) {comboDateSelected_ValueChanged(s, '" & UniqueClientID & "')}"

        If ReportParameter.Description.Contains("Start Date") Then
            comboDateSelected.ClientInstanceName = "StartDate"
            If Not HttpContext.Current.Session("StartDate") Is Nothing And Not HttpContext.Current.Session("StartDate") = "null" Then
                comboDateSelected.Value = HttpContext.Current.Session("StartDate")
            End If
            dateSpecificDate.ClientInstanceName = "StartDateSpecific"
            If HttpContext.Current.Session("StartDate") = "Specific" Then
                'dateSpecificDate.Date = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(HttpContext.Current.Session("StartDateSpecific"), CultureInfo.InvariantCulture))
                'dateSpecificDate.Value = Convert.ToDateTime(HttpContext.Current.Session("StartDateSpecific"), CultureInfo.InvariantCulture) 'String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(HttpContext.Current.Session("StartDateSpecific"), CultureInfo.InvariantCulture))

                If Not HttpContext.Current.Session("StartDateSpecific") Is Nothing And Not HttpContext.Current.Session("StartDateSpecific") = "null" Then
                    dateSpecificDate.Value = Convert.ToDateTime(HttpContext.Current.Session("StartDateSpecific"), CultureInfo.InvariantCulture)
                End If
                dateSpecificDate.EditFormatString = "MM/dd/yyyy hh:mm tt"

                ISDisplay = "block"
            Else

                ISDisplay = "none"
            End If
        ElseIf ReportParameter.Description.Contains("End Date") Then
            If Not HttpContext.Current.Session("EndDate") Is Nothing And Not HttpContext.Current.Session("EndDate") = "null" Then
                comboDateSelected.Value = HttpContext.Current.Session("EndDate")
            End If
            comboDateSelected.ClientInstanceName = "EndDate"
            dateSpecificDate.ClientInstanceName = "EndDateSpecific"
            If HttpContext.Current.Session("EndDate") = "Specific" Then
                'If Not HttpContext.Current.Session("EndDate") Is Nothing Then
                If Not HttpContext.Current.Session("EndDateSpecific") Is Nothing And Not HttpContext.Current.Session("EndDateSpecific") = "null" Then
                    dateSpecificDate.Value = Convert.ToDateTime(HttpContext.Current.Session("EndDateSpecific"), CultureInfo.InvariantCulture)
                End If
                'dateSpecificDate.Date = String.Format("{0:MM/dd/yyyy}", Convert.ToDateTime(HttpContext.Current.Session("EndDateSpecific"), CultureInfo.InvariantCulture))
                dateSpecificDate.EditFormatString = "MM/dd/yyyy hh:mm tt"
                ISDisplay = "block"
            Else
                ISDisplay = "none"
            End If
        End If


        Dim paramControl As System.Web.UI.Control

        'there is a lookup list to use here
        If TypeOf ReportParameter.LookUpSettings Is DevExpress.XtraReports.Parameters.DynamicListLookUpSettings Then

            Dim lookupSettings = DirectCast(ReportParameter.LookUpSettings, 
                                                DevExpress.XtraReports.Parameters.DynamicListLookUpSettings)


            ' Dim comboBox As New DevExpress.Web.ASPxListBox()


            'Dim comboBox As New DevExpress.Web.ASPxListBox()
            'comboBox.ID = Guid.NewGuid.ToString
            'comboBox.SelectionMode = DevExpress.Web.ListEditSelectionMode.CheckColumn


            'comboBox.ValueField = lookupSettings.ValueMember
            'comboBox.TextField = lookupSettings.DisplayMember
            ''comboBox.ValueType = GetType(System.Int32)

            'Dim ods As DevExpress.DataAccess.ObjectBinding.ObjectDataSource = lookupSettings.DataSource
            'ods.Fill()

            'comboBox.DataSource = ods
            'comboBox.DataBind()

            Dim editor As New ASPxDropDownEdit()


            editor.ClientIDMode = ClientIDMode.Static

            Dim controlIndex As Integer = 1
            editor.ID = String.Format("ASPxDropDownEdit{0}", controlIndex)
            Dim container As New Control
            'Dim tet = DirectCast(ReportParameter.LookUpSettings, 
            '                                    DevExpress.XtraReports.Parameters.DynamicListLookUpSettings)
            editor.ClientInstanceName = "checkComboBox1" 
            editor.DropDownWindowTemplate = New CustomTemplate(DirectCast(ReportParameter.LookUpSettings, 
                                                DevExpress.XtraReports.Parameters.DynamicListLookUpSettings))


          

            paramControl = editor

            dateTimeDIV.Visible = False
        End If



        If ReportParameter.Type.ToString.ToLower = "system.datetime" Then
            'relative time OR exact time
            Dim uniqcoID As String = Guid.NewGuid.ToString.Replace("-", "")
        End If
        If paramControl IsNot Nothing Then panelContent.Controls.Add(paramControl)

    End Sub 

End Class

Public Class CustomTemplate
    Implements ITemplate

    Public Property ReportParameter As DevExpress.XtraReports.Parameters.Parameter
    'Dim lookupSettings = Nothing
    Dim _lookupsSettings As DevExpress.XtraReports.Parameters.DynamicListLookUpSettings
    Dim lookupsSettings
    Public Sub New(lookupsSettings)
        _lookupsSettings = lookupsSettings
    End Sub
    Public Sub New()

    End Sub
    Public Sub InstantiateIn(container As Control) Implements ITemplate.InstantiateIn 

        Dim comboBox As New DevExpress.Web.ASPxListBox()
        comboBox.ID = Guid.NewGuid.ToString
        comboBox.SelectionMode = DevExpress.Web.ListEditSelectionMode.CheckColumn
   
        comboBox.ValueField = _lookupsSettings.ValueMember
        comboBox.TextField = _lookupsSettings.DisplayMember
        'comboBox.ValueType = GetType(System.Int32)

        Dim ods As DevExpress.DataAccess.ObjectBinding.ObjectDataSource = _lookupsSettings.DataSource
        ods.Fill()

        comboBox.DataSource = ods
        comboBox.DataBind()
        comboBox.SelectionMode = ListEditSelectionMode.CheckColumn
        comboBox.ClientInstanceName = "vehiclecheckListBox"
         

        'function(s, e) { OnListBoxSelectionChangedValue(s, e); }"


        If _lookupsSettings.ValueMember.Contains("Name") Then
            If Not HttpContext.Current.Session("Vehicle") Is Nothing And Not HttpContext.Current.Session("Vehicle") = "null" Then
                comboBox.Value = HttpContext.Current.Session("Vehicle")
            End If
        Else
            If Not HttpContext.Current.Session("BusinessLocation") Is Nothing And Not HttpContext.Current.Session("BusinessLocation") = "null" Then
                comboBox.Value = HttpContext.Current.Session("BusinessLocation")
            End If
        End If 

        If _lookupsSettings.Parameter.Description.Contains("Vehicle") Then
            comboBox.ClientInstanceName = "Vehicle"
            comboBox.ClientSideEvents.SelectedIndexChanged = String.Format("function (s, e) {{ OnListBoxSelectionChangedValue(s, e, {0}); }}", "Vehicle")
        ElseIf _lookupsSettings.Parameter.Description.Contains("Driver") Then
            comboBox.ClientInstanceName = "Drivers"
            comboBox.ClientSideEvents.SelectedIndexChanged = String.Format("function (s, e) {{ OnListBoxSelectionChangedValue(s, e, {0}); }}", "Drivers")
        ElseIf _lookupsSettings.Parameter.Description.Contains("Business Location") Then
            comboBox.ClientInstanceName = "BusinessLocation"
            comboBox.ClientSideEvents.SelectedIndexChanged = String.Format("function (s, e) {{ OnListBoxSelectionChangedValue(s, e, {0}); }}", "BusinessLocation")
        End If
         

        container.Controls.Add(comboBox)
    End Sub
End Class