Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Web.UI
Imports DevExpress.Web

Namespace DemosExport
	Public Enum DemoExportFormat
		Pdf
		Xls
		Xlsx
		Rtf
		Csv
	End Enum

    Public Class ToolbarExport
        Inherits UserControl
        Private itemIcons_Renamed As Dictionary(Of DemoExportFormat, String)
        Private exportItemTypes_Renamed() As DemoExportFormat
        Private Shared ReadOnly EventItemClick As Object = New Object()
        Public Delegate Sub ExportItemClickEventHandler(ByVal source As Object, ByVal e As ExportItemClickEventArgs)

        <TypeConverter(GetType(EnumConverter))> _
        Public Property ExportItemTypes() As DemoExportFormat()
            Get
                If exportItemTypes_Renamed Is Nothing Then
                    exportItemTypes_Renamed = System.Enum.GetValues(GetType(DemoExportFormat)).Cast(Of DemoExportFormat)().ToArray()
                End If
                Return exportItemTypes_Renamed
            End Get
            Set(ByVal value As DemoExportFormat())
                exportItemTypes_Renamed = value
            End Set
        End Property
        Public Property IsDataAwareXls() As Boolean
        Public Property IsDataAwareXlsx() As Boolean
        Public Custom Event ItemClick As ExportItemClickEventHandler
            AddHandler(ByVal value As ExportItemClickEventHandler)
                Events.AddHandler(EventItemClick, value)
            End AddHandler
            RemoveHandler(ByVal value As ExportItemClickEventHandler)
                Events.RemoveHandler(EventItemClick, value)
            End RemoveHandler
            RaiseEvent(ByVal source As Object, ByVal e As ExportItemClickEventArgs)
            End RaiseEvent
        End Event
        Private ReadOnly Property ItemIcons() As Dictionary(Of DemoExportFormat, String)
            Get
                If itemIcons_Renamed Is Nothing Then
                    itemIcons_Renamed = New Dictionary(Of DemoExportFormat, String)()
                    FillItemIcons()
                End If
                Return itemIcons_Renamed
            End Get
        End Property
        Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs)
            For Each type In ExportItemTypes
                CreateMenuItem(type)
            Next type
        End Sub
        Private Sub CreateMenuItem(ByVal type As DemoExportFormat)
            Dim item = New MenuItem(String.Empty, type.ToString())
            'MenuExportButtons.Items.Add(item)
            If ItemIcons.ContainsKey(type) Then
                item.Image.IconID = ItemIcons(type)
            End If
            item.ToolTip = GetItemToolTip(type)
        End Sub
        Private Function GetItemToolTip(ByVal type As DemoExportFormat) As String
            Dim result = "Export to " & type.ToString()
            If (IsDataAwareXls AndAlso type = DemoExportFormat.Xls) OrElse (IsDataAwareXlsx AndAlso type = DemoExportFormat.Xlsx) Then
                result &= " (DataAware)"
            End If
            Return result
        End Function
        Private Sub FillItemIcons()
            ItemIcons(DemoExportFormat.Pdf) = "export_exporttopdf_32x32"
            ItemIcons(DemoExportFormat.Xls) = "export_exporttoxls_32x32"
            ItemIcons(DemoExportFormat.Xlsx) = "export_exporttoxlsx_32x32"
            ItemIcons(DemoExportFormat.Rtf) = "export_exporttortf_32x32"
            ItemIcons(DemoExportFormat.Csv) = "export_exporttocsv_32x32"
        End Sub
        Protected Sub MenuExportButtons_ItemClick(ByVal source As Object, ByVal e As MenuItemEventArgs)
            Dim handler = CType(Events(EventItemClick), ExportItemClickEventHandler)
            If handler IsNot Nothing Then
                handler(Me, New ExportItemClickEventArgs(CType(System.Enum.Parse(GetType(DemoExportFormat), e.Item.Name), DemoExportFormat)))
            End If
        End Sub
    End Class
	Public Class ItemTooltips
		Inherits Collection(Of ItemTooltip)
		Public Sub New()
			MyBase.New()
		End Sub
	End Class
	Public Class ItemTooltip
		Inherits CollectionItem
		Public Sub New()
		End Sub
		Public Sub New(ByVal type As DemoExportFormat, ByVal toolTip As String)
			Type = type
			ToolTip = toolTip
		End Sub
		Public Property Type() As DemoExportFormat
		Public Property ToolTip() As String
	End Class

	Public Class ExportItemClickEventArgs
		Inherits EventArgs
		Public Sub New(ByVal type As DemoExportFormat)
			ExportType = type
		End Sub
		Public Property ExportType() As DemoExportFormat
	End Class
	Public Class EnumConverter
		Inherits StringToObjectTypeConverter
		Public Overrides Function ConvertFrom(ByVal context As ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
			Dim items = value.ToString().Split(","c)
			Dim result = New DemoExportFormat(items.Length - 1){}
			For i = 0 To items.Length - 1
				System.Enum.TryParse(items(i), result(i))
			Next i
			Return result
		End Function
	End Class
End Namespace
