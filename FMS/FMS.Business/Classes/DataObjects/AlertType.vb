
Namespace DataObjects

    ''' <summary>
    ''' this class ihas been specifically created for binding purposes with the aspxGridView on the alerts page
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ActionEnumOption


        Public Property EnumValue As Integer
        Public Property EnumString As String

        Public Shared Function GetAll() As List(Of ActionEnumOption)

            Dim lst As New List(Of ActionEnumOption)

            lst.Add(New ActionEnumOption() With {.EnumString = "Enters", .EnumValue = 0})
            lst.Add(New ActionEnumOption() With {.EnumString = "Leaves", .EnumValue = 1})

            Return lst

        End Function

        ''' <summary>
        ''' for serialization only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

    End Class

    Public Class AlertType

#Region "Properties / enums"

        Public Enum ActionType
            Enters = 0
            Leaves = 1
        End Enum

        Public Property ApplicationAlertTypeID As Guid
        Public Property ApplicationID As Guid
        Public Property DriverID As Guid
        Public Property GeoFenceId As Guid
        Public Property Time_Period_mins As Integer
        Public Property DeliveryGroup As Guid?
        Public Property Action As ActionType
        Public Property Email As String
        Public Property SubscriberNativeID As Guid?
        Public Property SendEmail As Boolean
        Public Property SendText As Boolean

        Public Property BookingID As Guid?

        Public Property isBooking As Boolean
        Public Property isSent As Boolean

#End Region


#Region "Getters and setters"



        Public Shared Function GetALLForApplication(appID As Guid) As List(Of DataObjects.AlertType)


            Return (From x In SingletonAccess.FMSDataContextNew.AlertTypes _
                     Where x.ApplicationID = appID _
                     Select New DataObjects.AlertType(x)).ToList

        End Function
        Public Shared Function GetAllForApplicationIncludingOpenBookings(appID As Guid) As List(Of DataObjects.AlertType)

            Return (From x In SingletonAccess.FMSDataContextNew.AlertTypes _
                     Where (x.ApplicationID = appID And (x.isBooking = False Or (x.isBooking = True And x.isSent = False)))
                     Select New DataObjects.AlertType(x)).ToList
           
        End Function

        Public Shared Function GetAllForApplicationWithoutBookings(appID As Guid) As List(Of DataObjects.AlertType)


            If appID = New Guid() Then
                Return (From x In SingletonAccess.FMSDataContextNew.AlertTypes _
                     Where x.ApplicationID = ThisSession.ApplicationID And x.isBooking = False _
                     Select New DataObjects.AlertType(x)).ToList

            Else
                Return (From x In SingletonAccess.FMSDataContextNew.AlertTypes _
                                   Where x.ApplicationID = appID And x.isBooking = False _
                                   Select New DataObjects.AlertType(x)).ToList
            End If 

        End Function


#End Region

#Region "CRUD"

        Public Shared Function Insert(at As DataObjects.AlertType) As Guid

            Dim newObj As New FMS.Business.AlertType

            Try

                If at.ApplicationID = Guid.Empty Then Throw New Exception("the applicationID was left blank.")

                With newObj
                    .Action = at.Action
                    .ApplicationAlertTypeID = If(at.ApplicationAlertTypeID = Guid.Empty, Guid.NewGuid, at.ApplicationAlertTypeID)
                    .ApplicationID = at.ApplicationID
                    If at.DeliveryGroup.HasValue Then .DeliveryGroup = at.DeliveryGroup.Value
                    .DriverId = at.DriverID
                    .GeoFenceID = at.GeoFenceId
                    .Timespan_seconds = at.Time_Period_mins * 60
                    .SubscriberNativeID = If(at.SubscriberNativeID.HasValue, at.SubscriberNativeID, Nothing)
                    .BookingID = at.BookingID 'bookingID
                    .SendEmail = at.SendEmail
                    .SendText = at.SendText
                    .isBooking = at.isBooking
                    .isSent = at.isSent
                End With

                SingletonAccess.FMSDataContextContignous.AlertTypes.InsertOnSubmit(newObj)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()

                Return newObj.ApplicationAlertTypeID

            Catch ex As Exception
                Throw
            End Try
        End Function

        Public Shared Sub Update(at As DataObjects.AlertType)

            Dim foundObj As FMS.Business.AlertType

            foundObj = SingletonAccess.FMSDataContextContignous.AlertTypes. _
                                Where(Function(x) x.ApplicationAlertTypeID = at.ApplicationAlertTypeID).SingleOrDefault

            If foundObj Is Nothing Then Throw New Exception( _
                                "Could not find related ""AlertType"" object in database")

            With foundObj

                .Action = at.Action
                '.ApplicationAlertID = at.ApplicationID
                .ApplicationID = at.ApplicationID
                If at.DeliveryGroup.HasValue Then .DeliveryGroup = at.DeliveryGroup.Value
                .DriverId = at.DriverID
                '.Email = at.Email
                .GeoFenceID = at.GeoFenceId
                .Timespan_seconds = at.Time_Period_mins * 60

                .BookingID = at.BookingID

                .SubscriberNativeID = If(at.SubscriberNativeID.HasValue, at.SubscriberNativeID, Nothing)

                .SendEmail = at.SendEmail
                .SendText = at.SendText
                .isBooking = at.isBooking
                .isSent = at.isSent

            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(at As DataObjects.AlertType)

            Dim foundObj As FMS.Business.AlertType

            foundObj = SingletonAccess.FMSDataContextContignous.AlertTypes. _
                                Where(Function(x) x.ApplicationAlertTypeID = at.ApplicationAlertTypeID).SingleOrDefault

            If foundObj Is Nothing Then Throw New Exception( _
                                "Could not find related ""AlertType"" object in database")


            SingletonAccess.FMSDataContextContignous.AlertTypes.DeleteOnSubmit(foundObj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(dbobj As FMS.Business.AlertType)

            With dbobj

                Action = .Action
                ApplicationAlertTypeID = .ApplicationAlertTypeID
                ApplicationID = .ApplicationID

                If .DeliveryGroup.HasValue Then DeliveryGroup = .DeliveryGroup.Value

                DriverID = .DriverId
                
                SendEmail = If(.SendEmail.HasValue, .SendEmail.Value, False)
                SendText = If(.SendText.HasValue, .SendText.Value, False)

                isBooking = If(.isBooking.HasValue, .isBooking.Value, False)
                isSent = If(.isSent.HasValue, .isSent.Value, False)

                Me.BookingID = dbobj.BookingID

                GeoFenceId = .GeoFenceID
                Time_Period_mins = .Timespan_seconds / 60

                If .SubscriberNativeID.HasValue Then
                    SubscriberNativeID = .SubscriberNativeID
                    'Dim o As Subscriber = Subscriber.GetAllforApplication(.ApplicationID).Where(Function(x) x.NativeID = .SubscriberNativeID.Value).FirstOrDefault
                    'If o IsNot Nothing Then

                End If

            End With

        End Sub

#End Region

    End Class




End Namespace

