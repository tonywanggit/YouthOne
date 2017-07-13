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
        var empName;    //--团员姓名
        var catType;    //--通知类型
        var mmOID;      //--团员OID
        var nfOID;      //--通知OID
        var nfygOID;    //--转入支部OID
        var dept;       //--部门名称
        var workGroup;  //--科室名称
        var Post;       //--岗位名称
        var ygName;     //--支部名称
    
        function OnCustomButtonClick(s, e) {
            if (e.buttonID == "查看") {
                var url = "Report/YouthInfo.aspx";
                var feature = "height=" + (screen.availHeight - 30) + ",width=" + (screen.availWidth - 10) + ",left=0,top=0,scrollbars=yes";

                e.processOnServer = false;
                grid.GetRowValues(e.visibleIndex, "MM_OID", function(r) {
                    window.open(url + "?Window=New&OID=" + r, "_blank", feature);
                });
                
            }
            else if(e.buttonID == "处理")
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

                    if (catType == "超28") catType = "离团";
                    if (catType == "超35") catType = "内退";

                    if (catType == "转入") {
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
                    else if (confirm("您将对员工『" + empName + "』进行 " + catType + " 处理！")) {
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
        <dxpc:ASPxPopupControl ID="popChangeYG" Modal="true" runat="server" Width="290" Height="300" HeaderText="团员转入操作"
            ClientInstanceName="popChangeYG" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentStyle Paddings-PaddingRight="0"></ContentStyle>
            <ContentCollection>
                <dxpc:PopupControlContentControl runat="server">
                    <table cellpadding="3" cellspacing="0">
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel7" Text="姓名:" runat="server" /></td>
                            <td style="padding-top: 3px"><dxe:ASPxLabel ID="ASPxLabel8" Text="王旭东" runat="server" ClientInstanceName="lblEmpName" /></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel9" Text="来自:" runat="server" /></td>
                            <td style="padding-top: 3px"><dxe:ASPxLabel ID="ASPxLabel10" Text="设计团总支" runat="server" ClientInstanceName="lblYGName" /></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel2" Text="转入支部:" runat="server"/></td>
                            <td ><dxe:ASPxComboBox ID="cbYouthGroup" ClientInstanceName="cbYouthGroup" 
                                    DataSourceID="OdsYouthGroup" TextField="YG_NAME" ValueField="OID" 
                                    runat="server" ToolTip="请选择团支部">
                                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="请选择转入支部！" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnYouthGroupChanged(s); }" />  
                            </dxe:ASPxComboBox></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel3" Text="分支:" runat="server" /></td>
                            <td ><dxe:ASPxComboBox ID="cbYouthGroupFZ" ClientInstanceName="cbYouthGroupFZ"
                                    OnCallback="cbYouthGroupFZ_Callback"
                                    runat="server" ToolTip="请选择分支">
                                <ValidationSettings RequiredField-IsRequired="false" RequiredField-ErrorText="请选择转入支部！" ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                            </dxe:ASPxComboBox></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel4" Text="部门:" runat="server" /></td>
                            <td ><dxe:ASPxComboBox ID="cbDept" runat="server" ToolTip="请选择部门" ClientInstanceName="cbDept"
                                TextField="OG_NAME" ValueField="OG_NAME" DataSourceID="odsDept">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnDeptChanged(s); }" />    
                            </dxe:ASPxComboBox></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel5" Text="科室:" runat="server" /></td>
                            <td ><dxe:ASPxComboBox ID="cbWorkGroup" runat="server" ToolTip="请选择科室" 
                                TextField="OG_NAME" ValueField="OG_NAME" OnCallback="cbWorkGroup_Callback" ClientInstanceName="cbWorkGroup"></dxe:ASPxComboBox></td>
                        </tr>
                        <tr>
                            <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel6" Text="岗位:" runat="server" /></td>
                            <td >
                                <dxe:ASPxButtonEdit ID="cmdPost" runat="server" ClientInstanceName="txtPost" ToolTip="请选择岗位">
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
                                        <td><dxe:ASPxButton Text="确定" ID="cmdOK" runat="server" AutoPostBack="false">
                                                <ClientSideEvents Click="OnChangeOK" />
                                            </dxe:ASPxButton></td>
                                        <td><dxe:ASPxButton Text="取消" ID="cmdCancel" runat="server" AutoPostBack="false" >
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
        
        <dxpc:ASPxPopupControl ID="popPost" Modal="true" runat="server" Width="300" Height="500" HeaderText="岗位选择器"
            ClientInstanceName="popPost" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
            <ContentCollection>
                <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <dxwgv:ASPxGridView ID="gridPost" ClientInstanceName="gridPost" runat="server" DataSourceID="OdsPost" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
                        <%-- BeginRegion Columns --%>
                        <Columns>
                            <dxwgv:GridViewDataComboBoxColumn FieldName="CAT_NAME" Caption="岗位类别">
                            </dxwgv:GridViewDataComboBoxColumn> 
                            <dxwgv:GridViewDataTextColumn FieldName="POST_NAME" Caption="岗位名称" VisibleIndex="2">
                            </dxwgv:GridViewDataTextColumn>
                            <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="选择" HeaderStyle-HorizontalAlign="Center" Width="80">
                                <CustomButtons>
                                    <dxwgv:GridViewCommandColumnCustomButton Text="选择"></dxwgv:GridViewCommandColumnCustomButton>
                                </CustomButtons>
                            </dxwgv:GridViewCommandColumn>
                        </Columns>
                        <ClientSideEvents CustomButtonClick="OnPostSelected" />
                        <%-- EndRegion --%>
                        <Settings ShowGroupPanel="true" />
                        <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
                        <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！"  />
                    </dxwgv:ASPxGridView>
                </dxpc:PopupControlContentControl>
            </ContentCollection>
        </dxpc:ASPxPopupControl>
        
        <table cellpadding="0" cellspacing="0" visible="false">
            <tr>
                <td class="buttonCell"><dxe:ASPxLabel ID="ASPxLabel1" Text="通知类型:" runat="server" /></td>
                <td class="buttonCell">
                    <dxe:ASPxComboBox ID="cbNotification" runat="server" ToolTip="请选择通知类型" DataSourceID="xdsNotification" 
                        ValueField="Value" TextField="Text" Width="100px" />
                </td>
                <td class="buttonCell">
                    <dxe:ASPxButton ID="btnSearch" runat="server" Text="查询"/>
                </td>
            </tr>
        </table>
        <br />
        <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="OID" OnPageIndexChanged="grid_OnPageIndexChanged"  AutoGenerateColumns="False"  Width="100%">
            <%-- BeginRegion Columns --%>
            <Columns>
                <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="60">
                    <CustomButtons>
                        <dxwgv:GridViewCommandColumnCustomButton Text="处理"></dxwgv:GridViewCommandColumnCustomButton>
                        <dxwgv:GridViewCommandColumnCustomButton Text="查看"></dxwgv:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dxwgv:GridViewCommandColumn>                
                <dxwgv:GridViewDataTextColumn FieldName="OID" Caption="通知ID" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="MM_OID" Caption="团员ID" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NF_YG_OID" Caption="转入支部OID" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Dept" Caption="部门" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="WorkGroup" Caption="科室" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="Post" Caption="岗位" Visible="false"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="MM_YG_NAME" Caption="所属支部"  Width="110" ></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="MM_NAME" Caption="员工姓名"  Width="60" CellStyle-HorizontalAlign="Left" ></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NF_CAT" Caption="通知类型" Width="60" CellStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></dxwgv:GridViewDataTextColumn>
                <dxwgv:GridViewDataTextColumn FieldName="NF_NAME" Caption="通知内容"></dxwgv:GridViewDataTextColumn>
            </Columns>
            <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
            <%-- EndRegion --%>
            <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
        </dxwgv:ASPxGridView>
            <script type="text/javascript">
                //调用条件：页面包含ASPxLoadingPanel ClientInstanceName="LoadingPanel"
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
