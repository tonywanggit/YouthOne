<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="OutLayPublish.aspx.cs" Inherits="Outlay_OutLayPublish" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="localCssPlaceholder" runat="server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
    <script type="text/javascript">
        function OnCustomButtonClick(s, e) {
            if (e.buttonID == "cbPublish") {
                grid.GetRowValues(e.visibleIndex, "OID;OL_NUM;YG_NAME", function(values) {
                    var oid = values[0];
                    var olNum = values[1];
                    var ygName = values[2];

                    if (olNum == 0) {
                        alert("发放金额不能为零！");
                    } else {
                        grid.PerformCallback(oid + "|" + ygName);
                    }
                });
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="phContent" Runat="Server">
   <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
<%-- EndRegion --%>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnAllPublish" runat="server" Text="全部下发" >
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsOutlayPublish" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="YG_NAME" Caption="团组织名称" ReadOnly="true">
                <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="支部名称必须填写！"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="OL_NUM" Caption="下发金额">
                <PropertiesSpinEdit DecimalPlaces="2"></PropertiesSpinEdit>
                <CellStyle HorizontalAlign="Center"></CellStyle>
                <HeaderStyle HorizontalAlign="Center" />            
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataDateColumn FieldName="OL_DATE" Caption="下发时间" ReadOnly="true">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center"></CellStyle>
                <HeaderStyle HorizontalAlign="Center" />
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataTextColumn FieldName="OL_STATUS" Caption="发放状态">
                <EditFormSettings Visible="False" />
                <CellStyle HorizontalAlign="Center"></CellStyle>
                <HeaderStyle HorizontalAlign="Center" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="80">
                <EditButton Visible="True" Text="编辑" />
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton Text="下发" ID="cbPublish"></dxwgv:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
        <%-- EndRegion --%>
        <Settings ShowGroupPanel="false" ShowFooter="true" />
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="YG_NAME" SummaryType="Count" DisplayFormat="支部总数={0}"/>
            <dxwgv:ASPxSummaryItem FieldName="OL_NUM" SummaryType="Sum" DisplayFormat="合计={0:c}" />
        </TotalSummary>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="EditFormAndDisplayRow" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" PageSize="25" /> 
        <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑标准" />
    </dxwgv:ASPxGridView>
    <script type="text/javascript">
                //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
                InitCustomLoadingPanel(); 
    </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="OdsOutlayPublish" runat="server" 
        SelectMethod="MySelectNoGSTW" UpdateMethod="MyUpdate"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.OutlayPublishTableAdapter" >
        <SelectParameters>
            <asp:SessionParameter Name="YG_OID" SessionField="OutLayPublish_YG_OID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
<%-- EndRegion --%>
</asp:Content>


