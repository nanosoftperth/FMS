Namespace DataObjects
    Public Class usp_GetRevenueReportByZone

#Region "Properties / enums"
        Public Property AreaDescription As String
        Public Property CustomerName As String
        Public Property CustomerContactName As String
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property AddLine2 As String
        Public Property SuburbLine As String
        Public Property CustomerPhone As String
        Public Property Cid As System.Nullable(Of Integer)
        Public Property Zone As System.Nullable(Of Integer)
        Public Property Suburb As String
        Public Property Sales As List(Of FMS.Business.DataObjects.usp_GetSalesReportSuburb)

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetRevenueReportByZoneResult)
            With obj
                Me.AreaDescription = .AreaDescription
                Me.CustomerName = .CustomerName
                Me.CustomerContactName = .CustomerContactName
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.AddLine2 = .AddLine2
                Me.SuburbLine = .SuburbLine
                Me.CustomerPhone = .CustomerPhone
                Me.Cid = .Cid
                Me.Zone = .Zone
                Me.Suburb = .Suburb
            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll(ReportType As String, Optional zone As Integer = 0, Optional suburb As String = "") As List(Of DataObjects.usp_GetRevenueReportByZone)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objList As New List(Of DataObjects.usp_GetRevenueReportByZone)

            If ReportType = "Suburb" Then
                objList = (From r In SingletonAccess.FMSDataContextContignous.usp_GetRevenueReportByZone
                                    Where r.Suburb = suburb.TrimStart.TrimEnd
                                    Select New DataObjects.usp_GetRevenueReportByZone(r)).ToList
            Else
                objList = (From r In SingletonAccess.FMSDataContextContignous.usp_GetRevenueReportByZone
                                    Where r.Zone = zone
                                    Select New DataObjects.usp_GetRevenueReportByZone(r)).ToList

            End If


            For Each row In objList
                Dim sls = FMS.Business.DataObjects.usp_GetSalesReportSuburb.GetAllSalesReportSuburbPerCID(row.Cid)

                If sls.Count > 0 Then
                    row.Sales = sls
                End If

            Next
            'Dim objLengthOfService = (From c In SingletonAccess.FMSDataContextContignous.usp_GetRevenueReportByZone(report)
            '                Select New DataObjects.usp_GetRevenueReportByZone(c)).ToList
            Return objList
        End Function
#End Region

    End Class

End Namespace

