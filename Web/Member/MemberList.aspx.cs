using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YouthOne.Component.YouthOneDSTableAdapters;
using YouthOne.Component;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxClasses;
using DevExpress.Web.ASPxGridView;
using System.IO;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Web.ASPxTabControl;

public partial class Member_MemberList : BasePage
{
    private StandardEnumTableAdapter seAdapter = new StandardEnumTableAdapter();
    private OrganizeTableAdapter ogAdapter = new OrganizeTableAdapter();
    private MemberTableAdapter mmAdapter = new MemberTableAdapter();
    private YouthGroupTableAdapter ygAdapter = new YouthGroupTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["OID"] != "GSTW")
        {
            grid.SettingsPager.PageSize = 100000;
            grid.SettingsPager.ShowDisabledButtons = false;

        }

        if (!Page.IsPostBack && !Page.IsCallback)
        {
            gridSE.DataBound += new EventHandler(gridSE_DataBound);
            gridPost.DataBound += new EventHandler(gridPost_DataBound);
            BindFZ();
        }
        OdsMember.Inserting += new ObjectDataSourceMethodEventHandler(OdsMember_Inserting);
        grid.InitNewRow += new DevExpress.Web.Data.ASPxDataInitNewRowEventHandler(grid_InitNewRow);
        grid.CustomCallback += new DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventHandler(grid_CustomCallback);
        grid.HtmlCommandCellPrepared += new DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventHandler(grid_HtmlCommandCellPrepared);
        grid.HtmlEditFormCreated += new ASPxGridViewEditFormEventHandler(grid_HtmlEditFormCreated);

        InitRight();
    }

    protected void gridPrise_RowInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
    {

    }


    void OdsPrise_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        //e.InputParameters["PR_NAME"] = GetPriseGrid().edi
    }

    void grid_HtmlEditFormCreated(object sender, ASPxGridViewEditFormEventArgs e)
    {
        if (grid.IsNewRowEditing) return;

        Session["Session_ML_MM_OID"] = grid.GetRowValues(grid.EditingRowVisibleIndex, "OID").ToString();
        GetPriseGrid().DataBind();

    }

    private ASPxGridView GetPriseGrid()
    {
        ASPxPageControl pageControl = grid.FindEditFormTemplateControl("pageControl") as ASPxPageControl;
        return pageControl.FindControl("gridPrise") as ASPxGridView;
    }

    protected string SavePostedFiles(UploadedFile uploadedFile) {
         string ret = "";

         if (uploadedFile.IsValid == true)
         {
             FileInfo fileInfo = new FileInfo(uploadedFile.FileName);
             string resFileName = MapPath("~/EmpPicture/") + fileInfo.Name;
             uploadedFile.SaveAs(resFileName);
         }
         return ret;

     }
    protected void UploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
    {
        try
        {
            e.CallbackData = SavePostedFiles(e.UploadedFile);
        }
        catch (Exception ex)
        {
            e.IsValid = false;
            e.ErrorText = ex.Message;
        }
    }

    void grid_HtmlCommandCellPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableCommandCellEventArgs e)
    {
        if (e.CommandCellType == GridViewTableCommandCellType.Data)
        {
            if (!AuthUser.RoleName.Contains("Admin"))
            {
                e.Cell.Controls[0].Visible = false;
                e.Cell.Controls[1].Visible = false;
                e.Cell.Controls[2].Visible = false;
                e.Cell.Controls[3].Visible = false;
            }
        }
    }

    void InitRight()
    {
        if (!AuthUser.RoleName.Contains("Admin"))
        {
            btnAddAdmin.Visible = false;
            btnUpload.Visible = false;
            btnExport.Visible = false;
        }

        if (Request["OID"] == "GSTW")
            btnAddAdmin.Visible = false;
    }

    void BindFZ()
    {
        hidYouthGroup["OID"] = Request["OID"];

        List<YouthOneDS.YouthGroupRow> lstFZ = ygAdapter.GetFZCache(Request["OID"]);
        YouthOneDS.YouthGroupDataTable tbFZ = new YouthOneDS.YouthGroupDataTable();
        YouthOneDS.YouthGroupRow row = tbFZ.AddYouthGroupRow("", "所有青年", 0, "", DateTime.Now);
        lstFZ.Insert(0, row);

        cbFZ.DataSource = lstFZ;
        cbFZ.TextField = "YG_NAME";
        cbFZ.ValueField = "OID";
        cbFZ.DataBind();

        cbFZ.SelectedIndex = 0;
    }

    void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        string[] cbArray = e.Parameters.Split('|');
        string catName = cbArray[0];
        string mmOID = cbArray[1];
        string ygOID = cbArray[2];
        string mmName = cbArray[3];

        if (catName == "转出")
        {
            mmAdapter.ChangeYG(mmOID, ygOID, AuthUser.UserID);
            WriteLog(LogType.Bussiness, catName, "管理员『" + AuthUser.LoginName + "』对『" + mmName + "』执行了" + catName + "操作。");
        }
        else if (catName == "内退" || catName == "离职")
        {
            mmAdapter.ChangeStatus(mmOID, catName);
            WriteLog(LogType.Bussiness, catName, "管理员『" + AuthUser.LoginName + "』对『" + mmName + "』执行了" + catName + "操作。");
        }

        grid.DataBind();
    }

    void OdsMember_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {
        foreach (var key in e.InputParameters.Keys)
	    {
            if (e.InputParameters[key] == null)
            {
                string keyName = key.ToString();
                if (keyName.Contains("Date") || keyName == "Birthday")
                {
                    e.InputParameters[key] = new DateTime(1900, 1, 1);
                }
                else
                {
                    e.InputParameters[key] = "";
                }
            }
	    }

        e.InputParameters.Add("FK_YouthGroup", Request["OID"]);
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

    void grid_InitNewRow(object sender, DevExpress.Web.Data.ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["Nation"] = "汉族";
        e.NewValues["Politics"] = "共青团员";
        e.NewValues["Wedding"] = "未婚";
        e.NewValues["HrType"] = "在册员工";
        e.NewValues["HrStatus"] = "在岗";
        e.NewValues["FstSchoolExp"] = "大学本科";
        e.NewValues["FstDegree"] = "学士";
        e.NewValues["LstSchoolExp"] = "大学本科";
        e.NewValues["LstDegree"] = "学士";
        e.NewValues["House"] = "员工宿舍";
        e.NewValues["YouthChargeStd"] = "3";
    }

    protected void grid_CustomColumnDisplayText(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewColumnDisplayTextEventArgs e)
    {
        if (e.Column.FieldName == "PartyDate" ||
            e.Column.FieldName == "Birthday" ||
            e.Column.FieldName == "JobDateTime" ||
            e.Column.FieldName == "ComDateTime" ||
            e.Column.FieldName == "ApplyPartyDate" ||
            e.Column.FieldName == "FstGraduateDate" ||
            e.Column.FieldName == "LstGraduateDate"
            )
        {

            DateTime partyDate = Convert.ToDateTime(e.Value);
            if (partyDate.Year == 1900)
                e.DisplayText = "";
            else
                e.DisplayText = partyDate.ToString("yyyy-MM-dd");
        }
    }

    protected void gridExport_OnRenderBrick(object sender, DevExpress.Web.ASPxGridView.Export.ASPxGridViewExportRenderingEventArgs e)
    {
        if (e.RowType == DevExpress.Web.ASPxGridView.GridViewRowType.Data && e.Column != null
                & (e.Column.FieldName == "PartyDate"
                || e.Column.FieldName == "Birthday"
                || e.Column.FieldName == "JobDateTime"
                || e.Column.FieldName == "ComDateTime"
                || e.Column.FieldName == "ApplyPartyDate"
                || e.Column.FieldName == "FstGraduateDate"
                || e.Column.FieldName == "LstGraduateDate" 
                )
            )
        {
            DateTime dt = (DateTime)e.TextValue;
            if (dt.Year == 1900)
            {
                e.Text = "";
                e.TextValue = null;
            }
            else
            {
                e.Text = dt.ToString("yyyy-MM-dd");
                e.TextValue = null;
            }
        }
    }


    protected void grid_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
    {
        if (!grid.IsEditing) return;
        //if (e.KeyValue == DBNull.Value || e.KeyValue == null) return;
        //object val = grid.GetRowValuesByKeyValue(e.KeyValue, "Country");
        //if (val == DBNull.Value) return;
        //string country = (string)val;

        switch (e.Column.FieldName)
        {
            case "WorkGroup":
                ASPxComboBox cmbWorkGroup = e.Editor as ASPxComboBox;
                cmbWorkGroup.Callback += new CallbackEventHandlerBase(cmbWorkGroup_Callback);
                break;
            case "Dept":
                    ASPxComboBox cmbDept = e.Editor as ASPxComboBox;
                    cmbDept.DataSource = ogAdapter.GetDataCache().Where(x => x.OG_LEVEL == 1);
                    cmbDept.TextField = "OG_NAME";
                    cmbDept.ValueField = "OG_NAME";
                    cmbDept.DataBind();
                break;

            case "Politics":
            case "Nation":
            case "HrType":
            case "HrStatus":
            case "FstSchoolExp":
            case "FstDegree":
            case "LstSchoolExp":
            case "LstDegree":
            case "House":
            case "YouthChargeStd":
                ASPxComboBox cmb = e.Editor as ASPxComboBox;
                cmb.DataSource = seAdapter.GetDataCache().Where(x => e.Column.Caption.Contains(x.SE_TYPE));
                cmb.TextField = "SE_KEY";
                cmb.ValueField = "SE_VALUE";
                cmb.DataBind();
                break;
            case "ParttimeName":
                ASPxComboBox cmbParttimeName = e.Editor as ASPxComboBox;
                List<YouthOneDS.StandardEnumRow> lstPN = seAdapter.GetDataCache().Where(x => e.Column.Caption.Contains(x.SE_TYPE)).ToList();
                //lstPN.Add(
                //cmbParttimeName.DataSource = seAdapter.GetDataCache().Where(x => e.Column.Caption.Contains(x.SE_TYPE));
                //cmbParttimeName.TextField = "SE_KEY";
                //cmbParttimeName.ValueField = "SE_VALUE";
                //cmbParttimeName.DataBind();
                cmbParttimeName.Items.Add("无", "无");
                foreach (var item in lstPN)
	            {
                    cmbParttimeName.Items.Add(item.SE_KEY, item.SE_VALUE);
	            }

                break;
            default:
                break;
        }
    }

    protected void gridPrise_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditorEventArgs e)
    {
        if (!grid.IsEditing) return;

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


    void cmbWorkGroup_Callback(object source, CallbackEventArgsBase e)
    {
        String deptName = e.Parameter;
        ASPxComboBox cmbWorkGroup = source as ASPxComboBox;
        if (string.IsNullOrEmpty(deptName)) return;

        cmbWorkGroup.DataSource = ogAdapter.GetWorkGroupCache(deptName);
        cmbWorkGroup.TextField = "OG_NAME";
        cmbWorkGroup.ValueField = "OG_NAME";
        cmbWorkGroup.DataBind();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        this.gridExport.WriteXlsToResponse("团员信息");
    }


}
