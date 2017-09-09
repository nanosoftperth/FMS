Imports System.Net
Imports System.Web.Http
Imports System.Net.Http

Public Class DashboardController
    Inherits ApiController

    <HttpGet>
    <ActionName("")>
    Public Function [Get]() As IEnumerable(Of String)
        Return New String() {"grafana generic nanosoft response"}
    End Function

    <HttpPost()>
    <ActionName("Search")>
    Public Function Search(request As HttpRequestMessage) As IEnumerable(Of String)

        Dim retobj As New List(Of String)

        'get list of possible tags
        Dim app = Business.DataObjects.Application.GetFromApplicationName("devbox2")

        Dim vehicles = Business.DataObjects.ApplicationVehicle.GetAll(app.ApplicationID)

        For Each av As Business.DataObjects.ApplicationVehicle In vehicles

            For Each x In av.GetAvailableCANTags

                'vehicle>standard>spn>description
                retobj.Add(String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, x.Name))

            Next
        Next

        Return retobj.ToArray ' New String() {"upper_25", "upper_50", "upper_75", "upper_90", "sinusoid"}
    End Function

    <HttpPost()>
   <ActionName("Query")>
    Public Function Query(request As HttpRequestMessage) As IEnumerable(Of queryReturnType)

        Dim jsonString = request.Content.ReadAsStringAsync().Result



        Dim a = New queryReturnType With {.target = "sinusoid"}

        Dim y = {{1, 1504971442000}, {4, 1504971542000}}

        a.datapoints = y

        Return {a}


    End Function

    Public Class queryRequestType

        Public Property panelId As Integer

        Public Property range As Object

        Public Property rangeRaw As Object

        Public Property interval As String

        'intervalMs 

        'getting "how to do this"from 
        'https://github.com/grafana/simple-json-datasource

        Public Sub New()

        End Sub

    End Class

    Public Class queryReturnType

        Public Property target As String


        Public Property datapoints As Long(,)

        Public Sub New()

        End Sub


    End Class


    '    [
    '  {
    '    "target":"upper_75", // The field being queried for
    '    "datapoints":[
    '      [622,1450754160000],  // Metric value as a float , unixtimestamp in milliseconds
    '      [365,1450754220000]
    '    ]
    '  },
    '  {
    '    "target":"upper_90",
    '    "datapoints":[
    '      [861,1450754160000],
    '      [767,1450754220000]
    '    ]
    '  }
    ']



End Class