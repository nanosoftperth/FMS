
Namespace DataObjects

    Public Class ApplicationLocation

        Public Property ApplicationLocationID As Guid
        Public Property ApplicationID As Guid
        Public Property Name As String
        Public Property Longitude As String
        Public Property Lattitude As String
        Public Property Address As String

        Public Property ApplicationImageID As Guid

        Public Property ApplicationImage As DataObjects.ApplicationImage

        ''' <summary>
        ''' for binding to devexpress controls, just gets the image from the applicationimage object
        ''' </summary>
        Public ReadOnly Property ImageBinary() As Byte()
            Get
                Return If(ApplicationImage Is Nothing, Nothing, ApplicationImage.Img)
            End Get
        End Property

        Public Sub New()

        End Sub


        Public Sub New(al As Business.ApplicationLocation)

            With al

                Me.ApplicationID = .ApplicationID
                Me.ApplicationLocationID = .ApplicationLocationID
                Me.Lattitude = .Lattitude
                Me.Longitude = .Longitude
                Me.Address = .Address
                Me.Name = .Name

                'If there is no applicationimage defined (param == null, then use the "default" one instead).
                Me.ApplicationImageID = If(.ApplicationImageID Is Nothing, DataObjects.ApplicationImage.GetDefaultHomeImageID, .ApplicationImageID)
                Me.ApplicationImage = DataObjects.ApplicationImage.GetImageFromID(.ApplicationImageID)

            End With

        End Sub

#Region "CRUD"

        Public Shared Function Create(al As Business.DataObjects.ApplicationLocation) As Guid


            Dim newobj As New Business.ApplicationLocation

            With newobj
                .Address = al.Address
                .ApplicationID = al.ApplicationID
                .ApplicationLocationID = If(al.ApplicationLocationID = Guid.Empty, Guid.NewGuid, al.ApplicationLocationID)
                .ApplicationImageID = al.ApplicationImageID
                .Lattitude = al.Lattitude
                .Longitude = al.Longitude
                .Name = al.Name
            End With

            Business.SingletonAccess.FMSDataContextContignous.ApplicationLocations.InsertOnSubmit(newobj)
            Business.SingletonAccess.FMSDataContextContignous.SubmitChanges()

            Return newobj.ApplicationLocationID

        End Function

        Public Shared Sub Delete(al As Business.DataObjects.ApplicationLocation)


            With SingletonAccess.FMSDataContextContignous

                Dim delobj = .ApplicationLocations.Where(Function(x) x.ApplicationLocationID = al.ApplicationLocationID).Single

                .ApplicationLocations.DeleteOnSubmit(delobj)
                .SubmitChanges()
            End With

        End Sub

        Public Shared Sub Update(al As Business.DataObjects.ApplicationLocation)

            'if we are being asked to update the application locatoin which has been 
            '"temporarily created" (this is done when there are no locations defined yet but there is
            'old style setting defined for the copmany location. Then we need to create the applicationlocation object
            'and update that with these settings.
            Dim updatingNonExistantObj As Boolean = al.ApplicationLocationID = Guid.Empty

            If updatingNonExistantObj Then
                Create(al)
                Exit Sub
            End If

            Dim dbobj = SingletonAccess.FMSDataContextContignous.ApplicationLocations.Where( _
                            Function(x) x.ApplicationLocationID = al.ApplicationLocationID).Single

            With dbobj
                .Address = al.Address
                .ApplicationID = al.ApplicationID
                '.ApplicationLocationID = al.ApplicationLocationID
                .ApplicationImageID = al.ApplicationImageID
                .Lattitude = al.Lattitude
                .Longitude = al.Longitude
                .Name = al.Name
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

#End Region


#Region "gets & sets"

        Public Shared Function GetAllIncludingInheritFromApplication(ApplicationID As Guid) As List(Of DataObjects.ApplicationLocation)

            Dim r = GetAll(ApplicationID, True)

            For Each x In r
                If x.ApplicationLocationID = Guid.Empty Then x.Name = "use app default"
            Next

            Return r

        End Function

        'required for devexpress binding
        Public Shared Function GetAllIncludingDefault(ApplicationID As Guid) As List(Of DataObjects.ApplicationLocation)


            Dim includeDefault = DataObjects.Application.GetFromAppID(ApplicationID).DefaultBusinessLocationID = Guid.Empty

            Return GetAll(ApplicationID, includeDefault)
        End Function


        Public Shared Function GetFromID(appLocationID As Guid) As DataObjects.ApplicationLocation

            Dim dbobj = SingletonAccess.FMSDataContextContignous.ApplicationLocations.Where(Function(x) x.ApplicationLocationID = appLocationID).Single

            Return New DataObjects.ApplicationLocation(dbobj)

        End Function

        ''' <summary>
        ''' Normal "get all", with  few caveats
        ''' </summary>
        ''' <param name="ApplicationID"></param>
        ''' <param name="IncludeDefault">If there are 0 ApplicationLocations, then add
        ''' the "old style" application (lat/long/name) settings as a applictionLocation object </param>
        Public Shared Function GetAll(ApplicationID As Guid, Optional IncludeDefault As Boolean = False) As List(Of DataObjects.ApplicationLocation)

            Dim retobj As New List(Of DataObjects.ApplicationLocation)

            With New LINQtoSQLClassesDataContext

                retobj = (From x In .ApplicationLocations
                          Where x.ApplicationID = ApplicationID
                          Order By x.Name Ascending
                          Select New DataObjects.ApplicationLocation(x)).ToList


                .Dispose()
            End With

            'If we have 0 items in the list, then we shall use the "old school" logitude and lattitude provided by the application object
            If IncludeDefault Then

                Dim appObj As DataObjects.Application = DataObjects.Application.GetFromAppID(ApplicationID)

                Dim lattitude As String = appObj.Settings.Where(Function(x) x.Name = "Business_Lattitude").Single.Value
                Dim longitude As String = appObj.Settings.Where(Function(x) x.Name = "Business_Longitude").Single.Value
                Dim Name As String = appObj.Settings.Where(Function(x) x.Name = "CompanyName").Single.Value

                Dim appImageID As Guid = DataObjects.ApplicationImage.GetDefaultHomeImageID
                Dim img As DataObjects.ApplicationImage = DataObjects.ApplicationImage.GetImageFromID(appImageID)

                Dim defaultObj As New DataObjects.ApplicationLocation With {.ApplicationID = ApplicationID,
                                                                            .ApplicationLocationID = Guid.Empty,
                                                                            .Lattitude = lattitude,
                                                                            .Longitude = longitude,
                                                                            .Name = Name,
                                                                            .ApplicationImageID = appImageID,
                                                                            .ApplicationImage = img}
                retobj.Add(defaultObj)

            End If



            Return retobj


        End Function

        Public Shared Function GetLocationVehicle(startdate As DateTime, enddate As DateTime, vehicleid As Guid, applicationId As Guid) As DataObjects.ApplicationLocation

            Try
                Dim avd = FMS.Business.DataObjects.ApplicationVehicleDriverTime.GetAllForApplicationAndDatePeriodIncludingDuds(applicationId, startdate, enddate)
                Dim assigneddriver = (From x In avd Where x.VehicleID = vehicleid Select DataObjects.ApplicationDriver.GetDriverFromID(x.ApplicationDriverId)).FirstOrDefault
                If assigneddriver.ApplicationLocationID Is Nothing Or assigneddriver.ApplicationLocationID = Guid.Empty Then
                    Return GetFromID(DataObjects.Application.GetFromAppID(applicationId).DefaultBusinessLocationID)
                Else
                    Return GetFromID(assigneddriver.ApplicationLocationID)
                End If
            Catch ex As Exception
                Return GetFromID(DataObjects.Application.GetFromAppID(applicationId).DefaultBusinessLocationID)
            End Try


        End Function
#End Region


    End Class

End Namespace


