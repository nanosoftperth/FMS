Imports System.ComponentModel
Imports System.Text.RegularExpressions

Public Class PickleProcessor


    Public Event MessageFired(message As String)
    Public Property synccontext As New Threading.SynchronizationContext


    Public Sub New()
        synccontext = AsyncOperationManager.SynchronizationContext
    End Sub

    Private Property main_thread As New System.Threading.Thread(AddressOf ProcessFileThread)


    Public Sub StopAllWorking()
        If main_thread IsNot Nothing Then main_thread.Abort()
    End Sub

    Public Sub ProcessFile(fileLocation As String, URLSyntax As String)

        Dim obj As New PickleParams With {
                            .FileLocation = fileLocation,
                            .URLSyntax = URLSyntax}

        main_thread.Start(obj)

    End Sub

    Private Sub fireevent(msg As String)
        RaiseEvent MessageFired(Now.ToString("HH:mm:ss") & vbTab & msg)
    End Sub

    Private Sub sendMessage(msg As String, ParamArray args() As String)

        Dim retStr As String = String.Format(msg, args)
        sendMessage(retStr)
    End Sub

    Public Sub SendMessage(msg As String)

        synccontext.Post(AddressOf fireevent, String.Format(msg))

    End Sub


    Public Class PickleParams
        Public Property FileLocation As String
        Public Property URLSyntax As String

        ''' <summary>
        ''' serializatoipn purposes only
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub
    End Class

    Private Sub ProcessFileThread(pp As PickleParams)

        Dim fileLocation As String = pp.FileLocation
        Dim URLSyntax As String = pp.URLSyntax

        sendMessage("reading contents of file")
        sendMessage("the file name is: {0}", fileLocation.Split("\").ToList.Last)

        Dim memString As String = System.IO.File.ReadAllText(fileLocation)

        sendMessage("the file size is {0} characters", memString.Length)

        Dim mc As MatchCollection = Regex.Matches(memString, "truckid=[^qX]*")

        Dim i As Integer = 1
        Dim cnt As Integer = mc.Count

        Dim webClient As New System.Net.WebClient

        For Each s As Match In mc

            Dim urlQueryStr As String = s.Value
            Dim url As String = String.Format(pp.URLSyntax, "?" & urlQueryStr)
            Dim result As String

            Try
                result = webClient.DownloadString(url)

            Catch ex As Exception
                result = ex.Message
            End Try

            If result <> """success""" Or i Mod 100 = 0 Then sendMessage("{0:000} of {1:000}: {2}", i, cnt, result)

            i += 1

        Next


        sendMessage("processing complete")


    End Sub



End Class
