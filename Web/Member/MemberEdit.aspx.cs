using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component.YouthOneDSTableAdapters;
using DevExpress.Web.ASPxEditors;
using YouthOne.Component;
using DevExpress.Web.ASPxTabControl;
using DevExpress.Web.ASPxGridView;

public partial class Member_MemberEdit : BasePage
{
    private StandardEnumTableAdapter seAdapter = new StandardEnumTableAdapter();
    private OrganizeTableAdapter ogAdapter = new OrganizeTableAdapter();
    private MemberTableAdapter mmAdapter = new MemberTableAdapter();
    private YouthGroupTableAdapter ygAdapter = new YouthGroupTableAdapter();
    private MemberImportTableAdapter miAdapter = new MemberImportTableAdapter();
    private PostTableAdapter psAdapter = new PostTableAdapter();

    private string[] formFields = { "FK_YouthGroup", "FK_YouthGroup_FZ", "Dept", "WorkGroup", "Post", "HrCode", "HrType", "HrStatus", "EmpName", "EmpID", "Sex", "Birthday", "ParttimeName", "JobDateTime", "ComDateTime", "Wedding", "NativePlace", "Politics", "PartyDate", "Nation", "House", "SkillLevel", "FstSchoolExp", "FstDegree", "LstSchoolExp", "LstDegree", "FstSchool", "FstProfession", "FstGraduateDate", "LstSchool", "LstProfession", "LstGraduateDate", "ApplyParty", "ApplyPartyDate", "VolunteerInfo", "Mobile", "Email", "SpecialSkill", "YouthChargeStd"};
    string[] cmbArray = { "Politics", "Nation", "HrType", "HrStatus", "FstSchoolExp", "FstDegree", "LstSchoolExp", "LstDegree", "House", "YouthChargeStd", "ParttimeName" };
    string[] stdArray = { "政治面貌", "民族", "员工性质", "员工状态", "学历", "学位", "学历", "学位", "住房情况", "团费标准", "兼职职务" };
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitStdEnumCombox();
            InitEditForm();
        }

        if (Request["Action"] == "Import" || String.IsNullOrEmpty(txtOID.Value)) pageControl.TabPages[1].Enabled = false;

        WorkGroup.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(WorkGroup_Callback);
        FK_YouthGroup_FZ.Callback += new DevExpress.Web.ASPxClasses.CallbackEventHandlerBase(FK_YouthGroup_FZ_Callback);
        btnOK.Click += new EventHandler(btnOK_Click);
    }

    void btnOK_Click(object sender, EventArgs e)
    {
        int ygCharge = 0;
        if (!int.TryParse(GetStringEditorValue(YouthChargeStd), out ygCharge)) ygCharge = 3;

        if (Request["Action"] == "Import" && !ValidateFormField()) return;

        //--如果是员工新增界面
        if (String.IsNullOrEmpty(txtOID.Value))
        {
            txtOID.Value= Guid.NewGuid().ToString();

            mmAdapter.MyInsert(txtOID.Value, GetStringEditorValue(EmpName), GetStringEditorValue(HrCode), GetStringEditorValue(Sex), GetStringEditorValue(Dept), GetStringEditorValue(WorkGroup), GetStringEditorValue(Post), GetStringEditorValue(ParttimeName),
                GetStringEditorValue(Politics), GetDateTimeEditorValue(PartyDate), GetStringEditorValue(Wedding), GetStringEditorValue(Nation), GetStringEditorValue(NativePlace), GetStringEditorValue(Mobile), GetStringEditorValue(EmpID), GetStringEditorValue(House),
                GetDateTimeEditorValue(Birthday), GetDateTimeEditorValue(JobDateTime), GetDateTimeEditorValue(ComDateTime), GetStringEditorValue(FstSchoolExp), GetStringEditorValue(FstDegree), GetStringEditorValue(LstSchoolExp), GetStringEditorValue(LstDegree),
                GetStringEditorValue(SkillLevel), GetStringEditorValue(VolunteerInfo), GetStringEditorValue(SpecialSkill), GetStringEditorValue(ApplyParty), GetDateTimeEditorValue(ApplyPartyDate), GetStringEditorValue(FstSchool),
                GetStringEditorValue(FstProfession), GetDateTimeEditorValue(FstGraduateDate), GetStringEditorValue(LstSchool), GetStringEditorValue(LstProfession), GetDateTimeEditorValue(LstGraduateDate), GetStringEditorValue(HrType),
                GetStringEditorValue(HrStatus), ygCharge, GetStringEditorValue(FK_YouthGroup_FZ), GetStringEditorValue(Email), GetStringEditorValue(FK_YouthGroup));

            if (Request["Action"] == "Import")
                miAdapter.MyDelete(Request["MI_OID"]);

            NotificationTableAdapter.ClearNfCache();
        }
        else
        {
            mmAdapter.MyUpdate(GetStringEditorValue(EmpName), GetStringEditorValue(HrCode), GetStringEditorValue(Sex), GetStringEditorValue(Dept), GetStringEditorValue(WorkGroup), GetStringEditorValue(Post), GetStringEditorValue(ParttimeName),
                GetStringEditorValue(Politics), GetDateTimeEditorValue(PartyDate), GetStringEditorValue(Wedding), GetStringEditorValue(Nation), GetStringEditorValue(NativePlace), GetStringEditorValue(Mobile), GetStringEditorValue(EmpID), GetStringEditorValue(House),
                GetDateTimeEditorValue(Birthday), GetDateTimeEditorValue(JobDateTime), GetDateTimeEditorValue(ComDateTime), GetStringEditorValue(FstSchoolExp), GetStringEditorValue(FstDegree), GetStringEditorValue(LstSchoolExp), GetStringEditorValue(LstDegree),
                GetStringEditorValue(SkillLevel), GetStringEditorValue(VolunteerInfo), GetStringEditorValue(SpecialSkill), GetStringEditorValue(ApplyParty), GetDateTimeEditorValue(ApplyPartyDate), GetStringEditorValue(FstSchool),
                GetStringEditorValue(FstProfession), GetDateTimeEditorValue(FstGraduateDate), GetStringEditorValue(LstSchool), GetStringEditorValue(LstProfession), GetDateTimeEditorValue(LstGraduateDate), GetStringEditorValue(HrType),
                GetStringEditorValue(HrStatus), ygCharge, GetStringEditorValue(Email), GetStringEditorValue(FK_YouthGroup), GetStringEditorValue(FK_YouthGroup_FZ), txtOID.Value);

            NotificationTableAdapter.ClearNfCache();
        }
    }

    /// <summary>
    /// 验证表单字段的规范性：比较复选框值与数据源的一致性
    /// </summary>
    /// <returns></returns>
    private bool ValidateFormField()
    {
        bool ret = true;

        for (int i = 0; i < cmbArray.Length; i++)
        {
            ASPxComboBox cmb = this.pageControl.FindControl(cmbArray[i]) as ASPxComboBox;
            if (cmb != null)
            {
                List<YouthOneDS.StandardEnumRow> lstSE = seAdapter.GetDataCache().Where(x => x.SE_TYPE == stdArray[i]).ToList();

                if (cmb.Value != null && lstSE.Where(x => x.SE_KEY == cmb.Value.ToString()).Count() == 0)
                {
                    cmb.IsValid = false;
                    cmb.ErrorText = String.Format("{0}填写不规范！", stdArray[i]);
                    ret = false;
                }
            }
        }

        if (SkillLevel.Value != null)
        {
            if (seAdapter.GetDataCache().Where(x => (x.SE_TYPE == "职称" || x.SE_TYPE == "技能等级") && x.SE_VALUE == SkillLevel.Value.ToString()).Count() == 0)
            {
                SkillLevel.IsValid = false;
                SkillLevel.ErrorText = String.Format("{0}填写不规范！", "职称/技能等级");
                ret = false;
            }
        }

        if (Post.Value != null)
        {
            if (psAdapter.GetDataCache().Where(x => x.POST_NAME == Post.Value.ToString()).Count() == 0)
            {
                Post.IsValid = false;
                Post.ErrorText = String.Format("{0}填写不规范！", "岗位");
                ret = false;
            }
        }

        if (Dept.Value != null)
        {
            if (ogAdapter.GetDataCache().Where(x => x.OG_NAME == Dept.Value.ToString()).Count() == 0)
            {
                Dept.IsValid = false;
                Dept.ErrorText = String.Format("{0}填写不规范！", "部门");
                ret = false;
            }
        }

        if (WorkGroup.Value != null)
        {
            if (ogAdapter.GetDataCache().Where(x => x.OG_NAME == WorkGroup.Value.ToString()).Count() == 0)
            {
                WorkGroup.IsValid = false;
                WorkGroup.ErrorText = String.Format("{0}填写不规范！", "科室/作业区");
                ret = false;
            }
        }

        if (FK_YouthGroup.Value != null)
        {
            if (ygAdapter.GetZBCache().Where(x => x.OID == FK_YouthGroup.Value.ToString()).Count() == 0)
            {
                FK_YouthGroup.IsValid = false;
                FK_YouthGroup.ErrorText = String.Format("{0}填写不规范！", "支部");
                ret = false;
            }
            else if (AuthUser.RoleName == AuthenUserType.TZB_Admin && FK_YouthGroup.Value.ToString() != AuthUser.YouthGroup)
            {
                FK_YouthGroup.IsValid = false;
                FK_YouthGroup.ErrorText = "只能填写本支部！";
                ret = false;
            }
        }

        if (FK_YouthGroup_FZ.Value != null)
        {
            if (ygAdapter.GetDataCache().Where(x => x.OID == FK_YouthGroup_FZ.Value.ToString()).Count() == 0)
            {
                FK_YouthGroup_FZ.IsValid = false;
                FK_YouthGroup_FZ.ErrorText = String.Format("{0}填写不规范！", "分支");
                ret = false;
            }
        }

        return ret;
    }

    private string GetStringEditorValue(ASPxEdit editor)
    {
        if (editor.Value == null) return String.Empty;
        else return editor.Value.ToString();
    }

    private DateTime GetDateTimeEditorValue(ASPxDateEdit editor)
    {
        if (editor.Value == null) return new DateTime(1900, 1, 1);
        else return (DateTime)editor.Value;
    }

    void FK_YouthGroup_FZ_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        String YG_OID = e.Parameter;
        if (string.IsNullOrEmpty(YG_OID)) return;

        FK_YouthGroup_FZ.DataSource = ygAdapter.GetFZCache(YG_OID);
        FK_YouthGroup_FZ.ValueField = "OID";
        FK_YouthGroup_FZ.TextField = "YG_NAME";
        FK_YouthGroup_FZ.DataBind();
    }

    void WorkGroup_Callback(object source, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        String deptName = e.Parameter;
        if (string.IsNullOrEmpty(deptName)) return;

        WorkGroup.DataSource = ogAdapter.GetWorkGroupCache(deptName);
        WorkGroup.TextField = "OG_NAME";
        WorkGroup.ValueField = "OG_NAME";
        WorkGroup.DataBind();
    }

    private ASPxGridView GetPriseGrid()
    {
        return pageControl.FindControl("gridPrise") as ASPxGridView;
    }

    protected void gridPrise_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
    {
        switch (e.Column.FieldName)
        {
            case "PR_NAME":
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = seAdapter.GetDataCache().Where(x => x.SE_TYPE == "奖励标准");
                cmb.TextField = "SE_KEY";
                cmb.ValueField = "SE_KEY";
                cmb.DataBind();
                break;
            default:
                break;
        }

    }

    private void InitStdEnumCombox()
    {
        for (int i = 0; i < cmbArray.Length; i++)
		{
            ASPxComboBox cmb = this.pageControl.FindControl(cmbArray[i]) as ASPxComboBox;
            if (cmb != null)
            {
                List<YouthOneDS.StandardEnumRow> lstSE = seAdapter.GetDataCache().Where(x => x.SE_TYPE == stdArray[i]).ToList();

                if (stdArray[i] == "兼职职务")
                {
                    cmb.Items.Add("无", "无");
                    foreach (var item in lstSE)
                    {
                        cmb.Items.Add(item.SE_KEY, item.SE_VALUE);
                    }
                }
                else
                {
                    cmb.DataSource = lstSE;
                    cmb.TextField = "SE_KEY";
                    cmb.ValueField = "SE_VALUE";
                    cmb.DataBind();
                }
            }
		}
    }

    private void InitEditForm()
    {
        //--如果是导入界面，则必须传入MI_OID
        if (Request["Action"] == "Import")
        {
            YouthOneDS.MemberImportRow row = miAdapter.GetMember(Request["MI_OID"]).SingleOrDefault();

            foreach (String field in formFields)
            {
                ASPxTextEdit editor = this.pageControl.FindControl(field) as ASPxTextEdit;
                editor.Value = row[field];
            }

            //pageControl.TabPages[1].Visible = false;
        }
        //--如果是员工新增界面
        else if (Request["Action"] == "New")
        {
            Nation.Value = "汉族";
            Politics.Value = "共青团员";
            Wedding.Value = "未婚";
            HrType.Value = "在册员工";
            HrStatus.Value = "在岗";
            House.Value = "员工宿舍";
            YouthChargeStd.Value = 10;

            if (!String.IsNullOrEmpty(Request["YG_OID"]))
            {
                FK_YouthGroup.Value = Request["YG_OID"];
                FK_YouthGroup.ReadOnly = true;
            }
        }
        //--如果是员工编辑界面
        else if (Request["Action"] == "Edit")
        {
            txtOID.Value = Request["MM_OID"];
            YouthOneDS.MemberRow row = mmAdapter.GetMemberByOID(txtOID.Value).SingleOrDefault();

            foreach (String field in formFields)
            {
                ASPxTextEdit editor = this.pageControl.FindControl(field) as ASPxTextEdit;
                editor.Value = row[field];
            }
            FK_YouthGroup.ReadOnly = true;
        }
    }
}
