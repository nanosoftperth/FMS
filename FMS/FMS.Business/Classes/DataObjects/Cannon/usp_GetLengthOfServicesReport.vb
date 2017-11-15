﻿Namespace DataObjects
    Public Class usp_GetLengthOfServicesReport
#Region "Properties / enums"
        Public Property Years As System.Nullable(Of Integer)
        Public Property sitestartdate As System.Nullable(Of Date)
        Public Property SiteName As String
#End Region
#Region "Get methods"
        Public Shared Function GetLengthOfService(GTYears As Integer) As List(Of DataObjects.usp_GetLengthOfServicesReport)
            Dim objLengthOfService = (From c In SingletonAccess.FMSDataContextContignous.usp_GetLengthOfServicesReport(GTYears)
                            Select New DataObjects.usp_GetLengthOfServicesReport(c)).ToList
            Return objLengthOfService
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objLengthOfService As FMS.Business.usp_GetLengthOfServicesReportResult)
            With objLengthOfService
                Me.Years = .Years
                Me.sitestartdate = .sitestartdate
                Me.SiteName = .SiteName
            End With
        End Sub
#End Region
    End Class
End Namespace