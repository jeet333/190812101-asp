﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Register : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseConnectionString1"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        //ConnectionString="<%$ ConnectionStrings:DatabaseConnectionString1 %>" 
        //DeleteCommand="DELETE FROM [users] WHERE [id] = @id" 
        //InsertCommand="INSERT INTO [users] ([fullname], [email], [password]) VALUES (@fullname, @email, @password)" 
        //ProviderName="<%$ ConnectionStrings:DatabaseConnectionString1.ProviderName %>" 
        //SelectCommand="SELECT [id], [fullname], [email], [password] FROM [users]" 
    }
    protected void Button1_Click(object sender, EventArgs e)
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
            
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            TextBox1.Text = string.Empty;
            TextBox2.Text = string.Empty;
            TextBox3.Text = string.Empty;
            
        }
    }
}