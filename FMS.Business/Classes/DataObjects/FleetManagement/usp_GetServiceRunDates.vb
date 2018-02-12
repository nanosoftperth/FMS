﻿Namespace DataObjects

    Public Class usp_GetServiceRunDates

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property DriverName As String
        Public Property Did As Integer
        Public Property DateOfRun As Date
        Public Property wkday As String
        Public Property Rid As Integer
        Public Property RunNUmber As Integer
        Public Property RunDescription As String
        Public Property MondayRun As Boolean
        Public Property TuesdayRun As Boolean
        Public Property WednesdayRun As Boolean
        Public Property ThursdayRun As Boolean
        Public Property FridayRun As Boolean
        Public Property SaturdayRun As Boolean
        Public Property SundayRun As Boolean
        Public Property InactiveRun As Boolean
        Public Property DriverID As Guid
        Public Property RunID As Guid

#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetServiceRunDatesResult)
            With obj
                Me.ApplicationId = .ApplicationId
                Me.DriverName = .DriverName
                Me.Did = .Did
                Me.DateOfRun = .DateOfRun
                Me.wkday = .wkday
                Me.Rid = .Rid
                Me.RunNUmber = .RunNUmber
                Me.RunDescription = .RunDescription
                Me.MondayRun = .MondayRun
                Me.TuesdayRun = .TuesdayRun
                Me.WednesdayRun = .WednesdayRun
                Me.ThursdayRun = .ThursdayRun
                Me.FridayRun = .FridayRun
                Me.SaturdayRun = .SaturdayRun
                Me.DriverID = .DriverID
                Me.RunID = .RunID
            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllPerApplication(StartDate As Date, EndDate As Date) As List(Of DataObjects.usp_GetServiceRunDates)
            Try
                Dim appId = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.usp_GetServiceRunDates)

                With New LINQtoSQLClassesDataContext
                    obj = (From s In .usp_GetServiceRunDates(appId, StartDate, EndDate)
                           Order By s.Did, s.DriverName
                           Select New DataObjects.usp_GetServiceRunDates(s)).ToList
                    .Dispose()
                End With

                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function

        Public Shared Function GetAllPerApplicationAndDistinctDriverName(StartDate As Date, EndDate As Date) As List(Of Drivers)
            Try
                Dim appId = ThisSession.ApplicationID
                Dim obj As New List(Of Drivers)
                Dim DriverList As New List(Of Drivers)

                With New LINQtoSQLClassesDataContext
                    obj = (From s In .usp_GetServiceRunDates(appId, StartDate, EndDate)
                           Order By s.Did, s.DriverName
                           Select s.Did, s.DriverName).ToList.GroupBy(Function(x) x.DriverName)

                    .Dispose()

                End With

                If (obj.Count > 0) Then

                    'Dim ctr As Integer = 0

                    'For Each g In obj

                    '    Dim Row As New Drivers

                    '    Row.id = g(0).Did
                    '    Row.Name = g(0).DriverName

                    '    DriverList.Add(Row)

                    'Next

                End If



                Return DriverList
            Catch ex As Exception
                Throw ex
            End Try



        End Function
#End Region

        Public Class Drivers
            Public Property id As Integer
            Public Property Name As String


        End Class

    End Class

End Namespace

