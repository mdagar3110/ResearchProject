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
    public partial class Survey : System.Web.UI.Page
    {
      // this i variable is to count number of questions per attempt

        int CorrectAnswers = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SessionOut();
                IsAnswerReset(Session["Uname"].ToString());
                int i = 1;
                ViewState["IValue"] = i; // to count the number of questions
                btnTest.Visible = true;
                pnlShowHide.Visible = false;
                lblTimer.Visible = false;
                Session["time"] = System.Configuration.ConfigurationManager.AppSettings["TotalTestTime"];
                Session["lstanswers"] = null;
                ViewState["PreviousTime"] = System.Configuration.ConfigurationManager.AppSettings["TotalTestTime"];
            }
         
            CallTimer();
        }

        private void IsAnswerReset(string UserName)
        {
            try
            {
                string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                // SqlConnection is in System.Data.SqlClient namespace
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spCheckIsAnswerReset", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramUsername = new SqlParameter("@UserName", UserName);
                    cmd.Parameters.Add(paramUsername);
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        ShowModelPopUp("OOPS!! Looks like you have attended the Test Before, If you want to re-attempt Please Contact to Administration", "");
                    }

                }
            }
            catch (Exception ex)
            {
                //  throw ex;//Response.Redirect("~/NotFound.aspx", true);

            }

            
        }
        private void SessionOut()
        {
            if (string.IsNullOrEmpty(Session["Uname"] as string))
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        private void CallTimer()
        {
            Timer1.Enabled = true;
            Timer1.Interval = 1000;
            Timer1.Tick += new EventHandler<EventArgs>(Timer1_Tick);

        }

        private string ConvertTimetoString(int time)
        {
            int seconds = time % 60;
            int minutes = time / 60;
            string string_time = minutes.ToString("00") + ":" + seconds.ToString("00");
            return string_time;
        }

       private void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                // if=totalSeconds
                int t = Convert.ToInt32(Session["time"]);
                string time = ConvertTimetoString(t);
                if (t <= 30)// if seconds are less than 10
                {
                    lblTimer.ForeColor = System.Drawing.Color.Red;

                }
                lblTimer.Text = time;
                if (t > 0)
                    t = t - 1;
                Session["time"] = t;

                //Label1.Text = i.ToString();
                if (t <= 0)
                {
                    Timer1.Enabled = false;
                    updateAnswers();
                    //popupmessage();
                    ShowModelPopUp("Time Out!! Your Score is: ", "timeout");
                }
            }
            catch (Exception ex)
            {
                  throw ex;// throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        /// <summary>
        /// This method is used to save the answers to the Database
        /// No matter either on time out or finish of test
        /// </summary>
       private void SaveAnswer()
       {
           updateAnswers();
       }

       private string ConvertListtoString(List<int> answers)
       { string OutAnswer="";
       try
       {

           foreach (int i in answers)
           {
               if (i == 1)
                   OutAnswer += "A ";
               if (i == 2)
                   OutAnswer += "B ";
               if (i == 3)
                   OutAnswer += "C ";
               if (i == 4)
                   OutAnswer += "D ";
           }
       }
       catch (Exception ex)
       {
             throw ex;//Response.Redirect("~/NotFound.aspx", true);
       }

         return OutAnswer;
       }

        private void updateAnswers()
        {
            try
            {
                //to get the timespan
                int previousTime = Convert.ToInt32(ViewState["PreviousTime"]);
                int CurrentTime = Convert.ToInt32(Session["time"]);
                ViewState["PreviousTime"] = CurrentTime;
                string TimeStamp = ConvertTimetoString(previousTime - CurrentTime);

                // List of expected Answers
                List<int> lstAnswers = new List<int>();
                lstAnswers = (List<int>)Session["lstanswers"];

                string UserResponse = "";
                if (chk1.Checked)//&& lstAnswers.Contains(1))
                {
                    UserResponse += "A ";
                    CorrectAnswers += 1;
                }
                if (chk2.Checked)// && lstAnswers.Contains(2))
                {
                    UserResponse += "B ";
                    CorrectAnswers += 1;
                }
                if (chk3.Checked)// && lstAnswers.Contains(3))
                {
                    UserResponse += "C ";
                    CorrectAnswers += 1;
                }
                if (chk4.Checked)// && lstAnswers.Contains(4))
                {
                    UserResponse += "D ";
                    CorrectAnswers += 1;
                }
                string UName = Session["Uname"].ToString();
                //  if (CorrectAnswers == 2)
                updateDataBase(UName, (int)ViewState["QuestionNo"], TimeStamp, ConvertListtoString(lstAnswers), UserResponse);
            }
            catch (Exception ex)
            {
                throw ex; //Response.Redirect("~/NotFound.aspx", true);
            }
           
        }

        private void updateDataBase(string UserName,int QuestionNumber, string TimeStamp, string ExpectedAnswers, string UserResponse) 
        {
            try
            {
                string Question_Image = imgQuestion.ImageUrl.Split('/').Last();
                string Img1 = img1.ImageUrl.Split('/').Last();
                string Img2 = img2.ImageUrl.Split('/').Last();
                string Img3 = img3.ImageUrl.Split('/').Last();
                string Img4 = img4.ImageUrl.Split('/').Last();
                String Options = "A/" + Img1 + ", B/" + Img2 + ", C/" + Img3 + ", D/" + Img4;// for string concatenation

                bool IsAnswerCorrect = false;

                if (ExpectedAnswers.Equals(UserResponse))
                {
                    IsAnswerCorrect = true;
                }
                string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                using (SqlConnection con = new SqlConnection(CS))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateAnswers", con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter username = new SqlParameter("@UserName", UserName);
                    SqlParameter NoOfAnswers = new SqlParameter("@count", IsAnswerCorrect);
                    SqlParameter Q_No = new SqlParameter("@Q_No", QuestionNumber);
                    SqlParameter TimeTaken = new SqlParameter("@TimeStamp", TimeStamp);
                    SqlParameter Q_Image = new SqlParameter("@Q_Image", Question_Image);
                    SqlParameter G_Options = new SqlParameter("@GivenOptions", Options);
                    SqlParameter ExpectedReply = new SqlParameter("@Expected_Answer", ExpectedAnswers);
                    SqlParameter UserReply = new SqlParameter("@UserResponse", UserResponse);
                    cmd.Parameters.Add(username);
                    cmd.Parameters.Add(NoOfAnswers);
                    cmd.Parameters.Add(Q_No);
                    cmd.Parameters.Add(TimeTaken);
                    cmd.Parameters.Add(Q_Image);
                    cmd.Parameters.Add(G_Options);
                    cmd.Parameters.Add(ExpectedReply);
                    cmd.Parameters.Add(UserReply);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }

        }

        private void BindImages(int? i)
        {
            try
            {
                DataSet ds = new DataSet();
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("spGetImages", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    // cmd.Parameters.AddWithValue("@status", "SELECT");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    con.Close();

                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            imgQuestion.ImageUrl = "~/Images/" + ds.Tables[0].Rows[0]["ImageName"].ToString();

                        }
                        if (ds.Tables[1].Rows.Count != 0)
                        {
                            DataTable dt = ds.Tables[1];
                            img1.ImageUrl = "~/Images/" + dt.Rows[0]["ImageName"].ToString();
                            img2.ImageUrl = "~/Images/" + dt.Rows[1]["ImageName"].ToString();
                            img3.ImageUrl = "~/Images/" + dt.Rows[2]["ImageName"].ToString();
                            img4.ImageUrl = "~/Images/" + dt.Rows[3]["ImageName"].ToString();

                        }
                        lblQuestionNo.Text = ViewState["IValue"].ToString();
                        FindAnswers(ds.Tables[1], ds.Tables[0].Rows[0]["BlockNo"].ToString());
                        ResetChkBox();
                    }

                }
            }

            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        private void ResetChkBox() {
            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;
        }

        /// <summary>
        /// This method is used to store the block no values which matches with correct answer
        /// lstAnswers list of intergers would be used later to cross check the answers
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="BlockNo"></param>
        private void FindAnswers(DataTable dt, string BlockNo)
        {
            try
            {
                int recordnumber = 1;
                List<int> lstAnswers = new List<int>();
                foreach (DataRow row in dt.Rows)
                {
                    if (row["BlockNo"].ToString() == BlockNo)
                    {
                        lstAnswers.Add(recordnumber);

                    }
                    recordnumber++;

                } Session["lstanswers"] = lstAnswers;
            }

            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        protected void btnTest_Click(object sender, EventArgs e)
        {
            BindImages(1);
            pnlShowHide.Visible = true;
            lblTimer.Visible = true;
            btnTest.Visible = false;
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                ViewState["CurrentTime"] = Session["time"];
                // int maxQuestion =System.Configuration.ConfigurationManager.AppSettings["maxQuestion"];
                if ((int)ViewState["IValue"] < 24)
                {
                    ViewState["QuestionNo"] = ViewState["IValue"];
                    int i = (int)ViewState["IValue"] + 1;
                    ViewState["IValue"] = i;
                    SaveAnswer();
                    BindImages(i);


                }
                else
                {
                    ViewState["QuestionNo"] = 24;
                    ViewState["IValue"] = 24;// to keep the question number for last question
                    SaveAnswer();
                    string Message = "Congratulation!! You finished the Survey. </br> You scored is: ";
                    ShowModelPopUp(Message, "complete");
                }
                // Session["lstanswers"] = lstAnswers;
            }

            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
            }
        }

        /// <summary>
        /// This Function call the Popup 
        /// Pass string message to the popup
        /// if this function is called when we found User already take the survery then we only pass the message to popup
        /// otherwise we pass the correct answered value also to popup
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgType"></param>
        private void ShowModelPopUp(string msg,string msgType)
        {

            if (string.IsNullOrEmpty(msgType))
            {
                lblMessage.Text = msg + "";
                Label1.Visible = false;
            }
            else
            {
               // int correctAnswers = GetCorrectAnswersValue(Session["Uname"].ToString());

              //  Label1.Text = msg;//+ "" +correctAnswers;
                Label1.Visible = true;
            }
            ModalPopupExtender2.Show();
        }

        /// <summary>
        /// This function is to get the correct number of answers attempted by the user
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        private int GetCorrectAnswersValue(string username)
        {
            try
            {
                int countAnswers;
                string CS = ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                // SqlConnection is in System.Data.SqlClient namespace
                using (SqlConnection con = new SqlConnection(CS))
                {
                    SqlCommand cmd = new SqlCommand("spGetTotalAnswers", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter paramUsername = new SqlParameter("@UserName", username);
                    cmd.Parameters.Add(paramUsername);
                    con.Open();
                    if (cmd.ExecuteScalar() != null)
                        countAnswers = (int)cmd.ExecuteScalar();
                    else
                        countAnswers = 0;
                }

                return countAnswers;
            }

            catch (Exception ex)
            {
                  throw ex;//Response.Redirect("~/NotFound.aspx", true);
                return 0;
            }
        }

        protected void OKButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Login.aspx");
        }
      
                  
    }
}