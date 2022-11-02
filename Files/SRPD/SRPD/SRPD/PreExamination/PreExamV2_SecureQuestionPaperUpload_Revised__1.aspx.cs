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

using PreExamClstLib.Classes;
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
    public partial class PreExamV2_SecureQuestionPaperUpload_Revised__1 : System.Web.UI.Page
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
            }
        }
        #endregion

        #region btnUpload_Click
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //int result = 0;
            //int resultmain = 0;
            SRVSecurePaper srvSecure = new SRVSecurePaper();
            string serverFileName = string.Empty;
            string UniID = clsGetSettings.UniversityID.Trim();
            btnUpload.Enabled = false;
            try
            {
                string fileName = hidCourseName.Value + "_" + hidCoursePartName.Value + "_" + hidCoursePartTermName.Value + "_" + UniID + hidFacID.Value + hidCrID.Value + hidMoLrnID.Value +
               hidPtrnID.Value + hidBrnID.Value + hidCrPrDetailsID.Value + hidCrPrChID.Value + hidExEvID.Value + hidPpPpHeadCrPrChID.Value + hidTchLrnMthID.Value + hidAssMthID.Value + hidAssTypeID.Value;

                fileName = RemoveSpecialCharacter(fileName).ToString();

                hidQPSName.Value = txtQPSName.Value;
                hidFilepath.Value = flUpload.Value.ToString();

                string uploadForlder = clsGetSettings.PhysicalSitePath + @"ExamDownloads\SRPD\Upload\RevisedPaper\";
                string downloadForlder = clsGetSettings.PhysicalSitePath + @"ExamDownloads\SRPD\Download\RevisedPaper\";

                if (flUpload.Value.EndsWith(".pdf"))
                {
                    serverFileName = Path.GetFileName(flUpload.PostedFile.FileName);

                    flUpload.PostedFile.SaveAs(uploadForlder + fileName + ".pdf");

                    clsUser user = (clsUser)Session["user"];
                    int res = 0;
                    string sKey = "MISSION20@)";
                    res = srvSecure.SaveRevisedUplodedDetails(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, hidPpPpHeadCrPrChID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value, hidRecordID.Value, hidQPSName.Value.ToString(), fileName, sKey, user.User_ID, serverFileName);

                    if (res > 0)
                    {
                        lblMesg.Text = "file uploaded successfully, Colleges will receive file and above message through E-Mail shortly";
                        lblMesg.CssClass = "saveNote";

                        //if (!Directory.Exists(downloadForlder))
                        //    Directory.CreateDirectory(downloadForlder);

                        DataTable dtVenue = new DataTable();
                        dtVenue = srvSecure.GetRevisedPpVenueDetails(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, hidPpPpHeadCrPrChID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);
                        dtVenue.Columns.Add("RandomWatermarkCode");

                        bool flag = false;

                        DataTable dtNotification = srvSecure.GetMailNotificationRevised();

                        string smsMsg = "Do not print or Distribute Paper Code: " + dtVenue.Rows[0]["PaperCode"].ToString() +
                                        "Scheduled at : " + dtVenue.Rows[0]["ExamDateTime"].ToString() + " today." +
                                        "You will get correct question paper file on your E-mail shortly.";

                        StringBuilder strQPForDonload = new StringBuilder();

                        //venue loop
                        for (int i = 0; i < dtVenue.Rows.Count; i++)
                        {
                            if (!Directory.Exists(downloadForlder + dtVenue.Rows[i]["VenueCode"].ToString()))
                                Directory.CreateDirectory(downloadForlder + dtVenue.Rows[i]["VenueCode"].ToString());

                            Random rndWM = new Random();
                            int venueRandomNo = rndWM.Next(11111, 99999);

                            dtVenue.Rows[i]["RandomWatermarkCode"] = venueRandomNo;

                            string fname = dtVenue.Rows[i]["pk_Inst_Id"].ToString() + "_" + fileName;

                            //Water mark with random code and save in download folder
                            flag = waterMarkPDF(uploadForlder + fileName + ".pdf",downloadForlder + dtVenue.Rows[i]["VenueCode"].ToString() +"/"+ fname + ".pdf", Classes.clsGetSettings.UniversityID, venueRandomNo.ToString());

                            if (flag)
                            {
                                //send sms
                                SendSMS(dtVenue.Rows[i]["Mobile_Number"].ToString(), dtVenue.Rows[i]["First_Name"].ToString(), smsMsg);

                                //send mail
                                SendMail(dtVenue.Rows[i]["Email_ID"].ToString(), txtQPMsg.Text, dtNotification, downloadForlder + dtVenue.Rows[i]["VenueCode"].ToString() + "/" + fname + ".pdf");

                                //add to xml
                                strQPForDonload.Append("<Root>");
                                strQPForDonload.Append("<PaperDetails pk_Inst_ID='" + dtVenue.Rows[i]["pk_inst_ID"].ToString() + "' ");
                                strQPForDonload.Append("pk_Uni_ID='" + dtVenue.Rows[0]["pk_Uni_ID"].ToString() + "' pk_Fac_ID='" + dtVenue.Rows[0]["pk_Fac_ID"].ToString() + "' pk_Cr_ID='" + dtVenue.Rows[0]["pk_Cr_ID"].ToString() + "' pk_MoLrn_ID='" + dtVenue.Rows[0]["pk_MoLrn_ID"].ToString() + "' ");
                                strQPForDonload.Append("pk_Ptrn_ID='" + dtVenue.Rows[0]["pk_Ptrn_ID"].ToString() + "' pk_Brn_ID='" + dtVenue.Rows[0]["pk_Brn_ID"].ToString() + "' pk_CrPr_Details_ID='" + dtVenue.Rows[0]["pk_CrPr_Details_ID"].ToString() + "' pk_CrPrCh_ID='" + dtVenue.Rows[0]["pk_CrPrCh_ID"].ToString() + "' ");
                                strQPForDonload.Append("pk_ExEv_ID='" + dtVenue.Rows[0]["pk_ExEv_ID"].ToString() + "' pk_PpPpHead_CrPrCh_ID='" + dtVenue.Rows[0]["pk_Pp_PpHead_CrPrCh_ID"].ToString() + "' pk_TchLrnMth_ID='" + dtVenue.Rows[0]["pk_TchLrnMth_ID"].ToString() + "' pk_AssMth_ID='" + dtVenue.Rows[0]["pk_AssMth_ID"].ToString() + "' ");
                                strQPForDonload.Append("pk_AssType_ID ='" + dtVenue.Rows[0]["pk_AssType_ID"].ToString() + "' FileName='" + fname + "' RandomWatermarkCode='" + dtVenue.Rows[0]["RandomWatermarkCode"].ToString());
                                strQPForDonload.Append("/>");
                                strQPForDonload.Append("</Root>");
                            }
                            else
                            {
                                lblMesg.Text = "unable to upload file";
                                lblMesg.CssClass = "errorNote";
                            }
                        }

                        //save download details to db
                        if (strQPForDonload.ToString().Length > 20)
                            srvSecure.SavePaperIncludedInDownaloddetails(strQPForDonload.ToString());

                    }
                    else
                    {
                        lblMesg.Text = "unable to upload file";
                        lblMesg.CssClass = "errorNote";
                    }
                }
                else
                {
                    lblMesg.Text = "Please uploaded pdf file only";
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

                if (gvCoursePart.DataKeys[e.Row.RowIndex]["Published"].ToString() == "0")
                {
                    ((LinkButton)e.Row.Cells[5].FindControl("lnkUpload")).Enabled = false;
                }

                if (gvCoursePart.DataKeys[e.Row.RowIndex]["IsSlotCreated"].ToString() == "0")
                    ((LinkButton)e.Row.Cells[5].FindControl("lnkUpload")).Enabled = false;

                if (gvCoursePart.DataKeys[e.Row.RowIndex]["RevisedFileUploadCnt"].ToString() == "0")
                    ((LinkButton)e.Row.Cells[5].FindControl("lnkUpload")).Enabled = true;
                else
                    ((LinkButton)e.Row.Cells[5].FindControl("lnkUpload")).Enabled = false;

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

                // ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "fnShowFileDialog();", true);

                lblPaperDetails.Text = gvCoursePart.DataKeys[id]["Course_Name"].ToString() + " - " + gvCoursePart.DataKeys[id]["Crpr_Abbr"].ToString()
                    + " - " + gvCoursePart.DataKeys[id]["CrPrCh_Abbr"].ToString() + " - " + gvCoursePart.DataKeys[id]["Paper"].ToString();

                lblPaperSchedule.Text = gvCoursePart.DataKeys[id]["ExamDateTime"].ToString();

                divListPaper.Style.Add("display", "none");
                divPaperUpload.Style.Add("display", "block");
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

            dt = srvSecure.ListAmAtWisePapersForRevision(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvCoursePart.DataSource = dt;
                gvCoursePart.DataBind();
            }
            else
            {
                gvCoursePart.DataSource = null;
                gvCoursePart.DataBind();

                lblErrMsg.Text = "No Record Found.";
                lblErrMsg.CssClass = "errorNote";
                divListPaper.Style.Add("dispaly", "none");
                divPaperUpload.Style.Add("dispaly", "none");
            }
        }
        #endregion

        #region FillGridPaperCodeWise
        public void FillGrid_PaperCodeWise()
        {
            SRVSecurePaper srvSecure = new SRVSecurePaper();
            DataTable dt = new DataTable();

            dt = srvSecure.ListAmAtWisePapersForRevision_PaperCodeWise(hidppcode.Value, hidExEvID.Value, hidTchLrnMthID.Value, hidAssMthID.Value, hidAssTypeID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                gvCoursePart.DataSource = dt;
                gvCoursePart.DataBind();
            }
            else
            {
                gvCoursePart.DataSource = null;
                gvCoursePart.DataBind();

                lblErrMsg.Text = "No Record Found.";
                lblErrMsg.CssClass = "errorNote";
                divListPaper.Style.Add("dispaly", "none");
                divPaperUpload.Style.Add("dispaly", "none");
            }
        }
        #endregion

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
            catch (Exception Ex)
            {
                WriteErrorLog(Ex.Message);
                return false;
            }
        }
        #endregion

        #region SendMail
        public void SendMail(string sEmail_ID, string notification, DataTable dtMailNotification, string downloadPath)
        {
            try
            {
                clsEmailClient oEmailClient = new clsEmailClient();
                string sNotificationMessage = notification;
                oEmailClient.From = dtMailNotification.Rows[0]["MailFrom"].ToString();
                oEmailClient.Subject = txtSubject.Text;//dtMailNotification.Rows[0]["MailSubject"].ToString();
                oEmailClient.Body = sNotificationMessage;
                oEmailClient.IsBodyHtml = true;
                oEmailClient.To = sEmail_ID;
                oEmailClient.SendWithAttachment(dtMailNotification.Rows[0]["SmtpHost"].ToString(), Convert.ToInt32(dtMailNotification.Rows[0]["SmtpPort"].ToString()), downloadPath);

            }
            catch (Exception Ex)
            { WriteErrorLog(Ex.Message); }

        }
        #endregion

        #region Send SMS function
        public string SendSMS(string sMobile_No, string Name, string sMsg)
        {
            string res = "-1";
            sMobile_No = "91" + sMobile_No.Trim();

            clsUser user = (clsUser)Session["user"];

            try
            {
                Sancharak.SendSMS obj = new Sancharak.SendSMS();
                obj.epMessage = "Dear " + Name + sMsg;
                obj.epUser = user.User_ID.ToString();
                res = obj.SendPersonalizedSMS(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"));
            }
            catch (Exception Ex)
            {
                WriteErrorLog(Ex.Message);
            }

            return res;
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