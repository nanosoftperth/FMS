Namespace DataObjects


    Public Class Feature

        Public Property Name As String
        Public Property FeatureID As Guid
        Public Property Description As String


        Public ReadOnly Property BitWiseID As Integer
            Get

                Select Case Name

                    Case FeatureListConstants.Fleet_Map
                        Return FeatureListConstants.FeatureListAccess.Fleet_Map

                    Case FeatureListConstants.Fleet_Map__GeoFence_Edit
                        Return FeatureListConstants.FeatureListAccess.Fleet_Map__GeoFence_Edit

                    Case FeatureListConstants.GeoFence_and_Alerts
                        Return FeatureListConstants.FeatureListAccess.GeoFence_and_Alerts

                    Case FeatureListConstants.Home_Page
                        Return FeatureListConstants.FeatureListAccess.Home_Page

                    Case FeatureListConstants.Report_Generator
                        Return FeatureListConstants.FeatureListAccess.Report_Generator

                    Case FeatureListConstants.User_Management__Edit__Application_Settings
                        Return FeatureListConstants.FeatureListAccess.User_Management__Edit__Application_Settings

                    Case FeatureListConstants.User_Management__Edit__Roles
                        Return FeatureListConstants.FeatureListAccess.User_Management__Edit__Roles

                    Case FeatureListConstants.User_Management__Edit__Roles_Access_to_Features
                        Return FeatureListConstants.FeatureListAccess.User_Management__Edit__Roles_Access_to_Features

                    Case FeatureListConstants.User_Management__Edit__Users
                        Return FeatureListConstants.FeatureListAccess.User_Management__Edit__Users

                    Case FeatureListConstants.User_Management__Read_Only
                        Return FeatureListConstants.FeatureListAccess.User_Management__Read_Only

                    Case FeatureListConstants.Vehicle_and_Driver_Management__Edit__Assign_Drivers_to_Vehicles
                        Return FeatureListConstants.FeatureListAccess.Vehicle_and_Driver_Management__Edit__Assign_Drivers_to_Vehicles


                    Case FeatureListConstants.Vehicle_and_Driver_Management__Edit__Drivers
                        Return FeatureListConstants.FeatureListAccess.Vehicle_and_Driver_Management__Edit__Drivers

                    Case FeatureListConstants.Vehicle_and_Driver_Management__Edit__Vehicles
                        Return FeatureListConstants.FeatureListAccess.Vehicle_and_Driver_Management__Edit__Vehicles

                    Case FeatureListConstants.Vehicle_and_Driver_Management__Read_Only
                        Return FeatureListConstants.FeatureListAccess.Vehicle_and_Driver_Management__Read_Only

                    Case FeatureListConstants.Contact_Management__Edit
                        Return FeatureListConstants.FeatureListAccess.Contact_Management__Edit

                    Case FeatureListConstants.Contact_Management__View
                        Return FeatureListConstants.FeatureListAccess.Contact_Management__View

                    Case Else
                        Return 0

                End Select

            End Get
        End Property

#Region "constructors"
        Public Sub New()

        End Sub

        Public Sub New(name As String, featureid As Guid, desc As String)
            Me.Name = name
            Me.FeatureID = featureid
            Me.Description = desc
        End Sub

        Public Sub New(f As FMS.Business.Feature)

            With Me
                .Description = f.FeatureDescription
                .FeatureID = f.FeatureID
                .Name = f.FeatureName
            End With

        End Sub

#End Region

        Public Shared Function InsertNewFeature(f As DataObjects.Feature) As Guid

            Dim basef As New FMS.Business.Feature

            basef.FeatureDescription = f.Description
            basef.FeatureName = f.Name
            basef.FeatureID = Guid.NewGuid

            SingletonAccess.FMSDataContextContignous.Features.InsertOnSubmit(basef)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()

            'return the GUID id from the SQL datasource itself.
            Return (From x In SingletonAccess.FMSDataContextContignous.Features
                    Where x.FeatureID = basef.FeatureID).Single.FeatureID

        End Function

        Public Shared Function GetAllFeatures() As List(Of Feature)

            Return (From i In SingletonAccess.FMSDataContextNew.Features
                     Select New DataObjects.Feature(i)).ToList

        End Function


    End Class


    Public Class FeatureListConstants

        Public Const Fleet_Map As String = "Fleet Map"
        Public Const Home_Page As String = "Home Page"
        Public Const Report_Generator As String = "Report Generator"
        Public Const User_Management__Read_Only As String = "User Management - Read Only"
        Public Const User_Management__Edit__Roles_Access_to_Features As String = "User Management - Edit - Roles Access to Features"
        Public Const Vehicle_and_Driver_Management__Edit__Drivers As String = "Vehicle and Driver Management - Edit - Drivers"
        Public Const Vehicle_and_Driver_Management__Edit__Assign_Drivers_to_Vehicles As String = "Vehicle and Driver Management - Edit - Assign Drivers to Vehicles"
        Public Const User_Management__Edit__Application_Settings As String = "User Management - Edit - Application Settings"
        Public Const GeoFence_and_Alerts As String = "Geo-Fence and Alerts"
        Public Const User_Management__Edit__Roles As String = "User Management - Edit - Roles"
        Public Const Fleet_Map__GeoFence_Edit As String = "Fleet Map - GeoFence Edit"
        Public Const Vehicle_and_Driver_Management__Read_Only As String = "Vehicle and Driver Management - Read Only"
        Public Const Vehicle_and_Driver_Management__Edit__Vehicles As String = "Vehicle and Driver Management - Edit - Vehicles"
        Public Const User_Management__Edit__Users As String = "User Management - Edit - Users"
        Public Const Contact_Management__Edit As String = "Contact Management - Edit"
        Public Const Contact_Management__View As String = "Contact Management - View"


        Public Enum FeatureListAccess
            Fleet_Map = 1
            Home_Page = 2
            Report_Generator = 4
            User_Management__Read_Only = 8
            User_Management__Edit__Roles_Access_to_Features = 16
            Vehicle_and_Driver_Management__Edit__Drivers = 32
            Vehicle_and_Driver_Management__Edit__Assign_Drivers_to_Vehicles = 64
            User_Management__Edit__Application_Settings = 128
            GeoFence_and_Alerts = 256
            User_Management__Edit__Roles = 512
            Fleet_Map__GeoFence_Edit = 1024
            Vehicle_and_Driver_Management__Read_Only = 2048
            Vehicle_and_Driver_Management__Edit__Vehicles = 4069
            User_Management__Edit__Users = 8192
            Contact_Management__View = 16384
            Contact_Management__Edit = 32768
        End Enum

    End Class

End Namespace
