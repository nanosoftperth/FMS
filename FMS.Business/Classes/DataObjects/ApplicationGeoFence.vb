
Namespace DataObjects

    Public Class ApplicationGeoFence

#Region "properties"
        Public Property ApplicationGeoFenceID As Guid
        Public Property ApplicationID As Guid
        Public Property Name As String
        Public Property Description As String
        Public Property UserID As Guid
        Public Property DateCreated As Date
        Public Property Colour As String
        Public Property ApplicationGeoFenceSides As New List(Of ApplicationGeoFenceSide)
        Public Property IsCircular As Boolean
        Public Property CircleRadiusMetres As Decimal
        Public Property CircleCentre As String
        Dim _isBooking As Boolean
        Public Property isBooking As Nullable(Of Boolean)
            Set(value As Nullable(Of Boolean))
                _isBooking = If(value Is Nothing, False, value)
            End Set
            Get
                Return _isBooking
            End Get
        End Property

        'this is not read-only becuase this causes it to be invisible in javascript
        Public Property CircleCentreLat As Decimal
            Get
                If Not String.IsNullOrEmpty(CircleCentre) Then
                    Return CDec(CircleCentre.Split("|")(0))
                Else
                    Return Nothing
                End If
            End Get
            Set(value As Decimal)
                'do nothing 
            End Set
        End Property

        'this is not read-only becuase this causes it to be invisible in javascript
        Public Property CircleCentreLong As Decimal
            Get
                If Not String.IsNullOrEmpty(CircleCentre) Then
                    Return CDec(CircleCentre.Split("|")(1))
                Else
                    Return Nothing
                End If
            End Get
            Set(value As Decimal)
                'do nothing
            End Set
        End Property


#End Region

#Region "constructors"
        Public Sub New()

        End Sub

        Public Sub New(g As FMS.Business.ApplicationGeoFence)

            Dim defaultLatLong As String = "0|0"

            If g.ApplicationGeoFenceSides.Count > 0 Then

                With g.ApplicationGeoFenceSides(0)
                    defaultLatLong = String.Format("{0}|{1}", .Latitude, .Longitude)
                End With

            End If

            With Me
                .ApplicationGeoFenceID = g.ApplicationGeoFenceID
                .ApplicationID = g.ApplictionID
                .DateCreated = g.DateCreated
                .UserID = g.UserID
                .Name = g.Name
                .Description = g.Description
                .Colour = g.Colour
                .IsCircular = If(g.isCircular.HasValue, g.isCircular, False) 'default to polygon (historical reasons, shold not need to do this in future)
                .CircleRadiusMetres = If(g.CircleRadiusMetres.HasValue, g.CircleRadiusMetres, False)
                .CircleCentre = g.CircleCentre
                .isBooking = g.isBooking
            End With

            For Each s As Business.ApplicationGeoFenceSide In g.ApplicationGeoFenceSides.OrderBy(Function(x) x.loadOrder)
                Me.ApplicationGeoFenceSides.Add(New ApplicationGeoFenceSide(s))
            Next

        End Sub
#End Region

