<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="Post.aspx.cs" Inherits="BaseInfo_Post" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>

<asp:Content ID="Content2" ContentPlaceHolderID="localCssPlaceholder" runat="server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
    <script type="text/javascript">
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="phContent" Runat="Server">
<%-- EndRegion --%>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnAddAdmin" runat="server" Text="新增岗位" UseSubmitBehavior="False" AutoPostBack="false">
                    <ClientSideEvents Click="function(){ grid.AddNewRow(); }" />
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsPost" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn FieldName="CAT_NAME" VisibleIndex="1" Caption="岗位类别">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="岗位类别必须填写！"
                     TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsPostCategory">
                </PropertiesComboBox>
                <EditFormSettings VisibleIndex="1" ColumnSpan="1" />
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="POST_NAME" Caption="岗位名称" VisibleIndex="2">
                <EditFormSettings VisibleIndex="2" />
                <PropertiesTextEdit ClientInstanceName="txtLogName" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="岗位名称必须填写！"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataDateColumn FieldName="CRE_DATE" Caption="创建时间" VisibleIndex="3">
                <EditFormSettings Visible="False" />
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="80">
                <EditButton Visible="True" Text="编辑" />
                <DeleteButton Visible="True" Text="删除" />
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents RowDblClick="function(s, e){
            grid.StartEditRow(e.visibleIndex);
        }" />
        <%-- EndRegion --%>
        <Settings ShowGroupPanel="true" />
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="EditFormAndDisplayRow" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" PageSize="30" /> 
        <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑用户" />
    </dxwgv:ASPxGridView>
    <asp:ObjectDataSource ID="OdsPost" runat="server" 
        SelectMethod="GetData" InsertMethod="MyInsert" UpdateMethod="MyUpdate" DeleteMethod="MyDelete"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.PostTableAdapter" >
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/PostCategoryEnum.xml" XPath="//Cate" ID="xdsPostCategory" runat="server" />
<%-- EndRegion --%>
</asp:Content>
