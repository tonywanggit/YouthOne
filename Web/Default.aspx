<%@ Page Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_DefaultPage" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dxlp" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="localCssPlaceholder" Runat="Server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
            text-align:right;
        }
    </style>
    <script type="text/javascript">
        var empName;    //--��Ա����
        var catType;    //--֪ͨ����
        var mmOID;      //--��ԱOID
        var nfOID;      //--֪ͨOID
        var nfygOID;    //--ת��֧��OID
        var dept;       //--��������
        var workGroup;  //--��������
        var Post;       //--��λ����
        var ygName;     //--֧������
    
        function OnCustomButtonClick(s, e) {
            if (e.buttonID == "�鿴") {
                var url = "Report/YouthInfo.aspx";
                var feature = "height=" + (screen.availHeight - 30) + ",width=" + (screen.availWidth - 10) + ",left=0,top=0,scrollbars=yes";

                e.processOnServer = false;
                grid.GetRowValues(e.visibleIndex, "MM_OID", function(r) {
                    window.open(url + "?Window=New&OID=" + r, "_blank", feature);
                });
                
            }
            else if(e.buttonID == "����")
            {
                grid.GetRowValues(e.visibleIndex, "MM_NAME;NF_CAT;MM_OID;OID;NF_YG_OID;Dept;WorkGroup;Post;MM_YG_NAME", function(values) {
                    empName = values[0];
                    catType = values[1];
                    mmOID = values[2];
                    nfOID = values[3];
                    nfygOID = values[4];
                    dept = values[5];
                    workGroup = values[6];
                    post = values[7];
                    ygName = values[8];

                    if (catType == "��28") catType = "����";
                    if (catType == "��35") catType = "����";

                    if (catType == "ת��") {
                        txtPost.SetValue(post);
                        lblEmpName.SetValue(empName);
                        lblYGName.SetValue(ygName);
                        cbDept.SetValue(dept);
                        cbYouthGroup.SetValue(nfygOID);

                        cbWorkGroup.PerformCallback(cbDept.GetValue().toString());
                        cbYouthGroupFZ.PerformCallback(cbYouthGroup.GetValue().toString());

                        cbWorkGroup.SetValue(workGroup);

                        cbYouthGroup.SetEnabled(false);
                        popChangeYG.Show();
                    }
                    else if (confirm("������Ա����" + empName + "������ " + catType + " ����")) {
                        grid.PerformCallback(catType + "|" + mmOID + "|" + nfOID + "|" + empName);
                    }
                });
                e.processOnServer = false;
            }
        }

        function OnPostClick() {
            popPost.Show();
        }

        function OnPostSelected(s, e) {
            popPost.Hide();
            gridPost.GetRowValues(e.visibleIndex, "POST_NAME", function(value) {
                txtPost.SetValue(value);
            });
        }

        function OnChangeOK() {
            fzOID = cbYouthGroupFZ.GetValue() ? cbYouthGroupFZ.GetValue() : "";
            dpName = cbDept.GetValue() ? cbDept.GetValue() : "";
            ksName = cbWorkGroup.GetValue() ? cbWorkGroup.GetValue() : "";
            psName = txtPost.GetValue() ? txtPost.GetValue() : "";
            grid.PerformCallback(catType + "|" + mmOID + "|" + nfOID + "|" + empName + "|" + nfygOID + "|" + fzOID + "|" + dpName + "|" + ksName + "|" + psName);
            //cbYouthGroup.Validate();
        
            popChangeYG.Hide();
        }

        function OnChangeCancel() {
            popChangeYG.Hide();
        }

        function OnDeptChanged(s) {
            cbWorkGroup.PerformCallback(cbDept.GetValue().toString());
        }

        function OnYouthGroupChanged(s) {
            cbYouthGroupFZ.PerformCallback(cbYouthGroup.GetValue().toString());
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <asp:ScriptManager ID="ScriptManager" runat="server" />
    <dxlp:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="False" />
    <asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>
        <dxpc:ASPxPopupControl ID="popChangeYG" Modal="true" runat="server" Width="290" Height="300" HeaderText="��Աת�����"
            ClientInstanceName="popChangeYG" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentStyle Paddings-PaddingRight="0"></ContentStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellpadding="3" cellspacing="0">
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel7" Text="����:" runat="server" /></td>
                            <td style="padding-top: 3px"><dxe:ASPxLabel ID="ASPxLabel8" Text="����" runat="server" ClientInstanceName="lblEmpName" /></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel9" Text="����:" runat="server" /></td>
                            <td style="padding-top: 3px"><dxe:ASPxLabel ID="ASPxLabel10" Text="�������֧" runat="server" ClientInstanceName="lblYGName" /></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel2" Text="ת��֧��:" runat="server"/></td>
                            <td ><dxe:ASPxComboBox ID="cbYouthGroup" ClientInstanceName="cbYouthGroup" 
                                    DataSourceID="OdsYouthGroup" TextField="YG_NAME" ValueField="OID" 
                                    runat="server" ToolTip="��ѡ����֧��">
                                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="��ѡ��ת��֧����" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnYouthGroupChanged(s); }" />  
                            </dxe:ASPxComboBox></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel3" Text="��֧:" runat="server" /></td>
                            <td ><dxe:ASPxComboBox ID="cbYouthGroupFZ" ClientInstanceName="cbYouthGroupFZ"
                                    OnCallback="cbYouthGroupFZ_Callback"
                                    runat="server" ToolTip="��ѡ���֧">
                                <ValidationSettings RequiredField-IsRequired="false" RequiredField-ErrorText="��ѡ��ת��֧����" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dxe:ASPxComboBox></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel4" Text="����:" runat="server" /></td>
                            <td ><dxe:ASPxComboBox ID="cbDept" runat="server" ToolTip="��ѡ����" ClientInstanceName="cbDept"
                                TextField="OG_NAME" ValueField="OG_NAME" DataSourceID="odsDept">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDeptChanged(s); }" />    
                            </dxe:ASPxComboBox></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel5" Text="����:" runat="server" /></td>
                            <td ><dxe:ASPxComboBox ID="cbWorkGroup" runat="server" ToolTip="��ѡ�����" 
                                TextField="OG_NAME" ValueField="OG_NAME" OnCallback="cbWorkGroup_Callback" ClientInstanceName="cbWorkGroup"></dxe:ASPxComboBox></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel6" Text="��λ:" runat="server" /></td>
                            <td >
                                <dxe:ASPxButtonEdit ID="cmdPost" runat="server" ClientInstanceName="txtPost" ToolTip="��ѡ���λ">
                                    <Buttons>
                                        <dxe:EditButton ></dxe:EditButton>
                                    </Buttons>
                                    <ClientSideEvents ButtonClick="OnPostClick" />
                                </dxe:ASPxButtonEdit>
                            </td>
                        </tr>
                        <tr>
                            <td class="buttonCell" colspan="2" style="padding-right:28px">
                                <table cellpadding="3" cellspacing="0">
                                    <tr><td colspan="2" style="height:10px"></td></tr>
                                    <tr >
                                        <td><dxe:ASPxButton Text="ȷ��" ID="cmdOK" runat="server" AutoPostBack="false">
                                                <ClientSideEvents Click="OnChangeOK" />
                                            </dxe:ASPxButton></td>
                                        <td><dxe:ASPxButton Text="ȡ��" ID="cmdCancel" runat="server" AutoPostBack="false" >
                                            <ClientSideEvents Click="OnChangeCancel" />
                                        </dxe:ASPxButton></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>    
        
        <dxpc:ASPxPopupControl ID="popPost" Modal="true" runat="server" Width="300" Height="500" HeaderText="��λѡ����"
            ClientInstanceName="popPost" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <dxwgv:ASPxGridView ID="gridPost" ClientInstanceName="gridPost" runat="server" DataSourceID="OdsPost" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
                        <%-- BeginRegion Columns --%>
                        <Columns>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="CAT_NAME" Caption="��λ���">
                            </dxwgv:GridViewDataComboBoxColumn> 
                            <dxwgv:GridViewDataTextColumn FieldName="POST_NAME" Caption="��λ����" VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="ѡ��" HeaderStyle-HorizontalAlign="Center" Width="80">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton Text="ѡ��"></dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <ClientSideEvents CustomButtonClick="OnPostSelected" />
                        <%-- EndRegion --%>
                        <Settings ShowGroupPanel="true" />
                        <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
                        <SettingsText GroupPanel="�϶���ͷ���˴������Խ��л���" EmptyDataRow="�������ݣ�"  />
                    </dxwgv:ASPxGridView>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        
        <table cellpadding="0" cellspacing="0" visible="false">
            <tr>
                <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel1" Text="֪ͨ����:" runat="server" /></td>
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbNotification" runat="server" ToolTip="��ѡ��֪ͨ����" DataSourceID="xdsNotification" 
                        ValueField="Value" TextField="Text" Width="100px" />
                </td>
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnSearch" runat="server" Text="��ѯ"/>
                </td>
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="OID" OnPageIndexChanged="grid_OnPageIndexChanged"  AutoGenerateColumns="False"  Width="100%">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="����" HeaderStyle-HorizontalAlign="Center" Width="60">
                    <CustomButtons>
                        <dxwgv:GridViewCommandColumnCustomButton Text="����"></dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton Text="�鿴"></dxwgv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dxwgv:GridViewCommandColumn>                
                <dxwgv:GridViewDataTextColumn FieldName="OID" Caption="֪ͨID" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="MM_OID" Caption="��ԱID" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NF_YG_OID" Caption="ת��֧��OID" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Dept" Caption="����" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="WorkGroup" Caption="����" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Post" Caption="��λ" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="MM_YG_NAME" Caption="����֧��"  Width="110" ></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="MM_NAME" Caption="Ա������"  Width="60" CellStyle-HorizontalAlign="Left" ></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NF_CAT" Caption="֪ͨ����" Width="60" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NF_NAME" Caption="֪ͨ����"></dxwgv:GridViewDataTextColumn>
            </Columns>
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
            <%-- EndRegion --%>
            <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
        </dxwgv:ASPxGridView>
            <script type="text/javascript">
                //����������ҳ�����ASPxLoadingPanel ClientInstanceName="LoadingPanel"
                InitCustomLoadingPanel(); 
        </script>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ObjectDataSource ID="OdsNotification" runat="server" 
        SelectMethod="MySelect" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.NotificationTableAdapter" >
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="OdsPost" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.PostTableAdapter" 
        SelectMethod="GetDataCache" >
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="OdsDept" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.OrganizeTableAdapter" 
        SelectMethod="GetDeptCache" >
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="OdsYouthGroup" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.YouthGroupTableAdapter" 
        SelectMethod="GetZBCache" >
    </asp:ObjectDataSource>
    
    <asp:XmlDataSource DataFile="~/App_Data/NotificationEnum.xml" XPath="//NotificationType" ID="xdsNotification" runat="server" />
</asp:Content>
