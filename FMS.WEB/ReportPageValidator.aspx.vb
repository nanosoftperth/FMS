Imports System.Net
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Public Class ReportPageValidator
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AuthenticateToken()
    End Sub
    Private Sub AuthenticateToken()
        Dim userName = IIf(Membership.GetUser Is Nothing, "", FMS.Business.ThisSession.Username)
        Dim password = IIf(Membership.GetUser Is Nothing, "", FMS.Business.ThisSession.Password)
        Try
            Dim srU As New UniqcoIntegrator.Uniqco_IntegratorClient()
            Dim vnr As New ServiceAccess.WebServices.VINNumberRequest()
            'ByPass SSL Certificate Validation Checking            
            ServicePointManager.ServerCertificateValidationCallback = Function(se As Object, cert As X509Certificate, chain As X509Chain, sslerror As SslPolicyErrors) True
            Dim urlToken = srU.CreateToken(userName, password, DateTime.Now().AddHours(1)) 'check that the token Is valid
            If urlToken.SuccessLogin Then
                Dim company = urlToken.URL.Split("/")(2).Split(".")(0) 'get company name
                Dim app = FMS.Business.DataObjects.Application.GetFromApplicationName(company) 'get applicationid
                Dim appId = app.ApplicationID
                FMS.Business.ThisSession.User = FMS.Business.DataObjects.User.GetAllUsersForApplication _
                                (appId).Where(Function(x) x.UserName.ToLower = userName.ToLower).Single

                Dim appVehicle = FMS.Business.DataObjects.ApplicationVehicle.GetAll_DraftVer01(appId) 'Get vehicle list
                Dim appVehicleList As New List(Of Business.DataObjects.ApplicationVehicle)
                For Each av In appVehicle
                    If Not av.VINNumber.Equals("") Then
                        appVehicleList.Add(av)
                    End If
                Next
                FMS.Business.ThisSession.ApplicationVehicle = appVehicleList

                ''for testing
                'Dim company = urlToken.URL.Split("/")(2).Split(".")(0) 'get company name
                'Dim app = FMS.Business.DataObjects.Application.GetFromApplicationName("uniqco") 'get applicationid
                'Dim appId = app.ApplicationID

                'Dim appTest = FMS.Business.DataObjects.Application.GetFromApplicationName("uniqco")
                'FMS.Business.ThisSession.User = FMS.Business.DataObjects.User.GetAllUsersForApplication _
                '                    (appTest.ApplicationID).Where(Function(x) x.UserName.ToLower = userName.ToLower).Single
                'FMS.Business.ThisSession.ApplicationID = appTest.ApplicationID
                'Dim userID = FMS.Business.ThisSession.User.UserId
                'Dim userRoledID = FMS.Business.ThisSession.User.RoleID
                'Dim appIDc = FMS.Business.ThisSession.ApplicationID
                'Dim appVehicle = FMS.Business.DataObjects.ApplicationVehicle.GetAll_DraftVer01(appId) 'Get vehicle list
                'Dim appVehicleList As New List(Of Business.DataObjects.ApplicationVehicle)
                'For Each av In appVehicle
                '    If Not av.VINNumber.Equals("") Then
                '        appVehicleList.Add(av)
                '    End If
                'Next
                'FMS.Business.ThisSession.ApplicationVehicle = appVehicleList

                'Response.Redirect(urlToken.URL.Replace("FleetMap.aspx", "DataLoggerReport.aspx").Replace("VinNumber={0}&", ""), False)

                'for testing
                'Response.Redirect(urlToken.URL.Replace("uniqco.nanosoft.com.au", "localhost:18356").Replace("FleetMap.aspx", "DataLoggerReport.aspx").Replace("VinNumber={0}&", ""), False)

                'for testing in demo.nanosoft.com.au:555
                'Response.Redirect(urlToken.URL.Replace("uniqco.nanosoft.com.au", "demo.nanosoft.com.au:555").Replace("FleetMap.aspx", "DataLoggerReport.aspx").Replace("VinNumber={0}&", ""), False)
                'Response.Redirect(urlToken.URL.Replace("uniqco.nanosoft.com.au", "localhost:18356").Replace("FleetMap.aspx", "DataLoggerReport.aspx").Replace("VinNumber={0}&", ""), False)

                Response.Redirect(urlToken.URL.Replace("FleetMap.aspx", "DataLoggerReport.aspx").Replace("VinNumber={0}&", ""), False)
            Else
                FMS.Business.ThisSession.ProblemPageMessage = urlToken.ErrorMessage

                Response.Redirect("~/ProblemPage.aspx", False)
            End If
        Catch ex As Exception
            'FMS.Business.ThisSession.ProblemPageMessage = ex.InnerException.Message
            FMS.Business.ThisSession.ProblemPageMessage = ex.Message
            Response.Redirect("~/ProblemPage.aspx", False)
        End Try
    End Sub

End Class