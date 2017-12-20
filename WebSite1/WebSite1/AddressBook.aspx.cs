using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddressBook : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGrid();
        }
    }

    private void bindGrid()
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
          "SELECT EmployeeID, Name, City, State, MobilePhone FROM Employees order by Name",
          conn);

        grid.DataKeyNames = new string[] { "EmployeeID" };

        // Enclose database code in Try-Catch-Finally
        try
        {
            // Open the connection
            conn.Open();
            // Execute the command
            reader = comm.ExecuteReader();
            // Bind the reader to the DataList
            grid.DataSource = reader;
            grid.DataBind();
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

    protected void gridSelectionChanged(Object sender, EventArgs e)
    {
        int selectredRowIndex = grid.SelectedIndex;
        GridViewRow row = grid.Rows[selectredRowIndex];
        string name = row.Cells[0].Text;
        string city = row.Cells[3].Text;
        //detailsLabel.Text = "You have selected a mother fucker named " + name + " who is from fucking " + city ;
        bindDetails();

    }

    protected void bindDetails()
    {
            int employeeId = (int)grid.DataKeys[grid.SelectedIndex].Value;
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
              "SELECT EmployeeID, Name, Address, City, State, Zip, Extension, HomePhone, MobilePhone FROM Employees where EmployeeId= @EmployeeId",
              conn);
            comm.Parameters.Add("@EmployeeId", System.Data.SqlDbType.Int);
            comm.Parameters["@EmployeeId"].Value = employeeId;
        employeeDetails.DataKeyNames = new string[] { "EmployeeID" };

            // Enclose database code in Try-Catch-Finally
            try
            {
                // Open the connection
                conn.Open();
                // Execute the command
                reader = comm.ExecuteReader();
            // Bind the reader to the DataList
            employeeDetails.DataSource = reader;
            employeeDetails.DataBind();
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

    protected void employeeDetails_ModeChanging(object sender, DetailsViewModeEventArgs e)
    {
        employeeDetails.ChangeMode(e.NewMode);
        bindDetails();
    }

    protected void employeeDetails_ItemUpdating(object sender, DetailsViewUpdateEventArgs e)
    {
        // get updated values
        //int employeeId= (int)grid.DataKeys[grid.SelectedIndex].Value;
        int employeeId = (int)employeeDetails.DataKey.Value;

        TextBox newAddressTextBox = (TextBox)employeeDetails.FindControl("editAddress");
        TextBox newStateTextBox = (TextBox)employeeDetails.FindControl("editState");
        TextBox newCityTextBox = (TextBox)employeeDetails.FindControl("editCity");
        string newAddress = newAddressTextBox.Text;
        string newCity = newCityTextBox.Text;
        string newState = newStateTextBox.Text;

        // update db
        SqlConnection conn;
        SqlCommand comm;
        // Read the connection string from Web.config
        string connectionString =
            ConfigurationManager.ConnectionStrings[
            "DorkNozzle"].ConnectionString;
        // Initialize connection
        conn = new SqlConnection(connectionString);
        string sqlString = "update employees set address=@address, state=@state, city=@city where employeeId = @employeeId";
        comm = new SqlCommand(sqlString, conn);

        comm.Parameters.Add("@employeeId", System.Data.SqlDbType.Int);
        comm.Parameters["@employeeId"].Value = employeeId;
        comm.Parameters.Add("@address", System.Data.SqlDbType.NVarChar, 50);
        comm.Parameters["@address"].Value = newAddress;
        comm.Parameters.Add("@state", System.Data.SqlDbType.NVarChar, 50);
        comm.Parameters["@state"].Value = newState;
        comm.Parameters.Add("@city", System.Data.SqlDbType.NVarChar, 50);
        comm.Parameters["@city"].Value = newCity;
        conn.Open();
        comm.ExecuteNonQuery();
        conn.Close();

        // change mode
        employeeDetails.ChangeMode(DetailsViewMode.ReadOnly);

        // refresh data to reflect updated values
        bindDetails();
        bindGrid();
    }
}
