Imports System.IO
Imports System.Xml.Serialization

Public Class GoogleAPI


    Public Class TimeZoneResponse

        Public Property status As String
        Public Property raw_offset As String
        Public Property dst_offset As String
        Public Property time_zone_id As String
        Public Property time_zone_name As String

        Public ReadOnly Property OffsetHours As Double
            Get

                If String.IsNullOrEmpty(raw_offset) Then Return 0

                Dim offset As Double = CInt(raw_offset) / (60 ^ 2)
                Return offset
            End Get
        End Property

        Public ReadOnly Property OffsetHoursDST As Double
            Get
                If String.IsNullOrEmpty(DST_Offset) Then Return 0

                Dim offset As Double = CInt(DST_Offset) / (60 ^ 2)
                Return offset
            End Get
        End Property


        'for serialization only  (reqiured)
        Public Sub New()

        End Sub

    End Class

    Public Shared Function GetCurrentTimeZoneOffset(lat As String, lng As String) As TimeZoneResponse

        Dim url As String = "https://maps.googleapis.com/maps/api/timezone/xml?location={0},{1}&timestamp={2}&key=AIzaSyA2FG3uZ6Pnj8ANsyVaTwnPOCZe4r6jd0g"

        '{0} = lat ,{1} = lng, {2} = timestamp (in seconds, i think UTC format)

        If String.IsNullOrEmpty(lat) Or lat = "0" Then lat = "31.9535"
        If String.IsNullOrEmpty(lng) Or lng = "0" Then lng = "115.8570"

        'Get the total amount of seconds from 01/jan/1970 to the gurrent GMT time
        Dim timestamp As String = CInt((DateTime.UtcNow - CDate("01 jan 1970")).TotalSeconds)

        url = String.Format(url, lat, lng, timestamp)

        Dim webRequest As Net.HttpWebRequest = Net.WebRequest.Create(url)
        Dim XMLStr As String

        With New StreamReader(webRequest.GetResponse.GetResponseStream, System.Text.Encoding.UTF8)
            XMLStr = .ReadToEnd()
        End With

        Dim tzr As New TimeZoneResponse

        'Dim xRoot As New XmlRootAttribute With {.ElementName = "TimeZoneResponse" _
        '                                       , .IsNullable = True}



        Dim xmls As New XmlSerializer(tzr.GetType)

        Dim rslt As TimeZoneResponse


        'XmlRootAttribute xRoot = new XmlRootAttribute();
        'xRoot.ElementName = "user";
        '// xRoot.Namespace = "http://www.cpandl.com";
        'xRoot.IsNullable = true;
        'XmlSerializer xs = new XmlSerializer(typeof(User),xRoot);


        Using reader As TextReader = New StringReader(XMLStr)
            rslt = xmls.Deserialize(reader)
        End Using

        Return rslt

    End Function


End Class
