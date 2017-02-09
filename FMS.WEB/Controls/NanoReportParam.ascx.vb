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

            paramControl = comboBox

        End If


        If ReportParameter.Type.ToString.ToLower = "system.datetime" Then

            'relative time OR exact time
            Dim uniqcoID As String = Guid.NewGuid.ToString.Replace("-", "")




        End If


        If paramControl IsNot Nothing Then panelContent.Controls.Add(paramControl)


    End Sub

End Class