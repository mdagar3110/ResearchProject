using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Research_Project.UILayer
{
    public partial class Home : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionOut();
                Session["Isadmin"] = null;
                ViewState["Isadmin"] = null;
                if (Session["Uname"] != null)
                {
                    EnableAdminButtons(Session["Uname"].ToString());
                }
            }
        }

        private void SessionOut()
        {
            if (string.IsNullOrEmpty(Session["Uname"] as string))
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        public void EnableAdminButtons(string UserName)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spCheckAdmin", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter paramUsername = new SqlParameter("@UserName", UserName);

                    cmd.Parameters.Add(paramUsername);

                    con.Open();
                    int AdminStatus = (int)cmd.ExecuteScalar();

                    if (AdminStatus == 2)
                    {
                        btnMgr.Visible = true;
                        btnReport.Visible = true;
                        Session["Isadmin"] = "2";
                    }
                    else if (AdminStatus == 1)
                    {
                        btnReport.Visible = true;
                        btnMgr.Visible = false;
                        Session["Isadmin"] = "1";
                    }
                    else
                    {
                        btnReport.Visible = false;
                        btnMgr.Visible = false;
                        Session["Isadmin"] = "0";
                        Response.Redirect("~/Survey.aspx");
                    }

                }
            }
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            // IsAdmin 0 means it is a normal user without any admin righst
            if (!Session["Isadmin"].Equals(0))
            {
                Response.Redirect("~/Admin Reports.aspx");

            }
        }

        protected void btnMgr_Click(object sender, EventArgs e)
        {
            
                Response.Redirect("~/ManageUsers.aspx");
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Survey.aspx");
        }

        protected void btnResetUserRights_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/ManageUsers.aspx");
        }

        protected void btnUploadImages_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UILayer/RestoreImages.aspx");
        }
    }
}