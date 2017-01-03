

Namespace DataObjects
    Public Class FleetMapMarker

#Region "Properties"

        Public Property ApplicationID As Guid
        Public Property FleetMapMarkerId As Guid
        Public Property Vehicle_ApplicationImageID As Guid
        Public Property Home_ApplicationImageID As Guid

#End Region


#Region "constructors"
        ''' <summary>
        ''' for serialization 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()

        End Sub

        Public Sub New(ad As FMS.Business.FleetMapMarker)
            With ad
                Me.ApplicationID = ad.ApplicationID
                Me.FleetMapMarkerId = ad.FleetMapMarkerId
                Me.Vehicle_ApplicationImageID = If(ad.Vehicle_ApplicationImageId Is Nothing, DataObjects.ApplicationImage.GetDefaultImages("vehicle"), ad.Vehicle_ApplicationImageId)
                Me.Home_ApplicationImageID = If(ad.Home_ApplicationImageId Is Nothing, DataObjects.ApplicationImage.GetDefaultImages("home"), ad.Home_ApplicationImageId)

            End With

        End Sub

#End Region

#Region "CRUD"

        Public Shared Sub Create(ad As DataObjects.FleetMapMarker)

            Dim d As New FMS.Business.FleetMapMarker

            With d
                .FleetMapMarkerId = Guid.NewGuid
                .Vehicle_ApplicationImageId = ad.Vehicle_ApplicationImageID
                .Home_ApplicationImageId = ad.Home_ApplicationImageID
                .ApplicationID = ad.ApplicationID
            End With

            SingletonAccess.FMSDataContextContignous.FleetMapMarkers.InsertOnSubmit(d)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

        End Sub

        Public Shared Sub Update(ad As DataObjects.FleetMapMarker)

            Dim d As FMS.Business.FleetMapMarker = (From i In SingletonAccess.FMSDataContextContignous.FleetMapMarkers
                                                        Where i.FleetMapMarkerId = ad.FleetMapMarkerId).SingleOrDefault
            If (d Is Nothing) Then 'BY RYAN : create if not existing
                Create(ad)
            Else

                With d
                    .Vehicle_ApplicationImageId = ad.Vehicle_ApplicationImageID
                    .Home_ApplicationImageId = ad.Home_ApplicationImageID
                End With

                SingletonAccess.FMSDataContextContignous.SubmitChanges()
            End If

        End Sub

#End Region


#Region "Get methods"


        Public Shared Function GetApplicationFleetMapMarket(applicationid As Guid) As DataObjects.FleetMapMarker

            Dim ctx = New FMS.Business.LINQtoSQLClassesDataContext()

            Using ctx
                Dim ff = ctx.FleetMapMarkers. _
                            Where(Function(y) y.ApplicationId = applicationid).SingleOrDefault

                Dim retval As DataObjects.FleetMapMarker

                If ff IsNot Nothing Then
                    retval = New DataObjects.FleetMapMarker(ff)
                Else

                    retval = New DataObjects.FleetMapMarker()
                    retval.FleetMapMarkerId = Guid.NewGuid
                    retval.ApplicationID = applicationid

                    retval.Vehicle_ApplicationImageID = DataObjects.ApplicationImage.GetDefaultImages("truck")
                    retval.Home_ApplicationImageID = DataObjects.ApplicationImage.GetDefaultImages("home")

                End If
                Return retval
            End Using


        End Function

#End Region

    End Class
End Namespace
