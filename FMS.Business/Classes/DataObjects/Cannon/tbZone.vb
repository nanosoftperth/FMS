Namespace DataObjects
    Public Class tbZone
#Region "Properties / enums"
        Public Property ZoneID As System.Guid
        Public Property Aid As Integer
        Public Property AreaDescription As String
        Public Property SortOrder As Integer
#End Region
#Region "CRUD"
        Public Shared Sub Create(Zone As DataObjects.tbZone)
            Dim objZone As New FMS.Business.tbZone
            With objZone
                .ZoneID = Guid.NewGuid
                .Aid = GetLastIDUsed() + 1
                .AreaDescription = Zone.AreaDescription
                .SortOrder = GetLastIDUsed() + 1
            End With
            SingletonAccess.FMSDataContextContignous.tbZones.InsertOnSubmit(objZone)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(Zone As DataObjects.tbZone)
            Dim objZone As FMS.Business.tbZone = (From c In SingletonAccess.FMSDataContextContignous.tbZones
                                                           Where c.ZoneID.Equals(Zone.ZoneID)).SingleOrDefault
            With objZone
                .AreaDescription = Zone.AreaDescription
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(Zone As DataObjects.tbZone)
            Dim objZone As FMS.Business.tbZone = (From c In SingletonAccess.FMSDataContextContignous.tbZones
                                                         Where c.ZoneID.Equals(Zone.ZoneID)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tbZones.DeleteOnSubmit(objZone)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Private Shared Function GetLastIDUsed() As Integer
            Dim objZone = (From c In SingletonAccess.FMSDataContextContignous.tbZones
                               Order By c.Aid Descending
                               Select New DataObjects.tbZone(c)).First()
            Return objZone.Aid
        End Function
        Public Shared Function GetAll() As List(Of DataObjects.tbZone)
            Dim objZones = (From c In SingletonAccess.FMSDataContextContignous.tbZones
                            Order By c.AreaDescription
                                          Select New DataObjects.tbZone(c)).ToList
            Return objZones
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTbZone As FMS.Business.tbZone)
            With objTbZone
                Me.ZoneID = .ZoneID
                Me.Aid = .Aid
                Me.AreaDescription = .AreaDescription
                Me.SortOrder = .SortOrder
            End With
        End Sub
#End Region
    End Class
End Namespace

