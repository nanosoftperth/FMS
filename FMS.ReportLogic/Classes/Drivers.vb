Public Class CacheDriver
    Public Property ID As Int32
    Public Property LineValies As List(Of Driver)
End Class
Public Class Driver
    Public Property ApplicationID As Guid
    Public Property ApplicationDriverID As Guid
    Public Property FirstName As String
    Public Property Surname As String
    Public Property PhoneNumber As String
    Public Property PhotoLocation As String
    Public Property PhotoBinary() As Byte()
    Public Property Notes As String 
    Public Property EmailAddress As String  
End Class

