using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml;


/// <summary>
/// Summary description for Common
/// </summary>
public class Common
{
    //public Common()
    //{
        
    //}

    public static String GetIPAddress()
    {
        String ip = "";
        try
        {
            //ip = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            ip = "::1";
            if (!string.IsNullOrEmpty(ip))
            {
                string[] ipRange = ip.Split(',');
                int le = ipRange.Length - 1;
                string trueIP = ipRange[le];
            }
            else
            {
                ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
        }
        catch
        {
            ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        }
        return ip;
    }

    public static String SiteUrl
    {
        get
        {
            return WebConfigurationManager.ConnectionStrings["SiteUrl"].ConnectionString;
        }
    }

    public static DataTable YearList()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("yearId");
        dt.Columns.Add("YearName");
        for (int i = 2016; i <= DateTime.Now.Year; i++)
        {
            DataRow dr = dt.NewRow();
            dr["yearId"] = i;
            dr["YearName"] = i;
            dt.Rows.Add(dr);
        }
        return dt;
    }

    public string EncodeTo64(string toEncode)
    {
        try
        {
            byte[] toEncodeAsBytes = System.Text.Encoding.Unicode.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }
        catch (Exception ex) { return ex.Message; }
    }

    public string DecodeFrom64(string encodedData)
    {
        try
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.Encoding.Unicode.GetString(encodedDataAsBytes);
            return returnValue;
        }
        catch (Exception ex) { return ex.Message; }
    }

    public string FillCapctha()
    {
        try
        {

            Random random = new Random();

            string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            StringBuilder captcha = new StringBuilder();

            for (int i = 0; i < 6; i++)
            {
                captcha.Append(combination[random.Next(combination.Length)]);
            }

            return captcha.ToString().ToUpper();

            //imgCaptcha.ImageUrl = "GenerateCaptcha.aspx?" + DateTime.Now.Ticks.ToString();

        }
        catch (Exception ex)
        {
            return "";
        }
    }

    public string ValidateImagePDF_FileExtWithSize(HttpPostedFileBase Uploadedfile, int ImageSize)
    {
        string massage;
        String fn = Path.GetFileNameWithoutExtension(Uploadedfile.FileName);
        String ext = Path.GetExtension(Uploadedfile.FileName);
        char[] SpecialChars = "!@#$%^&*()+=~`\\|/?><,\"".ToCharArray();
        int indexOf = fn.IndexOfAny(SpecialChars);
        String fileName = fn;
        int count = fileName.Split('.').Length - 1;
        if (count > 1)
        {
            massage = "Double extension not allowed in File name";
        }
        else
        {
            if (indexOf != -1)
            {
                massage = "Special character not allowed in File name/फ़ाइल के नाम में विशेष वर्ण नहीं होने चाहिए।’";

            }
            else
            {
                string mimetype = Uploadedfile.ContentType;
                if ((ext.ToLower() == ".jpg" || ext.ToLower() == ".jpeg") && (mimetype == "image/jpeg" || mimetype == "image/jpg"))
                {
                    fn = "";

                    string fFullName = Uploadedfile.FileName;
                    int len = fFullName.Length;
                    string ext1 = Path.GetExtension(fFullName);
                    string str = fFullName.Substring(fFullName.LastIndexOf("\\") + 1);
                    len = str.Length;
                    string fileN = str.Substring(0, len - ext1.Length);

                    Regex FilenameRegex = null;
                    FilenameRegex = new Regex("(.*?)\\.(jpeg|jpg|JPEG|JPG)$", RegexOptions.IgnoreCase);
                    int index = fileN.IndexOf(".");

                    if (!FilenameRegex.IsMatch(fFullName) || index != -1)
                    {
                        massage = "Please upload .jpg, .jpeg or .pdf File only";
                    }
                    else
                    {
                        string Photoname = Path.GetFileNameWithoutExtension(Uploadedfile.FileName);
                        string fileSize = Uploadedfile.ContentLength.ToString();
                        String ImageFileNameMBA = Uploadedfile.FileName;

                        Byte[] stu_imageMBA = new Byte[Uploadedfile.ContentLength];

                        Stream fs = Uploadedfile.InputStream;
                        fs.Read(stu_imageMBA, 0, Convert.ToInt32(fileSize));

                        fs.Seek(0, SeekOrigin.Begin);
                        StreamReader sr = new StreamReader(fs, true);

                        string firstLine = sr.ReadLine().ToString();

                        //string firstLine = "JFIF";

                        if ((firstLine.IndexOf("JFIF") > -1) || (firstLine.IndexOf("Exif") > -1))
                        {

                            if (Uploadedfile.ContentLength <= 1024 * ImageSize)
                            {
                                massage = "Valid";
                            }
                            else
                            {
                                massage = "File size can not exceed " + ImageSize + " KB ";
                            }
                        }
                        else
                        {
                            massage = "Please upload .jpg, .jpeg or .pdf File only";
                        }
                    }
                }
                else if (ext.ToLower() == ".pdf" && mimetype == "application/pdf")
                {
                    fn = "";

                    string fFullName = Uploadedfile.FileName;
                    int len = fFullName.Length;
                    string ext1 = Path.GetExtension(fFullName);
                    string str = fFullName.Substring(fFullName.LastIndexOf("\\") + 1);
                    len = str.Length;
                    string fileN = str.Substring(0, len - ext1.Length);

                    Regex FilenameRegex = null;
                    //FilenameRegex = new Regex("(.*?)\\.(jpeg|jpg|JPEG|JPG)$", RegexOptions.IgnoreCase);
                    FilenameRegex = new Regex("(.*?)\\.(pdf|PDF)$", RegexOptions.IgnoreCase);
                    int index = fileN.IndexOf(".");

                    if (!FilenameRegex.IsMatch(fFullName) || index != -1)
                    {
                        massage = "Please upload .jpg, .jpeg or .pdf File only";
                    }
                    else
                    {
                        string Photoname = Path.GetFileNameWithoutExtension(Uploadedfile.FileName);
                        string fileSize = Uploadedfile.ContentLength.ToString();
                        String ImageFileNameMBA = Uploadedfile.FileName;

                        Byte[] stu_imageMBA = new Byte[Uploadedfile.ContentLength];

                        Stream fs = Uploadedfile.InputStream;
                        fs.Read(stu_imageMBA, 0, Convert.ToInt32(fileSize));

                        fs.Seek(0, SeekOrigin.Begin);
                        StreamReader sr = new StreamReader(fs, true);

                        string firstLine = sr.ReadLine().ToString();

                        //string firstLine = "JFIF";

                        if ((firstLine.IndexOf("%PDF") > -1))
                        {

                            if (Uploadedfile.ContentLength <= 1024 * ImageSize)
                            {
                                massage = "Valid";
                            }
                            else
                            {
                                massage = "File size can not exceed " + ImageSize + " KB ";
                            }
                        }
                        else
                        {
                            massage = "Please upload .jpg, .jpeg or .pdf File only";
                        }
                    }
                }


                else
                {
                    massage = "Please upload .jpg, .jpeg or .pdf File only";
                }
            }
        }
        return massage;
    }

    #region  convert in Hashcode
    public string SingleHashing(string value)
    {      
        string hashCode = "";
        hashCode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(value, "MD5");
        return hashCode;
    }

    public string doubleHashing(string value, string seed)
    {
        string newvalue = value + seed;
        string hashCode = "";
        hashCode = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(newvalue, "MD5");
        return hashCode;
    }


    #endregion

    /// <summary>
    /// Get Calc Parameters
    /// </summary>
    /// <param name="procId">procId=1 for Select feeder type </param>
    /// <param name="KeyId"></param>
    /// <param name="subKeyId"></param>
    /// <returns></returns>
    

    //for Get MimeType of uploaded document 
    public string GetMimeType(string extension)
    {
        if (extension == null)
            throw new ArgumentNullException("extension");

        if (extension.StartsWith("."))
        {
            extension = extension.Substring(1);
        }
        switch (extension.ToLower())
        {
            case "jpeg": return "image/jpeg";
            case "jpg": return "image/jpeg";
            case "png": return "image/png";
            case "pdf": return "application/pdf";
            default: return "application/octet-stream";
        }
    }

    public string GetUniqueFileName(string filename)
    {
        try
        {
            string[] ext = new string[5];
            ext = filename.Split('.');
            return Guid.NewGuid().ToString() + "." + ext[ext.Length - 1];
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string getImagePath()
    {
        return WebConfigurationManager.AppSettings["FileUpload"];
    }

}
public class RandomPassword
{
    // Define default min and max password lengths.
    private static int DEFAULT_MIN_PASSWORD_LENGTH = 8;
    private static int DEFAULT_MAX_PASSWORD_LENGTH = 10;

    // Define supported password characters divided into groups.
    // You can add (or remove) characters to (from) these groups.
    private static string PASSWORD_CHARS_LCASE = "abcdefgijkmnopqrstwxyz";
    private static string PASSWORD_CHARS_UCASE = "ABCDEFGHJKLMNPQRSTWXYZ";
    private static string PASSWORD_CHARS_NUMERIC = "23456789";
    private static string PASSWORD_CHARS_SPECIAL = "*";

    /// <summary>
    /// Generates a random password.
    /// </summary>
    /// <returns>
    /// Randomly generated password.
    /// </returns>
    /// <remarks>
    /// The length of the generated password will be determined at
    /// random. It will be no shorter than the minimum default and
    /// no longer than maximum default.
    /// </remarks>
    public static string Generate()
    {
        return Generate(DEFAULT_MIN_PASSWORD_LENGTH,
                        DEFAULT_MAX_PASSWORD_LENGTH);
    }

    /// <summary>
    /// Generates a random password of the exact length.
    /// </summary>
    /// <param name="length">
    /// Exact password length.
    /// </param>
    /// <returns>
    /// Randomly generated password.
    /// </returns>
    public static string Generate(int length)
    {
        return Generate(length, length);
    }

    /// <summary>
    /// Generates a random password.
    /// </summary>
    /// <param name="minLength">
    /// Minimum password length.
    /// </param>
    /// <param name="maxLength">
    /// Maximum password length.
    /// </param>
    /// <returns>
    /// Randomly generated password.
    /// </returns>
    /// <remarks>
    /// The length of the generated password will be determined at
    /// random and it will fall with the range determined by the
    /// function parameters.
    /// </remarks>
    public static string Generate(int minLength,
                                  int maxLength)
    {
        // Make sure that input parameters are valid.
        if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
            return null;

        // Create a local array containing supported password characters
        // grouped by types. You can remove character groups from this
        // array, but doing so will weaken the password strength.
        char[][] charGroups = new char[][] 
        {
            PASSWORD_CHARS_LCASE.ToCharArray(),
            PASSWORD_CHARS_UCASE.ToCharArray(),
            PASSWORD_CHARS_NUMERIC.ToCharArray(),
            PASSWORD_CHARS_SPECIAL.ToCharArray()
        };

        // Use this array to track the number of unused characters in each
        // character group.
        int[] charsLeftInGroup = new int[charGroups.Length];

        // Initially, all characters in each group are not used.
        for (int i = 0; i < charsLeftInGroup.Length; i++)
            charsLeftInGroup[i] = charGroups[i].Length;

        // Use this array to track (iterate through) unused character groups.
        int[] leftGroupsOrder = new int[charGroups.Length];

        // Initially, all character groups are not used.
        for (int i = 0; i < leftGroupsOrder.Length; i++)
            leftGroupsOrder[i] = i;

        // Because we cannot use the default randomizer, which is based on the
        // current time (it will produce the same "random" number within a
        // second), we will use a random number generator to seed the
        // randomizer.

        // Use a 4-byte array to fill it with random bytes and convert it then
        // to an integer value.
        byte[] randomBytes = new byte[4];

        // Generate 4 random bytes.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        rng.GetBytes(randomBytes);

        // Convert 4 bytes into a 32-bit integer value.
        int seed = BitConverter.ToInt32(randomBytes, 0);

        // Now, this is real randomization.
        Random random = new Random(seed);

        // This array will hold password characters.
        char[] password = null;

        // Allocate appropriate memory for the password.
        if (minLength < maxLength)
            password = new char[random.Next(minLength, maxLength + 1)];
        else
            password = new char[minLength];

        // Index of the next character to be added to password.
        int nextCharIdx;

        // Index of the next character group to be processed.
        int nextGroupIdx;

        // Index which will be used to track not processed character groups.
        int nextLeftGroupsOrderIdx;

        // Index of the last non-processed character in a group.
        int lastCharIdx;

        // Index of the last non-processed group.
        int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;

        // Generate password characters one at a time.
        for (int i = 0; i < password.Length; i++)
        {
            // If only one character group remained unprocessed, process it;
            // otherwise, pick a random character group from the unprocessed
            // group list. To allow a special character to appear in the
            // first position, increment the second parameter of the Next
            // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
            if (lastLeftGroupsOrderIdx == 0)
                nextLeftGroupsOrderIdx = 0;
            else
                nextLeftGroupsOrderIdx = random.Next(0,
                                                     lastLeftGroupsOrderIdx);

            // Get the actual index of the character group, from which we will
            // pick the next character.
            nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

            // Get the index of the last unprocessed characters in this group.
            lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;

            // If only one unprocessed character is left, pick it; otherwise,
            // get a random character from the unused character list.
            if (lastCharIdx == 0)
                nextCharIdx = 0;
            else
                nextCharIdx = random.Next(0, lastCharIdx + 1);

            // Add this character to the password.
            password[i] = charGroups[nextGroupIdx][nextCharIdx];

            // If we processed the last character in this group, start over.
            if (lastCharIdx == 0)
                charsLeftInGroup[nextGroupIdx] =
                                          charGroups[nextGroupIdx].Length;
            // There are more unprocessed characters left.
            else
            {
                // Swap processed character with the last unprocessed character
                // so that we don't pick it until we process all characters in
                // this group.
                if (lastCharIdx != nextCharIdx)
                {
                    char temp = charGroups[nextGroupIdx][lastCharIdx];
                    charGroups[nextGroupIdx][lastCharIdx] =
                                charGroups[nextGroupIdx][nextCharIdx];
                    charGroups[nextGroupIdx][nextCharIdx] = temp;
                }
                // Decrement the number of unprocessed characters in
                // this group.
                charsLeftInGroup[nextGroupIdx]--;
            }

            // If we processed the last group, start all over.
            if (lastLeftGroupsOrderIdx == 0)
                lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
            // There are more unprocessed groups left.
            else
            {
                // Swap processed group with the last unprocessed group
                // so that we don't pick it until we process all groups.
                if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                {
                    int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                    leftGroupsOrder[lastLeftGroupsOrderIdx] =
                                leftGroupsOrder[nextLeftGroupsOrderIdx];
                    leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                }
                // Decrement the number of unprocessed groups.
                lastLeftGroupsOrderIdx--;
            }
        }

        // Convert password characters into a string and return the result.
        return new string(password);
    }


}
#region Class For ServiceModel
public class ServiceModel
{
    public Int64 sendResponseId { get; set; }
    public string requestKey { get; set; }
    public string deptRegistrationId { get; set; }
    public string serviceResponse { get; set; }
    public string applicationNumber { get; set; }
    public string serviceCode { get; set; }
    public string applicationType { get; set; }
    public string transIp { get; set; }
}
#endregion