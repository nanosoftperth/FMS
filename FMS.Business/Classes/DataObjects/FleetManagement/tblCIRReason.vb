﻿Namespace DataObjects
    Public Class tblCIRReason
#Region "Properties / enums"
        Public Property ReasonID As System.Guid
        Public Property CId As Integer
        Public Property CIRReason As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(Reason As DataObjects.tblCIRReason)
            Dim objCIRReason As New FMS.Business.tblCIRReason
            With objCIRReason
                .ReasonID = Guid.NewGuid
                .CId = tblProjectID.CIRReasonIDCreateOrUpdate()
                .CIRReason = Reason.CIRReason
            End With
            SingletonAccess.FMSDataContextContignous.tblCIRReasons.InsertOnSubmit(objCIRReason)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Reason As DataObjects.tblCIRReason)
            Dim objCIRReason As FMS.Business.tblCIRReason = (From c In SingletonAccess.FMSDataContextContignous.tblCIRReasons
                                                           Where c.ReasonID.Equals(Reason.ReasonID)).SingleOrDefault
            With objCIRReason
                .CIRReason = Reason.CIRReason
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Reason As DataObjects.tblCIRReason)
            Dim objCIRReason As FMS.Business.tblCIRReason = (From c In SingletonAccess.FMSDataContextContignous.tblCIRReasons
                                                         Where c.ReasonID.Equals(Reason.ReasonID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblCIRReasons.DeleteOnSubmit(objCIRReason)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCIRReason)
            Dim objCIRReason = (From c In SingletonAccess.FMSDataContextContignous.tblCIRReasons
                            Order By c.CIRReason
                            Select New DataObjects.tblCIRReason(c)).ToList
            Return objCIRReason
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objCIRReaason As FMS.Business.tblCIRReason)
            With objCIRReaason
                Me.ReasonID = .ReasonID
                Me.CId = .CId
                Me.CIRReason = .CIRReason
            End With
        End Sub
#End Region
    End Class
End Namespace
