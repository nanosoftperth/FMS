Namespace DataObjects
    Public Class Group

#Region "properties"

        Public Property GroupID As Guid
        Public Property GroupName As String
        Public Property ApplicationID As Guid

#End Region

#Region "constructors"

        Public Sub New()

        End Sub


        Public Sub New(x As Business.Group)

            Me.GroupID = x.GroupID
            Me.GroupName = x.GroupName
            Me.ApplicationID = x.ApplicationID

        End Sub

#End Region

#Region "CRUD"

        Public Shared Function Create(x As DataObjects.Group) As Guid

            Try

                Dim newDBObject As New FMS.Business.Group

                With newDBObject

                    .GroupID = If(x.GroupID = Guid.Empty, Guid.NewGuid, x.GroupID)
                    .GroupName = x.GroupName
                    .ApplicationID = x.ApplicationID
                End With

                SingletonAccess.FMSDataContextContignous.Groups.InsertOnSubmit(newDBObject)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()

                Return x.GroupID

            Catch ex As Exception
                Dim str As String = ex.Message
            End Try

        End Function

        Public Shared Sub Update(x As DataObjects.Group)

            Dim dbobj As FMS.Business.Group = _
                        SingletonAccess.FMSDataContextContignous.Groups _
                                .Where(Function(o) o.GroupID = x.GroupID).Single

            With x

                dbobj.ApplicationID = .ApplicationID
                dbobj.GroupName = .GroupName
                dbobj.GroupID = .GroupID

            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(x As DataObjects.Group)

            Dim dbobj As FMS.Business.Group = _
                       SingletonAccess.FMSDataContextContignous.Groups _
                               .Where(Function(o) o.GroupID = x.GroupID).Single

            SingletonAccess.FMSDataContextContignous.Groups.DeleteOnSubmit(dbobj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub


#End Region

#Region "gets and sets"

        Public Shared Function GetForAlertID(alertid As Guid) As List(Of DataObjects.Group)


            Throw New Exception("not implemented yet!")

            'Return (From x In SingletonAccess.FMSDataContextNew.Groups _
            '         Where x. = GroupID _
            '         Select New DataObjects.Group(x)).ToList
        End Function

        Public Shared Function GetForApplication(appid As Guid) As List(Of DataObjects.Group)

            Return (From x In SingletonAccess.FMSDataContextNew.Groups _
                    Where x.ApplicationID = appid _
                    Select New DataObjects.Group(x)).ToList

        End Function

        Public Shared Function GetFromGroupID(groupid As Guid) As DataObjects.Group

            Return (From x In SingletonAccess.FMSDataContextNew.Groups _
                    Where x.GroupID = groupid _
                    Select New DataObjects.Group(x)).Single

        End Function


        Public Shared Function GetSubscribers(groupid As Guid) As List(Of DataObjects.GroupSubscriber)


            Return (From x In SingletonAccess.FMSDataContextNew.GroupSubscribers _
                     Where x.GroupID = groupid _
                     Select New DataObjects.GroupSubscriber(x)).ToList

        End Function

        Public Shared Sub RemoveSubscribersByIDs(groupid As Guid, nativeids As List(Of Guid))

            Dim gs As New List(Of FMS.Business.GroupSubscriber)

            For Each g As Guid In nativeids

                Dim itm As FMS.Business.GroupSubscriber

                itm = (From x In SingletonAccess.FMSDataContextContignous.GroupSubscribers _
                        Where x.NativeID = g _
                        And x.GroupID = groupid).FirstOrDefault

                If itm IsNot Nothing Then _
                        SingletonAccess.FMSDataContextContignous.GroupSubscribers.DeleteOnSubmit(itm)
            Next

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub AddSubscribersByIDs(groupid As Guid, subscriberids As List(Of Guid))

            Dim newGroupSubs As New List(Of FMS.Business.GroupSubscriber)
            Dim lst As List(Of DataObjects.Subscriber) = Subscriber.GetAllForGroup(groupid)

            For Each nativeid As Guid In subscriberids

                Dim groupSub As New FMS.Business.GroupSubscriber

                With groupSub
                    .GroupID = groupid
                    .GroupSubscriberID = Guid.NewGuid
                    .NativeID = nativeid
                    .SendEmail = True
                    .SendText = False
                    '.SubsciberTypeID = (From s In subs Where s.NativeID = .NativeID).Single.SubscriberType_Str
                End With

                Dim alreadyExists As Boolean = lst.Where(Function(x) x.NativeID = nativeid).Count > 0

                If Not alreadyExists Then newGroupSubs.Add(groupSub)

            Next

            SingletonAccess.FMSDataContextContignous.GroupSubscribers.InsertAllOnSubmit(newGroupSubs)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub


#End Region

    End Class

End Namespace


