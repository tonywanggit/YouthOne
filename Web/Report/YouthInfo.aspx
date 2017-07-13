<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="YouthInfo.aspx.cs" Inherits="Report_YouthInfo" %>
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
                                                    <dxe:ASPxLabel CssClass="defaultLabel" ID="Label1" runat="server" Text="请输入员工工号:" EnableViewState="False" />&nbsp;</td>
                                                <td valign="middle">
                                                    <dxe:ASPxTextBox ID="txtHrCode" runat="server" Width="100px" EnableViewState="False" >
                                                        <clientsideevents keypress="function(s, e) {
                                                                if(e.htmlEvent.keyCode != 13)
                                                                    return;
                                                                ReportViewer1.Refresh();
                                                            }"></clientsideevents>
                                                </dxe:ASPxTextBox></td>
                                                <td>
                                                    <dxe:ASPxButton ID="btnOK" Text="确定" runat="server"></dxe:ASPxButton>
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


