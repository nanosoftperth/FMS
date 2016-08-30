
Namespace DataObjects

    Public Class ApplicationGeoFenceSide

        Public Property ApplicationGeoFenceSideID As Guid
        Public Property ApplicationGeoFenceID As Guid
        Public Property Latitude As String
        Public Property Longitude As String
        'Public Property DateCreated As Date
        Public Property LoadOrder As Integer

        Public Sub New(ags As Business.ApplicationGeoFenceSide)

            With ags
                ApplicationGeoFenceSideID = .ApplicationGeoFenceSideID
                ApplicationGeoFenceID = .ApplicationGeoFenceID
                Latitude = .Latitude
                Longitude = .Longitude
                LoadOrder = ags.loadOrder
            End With

        End Sub

        Public Sub New()

        End Sub

    End Class

End Namespace

