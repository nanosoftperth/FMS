Imports System.Data.SqlClient
Imports System
Imports System.Configuration

Namespace DataObjects
    Public Class CUST

#Region "Properties / enums"
        Public Property ID As System.Nullable(Of Integer)
        Public Property CardID As String
        Public Property CustomerName As String

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objCust As FMS.Business.CUST)
            With objCust
                Me.ID = .ID
                Me.CardID = .CardID
                Me.CustomerName = .CustomerName

            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(cust As DataObjects.CUST)
            Dim oCust As New FMS.Business.CUST
            With oCust
                '.ID = cust.ID
                .CardID = cust.CardID
                .CustomerName = cust.CustomerName
            End With
            SingletonAccess.FMSDataContextContignous.CUSTs.InsertOnSubmit(oCust)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(cust As DataObjects.CUST)
            Dim oCust As FMS.Business.CUST = (From c In SingletonAccess.FMSDataContextContignous.CUSTs
                                              Where c.ID.Equals(cust.ID)).SingleOrDefault
            With oCust
                '.ID = cust.ID
                .CardID = cust.CardID
                .CustomerName = cust.CustomerName
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(cust As DataObjects.CUST)
            Dim id As Integer = cust.ID
            Dim oCust As FMS.Business.CUST = (From c In SingletonAccess.FMSDataContextContignous.CUSTs
                                              Where c.ID = id).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.CUSTs.DeleteOnSubmit(oCust)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Extended CRUD"

        Public Shared Sub DeleteAll()
            Dim strConnection As String = System.Configuration.ConfigurationManager.ConnectionStrings("ApplicationServices").ConnectionString
            Dim connection As SqlConnection
            Dim adapter As SqlDataAdapter = New SqlDataAdapter()
            Dim sql As String = Nothing
            connection = New SqlConnection(strConnection)
            sql = "delete from CUST"

            connection.Open()
            adapter.DeleteCommand = connection.CreateCommand()
            adapter.DeleteCommand.CommandText = sql
            adapter.DeleteCommand.ExecuteNonQuery()

        End Sub

#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.CUST)
            Try
                Dim obj As New List(Of DataObjects.CUST)

                With New LINQtoSQLClassesDataContext
                    obj = (From d In .CUSTs
                           Order By d.CustomerName
                           Select New DataObjects.CUST(d)).ToList

                    .Dispose()

                End With

                Return obj

            Catch ex As Exception
                Throw ex
            End Try

            'Dim oCust = (From c In SingletonAccess.FMSDataContextContignous.CUSTs
            '             Order By c.CustomerName
            '             Select New DataObjects.CUST(c)).ToList
            'Return oCust
        End Function

        Public Shared Function GetAllExistInTableCustomer() As List(Of DataObjects.tblCustomers)
            Dim oCust = (From c In SingletonAccess.FMSDataContextContignous.tblCustomers
                         Where (From cs In SingletonAccess.FMSDataContextContignous.CUSTs
                                Select cs.CardID).Contains(c.MYOBCustomerNumber)
                         Select New DataObjects.tblCustomers(c)).ToList()

            'Dim oCust = (From c In SingletonAccess.FMSDataContextContignous.CUSTs
            '                Where (From cs In SingletonAccess.FMSDataContextContignous.tblCustomers
            '                   Select cs.MYOBCustomerNumber).Contains(c.CardID)
            '                Select New DataObjects.CUST(c)).ToList()

            Return oCust
        End Function

        Public Shared Function UpdateCustomerBasedOnCardID(CardID As String) As Boolean
            Try

                SingletonAccess.FMSDataContextContignous.usp_UpdateCustomersBaseOnCardID(ThisSession.ApplicationID, CardID)

                Return True

            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region

    End Class

End Namespace
