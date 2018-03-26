Imports System.Data.SqlClient
Imports System
Imports System.Configuration

Namespace DataObjects
    Public Class tblMYOBMatch
#Region "Properties / enums"
        Public Property MatchID As Guid
        Public Property Aid As Integer
        Public Property MYOBId As String
        Public Property CustomerName As String
        Public Property ImportedCustomerName As String

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objMTYOB As FMS.Business.tblMYOBMatch)
            With objMTYOB
                Me.MatchID = .MatchID
                Me.Aid = .Aid
                Me.MYOBId = .MYOBId
                Me.CustomerName = .CustomerName
                Me.ImportedCustomerName = .ImportedCustomerName

            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(myob As DataObjects.tblMYOBMatch)
            Try
                With New LINQtoSQLClassesDataContext
                    Dim oMYOB As New FMS.Business.tblMYOBMatch

                    With oMYOB
                        .MatchID = Guid.NewGuid()
                        .MYOBId = myob.MYOBId
                        .CustomerName = myob.CustomerName
                        .ImportedCustomerName = myob.ImportedCustomerName
                    End With

                    .tblMYOBMatches.InsertOnSubmit(oMYOB)
                    .SubmitChanges()
                    .Dispose()
                End With

            Catch ex As Exception
                Throw ex
            End Try

            'SingletonAccess.FMSDataContextContignous.tblMYOBMatches.InsertOnSubmit(oMYOB)
            'SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(myob As DataObjects.tblMYOBMatch)

            With New LINQtoSQLClassesDataContext
                Dim oMYOB As FMS.Business.tblMYOBMatch = (From m In .tblMYOBMatches
                                                          Where m.Aid.Equals(myob.Aid)).SingleOrDefault
                With oMYOB
                    .MYOBId = myob.MYOBId
                    .CustomerName = myob.CustomerName
                    .ImportedCustomerName = myob.ImportedCustomerName
                End With

                .SubmitChanges()
                .Dispose()

            End With

            'Dim oMYOB As FMS.Business.tblMYOBMatch = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches
            '                                          Where m.Aid.Equals(myob.Aid)).SingleOrDefault
            'With oMYOB
            '    .MYOBId = myob.MYOBId
            '    .CustomerName = myob.CustomerName
            '    .ImportedCustomerName = myob.ImportedCustomerName
            'End With
            'SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(myob As DataObjects.tblMYOBMatch)
            Dim id As Integer = myob.Aid

            With New LINQtoSQLClassesDataContext
                Dim oMYOB As FMS.Business.tblMYOBMatch = (From m In .tblMYOBMatches
                                                          Where m.Aid = id).SingleOrDefault
                .tblMYOBMatches.DeleteOnSubmit(oMYOB)
                .SubmitChanges()
                .Dispose()
            End With

            'Dim id As Integer = myob.Aid
            'Dim oMYOB As FMS.Business.tblMYOBMatch = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches
            '                                          Where m.Aid = id).SingleOrDefault
            'SingletonAccess.FMSDataContextContignous.tblMYOBMatches.DeleteOnSubmit(oMYOB)
            'SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Extended CRUD"
        Public Shared Sub DeleteAll()
            '--- get existing connections string properties
            'For Each constr As System.Configuration.ConnectionStringSettings In System.Configuration.ConfigurationManager.ConnectionStrings
            '    Dim name = constr.Name
            '    Dim connString = constr.ConnectionString
            '    Dim provider = constr.ProviderName

            'Next

            '--- Use this technique to faster delete the records from tblMYOBMatch
            Dim strConnection As String = System.Configuration.ConfigurationManager.ConnectionStrings("ApplicationServices").ConnectionString
            Dim connection As SqlConnection
            Dim adapter As SqlDataAdapter = New SqlDataAdapter()
            Dim sql As String = Nothing
            connection = New SqlConnection(strConnection)
            sql = "delete from tblMYOBMatch"

            connection.Open()
            adapter.DeleteCommand = connection.CreateCommand()
            adapter.DeleteCommand.CommandText = sql
            adapter.DeleteCommand.ExecuteNonQuery()

            '--- No errors and working but so slow
            'Dim listMYOB As IEnumerable(Of FMS.Business.tblMYOBMatch) = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches
            '                                             Select m).ToList()
            'For Each rMYOB In listMYOB
            '    SingletonAccess.FMSDataContextContignous.tblMYOBMatches.DeleteOnSubmit(rMYOB)
            'Next

            'For Each rMYOB In listMYOB
            '    Dim oMYOB As New FMS.Business.tblMYOBMatch
            '    With oMYOB
            '        .MatchID = Guid.NewGuid()
            '        .MYOBId = rMYOB.MYOBId
            '        .CustomerName = rMYOB.CustomerName
            '        .ImportedCustomerName = rMYOB.ImportedCustomerName
            '    End With

            '    SingletonAccess.FMSDataContextContignous.tblMYOBMatches.DeleteOnSubmit(oMYOB)

            'Next
            'SingletonAccess.FMSDataContextContignous.SubmitChanges()

            '--- No error(s) but not deleteing
            'Dim oMYOB As IEnumerable(Of FMS.Business.tblMYOBMatch) = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches
            '                                                          Select m).ToList()

            'SingletonAccess.FMSDataContextContignous.tblMYOBMatches.DeleteAllOnSubmit(oMYOB)
            'SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

        Public Shared Sub CreateAll(myob As List(Of DataObjects.tblMYOBMatch))
            Try
                Dim obj As New List(Of DataObjects.tblMYOBMatch)

                With New LINQtoSQLClassesDataContext

                    For Each rMYOB In myob
                        Dim oMYOB As New FMS.Business.tblMYOBMatch

                        With oMYOB
                            .MatchID = Guid.NewGuid()
                            .MYOBId = rMYOB.MYOBId
                            .CustomerName = rMYOB.CustomerName
                            .ImportedCustomerName = rMYOB.ImportedCustomerName
                        End With

                        .tblMYOBMatches.InsertOnSubmit(oMYOB)
                    Next

                    .SubmitChanges()

                    .Dispose()

                End With

            Catch ex As Exception
                Throw ex
            End Try

            'For Each rMYOB In myob
            '    Dim oMYOB As New FMS.Business.tblMYOBMatch
            '    With oMYOB
            '        .MatchID = Guid.NewGuid()
            '        .MYOBId = rMYOB.MYOBId
            '        .CustomerName = rMYOB.CustomerName
            '        .ImportedCustomerName = rMYOB.ImportedCustomerName
            '    End With
            '    SingletonAccess.FMSDataContextContignous.tblMYOBMatches.InsertOnSubmit(oMYOB)
            'Next

            'SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblMYOBMatch)
            Try
                Dim obj As New List(Of DataObjects.tblMYOBMatch)

                With New LINQtoSQLClassesDataContext
                    obj = (From m In .tblMYOBMatches
                           Order By m.CustomerName
                           Select New DataObjects.tblMYOBMatch(m)).ToList
                    .Dispose()
                End With

                Return obj

            Catch ex As Exception
                Throw ex
            End Try


            'Dim oMYOB = (From m In SingletonAccess.FMSDataContextContignous.tblMYOBMatches
            '             Order By m.CustomerName
            '             Select New DataObjects.tblMYOBMatch(m)).ToList
            'Return oMYOB
        End Function

#End Region

    End Class

End Namespace


