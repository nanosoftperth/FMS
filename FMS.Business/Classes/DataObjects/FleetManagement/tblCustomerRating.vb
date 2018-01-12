﻿Namespace DataObjects
    Public Class tblCustomerRating
#Region "Properties / enums"
        Public Property ApplicationId As System.Guid
        Public Property CustomerRatingID As System.Guid
        Public Property Rid As Integer
        Public Property CustomerRating As String
        Public Property CustomerRatingDesc As String
        Public Property FromValue As System.Nullable(Of Integer)
        Public Property ToValue As System.Nullable(Of Integer)
#End Region
#Region "CRUD"
        Public Shared Sub Create(CustomerRate As DataObjects.tblCustomerRating)
            Dim appId = ThisSession.ApplicationID
            Dim objCustomerRating As New FMS.Business.tblCustomerRating
            With objCustomerRating
                .ApplicationId = appId
                .CustomerRatingID = Guid.NewGuid
                .Rid = tblProjectID.CustomerRatingIDCreateOrUpdate()
                .CustomerRating = CustomerRate.CustomerRating
                .CustomerRatingDesc = CustomerRate.CustomerRatingDesc
                .FromValue = CustomerRate.FromValue
                .ToValue = CustomerRate.ToValue
            End With
            SingletonAccess.FMSDataContextContignous.tblCustomerRatings.InsertOnSubmit(objCustomerRating)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(CustomerRate As DataObjects.tblCustomerRating)
            Dim appId = ThisSession.ApplicationID
            Dim objCustomerRating As FMS.Business.tblCustomerRating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                                                                       Where c.CustomerRatingID.Equals(CustomerRate.CustomerRatingID) And c.ApplicationId = appId).SingleOrDefault
            With objCustomerRating
                .CustomerRating = CustomerRate.CustomerRating
                .CustomerRatingDesc = CustomerRate.CustomerRatingDesc
                .FromValue = CustomerRate.FromValue
                .ToValue = CustomerRate.ToValue
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(CustomerRate As DataObjects.tblCustomerRating)
            Dim appId = ThisSession.ApplicationID
            Dim objCustomerRating As FMS.Business.tblCustomerRating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                                                                       Where c.CustomerRatingID.Equals(CustomerRate.CustomerRatingID) And c.ApplicationId = appId).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblCustomerRatings.DeleteOnSubmit(objCustomerRating)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region
#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblCustomerRating)
            Dim objCustomerRating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                                     Order By c.CustomerRatingDesc
                                     Select New DataObjects.tblCustomerRating(c)).ToList
            Return objCustomerRating
        End Function

        Public Shared Function GetAllPerApplication() As List(Of DataObjects.tblCustomerRating)
            Dim appId = ThisSession.ApplicationID
            Dim objCustomerRating = (From c In SingletonAccess.FMSDataContextContignous.tblCustomerRatings
                                     Where c.ApplicationId = appId
                                     Order By c.CustomerRatingDesc
                                     Select New DataObjects.tblCustomerRating(c)).ToList
            Return objCustomerRating
        End Function
#End Region
#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(objTblCustomerRating As FMS.Business.tblCustomerRating)
            With objTblCustomerRating
                Me.ApplicationId = .ApplicationId
                Me.CustomerRatingID = .CustomerRatingID
                Me.Rid = .Rid
                Me.CustomerRating = .CustomerRating
                Me.CustomerRatingDesc = .CustomerRatingDesc
                Me.FromValue = .FromValue
                Me.ToValue = .ToValue
            End With
        End Sub
#End Region
    End Class
End Namespace
