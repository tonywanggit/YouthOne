using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.Data;
using YouthOne.Component;
using YouthOne.Component.YouthOneDSTableAdapters;

public partial class BaseInfo_YouthGroup : BasePage
{
    private YouthGroupTableAdapter ygAdapter = new YouthGroupTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        InitRight();
        InitPage();
    }

    private void InitPage()
    {
        this.treeList.ProcessDragNode += new TreeListNodeDragEventHandler(treeList_ProcessDragNode);
    }

    void treeList_ProcessDragNode(object sender, TreeListNodeDragEventArgs e)
    {
        //--如果目标节点和移动节点属于同一个父节点下，前两者不同，则执行移动操作
        if (e.Node.ParentNode.Key == e.NewParentNode.ParentNode.Key && e.Node.Key != e.NewParentNode.Key)
        {
            ygAdapter.MoveNode(e.Node.Key, e.NewParentNode.Key, e.Node.ParentNode.Key);
            treeList.DataBind();
        }

        e.Cancel = true;
    }

    protected void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.Admin && AuthUser.RoleName != AuthenUserType.TW_Admin)
        {
            treeList.Columns["OP_COL"].Visible = false;
            treeList.SettingsEditing.AllowNodeDragDrop = false;
        }
    }

    protected void treeList_HtmlCommandCellPrepared(object sender, TreeListHtmlCommandCellEventArgs e)
    {
        if (e.Cell.Controls.Count > 2)
        {
            //--公司团委级别不允许删除
            if(e.Level == 1)
                e.Cell.Controls[2].Visible = false;
            //--分支下不允许建立支部
            else if (e.Level == 3)
                e.Cell.Controls[1].Visible = false;
        }
    }

    protected void treeList_DataBound(object sender, EventArgs e)
    {
        this.treeList.ExpandToLevel(1);
    }

    protected void treeList_InitNewNode(object sender, ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CRE_DATE"] = DateTime.Now;
    }


    protected void OdsYouthGroup_Updated(object sender, ObjectDataSourceStatusEventArgs e)
    {

    }
    protected void OdsYouthGroup_Deleted(object sender, ObjectDataSourceStatusEventArgs e)
    {
    }
    protected void treeList_NodeDeleted(object sender, ASPxDataDeletedEventArgs e)
    {
    }
    protected void OdsYouthGroup_Inserting(object sender, ObjectDataSourceMethodEventArgs e)
    {

    }
}
