Namespace DataObjects
    Public Class usp_GetSalesReportSuburb

#Region "Properties / enums"
        Public Property Customer As System.Nullable(Of Integer)
        Public Property Cid As System.Nullable(Of Integer)
        Public Property SiteName As String
        Public Property ServiceDescription As String
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ServicePrice As System.Nullable(Of Double)
        Public Property PerAnnumCharge As System.Nullable(Of Double)
        Public Property UnitsHaveMoreThanOneRun As System.Nullable(Of Boolean)

        
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetSalesReportSuburbResult)
            With obj
                Me.Customer = .Customer
                Me.Cid = .Cid
                Me.SiteName = .SiteName
                Me.ServiceDescription = .ServiceDescription
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun
                
            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllSalesReportSuburb() As List(Of DataObjects.usp_GetSalesReportSuburb)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSalesReportSuburb()
                            Select New DataObjects.usp_GetSalesReportSuburb(s)).ToList
            Return objList
        End Function
        Public Shared Function GetAllSalesReportSuburbPerCID(cid As Integer) As List(Of DataObjects.usp_GetSalesReportSuburb)
            SingletonAccess.FMSDataContextContignous.CommandTimeout = 180
            Dim objList = (From s In SingletonAccess.FMSDataContextContignous.usp_GetSalesReportSuburb()
                           Where s.Cid = cid
                            Select New DataObjects.usp_GetSalesReportSuburb(s)).ToList
            Return objList
        End Function


#End Region

    End Class
End Namespace

