Imports FMS.Business


Public Class TimeZoneHelper

    

    '#Region "AltertoHQTimeZone with all overloads"

    '    Public Shared Sub AltertoHQTimeZone(x As List(Of ReportGeneration.GeoFenceReport_Simple))

    '        For Each o In x
    '            ConvertToHQTime(o.StartTime)
    '            ConvertToHQTime(o.EndTime)
    '        Next

    '    End Sub

    '    Public Shared Sub AltertoHQTimeZone(ByRef o As ClientSide_GeoFenceReport_ByDriver)

    '        ConvertToHQTime(o.TimeFrom)
    '        ConvertToHQTime(o.TimeTo)

    '        For Each r In o.ReportLines
    '            ConvertToHQTime(r.EndTime)
    '            ConvertToHQTime(r.StartTime)
    '        Next

    '    End Sub

    '    Public Shared Sub AltertoHQTimeZone(ByRef cvr As CachedVehicleReport)

    '        ConvertToHQTime(cvr.EndDate)
    '        ConvertToHQTime(cvr.StartDate)

    '        For Each lv In cvr.LineValies

    '            ConvertToHQTime(lv.ArrivalTime)
    '            ConvertToHQTime(lv.DepartureTime)
    '            ConvertToHQTime(lv.StartTime)
    '        Next

    '    End Sub

    '    Public Shared Sub AlterToHQTimeZone(ByRef retobj As FMS.Business.ClientServerRoundRobin_ReturnObject)

    '        ConvertToHQTime(retobj.queryDate)

    '        For Each o In retobj.Trucks
    '            Try
    '                Dim d As Date = CDate(o.time)
    '                ConvertToHQTime(d)
    '                o.time = d.ToString
    '            Catch ex As Exception : End Try
    '        Next

    '    End Sub
    '#End Region

    '    Public Shared Function GeoFenceReport_Simple_GetReport(appid As Guid,
    '                                                            startdate As Date,
    '                                                            enddate As Date) As List(Of ReportGeneration.GeoFenceReport_Simple)

    '        ConvertToPerthTime(startdate)
    '        ConvertToPerthTime(enddate)

    '        Dim retobj = ReportGeneration.GeoFenceReport_Simple.GetReport(appid, startdate, enddate)

    '        AltertoHQTimeZone(retobj)

    '        Return retobj

    '    End Function



End Class
