Imports FMS.Business.DataObjects.FeatureListConstants
Imports FMS.Business

Public Class RootMaster
    Inherits System.Web.UI.MasterPage

    Public Shared ReadOnly Property WebVersion As String
        Get
            Return My.Settings.version
        End Get
    End Property

    Public Shared ReadOnly Property CompanyName As String
        Get
            Return My.Settings.debug_company_name
        End Get
    End Property

    '   Page event order (from MSDN)
    '   =============================
    '1  Content page PreInit event.
    '2  Master page controls Init event.
    '3  Content controls Init event.
    '4  Master page Init event.
    '5  Content page Init event.    
    '6  Content page Load event.
    '7  Master page Load event.         
    '8  Master page controls Load event.
    '9  Content page controls Load event.
    '10 Content page PreRender event.
    '11 Master page PreRender event.
    '12 Master page controls PreRender event.    
    '13 Content page controls PreRender event.
    '14 Master page controls Unload event.
    '15 Content page controls Unload event.
    '16 Master page Unload event.
    '17 Content page Unload event.

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Init
        'we use the binary controls init event as this definitley happens BEFORE anything else (see above page event order list)
        'This method is left here as information.
    End Sub


    Private Sub initiate()
        'if this is a postback AND we already have an application name, then exit the sub 
        If IsPostBack And Membership.ApplicationName <> "/" Then Exit Sub


        '====================================================================================
        '|==================================================================================|
        '|                  FIND THE APPLICATION NAME, ADD TO SESSION STATE                 |
        '|==================================================================================|
        '====================================================================================

        'IF this is a test machine, then set the company name to whatever we have in the settings
        'if not, then get the company name from the SERVER_NAME server variable

        Dim servername As String = Request.ServerVariables("SERVER_NAME")

        If servername.ToLower = "localhost" Then
            'Here the company name is set to the "debug" value, such as "cannon" or "PPJS"
            Membership.ApplicationName = My.Settings.debug_company_name
        Else
            'get the company name from the SERVER_NAME server variable
            Dim strs() As String = servername.Split(".")
            If strs.Length < 1 Then Throw New Exception(String.Format("unxepected application name ({0})", servername))
            Membership.ApplicationName = strs(0)
        End If

        Select Case Membership.ApplicationName.ToLower
            Case "cannon"
                XmlDataSourceHeader.DataFile = "~/App_Data/TopMenu_cannon_v1.2.xml"
            Case "uniqco"
                XmlDataSourceHeader.DataFile = "~/App_Data/TopMenu_uniqco_v1.2.xml"
            Case "demo"
                XmlDataSourceHeader.DataFile = "~/App_Data/TopMenu_demo_v1.2.xml"
            Case Else
                XmlDataSourceHeader.DataFile = "~/App_Data/TopMenu_v1.2.xml"
        End Select


        SetApplicationSessionVars()

        Dim allowedPages As List(Of String) = My.Settings.NoAuthenticationRequiredURLs.Cast(Of String).ToList

        If allowedPages.Where(Function(x) x.ToLower = Request.Url.AbsolutePath.ToLower).Count > 0 Then Exit Sub

        '====================================================================================
        '|==================================================================================|
        '|                  lOG THE USER INTO THE APPLICATION                               |
        '|==================================================================================|
        '====================================================================================

        'look to see if there is a token defined in the URL
        Dim tokenID As String = Request.QueryString("token")

        If Not String.IsNullOrEmpty(tokenID) Then

            Dim tokenGUID As Guid

            Dim tokenWasValidGUID As Boolean = Guid.TryParse(tokenID, tokenGUID)
            Dim tokenErrorMsg As String = String.Empty

            If Not tokenWasValidGUID Then
                tokenErrorMsg &= String.Format("""{0}"" is not a valid token format.{1}", tokenID, vbNewLine)
            Else
                Dim tokenresp As DataObjects.AuthenticationToken = DataObjects.AuthenticationToken.GetTokenFromID(tokenGUID)

                If tokenresp.ExpiryDate < Now Then tokenErrorMsg &= String.Format("The authentication token ""{0} expired on {0}{1}" _
                                                                                , tokenID, tokenresp.ExpiryDate.ToLongDateString, vbNewLine)

                Dim a As DataObjects.Application = DataObjects.Application.GetFromAppID(tokenresp.ApplicationID)
                Membership.ApplicationName = a.ApplicationName

                If tokenresp.ExpiryDate > Now Then FormsAuthentication.SetAuthCookie("admin", True)

            End If

            FMS.Business.ThisSession.ProblemPageMessage = tokenErrorMsg

            If Not String.IsNullOrEmpty(tokenErrorMsg) Then Response.Redirect("~/ProblemPage.aspx", True)

            'NOTE: The cookie does not register unless you FULL refresh the page. 
            'dont know why, adding the above cookie means that we can authenticate the user without 
            'needing the password (when using the token)
            If Membership.GetUser Is Nothing Then Response.Redirect(Request.Url.ToString, True)

        End If

        SetApplicationSessionVars()

        'if not logged in, then redirect to the login page (applies to all pages so placed in the master page)
        If Membership.GetUser Is Nothing Then
            FormsAuthentication.RedirectToLoginPage()
            Exit Sub
        End If

        '====================================================================================
        '|==================================================================================|
        '|                  AFTER VERIFYING USER, SET SESSION VARIABLES                     |
        '|==================================================================================|
        '====================================================================================

        Dim uName = Membership.GetUser.UserName

        FMS.Business.ThisSession.User = FMS.Business.DataObjects.User.GetAllUsersForApplication _
                        (FMS.Business.ThisSession.ApplicationID).Where(Function(x) x.UserName.ToLower = uName.ToLower).Single

        'SET the userid in the session parameters
        FMS.Business.ThisSession.UserID = FMS.Business.ThisSession.User.UserId
    End Sub


    Private Sub SetApplicationSessionVars()
        'Set the application based data in the session state
        FMS.Business.ThisSession.ApplicationName = Membership.ApplicationName
        FMS.Business.ThisSession.ApplicationObject = FMS.Business.DataObjects.Application.GetFromApplicationName(Membership.ApplicationName)
        FMS.Business.ThisSession.ApplicationID = FMS.Business.ThisSession.ApplicationObject.ApplicationID
    End Sub

    Protected Sub btnUniqco_Click(sender As Object, e As EventArgs) Handles btnUniqco.Click
        Response.Redirect(FMS.Business.ThisSession.ReturnToUniqcoURL)
    End Sub

    Private Sub binaryImageLogo_CallbackError(sender As Object, e As EventArgs) Handles binaryImageLogo.CallbackError

    End Sub

    Private Sub binaryImageLogo_Init(sender As Object, e As EventArgs) Handles binaryImageLogo.Init

        initiate()

        If Not IsPostBack Then

            btnUniqco.ClientVisible = FMS.Business.ThisSession.ShowReturnToUniqcoButton

            Dim settings As List(Of FMS.Business.DataObjects.Setting) = _
                 FMS.Business.DataObjects.Setting.GetSettingsForApplication(Membership.ApplicationName)

            With settings.Where(Function(s) s.Name = "Logo").Single()

                If .ValueObj IsNot Nothing Then
                    FMS.Business.ThisSession.header_logoBinary = .ValueObj
                End If
            End With

            FMS.Business.ThisSession.header_companyName = settings.Where(Function(y) y.Name = "CompanyName").Single.Value & " - "
            FMS.Business.ThisSession.header_applicationName = "   " & settings.Where(Function(y) y.Name = "ApplicationName").Single.Value

            'Get the latest data from google about the time-zone which was initially auto-detected.
            FMS.Business.ThisSession.ApplicationObject.UpdateTimeZoneData()

            'Set the timezone for the business layer to be = to the user (the user timezone is taken from the application
            'timezone if they havent explicitly defined one)

            If FMS.Business.ThisSession.User IsNot Nothing Then

                FMS.Business.SingletonAccess.ClientSelected_TimeZone = _
                    If(String.IsNullOrEmpty(FMS.Business.ThisSession.User.TimeZone.ID), FMS.Business.ThisSession.ApplicationObject.TimeZone, FMS.Business.ThisSession.User.TimeZone)

                'set the users business location here (in the singleton)
                Dim usrAppID = FMS.Business.ThisSession.User.ApplicationID
                Dim userBusinessLocation = FMS.Business.DataObjects.ApplicationLocation.GetLocationUsingApplicationID(usrAppID)

                'FMS.Business.SingletonAccess.ClientSelected_BusinessLocation = ThisSession.User.
                FMS.Business.SingletonAccess.ClientSelected_BusinessLocation = userBusinessLocation
            Else
                FMS.Business.SingletonAccess.ClientSelected_TimeZone = FMS.Business.ThisSession.ApplicationObject.TimeZone
            End If

        End If

        Me.binaryImageLogo.ContentBytes = FMS.Business.ThisSession.header_logoBinary
        Me.binaryImageLogo.DataBind()

        header_companyName.InnerText = FMS.Business.ThisSession.header_companyName
        header_applicationName.InnerText = FMS.Business.ThisSession.header_applicationName
    End Sub


End Class