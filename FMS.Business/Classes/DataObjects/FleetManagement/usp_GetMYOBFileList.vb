Namespace DataObjects
    Public Class usp_GetMYOBFileList

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property MYOBCustomerNumber As String
        Public Property CustomerName As String
        Public Property chkCustomerExcludeFuelLevy As Boolean
        Public Property Cid As System.Nullable(Of Integer)
        Public Property SiteName As String
        Public Property InvoiceCommencing As System.Nullable(Of Date)
        Public Property InvoiceFrequency As System.Nullable(Of Integer)
        Public Property InvoiceAddress As String
        Public Property InvoiceAddress2 As String
        Public Property PurchaseOrderNumber As String
        Public Property SiteCeaseDate As System.Nullable(Of Date)
        Public Property SeparateInvoice As System.Nullable(Of Boolean)
        Public Property InvoiceMonth1 As System.Nullable(Of Integer)
        Public Property InvoiceMonth2 As System.Nullable(Of Integer)
        Public Property InvoiceMonth3 As System.Nullable(Of Integer)
        Public Property InvoiceMonth4 As System.Nullable(Of Integer)
        Public Property chkSitesExcludeFuelLevy As System.Nullable(Of Boolean)
        Public Property ServiceUnits As System.Nullable(Of Double)
        Public Property ServicePrice As System.Nullable(Of Double)
        Public Property PerAnnumCharge As System.Nullable(Of Double)
        Public Property UnitsHaveMoreThanOneRun As System.Nullable(Of Boolean)
        Public Property CSid As System.Nullable(Of Integer)

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbl As FMS.Business.usp_GetMYOBFileListResult)

            With objTbl
                Me.ApplicationId = .ApplicationID
                Me.MYOBCustomerNumber = .MYOBCustomerNumber
                Me.CustomerName = .CustomerName
                Me.chkCustomerExcludeFuelLevy = .chkCustomerExcludeFuelLevy
                Me.Cid = .SiteId
                Me.SiteName = .SiteName
                Me.InvoiceCommencing = .InvoiceCommencing
                Me.InvoiceFrequency = .InvoiceFrequency
                Me.InvoiceAddress = .InvoiceAddress
                Me.InvoiceAddress2 = .InvoiceAddress2
                Me.PurchaseOrderNumber = .PurchaseOrderNumber
                Me.SiteCeaseDate = .SiteCeaseDate
                Me.SeparateInvoice = .SeparateInvoice
                Me.InvoiceMonth1 = .InvoiceMonth1
                Me.InvoiceMonth2 = .InvoiceMonth2
                Me.InvoiceMonth3 = .InvoiceMonth3
                Me.InvoiceMonth4 = .InvoiceMonth4
                Me.chkSitesExcludeFuelLevy = .chkSitesExcludeFuelLevy
                Me.ServiceUnits = .ServiceUnits
                Me.ServicePrice = .ServicePrice
                Me.PerAnnumCharge = .PerAnnumCharge
                Me.UnitsHaveMoreThanOneRun = .UnitsHaveMoreThanOneRun
                Me.CSid = .CSid

            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.usp_GetMYOBFileList)
            Try
                Dim appID = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.usp_GetMYOBFileList)

                With New LINQtoSQLClassesDataContext
                    obj = (From a In .usp_GetMYOBFileList()
                           Where a.ApplicationID = appID
                           Select New DataObjects.usp_GetMYOBFileList(a)).ToList
                    .Dispose()
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function

#End Region

    End Class

End Namespace

