Imports System.Runtime.Serialization

Namespace WebServices

    <DataContract()>
    Public Class TokenResponse

        <DataMember()>
        Public Property TokenID As Guid

        <DataMember()>
        Public Property SuccessLogin As Boolean

        <DataMember()>
        Public Property ErrorMessage As String

        <DataMember()>
        Public Property URL As String

        Public Sub New()

        End Sub

    End Class

    <DataContract()>
    Public Class VINNumberRequest

        <DataMember()>
        Public Property VINNumber As String

        <DataMember()>
        Public Property StartDate As DateTime?

        <DataMember()>
        Public Property EndDate As DateTime?


        Public Sub New()

        End Sub

    End Class

    <DataContract()>
    Public Class GetVehicleData_Response

        <DataMember()>
        Public Property ReturnMessage As String

        <DataMember()>
        Public Property WasSuccess As Boolean = False

        <DataMember()>
        Public Property WasAuthenticated As Boolean

        <DataMember()>
        Public Property VINNumberResponses As New List(Of VINNumberResponse)

        ''' <summary>
        ''' for serialization purposed only.
        ''' </summary>
        Public Sub New()

        End Sub

    End Class

    <DataContract()>
        Public Class VINNumberResponse

        <DataMember()>
        Public Property ErrorMessage As String
        <DataMember()>
        Public Property WasError As String
        <DataMember()>
        Public Property VINNumber As String
        <DataMember()>
        Public Property DailyReadings As New List(Of DailyReading)

        Public Sub New()

        End Sub

    End Class

    <DataContract()>
    Public Class DailyReading

        <DataMember()>
        Public Property ReadingDate As DateTime

        <DataMember()>
        Public Property Enginehours As Double

        <DataMember()>
        Public Property Kilometers As Decimal


        Public Sub New()

        End Sub

    End Class

End Namespace

