using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Web;
using DevExpress.Web.ASPxClasses;

public partial class ReportViewerControl : System.Web.UI.UserControl {
	ITemplate documentMapContainer;
	DocumentMapContainer container;

	public ReportViewer ReportViewer { get { return ReportViewer1; } }
    public string ReportName {
        get { return ReportViewer1.ReportName; }
        set { ReportViewer1.ReportName = value; }
    }
    public XtraReport Report {
        get { return ReportViewer1.Report; }
        set { ReportViewer1.Report = value; }
    }
	public bool ReportToolbarsAutoWidth {
		get { return ReportToolbar1.Width == Unit.Percentage(100) || ReportToolbar2.Width == Unit.Percentage(100); }
		set { ReportToolbar1.Width = ReportToolbar2.Width = (value ? Unit.Percentage(100) : Unit.Empty); }
	}
	[
	TemplateContainer(typeof(DocumentMapContainer)),
	PersistenceMode(PersistenceMode.InnerProperty),
	]
	public ITemplate DocumentMapContainer {
		get { return documentMapContainer; }
		set { documentMapContainer = value; }
	}
	public Control FindControlInDocumentMapContainer(string id) {
		return container.FindControl(id);
	}
	void Page_Init() {
		if(documentMapContainer != null) {
			Panel1.Visible = true;
			container = new DocumentMapContainer();
			documentMapContainer.InstantiateIn(container);
			DocumentMapPlaceHolder.Controls.Add(container);
		}
		Page.Header.Controls.Add(new LiteralControl(Helper.GetPageBorderCSSLink()));
	}
	protected void Page_Load(object sender, EventArgs e) {
		DataBind();
	}
}
public class DocumentMapContainer : Control, INamingContainer { }

