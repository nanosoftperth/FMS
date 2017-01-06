Namespace DataObjects
    Public Class ApplicationImage


#Region "Properties"

        Public Property ApplicationID As Guid?
        Public Property ApplicationImageID As Guid
        Public Property Name As String
        Public Property Type As String
        Public Property Img() As Byte()

#End Region

#Region "constructors"
        ''' <summary>
        ''' for serialization 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(ad As FMS.Business.ApplicationImage)
            With ad
                Me.ApplicationID = ad.ApplicationID
                Me.ApplicationImageID = ad.ApplicationImageID
                Me.Name = ad.Name
                Me.Type = ad.Type

                If ad.Img IsNot Nothing Then _
                        Img = ad.Img.ToArray

            End With

        End Sub

#End Region

#Region "CRUD"

        Public Shared Function Create(ad As DataObjects.ApplicationImage)

            Dim d As New FMS.Business.ApplicationImage

            With d
                .ApplicationImageID = Guid.NewGuid
                .Name = ad.Name
                .Type = ad.Type
                .ApplicationID = ad.ApplicationID

                If ad.Img Is Nothing Then
                    Throw New Exception("Image must not be null")
                Else
                    .Img = New System.Data.Linq.Binary(ad.Img)
                End If

            End With

            SingletonAccess.FMSDataContextContignous.ApplicationImages.InsertOnSubmit(d)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
            Return d.ApplicationImageID
        End Function

        Public Shared Sub Update(ad As DataObjects.ApplicationImage)

            Dim d As FMS.Business.ApplicationImage = (From i In SingletonAccess.FMSDataContextContignous.ApplicationImages
                                                        Where i.ApplicationImageID = ad.ApplicationImageID).Single

            With d
                .Name = ad.Name
                .Type = ad.Type
                If ad.Img Is Nothing Then
                    Throw New Exception("Image must not be null")
                Else
                    .Img = New System.Data.Linq.Binary(ad.Img)
                End If
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(ad As DataObjects.ApplicationImage)

            Dim d As FMS.Business.ApplicationImage = (From i In SingletonAccess.FMSDataContextContignous.ApplicationImages
                                            Where i.ApplicationImageID = ad.ApplicationImageID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationImages.DeleteOnSubmit(d)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

#End Region

#Region "Get methods"



        Public Shared Function GetAllImages(applicationid As Guid, type As String)
            Dim retval = SingletonAccess.FMSDataContextNew.ApplicationImages. _
                        Where(Function(y) y.ApplicationID = applicationid And y.Type = type).Select(Function(x) _
                                                            New With {
                                                                .Id = x.ApplicationImageID,
                                                                .Name = x.Name
                                                                }).ToList
            retval.AddRange(GetAllDefaultImages(type))
            Return retval
        End Function

        Public Shared Function GetAllApplicationImages(applicationid As Guid, type As String) As List(Of DataObjects.ApplicationImage)
            Dim retval = (From x In SingletonAccess.FMSDataContextNew.ApplicationImages _
                        Where (x.ApplicationID = applicationid Or x.ApplicationID Is Nothing) And x.Type = type _
                        Select New With {
                                    .ApplicationImageID = x.ApplicationImageID,
                                    .Name = x.Name,
                                    .ImgUrl = "~/Content/MapImages.ashx?imgId=" + x.ApplicationImageID.ToString()
                                    }).ToList

            Return retval
        End Function

        'TODO: we need to tidy up the below two methods (are confusing)
        Public Shared Function GetDefaultImages(name As String) As Guid

            Return SingletonAccess.FMSDataContextNew.ApplicationImages.SingleOrDefault(Function(y) y.ApplicationID Is Nothing And y.Name = "Default Truck").ApplicationImageID
        End Function

        Public Shared Function GetDefaultTruckImageID() As Guid

            Return SingletonAccess.FMSDataContextNew.ApplicationImages.SingleOrDefault(Function(y) y.ApplicationID Is Nothing _
                                                                                           And y.Name = "Default Truck").ApplicationImageID
        End Function

        Public Shared Function GetDefaultHomeImageID() As Guid

            Return SingletonAccess.FMSDataContextNew.ApplicationImages.SingleOrDefault(Function(y) y.ApplicationID Is Nothing _
                                                                                           And y.Name = "Default Home").ApplicationImageID
        End Function

        Public Shared Function GetAllDefaultImages(type As String)

            Return SingletonAccess.FMSDataContextNew.ApplicationImages. _
                        Where(Function(y) y.ApplicationID Is Nothing And y.Type = type).Select(Function(x) _
                                                            New With {
                                                                .Id = x.ApplicationImageID,
                                                                .Name = x.Name
                                                                }).ToList
        End Function
        Public Shared Function GetImageFromID(Id As Guid) As DataObjects.ApplicationImage
            Dim ctx = New FMS.Business.LINQtoSQLClassesDataContext()
            Using ctx
                Return ctx.ApplicationImages. _
                            Where(Function(y) y.ApplicationImageID = Id).Select(Function(x) _
                                                                New DataObjects.ApplicationImage(x)).SingleOrDefault
            End Using
        End Function


#End Region
    End Class

End Namespace
