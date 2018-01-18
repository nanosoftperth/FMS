Namespace DataObjects

    Public Class usp_GetServiceRunDates

#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property Did As Integer
        Public Property DriverName As String
        Public Property DRid As Integer
        Public Property DateOfRun As Date
        Public Property RunNUmber As Integer
        Public Property RunDescription As String
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.usp_GetServiceRunDatesResult)
            With obj
                Me.ApplicationId = .ApplicationId
                Me.Did = .Did
                Me.DriverName = .DriverName
                Me.DRid = .DRid
                Me.DateOfRun = .DateOfRun
                Me.RunNUmber = .RunNUmber
                Me.RunDescription = .RunDescription
            End With
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAllPerApplication(StartDate As Date, EndDate As Date) As List(Of DataObjects.usp_GetServiceRunDates)
            Dim appId = ThisSession.ApplicationID
            Dim obj = (From s In SingletonAccess.FMSDataContextContignous.usp_GetServiceRunDates(appId, StartDate, EndDate)
                       Order By s.Did, s.DriverName
                       Select New DataObjects.usp_GetServiceRunDates(s)).ToList
            Return obj
        End Function
        Public Shared Function GetAllPerApplicationAndDistinctDriverName(StartDate As Date, EndDate As Date) As List(Of Drivers)
            Dim appId = ThisSession.ApplicationID

            Dim obj = (From s In SingletonAccess.FMSDataContextContignous.usp_GetServiceRunDates(appId, StartDate, EndDate)
                       Order By s.Did, s.DriverName
                       Select s.Did, s.DriverName).ToList.GroupBy(Function(x) x.DriverName)

            Dim DriverList As New List(Of Drivers)

            If (obj.Count > 0) Then

                Dim ctr As Integer = 0

                For Each g In obj

                    Dim Row As New Drivers

                    Row.id = g(0).Did
                    Row.Name = g(0).DriverName

                    DriverList.Add(Row)

                Next

            End If





            'Dim obj = (From s In SingletonAccess.FMSDataContextContignous.usp_GetServiceRunDates(appId).AsEnumerable()
            '           Order By s.Did, s.DriverName
            '           Select s.Did, s.DriverName).Distinct().ToList()

            'Dim obj = (From s In SingletonAccess.FMSDataContextContignous.usp_GetServiceRunDates(appId)
            '           Select s.Did, s.DriverName)
            'Distinct


            'Dim customerOrders = From cust In customers, ord In orders
            '                     Where cust.CustomerID = ord.CustomerID
            '                     Select cust.CompanyName, ord.OrderDate
            '                     Distinct

            'Dim DriverList As New List(Of Drivers)

            'For Each d In obj
            '    Dim row As New Drivers

            '    row.id = 

            'Next

            Return DriverList

        End Function
#End Region

        Public Class Drivers
            Public Property id As Integer
            Public Property Name As String


        End Class

    End Class

End Namespace

