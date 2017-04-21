Public Class CacheContact
    Public Property ID As Int32
    Public Property LineValies As List(Of Contact)
    Public Property LogoBinary() As Byte()

End Class
Public Class Contact
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
End Class
