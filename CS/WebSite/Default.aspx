<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.1, Version=14.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <dx:ASPxGridView ID="gvCategories" runat="server" ClientInstanceName="gvCategories"
            AutoGenerateColumns="False" DataSourceID="dsCategories" 
            KeyFieldName="CategoryID">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="CategoryID" ReadOnly="True" VisibleIndex="0">
                    <EditFormSettings Visible="False" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CategoryName" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Description" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
            </Columns>
            <SettingsDetail ShowDetailRow="True" />
            <Templates>
                <DetailRow>
                    <dx:ASPxGridView ID="gvProducts" runat="server" AutoGenerateColumns="False" DataSourceID="dsProducts"
                        KeyFieldName="ProductID" Width="100%" OnAfterPerformCallback="gvProducts_AfterPerformCallback"
                        OnLoad="gvProducts_Load">
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="ProductID" ReadOnly="True" VisibleIndex="1">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="2">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="QuantityPerUnit" VisibleIndex="3">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="UnitPrice" VisibleIndex="4">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="UnitsInStock" VisibleIndex="5">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="UnitsOnOrder" VisibleIndex="6">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataCheckColumn FieldName="Discontinued" VisibleIndex="7">
                            </dx:GridViewDataCheckColumn>
                        </Columns>
                        <SettingsBehavior EnableCustomizationWindow="True" />
                        <Settings ShowTitlePanel="True" />
                        <SettingsCookies Enabled="false" StoreColumnsVisiblePosition="false" StoreGroupingAndSorting="false" />
                        <Templates>
                            <TitlePanel>
                                <dx:ASPxHyperLink ID="hlColCust" runat="server" Text="Customize Columns" OnLoad="hlColCust_Load">
                                </dx:ASPxHyperLink>
                            </TitlePanel>
                        </Templates>
                    </dx:ASPxGridView>
                    <asp:AccessDataSource ID="dsProducts" runat="server" DataFile="~/App_Data/nwind.mdb"
                        OnInit="dsProducts_Init" SelectCommand="SELECT [ProductID], [ProductName], [QuantityPerUnit], [UnitPrice], [UnitsInStock], [UnitsOnOrder], [Discontinued] FROM [Products] WHERE ([CategoryID] = ?)">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="" Name="CategoryID" Type="Int32" />
                        </SelectParameters>
                    </asp:AccessDataSource>
                </DetailRow>
            </Templates>
        </dx:ASPxGridView>
        <asp:AccessDataSource ID="dsCategories" runat="server" DataFile="~/App_Data/nwind.mdb"
            SelectCommand="SELECT [CategoryID], [CategoryName], [Description] FROM [Categories]">
        </asp:AccessDataSource>
    </div>
    </form>
</body>
</html>
