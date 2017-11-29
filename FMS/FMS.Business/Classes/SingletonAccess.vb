Public Class SingletonAccess

#Region "Historian Logic"

    Private Shared _FMDDataContext As FMS.Business.LINQtoSQLClassesDataContext

    Private Shared Property _HistorianServer As PISDK.Server
    Private Shared Property _HistorianServerSDK As PISDK.PISDK

    Private Shared Property synclock_HistorianServer As New Object

    Public Shared ReadOnly Property HistorianServer As PISDK.Server
        Get

            SyncLock synclock_HistorianServer

                If _HistorianServerSDK Is Nothing Then _HistorianServerSDK = New PISDK.PISDK
                If _HistorianServer Is Nothing Then _HistorianServer = _HistorianServerSDK.Servers.DefaultServer


                Return _HistorianServer

            End SyncLock

        End Get
    End Property

     

    Public Shared Function CreateLatLongTagsForDevice(deviceid As String) As Boolean

        Try 

            Dim tagnameLat As String = String.Format("{0}_lat", deviceid)
            Dim tagnameLong As String = String.Format("{0}_long", deviceid)

            'switch compression OFF for these tags
            Dim nv As New PISDKCommon.NamedValues
            nv.Add("compressing", False)

            Dim pilat As PISDK.PIPoint = HistorianServer.PIPoints.Add(tagnameLat, "classic", PISDK.PointTypeConstants.pttypFloat64, nv)
            Dim pilong As PISDK.PIPoint = HistorianServer.PIPoints.Add(tagnameLong, "classic", PISDK.PointTypeConstants.pttypFloat64, nv)

            Return pilat IsNot Nothing AndAlso pilong IsNot Nothing

        Catch ex As Exception

        End Try

        Return True
    End Function

#End Region

#Region "SQL Logic"



    Private Shared synclock_FMSDataContextNew As New Object

    Friend Shared ReadOnly Property FMSDataContextNew As FMS.Business.LINQtoSQLClassesDataContext
        Get

            'If _FMDDataContext IsNot Nothing Then _FMDDataContext.Dispose()
            ' _FMDDataContext = New FMS.Business.LINQtoSQLClassesDataContext

            SyncLock synclock_FMSDataContextNew
                Return New FMS.Business.LINQtoSQLClassesDataContext
            End SyncLock

        End Get
    End Property

    Private Shared _FMDDataContextContignous As FMS.Business.LINQtoSQLClassesDataContext

    Private Shared synclock_FMSDataContextContignous As New Object

    Friend Shared ReadOnly Property FMSDataContextContignous As FMS.Business.LINQtoSQLClassesDataContext

        Get

            SyncLock synclock_FMSDataContextContignous

                If _FMDDataContextContignous Is Nothing Then
                    _FMDDataContextContignous = New FMS.Business.LINQtoSQLClassesDataContext()
                    _FMDDataContextContignous.Connection.Open()
                End If

                Return _FMDDataContextContignous

            End SyncLock


        End Get
    End Property

    Public Shared Function GetApplicationID(applicationname As String) As System.Guid

        Dim retGUID As System.Guid = Nothing

        retGUID = (From i In FMSDataContextNew.aspnet_Applications _
                    Where i.LoweredApplicationName = applicationname _
                    Select i.ApplicationId).SingleOrDefault

        Return retGUID

    End Function

    Public Shared Sub CreateSetting(settingName)

        Dim stng As New FMS.Business.Setting With {.Name = settingName, .SettingID = Guid.NewGuid}

        FMSDataContextContignous.Settings.InsertOnSubmit(stng)
        FMSDataContextContignous.SubmitChanges()


    End Sub



#End Region

   
#Region "Time-Zone Logic"

    Private Shared Property timezn As DataObjects.TimeZone

    Public Shared Property synclock_TimeZone As New Object



    ''' <summary>
    ''' Singleton object which  holds the time-zone of the applications
    ''' this is is actually the time-zone of the user, if that is not defined, then that of the application
    ''' If THAT is not defined, then is auto-assigns Perth as the timezone for the appcliation 
    ''' this can be altered by the user via the website if theyhave privilages.
    ''' </summary>
    Public Shared Property ClientSelected_TimeZone As DataObjects.TimeZone
        Get
            SyncLock synclock_TimeZone
                'if there has been no timezone set, then we cna only presume that we are talking perth time?!?
                If timezn Is Nothing Then timezn = DataObjects.TimeZone.GetMSoftTZones.Where(Function(x) x.ID = "W. Australia Standard Time").ToList.Single
                Return timezn
            End SyncLock

        End Get
        Set(value As DataObjects.TimeZone)
            SyncLock synclock_TimeZone
                timezn = value
            End SyncLock
        End Set
    End Property


#Region "UW 227 - BusinessLocation (Filter vehicles returned dependant on the user requesting them) - for approval"
    Private Shared Property businessLocation As List(Of DataObjects.ApplicationLocation)
    Public Shared Property synclock_BusinessLocation As New Object

    Public Shared Property ClientSelected_BusinessLocation As List(Of DataObjects.ApplicationLocation)
        Get
            SyncLock synclock_BusinessLocation
                If businessLocation Is Nothing Then businessLocation = DataObjects.ApplicationLocation.GetLocationUsingApplicationID(ThisSession.ApplicationID)
                Return businessLocation
            End SyncLock

        End Get
        Set(value As List(Of DataObjects.ApplicationLocation))
            SyncLock synclock_BusinessLocation
                businessLocation = value
            End SyncLock
        End Set
    End Property
#End Region
    


#End Region





End Class


