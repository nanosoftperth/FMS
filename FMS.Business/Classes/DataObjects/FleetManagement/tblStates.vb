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
            With New LINQtoSQLClassesDataContext
                Dim objState As New FMS.Business.tblState
                With objState
                    .StateCode = State.StateCode
                    .StateDesc = State.StateDesc
                End With
                .tblStates.InsertOnSubmit(objState)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(State As DataObjects.tblStates)
            With New LINQtoSQLClassesDataContext
                Dim objState As FMS.Business.tblState = (From c In .tblStates
                                                         Where c.Sid.Equals(State.Sid)).SingleOrDefault
                With objState
                    .StateCode = State.StateCode
                    .StateDesc = State.StateDesc
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(State As DataObjects.tblStates)
            With New LINQtoSQLClassesDataContext
                Dim objState As FMS.Business.tblState = (From c In .tblStates
                                                         Where c.Sid.Equals(State.Sid)).SingleOrDefault
                .tblStates.DeleteOnSubmit(objState)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblStates)
            Try
                Dim objStates As New List(Of DataObjects.tblStates)
                With New LINQtoSQLClassesDataContext
                    objStates = (From c In .tblStates
                                 Order By c.Sid
                                 Select New DataObjects.tblStates(c)).ToList

                    .Dispose()
                End With
                Return objStates
            Catch ex As Exception
                Throw ex
            End Try
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

