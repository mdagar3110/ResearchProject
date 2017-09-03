using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;

namespace Research_Project.DAL
{
    public class SaveData
    {
        public void ImageDetails(string[] ImageName, string I_name)
        {
            try
            {
                SaveDatainDb(ImageName,I_name);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SaveDatainDb(string[] ImageName, string I_name)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Default"].ConnectionString);
                SqlCommand com = new SqlCommand("insertImages", con);
                con.Open();
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add("@BlockNo", SqlDbType.Int).Value = ImageName[0];
                com.Parameters.Add("@Rotation", SqlDbType.Char).Value = ImageName[1].ToString();
                com.Parameters.Add("@Degree", SqlDbType.Int).Value = ImageName[2];
                com.Parameters.Add("@IsMirrorImage", SqlDbType.Char).Value = (ImageName[3].Split('.'))[0].ToString();
                com.Parameters.Add("@ImageName", SqlDbType.NVarChar).Value = I_name;
                com.Parameters.Add("@CreatedDate", SqlDbType.NVarChar).Value = DateTime.Now.ToString();
                com.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = 0;

                com.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}