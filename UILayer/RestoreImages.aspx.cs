using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.IO;
using Research_Project.BAL;
namespace Research_Project.UILayer
{
    public partial class RestoreImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblResult.Text = "";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ArrayList arrFileName = new ArrayList();
            if (imgUpload.HasFile)
            {
                foreach (HttpPostedFile file in imgUpload.PostedFiles)
                {
                    arrFileName.Add(file.FileName);
                    string rootPath = Server.MapPath("~/Images/");
                    file.SaveAs(Path.Combine(rootPath, file.FileName));
                    
                 }

                 Common objCommmon = new Common();
                try
                {
                    objCommmon.SaveFile(arrFileName);
                    lblResult.Text= "Congratulation! "+arrFileName.Count+" Images has been uploaded in Database.";

                }
                catch(Exception ex)
                {
                      throw ex;//Response.Redirect("~/NotFound.aspx", true);
                }
            }

        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("~/HOme.aspx");
            }
            catch(Exception ex)
            {
                Response.Redirect("~/NotFound.aspx",true);
            }
            }
    }
}