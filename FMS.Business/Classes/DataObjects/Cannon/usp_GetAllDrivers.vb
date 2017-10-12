Namespace DataObjects
    Public Class usp_GetAllDrivers
#Region "Properties / enums"
        Public Property DriverID As System.Guid
        Public Property DriverName As String
        Public Property InActive As Integer
        Public Property Source As String
#End Region
#Region "Get methods"

        Public Shared Function GetAllDrivers() As List(Of DataObjects.usp_GetAllDrivers)
            Dim lstGetAllDrivers = (From d In SingletonAccess.FMSDataContextContignous.usp_GetAllDrivers
                                    Order By d.DriverName
                                 Select New DataObjects.usp_GetAllDrivers(d)).ToList()
            Return lstGetAllDrivers
        End Function

#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objGetAllDrivers As FMS.Business.usp_GetAllDriversResult)
            With objGetAllDrivers
                Me.DriverID = .DriverID
                Me.DriverName = .DriverName
                Me.InActive = .InActive
                Me.Source = .Source
            End With
        End Sub
#End Region
    End Class
End Namespace

