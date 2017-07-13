<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="MemberStatic.aspx.cs" Inherits="Report_MemberStatic" %>
<%@ Register Src="../ReportViewerControl.ascx" TagName="ReportViewerControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="phContent" runat="Server">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <dxrp:ASPxRoundPanel id="ASPxRoundPanel1" runat="server" width="100%"
                    enableviewstate="False" ShowHeader="False">
                    <PanelCollection>
                        <dxrp:PanelContent ID="PanelContent1" runat="server">
                            <table>
                                <tr>
                                    <td valign="middle">
                                        <dxe:ASPxLabel CssClass="defaultLabel" ID="Label1" runat="server" Text="请选择年度 :" EnableViewState="False" />&nbsp;</td>
                                    <td valign="middle">
                                        <dxe:ASPxComboBox ID="cmbYear" runat="server" OnSelectedIndexChanged="cmbYear_OnSelectedIndexChanged" AutoPostBack="true" Width="100"></dxe:ASPxComboBox>    
                                    </td>
                                    <td valign="middle">
                                        <dxe:ASPxLabel CssClass="defaultLabel" ID="ASPxLabel1" runat="server" Text="请选择月份 :" EnableViewState="False" />&nbsp;</td>
                                    <td valign="middle">
                                        <dxe:ASPxComboBox ID="cmbMonth" runat="server" AutoPostBack="true" Width="100"></dxe:ASPxComboBox>    
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btnBackup" runat="server" Text="备份团员数据"></dxe:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                        </dxrp:PanelContent>
                    </PanelCollection>
                </dxrp:ASPxRoundPanel>
            </td>
        </tr>
        <tr>
            <td style="height: 12px" />
        </tr>
        <tr>
            <td>
                <uc1:ReportViewerControl ID="ReportViewerControl1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>