#Region "CRUD"

        Public Shared Function Create(agf As DataObjects.ApplicationGeoFence) As Guid

            Dim x As New FMS.Business.ApplicationGeoFence

            With x
                .ApplicationGeoFenceID = Guid.NewGuid
                .ApplictionID = agf.ApplicationID
                .DateCreated = Now
                .Description = agf.Description
                .isBooking = agf.isBooking
                .Name = agf.Name
                .Colour = agf.Colour
                .UserID = agf.UserID

                .isCircular = agf.IsCircular
                .CircleRadiusMetres = agf.CircleRadiusMetres
                .CircleCentre = agf.CircleCentre
            End With

            SingletonAccess.FMSDataContextContignous.ApplicationGeoFences.InsertOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            SingletonAccess.FMSDataContextContignous.Refresh(Data.Linq.RefreshMode.KeepChanges, x)

            Dim loadOrderIndex As Integer = 0

            'Create the new applicationGeoFenceSides and associate them with this geofence
            For Each f As ApplicationGeoFenceSide In agf.ApplicationGeoFenceSides

                Dim s As New FMS.Business.ApplicationGeoFenceSide

                s.ApplicationGeoFenceID = x.ApplicationGeoFenceID
                s.ApplicationGeoFenceSideID = Guid.NewGuid
                s.Latitude = f.Latitude
                s.Longitude = f.Longitude
                s.loadOrder = loadOrderIndex

                SingletonAccess.FMSDataContextContignous.ApplicationGeoFenceSides.InsertOnSubmit(s)
                SingletonAccess.FMSDataContextContignous.SubmitChanges()

                loadOrderIndex += 1
            Next

            'we want this to crash if the submit was not successful
            Return (From y In SingletonAccess.FMSDataContextNew.ApplicationGeoFences
                         Where y.ApplicationGeoFenceID = x.ApplicationGeoFenceID).Single.ApplicationGeoFenceID

        End Function

        Public Shared Sub Update(agf As DataObjects.ApplicationGeoFence)

            Dim x As FMS.Business.ApplicationGeoFence = (From i In SingletonAccess.FMSDataContextContignous.ApplicationGeoFences
                                                          Where i.ApplicationGeoFenceID = agf.ApplicationGeoFenceID).Single

            With x
                x.ApplictionID = agf.ApplicationID
                x.DateCreated = agf.DateCreated
                x.Description = agf.Description
                'x.isBooking = agf.isBooking
                x.Name = agf.Name
                x.UserID = agf.UserID
                x.Colour = agf.Colour

                x.isCircular = agf.IsCircular
                x.CircleRadiusMetres = agf.CircleRadiusMetres
                x.CircleCentre = agf.CircleCentre
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(agf As DataObjects.ApplicationGeoFence)

            'first, delete all of the "sides" related to the ApplicationGeoFence
            Dim geofencesides As List(Of FMS.Business.ApplicationGeoFenceSide) = _
                            (From agfs In SingletonAccess.FMSDataContextContignous.ApplicationGeoFenceSides _
                              Where agfs.ApplicationGeoFenceID = agf.ApplicationGeoFenceID).ToList

            SingletonAccess.FMSDataContextContignous.ApplicationGeoFenceSides.DeleteAllOnSubmit(geofencesides)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            'now, delete the geofence itself
            Dim x As FMS.Business.ApplicationGeoFence = (From i In SingletonAccess.FMSDataContextContignous.ApplicationGeoFences
                                                        Where i.ApplicationGeoFenceID = agf.ApplicationGeoFenceID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationGeoFences.DeleteOnSubmit(x)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

#End Region

#Region "static methods"
        Public Shared Function GetApplicationGeoFence(applicationGeoFenceID As Guid) As DataObjects.ApplicationGeoFence

            Dim retobj As DataObjects.ApplicationGeoFence = _
                       (From i In SingletonAccess.FMSDataContextNew.ApplicationGeoFences
                           Where i.ApplicationGeoFenceID = applicationGeoFenceID
                           Select New DataObjects.ApplicationGeoFence(i)).Single

            Return retobj

        End Function
        Public Shared Function FindApplicationGeoFence(applicationID As Guid, name As String) As DataObjects.ApplicationGeoFence

            Dim retobj As DataObjects.ApplicationGeoFence = _
                       (From i In SingletonAccess.FMSDataContextNew.ApplicationGeoFences
                           Where i.ApplictionID = applicationID And i.Name.Trim.ToLower.Equals(name.Trim.ToLower)
                           Select New DataObjects.ApplicationGeoFence(i)).FirstOrDefault

            Return retobj

        End Function
        Public Shared Function GetAllApplicationGeoFences(appID As Guid) As List(Of DataObjects.ApplicationGeoFence)

            Dim retobj As List(Of DataObjects.ApplicationGeoFence) = _
                            (From i In SingletonAccess.FMSDataContextNew.ApplicationGeoFences
                                Where i.ApplictionID = appID
                                Select New DataObjects.ApplicationGeoFence(i)).ToList.Take(2000).ToList

            Return retobj

        End Function
        'BY RYAN 
        'Check if name for Geofence already exist
        'Need to ensure that Geofence name is Unique
        Public Shared Function IfApplicationGeoFencesAlreadyExist(appID As Guid, xname As String) As Boolean

            Dim retobj = _
                            (From i In SingletonAccess.FMSDataContextNew.ApplicationGeoFences
                                Where i.ApplictionID = appID
                                Select i.Name).ToList.Contains(xname)

            Return retobj

        End Function

        'Public Shared Function GetAllApplicationGeoFences(appID As Guid,tl As Business.) As List(Of DataObjects.ApplicationGeoFence)

        '    Dim retobj As List(Of DataObjects.ApplicationGeoFence) = _
        '                    (From i In SingletonAccess.FMSDataContextNew.ApplicationGeoFences
        '                        Where i.ApplictionID = appID
        '                        Select New DataObjects.ApplicationGeoFence(i)).ToList.Take(2000).ToList

        '    Return retobj

        'End Function

#End Region

#Region "methods"

        Public Function UpdateGeoFenceSides(gfs As List(Of ApplicationGeoFenceSide)) As Boolean

            'find the old geo-fence sides
            Dim oldSides As List(Of FMS.Business.ApplicationGeoFenceSide) = _
                (From y In SingletonAccess.FMSDataContextContignous.ApplicationGeoFenceSides
                  Where y.ApplicationGeoFenceID = Me.ApplicationGeoFenceID).ToList

            'delete the old sides for the geo-fence
            SingletonAccess.FMSDataContextContignous.ApplicationGeoFenceSides.DeleteAllOnSubmit(oldSides)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            'create new geofencesides
            Dim newSides As New List(Of FMS.Business.ApplicationGeoFenceSide)
            Dim loadOrderIndex As Integer = 0

            For Each a As DataObjects.ApplicationGeoFenceSide In gfs

                Dim loopAppGeoFenceSide As New FMS.Business.ApplicationGeoFenceSide

                With loopAppGeoFenceSide
                    .Latitude = a.Latitude
                    .Longitude = a.Longitude
                    .loadOrder = loadOrderIndex
                    .ApplicationGeoFenceSideID = Guid.NewGuid
                    .ApplicationGeoFenceID = Me.ApplicationGeoFenceID
                End With

                newSides.Add(loopAppGeoFenceSide)

                loadOrderIndex += 1
            Next

            'add them to the DB
            SingletonAccess.FMSDataContextContignous.ApplicationGeoFenceSides.InsertAllOnSubmit(newSides)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            Return True
        End Function
#End Region

    End Class

End Namespace

