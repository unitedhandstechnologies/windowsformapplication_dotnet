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

using PreExamClstLib.Services;
using Classes;
using System.IO;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Data.Sql;
using System.Data.SqlClient;
using Sancharak;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaperDownload__2 : System.Web.UI.Page
    {
        #region Variables
        DataTable newDt = null;
        static DataTable dt = null;
        DataView dv = null;
        string[] retrunKeys = new string[1];
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SetHiddenVariables();
                lnkBtnExpand.Attributes.Add("onclick", "return fnExpandAll('" + gvPapers.ClientID + "');");
                lnkBtnCollapse.Attributes.Add("onclick", "return fnCollapseAll('" + gvPapers.ClientID + "');");
                FillGrid();
            }
        }
        #endregion

        #region Configure Data Grid View Related Events

        #region gvPapers_RowDataBound

        protected void gvPapers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                GridView gvChild = (GridView)e.Row.FindControl("dgchild");
                gvChild.RowCommand += new GridViewCommandEventHandler(gvChild_RowCommand);

                HtmlImage img = (HtmlImage)e.Row.FindControl("iplus");
                img.Src = clsGetSettings.SitePath + @"\Images\minus.gif";

                SRVSecurePaper srv = new SRVSecurePaper();

                try
                {
                    DataTable dtChild = new DataTable("dtChild");
                    DataRow[] drArr = null;
                    //Fill Child gridview Exam slotwise
                    drArr = dt.Select("ExamDateTime ='" + gvPapers.DataKeys[e.Row.RowIndex]["ExamDateTime"].ToString() + "'");

                    if (drArr.Length > 0)
                    {

                        dtChild.Columns.Add("pk_Uni_ID");
                        dtChild.Columns.Add("pk_Fac_ID");
                        dtChild.Columns.Add("pk_Cr_ID");
                        dtChild.Columns.Add("pk_MoLrn_ID");
                        dtChild.Columns.Add("pk_Ptrn_ID");
                        dtChild.Columns.Add("pk_Brn_ID");
                        dtChild.Columns.Add("pk_CrPr_Details_ID");
                        dtChild.Columns.Add("pk_CrPrCh_ID");
                        dtChild.Columns.Add("pk_ExEv_ID");
                        dtChild.Columns.Add("pk_Pp_PpHead_CrPrCh_ID");
                        dtChild.Columns.Add("pk_TchLrnMth_ID");
                        dtChild.Columns.Add("pk_AssMth_ID");
                        dtChild.Columns.Add("pk_AssType_ID");
                        dtChild.Columns.Add("pk_Inst_ID");
                        dtChild.Columns.Add("FileName");
                        dtChild.Columns.Add("Paper");
                        dtChild.Columns.Add("ExamDateTime");
                        dtChild.Columns.Add("ExamStartTime");
                        dtChild.Columns.Add("ExamDate");
                        dtChild.Columns.Add("DownloadStatus");

                        foreach (DataRow row in drArr)
                        {
                            dtChild.ImportRow(row);
                        }
                        dtChild.AcceptChanges();

                        if (dtChild.Rows.Count > 0)
                        {
                            gvChild.DataSource = dtChild;
                            gvChild.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region gvPapers_RowCommand
        protected void gvPapers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            SRVSecurePaper srv = new SRVSecurePaper();
            GridView gvChild = (GridView)gvPapers.Rows[id].FindControl("dgchild");
            HtmlTable tblOtp = (HtmlTable)gvPapers.Rows[id].FindControl("tblOTP");
            HtmlInputHidden hidOTP = (HtmlInputHidden)gvPapers.Rows[id].FindControl("hidOTP");
            HtmlTable tblVerifyOtp = (HtmlTable)gvPapers.Rows[id].FindControl("tblVerifyOTP");
            HtmlTable tblDownloadMsg = (HtmlTable)gvPapers.Rows[id].FindControl("tblDownloadMsg");
            HtmlTable tblDownload = (HtmlTable)gvChild.FooterRow.FindControl("tblDownload");
            HtmlTable tblSupervisor = (HtmlTable)gvPapers.Rows[id].FindControl("tblSupervisor");
            DropDownList ddlSupervisor = (DropDownList)tblSupervisor.FindControl("ddlSupervisor");

            DataTable dtSupervisor = srv.GetSupervisorDetails(hidEventID.Value, hidVenueID.Value);

            HtmlInputHidden hidSupervisorID = (HtmlInputHidden)tblSupervisor.FindControl("hidSupervisorID");

            if (ddlSupervisor.Items.Count == 1)
            {
                ddlSupervisor.Items.Clear();

                ListItem item1 = new ListItem();
                item1.Text = "--Select--";
                item1.Value = "-1";
                ddlSupervisor.Items.Add(item1);

                if (dtSupervisor != null && dtSupervisor.Rows.Count > 0)
                {

                    for (int i = 0; i < dtSupervisor.Rows.Count; i++)
                    {
                        ListItem item = new ListItem();
                        item.Text = dtSupervisor.Rows[i]["Name"].ToString();
                        item.Value = dtSupervisor.Rows[i]["Pk_Supervisor_ID"].ToString();
                        ddlSupervisor.Items.Add(item);
                    }

                    ddlSupervisor.SelectedValue = hidSupervisorID.Value;
                }
            }

            DataRow[] dr = dtSupervisor.Select("Pk_Supervisor_ID = " + hidSupervisorID.Value);

            if (dr == null || dr.Length == 0)
            {
                tblOtp.Style.Add("display", "none");
                tblVerifyOtp.Style.Add("display", "none");
                tblDownload.Style.Add("display", "none");
                tblDownloadMsg.Style.Add("display", "block");
                Label lbl = (Label)tblDownloadMsg.FindControl("lblDownloadMsg");
                lbl.Text = "No Supervisor data found.";
                lbl.CssClass = "errorNote";
                return;
            }


            if (e.CommandName == "GetOTP") //Get One time password
            {
                if (hidSupervisorID.Value == "-1" || hidSupervisorID.Value == "0")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Please select Supervisor to send Password')", true);
                    return;
                }

                retrunKeys = srv.GetOTP(); // retrieve One time password
                hidOTP.Value = retrunKeys[0].ToString(); // set One time password to hidden variable

                string msg = ",Your Download Password is ";
                string res = "";

                if (dr != null && dr.Length > 0)
                {
                    //send one time password to supervisor
                    //res = SendSMS(dtSupervisor.Rows[0]["Mobile_Number"].ToString(),
                    //        dtSupervisor.Rows[0]["First_Name"].ToString(),
                    //        hidOTP.Value, msg);

                    res = SendSMS(dr[0]["Mobile_Number"].ToString(),
                           dr[0]["First_Name"].ToString(),
                           hidOTP.Value, msg);

                    tblOtp.Style.Add("display", "none");
                    tblVerifyOtp.Style.Add("display", "block");
                    tblDownloadMsg.Style.Add("display", "none");

                    Label lbl = (Label)tblVerifyOtp.FindControl("lblVerifyMsg");
                    lbl.Text = "Dowload Password sent to " + dr[0]["Name"].ToString();
                    lbl.Font.Bold = true;

                    Button btn = (Button)tblVerifyOtp.FindControl("btnSubmit");
                    TextBox txt = (TextBox)tblVerifyOtp.FindControl("txtPassword");

                    ddlSupervisor.Enabled = false;

                    btn.Attributes.Add("onclick", "return validate(" + btn.ClientID + "," + txt.ClientID + "," + hidOTP.Value + ");");
                }
            }

            if (e.CommandName == "VerifyOTP") //verify one time password
            {
                Label lbl = (Label)tblDownloadMsg.FindControl("lblDownloadMsg");
                TextBox txt = (TextBox)tblVerifyOtp.FindControl("txtPassword");

                if (hidOTP.Value == txt.Text)
                {
                    //send zip file password to supervisor after verification
                    string zipFilePassword = gvPapers.DataKeys[id]["ZipFilePwd"].ToString();
                    string msg = ",Your password for zip file is ";
                    //string res = SendSMS(dtSupervisor.Rows[0]["Mobile_Number"].ToString(),
                    //           dtSupervisor.Rows[0]["First_Name"].ToString(),
                    //           gvPapers.DataKeys[id]["ZipFilePwd"].ToString(), msg);


                    string res = SendSMS(dr[0]["Mobile_Number"].ToString(),
                              dr[0]["First_Name"].ToString(),
                              gvPapers.DataKeys[id]["ZipFilePwd"].ToString(), msg);

                    lbl.Text = "File opening password is sent to " + dr[0]["Name"].ToString(); ;
                    lbl.Font.Bold = true;
                    tblDownloadMsg.Style.Add("display", "block");

                    tblDownload.Style.Add("display", "block");
                    tblOtp.Style.Add("display", "none");
                    tblVerifyOtp.Style.Add("display", "none");
                }
            }
        }
        #endregion

        protected void gvPapers_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (this.Request.Params.ToString().Contains("btnGetOTP"))
                {
                    return;
                }

                SRVSecurePaper srv = new SRVSecurePaper();

                hidEventID.Value = gvPapers.DataKeys[e.Row.RowIndex]["pk_ExEv_ID"].ToString();
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSupervisor");
                DataTable dtSupervisor = srv.GetSupervisorDetails(hidEventID.Value, hidVenueID.Value);

                //HtmlTable tblOtp = (HtmlTable)gvPapers.Rows[e.Row.RowIndex].FindControl("tblOTP");
                Button btn = (Button)e.Row.FindControl("btnGetOTP");
                HtmlInputHidden hidValue = (HtmlInputHidden)e.Row.FindControl("hidSupervisorID");
                HtmlInputHidden hidText = (HtmlInputHidden)e.Row.FindControl("hidSupervisorName");


                if (dtSupervisor != null && dtSupervisor.Rows.Count > 0)
                {
                    //ddl.Items.Clear();

                    //ListItem item1 = new ListItem();
                    //item1.Text = "--Select--";
                    //item1.Value = "-1";
                    //ddl.Items.Add(item1); 

                    //for (int i = 0; i < dtSupervisor.Rows.Count; i++)
                    //{
                    //    ListItem item = new ListItem();
                    //    item.Text = dtSupervisor.Rows[i]["Name"].ToString();
                    //    item.Value = dtSupervisor.Rows[i]["Pk_Supervisor_ID"].ToString();

                    //    ddl.Items.Add(item); 
                    //}

                    ddl.Items.Clear();
                    ddl.Items.Add(new ListItem("--- Select ---", "-1"));

                    foreach (DataRow row in dtSupervisor.Rows)
                    {
                        ddl.Items.Add(new ListItem(row["Name"].ToString(), row["Pk_Supervisor_ID"].ToString()));
                    }

                    //btn.Attributes.Add("onclick", "setIDs('" + ddl.UniqueID + "','" + hidValue.UniqueID + "','" + hidText.UniqueID + "');");

                    btn.OnClientClick = "setIDs('" + ddl.UniqueID + "','" + hidValue.UniqueID + "','" + hidText.UniqueID + "');";
                }
            }
        }


        #region gvChild_RowDataBound
        protected void gvChild_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gv = (GridView)sender;
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                Label lblStatus = (Label)e.Row.FindControl("lblDownloadStatus");

                if (gv.DataKeys[e.Row.RowIndex]["DownloadStatus"].ToString() == "1")
                {
                    lblStatus.Text = "Downloaded";
                }
                else
                {
                    lblStatus.Text = "Not Downloaded";
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                HtmlTable tblDownload = (HtmlTable)e.Row.FindControl("tblDownload");
                tblDownload.Style.Add("display", "none");


                HtmlInputHidden hidExamStartTime = (HtmlInputHidden)gv.Parent.FindControl("hidExamStartTime");
                HtmlInputHidden hidExamDate = (HtmlInputHidden)gv.Parent.FindControl("hidExamDate");
                HtmlInputHidden hidExamEndTime = (HtmlInputHidden)gv.Parent.FindControl("hidExamEndTime");

                DateTime ExamDateTime = DateTime.Parse(hidExamDate.Value);

                Button btn = (Button)e.Row.FindControl("btnDownload");
                Button btnGetOTP = (Button)gv.Parent.FindControl("btnGetOTP");
                //check exam data with current date
                if (ExamDateTime.Date == DateTime.Now.Date)
                {
                    if (hidExamStartTime.Value.EndsWith(":AM") || hidExamStartTime.Value.EndsWith(":PM"))
                    {
                        hidExamStartTime.Value = hidExamStartTime.Value.Remove(hidExamStartTime.Value.Length - 3);
                        hidExamStartTime.Value = hidExamStartTime.Value.Trim();
                    }

                    if (hidExamStartTime.Value.Length > 5)
                    {
                        hidExamStartTime.Value = hidExamStartTime.Value.Remove(hidExamStartTime.Value.Length - 3);
                    }

                    DateTime dateStartDate = DateTime.ParseExact(hidExamStartTime.Value, "HH:mm", new DateTimeFormatInfo());

                    if (hidExamEndTime.Value.EndsWith(":AM") || hidExamEndTime.Value.EndsWith(":PM"))
                    {
                        hidExamEndTime.Value = hidExamEndTime.Value.Remove(hidExamEndTime.Value.Length - 3);
                        hidExamEndTime.Value = hidExamEndTime.Value.Trim();
                    }

                    if (hidExamEndTime.Value.Length > 5)
                    {
                        hidExamEndTime.Value = hidExamEndTime.Value.Remove(hidExamEndTime.Value.Length - 3);
                    }

                    DateTime dateEndDate = DateTime.ParseExact(hidExamEndTime.Value, "HH:mm", new DateTimeFormatInfo());

                    // string strTime = DateTime.Now.Hour.ToString()+ ":" + DateTime.Now.Minute.ToString();
                    DateTime dateCurrentDate = DateTime.ParseExact(DateTime.Now.ToString("HH:mm"), "HH:mm", new DateTimeFormatInfo());

                    // DUConfigurations.clsSRPDDownloadTime time = DUConfigurations.clsDUConfigurations.Instance.DownloadTime; 
                    DUConfigurations.clsSRPDDownloadTime time = DUConfigurations.clsDUConfigurations.Instance.SRPDownloadTime;

                    int timeSpan = time.DownloadTime;

                    timeSpan = timeSpan * 60;

                    string startHour = dateStartDate.ToString("HH");
                    string Startmin = dateStartDate.ToString("mm");
                    string currentHour = dateCurrentDate.ToString("HH");
                    string currentMin = dateCurrentDate.ToString("mm");
                    string endHour = dateEndDate.ToString("HH");
                    string endMin = dateEndDate.ToString("mm");

                    string timeToValidate = timeSpan.ToString();

                    int StartTime = (Convert.ToInt32(startHour) * 60) + Convert.ToInt32(Startmin);
                    int CurrentTime = (Convert.ToInt32(currentHour) * 60) + Convert.ToInt32(currentMin);
                    int endTime = (Convert.ToInt32(endHour) * 60) + Convert.ToInt32(endMin);




                    //Allow download before exam start time for specified hours and upto endtime
                    //if ((StartTime - CurrentTime) <= Convert.ToInt32(timeToValidate))
                    //{
                    //    btn.Enabled = true;
                    //    btnGetOTP.Enabled = true;
                    //}
                    //else
                    //{
                    //    btn.Enabled = false;
                    //    btnGetOTP.Enabled = false;
                    //}


                    //if (endTime < CurrentTime)
                    //{
                    //    btn.Enabled = false;
                    //    btnGetOTP.Enabled = false;
                    //}


                    //TimeSpan tSpan = new TimeSpan(timeSpan, 0, 0);

                    //TimeSpan ts = dt1.Subtract(dt2);

                    //if (ts <= tSpan)
                    //{
                    //    btn.Enabled = true;
                    //}
                    //else
                    //{
                    //    btn.Enabled = false;
                    //}

                    //int DownlodTime = Convert.ToInt32(Convert.ToString(examStartTime.TimeOfDay.Hours.ToString()) + Convert.ToString(examStartTime.TimeOfDay.Minutes.ToString()));

                    //int linkAvailableTime = Convert.ToInt32(Convert.ToString(examStartTime.TimeOfDay.Hours.ToString()) + Convert.ToString(examStartTime.TimeOfDay.Minutes.ToString()));


                }
                else
                {
                    //btn.Enabled = false;
                    //btnGetOTP.Enabled = false;
                }

            }
        }

        #endregion

        #region gvChild_RowCommand
        public void gvChild_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(e.CommandArgument);
                GridView gv = (GridView)sender;
                clsUser user = (clsUser)Session["user"];
                StringBuilder strCourse = new StringBuilder();
                XmlDocument xml = new XmlDocument();
                XmlNode root = xml.CreateNode(XmlNodeType.Element, "Root", "");
                XmlNode childNode = null;
                string xmldata = "";
                xmldata = "<Root>";
                if (e.CommandName == "Download")
                {
                    #region Save download status
                    for (int i = 0; i < gv.Rows.Count; i++)
                    {
                        childNode = xml.CreateNode(XmlNodeType.Element, "Papers", "");
                        childNode.AppendChild(xml.CreateElement("pk_Fac_ID")).InnerText = gv.DataKeys[i]["pk_Fac_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_Cr_ID")).InnerText = gv.DataKeys[i]["pk_Cr_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_MoLrn_ID")).InnerText = gv.DataKeys[i]["pk_MoLrn_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_Ptrn_ID")).InnerText = gv.DataKeys[i]["pk_Ptrn_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_Brn_ID")).InnerText = gv.DataKeys[i]["pk_Brn_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_CrPr_Details_ID")).InnerText = gv.DataKeys[i]["pk_CrPr_Details_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_CrPrCh_ID")).InnerText = gv.DataKeys[i]["pk_CrPrCh_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_ExEv_ID")).InnerText = gv.DataKeys[i]["pk_ExEv_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_Pp_PpHead_CrPrCh_ID")).InnerText = gv.DataKeys[i]["pk_Pp_PpHead_CrPrCh_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_TchLrnMth_ID")).InnerText = gv.DataKeys[i]["pk_TchLrnMth_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_AssMth_ID")).InnerText = gv.DataKeys[i]["pk_AssMth_ID"].ToString();
                        childNode.AppendChild(xml.CreateElement("pk_AssType_ID")).InnerText = gv.DataKeys[i]["pk_AssType_ID"].ToString();
                        root.AppendChild(childNode);
                        xml.AppendChild(root);
                    }
                    xmldata += "</Root>";
                    int status = 0;

                    if (xmldata.Trim() != "")
                    {
                        SRVSecurePaper srv = new SRVSecurePaper();

                        //status = srv.SaveDownloadDetails(xml.OuterXml.Trim(), hidVenueID.Value, user.User_ID.ToString());
                    }
                    #endregion

                    #region Download
                    if (status > 0)
                    {

                        HtmlInputHidden hidExamEndTime = (HtmlInputHidden)gv.Parent.FindControl("hidExamEndTime");

                        if (hidExamEndTime.Value.EndsWith(":AM") || hidExamEndTime.Value.EndsWith(":PM"))
                        {
                            hidExamEndTime.Value = hidExamEndTime.Value.Remove(hidExamEndTime.Value.Length - 3);
                            hidExamEndTime.Value = hidExamEndTime.Value.Trim();
                        }

                        DateTime dt1 = DateTime.ParseExact(hidExamEndTime.Value, "HH:mm", new DateTimeFormatInfo());

                        DateTime dt2 = DateTime.ParseExact(DateTime.Now.ToString("HH:mm"), "HH:mm", new DateTimeFormatInfo());


                        if (dt2 < dt1)
                        {

                            string filePath = clsGetSettings.PhysicalSitePath + @"ExamDownloads\SRPD\Download\";

                            string examDateTime = gv.DataKeys[0]["ExamDateTime"].ToString();

                            examDateTime = examDateTime.Replace("/", "-");
                            examDateTime = examDateTime.Replace(":", "");

                            filePath = filePath + hidVenueCode.Value + @"/" + examDateTime;

                            string fileName = gv.DataKeys[0]["FileName"].ToString();

                            filePath = filePath + @"/" + fileName;

                            if (File.Exists(filePath + ".zip"))
                            {
                                Response.AddHeader("Content-Type", "application/octet-stream");
                                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".zip");   //strCrName.ToString().Trim()  size=" + b.Length.ToString()); //strCrName.ToString().Trim()
                                Response.TransmitFile(filePath + ".zip");
                                Response.End();
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Data cannot be downloaded')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Data cannot be downloaded')", true);
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                //throw ex;
            }
        }
        #endregion

        #endregion

        #region Other Functions
        #region FillGrid
        public void FillGrid()
        {
            dt = null;
            SRVSecurePaper srv = new SRVSecurePaper();

            dt = srv.ListPaperTlmAmAtForDownload(hidVenueID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                dv = new DataView();
                dv.Table = CreateTable();

                if (dv.Table != null && dv.Table.Rows.Count > 0)
                {
                    try
                    {
                        gvPapers.DataSource = dv;
                        gvPapers.DataBind();
                        gvPapers.Visible = true;
                        dt.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            else
            {
                lblMsg.Text = "No Data available for download";
                lblMsg.CssClass = "errorNote";
            }
        }
        #endregion

        #region SetHiddenVariables
        void SetHiddenVariables()
        {
            try
            {
                ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");

                hidVenueID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueID")).Value;
                hidVenueName.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueName")).Value;
                hidVenueCode.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueCode")).Value;

                lblSubHeader.Text = "for " + hidVenueCode.Value + " - " + hidVenueName.Value;

                DUConfigurations.clsSRPDDownloadTime time = DUConfigurations.clsDUConfigurations.Instance.SRPDownloadTime;

                int timeSpan = time.DownloadTime;

                lblMesg.Text = "*Note: Question Paper download will be available only " + timeSpan.ToString() + " hour before 'Exam Start Time' and upto 'Exam End Time'";

                lblMesg.Font.Bold = true;


            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region CreateTable
        protected DataTable CreateTable()
        {
            newDt = new DataTable("SlotTable");
            DataView dv = dt.DefaultView;
            newDt = dv.ToTable(true, "ExamDateTime", "ExamStartTime", "ExamDate", "ExamEndTime", "ZipFilePwd", "pk_ExEv_ID");

            DataView dataView = new DataView(newDt);
            dataView.Sort = "ExamDateTime ASC";
            newDt = dataView.ToTable();
            return newDt;

        }
        #endregion

        #region Send SMS function
        public string SendSMS(string sMobile_No, string Name, string sOTP, string sMsg)
        {
            string res = "-1";
            sMobile_No = "91" + sMobile_No.Trim();
            try
            {
                Sancharak.SendSMS obj = new Sancharak.SendSMS();
                obj.epMessage = "Dear " + Name + sMsg + sOTP;
                res = obj.SendPersonalizedSMS(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"));
            }
            catch
            { }
            return res;
        }
        #endregion

        #endregion
    }
}