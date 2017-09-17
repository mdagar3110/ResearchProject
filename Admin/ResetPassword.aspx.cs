using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Research_Project.Admin
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spResetPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUsername = new SqlParameter("@UserName", txtUserName.Text);

                cmd.Parameters.Add(paramUsername);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    if (Convert.ToBoolean(rdr["ReturnCode"]))
                    {
                        SendPasswordResetEmail(rdr["Email"].ToString(), txtUserName.Text, rdr["UniqueId"].ToString());
                        lblMessage.Text = "An email with instructions to reset your password is sent to your registered email";
                    }
                    else
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Username not found!"; 
                    }
                }
            }
        }



        private void SendPasswordResetEmail(string ToEmail, string UserName, string UniqueId)
        {
            // MailMessage class is present is System.Net.Mail namespace
            MailMessage mailMessage = new MailMessage("mdagar3110@gmail.com", ToEmail);
            string emailFrom = System.Configuration.ConfigurationManager.AppSettings["uName"];
            string epassword = System.Configuration.ConfigurationManager.AppSettings["Pass"];

            // StringBuilder class is present in System.Text namespace
            StringBuilder sbEmailBody = new StringBuilder();
            sbEmailBody.Append("Dear " + UserName + ",<br/><br/>");
            sbEmailBody.Append("Please click on the following link to reset your password");
            sbEmailBody.Append("<br/>"); sbEmailBody.Append("http:localhost//Admin/changepassword.aspx?uid=" + UniqueId);
            sbEmailBody.Append("<br/><br/>");
            sbEmailBody.Append("<b>Oakland University</b>");

            
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(emailFrom);
                mail.To.Add(ToEmail);
                mail.Subject = "Reset Password";
                mail.Body = sbEmailBody.ToString();
                mail.IsBodyHtml = true;
                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential(emailFrom, epassword);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

                txtUserName.Text = "";
            
            }
            catch (Exception ex)
            {
                 ex.ToString();
                
            }
        }
    }
}