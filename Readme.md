<!-- default file list -->
*Files to look at*:

* [Default.aspx](./CS/WebSite/Default.aspx) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
* [Default.aspx.cs](./CS/WebSite/Default.aspx.cs) (VB: [Default.aspx](./VB/WebSite/Default.aspx))
<!-- default file list end -->
# How to set identical column collection and order for all detail grids in a Master-Detail scenario


<p>This example demonstrates how to set an identical column collection for all detail grids.</p><br />
<p>The main idea is to store a detail grid's columns collection in the Session. Handle the detail grid's Load event to load columns from the session. Handle the detail grid's <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridView_AfterPerformCallbacktopic"><u>ASPxGridView.AfterPerformCallback</u></a>  event to check if columns were moved (<a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxGridViewAfterPerformCallbackEventArgs_CallbackNametopic"><u>ASPxGridViewAfterPerformCallbackEventArgs.CallbackName</u></a> is set to "COLUMNMOVE" value), and save them to the Session.</p><br />
<p>On the client-side, it is necessary to update the master grid when the column was moved in one of the details - to apply a new columns collection for all other detail grids. To do that, handle the detail's client-side <a href="http://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_EndCallbacktopic"><u>ASPxClientGridView.EndCallback</u></a> event to send an empty callback to the master grid, after the columns are moved.</p>

<br/>


