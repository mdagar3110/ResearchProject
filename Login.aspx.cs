using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Research_Project
{
    public partial class Login : System.Web.UI.Page
    {

        #region // Protected Methods
     
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session.Abandon();
                Session.Clear();
            }
        }
        /// <summary>
        /// Purpose: button click method and storing the username in session value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            AuthenticateUser(txtUserName.Text, txtPassword.Text);
            Session["Uname"] = txtUserName.Text;
        }

        #endregion

        #region // Private Methods
        
        /// <summary>
        /// this method validate our credentials with database records
        /// this method is using form authentication and for more information refer Pragim tech.
        /// for form authentication video
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        private void AuthenticateUser(string username, string password)
        {
            
                // ConfigurationManager class is in System.Configuration namespace
                string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                // SqlConnection is in System.Data.SqlClient namespace
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    //Formsauthentication is in system.web.security
                    string encryptedpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

                    //sqlparameter is in System.Data namespace
                    SqlParameter paramUsername = new SqlParameter("@UserName", username);
                    SqlParameter paramPassword = new SqlParameter("@Password", encryptedpassword);

                    cmd.Parameters.Add(paramUsername);
                    cmd.Parameters.Add(paramPassword);

                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        int RetryAttempts = Convert.ToInt32(rdr["RetryAttempts"]);
                        if (Convert.ToBoolean(rdr["AccountLocked"]))
                        {
                            lblMessage.Text = "Account locked. Please contact administrator";
                        }
                        else if (RetryAttempts > 0)
                        {
                            int AttemptsLeft = (4 - RetryAttempts);
                            lblMessage.Text = "Invalid user name and/or password. " +
                                AttemptsLeft.ToString() + "attempt(s) left";
                        }
                        else if (Convert.ToBoolean(rdr["Authenticated"]))
                        {
                            FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkBoxRememberMe.Checked);
                        }
                    }
                }


        }

        #endregion
    }
}