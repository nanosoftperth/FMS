
Namespace ReportGeneration

    Public Class AlertTypeUnprocessedCollission_Report


#Region "properties"

        Public Property ID As Integer
        Public Property DeviceID As String
        Public Property StartTime As Date?
        Public Property EndTime As Date?
        Public Property GeoFence_Description As String
        Public Property Vehicle_Name As String
        Public Property GeoFence_Name As String
        Public Property ApplicationGeoFenceID As Guid
        Public Property ApplicationVehicleID As Guid
        Public Property ApplicationDriverID As Guid?
        Public Property GeoFenceCollissoinID As Guid
        Public Property ApplicationID As Guid
        Public Property PhoneNumber As String
        Public Property Emails As String
        Public Property DriverName As String
        Public Property MessageContent As String


        '======     DRIVER DETAILS  ===========
        Public Property Driver_PhoneNumber As String
        Public Property Driver_FristName As String
        Public Property Driver_Surname As String


        'CALCULATED PROPERTIES
        Public ReadOnly Property TimeTakes As TimeSpan
            Get

                Dim thisEndtime As Date = If(EndTime.HasValue, EndTime.Value, Now)
                Return thisEndtime - StartTime
            End Get
        End Property


#End Region

#Region "constructors"

        ''' <summary>
        ''' required for serialization purpoes only 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(db_obj As Business.usp_GetUnprocessedCollissionsForAlertTypeResult)


            With db_obj

                Me.ApplicationDriverID = .ApplictionID

                Me.ApplicationDriverID = .ApplicationDriverID
                Me.ApplicationGeoFenceID = .ApplicationGeoFenceID
                Me.ApplicationVehicleID = .ApplicationVehicleID
                Me.DeviceID = .DeviceID
                Me.DriverName = .Driver_Name
                Me.Emails = .Emails
                Me.EndTime = .EndTime
                Me.GeoFence_Description = .GeoFence_Description
                Me.GeoFence_Name = .GeoFence_Name
                Me.GeoFenceCollissoinID = .GeoFenceDeviceCollissionID
                Me.ID = .id
                Me.MessageContent = .MessageContent
                Me.PhoneNumber = .PhoneNumber
                Me.StartTime = .StartTime
                Me.Vehicle_Name = .Vehicle_Name

                ''driver details
                'Me.Driver_FristName = .Driver_FristName
                'Me.Driver_PhoneNumber = .Driver_PhoneNumber
                'Me.Driver_Surname = .Driver_Surname


            End With

        End Sub

#End Region


#Region "gets/sets"


        ''' <summary>
        ''' Gets all the Unprocessed COllissions for an alert type
        ''' It is presumed that you want the latest 
        ''' </summary>
        ''' <param name="AlertTypeID"></param>
        ''' <param name="startDate"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetForAlertType(appId As Guid, AlertTypeID As Guid, startDate As Date)

            Return GetForAlertType(appId, AlertTypeID, startDate, Now.timezoneToPerth)

        End Function

        Public Shared Function GetForAlertType(appId As Guid, AlertTypeID As Guid, startDate As Date, endDate As Date)

            Dim lst As List(Of usp_GetUnprocessedCollissionsForAlertTypeResult) = _
                            SingletonAccess.FMSDataContextContignous.usp_GetUnprocessedCollissionsForAlertType(appId, startDate, endDate, AlertTypeID).ToList()


            Return (From x In lst Select New ReportGeneration.AlertTypeUnprocessedCollission_Report(x)).ToList


        End Function

#End Region



    End Class


End Namespace