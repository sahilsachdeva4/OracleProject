using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnRegister_Click(object sender, EventArgs e)
    {
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["Oracle1ConnectionString"].ConnectionString);
        OracleCommand cmd = new OracleCommand("DN_Register", conn);
        cmd.CommandType = System.Data.CommandType.StoredProcedure;

        string encodedPass = FormsAuthentication.HashPasswordForStoringInConfigFile(txtPassword.Text, "SHA1");
        cmd.Parameters.Add("P_Username", txtUsername.Text);
        cmd.Parameters.Add("P_Name", txtName.Text);
        cmd.Parameters.Add("P_Password", encodedPass);
        cmd.Parameters.Add("P_Department", ddlDepartments.SelectedValue);
        cmd.Parameters.Add("P_Email", txtEmail.Text);

        OracleParameter outPutParameter = new OracleParameter();
        outPutParameter.ParameterName = "REGISTERSTATUS";
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
            lblStatus.Text = "Username already exists";
        }
        else if (outStatus == "2")
        {
            lblStatus.Text = "email already exists";
        }
        else if (outStatus == "3")
        {
            Response.Redirect("Default.aspx");
        }
    }

}