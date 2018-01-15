Namespace DataObjects
    Public Class usp_UpdateCustomeRating
#Region "Extended CRUD"
        Public Shared Sub UpdateCustomerRating()
            Dim appId = ThisSession.ApplicationID

            SingletonAccess.FMSDataContextContignous.CommandTimeout = 1200
            SingletonAccess.FMSDataContextContignous.usp_UpdateCustomeRating(appId)

        End Sub
#End Region
    End Class

End Namespace

