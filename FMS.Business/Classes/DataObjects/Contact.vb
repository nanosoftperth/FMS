Namespace DataObjects

    Public Class Contact



#Region "properties"

        Public Property ApplicationID As Guid
        Public Property Forname As String
        Public Property Surname As String
        Public Property EmailAddress As String
        Public Property MobileNumber As String
        Public Property CompanyName As String
        Public Property ContactID As Guid
        Public ReadOnly Property NameFormatted As String
            Get
                Return String.Format("{0} {1}", Forname, Surname)
            End Get
        End Property

#End Region

#Region "constructors"

        Public Sub New()

        End Sub


        Public Sub New(x As Business.Contact)

            Me.ApplicationID = x.ApplicationID
            Me.Forname = x.Forname
            Me.Surname = x.Surname
            Me.EmailAddress = x.EmailAddress
            Me.MobileNumber = x.MobileNumber
            Me.CompanyName = x.CompanyName
            Me.ContactID = x.ContactID

        End Sub

#End Region


#Region "CRUD"

        Public Shared Function Create(x As DataObjects.Contact) As Guid

            Dim newDBObject As New Business.Contact

            With newDBObject

                .ContactID = If(x.ContactID = Guid.Empty, Guid.NewGuid, x.ContactID)

                .ApplicationID = x.ApplicationID
                .Forname = x.Forname
                .Surname = x.Surname
                .EmailAddress = x.EmailAddress
                .MobileNumber = x.MobileNumber
                .CompanyName = x.CompanyName
            End With

            SingletonAccess.FMSDataContextContignous.Contacts.InsertOnSubmit(newDBObject)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Function

        Public Shared Sub Update(x As DataObjects.Contact)

            Dim dbobj As FMS.Business.Contact = _
                        SingletonAccess.FMSDataContextContignous.Contacts _
                                .Where(Function(o) o.ContactID = x.ContactID).Single

            With x

                dbobj.ApplicationID = .ApplicationID
                dbobj.Forname = .Forname
                dbobj.Surname = .Surname
                dbobj.EmailAddress = .EmailAddress
                dbobj.MobileNumber = .MobileNumber
                dbobj.CompanyName = .CompanyName

            End With

            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Delete(x As DataObjects.Contact)

            Dim dbobj As FMS.Business.Contact = _
                       SingletonAccess.FMSDataContextContignous.Contacts _
                               .Where(Function(o) o.ContactID = x.ContactID).Single

            SingletonAccess.FMSDataContextContignous.Contacts.DeleteOnSubmit(dbobj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub


#End Region

#Region "gets and sets"

        Public Shared Function GetForAlertID(contactid As Guid)

            Return (From x In SingletonAccess.FMSDataContextNew.Contacts _
                     Where x.ContactID = contactid _
                     Select New DataObjects.Contact(x)).ToList

        End Function
        Public Shared Function GetAllForApplication(appidd As Guid) As List(Of Contact)

            Return (From x In SingletonAccess.FMSDataContextNew.Contacts _
                     Where x.ApplicationID = appidd _
                     Select New DataObjects.Contact(x)).ToList

        End Function

#End Region




    End Class


End Namespace
