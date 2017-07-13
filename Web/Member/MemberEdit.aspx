<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberEdit.aspx.cs" Inherits="Member_MemberEdit" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTabControl" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxClasses" tagprefix="dxw" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxHiddenField" TagPrefix="dxhf" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v9.1, Version=9.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxUploadControl" TagPrefix="dxuc" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>团员信息编辑</title>
    <script type="text/javascript">
        function OnCancel(s, e) {
            parent.popEdit.Hide();
        }

        function OnOK(s, e) {
            parent.edit = true;
        }
    
        function OnSEClick(s, e) {
            parent.popSE.Show();
            parent.seEditor = SkillLevel;
        }

        function OnPostClick(s, e) {
            parent.popPost.Show();
            parent.postEditor = Post;
        }
        
        function OnDeptChange(s, e) {
            WorkGroup.PerformCallback(s.GetValue().toString());
        }

        function OnYGChange(s, e) {
            FK_YouthGroup_FZ.PerformCallback(s.GetValue().toString());
        }

        function OnPoliticsChanged(s, e) {
            var politics = s.GetValue().toString();

            if (politics == "中共预备党员" || politics == "中共党员") {
                PartyDate.SetEnabled(true);
                
            }
            else {
                PartyDate.SetEnabled(false);
                PartyDate.SetDate(null);
            }

            if (!PartyDate.GetDate() || PartyDate.GetDate().getFullYear() <= 1900) PartyDate.SetDate(null);

            if (politics == "共青团员") {
                YouthChargeStd.SetEnabled(true);
                OnHrTypeChange(null, null);
            } else {
                YouthChargeStd.SetEnabled(false);
                YouthChargeStd.SetValue(0);
            }
        }

        function OnApplyParty(s, e) {
            var applyParty = s.GetValue().toString();
            if (applyParty == "是") {
                ApplyPartyDate.SetEnabled(true);

            }
            else {
                ApplyPartyDate.SetEnabled(false);
                ApplyPartyDate.SetDate(null);
            }
        }

        //--格式化时间
        function OnInitFormatDate(s, e) {
            if (!s.GetDate() || s.GetDate().getFullYear() == 1900) s.SetDate(null);
        }
        function OnInitFormatDate1(s, e) {
            if (!s.GetDate() || s.GetDate().getFullYear() == 1900) s.SetDate(null);
        }

        function OnPriseNameChange(s, e) {
            var prVal = s.GetSelectedItem().GetColumnText(1);
            prValue.SetValue(prVal);
        }

        function OnHrTypeChange(s, e) {
            var hrType = HrType.GetValue();
            var ygCharge;

            if (hrType == '在册员工' || hrType == '委派员工' || hrType == '商借员工') {
                ygCharge = 10;
            }
            else if (hrType == '准员工' || hrType == '协力工') {
                ygCharge = 8;
            }
            else if(hrType == '劳务工'){
                ygCharge = 3;
            }

            YouthChargeStd.SetValue(ygCharge);
        }
    </script>
