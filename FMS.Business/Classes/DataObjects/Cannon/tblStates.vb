Namespace DataObjects
    Public Class tblStates
#Region "Properties / enums"
        Public Property StateID As System.Guid
        Public Property Sid As Integer
        Public Property StateCode As String
        Public Property StateDesc As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(State As DataObjects.tblStates)
            Dim objState As New FMS.Business.tblState
            With objState
                .Sid = GetLastIDUsed() + 1
                .StateCode = State.StateCode
                .StateDesc = State.StateDesc
            End With
            SingletonAccess.FMSDataContextContignous.tblStates.InsertOnSubmit(objState)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(State As DataObjects.tblStates)
            Dim objState As FMS.Business.tblState = (From c In SingletonAccess.FMSDataContextContignous.tblStates
                                                           Where c.Sid.Equals(State.Sid)).SingleOrDefault
            With objState
                .StateCode = State.StateCode
                .StateDesc = State.StateDesc
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(State As DataObjects.tblStates)
            Dim objState As FMS.Business.tblState = (From c In SingletonAccess.FMSDataContextContignous.tblStates
                                                         Where c.Sid.Equals(State.Sid)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblStates.DeleteOnSubmit(objState)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Private Shared Function GetLastIDUsed() As Integer
            Dim objState = (From c In SingletonAccess.FMSDataContextContignous.tblStates
                               Order By c.Sid Descending
                               Select New DataObjects.tblStates(c)).First()
            Return objState.Sid
        End Function
        Public Shared Function GetAll() As List(Of DataObjects.tblStates)
            Dim objStates = (From c In SingletonAccess.FMSDataContextContignous.tblStates
                                            Order By c.Sid
                                          Select New DataObjects.tblStates(c)).ToList
            Return objStates
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblStates As FMS.Business.tblState)
            With objTblStates
                Me.StateID = .StateID
                Me.Sid = .Sid
                Me.StateCode = .StateCode
                Me.StateDesc = .StateDesc
            End With
        End Sub
#End Region
    End Class
End Namespace

