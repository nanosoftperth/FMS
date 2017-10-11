﻿Namespace DataObjects
    Public Class usp_GetCustomerUpdateValue
#Region "Properties / enums"
        Public Property CustomerName As String
        Public Property TotalAmount As System.Nullable(Of Double)
        Public Property Cid As System.Nullable(Of Integer)
#End Region
#Region "Get methods"
        Public Shared Function GetCustomerUpdateValue(cid As Integer) As DataObjects.usp_GetCustomerUpdateValue
            Dim objGetCustomerUpdateValue = (From c In SingletonAccess.FMSDataContextContignous.usp_GetCustomerUpdateValue(cid)
                                             Select New DataObjects.usp_GetCustomerUpdateValue(c)).SingleOrDefault
            Return objGetCustomerUpdateValue
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGetCustomerUpdateValue As FMS.Business.usp_GetCustomerUpdateValueResult)
            With objGetCustomerUpdateValue
                Me.Cid = .Cid
                Me.CustomerName = .CustomerName
                Me.TotalAmount = .TotalAmount
            End With
        End Sub
#End Region
    End Class
End Namespace

