using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.IO;
using Research_Project.DAL;
namespace Research_Project.BAL
{
    public class Common
    {
        string[] ImageDetails;
        public bool SaveFile(ArrayList FileNames)
        {
            try
            {
                SeperateFiledetails(FileNames);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
           
        }

        private string[] SeperateFiledetails(ArrayList ImgName)
        {
            try
            {
                foreach (string f_name in ImgName)
                {

                    ImageDetails = f_name.Split('_');
                    SaveData objData = new SaveData();
                    objData.ImageDetails(ImageDetails, f_name);
                }

                return ImageDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
            }


    }
}