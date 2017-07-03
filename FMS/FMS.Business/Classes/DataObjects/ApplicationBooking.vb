Namespace DataObjects
    Public Class ApplicationBooking

#Region "Properties"
        Public Property ApplicationBookingId As System.Guid
        Public Property ApplicationDriverID As System.Nullable(Of System.Guid)
        Public Property ApplicationId As System.Nullable(Of System.Guid)
        Public Property ArrivalTime As System.Nullable(Of Date)
        Public Property GeofenceLeave As String
        Public Property GeofenceDestination As String
        Public Property GeofenceLeaveId As System.Guid
        Public Property GeofenceDestinationId As System.Guid
        Public Property ContactID As System.Guid
        Public Property CustomerPhone As String
        Public Property CustomerEmail As String
        Public Property IsAlert5min As System.Nullable(Of Boolean)
        Public Property IsAlertLeaveForPickup As System.Nullable(Of Boolean)

        ''' <summary>
        ''' This will be set to 01/jan/2970 if there is a NULL in the database for 
        ''' legacy reasons (cannot delete older data)
        ''' </summary>
        Public Property DateCreated As Date

        Public ReadOnly Property DriverPhotoBinary() As Byte()
            Get
                Return Driver.PhotoBinary
            End Get
        End Property
        Public Property Driver As DataObjects.ApplicationDriver
#End Region

#Region "constructors"

        ''' <summary>
        ''' for serialization 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(d As FMS.Business.ApplicationBooking)

            With Me
                .ApplicationBookingId = d.ApplicationBookingId
                .ApplicationDriverID = d.ApplicationDriverID
                .ApplicationId = d.ApplicationId
                .ArrivalTime = d.ArrivalTime
                .GeofenceLeave = d.GeofenceLeave
                .GeofenceDestination = d.GeofenceDestination
                .GeofenceLeaveId = d.GeofenceLeaveId
                .GeofenceDestinationId = d.GeofenceDestinationId
                .ContactID = d.ContactID
                .CustomerPhone = d.CustomerPhone
                .CustomerEmail = d.CustomerEmail
                .IsAlert5min = d.IsAlert5min
                .IsAlertLeaveForPickup = d.IsAlertLeaveForPickup

                .Driver = New DataObjects.ApplicationDriver(d.ApplicationDriver) 'inefficient! Ryan legacy

                .DateCreated = If(d.DateCreated.HasValue, d.DateCreated.Value, CDate("01 jan 1970"))
            End With

        End Sub

#End Region

