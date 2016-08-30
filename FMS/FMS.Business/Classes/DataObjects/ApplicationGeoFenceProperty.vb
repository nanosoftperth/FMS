

Namespace DataObjects

    Public Class ApplicationGeoFenceProperty

#Region "properties"

        Public Property ApplicationGeoFencePropertyID As Integer
        Public Property ApplicationGeoFenceID As Guid
        Public Property PropertyName As String
        Public Property PropertyValue As String

#End Region


#Region "constructors"

        ''' <summary>
        ''' for serialization
        ''' </summary>
        Public Sub New()

        End Sub

        Public Sub New(x As FMS.Business.ApplicationGeofenceProperty)

            Me.ApplicationGeoFenceID = x.ApplicationGeoFenceID
            Me.ApplicationGeoFencePropertyID = x.ApplicationGeoFencePropertyID
            Me.PropertyName = x.PropertyName
            Me.PropertyValue = x.PropertyValue

        End Sub

#End Region


#Region "CRUD"

        Public Shared Function Insert(c As DataObjects.ApplicationGeoFenceProperty) As Integer

            Dim dbobj As New FMS.Business.ApplicationGeofenceProperty

            With dbobj
                .ApplicationGeoFenceID = c.ApplicationGeoFenceID
                .ApplicationGeoFencePropertyID = c.ApplicationGeoFencePropertyID
                .PropertyName = c.PropertyName
                .PropertyValue = c.PropertyValue
            End With


            SingletonAccess.FMSDataContextContignous.ApplicationGeofenceProperties.InsertOnSubmit(dbobj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            'return the new primary key
            Return dbobj.ApplicationGeoFencePropertyID

        End Function

        Public Shared Sub Update(x As DataObjects.ApplicationGeoFenceProperty)

            Dim dbobj As FMS.Business.ApplicationGeofenceProperty = _
                SingletonAccess.FMSDataContextContignous.ApplicationGeofenceProperties _
                            .Where(Function(y) y.ApplicationGeoFencePropertyID = x.ApplicationGeoFencePropertyID).Single

            With dbobj
                '.ApplicationGeoFenceID = x.ApplicationGeoFenceID
                .PropertyName = x.PropertyName
                .PropertyValue = x.PropertyValue
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(x As DataObjects.ApplicationGeoFenceProperty)

            Dim dbobj As FMS.Business.ApplicationGeofenceProperty = _
                SingletonAccess.FMSDataContextContignous.ApplicationGeofenceProperties _
                    .Where(Function(y) y.ApplicationGeoFencePropertyID = x.ApplicationGeoFencePropertyID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationGeofenceProperties.DeleteOnSubmit(dbobj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub


#End Region

#Region "gets and sets"

        Public Shared Function GetForGeoFence(GeoFenceID As Guid) As List(Of DataObjects.ApplicationGeoFenceProperty)

            'Dim retobj As New List(Of Business.ApplicationGeofenceProperty) = SingletonAccess.
            Dim retobj As List(Of Business.DataObjects.ApplicationGeoFenceProperty) = _
                         (From agfp In SingletonAccess.FMSDataContextContignous.ApplicationGeofenceProperties _
                                Where agfp.ApplicationGeoFenceID = GeoFenceID _
                                    Select New DataObjects.ApplicationGeoFenceProperty(agfp)).ToList

            Return retobj

        End Function


#End Region


    End Class



End Namespace


