Namespace DataObjects
    Public Class tblCIRReason
#Region "Properties / enums"
        Public Property ReasonID As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property CId As Integer
        Public Property CIRReason As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(Reason As DataObjects.tblCIRReason)
            With New LINQtoSQLClassesDataContext
                Dim objCIRReason As New FMS.Business.tblCIRReason
                Dim appId = ThisSession.ApplicationID
                With objCIRReason
                    .ReasonID = Guid.NewGuid
                    .ApplicationID = appId
                    .CId = tblProjectID.CIRReasonIDCreateOrUpdate(appId)
                    .CIRReason = Reason.CIRReason
                End With
                .tblCIRReasons.InsertOnSubmit(objCIRReason)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(Reason As DataObjects.tblCIRReason)
            With New LINQtoSQLClassesDataContext
                Dim objCIRReason As FMS.Business.tblCIRReason = (From c In .tblCIRReasons
                                                                 Where c.ReasonID.Equals(Reason.ReasonID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objCIRReason
                    .CIRReason = Reason.CIRReason
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(Reason As DataObjects.tblCIRReason)
            With New LINQtoSQLClassesDataContext
                Dim objCIRReason As FMS.Business.tblCIRReason = (From c In .tblCIRReasons
                                                                 Where c.ReasonID.Equals(Reason.ReasonID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblCIRReasons.DeleteOnSubmit(objCIRReason)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCIRReason)
            Try
                Dim objCIRReason As New List(Of DataObjects.tblCIRReason)
                With New LINQtoSQLClassesDataContext
                    objCIRReason = (From c In .tblCIRReasons
                                    Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                    Order By c.CIRReason
                                    Select New DataObjects.tblCIRReason(c)).ToList
                    .Dispose()
                End With
                Return objCIRReason
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objCIRReaason As FMS.Business.tblCIRReason)
            With objCIRReaason
                Me.ReasonID = .ReasonID
                Me.ApplicationID = .ApplicationID
                Me.CId = .CId
                Me.CIRReason = .CIRReason
            End With
        End Sub
#End Region
    End Class
End Namespace

