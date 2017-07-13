using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.Data;
using DevExpress.Web.ASPxGridView;
using YouthOne.Component;

public partial class Security_SystemAdmin : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.grid.RowDeleted += new ASPxDataDeletedEventHandler(grid_RowDeleted);
        this.grid.RowInserted += new ASPxDataInsertedEventHandler(grid_RowInserted);

        InitRight();
    }

    /// <summary>
    /// 初始化权限
    /// </summary>
    void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.Admin)
        {
            btnAddAdmin.Enabled = false;
            this.grid.Columns["OP_COL"].Visible = false;
        }
    }

    void grid_RowInserted(object sender, ASPxDataInsertedEventArgs e)
    {
        WriteLog(YouthOne.Component.LogType.System, "添加管理员", e.NewValues["LOG_NAME"].ToString());
    }

    void grid_RowDeleted(object sender, ASPxDataDeletedEventArgs e)
    {
        WriteLog(YouthOne.Component.LogType.System, "删除管理员", e.Values["LOG_NAME"].ToString());
    }

    protected void grid_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
    {
    }

    protected void grid_HtmlRowPrepared(object sender, ASPxGridViewTableRowEventArgs e)
    {
        if (e.RowType == GridViewRowType.Data)
        {
            int cmdColIndex = e.Row.Cells.Count - 1;
            if (e.GetValue("LOG_NAME").ToString() == "Admin"  && e.Row.Cells[cmdColIndex].Controls.Count == 2)
            {
                e.Row.Cells[cmdColIndex].Controls[1].Visible = false;
            }
        }
    }

    protected void grid_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["ROL_NAME"] = "TZB_Admin";
    }

    protected void grid_RowValidating(object sender, ASPxDataValidationEventArgs e)
    {
        if (e.Errors.Count > 0) e.RowError = "请修正所有错误后再保存！.";

        if (e.NewValues["YG_OID"].ToString() == "GSTW" && e.NewValues["ROL_NAME"].ToString()== "TZB_Admin")
        {
            AddError(e.Errors, grid.Columns["YG_OID"], "团支部管理员不能选择公司团委.");
        }

        if (string.IsNullOrEmpty(e.RowError) && e.Errors.Count > 0)
        {
            e.RowError = "请修正所有错误后再保存！";
            this.hidPassWord.Value = e.NewValues["PAS_WORD"].ToString();
        }
        else
        {
            this.hidPassWord.Value = "";
        }

    }

    void AddError(Dictionary<GridViewColumn, string> errors, GridViewColumn column, string errorText)
    {
        if (errors.ContainsKey(column)) return;
        errors[column] = errorText;
    }

    protected void grid_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
    {
        if (grid.IsNewRowEditing) return;

        if (e.KeyValue.ToString() == "Admin" && e.Column.FieldName != "PAS_WORD")
        {
            e.Editor.ReadOnly = true;
        }
    }

    protected void grid_HtmlEditFormCreated(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewEditFormEventArgs e)
    {
        Control wc = grid.FindEditFormTemplateControl("ASPxButtonEdit1");
        //grid.fin
        //if (wc != null)
        //{
        //    ASPxButtonEdit templateEdit = wc as ASPxButtonEdit;
        //    //
        //    // Initialize the editor as required
        //    //
        //    templateEdit.ForeColor = System.Drawing.Color.Blue;
        //}
    }

}
