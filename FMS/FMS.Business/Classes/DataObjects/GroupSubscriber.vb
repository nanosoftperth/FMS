Namespace DataObjects

    Public Class GroupSubscriber


#Region "properties"

        Public Property GroupSubscriberID As Guid
        Public Property GroupID As Guid
        Public Property SubscriberTypeID As Integer
        Public Property SendEmail As Boolean

        Public Property SendText As Boolean

        Public Property NativeID As Guid

#End Region

#Region "constructors"

        Public Sub New()

        End Sub


        Public Sub New(x As Business.GroupSubscriber)

            Me.GroupSubscriberID = x.GroupSubscriberID
            Me.GroupID = x.GroupID
            Me.SubscriberTypeID = x.SubsciberTypeID
            Me.SendEmail = x.SendEmail
            Me.SendText = x.SendText
            Me.NativeID = x.NativeID

        End Sub

#End Region


#Region "CRUD"

        Public Shared Function Create(x As DataObjects.GroupSubscriber) As Guid

            Dim newDBObject As New Business.GroupSubscriber

            With newDBObject

                .GroupSubscriberID = x.GroupSubscriberID
                .GroupID = x.GroupID
                .SubsciberTypeID = x.SubscriberTypeID
                .SendEmail = x.SendEmail
                .SendText = x.SendText
                .NativeID = x.NativeID
            End With

            SingletonAccess.FMSDataContextContignous.GroupSubscribers.InsertOnSubmit(newDBObject)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Function

        Public Shared Sub Update(x As DataObjects.GroupSubscriber)

            Dim dbobj As FMS.Business.GroupSubscriber = _
                        SingletonAccess.FMSDataContextContignous.GroupSubscribers _
                                .Where(Function(o) o.GroupSubscriberID = x.GroupSubscriberID).Single

            With dbobj

                .GroupSubscriberID = x.GroupSubscriberID
                .GroupID = x.GroupID
                .SubsciberTypeID = x.SubscriberTypeID
                .SendEmail = x.SendEmail
                .SendText = x.SendText
                .NativeID = x.NativeID

            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(x As DataObjects.GroupSubscriber)

            Dim dbobj As FMS.Business.GroupSubscriber = _
                       SingletonAccess.FMSDataContextContignous.GroupSubscribers _
                               .Where(Function(o) o.GroupSubscriberID = x.GroupSubscriberID).Single

            SingletonAccess.FMSDataContextContignous.GroupSubscribers.DeleteOnSubmit(dbobj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub


#End Region

    End Class

End Namespace


