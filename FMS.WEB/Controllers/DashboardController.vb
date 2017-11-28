Imports System.Net
Imports System.Web.Http
Imports System.Net.Http
Imports System.Runtime.Caching
Imports System.Web.Script.Serialization

Public Class DashboardController
    Inherits ApiController

    <HttpGet>
    <ActionName("")>
    Public Function [Get]() As IEnumerable(Of String)
        Return New String() {"grafana generic nanosoft response"}
    End Function


    Public Class CompanyAndAuthortized

        Public Property CompanyName As String
        Public Property IsAuthorized As Boolean = True

        Public Sub New()
        End Sub
    End Class

    Private Function GetAuthorizedAndCompanyName(authHeader As System.Net.Http.Headers.AuthenticationHeaderValue) As CompanyAndAuthortized


        Dim e = Encoding.UTF8

        Dim encodedUsernamePassword As String = authHeader.Parameter.Trim
        Dim strm = Convert.FromBase64String(encodedUsernamePassword)

        Dim usernameAndPAssword As String = e.GetString(strm)

        Dim seperatorIndx As Integer = usernameAndPAssword.IndexOf(":")

        'The username is splitup as follows username:<<company>>|<<username>>
        Dim usrNameAndCompan() As String = usernameAndPAssword.Substring(0, seperatorIndx).Split("|"c)

        Dim companyName As String = usrNameAndCompan(0)
        Dim userName As String = usrNameAndCompan(1)
        Dim password As String = usernameAndPAssword.Substring(seperatorIndx + 1)

        'Automatically authorize as OK
        Return New CompanyAndAuthortized() With {.CompanyName = companyName, .IsAuthorized = True}

    End Function

    ''' <summary>
    ''' resurns a list of ALL the CANBUS tags which are available.
    ''' </summary>
    ''' <param name="request"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <HttpPost()>
    <ActionName("Search")>
    Public Function Search(request As HttpRequestMessage) As IEnumerable(Of String)


        'we presume at this point in time that the user is authorized
        Dim companyName As String = GetAuthorizedAndCompanyName(request.Headers.Authorization).CompanyName

        Dim policy As New CacheItemPolicy With {.AbsoluteExpiration = Now.AddHours(6)}

        Dim CACHE_NAME = "CACHE_NAME_LIST" & "|" & companyName

        Dim cache_obj As List(Of String) = MemoryCache.Default.Get(CACHE_NAME)

        If cache_obj Is Nothing Then

            Dim new_cache_obj = New List(Of String)

            'get list of possible tags
            Dim app = Business.DataObjects.Application.GetFromApplicationName(companyName)

            Dim vehicles = Business.DataObjects.ApplicationVehicle.GetAll(app.ApplicationID)

            For Each av As Business.DataObjects.ApplicationVehicle In vehicles

                For Each x In av.GetAvailableCANTags

                    'vehicle>standard>spn>description
                    new_cache_obj.Add(String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, x.Name))

                Next
            Next

            MemoryCache.Default.Set(CACHE_NAME, new_cache_obj, New DateTimeOffset(Now.AddHours(1)))

            cache_obj = MemoryCache.Default.Get(CACHE_NAME)
        End If


        Return cache_obj ' New String() {"upper_25", "upper_50", "upper_75", "upper_90", "sinusoid"}
    End Function


    ''' <summary>
    ''' Accepts a list of tag name and return a list of values between a time range
    ''' </summary>
    ''' <param name="request"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <HttpPost()>
   <ActionName("Query")>
    Public Function Query(request As HttpRequestMessage) As Object

        Dim retobj As Object = Nothing

        Dim i As Integer = 0

        Dim jsonString = request.Content.ReadAsStringAsync().Result

        Dim requestHAsh As Integer = (jsonString & request.Headers.Authorization.ToString).GetHashCode

        'if we have already gotten the result previously, then return it from memory this time
        If MemoryCache.Default.Contains(requestHAsh) Then Return MemoryCache.Default.GetCacheItem(requestHAsh).Value


        'we presume at this point in time that the user is authorized
        Dim companyName As String = GetAuthorizedAndCompanyName(request.Headers.Authorization).CompanyName

        Dim application = Business.DataObjects.Application.GetFromApplicationName(companyName)



        Dim jSerializer = New JavaScriptSerializer()

        Dim lok As queryRequestType = jSerializer.Deserialize(Of queryRequestType)(jsonString)

        Dim queryReturnList As New List(Of queryReturnType)

        Dim startTime As Date = CDate(lok.range.from)
        Dim endTime As Date = CDate(lok.range.to)

        'grab the first target to determine if the result set should be timeserie or table
        Dim isTimeserie As Boolean = lok.targets(0).type = "timeserie"

        If isTimeserie Then
            retobj = GetTimeSeriesResult(lok, startTime, endTime)
        Else
            retobj = GetTableResult(lok, startTime, endTime)
        End If

        'add to the cache so we dont have to do this wole calc again next time
        MemoryCache.Default.Add(requestHAsh, retobj, New DateTimeOffset(Now.AddHours(1)), Nothing)

        Return retobj

    End Function




    Public Class table_row

        Public Sub New()

        End Sub

    End Class

    Public Class table_column

        Public Property text As String
        Public Property type As String

        Public Sub New()

        End Sub
    End Class




    Public Function GetTableResult(lok As queryRequestType, startTime As Date, endTime As Date) As Object

        Dim retobj As New table_retObj

        retobj.type = "table"

        retobj.columns = New List(Of table_column)
        retobj.rows = New List(Of table_row)


        retobj.columns.Add(New table_column With {.text = "Time", .type = "time"})
        retobj.columns.Add(New table_column With {.text = "Value", .type = "string"})


        For Each x As cust_target In lok.targets

            Dim qrt = New queryReturnType With {.target = x.target}

            Dim strs() As String = x.target.Split(">")
            Dim vehicleName As String = strs(0)
            Dim standard As String = strs(1)
            Dim spn As Integer = CInt(strs(2))
            Dim spnNAme As String = strs(3)

            Dim canDataPoint = Business.DataObjects.CanDataPoint.GetPointWithData(spn, vehicleName, standard, startTime, endTime)

            Dim cnt As Integer = canDataPoint.CanValues.Count
            Dim i As Integer = 0

            Dim dataPoints As Object(,) = New Object(cnt - 1, 1) {}

            For Each cv In canDataPoint.CanValues

                Try

                    Dim unixTimestamp = CLng(cv.Time.AddHours(-8).Subtract(CDate("01/jan/1970")).TotalSeconds) * 1000
                    Dim val As Object = cv.Value

                    If cv.Time >= startTime AndAlso cv.Time <= endTime Then
                        dataPoints(i, 0) = unixTimestamp
                        dataPoints(i, 1) = val
                    End If

                    i += 1

                Catch ex As Exception

                End Try
            Next

            retobj.rows = dataPoints

        Next

        Return {retobj}

    End Function

    Public Class table_retObj

        Public Property columns As List(Of table_column)

        Public Property rows As Object

        Public Property type As String

        Public Sub New()

        End Sub
    End Class



    Public Function GetTimeSeriesResult(lok As queryRequestType, startTime As Date, endTime As Date) As Object

        Dim retobj As New List(Of queryReturnType)

        For Each x As cust_target In lok.targets

            Dim qrt = New queryReturnType With {.target = x.target}

            'new_cache_obj.Add(String.Format("{0}>{1}>{2}>{3}", av.Name, x.Standard, x.SPN, x.Name))
            Dim strs() As String = x.target.Split(">")
            Dim vehicleName As String = strs(0)
            Dim standard As String = strs(1)
            Dim spn As Integer = CInt(strs(2))
            Dim spnNAme As String = strs(3)

            Dim canDataPoint = Business.DataObjects.CanDataPoint.GetPointWithData(spn, vehicleName, standard, startTime, endTime)

            Dim cnt As Integer = canDataPoint.CanValues.Count

            Dim dataPoints As Long(,) = New Long(cnt - 1, 1) {}

            Dim i As Integer = 0

            For Each cv In canDataPoint.CanValues

                Try
                    'Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds

                    Dim unixTimestamp = CLng(cv.Time.AddHours(-8).Subtract(CDate("01/jan/1970")).TotalSeconds) * 1000
                    Dim val As Long = cv.Value

                    If cv.Time >= startTime AndAlso cv.Time <= endTime Then
                        dataPoints(i, 0) = val
                        dataPoints(i, 1) = unixTimestamp
                    End If

                    i += 1

                Catch ex As Exception

                End Try
            Next

            qrt.datapoints = dataPoints

            retobj.Add(qrt)
        Next


        Return retobj


    End Function


    Public Class cust_range

        Public Property from As String
        Public Property [to] As String

        Public Sub New()

        End Sub
    End Class

    Public Class cust_target

        Public Property target As String
        Public Property refId As String
        Public Property type As String

        Public Sub New()

        End Sub
    End Class

    Public Class queryRequestType

        Public Property panelId As Integer

        ''' <summary>
        ''' between start and end date 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property range As cust_range

        ''' <summary>
        ''' list of tags
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property targets As List(Of cust_target)

        Public Property rangeRaw As Object

        Public Property interval As String

        Public Property intervalMs As Integer

        ''' <summary>
        ''' will accept a string which is either "table" or "timeserie" 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property format As String

        Public Property maxDataPoints As Integer



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



End Class