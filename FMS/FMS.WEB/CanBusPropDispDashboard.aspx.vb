Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json
Imports FMS.Business.DataObjects
Imports System.Web.Script.Serialization


Public Class CanBusPropDispDashboard
    Inherits System.Web.UI.Page

    Public ReadOnly Property deviceID As String
        Get
            Return Request.QueryString("deviceID")
        End Get
    End Property

    Public ReadOnly Property VehicleName As String
        Get
            Return Request.QueryString("VehicleName")
        End Get
    End Property

    Public ReadOnly Property ClickEvent As String
        Get
            Return Request.QueryString("ClickEvent")
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If IsPostBack = False Then
            Dim oList As List(Of DashboardValues) = New List(Of DashboardValues)

            oList = Session("ses_DashboardRecord")
            ConvertToJSON(oList)

            'Page.ClientScript.RegisterStartupScript(Me.[GetType](), "CallMyFunction", "MyFunction()", True)

            'Page.ClientScript.RegisterStartupScript(Me.GetType(), "CallGMInit", "<script>initialize()</script>", False)

            'lblVehicleID.Text = vehicleID

            'Dim scriptKey As String = "UniqueKeyForThisScript"
            'Dim javaScript As String = "<script type='text/javascript'>GetDashboardData();</script>"
            'ClientScript.RegisterStartupScript(Me.GetType(), scriptKey, javaScript)

        End If
        
    End Sub

    Public Sub ConvertToJSON(dashlist As List(Of DashboardValues))
        Dim jss1 As New JavaScriptSerializer()
        Dim _myJSONstring As String = jss1.Serialize(dashlist)
        Dim strList As String = (Convert.ToString("var oDashList=") & _myJSONstring) + ";"
        Page.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "dashlst", strList, True)
    End Sub

End Class