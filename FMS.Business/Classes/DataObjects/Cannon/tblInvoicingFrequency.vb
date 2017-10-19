﻿Namespace DataObjects
    Public Class tblInvoicingFrequency
#Region "Properties / enums"
        Public Property InvoiceFrequencyID As System.Guid
        Public Property IId As Integer
        Public Property InvoiceId As String
        Public Property Frequency As String
        Public Property NoOfWeeks As System.Nullable(Of Single)
#End Region
#Region "CRUD"
        Public Shared Sub Create(InvoicingFrequency As DataObjects.tblInvoicingFrequency)
            Dim objInvoicingFrequency As New FMS.Business.tblInvoicingFrequency
            With objInvoicingFrequency
                .InvoiceFrequencyID = Guid.NewGuid
                .InvoiceId = InvoicingFrequency.InvoiceId
                .Frequency = InvoicingFrequency.Frequency
                .NoOfWeeks = InvoicingFrequency.NoOfWeeks
            End With
            SingletonAccess.FMSDataContextContignous.tblInvoicingFrequencies.InsertOnSubmit(objInvoicingFrequency)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(InvoicingFrequency As DataObjects.tblInvoicingFrequency)
            Dim objInvoicingFrequency As FMS.Business.tblInvoicingFrequency = (From c In SingletonAccess.FMSDataContextContignous.tblInvoicingFrequencies
                                                           Where c.InvoiceFrequencyID.Equals(InvoicingFrequency.InvoiceFrequencyID)).SingleOrDefault
            With objInvoicingFrequency
                .InvoiceId = InvoicingFrequency.InvoiceId
                .Frequency = InvoicingFrequency.Frequency
                .NoOfWeeks = InvoicingFrequency.NoOfWeeks
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(InvoicingFrequency As DataObjects.tblInvoicingFrequency)
            Dim objInvoicingFrequency As FMS.Business.tblInvoicingFrequency = (From c In SingletonAccess.FMSDataContextContignous.tblInvoicingFrequencies
                                                         Where c.InvoiceFrequencyID.Equals(InvoicingFrequency.InvoiceFrequencyID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblInvoicingFrequencies.DeleteOnSubmit(objInvoicingFrequency)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblInvoicingFrequency)
            Dim objInvoicingFrequency = (From c In SingletonAccess.FMSDataContextContignous.tblInvoicingFrequencies
                            Order By c.IId
                                          Select New DataObjects.tblInvoicingFrequency(c)).ToList
            Return objInvoicingFrequency
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objInvoicingFrequency As FMS.Business.tblInvoicingFrequency)
            With objInvoicingFrequency
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
