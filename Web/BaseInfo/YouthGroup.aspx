<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="YouthGroup.aspx.cs" Inherits="BaseInfo_YouthGroup" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"  Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>

<%@ Register assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxPopupControl" tagprefix="dxpc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="localCssPlaceholder" Runat="Server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
    <script type="text/javascript">
        function OnClick(s, e) {
            if (btnExpand.GetText() == "全部展开") {
                btnExpand.SetText("全部收拢");
                treeList.ExpandAll();
            } else {
                btnExpand.SetText("全部展开");
                treeList.CollapseAll();
            }
        }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0">
            <tr>          
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnAdd" ClientInstanceName="btnExpand" runat="server" Text="全部展开" UseSubmitBehavior="False" AutoPostBack="false">
                        <ClientSideEvents Click="OnClick" />
                    </dxe:ASPxButton>
                </td>     
            </tr>
        </table>
        <br />
        <dxwtl:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="False" Width="100%" DataSourceID="OdsYouthGroup" 
            KeyFieldName="OID" ParentFieldName="PARENT_OID" ClientInstanceName="treeList" Border-BorderStyle="Solid" 
            OnHtmlCommandCellPrepared="treeList_HtmlCommandCellPrepared" OnDataBound="treeList_DataBound" OnInitNewNode = "treeList_InitNewNode"
            Settings-GridLines="Both" SettingsText-CommandNew="新增" SettingsText-CommandUpdate="保存" SettingsText-CommandCancel="取消"
            SettingsText-ConfirmDelete="您确认要删除此行?" SettingsText-CommandDelete="删除" SettingsText-CommandEdit="编辑" SettingsText-RecursiveDeleteError="该节点下有支部，无法删除该节点！" SettingsEditing-AllowNodeDragDrop="True">
             <settingstext commandcancel="取消" commanddelete="删除" commandedit="编辑" commandnew="新增" commandupdate="保存" confirmdelete="您确认要删除此行?" recursivedeleteerror="该节点下有支部，无法删除该节点！" />
             <border borderstyle="Solid"></border>
             <Settings GridLines="Both" />
             <SettingsBehavior ExpandCollapseAction="NodeDblClick" />
             <SettingsEditing Mode="EditFormAndDisplayNode" />
            <Columns>
                <dxwtl:TreeListTextColumn FieldName="YG_NAME" Caption="支部名称">
                    <EditFormSettings VisibleIndex="0" />
                    <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="支部名称必须填写！" />
                </dxwtl:TreeListTextColumn>
                <dxwtl:TreeListDateTimeColumn FieldName="CRE_DATE" Caption="创建日期" ReadOnly="True" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd" PropertiesDateEdit-EditFormatString="yyyy-MM-dd">
                </dxwtl:TreeListDateTimeColumn>
               <dxwtl:TreeListCommandColumn Width="100" Caption="操作" Name="OP_COL">
                    <EditButton Visible="true" Text="编辑" />
                    <NewButton Visible="true" Text="新增" />
                    <DeleteButton Visible="true" Text="删除" />
                    <HeaderStyle HorizontalAlign="Center" />
                </dxwtl:TreeListCommandColumn>
            </Columns>     
        </dxwtl:ASPxTreeList>  

        <script type="text/javascript">
            //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
            InitCustomLoadingPanel(); 
        </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <%-- BeginRegion DataSource --%>
    
    <asp:ObjectDataSource ID="OdsYouthGroup" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.YouthGroupTableAdapter" 
        oninserting="OdsYouthGroup_Inserting" 
        SelectMethod="GetDataOrder" DeleteMethod="MyDelete" InsertMethod="MyInsert" UpdateMethod="MyUpdate" 
        >
        <DeleteParameters>
            <asp:Parameter Name="OID" Type="String" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="YG_NAME" Type="String" />
            <asp:Parameter Name="PARENT_OID" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="YG_NAME" Type="String" />
            <asp:Parameter Name="OID" Type="String" />
        </UpdateParameters>
    </asp:ObjectDataSource>
</asp:Content>

