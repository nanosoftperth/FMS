Namespace DataObjects
    Public Class usp_UpdateCustomeRating
#Region "Extended CRUD"
        Public Shared Sub UpdateCustomerRating()
            Try
                Dim appId = ThisSession.ApplicationID

                With New LINQtoSQLClassesDataContext
                    .usp_UpdateCustomeRating(appId)

                    .Dispose()
                End With

                'Return obj

            Catch ex As Exception
                Throw ex
            End Try

            'Dim appId = ThisSession.ApplicationID

            'SingletonAccess.FMSDataContextContignous.CommandTimeout = 1200
            'SingletonAccess.FMSDataContextContignous.usp_UpdateCustomeRating(appId)

        End Sub
#End Region
    End Class

End Namespace

