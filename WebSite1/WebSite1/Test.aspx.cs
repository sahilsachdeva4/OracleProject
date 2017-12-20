using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int[] x = { };
        try
        {
            x[0] = 0;
        }catch(IndexOutOfRangeException ex)
        {
            ErrorLabel.Text = "You are the culprit!" + ex.Message;
            throw;
        }
    }
}