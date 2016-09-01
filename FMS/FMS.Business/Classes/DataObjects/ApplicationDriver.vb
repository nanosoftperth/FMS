Namespace DataObjects
    Public Class ApplicationDriver

#Region "Properties"

        Public Property ApplicationID As Guid
        Public Property ApplicationDriverID As Guid
        Public Property FirstName As String
        Public Property Surname As String
        Public Property PhoneNumber As String
        Public Property PhotoLocation As String
        Public Property PhotoBinary() As Byte()
        Public Property Notes As String

        Public Property EmailAddress As String

        ''' <summary>
        ''' For when bound to a cotnrol which needs the "everyone" option
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property RepresentsEveryone As Boolean = False


        Public ReadOnly Property ApplicationDriverIDAsString As String
            Get

                If ApplicationDriverID = Guid.Empty Then
                    Return String.Empty
                Else
                    Return ApplicationDriverID.ToString
                End If

            End Get
        End Property

        Public ReadOnly Property NameFormatted As String
            Get
                Return String.Format("{0} {1}", FirstName, Surname)
            End Get
        End Property

#End Region

#Region "constructors"
        ''' <summary>
        ''' for serialization 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(ad As FMS.Business.ApplicationDriver)

            With ad
                Me.ApplicationDriverID = ad.ApplicationDriverID
                Me.FirstName = ad.FirstName
                Me.PhoneNumber = ad.PhoneNumber
                'Me.PhotoBinary = ad.ApplicationDriverID
                Me.PhotoLocation = ad.photo
                Me.Surname = ad.Surname
                Me.ApplicationID = ad.ApplicationID
                Me.Notes = ad.Notes
                Me.EmailAddress = ad.emailaddress


                If ad.photoBinary IsNot Nothing Then _
                        PhotoBinary = ad.photoBinary.ToArray

            End With

        End Sub

#End Region

#Region "CRUD"

        Public Shared Sub Create(ad As DataObjects.ApplicationDriver)

            Dim d As New FMS.Business.ApplicationDriver

            With d
                .ApplicationDriverID = Guid.NewGuid
                .FirstName = ad.FirstName
                .PhoneNumber = ad.PhoneNumber
                .photo = ad.PhotoLocation
                .Surname = ad.Surname
                .ApplicationID = ad.ApplicationID
                .Notes = ad.Notes
                .emailaddress = ad.EmailAddress

                If ad.PhotoBinary Is Nothing Then
                    'get the "mysteryman" row
                    .photoBinary = SingletonAccess.FMSDataContextContignous.ApplicationDrivers.Where(Function(i) i.FirstName = "mysteryman").Single.photoBinary

                Else
                    .photoBinary = New System.Data.Linq.Binary(ad.PhotoBinary)
                End If

            End With

            SingletonAccess.FMSDataContextContignous.ApplicationDrivers.InsertOnSubmit(d)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Update(ad As DataObjects.ApplicationDriver)

            Dim d As FMS.Business.ApplicationDriver = (From i In SingletonAccess.FMSDataContextContignous.ApplicationDrivers
                                                        Where i.ApplicationDriverID = ad.ApplicationDriverID).Single

            With d
                .ApplicationDriverID = ad.ApplicationDriverID
                .FirstName = ad.FirstName
                .PhoneNumber = ad.PhoneNumber
                .photo = ad.PhotoLocation
                .Surname = ad.Surname
                .ApplicationID = ad.ApplicationID
                .Notes = ad.Notes
                .emailaddress = ad.EmailAddress

                If ad.PhotoBinary IsNot Nothing Then .photoBinary = New System.Data.Linq.Binary(ad.PhotoBinary)
            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(ad As DataObjects.ApplicationDriver)

            Dim d As FMS.Business.ApplicationDriver = (From i In SingletonAccess.FMSDataContextContignous.ApplicationDrivers
                                            Where i.ApplicationDriverID = ad.ApplicationDriverID).Single

            SingletonAccess.FMSDataContextContignous.ApplicationDrivers.DeleteOnSubmit(d)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub

#End Region

#Region "Get methods"


        Public Shared Function GetMysteryManImage() As Byte()

            Return SingletonAccess.FMSDataContextContignous.ApplicationDrivers.Where( _
                                Function(i) i.FirstName = "mysteryman").Single.photoBinary.ToArray

        End Function

        Public Shared ReadOnly Property DriverREpresentingEveryone As DataObjects.ApplicationDriver
            Get
                Return New ApplicationDriver() With {.ApplicationDriverID = New Guid("d44c2063-d241-49b8-92c1-623dfc23ddcc") _
                                                    , .Surname = "Anyone" _
                                                    , .RepresentsEveryone = True}
            End Get
        End Property


        Public Shared Function GetAllDriversIncludingEveryone(applicatoinid As Guid) As List(Of DataObjects.ApplicationDriver)

            Dim retSet As List(Of DataObjects.ApplicationDriver) = GetAllDrivers(applicatoinid)

            retSet = retSet.OrderBy(Function(x) x.Surname).ToList

            'add ot the top of the list
            retSet.Insert(0, DriverREpresentingEveryone)

            Return retSet

        End Function


        Public Shared Function GetAllDrivers(applicatoinid As Guid) As List(Of DataObjects.ApplicationDriver)

            Return SingletonAccess.FMSDataContextNew.ApplicationDrivers. _
                        Where(Function(y) y.ApplicationID = applicatoinid).Select(Function(x) _
                                                            New DataObjects.ApplicationDriver(x) _
                                                            ).ToList
        End Function

        Public Shared Function GetDriverFromID(applicationdriverid As Guid) As DataObjects.ApplicationDriver

            'if this happens to be the ""driver representing everyone"
            If applicationdriverid = DriverREpresentingEveryone.ApplicationDriverID Then Return DriverREpresentingEveryone

            Return SingletonAccess.FMSDataContextContignous.ApplicationDrivers. _
                        Where(Function(y) y.ApplicationDriverID = applicationdriverid).Select(Function(x) _
                                                            New DataObjects.ApplicationDriver(x)).SingleOrDefault
        End Function


#End Region

    End Class
End Namespace

