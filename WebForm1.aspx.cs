using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace AspCrud
{
	public partial class WebForm1 : System.Web.UI.Page
	{
		//public static SqlConnection cn = new SqlConnection("Data Source=laptop-h9ukkovf\\dev_server;Initial Catalog=WebcrudDB;Integrated Security=True");
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				GetData();
			}
		}

		private void GetData()
		{
			string sql = "select * from Students";
			SqlDataAdapter da = new SqlDataAdapter(sql, Class1.cn);
			DataTable dt = new DataTable();
			da.Fill(dt);
			Viewdata.DataSource = dt;
			Viewdata.DataBind();

		}
		protected void Button1_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(TextBox1.Text) || string.IsNullOrEmpty(TextBox2.Text))
			{
				// You can display an error message in a label or use a JavaScript alert
				lblMessage.Text = "Please enter both Name and Course.";
				lblMessage.ForeColor = System.Drawing.Color.Red;
				return; // Stop further execution
			}
			string sql = "INSERT INTO Students values( '" + TextBox1.Text + "', '" + TextBox2.Text + "')";
				SqlDataAdapter da = new SqlDataAdapter(sql, Class1.cn);
				DataTable dt = new DataTable();
				da.Fill(dt);
			int count = dt.Rows.Count;
			if(count == 1)
            {
				lblMessage.Text = "Record added successfully!";
				lblMessage.ForeColor = System.Drawing.Color.Green;
			}
			else
            {
				lblMessage.Text = "Not inserted";
				lblMessage.ForeColor = System.Drawing.Color.Red;
			}
			Clear();
			GetData();

		}
		private void Clear()
		{
			TextBox1.Text = "";
			TextBox2.Text = "";
			TextBox3.Text = "";
			TextBox3.Focus();

		}
		protected void Button3_Click(object sender, EventArgs e)
		{
			string sql = "UPDATE Students SET  Name = '" + TextBox1.Text + "', Course = '" + TextBox2.Text + "' where Id = '" + TextBox3.Text +"' "; 
			SqlDataAdapter da = new SqlDataAdapter(sql, Class1.cn);
			DataTable dt = new DataTable();
			da.Fill(dt);
			Clear();
			GetData();
		}

		protected void Button4_Click(object sender, EventArgs e)
		{
			string sql = "delete from Students where Id = '" + TextBox3.Text + "'";
			SqlDataAdapter da = new SqlDataAdapter(sql, Class1.cn);
			DataTable dt = new DataTable();
			da.Fill(dt);
			Clear();
			GetData();
		}

		protected void Viewdata_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Get the selected row
			GridViewRow row = Viewdata.SelectedRow;

			// Example: Display the selected row data (for testing)
			string id = row.Cells[1].Text; // Assuming the ID is in the first column
			string name = row.Cells[2].Text; // Assuming the Name is in the second column
			string course = row.Cells[3].Text; // Assuming the Course is in the third column

			// You can display these in a Label or use them for other purposes
			TextBox3.Text =  id;
			TextBox1.Text =  name;
			TextBox2.Text =  course;

		}
    }
}