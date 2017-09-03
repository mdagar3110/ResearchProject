using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
namespace Research_Project
{
    public partial class ManageUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMessageReset.Text = "";
                lblMessage.Text = "";
                SessionOut();
                BindGrid();
            }
        }

        private void SessionOut()
        {
            if (string.IsNullOrEmpty(Session["Uname"] as string))
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void BindGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spManageUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@status", "SELECT");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        gvUsers.DataSource = ds;
                        gvUsers.DataBind();
                    }
                    else
                    {
                        ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        gvUsers.DataSource = ds;
                        gvUsers.DataBind();
                        int columncount = gvUsers.Rows[0].Cells.Count;
                        gvUsers.Rows[0].Cells.Clear();
                        gvUsers.Rows[0].Cells.Add(new TableCell());
                        gvUsers.Rows[0].Cells[0].ColumnSpan = columncount;
                        gvUsers.Rows[0].Cells[0].Text = "No Records Found";
                    }

                }
            }
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        protected void gvUsers_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvUsers.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvUsers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUsers.EditIndex = -1;
            BindGrid();
        }



        protected void gvUsers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                string UserName = gvUsers.DataKeys[e.RowIndex].Values["UserName"].ToString();

                RadioButtonList IsAdmin = (gvUsers.Rows[e.RowIndex].FindControl("rdbAdmin") as RadioButtonList);
                RadioButtonList IsSuperAdmin = (gvUsers.Rows[e.RowIndex].FindControl("rdbSuperAdmin") as RadioButtonList);

                CRUDOperations("UPDATE", UserName, IsAdmin.SelectedValue.ToString(), IsSuperAdmin.SelectedValue.ToString());
            }
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        protected void CRUDOperations(string status, string UserName, string IsAdmin, string IsSuperAdmin)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spManageUsers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (status == "UPDATE")
                    {
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@IsAdmin", IsAdmin);
                        cmd.Parameters.AddWithValue("@IsSuperAdmin", IsSuperAdmin);
                        cmd.Parameters.AddWithValue("@status", status);
                    }
                    else if (status == "DELETE")
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                    }

                    cmd.ExecuteNonQuery();
                    gvUsers.EditIndex = -1;
                    BindGrid();
                }
            }
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                // ConfigurationManager class is in System.Configuration namespace
                string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                // SqlConnection is in System.Data.SqlClient namespace
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spAddAdmin", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //sqlparameter is in System.Data namespace
                    SqlParameter paramUsername = new SqlParameter("@Email", txtEmail.Text);
                    cmd.Parameters.Add(paramUsername);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int IsAdmin = Convert.ToInt32(rdr["Admin"]);
                        if (IsAdmin == 1)
                        {
                            lblMessage.Text = "User Has been assigned Admin rights";
                            BindGrid();
                            txtEmail.Text = "";
                        }
                        else
                        {
                            lblMessage.Text = "User doesn't exist, Please enter valid Email Id";
                            txtEmail.Text = "";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }



        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HOme.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {

                lblMessageReset.Text = "";
                // ConfigurationManager class is in System.Configuration namespace
                string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                // SqlConnection is in System.Data.SqlClient namespace
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spResetUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //sqlparameter is in System.Data namespace
                    SqlParameter paramUsername = new SqlParameter("@Email", TextBox1.Text);
                    cmd.Parameters.Add(paramUsername);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int IsAdmin = Convert.ToInt32(rdr["Reset"]);
                        if (IsAdmin == 1)
                        {
                            lblMessageReset.Text = "User Has been rights has been Reset";
                            BindGrid();
                            TextBox1.Text = "";
                        }
                        else
                        {

                            lblMessageReset.Text = "User doesn't exist, Please enter valid Email Id";
                            TextBox1.Text = "";
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }

        }
    }
}