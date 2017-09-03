using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using DocumentFormat.OpenXml;
using System.Drawing;
namespace Research_Project
{
    public partial class Admin_Reports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionOut();
                BindGrid();
            }

        }

        private void BindGrid()
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spAdminReport", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramUsername = new SqlParameter("@UserName", Session["Uname"].ToString());
                    cmd.Parameters.Add(paramUsername);
                    con.Open();
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {

                        gvAdminReports.DataSource = dt;
                        gvAdminReports.DataBind();
                        ViewState["dt_Excel"] = dt;
                    }


                }
            }
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        private void SessionOut()
        {
            if (string.IsNullOrEmpty(Session["Uname"] as string))
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Home.aspx");
        }

        protected void gvAdminReports_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAdminReports.PageIndex = e.NewPageIndex;
            BindGrid(); 
        }

        protected void btnExcelDownload_Click(object sender, EventArgs e)
        {

            DataTable   dt=null;
            try
            {
             dt = (DataTable)ViewState["dt_Excel"];

                        Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                using (StringWriter sw = new StringWriter())
                {
                    HtmlTextWriter hw = new HtmlTextWriter(sw);
 
                    //To Export all pages
                    gvAdminReports.AllowPaging = false;
                    this.BindGrid();
 
                    gvAdminReports.HeaderRow.BackColor = Color.White;
                    foreach (TableCell cell in gvAdminReports.HeaderRow.Cells)
                    {
                        cell.BackColor = gvAdminReports.HeaderStyle.BackColor;
                        cell.BorderColor = Color.Black;
                        cell.BorderStyle = BorderStyle.Solid;
                    }
                    foreach (GridViewRow row in gvAdminReports.Rows)
                    {
                        row.BackColor = Color.White;
                        
                        foreach (TableCell cell in row.Cells)
                        {
                            if (row.RowIndex % 2 == 0)
                            {
                                cell.BackColor = gvAdminReports.AlternatingRowStyle.BackColor;
                                cell.BorderColor = Color.Black;
                                cell.BorderStyle = BorderStyle.Solid;
                            }
                            else
                            {
                                cell.BackColor = gvAdminReports.RowStyle.BackColor;
                                cell.BorderColor = Color.Black;
                                cell.BorderStyle = BorderStyle.Solid;
                            }
                            cell.CssClass = "textmode";
                        }
                    }
 
                    gvAdminReports.RenderControl(hw);
           
                    //style to format numbers to string
                    string style = @"<style> .textmode { } </style>";
                    Response.Write(style);
                    Response.Output.Write(sw.ToString());
                    Response.Flush();
                    Response.End();
                }
             }

            
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
            finally
            {
                dt = null;
            }  
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

      
    }
}