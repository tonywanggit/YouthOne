using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxUploadControl;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Text;
using System.Globalization;
using YouthOne.Component.YouthOneDSTableAdapters;
using DevExpress.XtraRichEdit.API.Word;
using YouthOne.Component;

public partial class Member_MemberImport : BasePage
{
    private StandardEnumTableAdapter seAdapter = new StandardEnumTableAdapter();
    private PostTableAdapter psAdapter = new PostTableAdapter();
    private YouthGroupTableAdapter ygAdapter = new YouthGroupTableAdapter();
    private OrganizeTableAdapter ogAdapter = new OrganizeTableAdapter();
    private MemberImportTableAdapter miAdapter = new MemberImportTableAdapter();
    private MemberTableAdapter mmAdapter = new MemberTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        gridSE.DataBound += new EventHandler(gridSE_DataBound);
        gridPost.DataBound += new EventHandler(gridPost_DataBound);

        InitRight();
    }

    private void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.Admin && AuthUser.RoleName != AuthenUserType.TW_Admin && AuthUser.RoleName != AuthenUserType.TZB_Admin)
        {
            btnUpload.Enabled = false;
            grid.Columns["OP_COL"].Visible = false;
        }
    }

    void gridPost_DataBound(object sender, EventArgs e)
    {
        this.gridPost.GroupBy(this.gridPost.Columns["CAT_NAME"]);
        this.gridPost.ExpandAll();
    }

    void gridSE_DataBound(object sender, EventArgs e)
    {
        this.gridSE.GroupBy(this.gridSE.Columns["SE_TYPE"]);
        this.gridSE.ExpandAll();
    }


    protected string SavePostedFile(UploadedFile uploadedFile)
    {
        string ret = "";
        if (uploadedFile.IsValid)
        {
            string tempFileName = MapPath("~/TempData/") + Path.GetRandomFileName();
            uploadedFile.SaveAs(tempFileName);
            DataTable dtExcel = ExcelToDataTable(tempFileName);

            ret = AnalyseImportData(dtExcel);
        }
        return ret;
    }

    protected void uplExcel_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        if (!e.UploadedFile.FileName.EndsWith("xls") && !e.UploadedFile.FileName.EndsWith("xlsx"))
        {
            e.IsValid = false;
            e.ErrorText = "只允许上传Excel文档！";
        }
        else
        {
            try
            {
                e.CallbackData = SavePostedFile(e.UploadedFile);
                WriteLog(LogType.Bussiness, "批量导入团员", e.CallbackData);
            }
            catch (Exception ex)
            {
                e.IsValid = false;
                e.ErrorText = ex.Message;
            }
        }
    }

    /// <summary>
    /// 将上传的Excel转换成DataTable
    /// </summary>
    /// <param name="tempFileName"></param>
    /// <returns></returns>
    private DataTable ExcelToDataTable(string tempFileName)
    {
        string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + tempFileName + ";" + "Extended Properties=\"Excel 12.0;HDR=NO;IMEX=1;\"";
        DataTable dtExcel = new DataTable();

        using (OleDbConnection cnnxls = new OleDbConnection(strConn))
        {
            try
            {
                String sql = String.Format("SELECT TOP 5000 * FROM [Sheet1$]");
                OleDbDataAdapter oda = new OleDbDataAdapter(sql, cnnxls);

                oda.Fill(dtExcel);
            }
            catch (Exception ex)
            {

            }
        }
        return dtExcel;
    }

    /// <summary>
    /// 分析上传的Excels数据
    /// </summary>
    /// <param name="dtExcel"></param>
    private string AnalyseImportData(DataTable dtExcel)
    {
        int totalRow = 0;
        int successRow = 0;
        int failureRow = 0;

        SetColumnName(dtExcel);

        dtExcel.Columns.Add("ExcelNum");
        dtExcel.Columns.Add("ErrorDesc");

        miAdapter.TruncateTable();

        for (int i = 1; i < dtExcel.Rows.Count; i++)
        {
            DataRow row = dtExcel.Rows[i];
            if (String.IsNullOrEmpty(row["EmpName"].ToString())) break;

            totalRow++;

            row["ExcelNum"] = i;
            row["ErrorDesc"] = ValidateAndFormateRow(row, dtExcel.Columns);

            if (row["ErrorDesc"].ToString() == string.Empty)
            {
                int ygCharge = FormatCharge(row["HrType"].ToString());
                mmAdapter.Insert(Guid.NewGuid().ToString(), row["FK_YouthGroup"].ToString(), row["FK_YouthGroup_FZ"].ToString(), row["Dept"].ToString(), row["WorkGroup"].ToString(),
                    row["Post"].ToString(), row["HrCode"].ToString(), row["HrType"].ToString(), row["HrStatus"].ToString(),
                    row["EmpName"].ToString(), row["EmpID"].ToString(), row["Sex"].ToString(), DateTime.Parse(row["Birthday"].ToString()), 0,
                    row["ParttimeName"].ToString(), DateTime.Parse(row["JobDateTime"].ToString()), DateTime.Parse(row["ComDateTime"].ToString()), row["Wedding"].ToString(),
                    row["NativePlace"].ToString(), row["Politics"].ToString(), DateTime.Parse(row["PartyDate"].ToString()), row["Nation"].ToString(),
                    row["House"].ToString(), row["SkillLevel"].ToString(), row["FstSchoolExp"].ToString(), row["FstDegree"].ToString(),
                    row["LstSchoolExp"].ToString(), row["LstDegree"].ToString(), row["FstSchool"].ToString(), row["FstProfession"].ToString(),
                    DateTime.Parse(row["FstGraduateDate"].ToString()), row["LstSchool"].ToString(), row["LstProfession"].ToString(), DateTime.Parse(row["LstGraduateDate"].ToString()),
                    row["ApplyParty"].ToString(), DateTime.Parse(row["ApplyPartyDate"].ToString()), row["VolunteerInfo"].ToString(), row["Mobile"].ToString(),
                    row["Email"].ToString(), row["SpecialSkill"].ToString(), ygCharge);

                successRow++;
            }
            else
            {
                int ygCharge = FormatCharge(row["HrType"].ToString());
                miAdapter.Insert(Guid.NewGuid().ToString(), int.Parse(row["ExcelNum"].ToString()), row["ErrorDesc"].ToString(),
                    row["FK_YouthGroup"].ToString(), row["FK_YouthGroup_FZ"].ToString(), row["Dept"].ToString(), row["WorkGroup"].ToString(),
                    row["Post"].ToString(), row["HrCode"].ToString(), row["HrType"].ToString(), row["HrStatus"].ToString(),
                    row["EmpName"].ToString(), row["EmpID"].ToString(), row["Sex"].ToString(), DateTime.Parse(row["Birthday"].ToString()), 0,
                    row["ParttimeName"].ToString(), DateTime.Parse(row["JobDateTime"].ToString()), DateTime.Parse(row["ComDateTime"].ToString()), row["Wedding"].ToString(),
                    row["NativePlace"].ToString(), row["Politics"].ToString(), DateTime.Parse(row["PartyDate"].ToString()), row["Nation"].ToString(),
                    row["House"].ToString(), row["SkillLevel"].ToString(), row["FstSchoolExp"].ToString(), row["FstDegree"].ToString(),
                    row["LstSchoolExp"].ToString(), row["LstDegree"].ToString(), row["FstSchool"].ToString(), row["FstProfession"].ToString(),
                    DateTime.Parse(row["FstGraduateDate"].ToString()), row["LstSchool"].ToString(), row["LstProfession"].ToString(), DateTime.Parse(row["LstGraduateDate"].ToString()),
                    row["ApplyParty"].ToString(), DateTime.Parse(row["ApplyPartyDate"].ToString()), row["VolunteerInfo"].ToString(), row["Mobile"].ToString(),
                    row["Email"].ToString(), row["SpecialSkill"].ToString(), ygCharge);
                failureRow++;
            }
        }

        String msg = String.Empty;
        if (totalRow == 0)
            msg = "Excel中没有效数据！";
        else if (failureRow == 0)
            msg = String.Format("Excel中共有{0}条数据，全部成功导入！", totalRow);
        else if (successRow == 0)
            msg = String.Format("Excel中共有{0}条数据，全部导入失败，请完善！", totalRow);
        else
            msg = String.Format("Excel中共有{0}条数据，成功导入{1}条，另有{2}条数据不符合规范，请完善！", totalRow, successRow, failureRow);

        return msg;
    }

    #region 验证并格式化数据

    /// <summary>
    /// 根据Excel中第一行的值设置列名
    /// </summary>
    /// <param name="dtExcel"></param>
    private void SetColumnName(DataTable dtExcel)
    {
        if (dtExcel.Rows.Count < 2)
        {
            throw new Exception("请使用指定Excel模板导入有效数据！");
        }

        for (int i = 0; i < dtExcel.Columns.Count; i++)
        {
            string colCaption = dtExcel.Rows[0][i].ToString().Trim();
            switch (colCaption)
            {
                case "姓名":
                    dtExcel.Columns[i].ColumnName = "EmpName";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "工号":
                    dtExcel.Columns[i].ColumnName = "HrCode";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "支部":
                    dtExcel.Columns[i].ColumnName = "FK_YouthGroup";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "分支":
                    dtExcel.Columns[i].ColumnName = "FK_YouthGroup_FZ";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "员工性质":
                    dtExcel.Columns[i].ColumnName = "HrType";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "员工状态":
                    dtExcel.Columns[i].ColumnName = "HrStatus";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "岗位":
                    dtExcel.Columns[i].ColumnName = "Post";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "性别":
                    dtExcel.Columns[i].ColumnName = "Sex";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "部门":
                    dtExcel.Columns[i].ColumnName = "Dept";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "科室":
                    dtExcel.Columns[i].ColumnName = "WorkGroup";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "身份证号":
                    dtExcel.Columns[i].ColumnName = "EmpID";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "兼职职务":
                    dtExcel.Columns[i].ColumnName = "ParttimeName";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "政治面貌":
                    dtExcel.Columns[i].ColumnName = "Politics";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "入党日期":
                    dtExcel.Columns[i].ColumnName = "PartyDate";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "婚姻状况":
                    dtExcel.Columns[i].ColumnName = "Wedding";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "民族":
                    dtExcel.Columns[i].ColumnName = "Nation";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "出生日期":
                    dtExcel.Columns[i].ColumnName = "Birthday";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "籍贯":
                    dtExcel.Columns[i].ColumnName = "NativePlace";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "参加工作年月":
                    dtExcel.Columns[i].ColumnName = "JobDateTime";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "入厂年月":
                    dtExcel.Columns[i].ColumnName = "ComDateTime";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "住房情况":
                    dtExcel.Columns[i].ColumnName = "House";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "第一学历":
                    dtExcel.Columns[i].ColumnName = "FstSchoolExp";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "第一学位":
                    dtExcel.Columns[i].ColumnName = "FstDegree";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "第一毕业学校":
                    dtExcel.Columns[i].ColumnName = "FstSchool";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "第一专业":
                    dtExcel.Columns[i].ColumnName = "FstProfession";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "第一毕业时间":
                    dtExcel.Columns[i].ColumnName = "FstGraduateDate";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "最高学历":
                    dtExcel.Columns[i].ColumnName = "LstSchoolExp";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "最高学位":
                    dtExcel.Columns[i].ColumnName = "LstDegree";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "最高毕业学校":
                    dtExcel.Columns[i].ColumnName = "LstSchool";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "最高专业":
                    dtExcel.Columns[i].ColumnName = "LstProfession";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "最高毕业时间":
                    dtExcel.Columns[i].ColumnName = "LstGraduateDate";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "职称/技能等级":
                    dtExcel.Columns[i].ColumnName = "SkillLevel";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "是否申请入党":
                    dtExcel.Columns[i].ColumnName = "ApplyParty";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;                
                case "申请入党时间":
                    dtExcel.Columns[i].ColumnName = "ApplyPartyDate";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "志愿者信息":
                    dtExcel.Columns[i].ColumnName = "VolunteerInfo";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "手机号码":
                    dtExcel.Columns[i].ColumnName = "Mobile";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "电子邮箱":
                    dtExcel.Columns[i].ColumnName = "Email";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                case "特长":
                    dtExcel.Columns[i].ColumnName = "SpecialSkill";
                    dtExcel.Columns[i].Caption = colCaption;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 验证并格式化Excel中的行数据,成功返回String.Empty，失败返回错误信息
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    private string ValidateAndFormateRow(DataRow row, DataColumnCollection cols)
    {
        StringBuilder errText = new StringBuilder();

        foreach (DataColumn col in cols)
        {
            switch (col.ColumnName)
            {
                case "EmpName":
                case "HrCode":
                    if (row[col] == null || row[col].ToString().Trim() == string.Empty)
                    {
                        errText.AppendFormat("{0}不可为空！<br>", col.Caption);
                    }
                    break;
                case "FK_YouthGroup":
                case "FK_YouthGroup_FZ":
                    ValidateYouthGroup(row, col, errText);
                    break;
                case "Dept":
                case "WorkGroup":
                    ValidateOrganize(row, col, errText);
                    break;
                case "HrType":
                case "HrStatus":
                case "ParttimeName":
                case "Politics":
                case "Nation":
                case "House":
                case "SkillLevel":
                    ValidateStdEnum(col.Caption, row[col].ToString(), errText);
                    break;
                case "FstSchoolExp":
                case "LstSchoolExp":
                    ValidateStdEnum("学历", row[col].ToString(), errText);
                    break;
                case "LstDegree":
                case "FstDegree":
                    ValidateStdEnum("学位", row[col].ToString(), errText);
                    break;
                case "Post":
                    ValidatePost(row[col].ToString(), errText);
                    break;
                case "PartyDate":
                case "Birthday":
                case "JobDateTime":
                case "ComDateTime":
                case "FstGraduateDate":
                case "LstGraduateDate":
                case "ApplyPartyDate":
                    row[col] = FormatDateTime(row[col].ToString());
                    break;

                default:
                    break;
            }
        }

        return errText.ToString();
    }

    /// <summary>
    /// 格式化日期格式，支持：1900-01， 190001，19000101， 1900-01-01， 1900.01， 1900.01.01
    /// </summary>
    /// <param name="src"></param>
    /// <returns></returns>
    private DateTime FormatDateTime(string src)
    {
        if (String.IsNullOrEmpty(src)) return new DateTime(1900, 1, 1);
        if (src.Length != 6 && src.Length != 7 && src.Length != 8 && src.Length != 10) return new DateTime(1900, 1, 1);

        src = src.Replace("-", "").Replace(".", "");

        DateTime destDateTime;
        if (src.Length == 6)
            DateTime.TryParseExact(src, "yyyyMM", CultureInfo.CurrentCulture, DateTimeStyles.None, out destDateTime);
        else
            DateTime.TryParseExact(src, "yyyyMMdd", CultureInfo.CurrentCulture, DateTimeStyles.None, out destDateTime);

        if(destDateTime == null || destDateTime.Year == 1)
            return new DateTime(1900, 1, 1);

        return destDateTime;
    }

    /// <summary>
    /// 格式化团费标准
    /// </summary>
    /// <param name="hrType"></param>
    /// <returns></returns>
    private int FormatCharge(string hrType){
        if(String.IsNullOrEmpty(hrType)) return 0;

        if(hrType == "在册员工" || hrType == "委派员工" || hrType == "商借员工") return 10;
        if(hrType == "准员工" || hrType == "协力工") return 8;
        if(hrType == "劳务工") return 3;

        return 0;
    }

    /// <summary>
    /// 对标准枚举值进行验证
    /// </summary>
    /// <param name="seType"></param>
    /// <param name="seValue"></param>
    /// <param name="errText"></param>
    private void ValidateStdEnum(string seType, string seValue, StringBuilder errText)
    {
        if (!String.IsNullOrEmpty(seValue))
        {
            if (seType == "职称/技能等级")
            {
                if (seAdapter.GetDataCache().Where(x => (x.SE_TYPE == "职称" || x.SE_TYPE == "技能等级") && x.SE_VALUE == seValue).Count() == 0)
                {
                    errText.AppendFormat("{0}填写不规范！<br>", seType);
                }
            }
            else
            {
                if (seAdapter.GetDataCache().Where(x => x.SE_TYPE == seType && x.SE_VALUE == seValue).Count() == 0)
                {
                    errText.AppendFormat("{0}填写不规范！<br>", seType);
                }
            }
        }
        else
        {
            if (seType == "员工性质" || seType == "员工状态")
            {
                errText.AppendFormat("{0}不可为空！<br>", seType);
            }
        }
    }

    /// <summary>
    /// 对岗位进行验证
    /// </summary>
    /// <param name="post"></param>
    /// <param name="errText"></param>
    private void ValidatePost(string post, StringBuilder errText)
    {
        if (String.IsNullOrEmpty(post))
        {

        }
        else
        {
            if (psAdapter.GetDataCache().Where(x => x.POST_NAME == post.Trim()).Count() == 0)
            {
                errText.Append("岗位填写不规范！<br>");
            }
        }
    }

    /// <summary>
    /// 对支部和分支进行验证
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="errText"></param>
    private void ValidateYouthGroup(DataRow row, DataColumn col, StringBuilder errText)
    {
        if (String.IsNullOrEmpty(row[col].ToString()))
        {
            if (col.ColumnName == "FK_YouthGroup")
            {
                errText.AppendFormat("{0}不可为空！<br>", col.Caption);
            }
        }
        else
        {
            int ygLevel = col.ColumnName == "FK_YouthGroup" ? 1 : 2;
            List<YouthOneDS.YouthGroupRow> lstYG = ygAdapter.GetDataCache().Where(x => x.YG_LEVEL == ygLevel && x.YG_NAME == row[col].ToString()).ToList();

            if (lstYG.Count == 0)
            {
                errText.AppendFormat("{0}填写不规范！<br>", col.Caption);
                row[col] = String.Empty;
            }
            //--限制团支部管理员导入非本支部下的团员信息
            else if (AuthUser.RoleName == AuthenUserType.TZB_Admin && ygLevel == 1 && lstYG[0].OID != AuthUser.YouthGroup)
            {
                errText.AppendFormat("此团员非本支部，无法导入！<br>", col.Caption);
                row[col] = lstYG[0].OID;

            }
            else
            {
                row[col] = lstYG[0].OID;
            }
        }
    }

    /// <summary>
    /// 对部门和科室进行验证
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <param name="errText"></param>
    private void ValidateOrganize(DataRow row, DataColumn col, StringBuilder errText)
    {
        if (String.IsNullOrEmpty(row[col].ToString()))
        {
        }
        else
        {
            int ogLevel = col.ColumnName == "Dept" ? 1 : 2;
            List<YouthOneDS.OrganizeRow> lstOG = ogAdapter.GetDataCache().Where(x => x.OG_LEVEL == ogLevel && x.OG_NAME == row[col].ToString()).ToList();

            if (lstOG.Count == 0)
            {
                errText.AppendFormat("{0}填写不规范！<br>", col.Caption);
            }
        }
    }

    #endregion
}
