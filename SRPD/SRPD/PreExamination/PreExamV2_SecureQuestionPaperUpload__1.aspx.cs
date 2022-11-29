using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Resources;
using System.Threading;

using PreExamClstLib.Services;
using Classes;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing;
using System.Drawing.Imaging;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaperUpload__1 : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetVariables();


                if (hidflag.Value == "1")
                {
                    FillGrid();
                }
                else if (hidflag.Value == "2")
                {
                    FillGrid_PaperCodeWise();
                }
                else
                {
                    FillGrid_DateWise();
                }





            }
        }
        #endregion


        public int CheckPDF(string VerificationFolder, string fileName, string sKey, string serverFileName)
        {
            //string VerificationFolder = clsGetSettings.PhysicalSitePath + @"ExamDownloads\SRPD\Upload\CorruptFileFolder\";

            //string strFolderPath = Server.MapPath("..\\PreExamination\\TempDirectory\\");

            int IsFileCorroupt = 1;

            bool breturn = DecryptFileCheckPDF(VerificationFolder + @"\Encrypt\" + fileName + ".pdf", VerificationFolder + @"\Decrypt\" + fileName + ".pdf", sKey);

            if (breturn == true)
            {
                string strTempFolderPath = Server.MapPath("..\\PreExamination\\TempDirectory\\");

                if (md5Check(strTempFolderPath + serverFileName, VerificationFolder + @"\Decrypt\" + fileName + ".pdf"))
                {
                    breturn = waterMarkPDF(VerificationFolder + @"\Decrypt\" + fileName + ".pdf", VerificationFolder + @"\Watermark\" + fileName + ".pdf", "test", "1212");


                    if (breturn == true)
                    {
                        IsFileCorroupt = 1;
                    }
                    else
                    {
                        IsFileCorroupt = 2;
                    }
                }
                else
                {
                    IsFileCorroupt = 0;
                }
                return IsFileCorroupt;
            }
            else
            {
                return IsFileCorroupt = 0;
            }


        }


        private bool md5Check(string source, string dest)
        {
            return md5Checksum(source).Equals(md5Checksum(dest)) ? true : false;
        }


        private string md5Checksum(string s)
        {
            if (!File.Exists(s))
                return "";

            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(s))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", String.Empty);
                }
            }
        }

        //public bool waterMarkPDF(string sInputFilename, string sOutputFilename)
        //{
        //    ////Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();

        //    ////// load the source PDF document
        //    //////PdfFileInfo fileInfo = new PdfFileInfo()  (@"D:\PDF_OpenPassword.pdf");
        //    //////// determine that source PDF file is Encrypted with password
        //    //////bool encrypted = fileInfo. .IsEncrypted;

        //    //////PdfDocument doc = new PdfDocument(sOutputFilename);
        //    //////doc.Security.Permissions.ToString();


        //    //////PdfFileInfo fileInfo = new PdfFileInfo(@"D:\PDF_OpenPassword.pdf");
        //    //////// determine that source PDF file is Encrypted with password
        //    //////bool encrypted = fileInfo.IsEncrypted;

        //    //////doc.LoadFromFile(sOutputFilename);
        //    //////string isEncrypted = doc.Security.Permissions.ToString();


        //    ////doc.LoadFromFile(sOutputFilename);



        //    ////foreach (PdfPageBase page in doc.Pages)
        //    ////{
        //    ////    try
        //    ////    {
        //    ////        Spire.Pdf.Graphics.PdfTilingBrush brush
        //    ////           = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
        //    ////        brush.Graphics.SetTransparency(0.6f);
        //    ////        brush.Graphics.Save();
        //    ////        brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
        //    ////        brush.Graphics.RotateTransform(-45);
        //    ////        brush.Graphics.DrawString("6666",
        //    ////            new Spire.Pdf.Graphics.PdfFont(PdfFontFamily.Helvetica, 100), PdfBrushes.Violet, 0, 0, new PdfStringFormat(PdfTextAlignment.Center));
        //    ////        brush.Graphics.Restore();
        //    ////        brush.Graphics.SetTransparency(1);
        //    ////        page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page.Canvas.ClientSize));
        //    ////    }
        //    ////    catch (NullReferenceException e)
        //    ////    {
        //    ////        doc.SaveToFile(sOutputFilename);
        //    ////        return false;
        //    ////    }
        //    ////}

        //    ////doc.SaveToFile(sOutputFilename);
        //    ////return true;

        //        #region Decryption of question paper file
        //        //if (File.Exists(fileUploadedPath + file.Name))
        //        if (File.Exists(sInputFilename))
        //        {
        //            //Directory.CreateDirectory(decryptedFilePath);
        //            //DecryptFile(fileUploadedPath + file.Name, decryptedFilePath + file.Name, "Password");
        //            //PdfWaterMark(decryptedFilePath , file.Name);
        //            //Bitmap Venue = CreateQRCode(")venue code 001)");
        //            //WriteImageToPDF(Venue,sInputFilename, sOutputFilename);
        //            //PdfWaterMark(Venue, sOutputFilename, String.Empty);
        //            waterMarkPDF(sInputFilename, sOutputFilename, "test", "12121")
        //        }
        //        #endregion
        //        return true;
        //}


        //public void WriteImageToPDF(Bitmap img, string sInputFilename, string sOutputFilename)
        //{
        //    Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
        //    doc.LoadFromFile(sInputFilename);

        //    PdfImage image = PdfImage.FromImage(img);

        //    System.Drawing.Image img1 = img;//Image.FromFile(@"D:\WebApplication2\WebApplication2\images\Purushottam Kelkar.png");


        //    foreach (PdfPageBase page in doc.Pages)
        //    {              
        //        page.BackgroundImage = img1;
        //        page.BackgroundRegion = new RectangleF(5, 5, 80, 81);

        //    }

        //    doc.SaveToFile(sOutputFilename);


        //    Spire.Pdf.PdfDocument doc1 = new Spire.Pdf.PdfDocument();
        //    doc1.LoadFromFile(sOutputFilename);
        //    System.Drawing.Image img2 = img;

        //    foreach (PdfPageBase page in doc1.Pages)
        //    {
        //        //float x = (page.Canvas.ClientSize.Width - width) / 2;
        //        page.BackgroundImage = img1;
        //        page.BackgroundRegion = new RectangleF(page.Canvas.ClientSize.Width - 90, page.Canvas.ClientSize.Height - 90, 80, 81);
        //    }

        //    doc1.SaveToFile(sOutputFilename);

        //}


        //public Bitmap CreateQRCode(string venueCode)
        //{
        //    // string venueCode = "sdfsf";
        //    string utiltyEncrypt = encrypt(venueCode, "1234567891234567");
        //    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
        //    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
        //    try
        //    {
        //        qrCodeEncoder.QRCodeScale = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("Invalid size!");
        //    }
        //    try
        //    {
        //        qrCodeEncoder.QRCodeVersion = 7;
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
        //    System.Drawing.Bitmap image;
        //    image = qrCodeEncoder.Encode(utiltyEncrypt);

        //    return image;         

        //}

        public static string encrypt(string inputString, string key)
        {
            byte[] array = generateSalt(16);
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            Aes aes = Aes.Create();
            aes.Key = getUTF8Bytes(key);
            aes.IV = array;
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform cryptoTransform = aes.CreateEncryptor();
            byte[] input = cryptoTransform.TransformFinalBlock(bytes, 0, bytes.Length);
            byte[] inArray = addSalt(input, array);
            return Convert.ToBase64String(inArray);
        }

        private static byte[] getUTF8Bytes(string inputString)
        {
            byte[] array = new byte[inputString.Length];
            return Encoding.UTF8.GetBytes(inputString);
        }
        private static byte[] generateSalt(int saltLength)
        {
            byte[] array = new byte[saltLength];
            Random random = new Random();
            random.NextBytes(array);
            return array;
        }
        private static byte[] addSalt(byte[] input, byte[] salt)
        {
            byte[] array = new byte[input.Length + salt.Length];
            for (int i = 0; i < salt.Length; i++)
            {
                array[i] = salt[i];
            }
            for (int i = salt.Length; i < input.Length + salt.Length; i++)
            {
                array[i] = input[i - salt.Length];
            }
            return array;
        }



        #region Water mark PDF
        public bool waterMarkPDF(string sInputFilename, string sOutputFilename, string UniName, string VenueCode)
        {
            try
            {
                Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();

                doc.LoadFromFile(sInputFilename);

                string displayName = string.Empty;

                if (VenueCode != "")
                    displayName = UniName + "-" + VenueCode;
                else
                    displayName = UniName;

                foreach (PdfPageBase page in doc.Pages)
                {
                    PdfTilingBrush brush
                       = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
                    brush.Graphics.SetTransparency(0.3f);
                    brush.Graphics.Save();
                    brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
                    brush.Graphics.RotateTransform(-45);
                    brush.Graphics.DrawString(displayName,
                        new PdfFont(PdfFontFamily.Helvetica, 25), PdfBrushes.Violet, 0, 0,
                        new PdfStringFormat(PdfTextAlignment.Center));
                    brush.Graphics.Restore();
                    brush.Graphics.SetTransparency(1);
                    page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page.Canvas.ClientSize));
                }

                doc.SaveToFile(sOutputFilename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion


        //public bool PdfWaterMark(Bitmap img, string decryptedFilePath, string fileName)
        //{

        //    Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
        //    doc.LoadFromFile(decryptedFilePath + fileName);

        //    PdfImage image = PdfImage.FromImage(img);

        //    Random rnd = new Random();
        //    int venueRandomNo = rnd.Next(11111, 99999);

        //    foreach (PdfPageBase page in doc.Pages)
        //    {
        //        PdfTilingBrush brush
        //           = new PdfTilingBrush(new SizeF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height / 3));
        //        brush.Graphics.SetTransparency(0.3f);
        //        brush.Graphics.Save();
        //        brush.Graphics.TranslateTransform(brush.Size.Width / 2, brush.Size.Height / 2);
        //        brush.Graphics.RotateTransform(-45);
        //        //brush.Graphics.DrawImage(image, new PointF(page.Canvas.ClientSize.Width / 2, page.Canvas.ClientSize.Height - 200));

        //        brush.Graphics.DrawString(venueRandomNo.ToString(),new Spire.Pdf.Graphics.PdfFont(PdfFontFamily.Helvetica, 24), PdfBrushes.Violet, 0, 0,
        //            new PdfStringFormat(PdfTextAlignment.Center));


        //        brush.Graphics.Restore();
        //        brush.Graphics.SetTransparency(1);
        //        page.Canvas.DrawRectangle(brush, new RectangleF(new PointF(0, 0), page.Canvas.ClientSize));
        //    }

        //    doc.SaveToFile(decryptedFilePath + fileName);

        //    return true;
        //}

        #region Decrypt File

        public static bool DecryptFileCheckPDF(string sInputFilename, string sOutputFilename, string sKey)
        {
            try
            {
                //DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                ////A 64 bit key and IV is required for this provider.
                ////Set secret key For DES algorithm.

                //Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(sKey, Encoding.UTF8.GetBytes(sKey));
                //DES.Key = deriveBytes.GetBytes(8);
                //DES.Mode = CipherMode.ECB;
                //DES.Padding = PaddingMode.PKCS7;

                //FileStream fsread = new FileStream(sInputFilename,FileMode.Open,FileAccess.Read);

                //ICryptoTransform desdecrypt = DES.CreateDecryptor();

                //FileStream fsout = new FileStream(sOutputFilename, FileMode.OpenOrCreate, FileAccess.Write);

                //CryptoStream cryptostreamDecr = new CryptoStream(fsout,desdecrypt,CryptoStreamMode.Write);

                //byte[] inbuffer = new byte[fsread.Length];
                //fsread.Read(inbuffer, 0, (int)fsread.Length);

                //cryptostreamDecr.Write(inbuffer, 0, (int)fsread.Length);

                //cryptostreamDecr.Close();
                //cryptostreamDecr.Dispose();

                //fsread.Close();
                //fsread.Dispose();

                //fsout.Close();
                //fsout.Dispose();



                //PdfReader reader = new PdfReader(sOutputFilename);
                //Document inputDoc = new Document(reader.GetPageSizeWithRotation(1));
                //int n = reader.NumberOfPages;

                //int i = 1;
                //while (i <= n)
                //{
                //    //Test the page by getting the rotation
                //    int rotation = reader.GetPageRotation(i);
                //    ++i;
                //}
                //reader.Close();


                //return true;

                //DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                ////A 64 bit key and IV is required for this provider.
                ////Set secret key For DES algorithm.

                //Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(sKey, Encoding.UTF8.GetBytes(sKey));
                //DES.Key = deriveBytes.GetBytes(8);
                //DES.Mode = CipherMode.ECB;
                //DES.Padding = PaddingMode.PKCS7;

                //FileStream fsread = new FileStream(sInputFilename,FileMode.Open,FileAccess.Read);

                //ICryptoTransform desdecrypt = DES.CreateDecryptor();

                //FileStream fsout = new FileStream(sOutputFilename, FileMode.OpenOrCreate, FileAccess.Write);

                //CryptoStream cryptostreamDecr = new CryptoStream(fsout,desdecrypt,CryptoStreamMode.Write);

                //byte[] inbuffer = new byte[fsread.Length];
                //fsread.Read(inbuffer, 0, (int)fsread.Length);

                //cryptostreamDecr.Write(inbuffer, 0, (int)fsread.Length);

                //cryptostreamDecr.Close();
                //cryptostreamDecr.Dispose();

                //fsread.Close();
                //fsread.Dispose();

                //fsout.Close();
                //fsout.Dispose();

                //try
                //{
                //    PdfReader reader = new PdfReader(sOutputFilename);
                //    Document inputDoc = new Document(reader.GetPageSizeWithRotation(1));
                //    int n = reader.NumberOfPages;

                //    int i = 1;
                //    while (i <= n)
                //    {
                //        //Test the page by getting the rotation
                //        int rotation = reader.GetPageRotation(i);
                //        ++i;
                //    }
                //    reader.Close();
                //}
                //catch (BadPdfFormatException)
                //{
                //    return false;
                //}


                //bool passwordProtected = PdfDocument.IsPasswordProtected(@"Sample Data\" + fileName);
                //if (passwordProtected)


                string EncryptionKey = "MISSION20@)";
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (FileStream fsInput = new FileStream(sInputFilename, FileMode.Open))
                    {
                        using (CryptoStream cs = new CryptoStream(fsInput, encryptor.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (FileStream fsOutput = new FileStream(sOutputFilename, FileMode.Create))
                            {
                                int data;
                                while ((data = cs.ReadByte()) != -1)
                                {
                                    fsOutput.WriteByte((byte)data);
                                }

                                fsOutput.Close();
                            }
                            cs.Close();
                        }

                        fsInput.Close();
                        return true;
                    }
                }



            }
            catch (Exception ex)
            {
                return false;
            }


        }

        #endregion

        #region btnUpload_Click
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            int result = 0;
            int resultmain = 0;
            SRVSecurePaper srvSecure = new SRVSecurePaper();
            string serverFileName = string.Empty;
            string inputFilePath = txtQPSName.Value.ToString();
            string UniID = clsGetSettings.UniversityID.Trim();

            try
            {
                int seq = srvSecure.GetMaxSeqNo(hidRecordID.Value);
                hidMaxSeqNo.Value = seq.ToString();

                //add hidExEvID.Value in FileName by Jatin 18-Feb-2019
                string fileName = hidCourseName.Value + "_" + hidCoursePartName.Value + "_" + hidCoursePartTermName.Value + "_" + UniID + hidFacID.Value + hidCrID.Value + hidMoLrnID.Value +
               hidPtrnID.Value + hidBrnID.Value + hidCrPrDetailsID.Value + hidCrPrChID.Value + hidExEvID.Value + hidPpPpHeadCrPrChID.Value + hidTchLrnMthID.Value + hidAssMthID.Value + hidAssTypeID.Value + "_" + hidMaxSeqNo.Value;

                fileName = RemoveSpecialCharacter(fileName).ToString();

                string sKey = "MISSION20@)";

                hidQPSName.Value = txtQPSName.Value;
                hidFilepath.Value = flUpload.Value.ToString();

                // Encrypt the file using symmectric algorithm 
                string strFolderPath = Server.MapPath("..\\PreExamination\\TempDirectory\\");

                string searchForlder = clsGetSettings.PhysicalSitePath + @"ExamDownloads\SRPD\Upload";

                string Verificationfolder = clsGetSettings.PhysicalSitePath + @"PreExamination\TempDirectory\Verificationfolder";

                string VerificationfolderEncrypt = clsGetSettings.PhysicalSitePath + @"PreExamination\TempDirectory\Verificationfolder\Encrypt";

                string VerificationfolderDecrypt = clsGetSettings.PhysicalSitePath + @"PreExamination\TempDirectory\Verificationfolder\Decrypt";

                string VerificationfolderWatermark = clsGetSettings.PhysicalSitePath + @"PreExamination\TempDirectory\Verificationfolder\Watermark";


                if (flUpload.Value.EndsWith(".pdf"))
                {
                    serverFileName = Path.GetFileName(flUpload.PostedFile.FileName);

                    flUpload.PostedFile.SaveAs(strFolderPath + serverFileName);

                    if (Directory.Exists(searchForlder))
                    {


                        result = EncryptFileCheckPDF(strFolderPath + serverFileName, VerificationfolderEncrypt + @"\" + fileName + ".pdf", sKey);

                        //result = EncryptFile(strFolderPath + serverFileName, VerificationfolderEncrypt + @"\" + fileName + ".pdf", sKey);

                        if (result == 1)
                        {
                            string fileUploadedPath = searchForlder + @"\" + fileName + ".pdf";// serverFileName;
                            //string decryptedFilePath = "";
                            result = CheckPDF(Verificationfolder, fileName, sKey, serverFileName);
                        }
                    }
                    else
                    {
                        lblMesg.Text = "file not found";
                        lblMesg.CssClass = "errorNote";
                    }

                    if (result != 1)
                    {
                        if (Directory.Exists(strFolderPath))
                        {
                            FileInfo fil = new FileInfo(strFolderPath + serverFileName);

                            if (fil.Exists)
                            {
                                File.Delete(strFolderPath + serverFileName);
                            }

                            fil = new FileInfo(VerificationfolderEncrypt + @"\" + fileName + ".pdf");
                            if (fil.Exists)
                            {
                                File.Delete(VerificationfolderEncrypt + @"\" + fileName + ".pdf");
                            }

                            fil = new FileInfo(VerificationfolderEncrypt + @"\" + fileName + ".pdf");
                            if (fil.Exists)
                            {
                                File.Delete(VerificationfolderDecrypt + @"\" + fileName + ".pdf");
                            }
                        }
                    }

                }
                if (result == 1)
                {

                    resultmain = EncryptFile(strFolderPath + serverFileName, searchForlder + @"\" + fileName + ".pdf", sKey);
                    if (resultmain == 1)
                    {
                        if (Directory.Exists(strFolderPath))
                        {
                            FileInfo fil = new FileInfo(strFolderPath + serverFileName);

                            if (fil.Exists)
                            {
                                File.Delete(strFolderPath + serverFileName);
                            }

                            fil = new FileInfo(VerificationfolderEncrypt + @"\" + fileName + ".pdf");
                            if (fil.Exists)
                            {
                                File.Delete(VerificationfolderEncrypt + @"\" + fileName + ".pdf");
                            }

                            fil = new FileInfo(VerificationfolderDecrypt + @"\" + fileName + ".pdf");
                            if (fil.Exists)
                            {
                                File.Delete(VerificationfolderDecrypt + @"\" + fileName + ".pdf");
                            }

                            fil = new FileInfo(VerificationfolderWatermark + @"\" + fileName + ".pdf");
                            if (fil.Exists)
                            {
                                File.Delete(VerificationfolderWatermark + @"\" + fileName + ".pdf");
                            }
                        }

                        serverFileName = serverFileName.Replace(".pdf", "");
                        clsUser user = (clsUser)Session["user"];

                        int res = 0;

                        if (hidRdbFlag.Value == "1")
                        {
                            res = srvSecure.SaveUplodedDetails(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, hidPpPpHeadCrPrChID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value, hidRecordID.Value, hidQPSName.Value.ToString(), fileName, sKey, user.User_ID, serverFileName);
                            FillGrid();
                        }
                        else if (hidRdbFlag.Value == "2")
                        {
                            res = srvSecure.SaveUplodedDetails(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, hidPpPpHeadCrPrChID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value, hidRecordID.Value, hidQPSName.Value.ToString(), fileName, sKey, user.User_ID, serverFileName);
                            FillGrid_PaperCodeWise();
                        }

                        else if (hidRdbFlag.Value == "3")
                        {
                            res = srvSecure.SaveUplodedDetails(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, hidPpPpHeadCrPrChID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value, hidRecordID.Value, hidQPSName.Value.ToString(), fileName, sKey, user.User_ID, serverFileName);
                            FillGrid_DateWise();
                        }

                        lblMesg.Text = "file uploaded successfully";
                        lblMesg.CssClass = "saveNote";
                    }
                    else
                    {
                        lblMesg.Text = "Unable to water mark file.The file might be corrupt.Please create new file and re-upload.";
                        lblMesg.CssClass = "errorNote";
                    }

                }
                else if (result == 2)
                {

                    FileInfo fil = new FileInfo(searchForlder + serverFileName);

                    if (fil.Exists)
                    {
                        File.Delete(searchForlder + serverFileName);
                    }

                    lblMesg.Text = "Unable to water mark file.The file might be corrupt.Please create new file and re-upload.";
                    lblMesg.CssClass = "errorNote";
                }
                else
                {

                    FileInfo fil = new FileInfo(searchForlder + serverFileName);

                    if (fil.Exists)
                    {
                        File.Delete(searchForlder + serverFileName);
                    }

                    lblMesg.Text = "Unable to upload file.The file might be corrupt.Please create new file and re-upload.";
                    lblMesg.CssClass = "errorNote";
                }


                txtQPSName.Value = "";



            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region RemoveSpecialCharacter

        StringBuilder RemoveSpecialCharacter(string sFileName)
        {
            char[] inValidChars = { '~', '`', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '+', '=', '{', '}', '[', ']', '|', ':', ';', '<', '>', ',', '?', '.', '"', '/', '\\', '\'' };
            foreach (char c in inValidChars)
            {
                sFileName = sFileName.Replace(c, '_');
            }

            ////
            ////Replace spaces with '_' character.
            ////            
            sFileName = sFileName.Replace(' ', '_');

            return RemoveMultipleUnderscore(sFileName);
        }

        #endregion

        #region Remove multiple '_'

        StringBuilder RemoveMultipleUnderscore(string sFileName)
        {
            Regex reg = new Regex("(_)+");
            if (reg.IsMatch(sFileName))
            {
                sFileName = reg.Replace(sFileName, "_");
            }

            StringBuilder sb = new StringBuilder(sFileName);
            return sb;
        }

        #endregion


        #region gvCoursePart_RowDataBound

        protected void gvCoursePart_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((LinkButton)e.Row.Cells[6].FindControl("lnkDelete")).Enabled = false;

                if (gvCoursePart.DataKeys[e.Row.RowIndex]["Published"].ToString() == "0")
                {
                    ((LinkButton)e.Row.Cells[5].FindControl("lnkUpload")).Enabled = false;
                }

                if (Convert.ToInt32(gvCoursePart.DataKeys[e.Row.RowIndex]["fileUploadedCount"].ToString()) > 0)
                {
                    DateTime date = Convert.ToDateTime(gvCoursePart.DataKeys[e.Row.RowIndex]["ExamDate"].ToString());
                  
                    if (date >= DateTime.Today && date.Hour < 7)
                        ((LinkButton)e.Row.Cells[6].FindControl("lnkDelete")).Enabled = true;

                }

                //((LinkButton)e.Row.Cells[5].FindControl("lnkUpload")).Attributes.Add("onclick", "fnShowFileDialog();");
                //((LinkButton)e.Row.Cells[4].FindControl("lnkViewVenue")).Attributes.Add("onclick", "fnShowFileDialog1();");
            }
        }

        #endregion

        #region gvCoursePart_RowCommand
        protected void gvCoursePart_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SRVSecurePaper srv = new SRVSecurePaper();
            if (e.CommandName == "View")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                string PpPpHeadCrPrChID = gvCoursePart.DataKeys[id]["PpPpHeadCrPrChID"].ToString();

                hidPpPpHeadCrPrChID.Value = PpPpHeadCrPrChID;
                hidRecordID.Value = gvCoursePart.DataKeys[id]["fk_Record_ID"].ToString();


                DataTable dt = null;

                if (hidRdbFlag.Value == "1")
                {
                    dt = srv.ListPapersWiseVenue(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, PpPpHeadCrPrChID, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);
                }
                else if (hidRdbFlag.Value == "2")
                {
                    hidFacID.Value = gvCoursePart.DataKeys[id]["pk_Fac_ID"].ToString();
                    hidCrID.Value = gvCoursePart.DataKeys[id]["pk_Cr_ID"].ToString();
                    hidMoLrnID.Value = gvCoursePart.DataKeys[id]["pk_MoLrn_ID"].ToString();
                    hidPtrnID.Value = gvCoursePart.DataKeys[id]["pk_Ptrn_ID"].ToString();
                    hidBrnID.Value = gvCoursePart.DataKeys[id]["pk_Brn_ID"].ToString();
                    hidCrPrDetailsID.Value = gvCoursePart.DataKeys[id]["pk_CrPr_Details_ID"].ToString();
                    hidCrPrChID.Value = gvCoursePart.DataKeys[id]["pk_CrPrCh_ID"].ToString();
                    hidCourseName.Value = gvCoursePart.DataKeys[id]["Base_Course"].ToString();
                    hidCoursePartName.Value = gvCoursePart.DataKeys[id]["Crpr_Abbr"].ToString();
                    hidCoursePartTermName.Value = gvCoursePart.DataKeys[id]["CrPrCh_Abbr"].ToString();
                    //dt = srv.ListPaperCodeWiseVenue(hidppcode.Value, hidExEvID.Value, PpPpHeadCrPrChID, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);
                    dt = srv.ListPapersWiseVenue(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, PpPpHeadCrPrChID, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);
                }
                else if (hidRdbFlag.Value == "3")
                {
                    hidFacID.Value = gvCoursePart.DataKeys[id]["pk_Fac_ID"].ToString();
                    hidCrID.Value = gvCoursePart.DataKeys[id]["pk_Cr_ID"].ToString();
                    hidMoLrnID.Value = gvCoursePart.DataKeys[id]["pk_MoLrn_ID"].ToString();
                    hidPtrnID.Value = gvCoursePart.DataKeys[id]["pk_Ptrn_ID"].ToString();
                    hidBrnID.Value = gvCoursePart.DataKeys[id]["pk_Brn_ID"].ToString();
                    hidCrPrDetailsID.Value = gvCoursePart.DataKeys[id]["pk_CrPr_Details_ID"].ToString();
                    hidCrPrChID.Value = gvCoursePart.DataKeys[id]["pk_CrPrCh_ID"].ToString();
                    hidCourseName.Value = gvCoursePart.DataKeys[id]["Base_Course"].ToString();
                    hidCoursePartName.Value = gvCoursePart.DataKeys[id]["Crpr_Abbr"].ToString();
                    hidCoursePartTermName.Value = gvCoursePart.DataKeys[id]["CrPrCh_Abbr"].ToString();
                    // dt = srv.ListPaperDateWiseVenue(hiddate.Value, hidExEvID.Value, PpPpHeadCrPrChID, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);
                    dt = srv.ListPapersWiseVenue(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, PpPpHeadCrPrChID, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);
                }




                if (dt != null && dt.Rows.Count > 0)
                {
                    grvVenueList.DataSource = dt;
                    grvVenueList.DataBind();
                }
                else
                {
                    grvVenueList.DataSource = null;
                    grvVenueList.DataBind();
                    lblVenueMsg.Text = "No data found.";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "fnShowFileDialog1();", true);
            }

            else if (e.CommandName == "Upload")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                hidPpPpHeadCrPrChID.Value = gvCoursePart.DataKeys[id]["PpPpHeadCrPrChID"].ToString();
                string PpPpHeadCrPrChID = gvCoursePart.DataKeys[id]["PpPpHeadCrPrChID"].ToString();
                hidRecordID.Value = gvCoursePart.DataKeys[id]["fk_Record_ID"].ToString();


                if (hidRdbFlag.Value == "2")
                {
                    hidFacID.Value = gvCoursePart.DataKeys[id]["pk_Fac_ID"].ToString();
                    hidCrID.Value = gvCoursePart.DataKeys[id]["pk_Cr_ID"].ToString();
                    hidMoLrnID.Value = gvCoursePart.DataKeys[id]["pk_MoLrn_ID"].ToString();
                    hidPtrnID.Value = gvCoursePart.DataKeys[id]["pk_Ptrn_ID"].ToString();
                    hidBrnID.Value = gvCoursePart.DataKeys[id]["pk_Brn_ID"].ToString();
                    hidCrPrDetailsID.Value = gvCoursePart.DataKeys[id]["pk_CrPr_Details_ID"].ToString();
                    hidCrPrChID.Value = gvCoursePart.DataKeys[id]["pk_CrPrCh_ID"].ToString();
                    hidCourseName.Value = gvCoursePart.DataKeys[id]["Course_Name"].ToString();
                    hidCoursePartName.Value = gvCoursePart.DataKeys[id]["Crpr_Abbr"].ToString();
                    hidCoursePartTermName.Value = gvCoursePart.DataKeys[id]["CrPrCh_Abbr"].ToString();

                }
                else if (hidRdbFlag.Value == "3")
                {
                    hidFacID.Value = gvCoursePart.DataKeys[id]["pk_Fac_ID"].ToString();
                    hidCrID.Value = gvCoursePart.DataKeys[id]["pk_Cr_ID"].ToString();
                    hidMoLrnID.Value = gvCoursePart.DataKeys[id]["pk_MoLrn_ID"].ToString();
                    hidPtrnID.Value = gvCoursePart.DataKeys[id]["pk_Ptrn_ID"].ToString();
                    hidBrnID.Value = gvCoursePart.DataKeys[id]["pk_Brn_ID"].ToString();
                    hidCrPrDetailsID.Value = gvCoursePart.DataKeys[id]["pk_CrPr_Details_ID"].ToString();
                    hidCrPrChID.Value = gvCoursePart.DataKeys[id]["pk_CrPrCh_ID"].ToString();
                    hidCourseName.Value = gvCoursePart.DataKeys[id]["Course_Name"].ToString();
                    hidCoursePartName.Value = gvCoursePart.DataKeys[id]["Crpr_Abbr"].ToString();
                    hidCoursePartTermName.Value = gvCoursePart.DataKeys[id]["CrPrCh_Abbr"].ToString();

                }





                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "fnShowFileDialog();", true);
            }
            else if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                hidRecordID.Value = gvCoursePart.DataKeys[id]["fk_Record_ID"].ToString();


                clsUser user = (clsUser)Session["user"];

                int res = srv.DeleteUploadedPaper(hidRecordID.Value, user.User_ID);

                if (res > 0)
                {
                    lblMesg.Text = "Paper Deleted successfully";
                    lblMesg.CssClass = "saveNote";

                    if (hidflag.Value == "1")
                    {
                        FillGrid();
                    }
                    else if (hidflag.Value == "2")
                    {
                        FillGrid_PaperCodeWise();
                    }
                    else
                    {
                        FillGrid_DateWise();
                    }

                }



            }
        }
        #endregion

        #region  SetVariables

        void SetVariables()
        {
            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");

            hidRdbFlag.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidRdbFlag")).Value;
            hiddate.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hiddate")).Value;
            hidppcode.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidppcode")).Value;
            hidflag.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidflag")).Value;

            hidFacID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidFacID")).Value;
            hidCrID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidCrID")).Value;
            hidMoLrnID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidMoLrnID")).Value;
            hidPtrnID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidPtrnID")).Value;
            hidExEvID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExEvID")).Value;
            hidBrnID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidBrnID")).Value;
            hidCrPrDetailsID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidCrPrDetailsID")).Value;
            hidCrPrChID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidCrPrChID")).Value;

            hidCourseName.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidCourseName")).Value;
            hidCoursePartName.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidCoursePartName")).Value;
            hidCoursePartTermName.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidCoursePartTermName")).Value;

            hidPageDescription.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidPageDescription")).Value;

            hidAssMthAssTypeName.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidAssMthAssTypeName")).Value;
            hidExamEvent.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamEvent")).Value;
            hidTchLrnMthID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidTchLrnMthID")).Value;
            hidAssMthID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidAssMthID")).Value;
            hidAssTypeID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidAssTypeID")).Value;
            hidIsEventOpen.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidIsEventOpen")).Value;
            //Setting page heading
            lblSubHeader.Text = hidPageDescription.Value + " - " + hidAssMthAssTypeName.Value;
        }

        #endregion

        #region FillGrid
        public void FillGrid()
        {
            SRVSecurePaper srvSecure = new SRVSecurePaper();
            DataTable dt = new DataTable();

            dt = srvSecure.ListPapersWithStudentCountAssMthAssTypeWise(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvCoursePart.DataSource = dt;
                gvCoursePart.DataBind();
            }
            else
            {
                gvCoursePart.DataSource = null;
                gvCoursePart.DataBind();
            }
        }
        #endregion

        #region FillGridPaperCodeWise
        public void FillGrid_PaperCodeWise()
        {
            SRVSecurePaper srvSecure = new SRVSecurePaper();
            DataTable dt = new DataTable();

            dt = srvSecure.ListPapersWithStudentCountAssMthAssTypeWise_PaperCodeWise(hidppcode.Value, hidExEvID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvCoursePart.DataSource = dt;
                gvCoursePart.DataBind();
            }
            else
            {
                gvCoursePart.DataSource = null;
                gvCoursePart.DataBind();
            }
        }
        #endregion

        #region FillGrid_DateWise
        public void FillGrid_DateWise()
        {
            SRVSecurePaper srvSecure = new SRVSecurePaper();
            DataTable dt = new DataTable();

            dt = srvSecure.ListPapersWithStudentCountAssMthAssTypeWise_DateWise(hiddate.Value, hidExEvID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvCoursePart.DataSource = dt;
                gvCoursePart.DataBind();
            }
            else
            {
                gvCoursePart.DataSource = null;
                gvCoursePart.DataBind();
            }
        }
        #endregion

        #region Symmetric Encryption

        public static RijndaelManaged Rm;

        // Function to Generate a 64 bits Key.
        public static string GenerateKey()
        {
            // Create an instance of Symetric Algorithm. Key and IV is generated automatically.
            Rm = (RijndaelManaged)RijndaelManaged.Create();

            // Use the Automatically generated key for Encryption. 
            return UnicodeEncoding.ASCII.GetString(Rm.Key);
        }

        public int EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {
            try
            {
                int res = 0;

                //FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
                //FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.OpenOrCreate, FileAccess.Write);
                //DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                //Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(sKey, Encoding.UTF8.GetBytes(sKey));

                //DES.Key = deriveBytes.GetBytes(8);

                //DES.Mode = CipherMode.ECB;
                //DES.Padding = PaddingMode.PKCS7;

                //ICryptoTransform desencrypt = DES.CreateEncryptor();
                //CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

                //byte[] bytearrayinput = new byte[fsInput.Length];
                //fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                //cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                //cryptostream.Close();
                //fsInput.Close();
                //fsEncrypted.Close();
                //res = 1;
                //return res;


                string EncryptionKey = "MISSION20@)";
                using (Aes encryptor = Aes.Create())
                {
                    //encryptor.Padding = PaddingMode.None;
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (FileStream fsOutput = new FileStream(sOutputFilename, FileMode.Create))
                    {
                        using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (FileStream fsInput = new FileStream(sInputFilename, FileMode.Open))
                            {
                                int data;
                                while ((data = fsInput.ReadByte()) != -1)
                                {
                                    cs.WriteByte((byte)data);
                                }
                                fsInput.Close();
                            }

                            cs.Close();
                        }
                        fsOutput.Close();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }



        public int EncryptFileCheckPDF(string sInputFilename, string sOutputFilename, string sKey)
        {
            try
            {
                //int res = 0;
                ////Rm = new RijndaelManaged();

                ////string cryptFile = sOutputFilename;
                ////FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                ////Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(sKey, Encoding.UTF8.GetBytes(sKey));
                ////Rm.Key = deriveBytes.GetBytes(16);
                ////Rm.Mode = CipherMode.ECB;
                ////Rm.Padding = PaddingMode.PKCS7;

                ////byte[] key = deriveBytes.GetBytes(16);

                ////CryptoStream cs = new CryptoStream(fsCrypt, Rm.CreateEncryptor(key, key), CryptoStreamMode.Write);

                ////FileStream fsIn = new FileStream(sInputFilename, FileMode.OpenOrCreate);

                ////int data;
                ////while ((data = fsIn.ReadByte()) != -1)
                ////    cs.WriteByte((byte)data);

                ////fsIn.Close();
                ////cs.Close();
                ////fsCrypt.Close();

                //FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);

                //FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.OpenOrCreate, FileAccess.Write);

                //DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                //Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(sKey, Encoding.UTF8.GetBytes(sKey));

                //DES.Key = deriveBytes.GetBytes(8);

                //DES.Mode = CipherMode.ECB;
                //DES.Padding = PaddingMode.PKCS7;

                //ICryptoTransform desencrypt = DES.CreateEncryptor();
                //CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

                //byte[] bytearrayinput = new byte[fsInput.Length];
                //fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                //cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                //cryptostream.Close();
                //fsInput.Close();
                //fsEncrypted.Close();
                //res = 1;
                //return res;

                //int res = 0;

                //FileStream fsInput = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read);
                //FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.OpenOrCreate, FileAccess.Write);
                //DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                //Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(sKey, Encoding.UTF8.GetBytes(sKey));

                //DES.Key = deriveBytes.GetBytes(8);

                //DES.Mode = CipherMode.ECB;
                //DES.Padding = PaddingMode.PKCS7;

                //ICryptoTransform desencrypt = DES.CreateEncryptor();
                //CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write);

                //byte[] bytearrayinput = new byte[fsInput.Length];
                //fsInput.Read(bytearrayinput, 0, bytearrayinput.Length);
                //cryptostream.Write(bytearrayinput, 0, bytearrayinput.Length);
                //cryptostream.Close();
                //fsInput.Close();
                //fsEncrypted.Close();
                //res = 1;
                //return res;



                int res = 0;

                //string EncryptionKey = "MAKV2SPBNI99212";
                string EncryptionKey = "MISSION20@)";
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (FileStream fsOutput = new FileStream(sOutputFilename, FileMode.Create))
                    {
                        using (CryptoStream cs = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (FileStream fsInput = new FileStream(sInputFilename, FileMode.Open))
                            {
                                int data;
                                while ((data = fsInput.ReadByte()) != -1)
                                {
                                    cs.WriteByte((byte)data);
                                }
                                fsInput.Close();
                            }

                            cs.Close();
                        }
                        fsOutput.Close();
                    }

                    res = 1;
                    return res;
                }
            }
            catch (Exception ex)
            {
                return 0;
                throw (ex);
            }
        }

        #endregion

        #region WriteErrorLog
        public void WriteErrorLog(string message)
        {
            string sPath = clsGetSettings.PhysicalSitePath + "\\PreExamination\\Tempdirectory\\";
            System.IO.StreamWriter sw = System.IO.File.AppendText(sPath + "test.log");
            sw.WriteLine(message);
            sw.Flush();
            sw.Close();
            sw = null;
        }
        #endregion

    }
}