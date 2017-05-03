﻿Imports FMS.Business
Imports System.Globalization

Public Class NanoReportParam
    Inherits System.Web.UI.UserControl

    Private _UniqueClientID As String
    Private _ISDisplay As String
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


            Dim comboBox As New DevExpress.Web.ASPxComboBox()

            comboBox.ValueField = lookupSettings.ValueMember
            comboBox.TextField = lookupSettings.DisplayMember

            Dim ods As DevExpress.DataAccess.ObjectBinding.ObjectDataSource = lookupSettings.DataSource
            ods.Fill()

            comboBox.DataSource = ods
            comboBox.DataBind()
            comboBox.ID = Guid.NewGuid.ToString
            If lookupSettings.ValueMember.Contains("Name") Then

                If Not HttpContext.Current.Session("Vehicle") Is Nothing And Not HttpContext.Current.Session("Vehicle") = "null" Then
                    comboBox.Value = HttpContext.Current.Session("Vehicle")
                End If 
            Else
                If Not HttpContext.Current.Session("BusinessLocation") Is Nothing And Not HttpContext.Current.Session("BusinessLocation") = "null" Then
                    comboBox.Value = HttpContext.Current.Session("BusinessLocation")
                End If 
            End If 
            If ReportParameter.Description.Contains("Vehicle") Then
                comboBox.ClientInstanceName = "Vehicle"
            ElseIf ReportParameter.Description.Contains("Driver") Then
                comboBox.ClientInstanceName = "Drivers"
            ElseIf ReportParameter.Description.Contains("Business Location") Then
                comboBox.ClientInstanceName = "BusinessLocation"
            End If 

            paramControl = comboBox

            dateTimeDIV.Visible = False  
        End If



            If ReportParameter.Type.ToString.ToLower = "system.datetime" Then 
                'relative time OR exact time
            Dim uniqcoID As String = Guid.NewGuid.ToString.Replace("-", "")
            End If 
            If paramControl IsNot Nothing Then panelContent.Controls.Add(paramControl) 

    End Sub

End Class