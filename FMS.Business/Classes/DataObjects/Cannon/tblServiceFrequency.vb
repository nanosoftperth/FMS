Namespace DataObjects
    Public Class tblServiceFrequency
#Region "Properties / enums"
        Public Property FrequencyID As System.Guid
        Public Property Fid As Integer
        Public Property FrequencyDescription As String
        Public Property Factor As System.Nullable(Of Single)
        Public Property Periodical As Boolean
        Public Property Notes As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(ServiceFrequency As DataObjects.tblServiceFrequency)
            Dim objServiceFrequency As New FMS.Business.tblServiceFrequency
            With objServiceFrequency
                .FrequencyID = Guid.NewGuid
                .FrequencyDescription = ServiceFrequency.FrequencyDescription
                .Factor = ServiceFrequency.Factor
                .Periodical = ServiceFrequency.Periodical
                .Notes = ServiceFrequency.Notes
            End With
            SingletonAccess.FMSDataContextContignous.tblServiceFrequencies.InsertOnSubmit(objServiceFrequency)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(ServiceFrequency As DataObjects.tblServiceFrequency)
            Dim objServiceFrequency As FMS.Business.tblServiceFrequency = (From c In SingletonAccess.FMSDataContextContignous.tblServiceFrequencies
                                                           Where c.Fid.Equals(ServiceFrequency.Fid)).SingleOrDefault
            With objServiceFrequency
                .FrequencyDescription = ServiceFrequency.FrequencyDescription
                .Factor = ServiceFrequency.Factor
                .Periodical = ServiceFrequency.Periodical
                .Notes = ServiceFrequency.Notes
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(ServiceFrequency As DataObjects.tblServiceFrequency)
            Dim objServiceFrequency As FMS.Business.tblServiceFrequency = (From c In SingletonAccess.FMSDataContextContignous.tblServiceFrequencies
                                                         Where c.Fid.Equals(ServiceFrequency.Fid)).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblServiceFrequencies.DeleteOnSubmit(objServiceFrequency)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblServiceFrequency)
            Dim objServiceFrequency = (From c In SingletonAccess.FMSDataContextContignous.tblServiceFrequencies
                            Order By c.FrequencyDescription
                            Select New DataObjects.tblServiceFrequency(c)).ToList
            Return objServiceFrequency
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objServiceFrequency As FMS.Business.tblServiceFrequency)
            With objServiceFrequency
                Me.FrequencyID = .FrequencyID
                Me.Fid = .Fid
                Me.FrequencyDescription = .FrequencyDescription
                Me.Factor = .Factor
                Me.Periodical = .Periodical
                Me.Notes = .Notes
            End With
        End Sub
#End Region
    End Class
End Namespace

