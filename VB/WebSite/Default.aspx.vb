Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxGridView
Imports DevExpress.Web.ASPxEditors

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub dsProducts_Init(ByVal sender As Object, ByVal e As EventArgs)
		Dim dataSource As AccessDataSource = TryCast(sender, AccessDataSource)
		Dim container As GridViewDetailRowTemplateContainer = TryCast(dataSource.NamingContainer, GridViewDetailRowTemplateContainer)

		dataSource.SelectParameters(0).DefaultValue = container.KeyValue.ToString()
	End Sub

	Protected Sub gvProducts_AfterPerformCallback(ByVal sender As Object, ByVal e As ASPxGridViewAfterPerformCallbackEventArgs)
		Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)
		Dim sessionField As String = String.Format("{0}_ColumnsOrder", gridView.ID)

		Dim isColumnMoved As Boolean = e.CallbackName = "COLUMNMOVE"

		If isColumnMoved Then
			Session(sessionField) = ColumnState.GetState(gridView.Columns)

		End If

		gridView.JSProperties("cpIsColumnMoved") = isColumnMoved
	End Sub

	Protected Sub gvProducts_Load(ByVal sender As Object, ByVal e As EventArgs)
		Dim gridView As ASPxGridView = TryCast(sender, ASPxGridView)
		Dim container As GridViewDetailRowTemplateContainer = TryCast(gridView.NamingContainer, GridViewDetailRowTemplateContainer)
		Dim sessionField As String = String.Format("{0}_ColumnsOrder", gridView.ID)

		If Session(sessionField) IsNot Nothing Then
			ColumnState.LoadState(gridView.Columns, CType(Session(sessionField), ColumnState()))
		End If

		gridView.ClientInstanceName = String.Format("{0}_{1}", gridView.ID, container.KeyValue)
		gridView.ClientSideEvents.EndCallback = String.Format("function(s, e) {{ if (s.cpIsColumnMoved) {0}.PerformCallback(); }}", container.Grid.ClientInstanceName)
	End Sub
	Protected Sub hlColCust_Load(ByVal sender As Object, ByVal e As EventArgs)
		Dim link As ASPxHyperLink = TryCast(sender, ASPxHyperLink)
		Dim container As GridViewTitleTemplateContainer = TryCast(link.NamingContainer, GridViewTitleTemplateContainer)

		link.ClientSideEvents.Click = String.Format("function(s, e) {{ {0}.ShowCustomizationWindow(s.GetMainElement()); }}", container.Grid.ClientInstanceName)
	End Sub

	Private Class ColumnState
		Private visible_Renamed As Boolean
		Private visibleIndex_Renamed As Integer

		Public Sub New()
			visible_Renamed = False
			visibleIndex_Renamed = -1
		End Sub

		Public Property Visible() As Boolean
			Get
				Return visible_Renamed
			End Get
			Set(ByVal value As Boolean)
				visible_Renamed = value
			End Set
		End Property

		Public Property VisibleIndex() As Integer
			Get
				Return visibleIndex_Renamed
			End Get
			Set(ByVal value As Integer)
				visibleIndex_Renamed = value
			End Set
		End Property

		Public Shared Sub LoadState(ByVal columns As GridViewColumnCollection, ByVal state() As ColumnState)
			For i As Integer = 0 To state.Length - 1
				columns(i).VisibleIndex = state(i).VisibleIndex
				columns(i).Visible = state(i).Visible
			Next i
		End Sub

		Public Shared Function GetState(ByVal columns As GridViewColumnCollection) As ColumnState()
			Dim state(columns.Count - 1) As ColumnState
		For i As Integer = 0 To columns.Count - 1
			state(i) = New ColumnState()
			state(i).Visible = columns(i).Visible
			state(i).VisibleIndex = columns(i).VisibleIndex
		Next i
		Return state
		End Function
	End Class
End Class