</head>
<body style="padding: 0px; margin:0px; background-image: none; background-color:Transparent">
    <form id="form1" runat="server">
    <div>
        <asp:HiddenField ID="txtOID" runat="server" />
    
        <dxtc:ASPxPageControl runat="server" ID="pageControl" Width="100%" Height="100%" EnableCallBacks="True">
            <TabPages>
                <dxtc:TabPage Text="基本信息" Visible="true">
                 <ContentCollection>
                    <dxw:ContentControl ID="ContentControl1" runat="server">
                        <table cellpadding="3" cellspacing="0">
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel1" Text="姓名" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="EmpName" ClientInstanceName="EmpName" Width="180">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true"  RequiredField-ErrorText="姓名必须填写！"></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel2" Text="工号" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="HrCode" ClientInstanceName="HrCode" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="工号必须填写！"></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel3" Text="性别" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="Sex" ClientInstanceName="Sex" Width="180" AutoResizeWithContainer="True">
                                        <Items>
                                            <dxe:ListEditItem Text="男" Value="男" />
                                            <dxe:ListEditItem Text="女" Value="女" />
                                        </Items>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="性别必须填写！"></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel4" Text="部门" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="Dept" DataSourceID="OdsDept" TextField="OG_NAME" ValueField="OG_NAME"
                                        ClientInstanceName="Dept" Width="180" AutoResizeWithContainer="True" ClientSideEvents-ValueChanged="OnDeptChange">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="部门必须填写！"></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel5" Text="科室/作业区" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="WorkGroup" ClientInstanceName="WorkGroup" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel6" Text="岗位" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxButtonEdit runat="server" ID="Post" ClientInstanceName="Post" Width="180" AutoResizeWithContainer="True">
                                        <Buttons>
                                            <dxe:EditButton Text=""></dxe:EditButton>
                                        </Buttons>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                        <ClientSideEvents ButtonClick="OnPostClick" />
                                    </dxe:ASPxButtonEdit>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel7" Text="兼职职务" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="ParttimeName" ClientInstanceName="ParttimeName" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel8" Text="政治面貌" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="Politics" ClientInstanceName="Politics" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="政治面貌必须填写！"></ValidationSettings>
                                    
                                        <ClientSideEvents ValueChanged="OnPoliticsChanged" Init="OnPoliticsChanged" />
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel9" Text="入党日期" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxDateEdit runat="server" ID="PartyDate" ClientInstanceName="PartyDate" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxDateEdit>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel10" Text="婚姻状况" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="Wedding" ClientInstanceName="Wedding" Width="180" AutoResizeWithContainer="True">
                                        <Items>
                                            <dxe:ListEditItem Text="已婚" Value="已婚" />
                                            <dxe:ListEditItem Text="未婚" Value="未婚" />
                                            <dxe:ListEditItem Text="离异" Value="离异" />
                                        </Items>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="婚姻状况必须填写！"></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel11" Text="民族" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="Nation" ClientInstanceName="Nation" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel12" Text="籍贯" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="NativePlace" ClientInstanceName="NativePlace" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel13" Text="手机号码" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="Mobile" ClientInstanceName="Mobile" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel14" Text="身份证号" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="EmpID" ClientInstanceName="EmpID" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel15" Text="住房情况" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="House" ClientInstanceName="House" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel16" Text="生日日期" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxDateEdit runat="server" ID="Birthday" ClientInstanceName="Birthday" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                        <ClientSideEvents Init="OnInitFormatDate1" />
                                    </dxe:ASPxDateEdit>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel17" Text="参加工作" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxDateEdit runat="server" ID="JobDateTime" ClientInstanceName="JobDateTime" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                        <ClientSideEvents Init="OnInitFormatDate" />
                                    </dxe:ASPxDateEdit>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel18" Text="入厂年月" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxDateEdit runat="server" ID="ComDateTime" ClientInstanceName="ComDateTime" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                        <ClientSideEvents Init="OnInitFormatDate" />
                                    </dxe:ASPxDateEdit>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel19" Text="第一学历" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="FstSchoolExp" ClientInstanceName="FstSchoolExp" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel20" Text="第一学位" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="FstDegree" ClientInstanceName="FstDegree" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel21" Text="最高学历" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="LstSchoolExp" ClientInstanceName="LstSchoolExp" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel22" Text="最高学位" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="LstDegree" ClientInstanceName="LstDegree" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel23" Text="职称/技术等级" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxButtonEdit runat="server" ID="SkillLevel" ClientInstanceName="SkillLevel" Width="180" AutoResizeWithContainer="True">
                                        <Buttons>
                                            <dxe:EditButton Text=""></dxe:EditButton>
                                        </Buttons>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip"></ValidationSettings>
                                        <ClientSideEvents ButtonClick="OnSEClick" />
                                    </dxe:ASPxButtonEdit>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel24" Text="志愿者信息" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="VolunteerInfo" ClientInstanceName="VolunteerInfo" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel25" Text="特长" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="SpecialSkill" ClientInstanceName="SpecialSkill" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel26" Text="是否申请入党" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="ApplyParty" ClientInstanceName="ApplyParty" Width="180">
                                        <Items>
                                            <dxe:ListEditItem Text="是" Value="是" />
                                            <dxe:ListEditItem Text="否" Value="否" />
                                        </Items>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                        <ClientSideEvents ValueChanged="OnApplyParty" />
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel27" Text="申请入党时间" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxDateEdit runat="server" ID="ApplyPartyDate" ClientInstanceName="ApplyPartyDate" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                        <ClientSideEvents Init="OnInitFormatDate" />
                                    </dxe:ASPxDateEdit>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel28" Text="第一学历院校" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="FstSchool" ClientInstanceName="FstSchool" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel29" Text="毕业专业" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="FstProfession" ClientInstanceName="FstProfession" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel30" Text="毕业时间" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                     <dxe:ASPxDateEdit runat="server" ID="FstGraduateDate" ClientInstanceName="FstGraduateDate" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                        <ClientSideEvents Init="OnInitFormatDate" />
                                    </dxe:ASPxDateEdit>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel31" Text="最高学历院校" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="LstSchool" ClientInstanceName="LstSchool" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel32" Text="毕业专业" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="LstProfession" ClientInstanceName="LstProfession" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel33" Text="毕业时间" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                     <dxe:ASPxDateEdit runat="server" ID="LstGraduateDate" ClientInstanceName="LstGraduateDate" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                        <ClientSideEvents Init="OnInitFormatDate" />
                                    </dxe:ASPxDateEdit>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel34" Text="员工性质" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="HrType" ClientInstanceName="HrType" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="员工性质必须填写！"></ValidationSettings>
                                        <ClientSideEvents SelectedIndexChanged="OnHrTypeChange" />
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel35" Text="员工状态" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="HrStatus" ClientInstanceName="HrStatus" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="员工状态必须填写！"></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel36" Text="团费标准" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="YouthChargeStd" ClientInstanceName="YouthChargeStd" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="团费标准必须填写！"></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>                            
                            </tr>
                            <tr>
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel37" Text="所属支部" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="FK_YouthGroup" DataSourceID="OdsYG" TextField="YG_NAME" ValueField="OID"
                                        ClientInstanceName="FK_YouthGroup" Width="180" AutoResizeWithContainer="True" >
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" RequiredField-IsRequired="true" RequiredField-ErrorText="所属支部必须填写！"></ValidationSettings>
                                        <ClientSideEvents Init= "OnYGChange" ValueChanged="OnYGChange" />
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel38" Text="所属分支" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxComboBox runat="server" ID="FK_YouthGroup_FZ" ClientInstanceName="FK_YouthGroup_FZ" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxComboBox>
                                </td>  
                                <td style="width:120px"><dxe:ASPxLabel ID="ASPxLabel39" Text="电子邮箱" runat="server"></dxe:ASPxLabel></td>
                                <td>
                                    <dxe:ASPxTextBox runat="server" ID="Email" ClientInstanceName="Email" Width="180" AutoResizeWithContainer="True">
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" ></ValidationSettings>
                                    </dxe:ASPxTextBox>
                                </td>                            
                            </tr>
                            <tr>
                                <td> </td>
                                <td> </td>
                                <td> </td>
                                <td> </td>
                                <td> </td>
                                <td>
                                    <table cellpadding="5" cellspacing="0">
                                        <tr>
                                            <td style="width:60px;"></td>
                                            <td>
                                                <dxe:ASPxButton Text="确定" ID="btnOK" runat="server" ClientSideEvents-Click="OnOK"></dxe:ASPxButton>
                                            </td>
                                            <td>
                                                <dxe:ASPxButton Text="关闭" ID="btnCancel" runat="server" AutoPostBack="false" UseSubmitBehavior="false" ClientSideEvents-Click="OnCancel"></dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </dxw:ContentControl>
                 </ContentCollection>
                </dxtc:TabPage>
                <dxtc:TabPage Text="奖励记录"  Visible="true">
                    <ContentCollection>
                        <dxw:ContentControl ID="ctlPrise" runat="server">
                            <dxwgv:ASPxGridView ID="gridPrise" ClientInstanceName="gridPrise" runat="server" DataSourceID="OdsPrise" KeyFieldName="OID" 
                                OnCellEditorInitialize="gridPrise_CellEditorInitialize" 
                                AutoGenerateColumns="False" Width="100%" >
                                <Columns>
                                    <dxwgv:GridViewCommandColumn Name="OP_COL" Caption="操作" HeaderStyle-HorizontalAlign="Center" Width="60">
                                        <NewButton Visible="true" Text="新增"></NewButton>
                                        <EditButton Visible="True" Text="编辑" />
                                        <DeleteButton Visible="True" Text="删除" />
                                    </dxwgv:GridViewCommandColumn>
                                    <dxwgv:GridViewDataComboBoxColumn FieldName="PR_NAME" Caption="奖励名称"  Width="30%" >
                                        <PropertiesComboBox 
                                        ValidationSettings-RequiredField-IsRequired="true" 
                                        ValidationSettings-RequiredField-ErrorText="奖励名称必须填写！" TextFormatString="{0}">
                                            <ClientSideEvents ValueChanged="OnPriseNameChange" />
                                            <Columns>
                                                <dxe:ListBoxColumn Caption="奖励名称" FieldName="SE_KEY" Name="SE_KEY" />
                                                <dxe:ListBoxColumn Caption="标准金额" FieldName="SE_VALUE" Name="SE_VALUE" />
                                            </Columns>
                                        </PropertiesComboBox>
                                    </dxwgv:GridViewDataComboBoxColumn>
                                    <dxwgv:GridViewDataSpinEditColumn FieldName="PR_VALUE" Caption="奖励金额" >
                                        <PropertiesSpinEdit ClientInstanceName="prValue" DecimalPlaces="2" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="奖励金额必须填写！">
                                        
                                        </PropertiesSpinEdit>
                                    </dxwgv:GridViewDataSpinEditColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="PR_UNIT" Caption="授奖单位"  Width="30%">
                                        <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="授奖单位必须填写！">
                                        </PropertiesTextEdit>
                                    </dxwgv:GridViewDataTextColumn>
                                    <dxwgv:GridViewDataDateColumn FieldName="PR_DATE" Caption="获奖时间">
                                        <PropertiesDateEdit ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-RequiredField-ErrorText="获奖时间必须填写！">
                                        </PropertiesDateEdit>
                                    </dxwgv:GridViewDataDateColumn>
                                    <dxwgv:GridViewDataTextColumn FieldName="FK_Member" Caption="团员" Visible="false"></dxwgv:GridViewDataTextColumn>
 
                                </Columns>
                                <Settings ShowGroupPanel="false" />
                                <SettingsPager AlwaysShowPager="true" PageSize="20" /> 
                                <SettingsText GroupPanel="拖动表头到此处，可以进行汇总" EmptyDataRow="暂无数据！" ConfirmDelete="您确定要删除吗！" CommandCancel="取消" CommandUpdate="保存" PopupEditFormCaption="编辑用户" />
                            
                            </dxwgv:ASPxGridView>
                        </dxw:ContentControl>
                    </ContentCollection>
                 </dxtc:TabPage>
            </TabPages>
        </dxtc:ASPxPageControl>
        
        <asp:ObjectDataSource ID="OdsDept" runat="server" 
            TypeName="YouthOne.Component.YouthOneDSTableAdapters.OrganizeTableAdapter" 
            SelectMethod="GetDeptCache" >
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="OdsYG" runat="server" 
            TypeName="YouthOne.Component.YouthOneDSTableAdapters.YouthGroupTableAdapter" 
            SelectMethod="GetZBCache" >
        </asp:ObjectDataSource>
        
        <asp:ObjectDataSource ID="OdsPrise" runat="server" 
            SelectMethod="GetData" InsertMethod="MyInsert" UpdateMethod="MyUpdate" DeleteMethod="MyDelete"
            TypeName="YouthOne.Component.YouthOneDSTableAdapters.PriseTableAdapter" >
        <SelectParameters>
            <asp:ControlParameter Name="memberOID" ControlID="txtOID" PropertyName="Value" Type="String" DefaultValue="----" />
        </SelectParameters>
        <InsertParameters>
            <asp:Parameter Name="PR_NAME" Type="String" />
            <asp:Parameter Name="PR_VALUE" Type="Decimal" />
            <asp:Parameter Name="PR_UNIT" Type="String" />
            <asp:Parameter Name="PR_DATE" Type="DateTime" />
            <asp:ControlParameter Name="FK_Member" ControlID="txtOID" PropertyName="Value" Type="String" />
        </InsertParameters>
    </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
