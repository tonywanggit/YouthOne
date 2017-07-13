<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="SystemAdmin.aspx.cs" Inherits="Security_SystemAdmin" %>
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
        var hidPassWordClient = '<%= hidPassWord.ClientID%>';
    
        function OnRoleChange(s) {
            var roleName = cmbRole.GetValue().toString();
            if (roleName != "TZB_Admin") {
                cmbTZB.SetEnabled(false);
                cmbTZB.SetSelectedIndex(0);
            }
            else {
                cmbTZB.SetEnabled(true);
            }
        }

        function OnPassWordEditorInit() {
            if (txtLogName.GetValue()) {
                var passWord = document.getElementById(hidPassWordClient).value;
                if (passWord)
                    txtPassWord.SetValue(passWord);
                else
                    txtPassWord.SetValue("PAS_WORD_TONY");
            }
        }

        function OnTZBEditorValidation(s, e) {
            var roleName = cmbRole.GetValue().toString();

            if (roleName == "TZB_Admin" && e.value == "GSTW") {
                e.isValid = false;
                e.errorText = "不能选择公司团委";
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" Runat="Server">
<%-- EndRegion --%>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnAddAdmin" runat="server" Text="新增管理员" UseSubmitBehavior="False" AutoPostBack="false">
                    <ClientSideEvents Click="function(){ grid.AddNewRow(); }" />
                </dxe:ASPxButton>
                <asp:HiddenField ID="hidPassWord" runat="server"  />
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsSystemAdmin" KeyFieldName="OID" 
        OnHtmlRowPrepared="grid_HtmlRowPrepared" OnInitNewRow="grid_InitNewRow" OnCellEditorInitialize="grid_CellEditorInitialize"
        AutoGenerateColumns="False" OnRowValidating="grid_RowValidating" OnRowUpdating="grid_RowUpdating" Width="100%">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="LOG_NAME" Caption="登陆名" VisibleIndex="1">
                <EditFormSettings VisibleIndex="1" />
                <PropertiesTextEdit ClientInstanceName="txtLogName" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="登陆名必须填写！"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="PAS_WORD" Caption="密码" VisibleIndex="2" Visible="false" PropertiesTextEdit-Password="True">
                <EditFormSettings VisibleIndex="2" Visible="True" />
                <PropertiesTextEdit ClientInstanceName="txtPassWord" ClientSideEvents-Validation="" ClientSideEvents-Init="OnPassWordEditorInit" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="密码必须填写！"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataComboBoxColumn FieldName="ROL_NAME" VisibleIndex="3" Caption="角色">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="角色必须填写！"
                     ClientInstanceName="cmbRole" ClientSideEvents-SelectedIndexChanged="OnRoleChange"
                     TextField="Text" ValueField="Value" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="xdsRole">
                </PropertiesComboBox>
                <EditFormSettings VisibleIndex="3" ColumnSpan="1" />
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="YG_OID" VisibleIndex="4" Caption="团支部">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="团支部必须填写！"
                    ClientInstanceName="cmbTZB" ClientSideEvents-Validation="OnTZBEditorValidation"
                    TextField="YG_NAME" ValueField="OID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsYouthGroup">
                </PropertiesComboBox>
                <EditFormSettings VisibleIndex="4" ColumnSpan="1" />
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="80">
                <EditButton Visible="True" Text="编辑" />
                <DeleteButton Visible="True" Text="删除" />
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents RowDblClick="function(s, e){
            grid.StartEditRow(e.visibleIndex);
        }" />
        <%-- EndRegion --%>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="EditFormAndDisplayRow" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" /> 
        <SettingsText EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑用户" />
    </dxwgv:ASPxGridView>
    <asp:ObjectDataSource ID="OdsSystemAdmin" runat="server" 
        SelectMethod="MySelect" InsertMethod="MyInsert" DeleteMethod="MyDelete" UpdateMethod="MyUpdate"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.SystemAdminTableAdapter" >
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsYouthGroup" runat="server" 
        SelectMethod="GetDataZB" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.YouthGroupTableAdapter">
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/RoleEnum.xml" XPath="//Role" ID="xdsRole" runat="server" />
<%-- EndRegion --%>
</asp:Content>