<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="MemberList.aspx.cs" Inherits="Member_MemberList" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dxuc" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1.Export, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView.Export" TagPrefix="dxwgv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="localCssPlaceholder" runat="server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
    <script type="text/javascript">
        var mmOID;  //--员工OID
        var mmName; //--员工姓名
        var postEditor; //--岗位编辑器
        var seEditor;   //--职称/技能等级编辑器
        var edit = true;    //--编辑器是否编辑过

        var contextMenu_s;
        var contextMenu_e;

        var fieldSeparator = "|";
        function FileUploadStart() {
            //document.getElementById("uploadedListFiles").innerHTML = "";
        }
        function FileUploaded(s, e) {
        }
    
        function OnDeptChange(s, e) {
            grid.GetEditor("WorkGroup").PerformCallback(s.GetValue().toString());
        }

        function OnPriseNameChange(s, e) {
            var prVal = s.GetSelectedItem().GetColumnText(1);
            prValue.SetValue(prVal);
        }   

        function OnSEClick(s, e) {
            popSE.Show();
        }

        function OnSESelected(s, e) {
            popSE.Hide();
            gridSE.GetRowValues(e.visibleIndex, "SE_KEY", function(value) {
                seEditor.SetValue(value);
            });
        }

        function OnPostClick(s, e) {
            popPost.Show();
        }

        function OnPostSelected(s, e) {
            popPost.Hide();
            gridPost.GetRowValues(e.visibleIndex, "POST_NAME", function(value) {
                postEditor.SetValue(value);
            });
        }
        
        function OnYGSelected(s, e) {
            popYG.Hide();
            gridYG.GetRowValues(e.visibleIndex, "OID", function(value) {
                var ygOID = value;
                grid.PerformCallback("转出|" + mmOID + "|" + ygOID + "|" + mmName);
            });
        }

        //--Tony.2013-03-29.应客户要求增加右击菜单功能
        function OnGridContextMenu(s, e) {
            //--row代表数据行，head代表表头
            if(e.objectType == "row"){
                contextMenu_s = s;
                contextMenu_e = e;
                contextMenu_e.visibleIndex = e.index;
            
                var evt = e.htmlEvent;
                GridContexMenu.ShowAtPos(evt.clientX + DXGetDocumentScrollLeft(), evt.clientY + DXGetDocumentScrollTop());
                if (DXsafari)
                    evt.stopPropagation();
                else if (DXns)
                    evt.preventDefault();
                evt.cancelBubble = true;
                return false;
            }
        }

        //--右击菜单单击事件
        function OnContextMenuItemClick(s, e) {
            contextMenu_e.buttonID = e.item.name;
            OnCustomButtonClick(contextMenu_s, contextMenu_e);
        }

        function OnCustomButtonClick(s, e) {
            if (e.buttonID == "转出") {
                grid.GetRowValues(e.visibleIndex, "OID;EmpName", function(value) {
                    mmOID = value[0];
                    mmName = value[1];
                });
                popYG.Show();
            }
            else if (e.buttonID == "内退" || e.buttonID == "离职") {
                grid.GetRowValues(e.visibleIndex, "OID;FK_YouthGroup;EmpName", function(values) {
                    var mmOID = values[0];
                    var ygOID = values[1];
                    var empName = values[2];

                    if (confirm("您将对员工『" + empName + "』进行 " + e.buttonID + " 处理！")) {
                        grid.PerformCallback(e.buttonID + "|" + mmOID + "|" + ygOID + "|" + empName);
                    }
                });
            }
            else if (e.buttonID == "查看") {
                grid.GetRowValues(e.visibleIndex, "OID", function(value) {
                    mmOID = value;
                    var url = "../Report/YouthInfo.aspx";
                    var feature = "height=" + (screen.availHeight - 30) + ",width=900,left=0,top=0,scrollbars=yes";
                    window.open(url + "?Window=New&OID=" + value, "_blank", feature);
                });
            } 
            else if (e.buttonID == "编辑") {
                grid.GetRowValues(e.visibleIndex, "OID", function(value) {
                    var mmOID = value;
                    var url = "MemberEdit.aspx?Action=Edit&MM_OID=" + mmOID;

                    if (popEdit.GetContentUrl() != url)
                        popEdit.SetContentUrl(url);

                    edit = false;
                    popEdit.Show();

                });
            }
            else if (e.buttonID == "删除") {
                if(confirm("您确认要删除这个员工信息吗？"))
                    grid.DeleteRow(e.visibleIndex);
            }
        }

        function OnEditClose(s, e) {
            if (edit)
                grid.Refresh();
        }

        function NewMemeber(s, e) {
            var url = "MemberEdit.aspx?Action=New&YG_OID=" + hidYouthGroup.Get("OID");

            if (popEdit.GetContentUrl() != url)
                popEdit.SetContentUrl(url);

            edit = false;
            popEdit.Show();
            
        }
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="phContent" Runat="Server">
<%-- EndRegion --%>
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxhf:ASPxHiddenField ID="hidYouthGroup" runat="server" ClientInstanceName="hidYouthGroup"></dxhf:ASPxHiddenField>
                <dxe:ASPxComboBox ID="cbFZ" runat="server" AutoPostBack="true" ClientInstanceName="cbFZ" ></dxe:ASPxComboBox>
            </td>    
            <td class="buttonCell">
                <dxe:ASPxComboBox ID="cbLB" runat="server" AutoPostBack="true" ClientInstanceName="cbLB" SelectedIndex="0">
                    <Items>
                        <dxe:ListEditItem Text="" Value="所有青年" />
                        <dxe:ListEditItem Text="所有团员" Value="一二三类团员" />
                        <dxe:ListEditItem Text="一类团员" Value="一类团员" />
                        <dxe:ListEditItem Text="二类团员" Value="二类团员" />
                        <dxe:ListEditItem Text="三类团员" Value="三类团员" />
                        <dxe:ListEditItem Text="内退青年" Value="内退青年" />
                        <dxe:ListEditItem Text="离职青年" Value="离职青年" />
                    </Items>
                </dxe:ASPxComboBox>
            </td>
            <td class="buttonCell">
                <dxe:ASPxTextBox ID="txtSearch" runat="server" ClientInstanceName="txtSearch"></dxe:ASPxTextBox>
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnSearch" runat="server" Text="查询"></dxe:ASPxButton>
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnAddAdmin" runat="server" Text="新增" UseSubmitBehavior="False" AutoPostBack="false">
                    <ClientSideEvents Click="NewMemeber" />
                </dxe:ASPxButton>
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnUpload" runat="server" Text="上传照片" UseSubmitBehavior="false" AutoPostBack="false">
                    <ClientSideEvents Click="function(){ popUpload.Show(); }" />
                </dxe:ASPxButton>
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnExport" runat="server" Text="导出" UseSubmitBehavior="false" OnClick="btnExport_Click">
                </dxe:ASPxButton>
            </td>
        </tr>
    </table>
    <br />
    <dxpc:ASPxPopupControl ID="popUpload" Modal="true" runat="server" Width="400" Height="500" HeaderText="照片上传"
        ClientInstanceName="popUpload" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
        <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
             <dxuc:ASPxUploadControl ID="UploadControl" runat="server" ShowAddRemoveButtons="True" Width="350px"
                 ShowUploadButton="True" AddUploadButtonsHorizontalPosition="Left" ShowProgressPanel="True"
                 OnFileUploadComplete="UploadControl_FileUploadComplete"
                 ClientInstanceName="UploadControl"  FileInputCount="3">
                 <ValidationSettings AllowedContentTypes="image/jpeg, image/gif, image/pjpeg, image/jpg"></ValidationSettings>
                 <RemoveButton Text="删除"></RemoveButton>
                 <AddButton Text="添加"></AddButton>
                 <UploadButton Text="上传"></UploadButton>
             </dxuc:ASPxUploadControl>
        </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    
    <dxpc:ASPxPopupControl ID="popEdit" Modal="true" runat="server" Width="950" Height="520" HeaderText="团员信息编辑"
        ClientInstanceName="popEdit" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ContentUrl="MemberEdit.aspx">
       <ClientSideEvents Closing="OnEditClose" />
    </dxpc:ASPxPopupControl>
    
    <dxpc:ASPxPopupControl ID="popSE" Modal="true" runat="server" Width="400" Height="500" HeaderText="职称、技能等级选择器"
        ClientInstanceName="popSE" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
                <dxwgv:ASPxGridView ID="gridSE" ClientInstanceName="gridSE" runat="server" DataSourceID="OdsStandardEnum" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
                    <%-- BeginRegion Columns --%>
                    <Columns>
                        <dxwgv:GridViewDataComboBoxColumn FieldName="SE_TYPE" Caption="标准类别">
                        </dxwgv:GridViewDataComboBoxColumn> 
                        <dxwgv:GridViewDataTextColumn FieldName="SE_KEY" Caption="标准名称" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="选择" HeaderStyle-HorizontalAlign="Center" Width="80">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton Text="选择"></dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <ClientSideEvents CustomButtonClick="OnSESelected" />
                    <%-- EndRegion --%>
                    <Settings ShowGroupPanel="true" />
                    <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
                    <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！" />
                </dxwgv:ASPxGridView>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>
    <dxpc:ASPxPopupControl ID="popPost" Modal="true" runat="server" Width="400" Height="500" HeaderText="岗位选择器"
        ClientInstanceName="popPost" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dxpc:PopupControlContentControl runat="server">
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
    
    <dxpc:ASPxPopupControl ID="popYG" Modal="true" runat="server" Width="300" Height="500" HeaderText="团支部选择器"
        ClientInstanceName="popYG" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <dxwgv:ASPxGridView ID="gridYG" ClientInstanceName="gridYG" runat="server" DataSourceID="OdsYouthGroup" KeyFieldName="OID" AutoGenerateColumns="False" Width="100%">
                    <%-- BeginRegion Columns --%>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn FieldName="OID" Caption="OID" Visible="false"></dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="YG_NAME" Caption="支部名称" VisibleIndex="2">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewCommandColumn Name="OP_COL" VisibleIndex="5" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="80">
                            <CustomButtons>
                                <dxwgv:GridViewCommandColumnCustomButton Text="转入"></dxwgv:GridViewCommandColumnCustomButton>
                            </CustomButtons>
                        </dxwgv:GridViewCommandColumn>
                    </Columns>
                    <ClientSideEvents CustomButtonClick="OnYGSelected" />
                    <%-- EndRegion --%>
                    <Settings ShowGroupPanel="false" />
                    <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
                </dxwgv:ASPxGridView>
            </dxpc:PopupControlContentControl>
        </ContentCollection>
    </dxpc:ASPxPopupControl>    
    
    <dxwgv:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid" OnRenderBrick="gridExport_OnRenderBrick"></dxwgv:ASPxGridViewExporter>
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="OdsMember" KeyFieldName="OID" 
        OnCellEditorInitialize="grid_CellEditorInitialize" OnCustomColumnDisplayText="grid_CustomColumnDisplayText"
        AutoGenerateColumns="False" Width="930">
        <%-- BeginRegion Columns --%>
        <Columns>
            <dxwgv:GridViewCommandColumn Name="OP_COL" Caption="操作" Visible="false" HeaderStyle-HorizontalAlign="Center" Width="110" FixedStyle="Left">
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton Text="编辑"></dxwgv:GridViewCommandColumnCustomButton>
                    <dxwgv:GridViewCommandColumnCustomButton Text="转出"></dxwgv:GridViewCommandColumnCustomButton>
                    <dxwgv:GridViewCommandColumnCustomButton Text="查看"></dxwgv:GridViewCommandColumnCustomButton>
                </CustomButtons>                
                <DeleteButton Visible="True" Text="删除" />
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn FieldName="OID" Caption="团员OID" Visible="false"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="EmpName"  Caption="姓名" FixedStyle="Left" Width="50">
                <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="姓名必须填写！">
                </PropertiesTextEdit>
                <Settings AllowHeaderFilter="False" />
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="HrCode"  Caption="工号" Width="50">
                <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="工号必须填写！">
                    <ValidationSettings RegularExpression-ValidationExpression="[a-zA-Z]\d{5,6}" RegularExpression-ErrorText="请输入正确的工号！"></ValidationSettings>
                </PropertiesTextEdit>
                <Settings AllowHeaderFilter="False" />
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="Sex"  Caption="性别" Width="50">
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="Dept" Caption="部门" Width="90"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="WorkGroup" Caption="科室/作业区" Width="90"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Post" Caption="岗位" Width="90"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="ParttimeName" Caption="兼职职务" Width="80"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Politics" Caption="政治面貌" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="PartyDate"  Caption="入党日期" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Wedding"  Caption="婚姻状况" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Nation"  Caption="民族"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="NativePlace"  Caption="籍贯"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Mobile"  Caption="手机号码">
                <Settings AllowHeaderFilter="False" />
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="EmpID"  Caption="身份证号">
                <Settings AllowHeaderFilter="False" />
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="House"  Caption="住房情况"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Birthday"  Caption="出生日期" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="JobDateTime"  Caption="参加工作" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="ComDateTime"  Caption="入厂年月" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="FstSchoolExp"  Caption="第一学历"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="FstDegree"  Caption="第一学位"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="LstSchoolExp"  Caption="最高学历"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="LstDegree"  Caption="最高学位"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="SkillLevel"  Caption="职称/技术等级"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="VolunteerInfo"  Caption="志愿者信息"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="SpecialSkill"  Caption="特长"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="ApplyParty"  Caption="是否申请入党"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="ApplyPartyDate"  Caption="入厂年月" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="FstSchool"  Caption="第一学历院校"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="FstProfession"  Caption="毕业专业"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="FstGraduateDate"  Caption="毕业时间" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="LstSchool"  Caption="最高学历院校"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="LstProfession"  Caption="毕业专业"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="LstGraduateDate"  Caption="毕业时间" Width="70"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="HrType"  Caption="员工性质"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="HrStatus"  Caption="员工状态"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="YouthChargeStd"  Caption="团费标准"></dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="FK_YouthGroup_FZ" Caption="所属分支">
                <PropertiesComboBox TextField="YG_NAME" ValueField="OID" DataSourceID="OdsYouthGroupFZ" Width="180"></PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Email"  Caption="电子邮箱"></dxwgv:GridViewDataTextColumn> 

        </Columns>
        <ClientSideEvents CustomButtonClick="OnCustomButtonClick" ContextMenu="OnGridContextMenu" RowDblClick="function(s, e){
            e.buttonID = '查看';
             OnCustomButtonClick(s, e);
        }"  />
        <%-- EndRegion --%>
        <Settings ShowGroupPanel="false" ShowFilterRow="false" ShowHeaderFilterButton="true" ShowHorizontalScrollBar="true" ShowVerticalScrollBar="true" VerticalScrollBarStyle="Virtual" UseFixedTableLayout="true" VerticalScrollableHeight="500" />
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="930px" NewItemRowPosition="Top" EditFormColumnCount="3" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormHorizontalAlign="WindowCenter" />
        <SettingsPager AlwaysShowPager="True" PageSize="20" Mode="ShowPager" ShowNumericButtons="True" NextPageButton-Visible="True" PrevPageButton-Visible="True" /> 
        <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑用户" />
    </dxwgv:ASPxGridView>
    <asp:ObjectDataSource ID="OdsMember" runat="server" 
        SelectMethod="SearchMember" InsertMethod="MyInsert" UpdateMethod="MyUpdate" DeleteMethod="MyDelete"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.MemberTableAdapter" >
        <SelectParameters>
            <asp:QueryStringParameter Name="YG_OID" QueryStringField="OID" Type="String" />
            <asp:ControlParameter Name="FZ_OID" ControlID="cbFZ" Type="String" PropertyName="Value" />
            <asp:ControlParameter Name="SH_TEXT" ControlID="txtSearch" Type="String" PropertyName="Value" />
            <asp:ControlParameter Name="SH_LB" ControlID="cbLB" Type="String" PropertyName="Value" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsStandardEnum" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.StandardEnumTableAdapter" 
        SelectMethod="GetSpecialSkillCache" >
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsPost" runat="server" 
        DataObjectTypeName="YouthOne.Component.YouthOneDS.MemberRow"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.PostTableAdapter" 
        SelectMethod="GetData" >
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsYouthGroup" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.YouthGroupTableAdapter" 
        SelectMethod="GetZBSpecial" >
        <SelectParameters>
            <asp:QueryStringParameter Name="YG_OID" QueryStringField="OID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsYouthGroupFZ" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.YouthGroupTableAdapter" 
        SelectMethod="GetFZCache" >
        <SelectParameters>
            <asp:QueryStringParameter QueryStringField="OID" Type="String" Name="YG_OID" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
  <dxm:ASPxPopupMenu ID="GridContexMenu" runat="server" ClientInstanceName="GridContexMenu"  ShowPopOutImages="True" AutoPostBack="False"  PopupHorizontalAlign="OutsideRight" PopupVerticalAlign="TopSides" PopupAction="LeftMouseClick">
     <Items>
         <dxm:MenuItem Text="查看" Name="查看"></dxm:MenuItem>
         <dxm:MenuItem Text="编辑" Name="编辑"></dxm:MenuItem>
         <dxm:MenuItem Text="转出" Name="转出"></dxm:MenuItem>
         <dxm:MenuItem Text="内退" Name="内退"></dxm:MenuItem>
         <dxm:MenuItem Text="离职" Name="离职"></dxm:MenuItem>
         <dxm:MenuItem Text="删除" Name="删除"></dxm:MenuItem>
     </Items>
     <ClientSideEvents ItemClick="OnContextMenuItemClick"/>
     <ItemStyle Width="100px">
     </ItemStyle>
 </dxm:ASPxPopupMenu>

<%-- EndRegion --%>
</asp:Content>


