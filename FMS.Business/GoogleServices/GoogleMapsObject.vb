Imports System.IO

Public Class GoogleGeoCodeResponse

    Public Property status() As String
        Get
            Return m_status
        End Get
        Set(value As String)
            m_status = value
        End Set
    End Property
    Private m_status As String
    Public Property results() As results()
        Get
            Return m_results
        End Get
        Set(value As results())
            m_results = value
        End Set
    End Property
    Private m_results As results()

    Public Shared Function GetForLatLong(lat As String, lng As String) As String

        Try

            'If String.IsNullOrEmpty(lat) AndAlso String.IsNullOrEmpty(lng) Then
            '    lat = "-31.949618"
            '    lng = "115.832794"
            'End If

            Dim url As String = "https://maps.google.com/maps/api/geocode/json?address={0},{1}&sensor=false&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g"
            url = String.Format(url, lat, lng)

            Dim webRequest As Net.HttpWebRequest = Net.WebRequest.Create(url)
            Dim JSONString As String

            With New StreamReader(webRequest.GetResponse.GetResponseStream, System.Text.Encoding.UTF8)
                JSONString = .ReadToEnd()
            End With

            Dim rslt As Object

            Dim x As New Truck

            rslt = Newtonsoft.Json.JsonConvert.DeserializeObject(JSONString)
            Dim str As String = DirectCast(rslt, Newtonsoft.Json.Linq.JObject)("results")(0)("formatted_address")


            Return str

        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    Public Shared Sub cannontestMakeGeoFences()

        Dim app As DataObjects.Application = DataObjects.Application.GetFromApplicationName("cannon")

        Dim u As DataObjects.User = DataObjects.User.GetAllUsersForApplication(app.ApplicationID).Where( _
                                                                        Function(x) x.UserName.ToLower = "dave").First


        For Each cd In (From x In SingletonAccess.FMSDataContextContignous.CannonDatas Where x.calc_field1 IsNot Nothing).ToList

            Dim o As New FMS.Business.DataObjects.ApplicationGeoFence()

            With o

                .ApplicationGeoFenceID = Guid.NewGuid
                .CircleCentre = cd.calc_lat & "|" & cd.calc_lng
                .CircleRadiusMetres = 100
                .Description = cd.SiteName
                .Name = cd.CustomerName
                .ApplicationID = app.ApplicationID
                .Colour = "#FF0001"
                .DateCreated = Now
                .IsCircular = True
                .UserID = u.UserId
            End With

            'create square about 100m wide
            Dim lat As Decimal = cd.calc_lat
            Dim lng As Decimal = cd.calc_lng

            Dim tl As latlng = New latlng With {.lat = lat, .lng = lng}
            Dim tr As latlng = New latlng With {.lat = lat, .lng = lng}
            Dim bl As latlng = New latlng With {.lat = lat, .lng = lng}
            Dim br As latlng = New latlng With {.lat = lat, .lng = lng}



            DataObjects.ApplicationGeoFence.Create(o)

        Next


    End Sub

    Public Shared Sub CannonTest()

        Dim i As Integer = 0

        Dim app As DataObjects.Application = DataObjects.Application.GetFromApplicationName("cannon")
        Dim u As DataObjects.User = DataObjects.User.GetAllUsersForApplication(app.ApplicationID).Where(Function(x) x.UserName.ToLower = "dave").First

        For Each cd In (From x In SingletonAccess.FMSDataContextContignous.CannonDatas Where x.calc_field1 Is Nothing).ToList

            Try

                Dim address = String.Format("{0},{1},{2},{3},{4},{5}, Australia", cd.AddressLine1, cd.AddressLine2, cd.AddressLine3, _
                                                                                        cd.AddressLine4, cd.Suburb, cd.PostCode)


                Dim str As latlng = GetLatLongFromAddress(address)

                cd.calc_lat = str.lat
                cd.calc_lng = str.lng
                cd.calc_field1 = str.formatted_address

                For Each side In str.ApplicationGeoFenceSides
                    Select Case side.LoadOrder
                        Case 1 : cd.NW_latlng = side.Latitude & "|" & side.Longitude
                        Case 2 : cd.NE_latlng = side.Latitude & "|" & side.Longitude
                        Case 3 : cd.SE_latlng = side.Latitude & "|" & side.Longitude
                        Case 4 : cd.SW_latlng = side.Latitude & "|" & side.Longitude
                    End Select
                Next

                SingletonAccess.FMSDataContextContignous.SubmitChanges()

                Dim o As New FMS.Business.DataObjects.ApplicationGeoFence()

                With o

                    .ApplicationGeoFenceID = Guid.NewGuid
                    .CircleCentre = cd.calc_lat & "|" & cd.calc_lng
                    .CircleRadiusMetres = 100
                    .Description = cd.SiteName
                    .Name = cd.CustomerName
                    .ApplicationID = app.ApplicationID
                    .Colour = "#FF0000"
                    .DateCreated = Now
                    .IsCircular = True
                    .UserID = u.UserId
                End With


                For Each y In str.ApplicationGeoFenceSides
                    y.ApplicationGeoFenceID = o.ApplicationGeoFenceID
                    y.ApplicationGeoFenceSideID = Guid.NewGuid
                Next

                o.ApplicationGeoFenceSides = str.ApplicationGeoFenceSides

                o.ApplicationGeoFenceID = DataObjects.ApplicationGeoFence.Create(o)


                AddGeoFenceProperty(o.ApplicationGeoFenceID, "CustomerName", cd.CustomerName)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "RunDescription", cd.RunDescription)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "PhoneNo", cd.PhoneNo)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "RunDriver", cd.RunDriver)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "RunNUmber", cd.RunNUmber)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "ServiceDescription", cd.ServiceDescription)

                AddGeoFenceProperty(o.ApplicationGeoFenceID, "Address", cd.calc_field1)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "Units Have More Than One Run", cd.UnitsHaveMoreThanOneRun)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "ServiceCode", cd.ServiceCode)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "RunDriver", cd.RunDriver)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "ServiceComments", cd.ServiceComments)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "SiteName", cd.SiteName)

                AddGeoFenceProperty(o.ApplicationGeoFenceID, "InactiveRun", cd.InactiveRun)

                AddGeoFenceProperty(o.ApplicationGeoFenceID, "ServicePrice", cd.ServicePrice)

                AddGeoFenceProperty(o.ApplicationGeoFenceID, "ServiceDescription", cd.ServiceDescription)


                AddGeoFenceProperty(o.ApplicationGeoFenceID, "AddressLine1", cd.AddressLine1)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "AddressLine2", cd.AddressLine2)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "AddressLine3", cd.AddressLine3)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "AddressLine4", cd.AddressLine4)
                AddGeoFenceProperty(o.ApplicationGeoFenceID, "PostCode", cd.PostCode)


                Console.WriteLine(i)

                i += 1
                Console.WriteLine(i)

            Catch ex As Exception

                Console.WriteLine(ex.Message)

            End Try

        Next

    End Sub


    Public Shared Function AddGeoFenceProperty(ApplicationGeoFenceID As Guid, propName As String, propVal As String) As Integer


        Dim newprop As New DataObjects.ApplicationGeoFenceProperty With {.ApplicationGeoFenceID = ApplicationGeoFenceID _
                                                                , .PropertyName = propName _
                                                                 , .PropertyValue = propVal}

        Return DataObjects.ApplicationGeoFenceProperty.Insert(newprop)

    End Function

    Public Class latlng
        Public Property lat As String
        Public Property lng As String

        Public Property formatted_address As String

        Public Property ApplicationGeoFenceSides As New List(Of DataObjects.ApplicationGeoFenceSide)

        Public Sub New()

        End Sub


    End Class

    Public Shared Function GetLatLongFromAddress(address As String) As latlng

        Try

            'If String.IsNullOrEmpty(lat) AndAlso String.IsNullOrEmpty(lng) Then
            '    lat = "-31.949618"
            '    lng = "115.832794"
            'End If

            Dim url As String = "https://maps.google.com/maps/api/geocode/json?address={0}&sensor=false&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g"
            url = String.Format(url, address)

            Dim webRequest As Net.HttpWebRequest = Net.WebRequest.Create(url)
            Dim JSONString As String

            With New StreamReader(webRequest.GetResponse.GetResponseStream, System.Text.Encoding.UTF8)
                JSONString = .ReadToEnd()
            End With

            Dim rslt As Object

            Dim x As New Truck

            rslt = Newtonsoft.Json.JsonConvert.DeserializeObject(JSONString)

            Dim ltlng As New latlng
            '
            ltlng.formatted_address = DirectCast(rslt, Newtonsoft.Json.Linq.JObject)("results")(0)("formatted_address")
            ltlng.lat = DirectCast(rslt, Newtonsoft.Json.Linq.JObject)("results")(0)("geometry")("location")("lat")
            ltlng.lng = DirectCast(rslt, Newtonsoft.Json.Linq.JObject)("results")(0)("geometry")("location")("lng")

            '--------- NE
            ' 
            '
            'SW--------

            Dim ll_topright_lat As String = DirectCast(rslt, Newtonsoft.Json.Linq.JObject)("results")(0)("geometry")("viewport")("northeast")("lat")
            Dim ll_topright_lng As String = DirectCast(rslt, Newtonsoft.Json.Linq.JObject)("results")(0)("geometry")("viewport")("northeast")("lng")

            Dim ll_bottomleft_lat As String = DirectCast(rslt, Newtonsoft.Json.Linq.JObject)("results")(0)("geometry")("viewport")("southwest")("lat")
            Dim ll_bottomleft_lng As String = DirectCast(rslt, Newtonsoft.Json.Linq.JObject)("results")(0)("geometry")("viewport")("southwest")("lng")

            'below four can be calucated from the above four
            Dim ll_topleft_lat As String = ll_bottomleft_lat
            Dim ll_topleft_lng As String = ll_topright_lng

            Dim ll_bottomright_lat As String = ll_topright_lat
            Dim ll_bottomright_lng As String = ll_bottomleft_lng
            

            Dim xy As New List(Of DataObjects.ApplicationGeoFenceSide)

            xy.Add(New DataObjects.ApplicationGeoFenceSide With _
                                {.Latitude = ll_topleft_lat, .Longitude = ll_topleft_lng, .LoadOrder = 1})

            xy.Add(New DataObjects.ApplicationGeoFenceSide With _
                                {.Latitude = ll_topright_lat, .Longitude = ll_topright_lng, .LoadOrder = 2})

            xy.Add(New DataObjects.ApplicationGeoFenceSide With _
                                {.Latitude = ll_bottomright_lat, .Longitude = ll_bottomright_lng, .LoadOrder = 3})

            xy.Add(New DataObjects.ApplicationGeoFenceSide With _
                                {.Latitude = ll_bottomleft_lat, .Longitude = ll_bottomleft_lng, .LoadOrder = 4})

            ltlng.ApplicationGeoFenceSides = xy

            Return ltlng

        Catch ex As Exception
            Throw
        End Try

    End Function


