Namespace DataObjects
    Public Class tblRateIncrease
#Region "Properties / enums"
        Public Property RateIncreasesID As System.Guid
        Public Property Aid As Integer
        Public Property SiteName As String
        Public Property CustomerName As String
        Public Property CSid As System.Nullable(Of Integer)
        Public Property Units As System.Nullable(Of Short)
        Public Property OldServicePrice As System.Nullable(Of Single)
        Public Property NewServicePrice As System.Nullable(Of Single)
        Public Property OldPerAnnumCharge As System.Nullable(Of Single)
        Public Property NewPerAnnumCharge As System.Nullable(Of Single)
        Public Property CustomerID As System.Nullable(Of Integer)
        Public Property SiteID As System.Nullable(Of Integer)
        Public Property Invfreq As System.Nullable(Of Integer)
        Public Property InvStartDate As System.Nullable(Of Date)
        Public Property ApplicationID As System.Nullable(Of System.Guid)
#End Region
#Region "CRUD"
        Public Shared Sub Create(RateIncrease As DataObjects.tblRateIncrease)
            With New LINQtoSQLClassesDataContext
                Dim appID = ThisSession.ApplicationID
                Dim obj As New FMS.Business.tblRateIncrease
                With obj
                    .RateIncreasesID = Guid.NewGuid()
                    .Aid = 1
                    .SiteName = RateIncrease.SiteName
                    .CustomerName = RateIncrease.CustomerName
                    .CSid = RateIncrease.CSid
                    .Units = RateIncrease.Units
                    .OldServicePrice = RateIncrease.OldServicePrice
                    .NewServicePrice = RateIncrease.NewServicePrice
                    .OldPerAnnumCharge = RateIncrease.OldPerAnnumCharge
                    .NewPerAnnumCharge = RateIncrease.NewPerAnnumCharge
                    .CustomerID = RateIncrease.CustomerID
                    .SiteID = RateIncrease.SiteID
                    .Invfreq = RateIncrease.Invfreq
                    .InvStartDate = RateIncrease.InvStartDate
                    .ApplicationID = appID
                End With

                .tblRateIncreases.InsertOnSubmit(obj)
                .SubmitChanges()
                .Dispose()
            End With

        End Sub
        Public Shared Sub Update(RateIncrease As DataObjects.tblRateIncrease)
            With New LINQtoSQLClassesDataContext
                Dim obj As FMS.Business.tblRateIncrease = (From c In .tblRateIncreases
                                                           Where c.RateIncreasesID.Equals(RateIncrease.RateIncreasesID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                With obj
                    .SiteName = RateIncrease.SiteName
                    .CustomerName = RateIncrease.CustomerName
                    .CSid = RateIncrease.CSid
                    .Units = RateIncrease.Units
                    .OldServicePrice = RateIncrease.OldServicePrice
                    .NewServicePrice = RateIncrease.NewServicePrice
                    .OldPerAnnumCharge = RateIncrease.OldPerAnnumCharge
                    .NewPerAnnumCharge = RateIncrease.NewPerAnnumCharge
                    .CustomerID = RateIncrease.CustomerID
                    .SiteID = RateIncrease.SiteID
                    .Invfreq = RateIncrease.Invfreq
                    .InvStartDate = RateIncrease.InvStartDate
                End With
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub Delete(RateIncrease As DataObjects.tblRateIncrease)
            With New LINQtoSQLClassesDataContext
                Dim obj As FMS.Business.tblRateIncrease = (From c In .tblRateIncreases
                                                           Where c.RateIncreasesID.Equals(RateIncrease.RateIncreasesID) And c.ApplicationID.Equals(ThisSession.ApplicationID)).SingleOrDefault
                .tblRateIncreases.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
        Public Shared Sub DeleteAll()
            With New LINQtoSQLClassesDataContext
                Dim obj As List(Of FMS.Business.tblRateIncrease) = (From c In .tblRateIncreases
                                                                    Where c.ApplicationID.Equals(ThisSession.ApplicationID)).ToList()
                .tblRateIncreases.DeleteAllOnSubmit(obj)
                .SubmitChanges()
                .Dispose()
            End With
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblRateIncrease)
            Try
                Dim obj As New List(Of DataObjects.tblRateIncrease)

                With New LINQtoSQLClassesDataContext
                    obj = (From c In .tblRateIncreases
                           Where c.ApplicationID.Equals(ThisSession.ApplicationID)
                           Order By c.CustomerName
                           Select New DataObjects.tblRateIncrease(c)).ToList
                    .Dispose()
                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objRateIncrease As FMS.Business.tblRateIncrease)
            With objRateIncrease
                Me.RateIncreasesID = .RateIncreasesID
                Me.Aid = .Aid
                Me.SiteName = .SiteName
                Me.CustomerName = .CustomerName
                Me.CSid = .CSid
                Me.Units = .Units
                Me.OldServicePrice = .OldServicePrice
                Me.NewServicePrice = .NewServicePrice
                Me.OldPerAnnumCharge = .OldPerAnnumCharge
                Me.NewPerAnnumCharge = .NewPerAnnumCharge
                Me.CustomerID = .CustomerID
                Me.SiteID = .SiteID
                Me.Invfreq = .Invfreq
                Me.InvStartDate = .InvStartDate
                Me.ApplicationID = .ApplicationID
            End With
        End Sub
#End Region
    End Class
End Namespace

