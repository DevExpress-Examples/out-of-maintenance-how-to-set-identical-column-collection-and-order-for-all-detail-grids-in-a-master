using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxEditors;

public partial class _Default:System.Web.UI.Page {
    protected void dsProducts_Init(object sender, EventArgs e) {
        AccessDataSource dataSource = sender as AccessDataSource;
        GridViewDetailRowTemplateContainer container = dataSource.NamingContainer as GridViewDetailRowTemplateContainer;

        dataSource.SelectParameters[0].DefaultValue = container.KeyValue.ToString();
    }

    protected void gvProducts_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e) {
        ASPxGridView gridView = sender as ASPxGridView;
        string sessionField = String.Format("{0}_ColumnsOrder", gridView.ID);

        bool isColumnMoved = e.CallbackName == "COLUMNMOVE";
        
        if (isColumnMoved) {
            Session[sessionField] = ColumnState.GetState(gridView.Columns);

        }

        gridView.JSProperties["cpIsColumnMoved"] = isColumnMoved;
    }

    protected void gvProducts_Load(object sender, EventArgs e) {
        ASPxGridView gridView = sender as ASPxGridView;
        GridViewDetailRowTemplateContainer container = gridView.NamingContainer as GridViewDetailRowTemplateContainer;
        string sessionField = String.Format("{0}_ColumnsOrder", gridView.ID);

        if (Session[sessionField] != null) {
            ColumnState.LoadState(gridView.Columns, (ColumnState[])Session[sessionField]);
        }

        gridView.ClientInstanceName = String.Format("{0}_{1}", gridView.ID, container.KeyValue);
        gridView.ClientSideEvents.EndCallback = String.Format("function(s, e) {{ if (s.cpIsColumnMoved) {0}.PerformCallback(); }}", container.Grid.ClientInstanceName);
    }
    protected void hlColCust_Load(object sender, EventArgs e) {
        ASPxHyperLink link = sender as ASPxHyperLink;
        GridViewTitleTemplateContainer container = link.NamingContainer as GridViewTitleTemplateContainer;

        link.ClientSideEvents.Click = String.Format("function(s, e) {{ {0}.ShowCustomizationWindow(s.GetMainElement()); }}", container.Grid.ClientInstanceName);
    }

    class ColumnState {
        bool visible;
        int visibleIndex;

        public ColumnState() {
            visible = false;
            visibleIndex = -1;
        }

        public bool Visible {
            get {
                return visible;
            }
            set {
                visible = value;
            }
        }

        public int VisibleIndex {
            get {
                return visibleIndex;
            }
            set {
                visibleIndex = value;
            }
        }

        public static void LoadState(GridViewColumnCollection columns, ColumnState[] state) {
            for (int i = 0; i < state.Length; i++) {                
                columns[i].VisibleIndex = state[i].VisibleIndex;
                columns[i].Visible = state[i].Visible;
            }
        }

        public static ColumnState[] GetState(GridViewColumnCollection columns) {
            ColumnState[] state = new ColumnState[columns.Count];
        for (int i = 0; i < columns.Count; i++) {
            state[i] = new ColumnState();
            state[i].Visible = columns[i].Visible;
            state[i].VisibleIndex = columns[i].VisibleIndex;
        }
        return state;
    }
    }
}
