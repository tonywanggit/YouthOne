<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="OutlayUse.aspx.cs" Inherits="Outlay_OutlayUse" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="localCssPlaceholder" runat="server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
            text-align:right;

        }
    </style>
    <script type="text/javascript">
        function NewLayoutUse(s, e) {
            if (cbFZ.GetValue() != "ALL") cbYouthGroup.SetValue(cbFZ.GetValue());
        
            popEditForm.Show();
        }

        function OnCancel(s, e) { popEditForm.Hide(); }

        function OnOK(s, e) {
            if (txtNum.GetIsValid() && txtDesc.GetIsValid()) {
                grid.PerformCallback(cbYouthGroup.GetValue() + "|" + cbType.GetValue() + "|" + txtNum.GetValue() + "|" + txtDesc.GetValue());
                popEditForm.Hide();
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="phContent" Runat="Server">
   <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
        <dxpc:ASPxPopupControl ID="popEditForm" Modal="true" runat="server" Width="300" Height="250" HeaderText="经费使用明细"
        ClientInstanceName="popEditForm" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentStyle Paddings-PaddingRight="0"></ContentStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel2" Text="支部:" runat="server"/></td>
                        <td ><dxe:ASPxComboBox ID="cbYouthGroup" ClientInstanceName="cbYouthGroup"  TextField="YG_NAME" ValueField="OID" 
                                runat="server" ToolTip="请选择团支部" Width="200">
                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="请选择转入支部！" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                        </dxe:ASPxComboBox></td>
                    </tr>
                    <tr>
                        <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel6" Text="类型:" runat="server" /></td>
                        <td >
                            <dxe:ASPxComboBox ID="cbType" ClientInstanceName="cbType" runat="server" ToolTip="请选择类型！" SelectedIndex="0" Width="200">
                                <Items>
                                    <dxe:ListEditItem Text="收入" Value="收入" />
                                    <dxe:ListEditItem Text="支出" Value="支出" />
                                </Items>
                            </dxe:ASPxComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel1" Text="金额:" runat="server" /></td>
                        <td>
                            <dxe:ASPxSpinEdit ID="txtNum" runat="server" DecimalPlaces="2" Width="200" ClientInstanceName="txtNum" >
                                <ValidationSettings RequiredField-IsRequired="True" RequiredField-ErrorText="请输入金额！"  ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dxe:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel3" Text="摘要:" runat="server" /></td>
                        <td>
                            <dxe:ASPxTextBox ID="txtDesc" runat="server" Width="200" ClientInstanceName="txtDesc">
                                <ValidationSettings RequiredField-IsRequired="True" RequiredField-ErrorText="请输入摘要！"  ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dxe:ASPxTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonCell" colspan="2" style="padding-right:28px">
                            <table cellpadding="3" cellspacing="0">
                                <tr><td colspan="2" style="height:10px"></td></tr>
                                <tr >
                                    <td><dxe:ASPxButton Text="确定" ID="cmdOK" runat="server" AutoPostBack="false">
                                            <ClientSideEvents Click="OnOK" />
                                        </dxe:ASPxButton></td>
                                    <td><dxe:ASPxButton Text="取消" ID="cmdCancel" runat="server" AutoPostBack="false" >
                                        <ClientSideEvents Click="OnCancel" />
                                    </dxe:ASPxButton></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>    
    
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxe:ASPxComboBox ID="cbFZ" runat="server" ClientInstanceName="cbFZ" AutoPostBack="true" ></dxe:ASPxComboBox>
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnAdd" runat="server" AutoPostBack="false" Text="新增明细" ClientSideEvents-Click="NewLayoutUse"></dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsOutlayUse" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="YG_NAME" Caption="团组织名称" ReadOnly="true">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="OU_SL" Caption="收入">      
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="OU_ZC" Caption="支出" >
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="OU_JY" Caption="结余" ReadOnly="true">
                <EditFormSettings VisibleIndex="2" />
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataTextColumn FieldName="OU_DESC" Caption="摘要">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataDateColumn FieldName="OU_DATE" Caption="日期" ReadOnly="true"></dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="OU_STATUS" Caption="状态" Visible="false"></dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="60">
                <EditButton Visible="True" Text="编辑" />
                <DeleteButton Visible="true" Text="删除" />
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <%-- EndRegion --%>
        <Settings ShowGroupPanel="false" ShowFooter="true" />
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="OU_SL" SummaryType="Sum" DisplayFormat="合计={0:c}" />
            <dxwgv:ASPxSummaryItem FieldName="OU_ZC" SummaryType="Sum" DisplayFormat="合计={0:c}" />
        </TotalSummary>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="EditFormAndDisplayRow" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
        <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑经费使用明细" />
    </dxwgv:ASPxGridView>
    <script type="text/javascript">
        //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
        InitCustomLoadingPanel(); 
    </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="OdsOutlayUse" runat="server" 
        SelectMethod="MySelect" DeleteMethod="DeleteOutlayUse" UpdateMethod="MyUpdate"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.OutlayUseTableAdapter" >
        <SelectParameters>
            <asp:ControlParameter Name="YG_OID" ControlID="cbFZ" Type="String" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
<%-- EndRegion --%>
</asp:Content>
