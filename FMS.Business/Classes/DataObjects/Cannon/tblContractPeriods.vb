﻿Namespace DataObjects
    Public Class tblContractPeriods
#Region "Properties / enums"
        Public Property ContractPeriodID As System.Guid
        Public Property Aid As Integer
        Public Property ContractPeriodDesc As String
        Public Property ContractPeriodMonths As System.Nullable(Of Single)
        Public Property NumberOfYears As System.Nullable(Of Short)
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblContractPeriods)
            Dim objContractPeriod = (From c In SingletonAccess.FMSDataContextContignous.tblContractPeriods
                            Order By c.ContractPeriodDesc
                            Select New DataObjects.tblContractPeriods(c)).ToList
            Return objContractPeriod
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objContractPeriod As FMS.Business.tblContractPeriod)
            With objContractPeriod
                Me.ContractPeriodID = .ContractPeriodID
                Me.Aid = .Aid
                Me.ContractPeriodDesc = .ContractPeriodDesc
                Me.ContractPeriodMonths = .ContractPeriodMonths
                Me.NumberOfYears = .NumberOfYears
            End With
        End Sub
#End Region
    End Class
End Namespace
