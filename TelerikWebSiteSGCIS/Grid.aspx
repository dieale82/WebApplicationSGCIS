<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPageSingleMenu.Master" AutoEventWireup="true" CodeFile="Grid.aspx.cs" Inherits="Grid" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="styles/grid.css" rel="stylesheet" />
    <style>
        .imgButton {
            width:20px;
            height:20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <telerik:RadAjaxPanel ID="RadAjaxPanel1" ClientEvents-OnRequestStart="onRequestStart" runat="server" CssClass="grid_wrapper">
        <telerik:RadGrid ID="RadGrid1" runat="server" PageSize="10" PagerStyle-PageButtonCount="5"
            OnNeedDataSource="RadGrid1_NeedDataSource"  OnPreRender="RadGrid1_PreRender" OnItemCommand="RadGrid1_ItemCommand"
            OnUpdateCommand="RadGrid1_UpdateCommand" OnInsertCommand="RadGrid1_InsertCommand" OnDeleteCommand="RadGrid1_DeleteCommand"
            AllowPaging="True" AllowSorting="true" ShowGroupPanel="true" RenderMode="Auto">
            <MasterTableView AutoGenerateColumns="False"
                TableLayout="Fixed"
                DataKeyNames="ID" CommandItemDisplay="Top"
                InsertItemPageIndexAction="ShowItemOnFirstPage">
                <ColumnGroups>
                    <telerik:GridColumnGroup HeaderText="Actions" Name="Actions" HeaderStyle-HorizontalAlign="Center">
                    </telerik:GridColumnGroup>                    
                </ColumnGroups>

                <Columns>
                    <telerik:GridBoundColumn DataField="Name" HeaderText="Name" SortExpression="Name"
                        UniqueName="Name">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>
                    <telerik:GridNumericColumn DataField="Age" HeaderText="Age" SortExpression="Age"
                        UniqueName="Age">
                        <HeaderStyle Width="150px" />
                    </telerik:GridNumericColumn>

                    <telerik:GridBoundColumn DataField="Type" HeaderText="Type" SortExpression="Type"
                        UniqueName="Type">
                        <HeaderStyle Width="150px" />
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="PersonType" Display="false" HeaderText="PersonType" SortExpression="PersonType"
                        UniqueName="PersonType">                        
                    </telerik:GridBoundColumn>
                    <telerik:GridEditCommandColumn ColumnGroupName="Actions" ><HeaderStyle Width="20px" /></telerik:GridEditCommandColumn>
                    <telerik:GridButtonColumn ConfirmText="Delete this product?" ConfirmDialogType="RadWindow"
                        ConfirmTitle="Delete" ButtonType="FontIconButton" CommandName="Delete" ColumnGroupName="Actions"><HeaderStyle Width="20px" /></telerik:GridButtonColumn>
                    <telerik:GridButtonColumn ButtonType="ImageButton" UniqueName="btnReport" CommandName="Report" ColumnGroupName="Actions" ItemStyle-Width="20px" ItemStyle-Height="20px" ButtonCssClass="imgButton" ImageUrl="~/images/report.png"><HeaderStyle Width="20px" /></telerik:GridButtonColumn>                                  
                    

                </Columns>
                <EditFormSettings UserControlName="EditForm.ascx" EditFormType="WebUserControl">
                    <EditColumn UniqueName="EditCommandColumn1">
                    </EditColumn>
                </EditFormSettings>
            </MasterTableView>
        </telerik:RadGrid>
    </telerik:RadAjaxPanel>
    <telerik:RadCodeBlock runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("Button") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>


