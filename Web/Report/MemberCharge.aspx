<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="MemberCharge.aspx.cs" Inherits="Report_MemberCharge" %>
<%@ Register Src="../ReportViewerControl.ascx" TagName="ReportViewerControl" TagPrefix="uc1" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<asp:Content ID="Content2" ContentPlaceHolderID="localCssPlaceholder" runat="server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
            text-align:right;
        }
    </style>
    <script type="text/javascript">
        function OnBJClick(s, e) {
            popEditForm.Show();
        }

        function OnCancel(s, e) {
            popEditForm.Hide();
        }

        function OnOK(s, e) {
            ReportViewer1.Refresh();
            popEditForm.Hide();
        }
    
    </script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="phContent" runat="Server">
    <dxpc:ASPxPopupControl ID="popEditForm" Modal="true" runat="server" Width="270" Height="250" HeaderText="补缴团费"
        ClientInstanceName="popEditForm" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentStyle Paddings-PaddingRight="0"></ContentStyle>
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <table cellpadding="3" cellspacing="0">
                    <tr>
                        <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel1" Text="一类团员:" runat="server" /></td>
                        <td>
                            <dxe:ASPxSpinEdit ID="txtBJ1" runat="server" Width="150" ClientInstanceName="txtBJ1" NumberType="Integer">
                                <ValidationSettings RequiredField-IsRequired="True" RequiredField-ErrorText="一类团员人数！"  ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dxe:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel2" Text="二类团员:" runat="server" /></td>
                        <td>
                            <dxe:ASPxSpinEdit ID="txtBJ2" runat="server" Width="150" ClientInstanceName="txtBJ2" NumberType="Integer">
                                <ValidationSettings RequiredField-IsRequired="True" RequiredField-ErrorText="二类团员人数！"  ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dxe:ASPxSpinEdit>
                        </td>
                    </tr>
                    <tr>
                        <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel3" Text="三类团员:" runat="server" /></td>
                        <td>
                            <dxe:ASPxSpinEdit ID="txtBJ3" runat="server" Width="150" ClientInstanceName="txtBJ3" NumberType="Integer">
                                <ValidationSettings RequiredField-IsRequired="True" RequiredField-ErrorText="三类团员人数！"  ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dxe:ASPxSpinEdit>
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
                                        <dxe:ASPxLabel CssClass="defaultLabel" ID="Label1" runat="server" Text="请选择支部 :" EnableViewState="False" />&nbsp;</td>
                                    <td valign="middle">
                                        <dxe:ASPxComboBox ID="cbYouthGroup" AutoPostBack="true" runat="server" ToolTip="请选择团支部" Width="190"></dxe:ASPxComboBox>
                                    </td>
                                    <td valign="middle">
                                        <dxe:ASPxComboBox ID="cbQuarter" runat="server" Width="100" AutoPostBack="true">
                                            <Items>
                                                <dxe:ListEditItem Text="一季度" Value="1" />
                                                <dxe:ListEditItem Text="二季度" Value="2" />
                                                <dxe:ListEditItem Text="三季度" Value="3" />
                                                <dxe:ListEditItem Text="四季度" Value="4" />
                                            </Items>
                                        </dxe:ASPxComboBox>
                                    </td>
                                    <td valign="middle">
                                        <dxe:ASPxButton ID="btnBJ" runat="server" Text="补缴金费" AutoPostBack="false" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="OnBJClick" />
                                        </dxe:ASPxButton>
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

