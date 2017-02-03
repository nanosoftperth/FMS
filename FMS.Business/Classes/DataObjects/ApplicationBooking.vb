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
        Public ReadOnly Property DriverPhotoBinary() As Byte()
            Get
                Return Driver.PhotoBinary
            End Get
        End Property
        Public Property Driver As ApplicationDriver
#End Region

#Region "constructors"
        ''' <summary>
        ''' for serialization 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(d As FMS.Business.ApplicationBooking)

            With d
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

            End With

        End Sub

#End Region
#Region "CRUD"

        Public Shared Sub Create(ad As DataObjects.ApplicationBooking)

            Dim d As New FMS.Business.ApplicationBooking
            With d
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


            End With

            Dim desti As New FMS.Business.AlertType
            With desti
                .ApplicationAlertTypeID = Guid.NewGuid
                .ApplicationID = ad.ApplicationId
                .DriverId = ad.ApplicationDriverID
                .GeoFenceID = ad.GeofenceDestinationId
                .Timespan_seconds = 0
                .Action = 0
                .SubscriberNativeID = ad.ContactID
                .SendEmail = True
                .SendText = True
                .isBooking = True
            End With

            Dim le As New FMS.Business.AlertType
            With le
                .ApplicationAlertTypeID = Guid.NewGuid
                .ApplicationID = ad.ApplicationId
                .DriverId = ad.ApplicationDriverID
                .GeoFenceID = ad.GeofenceLeaveId
                .Timespan_seconds = 0
                .Action = 0
                .SubscriberNativeID = ad.ContactID
                .SendEmail = True
                .SendText = True
                .isBooking = True
            End With
            SingletonAccess.FMSDataContextContignous.ApplicationBookings.InsertOnSubmit(d)
            SingletonAccess.FMSDataContextContignous.AlertTypes.InsertOnSubmit(desti)
            SingletonAccess.FMSDataContextContignous.AlertTypes.InsertOnSubmit(le)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

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
                End With

                .SubmitChanges()

            End With



        End Sub

        Public Shared Sub Delete(ad As DataObjects.ApplicationBooking)

            Dim d As FMS.Business.ApplicationBooking = (From i In SingletonAccess.FMSDataContextContignous.ApplicationBookings
                                            Where i.ApplicationBookingId = ad.ApplicationBookingId).Single

            SingletonAccess.FMSDataContextContignous.ApplicationBookings.DeleteOnSubmit(d)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

#End Region

#Region "Get methods"


        Public Shared Function GetAllBookingsForApplication(applicationid As Guid) As List(Of DataObjects.ApplicationBooking)

            Return SingletonAccess.FMSDataContextNew.ApplicationBookings. _
                        Where(Function(y) y.ApplicationId = applicationid).Select(Function(d) _
                                                            New DataObjects.ApplicationBooking() With {
                                                                .ApplicationBookingId = d.ApplicationBookingId,
                                                                .ApplicationDriverID = d.ApplicationDriverID,
                                                                .ApplicationId = d.ApplicationId,
                                                                .ArrivalTime = d.ArrivalTime,
                                                                .GeofenceLeave = d.GeofenceLeave,
                                                                .GeofenceDestination = d.GeofenceDestination,
                                                                .GeofenceLeaveId = d.GeofenceLeaveId,
                                                                .GeofenceDestinationId = d.GeofenceDestinationId,
                                                                .ContactID = d.ContactID,
                                                                .CustomerPhone = d.CustomerPhone,
                                                                .CustomerEmail = d.CustomerEmail,
                                                                .IsAlert5min = d.IsAlert5min,
                                                                .IsAlertLeaveForPickup = d.IsAlertLeaveForPickup,
                                                                .Driver = New DataObjects.ApplicationDriver(d.ApplicationDriver)}
                                                            ).OrderByDescending(Function(f) f.ApplicationBookingId).ToList
        End Function


#End Region
    End Class

End Namespace
