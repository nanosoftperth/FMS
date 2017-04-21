Imports FMS.Business

Public Class BusinessLocation
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
End Class
Public Class CacheBusinessLocation
    Public Property ID As Int32
    Public Property LineValies As List(Of BusinessLocation)
    Public Property LogoBinary() As Byte()
End Class