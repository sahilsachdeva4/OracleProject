using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lastLoggedInLabel.Text = "";
        }
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
        //updateSignOutTime((String)Session["Username"]);
        lastLoggedInLabel.Text = "";
        Session["Username"] = null;
        MultiView1.ActiveViewIndex = 0;
    }

    //private void updateSignOutTime(String userName)
    //{
    //    int employeeId = getEmployeeId(userName);
    //    OracleConnection conn = new OracleConnection(
    //            ConfigurationManager.ConnectionStrings["Oracle1ConnectionString"].ConnectionString);
    //    OracleCommand comm;
    //    string sqlString = "update EMPLOYEESHIFT set ENDTIME=:ENDTIME where " +
    //        "EMPLOYEEID=:EMPLOYEEID";
    //    comm = new OracleCommand(sqlString, conn);
    //    comm.CommandType = CommandType.Text; 
    //    comm.Parameters.Add("ENDTIME", System.DateTime.Now);
    //    comm.Parameters.Add("EMPLOYEEID", employeeId);
    //    try
    //    {
    //        conn.Open();
    //        comm.ExecuteNonQuery();
    //    }
    //    finally
    //    {
    //        conn.Close();
    //    }
    //}

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
            lastLoggedInLabel.Text = "You last logged in at " + getLoginTime(txtUsername.Text).ToString();
            //update sign in time...
            updateSignInTime(txtUsername.Text);

        }
        else if (outStatus == "0")
        {
            Session["Username"] = null;
            lblLoginStatus.Text = "Username or password was incorrect";
        }
    }

    private DateTime getLoginTime(String userName)
    {
        int employeeId = getEmployeeId(userName);
        DateTime date;
        OracleConnection conn = new OracleConnection(
            ConfigurationManager.ConnectionStrings["Oracle1ConnectionString"].ConnectionString);
        OracleCommand comm;
        OracleDataReader reader;
        string sqlString = "select LASTLOGINTIME from LOGINDETAILS where EMPID=:EMPID";
        comm = new OracleCommand(sqlString, conn);
        comm.CommandType = CommandType.Text;
        comm.Parameters.Add(":EMPID", employeeId);
        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            if (reader.Read())
            {
                try
                {
                    date = reader.GetDateTime(0);
                }
                catch (Exception)
                {
                    date = DateTime.Now;
                }
            }
            else
            {
                date = DateTime.Now;
            }
            reader.Close();
        }
        finally
        {
            conn.Close();
        }
        return date;
    }

    private void updateSignInTime(string userName)
    {
        if (Page.IsValid)
        {
            int employeeId = getEmployeeId(userName);
            OracleConnection conn = new OracleConnection(
                ConfigurationManager.ConnectionStrings["Oracle1ConnectionString"].ConnectionString);
            OracleCommand comm;
            string sqlString = "insert into EMPLOYEESHIFT(" +
                "EMPLOYEEID,STARTTIME,ENDTIME,TOTALWEEKLYHOURS) " +
                                    "values (" +
                                    ":EMPLOYEEID,:STARTTIME,:ENDTIME,:TOTALWEEKLYHOURS)";
            comm = new OracleCommand(sqlString, conn);
            comm.CommandType = CommandType.Text;
            comm.Parameters.Add(":EMPLOYEEID", employeeId);
            comm.Parameters.Add(":STARTTIME", System.DateTime.Now);
            comm.Parameters.Add(":ENDTIME", "");
            comm.Parameters.Add(":TOTALWEEKLYHOURS", "");
            try
            {
                conn.Open();
                comm.ExecuteNonQuery();
            } finally
            {
                conn.Close();
            }
        }
    }

    private int getEmployeeId(string userName)
    {
        int employeeId;
        OracleConnection conn = new OracleConnection(
            ConfigurationManager.ConnectionStrings["Oracle1ConnectionString"].ConnectionString);
        OracleCommand comm;
        OracleDataReader reader;
        string sqlString = "select EMPLOYEEID from DN_EMPLOYEES where USERNAME=:NAME";
        comm = new OracleCommand(sqlString, conn);
        comm.CommandType = CommandType.Text;
        comm.Parameters.Add(":NAME", userName);
        try
        {
            conn.Open();
            reader = comm.ExecuteReader();
            if (reader.Read())
            {
                employeeId = reader.GetInt32(0);
            }
            else
            {
                employeeId = 0;
            }
            reader.Close();
        }
        finally
        {
            conn.Close();
        }
        return employeeId;
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
}
