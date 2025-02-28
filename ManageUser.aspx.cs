﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Default2 : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        Print();
        //DeleteCommand="DELETE FROM [users] WHERE [id] = @id" 
        //InsertCommand="INSERT INTO [users] ([fullname], [email], [password]) VALUES (@fullname, @email, @password)" 
        //SelectCommand="SELECT [id], [fullname], [email], [password] FROM [users]" 
        //UpdateCommand="UPDATE [users] SET [fullname] = @fullname, [email] = @email, [password] = @password WHERE [id] = @id">
    }
    private void Print()
    {
        SqlDataAdapter adpt = new SqlDataAdapter("SELECT [id], [fullname], [email], [password] FROM [users]", con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        GridView1.DataSource = dt;
        GridView1.DataBind();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Update")
        {
            SqlCommand cmd = new SqlCommand("UPDATE [users] SET [fullname] = @fullname, [email] = @email, [password] = @password WHERE [id] = @id", con);
            cmd.Parameters.AddWithValue("@fullname", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@email", TextBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@password", TextBox3.Text.Trim());
            cmd.Parameters.AddWithValue("@id", ViewState["user_id"]);
            con.Open();
            int s = cmd.ExecuteNonQuery();
            con.Close();
            if (s == 1)
            {
                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBox3.Text = string.Empty;
                Print();
                Literal1.Text = "User updated successfully!";
            }
            else
            {
                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBox3.Text = string.Empty;
                Literal1.Text = "Error!";
            }
        }
        else
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO [users] ([fullname], [email], [password]) VALUES (@fullname, @email, @password)", con);
            cmd.Parameters.AddWithValue("@fullname", TextBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@email", TextBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@password", TextBox3.Text.Trim());
            con.Open();
            int s = cmd.ExecuteNonQuery();
            con.Close();
            if (s == 1)
            {
                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBox3.Text = string.Empty;
                Literal1.Text = "Registration successfully completed!";
                Print();
            }
            else
            {
                TextBox1.Text = string.Empty;
                TextBox2.Text = string.Empty;
                TextBox3.Text = string.Empty;
                Literal1.Text = "Error!";
            }
        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        SqlCommand cmd = new SqlCommand("DELETE FROM [users] WHERE [id] = @id", con);
        cmd.Parameters.AddWithValue("@id", btn.CommandArgument);
        con.Open();
        int s = cmd.ExecuteNonQuery();
        con.Close();
        if (s == 1)
        {
            Literal1.Text = "User deleted successfully!";
            Print();
        }
        else
        {
            Literal1.Text = "Error!";
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        SqlDataAdapter adpt = new SqlDataAdapter("SELECT [id], [fullname], [email], [password] FROM [users]  WHERE [id] =" + btn.CommandArgument, con);
        DataTable dt = new DataTable();
        adpt.Fill(dt);
        TextBox1.Text = dt.Rows[0][1].ToString();
        TextBox2.Text = dt.Rows[0][2].ToString();
        TextBox3.Text = dt.Rows[0][3].ToString();
        ViewState["user_id"] = btn.CommandArgument;
        Button1.Text = "Update";
    }
}