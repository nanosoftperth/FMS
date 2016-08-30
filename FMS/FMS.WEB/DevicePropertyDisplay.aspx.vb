Public Class DriverPropertyDisplay
    Inherits System.Web.UI.Page


    Public ReadOnly Property DeviceID As String
        Get
            Return Request.QueryString("DeviceID")
        End Get
    End Property

    Public Property DriverID As String
    Public Property DriverName As String
    Public Property VehicleName As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack And Membership.ApplicationName <> "/" Then Exit Sub

        Dim truckLst As List(Of Business.Truck) = Business.Truck.GetExampleFleetNow(ThisSession.ApplicationID)

        Dim selectedTruck As Business.Truck = truckLst.Where(Function(x) x.ID = DeviceID).Single

        With selectedTruck
            Me.DriverID = .DriverID.ToString
            Me.DriverName = .Driver
            Me.VehicleName = .TruckName
        End With

        lblVehicle.Text = Me.VehicleName
        lblDriver.Text = Me.DriverName

        'if not logged in, then redirect to the login page (applies to all pages so placed in the master page)
        'If Membership.GetUser Is Nothing Then
        '    Response.Write("you do not have access to this page")
        '    Exit Sub
        'End If

        'Dim uName = Membership.GetUser.UserName


        'ThisSession.User = FMS.Business.DataObjects.User.GetAllUsersForApplication _
        '                (ThisSession.ApplicationID).Where(Function(x) x.UserName = uName).Single

        ''SET the userid in the session parameters
        'ThisSession.UserID = ThisSession.User.UserId
    End Sub

End Class