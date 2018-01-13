Namespace DataObjects
    Public Class tblFuelLevy
#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property FuelLevyID As System.Guid
        Public Property Aid As Integer
        Public Property Code As String
        Public Property Description As String
        Public Property Percentage As System.Nullable(Of Single)
        Public Property MYOBInvoiceCode As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(FuelLevy As DataObjects.tblFuelLevy)
            Dim appID = ThisSession.ApplicationID

            Dim objFuelLevy As New FMS.Business.tblFuelLevy
            With objFuelLevy
                .ApplicationId = appID
                .FuelLevyID = Guid.NewGuid
                .Aid = tblProjectID.FuelLevyIDCreateOrUpdate(appID)
                .Code = FuelLevy.Code
                .Description = FuelLevy.Description
                .Percentage = FuelLevy.Percentage
                .MYOBInvoiceCode = FuelLevy.MYOBInvoiceCode
            End With
            SingletonAccess.FMSDataContextContignous.tblFuelLevies.InsertOnSubmit(objFuelLevy)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(FuelLevy As DataObjects.tblFuelLevy)
            Dim appID = ThisSession.ApplicationID

            Dim objFuelLevy As FMS.Business.tblFuelLevy = (From c In SingletonAccess.FMSDataContextContignous.tblFuelLevies
                                                           Where c.FuelLevyID.Equals(FuelLevy.FuelLevyID) And c.ApplicationId = appID).SingleOrDefault
            With objFuelLevy
                .Code = FuelLevy.Code
                .Description = FuelLevy.Description
                .Percentage = FuelLevy.Percentage
                .MYOBInvoiceCode = FuelLevy.MYOBInvoiceCode
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(FuelLevy As DataObjects.tblFuelLevy)
            Dim appID = ThisSession.ApplicationID

            Dim objFuelLevy As FMS.Business.tblFuelLevy = (From c In SingletonAccess.FMSDataContextContignous.tblFuelLevies
                                                           Where c.FuelLevyID.Equals(FuelLevy.FuelLevyID) And c.ApplicationId = appID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblFuelLevies.DeleteOnSubmit(objFuelLevy)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblFuelLevy)
            Dim appID = ThisSession.ApplicationID

            Dim objFuelLevy = (From c In SingletonAccess.FMSDataContextContignous.tblFuelLevies
                               Where c.ApplicationId = appID
                               Order By c.Description
                               Select New DataObjects.tblFuelLevy(c)).ToList
            Return objFuelLevy

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objFuellevy As FMS.Business.tblFuelLevy)
            With objFuellevy
                Me.ApplicationId = .ApplicationId
                Me.FuelLevyID = .FuelLevyID
                Me.Aid = .Aid
                Me.Code = .Code
                Me.Description = .Description
                Me.Percentage = .Percentage
                Me.MYOBInvoiceCode = .MYOBInvoiceCode
            End With
        End Sub
#End Region
    End Class
End Namespace

