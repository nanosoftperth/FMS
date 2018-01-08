Namespace DataObjects
    Public Class tblDriverCommentsReason

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property Aid As Integer
        Public Property CommentDescription As String

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.tblDriverCommentsReason)
            With objTbl
                Me.ApplicationId = .ApplicationId
                Me.Aid = .Aid
                Me.CommentDescription = .CommentDescription
            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(dvr As DataObjects.tblDriverCommentsReason)
            Dim appID = ThisSession.ApplicationID

            Dim obj As New FMS.Business.tblDriverCommentsReason
            With obj
                .ApplicationId = appID
                .CommentDescription = dvr.CommentDescription

            End With
            SingletonAccess.FMSDataContextContignous.tblDriverCommentsReasons.InsertOnSubmit(obj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(dvr As DataObjects.tblDriverCommentsReason)
            Dim appID = ThisSession.ApplicationID

            Dim obj As FMS.Business.tblDriverCommentsReason = (From d In SingletonAccess.FMSDataContextContignous.tblDriverCommentsReasons
                                                               Where d.Aid.Equals(dvr.Aid) And d.ApplicationId = appID).SingleOrDefault
            With obj
                .CommentDescription = dvr.CommentDescription
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(dvr As DataObjects.tblDriverCommentsReason)
            Dim appID = ThisSession.ApplicationID

            Dim obj As FMS.Business.tblDriverCommentsReason = (From d In SingletonAccess.FMSDataContextContignous.tblDriverCommentsReasons
                                                               Where d.Aid.Equals(dvr.Aid) And d.ApplicationId.Equals(appID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblDriverCommentsReasons.DeleteOnSubmit(obj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllPerApplication() As List(Of DataObjects.tblDriverCommentsReason)
            Dim appID = ThisSession.ApplicationID

            Dim getDrivers = (From d In SingletonAccess.FMSDataContextContignous.tblDriverCommentsReasons
                              Where d.ApplicationId = appID
                              Order By d.CommentDescription
                              Select New DataObjects.tblDriverCommentsReason(d)).ToList
            Return getDrivers
        End Function
#End Region

    End Class

End Namespace


