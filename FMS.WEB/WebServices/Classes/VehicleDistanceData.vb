Imports System.Runtime.Serialization

Namespace WebServices

    <DataContract()>
    Public Class VehicleDistanceData


        <DataMember()>
        Public Property ReturnMessage As String

        <DataMember()>
        Public Property WasSuccess As Boolean = False

        <DataMember()>
        Public Property WasAuthenticated As Boolean

        <DataMember()>
        Public Property LastOdometerReading As OdometerReading

        <DataMember()>
        Public Property DistanceSinceLastOdometerReading_km As Decimal

        <DataMember()>
        Public Property EnginehoursOn As TimeSpan

        <DataMember()>
        Public Property CalcluatedDistance As Decimal

        ''' <summary>
        ''' required for serialization purposes 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

    End Class

    <DataContract()>
    Public Class OdometerReading

        <DataMember()>
        Public Property Kilometers As Decimal

        <DataMember()>
         Public Property DateOfReading As Date

        Public Sub New()

        End Sub
    End Class

End Namespace

