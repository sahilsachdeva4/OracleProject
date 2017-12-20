using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class HelpDesk : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // Define data objects
            SqlConnection conn;
            SqlCommand categoryComm;
            SqlCommand subjectComm;
            SqlDataReader reader;
            // Read the connection string from Web.config
            string connectionString =
                ConfigurationManager.ConnectionStrings[
                "Dorknozzle"].ConnectionString;
            // Initialize connection
            conn = new SqlConnection(connectionString);
            // Create command to read the help desk categories
            categoryComm = new SqlCommand(
                "SELECT CategoryID, Category FROM HelpDeskCategories",
                conn);
            // Create command to read the help desk subjects
            subjectComm = new SqlCommand(
                "SELECT SubjectID, Subject FROM HelpDeskSubjects", conn);
            // Enclose database code in Try-Catch-Finally
            try
            {
                // Open the connection
                conn.Open();
                // Execute the category command
                reader = categoryComm.ExecuteReader();
                // Populate the list of categories
                CategoryList.DataSource = reader;
                CategoryList.DataValueField = "CategoryID";
                CategoryList.DataTextField = "Category";
                CategoryList.DataBind();
                // Close the reader
                reader.Close();
                // Execute the subjects command
                reader = subjectComm.ExecuteReader();
                // Populate the list of subjects
                SubjectList.DataSource = reader;
                SubjectList.DataValueField = "SubjectID";
                SubjectList.DataTextField = "Subject";
                SubjectList.DataBind();
                // Close the reader
                reader.Close();
            }
            finally
            {
                // Close the connection
                conn.Close();
            }
        }
    }

    protected void submitButton_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
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
            string sqlString = "insert into HelpDesk(EmployeeID,StationNumber,CategoryID,SubjectID,Description,StatusID) " +
                                    "values (@EmployeeID,@StationNumber,@CategoryID,@SubjectID,@Description,@StatusID)";
            comm = new SqlCommand(sqlString, conn);
            comm.Parameters.Add("@EmployeeID", System.Data.SqlDbType.Int);
            comm.Parameters["@EmployeeID"].Value = 5; // any random value for now
            comm.Parameters.Add("@StationNumber",
                System.Data.SqlDbType.Int);
            comm.Parameters["@StationNumber"].Value = StationTextBox.Text;
            comm.Parameters.Add("@CategoryID", System.Data.SqlDbType.Int);
            comm.Parameters["@CategoryID"].Value =
                CategoryList.SelectedItem.Value;
            comm.Parameters.Add("@SubjectID", System.Data.SqlDbType.Int);
            comm.Parameters["@SubjectID"].Value =
                SubjectList.SelectedItem.Value;
            comm.Parameters.Add("@Description",
                System.Data.SqlDbType.NVarChar, 50);
            comm.Parameters["@Description"].Value =
                descriptionTextBox.Text;
            comm.Parameters.Add("@StatusID", System.Data.SqlDbType.Int);
            comm.Parameters["@StatusID"].Value = 1; // any random value
            // Enclose database code in Try-Catch-Finally
            try
            {
                // Open the connection
                conn.Open();
                // Execute the command
                comm.ExecuteNonQuery();
                // Reload page if the query executed successfully
                Response.Redirect("HelpDesk.aspx");
            }
            catch
            {
                // Display error message
                dbErrorMessage.Text =
                    "Error submitting the help desk request! Please " +
                    "try again later, and/or change the entered data!";
            }
            finally
            {
                // Close the connection
                conn.Close();
            }

        }
    }
}