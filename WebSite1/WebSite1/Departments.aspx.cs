using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Departments : System.Web.UI.Page
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
        SqlConnection conn;
        SqlCommand comm;
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter;

        string connectionString =
            ConfigurationManager.ConnectionStrings[
            "DorkNozzle"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand("SELECT DepartmentID, Department FROM Departments", conn);
        
        adapter = new SqlDataAdapter(comm);
        
            if(ViewState["DepartmentsDataSet"] == null)
            {
                adapter.Fill(dataSet,"Departments");
                ViewState["DepartmentsDataSet"] = dataSet;
            }
            else
            {
                dataSet = (DataSet)ViewState["DepartmentsDataSet"];
            }

        //adapter.SelectCommand = new SqlCommand("select * from Employees", conn);
        //adapter.Fill(dataSet);

        //adapter.SelectCommand = new SqlCommand("select * from Employees",conn);
        //adapter.Fill(dataSet,"Employees");
        //{ 
        //    string sortExpression = "";

        //    if (gridSortDirection == SortDirection.Ascending)
        //    {
        //        sortExpression = SortDirection.Descending + "ASC";
        //    }
        //    else
        //    {
        //        sortExpression = SortDirection.Ascending + " DESC";
        //    }

        //    dataSet.Tables["Departments"].DefaultView.Sort = sortExpression;
        //} NOT WORKING CHECK LATER
        departmentsGrid.DataSource = dataSet.Tables["Departments"];
        //departmentsGrid.DataSource = dataSet;
            //departmentsGrid.DataSource = dataSet.Tables[1];
            //departmentsGrid.DataMember = "Employees";
            //departmentsGrid.AutoGenerateColumns = true;
            //sometimes you need this to display columns(maybe a bug with Visual Studio)

            departmentsGrid.DataBind();
        
    }

    protected void departmentsGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        departmentsGrid.PageIndex = e.NewPageIndex;
        bindGrid();
    }

    protected void departmentsGrid_Sorting(object sender, GridViewSortEventArgs e)
    {
        string sortExpression = e.SortExpression;
        if(sortExpression == gridSortExpression)
        {
            if(gridSortDirection == SortDirection.Ascending)
            {
                gridSortDirection = SortDirection.Descending;
            }
            else
            {
                gridSortDirection = SortDirection.Ascending;
            }
        }
        else
        {
            gridSortDirection = SortDirection.Ascending;
        }
        gridSortExpression = sortExpression;
        bindGrid();
    }

    private String gridSortExpression
    {
        get{
            if(ViewState["gridSortExpression"] == null)
            {
                ViewState["gridSortExpression"] = "DepartmentID";
            }
            return (String)ViewState["gridSortExpression"];
        }

        set{
            ViewState["gridSortExpression"] = value;
        }
    }

    private SortDirection gridSortDirection
    {
        get
        {
            if (ViewState["gridSortDirection"] == null)
            {
                ViewState["gridSortDirection"] = SortDirection.Ascending;
            }
            return (SortDirection)ViewState["gridSortDirection"];
        }

        set
        {
            ViewState["gridSortDirection"] = value;
        }
    }

    protected void AddButton_Click(object sender, EventArgs e)
    {
        AddButton.Visible = false;
        deptPane.Visible = true;

        SqlConnection conn;
        SqlCommand comm;
        DataSet dataSet = new DataSet();
        SqlDataAdapter adapter;

        string connectionString =
            ConfigurationManager.ConnectionStrings[
            "DorkNozzle"].ConnectionString;
        conn = new SqlConnection(connectionString);
        comm = new SqlCommand("SELECT DepartmentID, Department FROM Departments", conn);

        adapter = new SqlDataAdapter(comm);

        if (ViewState["DepartmentsDataSet"] == null)
        {
            adapter.Fill(dataSet);
            ViewState["DepartmentsDataSet"] = dataSet;
        }
        else
        {
            dataSet = (DataSet)ViewState["DepartmentsDataSet"];
        }

        DataRow row = dataSet.Tables["Departments"].NewRow();
        row["Department"] = deptName.Text;
        dataSet.Tables["Departments"].Rows.Add(row);

        SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

        adapter.Update(dataSet.Tables["Departments"]);
        ViewState["DepartmentsDataSet"] = null;
        bindGrid();
    }

    protected void CancelButton_Click(object sender, EventArgs e)
    {
        AddButton.Visible = true;
        deptPane.Visible = false;
        deptName.Text = "";
    }
}