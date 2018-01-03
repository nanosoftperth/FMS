Namespace DataObjects
    Public Class tblPublicHolidayRegister

#Region "Properties / enums"
        Public Property ApplicationId As Guid
        Public Property Aid As System.Nullable(Of Integer)
        Public Property PublicHolidayDate As System.Nullable(Of Date)
        Public Property PublicHolidayDescription As String
        
#End Region

#Region "Constructors"
        Public Sub New()

        End Sub
        Public Sub New(obj As FMS.Business.tblPublicHolidayRegister)
            With obj
                Me.Aid = .Aid
                Me.ApplicationId = .ApplicationId
                Me.PublicHolidayDate = .PublicHolidayDate
                Me.PublicHolidayDescription = .PublicHolidayDescription
                
            End With
        End Sub
#End Region

#Region "CRUD"
        Public Shared Sub Create(holidays As DataObjects.tblPublicHolidayRegister)
            Dim AppID = ThisSession.ApplicationID

            Dim obj As New FMS.Business.tblPublicHolidayRegister
            With obj
                .ApplicationId = AppID
                .PublicHolidayDate = holidays.PublicHolidayDate
                .PublicHolidayDescription = holidays.PublicHolidayDescription

            End With
            SingletonAccess.FMSDataContextContignous.tblPublicHolidayRegisters.InsertOnSubmit(obj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Update(holidays As DataObjects.tblPublicHolidayRegister)
            Dim AppID = ThisSession.ApplicationID

            Dim obj As FMS.Business.tblPublicHolidayRegister = (From h In SingletonAccess.FMSDataContextContignous.tblPublicHolidayRegisters
                                                           Where h.Aid.Equals(holidays.Aid) And h.ApplicationId = AppID).SingleOrDefault
            With obj
                .PublicHolidayDate = holidays.PublicHolidayDate
                .PublicHolidayDescription = holidays.PublicHolidayDescription
            End With
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
        Public Shared Sub Delete(holidays As DataObjects.tblPublicHolidayRegister)
            Dim AppID = ThisSession.ApplicationID

            Dim obj As FMS.Business.tblPublicHolidayRegister = (From h In SingletonAccess.FMSDataContextContignous.tblPublicHolidayRegisters
                                                           Where h.Aid.Equals(holidays.Aid) And h.ApplicationId = AppID).SingleOrDefault
            SingletonAccess.FMSDataContextContignous.tblPublicHolidayRegisters.DeleteOnSubmit(obj)
            SingletonAccess.FMSDataContextContignous.SubmitChanges()
        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblPublicHolidayRegister)
            Dim AppID = ThisSession.ApplicationID

            Dim obj = (From h In SingletonAccess.FMSDataContextContignous.tblPublicHolidayRegisters
                        Where h.ApplicationId = AppID
                        Order By h.PublicHolidayDate
                        Select New DataObjects.tblPublicHolidayRegister(h)).ToList

            Return obj
        End Function
#End Region

    End Class

End Namespace


