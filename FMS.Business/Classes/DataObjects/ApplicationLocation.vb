
Namespace DataObjects

    Public Class ApplicationLocation

        Public Property ApplicationLocationID As Guid
        Public Property ApplicationID As Guid
        Public Property Name As String
        Public Property Longitude As String
        Public Property Lattitude As String

        Public Property Address As String

      
        Public Sub New()

        End Sub


        Public Sub New(al As Business.ApplicationLocation)

            With al
                Me.ApplicationID = .ApplicationID
                Me.ApplicationLocationID = .ApplicationLocationID
                Me.Lattitude = .Lattitude
                Me.Longitude = .Longitude
            End With

        End Sub

#Region "CRUD"

        Public Shared Function Create(al As Business.DataObjects.ApplicationLocation) As Guid


            Dim newobj As New Business.ApplicationLocation

            With newobj
                .Address = al.Address
                .ApplicationID = al.ApplicationID
                .ApplicationLocationID = If(al.ApplicationLocationID = Guid.Empty, Guid.NewGuid, al.ApplicationLocationID)
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

            Dim dbobj = SingletonAccess.FMSDataContextContignous.ApplicationLocations.Where( _
                            Function(x) x.ApplicationLocationID = al.ApplicationLocationID).Single

            With dbobj
                .Address = al.Address
                .ApplicationID = al.ApplicationID
                '.ApplicationLocationID = al.ApplicationLocationID
                .Lattitude = al.Lattitude
                .Longitude = al.Longitude
                .Name = al.Name
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

#End Region


#Region "gets & sets"


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
            If retobj.Count = 0 AndAlso IncludeDefault Then

                Dim appObj As DataObjects.Application = DataObjects.Application.GetFromAppID(ApplicationID)

                Dim lattitude As String = appObj.Settings.Where(Function(x) x.Name = "Business_Lattitude").Single.Value
                Dim longitude As String = appObj.Settings.Where(Function(x) x.Name = "Business_Longitude").Single.Value
                Dim Name As String = appObj.Settings.Where(Function(x) x.Name = "ApplicationName").Single.Value

                Dim defaultObj As New DataObjects.ApplicationLocation With {.ApplicationID = ApplicationID,
                                                                            .ApplicationLocationID = Guid.Empty,
                                                                            .Lattitude = lattitude,
                                                                            .Longitude = longitude,
                                                                            .Name = Name}

                retobj.Add(defaultObj)

            End If



            Return retobj


        End Function


#End Region


    End Class

End Namespace