#Region "CRUD"

        ''' <summary>
        ''' Normal CRUD create, this also creates two alerttypes for leaving and destination geofence-alerts
        ''' </summary>
        Public Shared Sub Create(ad As DataObjects.ApplicationBooking)

            Dim booking As New FMS.Business.ApplicationBooking

            'create a DB booking object 
            With booking
                .ApplicationBookingId = Guid.NewGuid
                .ApplicationDriverID = ad.ApplicationDriverID
                .ApplicationId = ad.ApplicationId
                .ArrivalTime = ad.ArrivalTime
                .GeofenceLeave = ad.GeofenceLeave
                .GeofenceDestination = ad.GeofenceDestination
                .GeofenceLeaveId = ad.GeofenceLeaveId
                .GeofenceDestinationId = ad.GeofenceDestinationId
                .ContactID = ad.ContactID
                .CustomerPhone = ad.CustomerPhone
                .CustomerEmail = ad.CustomerEmail
                .IsAlert5min = If(ad.IsAlert5min Is Nothing, False, ad.IsAlert5min)
                .IsAlertLeaveForPickup = If(ad.IsAlertLeaveForPickup Is Nothing, False, ad.IsAlertLeaveForPickup)
                .DateCreated = Now
            End With

            'create the booking in the DB 
            SingletonAccess.FMSDataContextContignous.ApplicationBookings.InsertOnSubmit(booking)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            'create both the 
            Dim alrt As New FMS.Business.DataObjects.AlertType _
                            With {.ApplicationAlertTypeID = Guid.NewGuid _
                                , .ApplicationID = ad.ApplicationId _
                                , .DriverID = ad.ApplicationDriverID _
                                , .Time_Period_mins = 0 _
                                , .SubscriberNativeID = ad.ContactID _
                                , .SendEmail = True _
                                , .SendText = True _
                                , .BookingID = booking.ApplicationBookingId _
                                , .isBooking = True}

            'create "leaving from" alert (geo-fence alert)
            alrt.ApplicationAlertTypeID = Guid.NewGuid
            alrt.Action = AlertType.ActionType.Leaves
            alrt.GeoFenceId = ad.GeofenceLeaveId
            Business.DataObjects.AlertType.Insert(alrt)

            'create "arriving to" alert (geo-fence alert)
            alrt.ApplicationAlertTypeID = Guid.NewGuid
            alrt.Action = AlertType.ActionType.Enters
            alrt.GeoFenceId = ad.GeofenceDestinationId
            Business.DataObjects.AlertType.Insert(alrt)

        End Sub

        Public Shared Sub Update(ad As DataObjects.ApplicationBooking)


            With New LINQtoSQLClassesDataContext

                Dim d As FMS.Business.ApplicationBooking = (From i In .ApplicationBookings
                                                        Where i.ApplicationBookingId = ad.ApplicationBookingId).Single

                With d
                    .ApplicationDriverID = ad.ApplicationDriverID
                    '.ApplicationId = ad.ApplicationId
                    .ArrivalTime = ad.ArrivalTime
                    .GeofenceLeave = ad.GeofenceLeave
                    .GeofenceDestination = ad.GeofenceDestination
                    .GeofenceLeaveId = ad.GeofenceLeaveId
                    .GeofenceDestinationId = ad.GeofenceDestinationId
                    .ContactID = ad.ContactID
                    .CustomerPhone = ad.CustomerPhone
                    .CustomerEmail = ad.CustomerEmail
                    .IsAlert5min = If(ad.IsAlert5min Is Nothing, False, ad.IsAlert5min)
                    .IsAlertLeaveForPickup = If(ad.IsAlertLeaveForPickup Is Nothing, False, ad.IsAlertLeaveForPickup)
                    .DateCreated = If(d.DateCreated.HasValue, d.DateCreated, CDate("01 jan 1970"))
                End With

                .SubmitChanges()

            End With



        End Sub


        ''' <summary>
        ''' deletes the booking AND the alerts which were creted 
        ''' </summary>
        ''' <param name="ad"></param>
        ''' <remarks></remarks>
        Public Shared Sub Delete(ad As DataObjects.ApplicationBooking)

            Dim d As FMS.Business.ApplicationBooking = (From i In SingletonAccess.FMSDataContextContignous.ApplicationBookings
                                            Where i.ApplicationBookingId = ad.ApplicationBookingId).Single

            'find all of the related alert types and delete them 
            Dim alertTypes As List(Of Business.AlertType) = (From x In SingletonAccess.FMSDataContextContignous.AlertTypes
                                                              Where x.BookingID = ad.ApplicationBookingId).ToList

            'send the changes to the database
            SingletonAccess.FMSDataContextContignous.AlertTypes.DeleteAllOnSubmit(alertTypes)
            SingletonAccess.FMSDataContextContignous.ApplicationBookings.DeleteOnSubmit(d)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

#End Region

#Region "Get methods"

        Public Shared Function GetFromID(Id As Guid) As DataObjects.ApplicationBooking

            'Designed to cause exception if there is nothing in the DB matching
            Return (From x In SingletonAccess.FMSDataContextNew.ApplicationBookings _
                     Where x.ApplicationBookingId = Id
                     Select New DataObjects.ApplicationBooking(x)).FirstOrDefault


        End Function

        Public Shared Function GetAllBookingsForApplication(applicationid As Guid) As List(Of DataObjects.ApplicationBooking)

            Return (From x In SingletonAccess.FMSDataContextNew.ApplicationBookings _
                     Where x.ApplicationId = applicationid _
                     Select New DataObjects.ApplicationBooking(x)).ToList

        End Function


#End Region
    End Class

End Namespace