End Class

Public Class results
    Public Property formatted_address() As String
        Get
            Return m_formatted_address
        End Get
        Set(value As String)
            m_formatted_address = value
        End Set
    End Property
    Private m_formatted_address As String
    Public Property geometry() As geometry
        Get
            Return m_geometry
        End Get
        Set(value As geometry)
            m_geometry = value
        End Set
    End Property
    Private m_geometry As geometry
    Public Property types() As String()
        Get
            Return m_types
        End Get
        Set(value As String())
            m_types = value
        End Set
    End Property
    Private m_types As String()
    Public Property address_components() As address_component()
        Get
            Return m_address_components
        End Get
        Set(value As address_component())
            m_address_components = value
        End Set
    End Property
    Private m_address_components As address_component()
End Class

Public Class geometry
    Public Property location_type() As String
        Get
            Return m_location_type
        End Get
        Set(value As String)
            m_location_type = value
        End Set
    End Property
    Private m_location_type As String
    Public Property location() As location
        Get
            Return m_location
        End Get
        Set(value As location)
            m_location = value
        End Set
    End Property
    Private m_location As location
End Class

Public Class location
    Public Property lat() As String
        Get
            Return m_lat
        End Get
        Set(value As String)
            m_lat = value
        End Set
    End Property
    Private m_lat As String
    Public Property lng() As String
        Get
            Return m_lng
        End Get
        Set(value As String)
            m_lng = value
        End Set
    End Property
    Private m_lng As String
End Class

Public Class address_component
    Public Property long_name() As String
        Get
            Return m_long_name
        End Get
        Set(value As String)
            m_long_name = value
        End Set
    End Property
    Private m_long_name As String
    Public Property short_name() As String
        Get
            Return m_short_name
        End Get
        Set(value As String)
            m_short_name = value
        End Set
    End Property
    Private m_short_name As String
    Public Property types() As String()
        Get
            Return m_types
        End Get
        Set(value As String())
            m_types = value
        End Set
    End Property
    Private m_types As String()
End Class
