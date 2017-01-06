
Namespace DataObjects
    Public Class Application


#Region "Properties"

        Public Property ApplicationName As String
        Public Property ApplicationID As Guid
        Public Property TimeZone As DataObjects.TimeZone
        Public Property DefaultBusinessLocationID As Guid?

        Public ReadOnly Property Settings As List(Of FMS.Business.DataObjects.Setting)
            Get
                Return Setting.GetSettingsForApplication(Me.ApplicationName.ToLower)
            End Get
        End Property
       
        Public ReadOnly Property GetLogoBinary() As Byte()
            Get

                Dim stng As Setting = (From x In Settings Where x.Name = "Logo").Single

                Return stng.ValueObj

            End Get
        End Property

        Public Function GetAllDevicesNames() As List(Of String)

            Return SingletonAccess.FMSDataContextNew.Devices.Where(Function(x) x.ApplicationID = Me.ApplicationID) _
                                                                                .Select(Function(y) y.DeviceID).ToList

        End Function

#End Region

#Region "constructors"


        ''' <summary>
        ''' for serialization only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(a As FMS.Business.aspnet_Application)

            Me.ApplicationID = a.ApplicationId
            Me.ApplicationName = a.ApplicationName

            Me.DefaultBusinessLocationID = a.DefaultApplicationLocationID

            'if the timezone has been SET, then use the timezoneinfo object 
            'if not, then use the google object which is gotten from their WEB API
            If a.TimeZoneSerialisedObj IsNot Nothing Then
                Me.TimeZone = New TimeZone(a.TimeZoneSerialisedObj)
            Else

                'if we have NEVER set a timezone, then guess it here.
                If String.IsNullOrEmpty(a.TimeZoneID) Then

                    Dim tz = GetTimezoneFromLatLong()
                    SaveTimeZone(tz)
                End If

                Me.TimeZone = New DataObjects.TimeZone()

                With Me.TimeZone

                    .Description = a.TimezoneDescription
                    .ID = a.TimeZoneID
                    .Offset = a.TimeZoneOffset
                    .DST_Offset = a.TimeZoneDSTOffset
                    .CurrentOffset = .Offset + .DST_Offset
                End With

            End If
        End Sub



        Public Shared Function CreateNew(applicationName As String) As DataObjects.Application
            Try

                Dim a As New aspnet_Application

                a.ApplicationId = Guid.NewGuid
                a.ApplicationName = applicationName

                a.LoweredApplicationName = applicationName.ToLower

                SingletonAccess.FMSDataContextContignous.aspnet_Applications.InsertOnSubmit(a)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()

                Return New DataObjects.Application(a)

            Catch ex As Exception
                Return Nothing
            End Try

        End Function

#End Region

#Region "gets / sets"


        Public Function GetAdminUser() As DataObjects.User

            Return DataObjects.User.GetAllUsersForApplication(Me.ApplicationID).Where(Function(x) x.UserName.ToLower = "admin").SingleOrDefault

        End Function

        Public Shared Function GetFromApplicationName(appName As String) As DataObjects.Application

            Dim y As FMS.Business.aspnet_Application = SingletonAccess.FMSDataContextNew.aspnet_Applications.Where(Function(x) x.LoweredApplicationName = appName.ToLower).SingleOrDefault

            If y IsNot Nothing Then
                Return New DataObjects.Application(y)
            Else
                Return Nothing
            End If

        End Function

        Public Shared Function GetAllApplications() As List(Of DataObjects.Application)

            Return (From i In SingletonAccess.FMSDataContextNew.aspnet_Applications _
                             Select New DataObjects.Application(i)).ToList

        End Function

        Public Shared Function GetFromAppID(appid As Guid) As DataObjects.Application


            Return (From i In SingletonAccess.FMSDataContextNew.aspnet_Applications _
                        Where i.ApplicationId = appid _
                        Select New DataObjects.Application(i)).Single

        End Function

#End Region





#Region "timezone methods"

        Public Sub SaveDefaultBusinessLocation(businessLocationID As Guid)


            Dim a As Business.aspnet_Application = (From x In SingletonAccess.FMSDataContextContignous.aspnet_Applications _
                                                     Where x.ApplicationId = Me.ApplicationID).Single

            With a
                a.DefaultApplicationLocationID = businessLocationID
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()


        End Sub

        Public Sub UpdateTimeZoneData()

            Dim a As Business.aspnet_Application = SingletonAccess.FMSDataContextNew.aspnet_Applications.Where( _
                                                                        Function(x) x.ApplicationId = Me.ApplicationID).ToList.Single

            'if we have NEVER set a timezone, then guess it here.
            If String.IsNullOrEmpty(a.TimeZoneSerialisedObj) Then

                Dim tz = GetTimezoneFromLatLong()
                SaveTimeZone(tz)
            End If

        End Sub


        Public Sub SaveTimeZone(tz As DataObjects.TimeZone)

            Me.TimeZone = tz

            Dim a As Business.aspnet_Application = (From x In SingletonAccess.FMSDataContextContignous.aspnet_Applications _
                                                     Where x.ApplicationId = Me.ApplicationID).Single

            With tz
                a.TimeZoneID = .ID
                a.TimeZoneOffset = .Offset
                a.TimeZoneDSTOffset = .DST_Offset
                a.TimeZoneSerialisedObj = .SerializedTimezoneObject
                a.TimezoneDescription = .Description
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Function GetTimezoneFromLatLong() As DataObjects.TimeZone

            Dim lat As Double = CDbl(Me.Settings.Where(Function(s) s.Name = "Business_Lattitude").Single.Value)
            Dim lng As Double = CDbl(Me.Settings.Where(Function(s) s.Name = "Business_Longitude").Single.Value)

            Dim googTZResp = GoogleAPI.GetCurrentTimeZoneOffset(lat, lng)

            Dim splitstr() As String = googTZResp.time_zone_id.Split("/")

            If splitstr.Count > 0 Then

                Dim cityName As String = googTZResp.time_zone_id.Split("/")(1).ToLower

                'find the correct timezone object from all possible
                For Each tz In System.TimeZoneInfo.GetSystemTimeZones

                    If tz.DisplayName.ToLower.Contains(cityName) Then

                        Return New DataObjects.TimeZone(tz)
                    End If
                Next

            End If

            Return New DataObjects.TimeZone(googTZResp)

        End Function

#End Region

    End Class
End Namespace

