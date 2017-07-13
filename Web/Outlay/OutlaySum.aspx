<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="OutlaySum.aspx.cs" Inherits="Outlay_OutlaySum" %>
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
            if (e.buttonID == "cbUse") {
                grid.GetRowValues(e.visibleIndex, "YG_OID", function(values) {
                    var YG_OID = values;
                    document.location = "OutlayUse.aspx?YG_OID=" + YG_OID;
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
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsOutlayPublish" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="YG_OID" Caption="支部OID" Visible="false"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="YG_NAME" Caption="团组织名称" ReadOnly="true">
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="OL_SL" Caption="收入" ReadOnly="true">      
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="OL_ZC" Caption="支出" ReadOnly="true">
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="OL_LEFT" Caption="结余" ReadOnly="true">
                <EditFormSettings VisibleIndex="2" />
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataTextColumn FieldName="OL_DESC" Caption="备注">
                <EditFormSettings ColumnSpan="2" />
            </dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="80">
                <EditButton Visible="True" Text="备注" />
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton Text="明细" ID="cbUse"></dxwgv:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
        <%-- EndRegion --%>
        <Settings ShowGroupPanel="false" ShowFooter="true" />
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="YG_NAME" SummaryType="Count" DisplayFormat="总数={0}"/>
            <dxwgv:ASPxSummaryItem FieldName="OL_SL" SummaryType="Sum" DisplayFormat="合计={0:c}" />
            <dxwgv:ASPxSummaryItem FieldName="OL_ZC" SummaryType="Sum" DisplayFormat="合计={0:c}" />
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
        SelectMethod="MySelect" UpdateMethod="OutlayRemark"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.OutlayPublishTableAdapter" >
        <SelectParameters>
            <asp:SessionParameter Name="YG_OID" SessionField="OutlaySum_YG_OID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
<%-- EndRegion --%>
</asp:Content>

