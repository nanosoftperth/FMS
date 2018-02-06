Namespace DataObjects
    Public Class tblParameters

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property ParId As String
        Public Property Field1 As String
        Public Property Field2 As String
        Public Property Field3 As String
        Public Property Field4 As String
        Public Property Field5 As String
        
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.tblParameter)
            With objTbl
                Me.ParId = .ParId
                Me.Field1 = .Field1
                Me.Field2 = .Field2
                Me.Field3 = .Field3
                Me.Field4 = .Field4
                Me.Field5 = .Field5
                Me.ApplicationId = .ApplicationId
            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(param As DataObjects.tblParameters)
            Try
                Dim AppID = ThisSession.ApplicationID

                With New LINQtoSQLClassesDataContext
                    Dim obj As New FMS.Business.tblParameter

                    With obj
                        .ParameterID = Guid.NewGuid
                        .ParId = param.ParId
                        .Field1 = param.Field1
                        .ApplicationId = AppID

                    End With

                    .tblParameters.InsertOnSubmit(obj)
                    .SubmitChanges()
                    .Dispose()
                End With


            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Shared Sub Update(param As DataObjects.tblParameters)
            Try

                With New LINQtoSQLClassesDataContext

                    Dim AppID = ThisSession.ApplicationID
                    Dim obj As FMS.Business.tblParameter = (From p In .tblParameters
                                                            Where p.ParId.Equals(param.ParId) And p.ApplicationId = AppID).Single
                    With obj
                        .ParId = param.ParId
                        .Field1 = param.Field1
                        .Field2 = param.Field2
                        .Field3 = param.Field3
                        .Field4 = param.Field4
                        .ApplicationId = AppID

                    End With

                    .SubmitChanges()
                    .Dispose()

                End With

            Catch ex As Exception
                Throw ex
            End Try

        End Sub

        Public Shared Sub Delete(param As DataObjects.tblParameters)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID

                Dim obj As FMS.Business.tblParameter = (From p In .tblParameters
                                                        Where p.ParId.Equals(param.ParId) And p.ApplicationId = appID).SingleOrDefault
                .tblParameters.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblParameters)
            Try
                Dim AppID = ThisSession.ApplicationID
                Dim objParam As New List(Of DataObjects.tblParameters)

                With New LINQtoSQLClassesDataContext
                    objParam = (From p In .tblParameters
                                Where p.ApplicationId = AppID
                                Select New DataObjects.tblParameters(p)).ToList()
                    .Dispose()
                End With
                Return objParam
            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region


    End Class

End Namespace
