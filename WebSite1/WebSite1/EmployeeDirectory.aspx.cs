using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeDirectory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadData();
        }
    }

    private void loadData()
    {
        // Define data objects
        SqlConnection conn;
        SqlCommand comm;
        SqlDataReader reader;
        // Read the connection string from Web.config
        string connectionString =
            ConfigurationManager.ConnectionStrings[
            "DorkNozzle"].ConnectionString;

        // Initialize connection
        conn = new SqlConnection(connectionString);
        // Create command
        comm = new SqlCommand(
          "SELECT EmployeeID, Name, Username FROM Employees",
          conn);
        // Enclose database code in Try-Catch-Finally
        try
        {
            // Open the connection
            conn.Open();
            // Execute the command
            reader = comm.ExecuteReader();
            // Bind the reader to the DataList
            employeeRepeater.DataSource = reader;
            employeeRepeater.DataBind();
            // Close the reader
            reader.Close();
        }
        catch
        {
            throw new Exception();
        }
        finally
        {
            // Close the connection
            conn.Close();
        }
    }

    protected void employeeRepeater_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "MoreDetailsPlease")
        {
            Literal li = (Literal)e.Item.FindControl("employeeDetailsLiteral");
            li.Text = "Employee Id: <strong>" + e.CommandArgument + "</strong><br />";
        }
        else if (e.CommandName == "EditItemPlease")
        {
            employeeRepeater.EditItemIndex = e.Item.ItemIndex;
            loadData();
        }
        else if (e.CommandName == "cancelEdits")
        {
            employeeRepeater.EditItemIndex = -1;
            loadData();
        }
        else if (e.CommandName == "updateEdits")
        {
            int employeeId = Convert.ToInt32(e.CommandArgument);
            TextBox nameTextBox = (TextBox)e.Item.FindControl("nameTextBox");
            TextBox userNameTextBox = (TextBox)e.Item.FindControl("userNameTextBox");

            saveDetails(employeeId, nameTextBox.Text, userNameTextBox.Text);

            employeeRepeater.EditItemIndex = -1;
            loadData();
        }
    }

    private void saveDetails(int employeeId, string newName, string newUserName)
    {
        // Define data objects
        SqlConnection conn;
        SqlCommand comm;
        // Read the connection string from Web.config
        string connectionString =
            ConfigurationManager.ConnectionStrings[
            "DorkNozzle"].ConnectionString;
        // Initialize connection
        conn = new SqlConnection(connectionString);
        string sqlString = "update employees set name=@name, username=@username where employeeId = @employeeId";
        comm = new SqlCommand(sqlString, conn);

        comm.Parameters.Add("@employeeId", System.Data.SqlDbType.Int);
        comm.Parameters["@employeeId"].Value = employeeId; // any random value for now
        comm.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 50);
        comm.Parameters["@name"].Value = newName;
        comm.Parameters.Add("@username", System.Data.SqlDbType.NVarChar, 50);
        comm.Parameters["@username"].Value = newUserName;
        conn.Open();       
        comm.ExecuteNonQuery();
        conn.Close();
    }
}