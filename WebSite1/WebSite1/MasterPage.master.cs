using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Username"] == null)
        {
            MultiView1.ActiveViewIndex = 0;
        }
        else
        {
            MultiView1.ActiveViewIndex = 2;
            lblUsername.Text = Session["Username"].ToString();
        }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        Response.Redirect("Register.aspx");
    }

    protected void btnLogout_Click(object sender, EventArgs e)
    {
        Session["Username"] = null;
        MultiView1.ActiveViewIndex = 0;
    }

    protected void btnLogin2_Click(object sender, EventArgs e)
    {
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["Oracle1ConnectionString"].ConnectionString);
        OracleCommand cmd = new OracleCommand("DN_AUTHENTICATE", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        string encodedPass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
        cmd.Parameters.Add("Username", txtUsername.Text);
        cmd.Parameters.Add("Password", encodedPass);

        OracleParameter outPutParameter = new OracleParameter();
        outPutParameter.ParameterName = "AUTHENTICATESTATUS";
        outPutParameter.OracleDbType = OracleDbType.Int16;
        outPutParameter.Direction = System.Data.ParameterDirection.Output;
        cmd.Parameters.Add(outPutParameter);

        conn.Open();

        cmd.ExecuteNonQuery();

        //Retrieve the value of the output parameter
        string outStatus = outPutParameter.Value.ToString();

        conn.Close();

        if (outStatus == "1")
        {
            Session["Username"] = txtUsername.Text;
            MultiView1.ActiveViewIndex = 2;
            lblUsername.Text = Session["Username"].ToString();
        }
        else if (outStatus == "0")
        {
            Session["Username"] = null;
            lblLoginStatus.Text = "Username or password was incorrect";
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
}
