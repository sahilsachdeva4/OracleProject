using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddressBookV2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void employeeDetails_ItemUpdated(object sender, DetailsViewUpdatedEventArgs e)
    {
        grid.DataBind();
    }

    protected void employeeDetails_ItemInserted(object sender, DetailsViewInsertedEventArgs e)
    {
        grid.DataBind();
    }

    protected void employeeDetails_ItemDeleted(object sender, DetailsViewDeletedEventArgs e)
    {
        grid.DataBind();
    }

    protected void insertNewRecord_Click(object sender, EventArgs e)
    {
        employeeDetails.ChangeMode(DetailsViewMode.Insert);
    }
}