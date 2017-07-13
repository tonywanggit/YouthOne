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

public partial class BaseInfo_Organize : BasePage
{
    private OrganizeTableAdapter ogAdapter = new OrganizeTableAdapter();

    protected void Page_Load(object sender, EventArgs e)
    {
        InitPage();
        InitRight();

    }

    protected void InitRight()
    {
        if (AuthUser.RoleName != AuthenUserType.Admin && AuthUser.RoleName != AuthenUserType.TW_Admin)
        {
            treeList.Columns["OP_COL"].Visible = false;
            treeList.SettingsEditing.AllowNodeDragDrop = false;
        }
    }

    protected void InitPage()
    {
        this.treeList.HtmlCommandCellPrepared += new TreeListHtmlCommandCellEventHandler(treeList_HtmlCommandCellPrepared);
        this.treeList.InitNewNode += new ASPxDataInitNewRowEventHandler(treeList_InitNewNode);
        this.treeList.DataBound += new EventHandler(treeList_DataBound);

        this.treeList.ProcessDragNode += new TreeListNodeDragEventHandler(treeList_ProcessDragNode);
    }

    void treeList_ProcessDragNode(object sender, TreeListNodeDragEventArgs e)
    {
        //--如果目标节点和移动节点属于同一个父节点下，前两者不同，则执行移动操作
        if (e.Node.ParentNode.Key == e.NewParentNode.ParentNode.Key && e.Node.Key != e.NewParentNode.Key)
        {
            ogAdapter.MoveNode(e.Node.Key, e.NewParentNode.Key, e.Node.ParentNode.Key);
            treeList.DataBind();
        }

        e.Cancel = true;
    }

    void treeList_DataBound(object sender, EventArgs e)
    {
        this.treeList.ExpandToLevel(1);
    }

    void treeList_InitNewNode(object sender, ASPxDataInitNewRowEventArgs e)
    {
        e.NewValues["CRE_DATE"] = DateTime.Now;
    }

    void treeList_HtmlCommandCellPrepared(object sender, TreeListHtmlCommandCellEventArgs e)
    {
        if (e.Cell.Controls.Count > 2)
        {
            //--长兴造船不允许删除
            if (e.Level == 1)
                e.Cell.Controls[2].Visible = false;
            //--科室作业区下不能新增组织
            else if (e.Level == 3)
                e.Cell.Controls[1].Visible = false;
        }
    }
}
