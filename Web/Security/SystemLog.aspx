<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="SystemLog.aspx.cs" Inherits="Security_SystemLog" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1.Export, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<asp:Content ID="Content1" ContentPlaceHolderID="localCssPlaceholder" Runat="Server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
    <script type="text/javascript">
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel1" Text="日志类型:" runat="server" /></td>
            <td class="buttonCell">
                <dxe:ASPxComboBox ID="cbLogType" runat="server" ToolTip="请选择日志类型" DataSourceID="xdsLogType" 
                    ValueField="Value" TextField="Text" Width="100px" />
            </td>
            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel2" Text="时间范围:" runat="server" /></td>
            <td class="buttonCell">
                <dxe:ASPxComboBox ID="cbTimeScope" runat="server" ToolTip="请选择时间范围" DataSourceID="xdsTimeScope" 
                    ValueField="Value" TextField="Text" Width="100px" />
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnSearch" runat="server" Text="查询" OnClick="cmdSearch_Click" />
            </td>
            <td>
                <dxe:ASPxButton ID="btnExport" runat="server" Text="导出" OnClick="cmdExport_Click"></dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsSystemLog" KeyFieldName="OID" 
        AutoGenerateColumns="False"  Width="100%">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewDataTextColumn FieldName="OP_TYPE" Caption="日志类型" ></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataDateColumn FieldName="OP_DATE" Caption="操作时间" PropertiesDateEdit-DisplayFormatString="yyyy-MM-dd HH:mm:ss" PropertiesDateEdit-EditFormat="Custom"></dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataTextColumn FieldName="SA_NAME" Caption="操作人"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="OP_NAME" Caption="日志名称"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="OP_DESC" Caption="日志信息"></dxwgv:GridViewDataTextColumn>
        </Columns>
        <%-- EndRegion --%>
        <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
    </dxwgv:ASPxGridView>
    <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid"></dxwgv:ASPxGridViewExporter>
    <asp:ObjectDataSource ID="OdsSystemLog" runat="server" 
        SelectMethod="GetData" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.SystemLogTableAdapter" >
        <SelectParameters>
            <asp:ControlParameter ControlID="cbLogType" Name="LogType" Type="String" />
            <asp:ControlParameter ControlID="cbTimeScope" Name="TimeScope" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:XmlDataSource DataFile="~/App_Data/LogTypeEnum.xml" XPath="//LogType" ID="xdsLogType" runat="server" />
    <asp:XmlDataSource DataFile="~/App_Data/TimeScopeEnum.xml" XPath="//TimeScope" ID="xdsTimeScope" runat="server" />
</asp:Content>

