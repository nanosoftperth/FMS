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
            With New LINQtoSQLClassesDataContext
                Dim AppID = ThisSession.ApplicationID
                Dim obj As New FMS.Business.tblPublicHolidayRegister

                With obj
                    .ApplicationId = AppID
                    .PublicHolidayDate = holidays.PublicHolidayDate
                    .PublicHolidayDescription = holidays.PublicHolidayDescription

                End With

                .tblPublicHolidayRegisters.InsertOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Update(holidays As DataObjects.tblPublicHolidayRegister)
            With New LINQtoSQLClassesDataContext
                Dim AppID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblPublicHolidayRegister = (From h In .tblPublicHolidayRegisters
                                                                    Where h.Aid.Equals(holidays.Aid) And h.ApplicationId = AppID).SingleOrDefault
                With obj
                    .PublicHolidayDate = holidays.PublicHolidayDate
                    .PublicHolidayDescription = holidays.PublicHolidayDescription
                End With

                .SubmitChanges()
                .Dispose()

            End With

        End Sub
        Public Shared Sub Delete(holidays As DataObjects.tblPublicHolidayRegister)
            With New LINQtoSQLClassesDataContext
                Dim AppID = ThisSession.ApplicationID
                Dim obj As FMS.Business.tblPublicHolidayRegister = (From h In .tblPublicHolidayRegisters
                                                                    Where h.Aid.Equals(holidays.Aid) And h.ApplicationId = AppID).SingleOrDefault
                .tblPublicHolidayRegisters.DeleteOnSubmit(obj)
                .SubmitChanges()
                .Dispose()

            End With

        End Sub
#End Region

#Region "Get methods"
        Public Shared Function GetAll() As List(Of DataObjects.tblPublicHolidayRegister)
            Try
                Dim AppID = ThisSession.ApplicationID
                Dim obj As New List(Of DataObjects.tblPublicHolidayRegister)

                With New LINQtoSQLClassesDataContext
                    obj = (From h In .tblPublicHolidayRegisters
                           Where h.ApplicationId = AppID
                           Order By h.PublicHolidayDate
                           Select New DataObjects.tblPublicHolidayRegister(h)).ToList
                    .Dispose()

                End With
                Return obj

            Catch ex As Exception
                Throw ex
            End Try

        End Function
#End Region

    End Class

End Namespace


