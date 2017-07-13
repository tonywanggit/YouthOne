<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="BudgetApply.aspx.cs" Inherits="Outlay_BudgetApply" %>
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
        }
    </style>
    <script type="text/javascript">
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="phContent" Runat="Server">
   <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxe:ASPxComboBox ID="cbYouthGroup" runat="server" ClientInstanceName="cbYouthGroup" AutoPostBack="true" ></dxe:ASPxComboBox>
            </td>
            <td class="buttonCell">
                <dxe:ASPxComboBox ID="cbMonth" runat="server" AutoPostBack="true">
                    <Items>
                        <dxe:ListEditItem Text="所有月份" Value="0" />
                        <dxe:ListEditItem Text="一月" Value="1" />
                        <dxe:ListEditItem Text="二月" Value="2" />
                        <dxe:ListEditItem Text="三月" Value="3" />
                        <dxe:ListEditItem Text="四月" Value="4" />
                        <dxe:ListEditItem Text="五月" Value="5" />
                        <dxe:ListEditItem Text="六月" Value="6" />
                        <dxe:ListEditItem Text="七月" Value="7" />
                        <dxe:ListEditItem Text="八月" Value="8" />
                        <dxe:ListEditItem Text="九月" Value="9" />
                        <dxe:ListEditItem Text="十月" Value="10" />
                        <dxe:ListEditItem Text="十一月" Value="11" />
                        <dxe:ListEditItem Text="十二月" Value="12" />
                    </Items>
                </dxe:ASPxComboBox>
            </td>
            <td class="buttonCell">
                <dxe:ASPxComboBox ID="cbStatus" runat="server" AutoPostBack="true" SelectedIndex="0">
                    <Items>
                        <dxe:ListEditItem Text="所有状态" Value="%%" />
                        <dxe:ListEditItem Text="审核未通过" Value="审核未通过" />
                        <dxe:ListEditItem Text="审核已通过" Value="审核已通过" />
                        <dxe:ListEditItem Text="等待审核中" Value="等待审核中" />
                    </Items>
                </dxe:ASPxComboBox>
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnAdd" runat="server" AutoPostBack="false" UseSubmitBehavior="false" Text="新增预算">
                    <ClientSideEvents Click="function(){ grid.AddNewRow(); }" />
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsBudgetApply" KeyFieldName="OID" AutoGenerateColumns="False" Width="800">
        <Columns>
            <dxwgv:GridViewDataComboBoxColumn FieldName="YG_OID" Caption="团组织名称" ReadOnly="true">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="团支部必须填写！"
                    TextField="YG_NAME" ValueField="OID" EnableSynchronization="False" EnableIncrementalFiltering="False" DataSourceID="OdsYouthGroup">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataDateColumn FieldName="CRE_DATE" Caption="申请日期" Width="70">
                <PropertiesDateEdit Width="204"></PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataDateColumn FieldName="SP_DATE" Caption="审批日期" Visible="false">
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataSpinEditColumn FieldName="BA_NUM" Caption="预算金额" Width="70">      
                <PropertiesSpinEdit DecimalPlaces="2" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="预算金额必须填写！"></PropertiesSpinEdit>
            </dxwgv:GridViewDataSpinEditColumn>
            <dxwgv:GridViewDataComboBoxColumn FieldName="YJ_MONTH" Caption="预计发生月份" >
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="预计发生月份必须填写！"
                    EnableIncrementalFiltering="False" >
                    <Items>
                        <dxe:ListEditItem Text="一月" Value="一月" />
                        <dxe:ListEditItem Text="二月" Value="二月" />
                        <dxe:ListEditItem Text="三月" Value="三月" />
                        <dxe:ListEditItem Text="四月" Value="四月" />
                        <dxe:ListEditItem Text="五月" Value="五月" />
                        <dxe:ListEditItem Text="六月" Value="六月" />
                        <dxe:ListEditItem Text="七月" Value="七月" />
                        <dxe:ListEditItem Text="八月" Value="八月" />
                        <dxe:ListEditItem Text="九月" Value="九月" />
                        <dxe:ListEditItem Text="十月" Value="十月" />
                        <dxe:ListEditItem Text="十一月" Value="十一月" />
                        <dxe:ListEditItem Text="十二月" Value="十二月" />                    
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>  
            <dxwgv:GridViewDataTextColumn FieldName="BA_DESC" Caption="活动摘要" EditFormSettings-ColumnSpan="2">
                <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="活动摘要必须填写！"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn>  
            <dxwgv:GridViewDataComboBoxColumn FieldName="BA_STATUS" Caption="状态" ReadOnly="true" EditFormSettings-Visible="true">
                <PropertiesComboBox DataSourceID="xdsBAStatus" TextField="Text" ValueField="Value" ></PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataMemoColumn FieldName="SP_REASON" Caption="未通过原因" EditFormSettings-ColumnSpan="2" EditFormSettings-Visible="true" CellStyle-Wrap="True" Width="100">
            </dxwgv:GridViewDataMemoColumn>
            <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="60">
                <EditButton Visible="True" Text="编辑" />
                <DeleteButton Visible="true" Text="删除" />
            </dxwgv:GridViewCommandColumn>
        </Columns>
        <%-- EndRegion --%>
        <Settings ShowGroupPanel="false" ShowFooter="true" />
        <TotalSummary>
            <dxwgv:ASPxSummaryItem FieldName="BA_NUM" SummaryType="Sum" DisplayFormat="合计={0:c}" />
        </TotalSummary>
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="EditFormAndDisplayRow" PopupEditFormWidth="500px" NewItemRowPosition="Top" />
        <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
        <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="提交" PopupEditFormCaption="编辑预算" />
    </dxwgv:ASPxGridView>
    <script type="text/javascript">
        //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
        InitCustomLoadingPanel(); 
    </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="OdsBudgetApply" runat="server" 
        SelectMethod="MySelect" InsertMethod="MyInsert" UpdateMethod="MyUpdate" DeleteMethod="MyDelete"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.BudgetApplyTableAdapter" >
        <SelectParameters>
            <asp:ControlParameter ControlID="cbYouthGroup" Type="String" PropertyName="Value" Name="YG_OID" />
            <asp:ControlParameter ControlID="cbMonth" Type="Int16" PropertyName="Value" Name="Month" />
            <asp:ControlParameter ControlID="cbStatus" Type="String" PropertyName="Value" Name="Status" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsYouthGroup" runat="server" 
        SelectMethod="GetZBCache" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.YouthGroupTableAdapter">
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/BudgetApplyStatusEnum.xml" XPath="//BudgetApplyStatus" ID="xdsBAStatus" runat="server" />
</asp:Content>

