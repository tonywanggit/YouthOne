<%@ Page Title="" Language="C#" MasterPageFile="~/Demo.master" AutoEventWireup="true" CodeFile="MemberImport.aspx.cs" Inherits="Member_MemberImport" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dxuc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="localCssPlaceholder" Runat="Server">
    <style type="text/css">
        td.buttonCell {
            padding-right: 6px;
        }
    </style>
    <script type="text/javascript">
        var postEditor; //--岗位编辑器
        var seEditor;   //--职称/技能等级编辑器
        var edit = true;    //--编辑器是否编辑过
    
        function Uploader_OnUploadStart() {
            btnUpload.SetText("上传中...");
            btnUpload.SetEnabled(false);
        }
        function Uploader_OnUploadComplete(args) {
            btnUpload.SetEnabled(true);
            btnUpload.SetText("上传");
            grid.Refresh();

            if (args.callbackData)
                alert(args.callbackData);
        }

        function OnCustomButtonClick(s, e) {
            if (e.buttonID == "编辑") {
                grid.GetRowValues(e.visibleIndex, "OID", function(value) {
                    var mmOID = value;
                    var url = "MemberEdit.aspx?Action=Import&MI_OID=" + mmOID;
                    
                    if (popEdit.GetContentUrl() != url)
                        popEdit.SetContentUrl(url);
                        
                    popEdit.Show();

                });
            }
        }

        function OnSESelected(s, e) {
            popSE.Hide();
            gridSE.GetRowValues(e.visibleIndex, "SE_KEY", function(value) {
                seEditor.SetValue(value);
            });
        }

        function OnPostSelected(s, e) {
            popPost.Hide();
            gridPost.GetRowValues(e.visibleIndex, "POST_NAME", function(value) {
                postEditor.SetValue(value);
            });
        }
        
        function OnEditClose(s, e) {
            if (edit)
                grid.Refresh();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phContent" Runat="Server">
    <table cellpadding="0" cellspacing="0">
        <tr>
            <td class="buttonCell">
                <dxuc:ASPxUploadControl ID="uplExcel" runat="server" ClientInstanceName="uploader"
                     Size="35" OnFileUploadComplete="uplExcel_FileUploadComplete"> 
                     <ClientSideEvents FileUploadComplete="function(s, e) { Uploader_OnUploadComplete(e); }" FileUploadStart="function(s, e) { Uploader_OnUploadStart(); }"></ClientSideEvents>
                     <ValidationSettings MaxFileSize="4000000"  NotAllowedContentTypeErrorText="只允许上传Excel文档！">
                     </ValidationSettings>
                 </dxuc:ASPxUploadControl>
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnUpload" runat="server" AutoPostBack="false" Text="上传" ClientInstanceName="btnUpload">
                    <ClientSideEvents Click="function(s, e) { uploader.UploadFile(); }" />
                </dxe:ASPxButton>
            </td>
            <td class="buttonCell">
                <dxe:ASPxButton ID="btnDownload" runat="server" AutoPostBack="false" Text="下载模板">
                    <ClientSideEvents Click="function(s, e) { document.getElementById('template').click(); }" />
                </dxe:ASPxButton>
                <a id="template" href="YouthOneTemplate.xlsx"></a>
            </td>
            <td class="buttonCell">
                <dxe:ASPxHyperLink ID="linkTemplate" ClientInstanceName="linkTemplate" Visible="false" runat="server" NavigateUrl="YouthOneTemplate.xlsx" Text="下载模板"></dxe:ASPxHyperLink>
            </td>
        </tr>
    </table>
    <br />
    <dxpc:ASPxPopupControl ID="popSE" Modal="true" runat="server" Width="400" Height="500" HeaderText="职称、技能等级选择器"
        ClientInstanceName="popSE" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
        <ContentCollection>
            <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
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
            <dxpc:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
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
    
    <dxpc:ASPxPopupControl ID="popEdit" Modal="true" runat="server" Width="960" Height="520" HeaderText="团员信息编辑"
        ClientInstanceName="popEdit" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" ContentUrl="MemberEdit.aspx">
       <ClientSideEvents Closing="OnEditClose" />
    </dxpc:ASPxPopupControl>
    
    <dxwgv:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" KeyFieldName="OID"  DataSourceID="OdsMemberImport"
            AutoGenerateColumns="False" Width="930">
        <Columns>
            <dxwgv:GridViewCommandColumn Name="OP_COL" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="60" FixedStyle="Left">
                <DeleteButton Visible="True" Text="删除" />
                <CustomButtons>
                    <dxwgv:GridViewCommandColumnCustomButton Text="编辑"></dxwgv:GridViewCommandColumnCustomButton>
                </CustomButtons>
            </dxwgv:GridViewCommandColumn>
            <dxwgv:GridViewDataTextColumn FieldName="OID" Caption="团员OID" Visible="false"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="ExcelNum" Caption="Excel行数" Visible="true"></dxwgv:GridViewDataTextColumn>
            <dxwgv:GridViewDataTextColumn FieldName="ErrorDesc" Caption="错误提示" Visible="true" Width="150" PropertiesTextEdit-EncodeHtml="false"></dxwgv:GridViewDataTextColumn>
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
            <dxwgv:GridViewDataComboBoxColumn FieldName="Sex"  Caption="性别" Width="50">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="性别必须填写！">
                    <Items>
                        <dxe:ListEditItem Text="男" Value="男" />
                        <dxe:ListEditItem Text="女" Value="女" />
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="Dept"  Caption="部门" Width="90">
                <PropertiesComboBox
                    ClientInstanceName="cmbDept" ClientSideEvents-ValueChanged="OnDeptChange"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="部门必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="WorkGroup"  Caption="科室/作业区" Width="90">
                <PropertiesComboBox EnableSynchronization="False" Width="180">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 

            <dxwgv:GridViewDataButtonEditColumn FieldName="Post"  Caption="岗位" Width="90">
                <PropertiesButtonEdit Width="180">
                    <Buttons>
                        <dxe:EditButton Text=""></dxe:EditButton>
                    </Buttons>
                    <ClientSideEvents ButtonClick="OnPostClick" />
                </PropertiesButtonEdit>
            </dxwgv:GridViewDataButtonEditColumn>
            
            <dxwgv:GridViewDataComboBoxColumn FieldName="ParttimeName"  Caption="兼职职务" Width="80">
                <PropertiesComboBox
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="兼职职务必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="Politics"  Caption="政治面貌" Width="70">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="政治面貌必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataDateColumn FieldName="PartyDate" Caption="入党日期" Width="70">
                <PropertiesDateEdit Width="180"></PropertiesDateEdit>
                <Settings AllowHeaderFilter="False" />
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataComboBoxColumn FieldName="Wedding"  Caption="婚姻状况" Width="70">
                <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="婚姻状况必须填写！">
                    <Items>
                        <dxe:ListEditItem Text="已婚" Value="已婚" />
                        <dxe:ListEditItem Text="未婚" Value="未婚" />
                        <dxe:ListEditItem Text="离异" Value="离异" />
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="Nation"  Caption="民族">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="民族必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="NativePlace"  Caption="籍贯">
                <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="籍贯必须填写！">
                </PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="Mobile"  Caption="手机号码">
                <PropertiesTextEdit >
                    <ValidationSettings RegularExpression-ValidationExpression="\d{8,11}" RegularExpression-ErrorText="请输入正确的手机号码！"></ValidationSettings>
                </PropertiesTextEdit>
                <Settings AllowHeaderFilter="False" />
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="EmpID"  Caption="身份证号">
                <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="身份证号必须填写！">
                    <ValidationSettings RegularExpression-ValidationExpression="[1-9]\d{16}[0-9X]" RegularExpression-ErrorText="请输入正确的身份证号！"></ValidationSettings>
                </PropertiesTextEdit>
                <Settings AllowHeaderFilter="False" />
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="House"  Caption="住房情况">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="住房情况必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>  
            <dxwgv:GridViewDataDateColumn FieldName="Birthday" Caption="出生日期">
                <PropertiesDateEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="出生日期必须填写！">
                </PropertiesDateEdit>
                <Settings AllowHeaderFilter="False" />
            </dxwgv:GridViewDataDateColumn> 
            <dxwgv:GridViewDataDateColumn FieldName="JobDateTime" Caption="参加工作">
                <PropertiesDateEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="参加工作日期必须填写！">
                </PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn> 
            <dxwgv:GridViewDataDateColumn FieldName="ComDateTime" Caption="入厂年月">
                <PropertiesDateEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="入厂年月必须填写！">
                </PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataComboBoxColumn FieldName="FstSchoolExp"  Caption="第一学历">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="第一学历必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="FstDegree"  Caption="第一学位">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="第一学位必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="LstSchoolExp"  Caption="最高学历">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="最高学历必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="LstDegree"  Caption="最高学位">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="最高学位必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataButtonEditColumn FieldName="SkillLevel"  Caption="职称/技术等级">
                <PropertiesButtonEdit Width="180">
                    <Buttons>
                        <dxe:EditButton Text=""></dxe:EditButton>
                    </Buttons>
                    <ClientSideEvents ButtonClick="OnSEClick" />
                </PropertiesButtonEdit>
            </dxwgv:GridViewDataButtonEditColumn>
            <dxwgv:GridViewDataTextColumn FieldName="VolunteerInfo"  Caption="志愿者信息">
                <PropertiesTextEdit Width="180"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="SpecialSkill"  Caption="特长">
                <PropertiesTextEdit Width="180"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="ApplyParty" Caption="是否申请入党">
                <PropertiesComboBox Width="180">
                    <Items>
                        <dxe:ListEditItem Text="是" Value="是" />
                        <dxe:ListEditItem Text="否" Value="否" />
                    </Items>
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataDateColumn FieldName="ApplyPartyDate" Caption="申请入党时间">
                <PropertiesDateEdit Width="180"></PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn>
            <dxwgv:GridViewDataTextColumn FieldName="FstSchool"  Caption="第一学历院校">
                <PropertiesTextEdit Width="180"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="FstProfession"  Caption="毕业专业">
                <PropertiesTextEdit Width="180"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataDateColumn FieldName="FstGraduateDate"  Caption="毕业时间">
                <PropertiesDateEdit Width="180"></PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="LstSchool"  Caption="最高学历院校">
                <PropertiesTextEdit Width="180"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataTextColumn FieldName="LstProfession"  Caption="毕业专业">
                <PropertiesTextEdit Width="180"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn> 
            <dxwgv:GridViewDataDateColumn FieldName="LstGraduateDate"  Caption="毕业时间">
                <PropertiesDateEdit Width="180"></PropertiesDateEdit>
            </dxwgv:GridViewDataDateColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="HrType"  Caption="员工性质">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="员工性质必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="HrStatus"  Caption="员工状态">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="员工状态必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="YouthChargeStd"  Caption="团费标准">
                <PropertiesComboBox  TextField="SE_KEY" ValueField="SE_VALUE"
                    ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="团费标准必须填写！">
                </PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn> 
            <dxwgv:GridViewDataComboBoxColumn FieldName="FK_YouthGroup_FZ" Caption="所属分支">
                <PropertiesComboBox TextField="YG_NAME" ValueField="OID"  Width="180"></PropertiesComboBox>
            </dxwgv:GridViewDataComboBoxColumn>
            <dxwgv:GridViewDataTextColumn FieldName="Email"  Caption="电子邮箱">
                <PropertiesTextEdit Width="180"></PropertiesTextEdit>
            </dxwgv:GridViewDataTextColumn> 

        </Columns>
        <ClientSideEvents CustomButtonClick="OnCustomButtonClick" />
        <Settings ShowGroupPanel="false" ShowFilterRow="false" ShowHeaderFilterButton="true" ShowHorizontalScrollBar="true" UseFixedTableLayout="true" />
        <SettingsBehavior ConfirmDelete="true" />
        <SettingsEditing Mode="PopupEditForm" PopupEditFormWidth="930px" NewItemRowPosition="Top" EditFormColumnCount="3" PopupEditFormVerticalAlign="WindowCenter" PopupEditFormHorizontalAlign="WindowCenter" />
        <SettingsPager AlwaysShowPager="true" PageSize="30" /> 
        <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑用户" />
    </dxwgv:ASPxGridView>
    <asp:ObjectDataSource ID="OdsMemberImport" runat="server" 
        SelectMethod="MySelect" DeleteMethod="MyDelete"
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.MemberImportTableAdapter" >
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsStandardEnum" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.StandardEnumTableAdapter" 
        SelectMethod="GetSpecialSkillCache" >
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="OdsPost" runat="server" 
        TypeName="YouthOne.Component.YouthOneDSTableAdapters.PostTableAdapter" 
        SelectMethod="GetDataCache" >
    </asp:ObjectDataSource>
</asp:Content>

