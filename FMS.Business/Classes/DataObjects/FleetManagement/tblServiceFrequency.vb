Namespace DataObjects
    Public Class tblServiceFrequency
#Region "Properties / enums"
        Public Property FrequencyID As System.Guid
        Public Property ApplicationID As System.Nullable(Of System.Guid)
        Public Property Fid As Integer
        Public Property FrequencyDescription As String
        Public Property Factor As System.Nullable(Of Single)
        Public Property Periodical As Boolean
        Public Property Notes As String
#End Region
#Region "CRUD"
        Public Shared Sub Create(ServiceFrequency As DataObjects.tblServiceFrequency)
            With New LINQtoSQLClassesDataContext
                Dim objServiceFrequency As New FMS.Business.tblServiceFrequency
                Dim appId = ThisSession.ApplicationID
                With objServiceFrequency
                    .FrequencyID = Guid.NewGuid
                    .ApplicationID = appId
                    .Fid = tblProjectID.FrequencyIDCreateOrUpdate(appId)
                    .FrequencyDescription = ServiceFrequency.FrequencyDescription
                    .Factor = ServiceFrequency.Factor
                    .Periodical = ServiceFrequency.Periodical
                    .Notes = ServiceFrequency.Notes
                End With
                .tblServiceFrequencies.InsertOnSubmit(objServiceFrequency)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Update(ServiceFrequency As DataObjects.tblServiceFrequency)
            With New LINQtoSQLClassesDataContext
                Dim objServiceFrequency As FMS.Business.tblServiceFrequency = (From c In .tblServiceFrequencies
                                                                               Where c.FrequencyID.Equals(ServiceFrequency.FrequencyID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With objServiceFrequency
                    .FrequencyDescription = ServiceFrequency.FrequencyDescription
                    .Factor = ServiceFrequency.Factor
                    .Periodical = ServiceFrequency.Periodical
                    .Notes = ServiceFrequency.Notes
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(ServiceFrequency As DataObjects.tblServiceFrequency)
            With New LINQtoSQLClassesDataContext
                Dim objServiceFrequency As FMS.Business.tblServiceFrequency = (From c In .tblServiceFrequencies
                                                                               Where c.FrequencyID.Equals(ServiceFrequency.FrequencyID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblServiceFrequencies.DeleteOnSubmit(objServiceFrequency)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblServiceFrequency)
            Try
                Dim objServiceFrequency As New List(Of DataObjects.tblServiceFrequency)
                With New LINQtoSQLClassesDataContext
                    objServiceFrequency = (From c In .tblServiceFrequencies
                                           Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                                           Order By c.FrequencyDescription
                                           Select New DataObjects.tblServiceFrequency(c)).ToList
                    .Dispose()
                End With
                Return objServiceFrequency
            Catch ex As Exception
                Throw ex
            End Try
        End Function
        Public Shared Function GetServiceFrequencyByFID(Fid As Integer) As List(Of DataObjects.tblServiceFrequency)
            Try
                Dim objServiceFrequency As New List(Of DataObjects.tblServiceFrequency)
                With New LINQtoSQLClassesDataContext
                    objServiceFrequency = (From c In .tblServiceFrequencies
                                           Where c.Fid.Equals(Fid) And c.ApplicationID.Equals(ThisSession.ApplicationID)
                                           Select New DataObjects.tblServiceFrequency(c)).ToList
                    .Dispose()
                End With
                Return objServiceFrequency
            Catch ex As Exception
                Throw ex
            End Try
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objServiceFrequency As FMS.Business.tblServiceFrequency)
            With objServiceFrequency
                Me.FrequencyID = .FrequencyID
                Me.ApplicationID = .ApplicationID
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

