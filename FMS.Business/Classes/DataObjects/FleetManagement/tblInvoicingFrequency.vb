Namespace DataObjects
    Public Class tblInvoicingFrequency
#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property InvoiceFrequencyID As System.Guid
        Public Property IId As Integer
        Public Property InvoiceId As String
        Public Property Frequency As String
        Public Property NoOfWeeks As System.Nullable(Of Single)
#End Region
#Region "CRUD"
        Public Shared Sub Create(InvoicingFrequency As DataObjects.tblInvoicingFrequency)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As New FMS.Business.tblInvoicingFrequency

                With obj
                    .ApplicationId = appID
                    .InvoiceFrequencyID = Guid.NewGuid
                    .IId = tblProjectID.InvoiceFrequencyIDCreateOrUpdate()
                    .InvoiceId = InvoicingFrequency.InvoiceId
                    .Frequency = InvoicingFrequency.Frequency
                    .NoOfWeeks = InvoicingFrequency.NoOfWeeks
                End With

                .tblInvoicingFrequencies.InsertOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Update(InvoicingFrequency As DataObjects.tblInvoicingFrequency)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblInvoicingFrequency = (From c In .tblInvoicingFrequencies
                                                                 Where c.InvoiceFrequencyID.Equals(InvoicingFrequency.InvoiceFrequencyID) And c.ApplicationId = appID).SingleOrDefault
                With obj
                    .InvoiceId = InvoicingFrequency.InvoiceId
                    .Frequency = InvoicingFrequency.Frequency
                    .NoOfWeeks = InvoicingFrequency.NoOfWeeks
                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(InvoicingFrequency As DataObjects.tblInvoicingFrequency)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblInvoicingFrequency = (From c In .tblInvoicingFrequencies
                                                                 Where c.InvoiceFrequencyID.Equals(InvoicingFrequency.InvoiceFrequencyID) And c.ApplicationId = appID).SingleOrDefault

                .tblInvoicingFrequencies.DeleteOnSubmit(obj)
                .SubmitChanges()
            End With



        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblInvoicingFrequency)
            Try
                Dim obj As New List(Of DataObjects.tblInvoicingFrequency)

                With New LINQtoSQLClassesDataContext
                    obj = (From c In .tblInvoicingFrequencies
                           Order By c.IId
                           Select New DataObjects.tblInvoicingFrequency(c)).ToList
                End With

                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Shared Function GetAllPerApplication() As List(Of DataObjects.tblInvoicingFrequency)
            Try
                Dim appID = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.tblInvoicingFrequency)

                With New LINQtoSQLClassesDataContext
                    obj = (From c In .tblInvoicingFrequencies
                           Where c.ApplicationId = appID
                           Order By c.IId
                           Select New DataObjects.tblInvoicingFrequency(c)).ToList
                End With

                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objInvoicingFrequency As FMS.Business.tblInvoicingFrequency)
            With objInvoicingFrequency
                Me.ApplicationId = .ApplicationId
                Me.InvoiceFrequencyID = .InvoiceFrequencyID
                Me.IId = .IId
                Me.InvoiceId = .InvoiceId
                Me.Frequency = .Frequency
                Me.NoOfWeeks = .NoOfWeeks
            End With
        End Sub
#End Region
    End Class
End Namespace
