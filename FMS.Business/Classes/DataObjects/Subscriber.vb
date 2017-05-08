Namespace DataObjects

    Public Class Subscriber

#Region "properties / enums"
        '******************   VARIABLE FROM SQL ******************
        'AS SubcriberType
        'AS NativeID
        'AS Name
        'AS Email
        'AS Mobile

        Public Enum SubscriberType_Enum
            Driver = 0
            Contact = 1
            Group = 2
            User = 3
        End Enum

        Public Property SubscriberType_Str As String
        Public Property SubscriberType As SubscriberType_Enum
        Public Property NativeID As Guid
        Public Property Email As String
        Public Property Mobile As String
        Public Property SendEmail As Boolean = False
        Public Property SendText As Boolean = False
        Public Property Name As String

        Public ReadOnly Property NameFormatted As String
            Get

                Return String.Format("{0}: {1}", SubscriberType_Str, Name)

            End Get
        End Property

        
        'Public ReadOnly Property AllEmails_formatted As String
        '    Get

        '        If SubscriberType = SubscriberType_Enum.Group Then

        '            DataObjects.Group.GetMembers(NativeID)
        '        Else
        '            Return Me.Email
        '        End If

        '    End Get
        'End Property


#End Region

#Region "construcors"

        Public Sub New()

        End Sub


        Public Sub New(x As usp_GetSubscribersForApplicationResult)

            With Me
                .SubscriberType_Str = x.SubcriberType
                .Email = x.Email
                .NativeID = x.NativeID
                .Mobile = x.Mobile
                .Name = x.Name
                .SubscriberType = x.SubcriberType_ID

            End With

        End Sub

#End Region

#Region "CRUD"

        ''' <summary>
        ''' left empty but required for the object data source in group.aspx
        ''' </summary>
        ''' <param name="x"></param>
        ''' <remarks></remarks>
        Public Shared Sub update(x As DataObjects.Subscriber)



        End Sub

#End Region


#Region "gets and sets"

        Public Shared Sub ChangeSettingForGroup(groupID As Guid, nativeid As Guid, sendEmail As Boolean, sendText As Boolean)

            Dim o As List(Of FMS.Business.GroupSubscriber) = _
                            (From x In SingletonAccess.FMSDataContextContignous.GroupSubscribers _
                                Where x.GroupID = groupID AndAlso x.NativeID = nativeid).ToList

            For Each obj As Business.GroupSubscriber In o

                obj.SendEmail = sendEmail
                obj.SendText = sendText
            Next

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Function GetAllforApplication(appid As Guid) As List(Of DataObjects.Subscriber)

            Return (From x In SingletonAccess.FMSDataContextNew.usp_GetSubscribersForApplication(appid) _
                     Select New Subscriber(x)).ToList

        End Function
        Public Shared Function GetAllforApplicationDestination(appid As Guid) As List(Of DataObjects.Subscriber)

            Dim ObjList As New List(Of DataObjects.Subscriber)
            Dim resultString = (From x In SingletonAccess.FMSDataContextNew.usp_GetSubscribersForApplication(appid) _
                       Select New Subscriber(x)).ToList


            ObjList.Add(New Subscriber() With
                                 {.Name = "Select All",
                                  .NativeID = New Guid()})
            For Each listItem In resultString 

                ObjList.Add(New Subscriber() With
                                  {.Name = listItem.NameFormatted,
                                   .NativeID = listItem.NativeID})
            Next

            Return ObjList

        End Function


        Public Shared Function GetAllForGroup(groupid As Guid) As List(Of DataObjects.Subscriber)

            Dim grp As DataObjects.Group = DataObjects.Group.GetFromGroupID(groupid)

            Dim allSubscribers As List(Of DataObjects.Subscriber) = GetAllforApplication(grp.ApplicationID)

            Dim grpMembers As List(Of FMS.Business.GroupSubscriber) = _
                                (From x In SingletonAccess.FMSDataContextNew.GroupSubscribers _
                                 Where x.GroupID = groupid).ToList

            Dim retlst As New List(Of DataObjects.Subscriber)


            For Each m As Business.GroupSubscriber In grpMembers

                Dim foundSubscriber As DataObjects.Subscriber

                foundSubscriber = allSubscribers.Where(Function(x) x.NativeID = m.NativeID).Single

                foundSubscriber.SendEmail = m.SendEmail
                foundSubscriber.SendText = m.SendText

                retlst.Add(foundSubscriber)

            Next

            Return retlst

        End Function


#End Region

    End Class

End Namespace
