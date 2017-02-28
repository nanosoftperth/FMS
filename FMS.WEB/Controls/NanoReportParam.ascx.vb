Public Class NanoReportParam
    Inherits System.Web.UI.UserControl

    Private _UniqueClientID As String
    Public ReadOnly Property UniqueClientID As String
        Get
            If String.IsNullOrEmpty(_UniqueClientID) Then _UniqueClientID = Guid.NewGuid.ToString.Replace("-"c, "")
            Return _UniqueClientID
        End Get
    End Property

    Public Property ReportParameter As DevExpress.XtraReports.Parameters.Parameter
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblParameterName.Text = ReportParameter.Description

        'populate the comboDateSelected, object data sources do not seem to work for custom controls in markup
        Business.DataObjects.ReportSchedule.GetDateTimeOptions.ForEach(Function(x) comboDateSelected.Items.Add(x))



        'add the value changed event to javascript here as we need to know the new client ID at runtime
        comboDateSelected.ClientSideEvents.ValueChanged = _
                 " function(s,e) {comboDateSelected_ValueChanged(s, '" & UniqueClientID & "')}"

        If ReportParameter.Description.Contains("Start Date") Then
            comboDateSelected.ClientInstanceName = "StartDate"
            'comboDateSelected.Attributes.AddAttributes()
            comboDateSelected.Text = ""
            dateSpecificDate.ClientInstanceName = "StartDateSpecific"

        ElseIf ReportParameter.Description.Contains("End Date") Then
            comboDateSelected.Text = ""
            comboDateSelected.ClientInstanceName = "EndDate"

            dateSpecificDate.ClientInstanceName = "EndDateSpecific"
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

            If ReportParameter.Description.Contains("Vehicle") Then
                comboBox.ClientInstanceName = "Vehicle"
                comboBox.Value = ""


            ElseIf ReportParameter.Description.Contains("Driver") Then
                comboBox.ClientInstanceName = "Drivers"
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