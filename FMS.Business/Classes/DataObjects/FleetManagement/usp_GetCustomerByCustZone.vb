Namespace DataObjects
    Public Class usp_GetCustomerByCustZone
#Region "Properties / enums"
        Public Property ZoneDescription As String
        Public Property Cid As Integer
        Public Property CustomerName As String
        Public Property AddressLine1 As String
        Public Property AddressLine2 As String
        Public Property StateCode As String
        Public Property Suburb As String
        Public Property PostCode As String
        Public Property CustomerContactName As String
        Public Property CustomerPhone As String
#End Region
#Region "Get methods"
        Public Shared Function GetCustByCustZone() As List(Of DataObjects.usp_GetCustomerByCustZone)
            Dim objCustByCustZone = (From c In SingletonAccess.FMSDataContextContignous.usp_GetCustomerByCustZone(ThisSession.ApplicationID)
                                     Select New DataObjects.usp_GetCustomerByCustZone(c)).ToList
            Return objCustByCustZone
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objCustByCust As FMS.Business.usp_GetCustomerByCustZoneResult)
            With objCustByCust
                Me.ZoneDescription = .ZoneDescription
                Me.Cid = .Cid
                Me.CustomerName = .CustomerName
                Me.AddressLine1 = .AddressLine1
                Me.AddressLine2 = .AddressLine2
                Me.StateCode = .StateCode
                Me.Suburb = .Suburb
                Me.PostCode = .PostCode
                Me.CustomerContactName = .CustomerContactName
                Me.CustomerPhone = .CustomerPhone
            End With
        End Sub
#End Region
    End Class
End Namespace


