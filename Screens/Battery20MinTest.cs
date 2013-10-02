
using System;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using clsiOS;
using nspTabletCommon;  
using clsTabletCommon.ITPExternal;
using System.Collections.Generic;

namespace ITPiPadSoln
{
    public partial class Battery20MinTest : UIViewController
    {
        //Global screen tags
        int m_i20MinSection = 0;
        int m_iSections = 0;

        int iPwrIdHdrRowTagId = 110;
        int iPwrIdHdrLabelTagId = 120;
        int iPwrIdHdrTagId = 130;
        int iBankNoHdrLabelTagId = 140;
        int iBankNoHdrTagId = 150;

        int iSectionTagId = 10000;
        int iSectionDBIdTagId = 10001;
        int iSectionDescTagId = 10002;
        int iSaveSectionBtnTagId = 10003;
        int iExpandSectionBtnTagId = 10004;
        int iContractSectionBtnTagId = 10005;
        int iContainerSectionTagId = 10006;
        int iSectionHeightTagId = 10010;
        int iSectionRowsTagId = 10011;
        int iSectionStatusTagId = 10012;
        int iSectionCompleteLabelTagId = 10013;
        int iSectionCounterTagId = 10014;

        //Header tags Ids
        int iHeaderRowStatusTagId = 10010700;
        int iInspectedByHdrLabel = 10010800;
        int iInspectedDateHdrLabel = 10010900;
        int iTestDateHdrLabel = 10011000;
        int iFloatVoltPriorHdrLabel = 10011100;
        int iChargePeriodPriorHdrLabel = 10011200;

        int iInspectedByTagId = 10011300;
        int iInspectedByHiddenTagId = 10011400;
        int iInspectedDateTagId = 10011500;
        int iInspectedDateHiddenTagId = 10011600;
        int iTestDateTagId = 10011700;
        int iTestDateHiddenTagId = 10011800;
        int iFloatVoltPriorTagId = 10011900;
        int iFloatVoltPriorHiddenTagId = 10012000;
        int iChargePeriodPriorTagId = 10012100;
        int iChargePeriodPriorHiddenTagId = 10012200;

        int iCellMbVoltageHdrLabel = 10012100;
        int iBatteryCapacityHdrLabel = 10012200;
        int iDischargeLoadRateHdrLabel = 10012300;
        int iCellMbPostHdrLabel = 10012400;
        int iBMPBPUCBAlarmHdrLabel = 10012500;

        int iCellMbVoltageTagId = 10012600;
        int iCellMbVoltageSearchTagId = 10012700;
        int iCellMbVoltageHiddenTagId = 10012800;
        int iBatteryCapacityTagId = 10012900;
        int iBatteryCapacityHiddenTagId = 10013000;
        int iDischargeLoadTagId = 10013100;
        int iDischargeLoadHiddenTagId = 10013200;
        int iCellMbPostTagId = 10013300;
        int iCellMbPostSearchTagId = 10013400;
        int iCellMbPostHiddenTagId = 10013500;
        int iBMPBPUCBTagId = 10013600;

        int iCommentsHdrLabel = 10014000;
        int iSummaryHdrLabel = 10014100;
        int iCommentsTagId = 10014200;
        int iSummaryFloatVoltLabelTagId = 10014300;
        int iSummaryFloatVoltResultTagId = 10014400;
        int iSummaryFloatVoltUnitTagId = 10014500;
        int iSummaryLoadCurrentLabelTagId = 10014600;
        int iSummaryLoadCurrentResultTagId = 10014700;
        int iSummaryLoadCurrentUnitTagId = 10014800;
        int iSummary20MinEndVoltLabelTagId = 10014900;
        int iSummary20MinEndVoltResultTagId = 10015000;
        int iSummary20MinEndVoltUnitTagId = 10015100;

        int iType1234CellContainerTagId = 10016000;
        int iType1234CellBaseLabelId = 10016100;
        int iType1234CellBaseId = 10016200;
        int iType1234CellBaseHiddenId = 10016300;

        string[] m_sCellMbVoltages;
        string[] m_sCellMbPost;

        string m_User = "";
        string m_sSessionId = "";
        string m_sPassedId = "";
        string m_sProjDesc = "";
        string m_PwrId = "";
        int m_BankNo = -1;
        string m_SPN = "";
        int m_iValidateType;
        object m_sender;

        bool m_bSuppressMove = false;
        bool gbSuppressSecondCheck = false;


        public Battery20MinTest() : base ("Battery20MinTest", null)
        {
        }

        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            // Perform any additional setup after loading the view, typically from a nib.
            base.ViewDidLoad ();
            UIBarButtonItem mybackbtn = new UIBarButtonItem("Back", UIBarButtonItemStyle.Plain, delegate (object sender, EventArgs e) {CheckUnsaved();});
            NavigationItem.SetHidesBackButton(true, true);
            NavigationItem.SetLeftBarButtonItem(mybackbtn, true);

            // Perform any additional setup after loading the view, typically from a nib.
            DrawMenu();
            DrawOpeningPage();
        }

        public void DrawMenu()
        {
            UIView[] arrItems = new UIView[8];
            string sUsername = "";
            HomeScreen home = GetHomeScreen();
            sUsername = home.GetLoginName();
            m_User = sUsername;
            m_sSessionId = home.GetSessionId();
            DownloadedITPsScreen downloadedScreen =   GetDownloadedITPsScreen();
            m_sPassedId = downloadedScreen.GetSelectedProjectId();
            m_sProjDesc = downloadedScreen.GetSelectedProjectDesc();
            Battery BattScreen =   GetBatteryPage();
            m_BankNo = BattScreen.GetSelectedBankNo();
            m_PwrId = BattScreen.GetSelectedPwrId();
            m_SPN = BattScreen.GetSelectedSPN();

            //Create a type of grid view
            iUtils.CreateFormGridItem lblProjIdLabel = new iUtils.CreateFormGridItem();
            UIView lblProjIdLabelVw = new UIView();
            lblProjIdLabel.SetDimensions(10f,5f, 80f, 25f, 2f, 2f, 2f, 2f);
            lblProjIdLabel.SetLabelText("Project Id:");
            lblProjIdLabel.SetBorderWidth(0.0f);
            lblProjIdLabel.SetFontName("Verdana");
            lblProjIdLabel.SetFontSize(12f);
            lblProjIdLabel.SetCellColour("Wheat");
            lblProjIdLabel.SetTag(10);
            lblProjIdLabelVw = lblProjIdLabel.GetLabelCell();
            arrItems[0] = lblProjIdLabelVw;

            iUtils.CreateFormGridItem lblProjId = new iUtils.CreateFormGridItem();
            UIView lblProjIdVw = new UIView();
            lblProjId.SetDimensions(90f,5f, 150f, 25f, 2f, 2f, 2f, 2f);
            lblProjId.SetLabelText(m_sPassedId);
            lblProjId.SetBorderWidth(0.0f);
            lblProjId.SetFontName("Verdana-Bold");
            lblProjId.SetFontSize(12f);
            lblProjId.SetCellColour("Wheat");
            lblProjId.SetTag(20);
            lblProjIdVw = lblProjId.GetLabelCell();
            arrItems[1] = lblProjIdVw;

            iUtils.CreateFormGridItem lblProjDesc = new iUtils.CreateFormGridItem();
            UIView lblProjDescVw = new UIView();
            lblProjDesc.SetDimensions(240f,5f, 370f, 25f, 2f, 2f, 2f, 2f);
            lblProjDesc.SetLabelText(m_sProjDesc);
            lblProjDesc.SetBorderWidth(0.0f);
            lblProjDesc.SetFontName("Verdana-Bold");
            lblProjDesc.SetFontSize(12f);
            lblProjDesc.SetCellColour("Wheat");
            lblProjDesc.SetTag(30);
            lblProjDescVw = lblProjDesc.GetLabelCell();
            arrItems[2] = lblProjDescVw;

            iUtils.CreateFormGridItem lblUsername = new iUtils.CreateFormGridItem();
            UIView lblUsernameVw = new UIView();
            lblUsername.SetDimensions(610f,5f, 80f, 25f, 2f, 2f, 2f, 2f);
            lblUsername.SetLabelText("Username:");
            lblUsername.SetBorderWidth(0.0f);
            lblUsername.SetFontName("Verdana");
            lblUsername.SetFontSize(12f);
            lblUsername.SetCellColour("Wheat");
            lblUsername.SetTag(40);
            lblUsernameVw = lblUsername.GetLabelCell();
            arrItems[3] = lblUsernameVw;

            iUtils.CreateFormGridItem txtUsername = new iUtils.CreateFormGridItem();
            UIView txtUsernameVw = new UIView();
            txtUsername.SetDimensions(690f,5f, 170f, 25f, 2f, 2f, 2f, 2f);
            txtUsername.SetLabelText(sUsername);
            txtUsername.SetBorderWidth(0.0f);
            txtUsername.SetFontName("Verdana");
            txtUsername.SetFontSize(12f);
            txtUsername.SetCellColour("Wheat");
            txtUsername.SetTag(50);
            txtUsernameVw = txtUsername.GetLabelCell();
            arrItems[4] = txtUsernameVw;

            iUtils.CreateFormGridItem txtChanges = new iUtils.CreateFormGridItem();
            UIView txtChangesVw = new UIView();
            txtChanges.SetDimensions(860f,5f, 150f, 25f, 2f, 2f, 2f, 2f);
            txtChanges.SetLabelText("UNSAVED CHANGES");
            txtChanges.SetBorderWidth(0.0f);
            txtChanges.SetFontSize(12f);
            txtChanges.SetFontName("Verdana-Bold");
            txtChanges.SetCellColour("Lilac");
            txtChanges.SetTag(65);
            txtChangesVw = txtChanges.GetLabelCell();
            txtChangesVw.Hidden = true;
            txtChangesVw.Tag = 60;
            arrItems[5] = txtChangesVw;

            UILabel hfSessionId = new UILabel();
            hfSessionId.Tag = 70;
            hfSessionId.Frame = new RectangleF(230f,100f,200f,30f);
            hfSessionId.Text = m_sSessionId;
            hfSessionId.Hidden = true;
            arrItems[6] = hfSessionId;

            UILabel hfEditStatus = new UILabel();
            hfEditStatus.Tag = 80;
            hfEditStatus.Frame = new RectangleF(230f,150f,200f,30f);
            hfEditStatus.Text = "0";
            hfEditStatus.Hidden = true;
            arrItems[7] = hfEditStatus;

            View.AddSubviews(arrItems);

        }

        public void DrawOpeningPage ()
        {
            DateClass dt = new DateClass();
            clsLocalUtils clsUtil = new clsLocalUtils();
            float iTotalHeight = 0f;
            float iVert = 0.0f;
            float iLayoutVert = 0.0f;
            float iRowHeight = 20f;
            float iEditRowHeight = 40f;
            float iSectionHdrRowHeight = 40f;
            float iSectionHeightId = 0f;
            float iThisHdrRowHeight = 40f;
            int iColNo = 0;
            float iHeightToAdd = 0f;
            UIView[] arrItems = new UIView[7];
            UIView[] arrItems2 = new UIView[10];
            UIView[] arrItems3 = new UIView[5];
            UIView[] arrItems4 = new UIView[11];
            UIView[] arrItems5 = new UIView[2];
            UIView[] arrItems6 = new UIView[10];

            UIView hdrSection;
            UIView hdrSection2;
            UIView hdrSection3;
            UIView hdrSection4; //Not used because 2 sub headers and we want to keep things synchronised
            UIView hdrSection5;
            UIView hdrSection6; //Not used because 2 sub headers and we want to keep things synchronised
            UIView hdrSubSection3;
            UIView hdrSubSection4;
            UIView hdrSubSection5;
            UIView hdrSubSection6;
            UIView hdrPwrId;
            clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
            clsTabletDB.ITPBatteryCellInfo ITPCellInfo = new clsTabletDB.ITPBatteryCellInfo();
            clsTabletDB.ITPHeaderTable ITPHeader = new clsTabletDB.ITPHeaderTable(); 
            string sInspectedBy = "";
            string sInspectDate = "";
            string sInspectDateDisplay = "";
            string sTestDate = "";
            string sTestDateDisplay = "";
            string sFloatVoltsPrior = "";
            double dFloatVoltsPrior = 0;
            string sChargePeriodPrior = "";
            double dChargePeriodPrior = 0;
            string sCellMbVoltage = "";
            double dCellMbVoltage = 0;
            string sBatteryCapacity = "";
            double dBatteryCapacity = 0;
            string sDischargeLoad = "";
            double dDischargeLoad = 0;
            string sCellMbPost = "";
            double dCellMbPost = 0;
            string sBMPBPUCB = "";
            int iBMPBPUCB = 0;
            string sSumFloatVoltResult = "";
            double dSumFloatVoltResult = 0;
            string sSumLoadCurrentResult = "";
            double dSumLoadCurrentResult = 0;
            string sSum20MinEndVoltResult = "";
            double dSum20MinEndVoltResult = 0;
            bool bReadOnly = false;
            string sComments = "";

            try
            {
                UIScrollView layout = new UIScrollView();
                layout.Frame = new RectangleF(0f,35f,1000f,620f);
                layout.Tag = 2;
                View.AddSubview(layout);

                //This has a height of 40f hence the positioning of the section header at 40f vertically and the section body at 80f vertically
                hdrPwrId = BuildPwrIdHeader(m_PwrId, m_BankNo);
                layout.AddSubview(hdrPwrId);

                iLayoutVert += 40f;

                hdrSection = BuildSectionHeader(m_iSections, "20 min Test Header Info", iLayoutVert, ref iSectionHdrRowHeight,1);
                layout.AddSubview(hdrSection);

                iLayoutVert += iSectionHdrRowHeight;

                UIView SectionTableRow = new UIView();
                SectionTableRow.Frame = new RectangleF(0f,iLayoutVert,1000f,iSectionHdrRowHeight);
                SectionTableRow.Tag = iContainerSectionTagId * (m_iSections);

                iVert = 0f;
                iSectionHeightId = 0f;

                //Get the base block voltage
                double dDefaultVoltage = ITPCellInfo.GetBatteryBlockVoltage(m_SPN);
                double dSystemVoltage = ITPHeader.GetSystemVolts(m_sPassedId, m_PwrId);

                //Now get the data from the DB
                DataSet arrTestHeader = ITPBattTest.GetBatteryAcceptTestType4KeyRecord(m_sPassedId, "ITPBattAcceptTest_Header", m_PwrId, m_BankNo);
                if(arrTestHeader.Tables[0].Rows.Count > 0)
                {
                    iColNo = arrTestHeader.Tables[0].Columns["InspectedBy"].Ordinal;
                    sInspectedBy = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    iColNo = arrTestHeader.Tables[0].Columns["InspectionDate"].Ordinal;
                    sInspectDate = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    DateTime dtInspectDate = Convert.ToDateTime(sInspectDate);
                    sInspectDateDisplay = dt.Get_Date_String(dtInspectDate, "dd/mm/yy");
                    iColNo = arrTestHeader.Tables[0].Columns["TestDate"].Ordinal;
                    sTestDate = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    DateTime dtTestDate = Convert.ToDateTime(sTestDate);
                    sTestDateDisplay = dt.Get_Date_String(dtTestDate, "dd/mm/yy");
                    iColNo = arrTestHeader.Tables[0].Columns["FloatVoltsPriorTest"].Ordinal;
                    sFloatVoltsPrior = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sFloatVoltsPrior))
                    {
                        dFloatVoltsPrior = Convert.ToDouble(sFloatVoltsPrior);
                    }
                    else
                    {
                        dFloatVoltsPrior = 0.0;
                    }
                    iColNo = arrTestHeader.Tables[0].Columns["PeriodOnChargeTest"].Ordinal;
                    sChargePeriodPrior = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sChargePeriodPrior))
                    {
                        dChargePeriodPrior = Convert.ToDouble(sChargePeriodPrior);
                    }
                    else
                    {
                        dChargePeriodPrior = 0.0;
                    }
                    iColNo = arrTestHeader.Tables[0].Columns["CellVoltage"].Ordinal;
                    sCellMbVoltage = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sCellMbVoltage))
                    {
                        dCellMbVoltage = Convert.ToDouble(sCellMbVoltage);
                        if(dCellMbVoltage == 0.0)
                        {
                            dCellMbVoltage = dDefaultVoltage;
                        }
                    }
                    else
                    {
                        dCellMbVoltage = dDefaultVoltage;
                    }

                    iColNo = arrTestHeader.Tables[0].Columns["BatteryStringCapacity"].Ordinal;
                    sBatteryCapacity = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sBatteryCapacity))
                    {
                        dBatteryCapacity = Convert.ToDouble(sBatteryCapacity);
                    }
                    else
                    {
                        dBatteryCapacity = 0.0;
                    }
                    iColNo = arrTestHeader.Tables[0].Columns["DischargeLoadRate"].Ordinal;
                    sDischargeLoad = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sDischargeLoad))
                    {
                        dDischargeLoad = Convert.ToDouble(sDischargeLoad);
                    }
                    else
                    {
                        dDischargeLoad = 0.0;
                    }
                    iColNo = arrTestHeader.Tables[0].Columns["CellPost"].Ordinal;
                    sCellMbPost = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sCellMbPost))
                    {
                        dCellMbPost = Convert.ToDouble(sCellMbPost);
                        if(dCellMbPost == 0.0)
                        {
                            dCellMbPost = dDefaultVoltage;
                        }
                    }
                    else
                    {
                        dCellMbPost = dDefaultVoltage;
                    }
                
                    iColNo = arrTestHeader.Tables[0].Columns["BMP_BPU_CB_Alarm"].Ordinal;
                    sBMPBPUCB = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sBMPBPUCB))
                    {
                        iBMPBPUCB = Convert.ToInt32(sBMPBPUCB);
                    }
                    else
                    {
                        iBMPBPUCB = 0;
                    }

                    iColNo = arrTestHeader.Tables[0].Columns["FloatVoltage"].Ordinal;
                    sSumFloatVoltResult = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sSumFloatVoltResult))
                    {
                        dSumFloatVoltResult = Convert.ToDouble(sSumFloatVoltResult);
                    }
                    else
                    {
                        dSumFloatVoltResult = 0.0;
                    }

                    iColNo = arrTestHeader.Tables[0].Columns["FloatLoadCurrent"].Ordinal;
                    sSumLoadCurrentResult = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sSumLoadCurrentResult))
                    {
                        dSumLoadCurrentResult = Convert.ToDouble(sSumLoadCurrentResult);
                    }
                    else
                    {
                        dSumLoadCurrentResult = 0.0;
                    }


                    iColNo = arrTestHeader.Tables[0].Columns["TwentyMinuteEndDischargeVolts"].Ordinal;
                    sSum20MinEndVoltResult = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    if(clsUtil.IsNumeric(sSum20MinEndVoltResult))
                    {
                        dSum20MinEndVoltResult = Convert.ToDouble(sSum20MinEndVoltResult);
                    }
                    else
                    {
                        dSum20MinEndVoltResult = 0.0;
                    }
                }


                UILabel hfRow10Status = new UILabel();
                hfRow10Status.Text = "0";
                hfRow10Status.Tag = iHeaderRowStatusTagId;
                hfRow10Status.Hidden = true;
                arrItems[0] = hfRow10Status;

                UILabel hfSectionStatus = new UILabel();
                hfSectionStatus.Text = "0";
                hfSectionStatus.Tag = iSectionStatusTagId;
                hfSectionStatus.Hidden = true;
                arrItems[1] = hfSectionStatus;

                /********************************************************************************/
                //              ROW 1 HEADER                                                    //
                /********************************************************************************/
                iUtils.CreateFormGridItem lblInspectByLabel = new iUtils.CreateFormGridItem();
                UIView lblInspectByLabelVw = new UIView();
                lblInspectByLabel.SetDimensions(0f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblInspectByLabel.SetLabelText("Inspected By");
                lblInspectByLabel.SetBorderWidth(1.0f);
                lblInspectByLabel.SetFontName("Verdana");
                lblInspectByLabel.SetFontSize(12f);
                lblInspectByLabel.SetTag(iInspectedByHdrLabel);
                lblInspectByLabel.SetCellColour("Pale Yellow");
                lblInspectByLabelVw = lblInspectByLabel.GetLabelCell();
                arrItems[2] = lblInspectByLabelVw;

                iUtils.CreateFormGridItem lblInspectDateLabel = new iUtils.CreateFormGridItem();
                UIView lblInspectDateLabelVw = new UIView();
                lblInspectDateLabel.SetDimensions(200f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblInspectDateLabel.SetLabelText("Inspected Date");
                lblInspectDateLabel.SetBorderWidth(1.0f);
                lblInspectDateLabel.SetFontName("Verdana");
                lblInspectDateLabel.SetFontSize(12f);
                lblInspectDateLabel.SetTag(iInspectedDateHdrLabel);
                lblInspectDateLabel.SetCellColour("Pale Yellow");
                lblInspectDateLabelVw = lblInspectDateLabel.GetLabelCell();
                arrItems[3] = lblInspectDateLabelVw;

                iUtils.CreateFormGridItem lblTestDateLabel = new iUtils.CreateFormGridItem();
                UIView lblTestDateLabelVw = new UIView();
                lblTestDateLabel.SetDimensions(400f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblTestDateLabel.SetLabelText("Test Date");
                lblTestDateLabel.SetBorderWidth(1.0f);
                lblTestDateLabel.SetFontName("Verdana");
                lblTestDateLabel.SetFontSize(12f);
                lblTestDateLabel.SetTag(iTestDateHdrLabel);
                lblTestDateLabel.SetCellColour("Pale Yellow");
                lblTestDateLabelVw = lblTestDateLabel.GetLabelCell();
                arrItems[4] = lblTestDateLabelVw;

                iUtils.CreateFormGridItem lblFloatVoltPriorLabel = new iUtils.CreateFormGridItem();
                UIView lblFloatVoltPriorLabelVw = new UIView();
                lblFloatVoltPriorLabel.SetDimensions(600f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblFloatVoltPriorLabel.SetLabelText("Float Voltage Prior to Test");
                lblFloatVoltPriorLabel.SetBorderWidth(1.0f);
                lblFloatVoltPriorLabel.SetFontName("Verdana");
                lblFloatVoltPriorLabel.SetFontSize(12f);
                lblFloatVoltPriorLabel.SetTag(iFloatVoltPriorHdrLabel);
                lblFloatVoltPriorLabel.SetCellColour("Pale Yellow");
                lblFloatVoltPriorLabelVw = lblFloatVoltPriorLabel.GetLabelCell();
                arrItems[5] = lblFloatVoltPriorLabelVw;

                iUtils.CreateFormGridItem lblChargePeriodPriorLabel = new iUtils.CreateFormGridItem();
                UIView lblChargePeriodPriorLabelVw = new UIView();
                lblChargePeriodPriorLabel.SetDimensions(800f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblChargePeriodPriorLabel.SetLabelText("Charge Period Prior to Test");
                lblChargePeriodPriorLabel.SetBorderWidth(1.0f);
                lblChargePeriodPriorLabel.SetFontName("Verdana");
                lblChargePeriodPriorLabel.SetFontSize(12f);
                lblChargePeriodPriorLabel.SetTag(iChargePeriodPriorHdrLabel);
                lblChargePeriodPriorLabel.SetCellColour("Pale Yellow");
                lblChargePeriodPriorLabelVw = lblChargePeriodPriorLabel.GetLabelCell();
                arrItems[6] = lblChargePeriodPriorLabelVw;

                SectionTableRow.AddSubviews(arrItems);

                iVert+= iRowHeight;
                iSectionHeightId += iRowHeight;

                /********************************************************************************/
                //              ROW 1 DETAIL                                                    //
                /********************************************************************************/
                iUtils.CreateFormGridItem lblInspectBy = new iUtils.CreateFormGridItem();
                UIView lblInspectByVw = new UIView();
                lblInspectBy.SetDimensions(0f, iVert, 200f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblInspectBy.SetLabelText(sInspectedBy);
                lblInspectBy.SetBorderWidth(1.0f);
                lblInspectBy.SetFontName("Verdana");
                lblInspectBy.SetFontSize(12f);
                lblInspectBy.SetTag(iInspectedByTagId);
                lblInspectBy.SetCellColour("Pale Yellow");

                lblInspectByVw = lblInspectBy.GetTextFieldCell();
                UITextField txtInspectByView = lblInspectBy.GetTextFieldView();
                txtInspectByView.AutocorrectionType = UITextAutocorrectionType.No;
                txtInspectByView.KeyboardType = UIKeyboardType.Default;
                txtInspectByView.ReturnKeyType = UIReturnKeyType.Next;
                txtInspectByView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 1);};
//                txtInspectByView.ShouldEndEditing += (sender) => {
//                    return sInspectedBy(sender,0);};
                txtInspectByView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 1);};

                if(bReadOnly)
                {
                    txtInspectByView.Enabled = false;
                }

                arrItems2 [0] = lblInspectByVw;

                UILabel hfCurrentInspectBy = new UILabel();
                hfCurrentInspectBy.Text = sInspectedBy;
                hfCurrentInspectBy.Tag = iInspectedByHiddenTagId;
                hfCurrentInspectBy.Hidden = true;
                arrItems2 [1] = hfCurrentInspectBy;

                iUtils.CreateFormGridItem lblInspectDate = new iUtils.CreateFormGridItem();
                UIView lblInspectDateVw = new UIView();
                lblInspectDate.SetDimensions(200f, iVert, 200f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblInspectDate.SetLabelText(sInspectDateDisplay);
                lblInspectDate.SetBorderWidth(1.0f);
                lblInspectDate.SetFontName("Verdana");
                lblInspectDate.SetFontSize(12f);
                lblInspectDate.SetTag(iInspectedDateTagId);
                lblInspectDate.SetCellColour("Pale Yellow");

                lblInspectDateVw = lblInspectDate.GetTextFieldCell();
                UITextField txtInspectDateView = lblInspectDate.GetTextFieldView();
                txtInspectDateView.AutocorrectionType = UITextAutocorrectionType.No;
                txtInspectDateView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtInspectDateView.ReturnKeyType = UIReturnKeyType.Next;
                txtInspectDateView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 2);};
                txtInspectDateView.ShouldEndEditing += (sender) => {
                    return ValidateInspectDate(sender,0);};
                txtInspectDateView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 2);};

                if(bReadOnly)
                {
                    txtInspectDateView.Enabled = false;
                }
  
                arrItems2 [2] = lblInspectDateVw;

                UILabel hfCurrentInspectDate = new UILabel();
                hfCurrentInspectDate.Text = sInspectDateDisplay;
                hfCurrentInspectDate.Tag = iInspectedDateHiddenTagId;
                hfCurrentInspectDate.Hidden = true;
                arrItems2 [3] = hfCurrentInspectDate;

                iUtils.CreateFormGridItem lblTestedDate = new iUtils.CreateFormGridItem();
                UIView lblTestedDateVw = new UIView();
                lblTestedDate.SetDimensions(400f, iVert, 200f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblTestedDate.SetLabelText(sTestDateDisplay);
                lblTestedDate.SetBorderWidth(1.0f);
                lblTestedDate.SetFontName("Verdana");
                lblTestedDate.SetFontSize(12f);
                lblTestedDate.SetTag(iTestDateTagId);
                lblTestedDate.SetCellColour("Pale Yellow");

                lblTestedDateVw = lblTestedDate.GetTextFieldCell();
                UITextField txtTestedDateView = lblTestedDate.GetTextFieldView();
                txtTestedDateView.AutocorrectionType = UITextAutocorrectionType.No;
                txtTestedDateView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtTestedDateView.ReturnKeyType = UIReturnKeyType.Next;
                txtTestedDateView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 3);};
                txtTestedDateView.ShouldEndEditing += (sender) => {
                    return ValidateTestDate(sender,0);};
                txtTestedDateView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 3);};

                if(bReadOnly)
                {
                    txtTestedDateView.Enabled = false;
                }

                arrItems2 [4] = lblTestedDateVw;

                UILabel hfCurrentTestedDate = new UILabel();
                hfCurrentTestedDate.Text = sTestDateDisplay;
                hfCurrentTestedDate.Tag = iTestDateHiddenTagId;
                hfCurrentTestedDate.Hidden = true;
                arrItems2 [5] = hfCurrentTestedDate;

                iUtils.CreateFormGridItem lblFloatVoltagePrior = new iUtils.CreateFormGridItem();
                UIView lblFloatVoltagePriorVw = new UIView();
                lblFloatVoltagePrior.SetDimensions(600f, iVert, 200f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblFloatVoltagePrior.SetLabelText(dFloatVoltsPrior.ToString());
                lblFloatVoltagePrior.SetBorderWidth(1.0f);
                lblFloatVoltagePrior.SetFontName("Verdana");
                lblFloatVoltagePrior.SetFontSize(12f);
                lblFloatVoltagePrior.SetTag(iFloatVoltPriorTagId);
                lblFloatVoltagePrior.SetCellColour("Pale Yellow");

                lblFloatVoltagePriorVw = lblFloatVoltagePrior.GetTextFieldCell();
                UITextField txtFloatVoltagePriorView = lblFloatVoltagePrior.GetTextFieldView();
                txtFloatVoltagePriorView.AutocorrectionType = UITextAutocorrectionType.No;
                txtFloatVoltagePriorView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtFloatVoltagePriorView.ReturnKeyType = UIReturnKeyType.Next;
                txtFloatVoltagePriorView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 4);};
                txtFloatVoltagePriorView.ShouldEndEditing += (sender) => {
                    return ValidateNumberOnly(sender,0, m_i20MinSection);};
                txtFloatVoltagePriorView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 4);};

                if(bReadOnly)
                {
                    txtFloatVoltagePriorView.Enabled = false;
                }

                arrItems2 [6] = lblFloatVoltagePriorVw;

                UILabel hfCurrentFloatVoltagePrior = new UILabel();
                hfCurrentFloatVoltagePrior.Text = dFloatVoltsPrior.ToString();
                hfCurrentFloatVoltagePrior.Tag = iFloatVoltPriorHiddenTagId;
                hfCurrentFloatVoltagePrior.Hidden = true;
                arrItems2 [7] = hfCurrentFloatVoltagePrior;

                iUtils.CreateFormGridItem lblChargePeriodPrior = new iUtils.CreateFormGridItem();
                UIView lblChargePeriodPriorVw = new UIView();
                lblChargePeriodPrior.SetDimensions(800f, iVert, 200f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblChargePeriodPrior.SetLabelText(dChargePeriodPrior.ToString());
                lblChargePeriodPrior.SetBorderWidth(1.0f);
                lblChargePeriodPrior.SetFontName("Verdana");
                lblChargePeriodPrior.SetFontSize(12f);
                lblChargePeriodPrior.SetTag(iChargePeriodPriorTagId);
                lblChargePeriodPrior.SetCellColour("Pale Yellow");

                lblChargePeriodPriorVw = lblChargePeriodPrior.GetTextFieldCell();
                UITextField txtChargePeriodPriorView = lblChargePeriodPrior.GetTextFieldView();
                txtChargePeriodPriorView.AutocorrectionType = UITextAutocorrectionType.No;
                txtChargePeriodPriorView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtChargePeriodPriorView.ReturnKeyType = UIReturnKeyType.Next;
                txtChargePeriodPriorView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 5);};
                txtChargePeriodPriorView.ShouldEndEditing += (sender) => {
                    return ValidateNumberOnly(sender,0, m_i20MinSection);};
                txtChargePeriodPriorView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 5);};

                if(bReadOnly)
                {
                    txtChargePeriodPriorView.Enabled = false;
                }

                arrItems2 [8] = lblChargePeriodPriorVw;

                UILabel hfCurrentChargePeriodPrior = new UILabel();
                hfCurrentChargePeriodPrior.Text = dChargePeriodPrior.ToString();
                hfCurrentChargePeriodPrior.Tag = iChargePeriodPriorHiddenTagId;
                hfCurrentChargePeriodPrior.Hidden = true;
                arrItems2 [9] = hfCurrentChargePeriodPrior;

                SectionTableRow.AddSubviews(arrItems2);

                iVert+= iEditRowHeight;
                iSectionHeightId += iEditRowHeight;

                /********************************************************************************/
                //              ROW 2 HEADER                                                    //
                /********************************************************************************/
                iUtils.CreateFormGridItem lblCellMbVoltageLabel = new iUtils.CreateFormGridItem();
                UIView lblCellMbVoltageLabelVw = new UIView();
                lblCellMbVoltageLabel.SetDimensions(0f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblCellMbVoltageLabel.SetLabelText("Cell/Monoblock Voltage");
                lblCellMbVoltageLabel.SetBorderWidth(1.0f);
                lblCellMbVoltageLabel.SetFontName("Verdana");
                lblCellMbVoltageLabel.SetFontSize(12f);
                lblCellMbVoltageLabel.SetTag(iCellMbVoltageHdrLabel);
                lblCellMbVoltageLabel.SetCellColour("Pale Yellow");
                lblCellMbVoltageLabelVw = lblCellMbVoltageLabel.GetLabelCell();
                arrItems3 [0] = lblCellMbVoltageLabelVw;

                iUtils.CreateFormGridItem lblBattStringCapacityLabel = new iUtils.CreateFormGridItem();
                UIView lblBattStringCapacityLabelVw = new UIView();
                lblBattStringCapacityLabel.SetDimensions(200f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblBattStringCapacityLabel.SetLabelText("Battery String Capacity (AHr)");
                lblBattStringCapacityLabel.SetBorderWidth(1.0f);
                lblBattStringCapacityLabel.SetFontName("Verdana");
                lblBattStringCapacityLabel.SetFontSize(12f);
                lblBattStringCapacityLabel.SetTag(iBatteryCapacityHdrLabel);
                lblBattStringCapacityLabel.SetCellColour("Pale Yellow");
                lblBattStringCapacityLabelVw = lblBattStringCapacityLabel.GetLabelCell();
                arrItems3 [1] = lblBattStringCapacityLabelVw;

                iUtils.CreateFormGridItem lblDischargeLoadRateLabel = new iUtils.CreateFormGridItem();
                UIView lblDischargeLoadRateLabelVw = new UIView();
                lblDischargeLoadRateLabel.SetDimensions(400f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblDischargeLoadRateLabel.SetLabelText("Discharge Load Rate (A)");
                lblDischargeLoadRateLabel.SetBorderWidth(1.0f);
                lblDischargeLoadRateLabel.SetFontName("Verdana");
                lblDischargeLoadRateLabel.SetFontSize(12f);
                lblDischargeLoadRateLabel.SetTag(iDischargeLoadRateHdrLabel);
                lblDischargeLoadRateLabel.SetCellColour("Pale Yellow");
                lblDischargeLoadRateLabelVw = lblDischargeLoadRateLabel.GetLabelCell();
                arrItems3[2] = lblDischargeLoadRateLabelVw;

                iUtils.CreateFormGridItem lblCellMbPostLabel = new iUtils.CreateFormGridItem();
                UIView lblCellMbPostLabelVw = new UIView();
                lblCellMbPostLabel.SetDimensions(600f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblCellMbPostLabel.SetLabelText("Cell/Monoblock Post");
                lblCellMbPostLabel.SetBorderWidth(1.0f);
                lblCellMbPostLabel.SetFontName("Verdana");
                lblCellMbPostLabel.SetFontSize(12f);
                lblCellMbPostLabel.SetTag(iCellMbPostHdrLabel);
                lblCellMbPostLabel.SetCellColour("Pale Yellow");
                lblCellMbPostLabelVw = lblCellMbPostLabel.GetLabelCell();
                arrItems3[3] = lblCellMbPostLabelVw;

                iUtils.CreateFormGridItem lblBMPBPUCBAlarmLabel = new iUtils.CreateFormGridItem();
                UIView lblBMPBPUCBAlarmLabelVw = new UIView();
                lblBMPBPUCBAlarmLabel.SetDimensions(800f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblBMPBPUCBAlarmLabel.SetLabelText("BMP/BPU CB ALarm Tested");
                lblBMPBPUCBAlarmLabel.SetBorderWidth(1.0f);
                lblBMPBPUCBAlarmLabel.SetFontName("Verdana");
                lblBMPBPUCBAlarmLabel.SetFontSize(12f);
                lblBMPBPUCBAlarmLabel.SetTag(iBMPBPUCBAlarmHdrLabel);
                lblBMPBPUCBAlarmLabel.SetCellColour("Pale Yellow");
                lblBMPBPUCBAlarmLabelVw = lblBMPBPUCBAlarmLabel.GetLabelCell();
                arrItems3[4] = lblBMPBPUCBAlarmLabelVw;

                SectionTableRow.AddSubviews(arrItems3);

                iVert+= iRowHeight;
                iSectionHeightId += iRowHeight;

                /********************************************************************************/
                //              ROW 2 DETAIL                                                    //
                /********************************************************************************/
                iUtils.CreateFormGridItem lblCellMbVoltage = new iUtils.CreateFormGridItem();
                UIView lblCellMbVoltageVw = new UIView();
                lblCellMbVoltage.SetDimensions(0f,iVert, 140f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblCellMbVoltage.SetLabelText(dCellMbVoltage.ToString());
                lblCellMbVoltage.SetTextAlignment("centre");
                lblCellMbVoltage.SetHideBorder(false, false, true, false);
                lblCellMbVoltage.SetBorderWidth(1.0f);
                lblCellMbVoltage.SetFontName("Verdana");
                lblCellMbVoltage.SetFontSize(12f);
                lblCellMbVoltage.SetTag(iCellMbVoltageTagId);
                lblCellMbVoltage.SetCellColour("Pale Yellow");
                lblCellMbVoltageVw = lblCellMbVoltage.GetLabelCell();
                arrItems4[0] = lblCellMbVoltageVw;

                iUtils.CreateFormGridItem btnCellMBVoltageSearch = new iUtils.CreateFormGridItem();
                UIView btnCellMBVoltageSearchVw = new UIView();
                btnCellMBVoltageSearch.SetDimensions(140f, iVert, 60f, iEditRowHeight, 8f, 4f, 8f, 4f);
                btnCellMBVoltageSearch.SetLabelText("...");
                btnCellMBVoltageSearch.SetHideBorder(true, false, false, false);
                btnCellMBVoltageSearch.SetBorderWidth(1.0f);
                btnCellMBVoltageSearch.SetFontName("Verdana");
                btnCellMBVoltageSearch.SetFontSize(12f);
                btnCellMBVoltageSearch.SetTag(iCellMbVoltageSearchTagId);
                btnCellMBVoltageSearch.SetCellColour("Pale Yellow");
                btnCellMBVoltageSearchVw = btnCellMBVoltageSearch.GetButtonCell();

                UIButton btnCellMBVoltageSearchButton = new UIButton();
                btnCellMBVoltageSearchButton = btnCellMBVoltageSearch.GetButton();
                btnCellMBVoltageSearchButton.TouchUpInside += (sender,e) => {
                    OpenCellMbVoltageList(sender, e);};

                if(bReadOnly)
                {
                    btnCellMBVoltageSearchButton.Enabled = false;
                }
                arrItems4[1] = btnCellMBVoltageSearchVw;

                UILabel hfCurrentCellMBVoltage = new UILabel();
                hfCurrentCellMBVoltage.Text = dCellMbVoltage.ToString();
                hfCurrentCellMBVoltage.Tag = iCellMbVoltageHiddenTagId;
                hfCurrentCellMBVoltage.Hidden = true;
                arrItems4[2] = hfCurrentCellMBVoltage;

                iUtils.CreateFormGridItem lblBatteryCapacity = new iUtils.CreateFormGridItem();
                UIView lblBatteryCapacityVw = new UIView();
                lblBatteryCapacity.SetDimensions(200f, iVert, 200f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblBatteryCapacity.SetLabelText(dBatteryCapacity.ToString());
                lblBatteryCapacity.SetBorderWidth(1.0f);
                lblBatteryCapacity.SetFontName("Verdana");
                lblBatteryCapacity.SetFontSize(12f);
                lblBatteryCapacity.SetTag(iBatteryCapacityTagId);
                lblBatteryCapacity.SetCellColour("Pale Yellow");

                lblBatteryCapacityVw = lblBatteryCapacity.GetTextFieldCell();
                UITextField txtBatteryCapacityView = lblBatteryCapacity.GetTextFieldView();
                txtBatteryCapacityView.AutocorrectionType = UITextAutocorrectionType.No;
                txtBatteryCapacityView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtBatteryCapacityView.ReturnKeyType = UIReturnKeyType.Next;
                txtBatteryCapacityView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 6);};
                txtBatteryCapacityView.ShouldEndEditing += (sender) => {
                    return ValidateNumberOnly(sender,0, m_i20MinSection);};
                txtBatteryCapacityView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 6);};

                if(bReadOnly)
                {
                    txtBatteryCapacityView.Enabled = false;
                }

                arrItems4[3] = lblBatteryCapacityVw;

                UILabel hfCurrentBatteryCapacity = new UILabel();
                hfCurrentBatteryCapacity.Text = dBatteryCapacity.ToString();
                hfCurrentBatteryCapacity.Tag = iBatteryCapacityHiddenTagId;
                hfCurrentBatteryCapacity.Hidden = true;
                arrItems4[4] = hfCurrentBatteryCapacity;

                iUtils.CreateFormGridItem lblDischargeLoad = new iUtils.CreateFormGridItem();
                UIView lblDischargeLoadVw = new UIView();
                lblDischargeLoad.SetDimensions(400f, iVert, 200f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblDischargeLoad.SetLabelText(dDischargeLoad.ToString());
                lblDischargeLoad.SetBorderWidth(1.0f);
                lblDischargeLoad.SetFontName("Verdana");
                lblDischargeLoad.SetFontSize(12f);
                lblDischargeLoad.SetTag(iDischargeLoadTagId);
                lblDischargeLoad.SetCellColour("Pale Yellow");

                lblDischargeLoadVw = lblDischargeLoad.GetTextFieldCell();
                UITextField txtDischargeLoadView = lblDischargeLoad.GetTextFieldView();
                txtDischargeLoadView.AutocorrectionType = UITextAutocorrectionType.No;
                txtDischargeLoadView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtDischargeLoadView.ReturnKeyType = UIReturnKeyType.Next;
                txtDischargeLoadView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 7);};
                txtDischargeLoadView.ShouldEndEditing += (sender) => {
                    return ValidateNumberOnly(sender,0, m_i20MinSection);};
                txtDischargeLoadView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 7);};

                if(bReadOnly)
                {
                    txtDischargeLoadView.Enabled = false;
                }

                arrItems4[5] = lblDischargeLoadVw;

                UILabel hfCurrentDischargeLoad = new UILabel();
                hfCurrentDischargeLoad.Text = dDischargeLoad.ToString();
                hfCurrentDischargeLoad.Tag = iDischargeLoadHiddenTagId;
                hfCurrentDischargeLoad.Hidden = true;
                arrItems4[6] = hfCurrentDischargeLoad;

                iUtils.CreateFormGridItem lblCellMbPost = new iUtils.CreateFormGridItem();
                UIView lblCellMbPostVw = new UIView();
                lblCellMbPost.SetDimensions(600f,iVert, 140f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblCellMbPost.SetLabelText(dCellMbPost.ToString());
                lblCellMbPost.SetTextAlignment("centre");
                lblCellMbPost.SetHideBorder(false, false, true, false);
                lblCellMbPost.SetBorderWidth(1.0f);
                lblCellMbPost.SetFontName("Verdana");
                lblCellMbPost.SetFontSize(12f);
                lblCellMbPost.SetTag(iCellMbPostTagId);
                lblCellMbPost.SetCellColour("Pale Yellow");
                lblCellMbPostVw = lblCellMbPost.GetLabelCell();
                arrItems4[7] = lblCellMbPostVw;

                iUtils.CreateFormGridItem btnCellMbPostSearch = new iUtils.CreateFormGridItem();
                UIView btnCellMbPostSearchVw = new UIView();
                btnCellMbPostSearch.SetDimensions(740f, iVert, 60f, iEditRowHeight, 8f, 4f, 8f, 4f);
                btnCellMbPostSearch.SetLabelText("...");
                btnCellMbPostSearch.SetHideBorder(true, false, false, false);
                btnCellMbPostSearch.SetBorderWidth(1.0f);
                btnCellMbPostSearch.SetFontName("Verdana");
                btnCellMbPostSearch.SetFontSize(12f);
                btnCellMbPostSearch.SetTag(iCellMbPostSearchTagId);
                btnCellMbPostSearch.SetCellColour("Pale Yellow");
                btnCellMbPostSearchVw = btnCellMbPostSearch.GetButtonCell();

                UIButton btnCellMbPostSearchButton = new UIButton();
                btnCellMbPostSearchButton = btnCellMbPostSearch.GetButton();
                btnCellMbPostSearchButton.TouchUpInside += (sender,e) => {
                    OpenCellMbPostList(sender, e);};

                if(bReadOnly)
                {
                    btnCellMbPostSearchButton.Enabled = false;
                }
                arrItems4[8] = btnCellMbPostSearchVw;

                UILabel hfCurrentCellMbPost = new UILabel();
                hfCurrentCellMbPost.Text = dCellMbPost.ToString();
                hfCurrentCellMbPost.Tag = iCellMbPostHiddenTagId;
                hfCurrentCellMbPost.Hidden = true;
                arrItems4[9] = hfCurrentCellMbPost;


                iUtils.CreateFormGridItem chkBMPBPUCB = new iUtils.CreateFormGridItem();
                UIView chkBMPBPUCBVw = new UIView();
                chkBMPBPUCB.SetDimensions(800f,iVert, 200f, iEditRowHeight, 30f, 2.5f, 30f, 2.5f);
                bool bBMPBPUCB = false;
                if (iBMPBPUCB > 0)
                {
                    bBMPBPUCB = true;
                }
                chkBMPBPUCB.SetCheckboxOnOff(bBMPBPUCB);
                chkBMPBPUCB.SetBorderWidth(1.0f);
                chkBMPBPUCB.SetSwitchType(2);
                chkBMPBPUCB.SetTag(iBMPBPUCBTagId);
                chkBMPBPUCB.SetCellColour("Pale Yellow");
                chkBMPBPUCBVw = chkBMPBPUCB.GetCheckboxCell();
                UISwitch chkBMPBPUCBCheck = chkBMPBPUCB.GetCheckbox();
                chkBMPBPUCBCheck.ValueChanged += (sender,e) => {CheckboxChanged(sender, e, 1, m_i20MinSection);};

                if(bReadOnly)
                {
                    chkBMPBPUCBCheck.Enabled = false;
                }

                arrItems4[10] = chkBMPBPUCBVw;

                SectionTableRow.AddSubviews(arrItems4);

                iVert+= iEditRowHeight;
                iSectionHeightId += iEditRowHeight;

                /********************************************************************************/
                //              ROW 3 HEADER                                                    //
                /********************************************************************************/
                iUtils.CreateFormGridItem lblCommentsLabel = new iUtils.CreateFormGridItem();
                UIView lblCommentsLabelVw = new UIView();
                lblCommentsLabel.SetDimensions(0f,iVert, 600f, iRowHeight, 2f, 2f, 2f, 2f);
                lblCommentsLabel.SetLabelText("Comments");
                lblCommentsLabel.SetBorderWidth(1.0f);
                lblCommentsLabel.SetFontName("Verdana");
                lblCommentsLabel.SetFontSize(12f);
                lblCommentsLabel.SetTag(iCommentsHdrLabel);
                lblCommentsLabel.SetCellColour("Pale Yellow");
                lblCommentsLabelVw = lblCommentsLabel.GetLabelCell();
                arrItems5 [0] = lblCommentsLabelVw;

                iUtils.CreateFormGridItem lblSummaryLabel = new iUtils.CreateFormGridItem();
                UIView lblSummaryLabelVw = new UIView();
                lblSummaryLabel.SetDimensions(600f,iVert, 400f, iRowHeight, 2f, 2f, 2f, 2f);
                lblSummaryLabel.SetLabelText("Summarise Site Results");
                lblSummaryLabel.SetBorderWidth(1.0f);
                lblSummaryLabel.SetFontName("Verdana");
                lblSummaryLabel.SetFontSize(12f);
                lblSummaryLabel.SetTag(iSummaryHdrLabel);
                lblSummaryLabel.SetCellColour("Pale Yellow");
                lblSummaryLabelVw = lblSummaryLabel.GetLabelCell();
                arrItems5 [1] = lblSummaryLabelVw;


                SectionTableRow.AddSubviews(arrItems5);

                iVert+= iRowHeight;
                iSectionHeightId += iRowHeight;


                /********************************************************************************/
                //              ROW 3 DETAIL                                                    //
                /********************************************************************************/
                iUtils.CreateFormGridItem lblComments = new iUtils.CreateFormGridItem();
                UIView lblCommentsVw = new UIView();
                lblComments.SetDimensions(0f, iVert, 600f, iEditRowHeight * 3, 5f, 3f, 5f, 3f);
                lblComments.SetLabelText(sComments);
                lblComments.SetBorderWidth(1.0f);
                lblComments.SetFontName("Verdana");
                lblComments.SetFontSize(12f);
                lblComments.SetTag(iCommentsTagId);
                lblComments.SetCellColour("Pale Yellow");

                lblCommentsVw = lblComments.GetTextCell();
                UITextView txtCommentsView = lblComments.GetTextView();
                txtCommentsView.AutocorrectionType = UITextAutocorrectionType.No;
                txtCommentsView.KeyboardType = UIKeyboardType.Default;
                txtCommentsView.ReturnKeyType = UIReturnKeyType.Default;
                txtCommentsView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 8);};
                //                txtCommentsView.ShouldEndEditing += (sender) => {
                //                    return sInspectedBy(sender,0);};
                txtCommentsView.Changed += (sender, e) => {
                    return SetCommentsTextChanged(sender, e);};

                if(bReadOnly)
                {
                    txtCommentsView.Editable = false;
                }

                arrItems6 [0] = lblCommentsVw;

                iUtils.CreateFormGridItem lblSumFloatVoltLabel = new iUtils.CreateFormGridItem();
                UIView lblSumFloatVoltLabelVw = new UIView();
                lblSumFloatVoltLabel.SetDimensions(600f,iVert, 200f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSumFloatVoltLabel.SetLabelText("Float Voltage");
                lblSumFloatVoltLabel.SetBorderWidth(1.0f);
                lblSumFloatVoltLabel.SetHideBorder(false, false, true, false);
                lblSumFloatVoltLabel.SetFontName("Verdana");
                lblSumFloatVoltLabel.SetFontSize(12f);
                lblSumFloatVoltLabel.SetTag(iSummaryFloatVoltLabelTagId);
                lblSumFloatVoltLabel.SetCellColour("Pale Yellow");
                lblSumFloatVoltLabelVw = lblSumFloatVoltLabel.GetLabelCell();
                arrItems6 [1] = lblSumFloatVoltLabelVw;

                iUtils.CreateFormGridItem lblSumFloatVoltResult = new iUtils.CreateFormGridItem();
                UIView lblSumFloatVoltResultVw = new UIView();
                lblSumFloatVoltResult.SetDimensions(800f,iVert, 100f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSumFloatVoltResult.SetLabelText(dSumFloatVoltResult.ToString());
                lblSumFloatVoltResult.SetTextAlignment("right");
                lblSumFloatVoltResult.SetHideBorder(true, false, true, false);
                lblSumFloatVoltResult.SetBorderWidth(1.0f);
                lblSumFloatVoltResult.SetFontName("Verdana");
                lblSumFloatVoltResult.SetFontSize(12f);
                lblSumFloatVoltResult.SetTag(iSummaryFloatVoltResultTagId);
                lblSumFloatVoltResult.SetCellColour("Pale Yellow");
                lblSumFloatVoltResultVw = lblSumFloatVoltResult.GetLabelCell();
                arrItems6 [2] = lblSumFloatVoltResultVw;

                iUtils.CreateFormGridItem lblSumFloatVoltUnit = new iUtils.CreateFormGridItem();
                UIView lblSumFloatVoltUnitVw = new UIView();
                lblSumFloatVoltUnit.SetDimensions(900f,iVert, 100f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSumFloatVoltUnit.SetLabelText("V");
                lblSumFloatVoltUnit.SetHideBorder(true, false, false, false);
                lblSumFloatVoltUnit.SetBorderWidth(1.0f);
                lblSumFloatVoltUnit.SetFontName("Verdana");
                lblSumFloatVoltUnit.SetFontSize(12f);
                lblSumFloatVoltUnit.SetTag(iSummaryFloatVoltUnitTagId);

                lblSumFloatVoltUnit.SetCellColour("Pale Yellow");
                lblSumFloatVoltUnitVw = lblSumFloatVoltUnit.GetLabelCell();
                arrItems6 [3] = lblSumFloatVoltUnitVw;

                iVert += iEditRowHeight; 

                iUtils.CreateFormGridItem lblSumLoadCurrentLabel = new iUtils.CreateFormGridItem();
                UIView lblSumLoadCurrentLabelVw = new UIView();
                lblSumLoadCurrentLabel.SetDimensions(600f,iVert, 200f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSumLoadCurrentLabel.SetLabelText("Load Current");
                lblSumLoadCurrentLabel.SetBorderWidth(1.0f);
                lblSumLoadCurrentLabel.SetHideBorder(false, false, true, false);
                lblSumLoadCurrentLabel.SetFontName("Verdana");
                lblSumLoadCurrentLabel.SetFontSize(12f);
                lblSumLoadCurrentLabel.SetTag(iSummaryLoadCurrentLabelTagId);
                lblSumLoadCurrentLabel.SetCellColour("Pale Yellow");
                lblSumLoadCurrentLabelVw = lblSumLoadCurrentLabel.GetLabelCell();
                arrItems6 [4] = lblSumLoadCurrentLabelVw;

                iUtils.CreateFormGridItem lblSumLoadCurrentResult = new iUtils.CreateFormGridItem();
                UIView lblSumLoadCurrentResultVw = new UIView();
                lblSumLoadCurrentResult.SetDimensions(800f,iVert, 100f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSumLoadCurrentResult.SetLabelText(dSumLoadCurrentResult.ToString());
                lblSumLoadCurrentResult.SetTextAlignment("right");
                lblSumLoadCurrentResult.SetHideBorder(true, false, true, false);
                lblSumLoadCurrentResult.SetBorderWidth(1.0f);
                lblSumLoadCurrentResult.SetFontName("Verdana");
                lblSumLoadCurrentResult.SetFontSize(12f);
                lblSumLoadCurrentResult.SetTag(iSummaryLoadCurrentResultTagId);
                lblSumLoadCurrentResult.SetCellColour("Pale Yellow");
                lblSumLoadCurrentResultVw = lblSumLoadCurrentResult.GetLabelCell();
                arrItems6 [5] = lblSumLoadCurrentResultVw;

                iUtils.CreateFormGridItem lblSumLoadCurrentUnit = new iUtils.CreateFormGridItem();
                UIView lblSumLoadCurrentUnitVw = new UIView();
                lblSumLoadCurrentUnit.SetDimensions(900f,iVert, 100f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSumLoadCurrentUnit.SetLabelText("A");
                lblSumLoadCurrentUnit.SetHideBorder(true, false, false, false);
                lblSumLoadCurrentUnit.SetBorderWidth(1.0f);
                lblSumLoadCurrentUnit.SetFontName("Verdana");
                lblSumLoadCurrentUnit.SetFontSize(12f);
                lblSumLoadCurrentUnit.SetTag(iSummaryLoadCurrentUnitTagId);

                lblSumLoadCurrentUnit.SetCellColour("Pale Yellow");
                lblSumLoadCurrentUnitVw = lblSumLoadCurrentUnit.GetLabelCell();
                arrItems6 [6] = lblSumLoadCurrentUnitVw;

                iVert += iEditRowHeight; 

                iUtils.CreateFormGridItem lblSum20MinEndVoltLabel = new iUtils.CreateFormGridItem();
                UIView lblSum20MinEndVoltLabelVw = new UIView();
                lblSum20MinEndVoltLabel.SetDimensions(600f,iVert, 200f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSum20MinEndVoltLabel.SetLabelText("20 Min End of Discharge Voltage");
                lblSum20MinEndVoltLabel.SetBorderWidth(1.0f);
                lblSum20MinEndVoltLabel.SetHideBorder(false, false, true, false);
                lblSum20MinEndVoltLabel.SetFontName("Verdana");
                lblSum20MinEndVoltLabel.SetFontSize(12f);
                lblSum20MinEndVoltLabel.SetTag(iSummary20MinEndVoltLabelTagId);
                lblSum20MinEndVoltLabel.SetCellColour("Pale Yellow");
                lblSum20MinEndVoltLabelVw = lblSum20MinEndVoltLabel.GetLabelCell();
                arrItems6 [7] = lblSum20MinEndVoltLabelVw;

                iUtils.CreateFormGridItem lblSum20MinEndVoltResult = new iUtils.CreateFormGridItem();
                UIView lblSum20MinEndVoltResultVw = new UIView();
                lblSum20MinEndVoltResult.SetDimensions(800f,iVert, 100f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSum20MinEndVoltResult.SetLabelText(dSum20MinEndVoltResult.ToString());
                lblSum20MinEndVoltResult.SetTextAlignment("right");
                lblSum20MinEndVoltResult.SetHideBorder(true, false, true, false);
                lblSum20MinEndVoltResult.SetBorderWidth(1.0f);
                lblSum20MinEndVoltResult.SetFontName("Verdana");
                lblSum20MinEndVoltResult.SetFontSize(12f);
                lblSum20MinEndVoltResult.SetTag(iSummary20MinEndVoltResultTagId);
                lblSum20MinEndVoltResult.SetCellColour("Pale Yellow");
                lblSum20MinEndVoltResultVw = lblSum20MinEndVoltResult.GetLabelCell();
                arrItems6 [8] = lblSum20MinEndVoltResultVw;

                iUtils.CreateFormGridItem lblSum20MinEndVoltUnit = new iUtils.CreateFormGridItem();
                UIView lblSum20MinEndVoltUnitVw = new UIView();
                lblSum20MinEndVoltUnit.SetDimensions(900f,iVert, 100f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblSum20MinEndVoltUnit.SetLabelText("V");
                lblSum20MinEndVoltUnit.SetHideBorder(true, false, false, false);
                lblSum20MinEndVoltUnit.SetBorderWidth(1.0f);
                lblSum20MinEndVoltUnit.SetFontName("Verdana");
                lblSum20MinEndVoltUnit.SetFontSize(12f);
                lblSum20MinEndVoltUnit.SetTag(iSummary20MinEndVoltUnitTagId);

                lblSum20MinEndVoltUnit.SetCellColour("Pale Yellow");
                lblSum20MinEndVoltUnitVw = lblSum20MinEndVoltUnit.GetLabelCell();
                arrItems6 [9] = lblSum20MinEndVoltUnitVw;

                SectionTableRow.AddSubviews(arrItems6);

                iVert += iEditRowHeight; 
                iSectionHeightId += iEditRowHeight * 3;

                layout.AddSubview(SectionTableRow);

                iLayoutVert += iSectionHeightId;

                //this UILabel is created int BuildPwrIdHeader function
                UILabel hfSectionEquipmentHeight = (UILabel)View.ViewWithTag (iSectionHeightTagId * (m_iSections));
                hfSectionEquipmentHeight.Text = iSectionHeightId.ToString();

                //Resize the main frame for the section
                RectangleF frame1 = SectionTableRow.Frame;
                frame1.Height = iSectionHeightId;
                SectionTableRow.Frame = frame1;

                /********************************************************************************/
                //              O/C Volts when Unpacked                                         //
                /********************************************************************************/
                hdrSection2 = BuildSectionHeader(m_iSections, "O/C Voltage When Unpacked", iLayoutVert, ref iSectionHdrRowHeight,1);
                layout.AddSubview(hdrSection2);

                iLayoutVert += iSectionHdrRowHeight;

                UIView SectionTableRow2 = new UIView();
                SectionTableRow2.Frame = new RectangleF(0f,iLayoutVert,1000f,iSectionHdrRowHeight);
                SectionTableRow2.Tag = iContainerSectionTagId * (m_iSections);

                iVert = 0.0f; //Reset for this section
                iSectionHeightId = 0.0f; //Reset for this section

                //Now add in all the cell headings and details
                int iNoCells = Convert.ToInt32(dSystemVoltage / (ITPCellInfo.GetBatteryBlockNoOfCells(m_SPN) * dDefaultVoltage));
                DataSet dsUnpacked = ITPBattTest.GetBatteryAcceptTestType2Records(m_sPassedId, m_PwrId, m_BankNo, ITPBattTest.sITPBatteryAcceptTestUnpackedTableName);
                UIView vwOCUnpacked = BuildType2Row(iVert, 1, m_iSections,dsUnpacked,iNoCells, bReadOnly, "Pale Yellow", ref iHeightToAdd);
                SectionTableRow2.AddSubview(vwOCUnpacked);
                layout.AddSubview(SectionTableRow2);

                iVert += iHeightToAdd;
                iSectionHeightId += iHeightToAdd;
                iLayoutVert += iHeightToAdd;

                //this UILabel is created int BuildPwrIdHeader function
                UILabel hfSectionEquipmentHeight2 = (UILabel)View.ViewWithTag (iSectionHeightTagId * (m_iSections));
                hfSectionEquipmentHeight2.Text = iSectionHeightId.ToString();

                //Resize the main frame for the section
                RectangleF frame2 = SectionTableRow2.Frame;
                frame2.Height = iSectionHeightId;
                SectionTableRow2.Frame = frame2;

                /********************************************************************************/
                //              Float Voltage                                                   //
                /********************************************************************************/
                iThisHdrRowHeight = 40f;
                hdrSection3 = BuildSectionHeader(m_iSections, "Cell/Float Record (Float batteries for 24 Hrs and take reading while still connected to charger)", iLayoutVert, ref iThisHdrRowHeight,1);
                layout.AddSubview(hdrSection3);

                iLayoutVert += iThisHdrRowHeight;

                UIView SectionTableRow3 = new UIView();
                SectionTableRow3.Frame = new RectangleF(0f,iLayoutVert,1000f,iThisHdrRowHeight);
                SectionTableRow3.Tag = iContainerSectionTagId * (m_iSections);

                iVert = 0.0f; //Reset for this section
                iSectionHeightId = 0.0f; //Reset for this section

                hdrSubSection3 = BuildSubHeader("Float Voltage [V]", "Pale Yellow", 960f, iVert);
                SectionTableRow3.AddSubview(hdrSubSection3);
                iVert += iSectionHdrRowHeight;
                iSectionHeightId += iSectionHdrRowHeight;
                iLayoutVert += iSectionHdrRowHeight;

                //Now add in all the cell headings and details
                DataSet dsFloatVolts = ITPBattTest.GetBatteryAcceptTestType2Records(m_sPassedId, m_PwrId, m_BankNo, ITPBattTest.sITPBatteryAcceptTestFloatRecordTableName);
                UIView vwFloatVolts = BuildType2Row(iVert, 2, m_iSections,dsFloatVolts,iNoCells, bReadOnly, "Pale Yellow", ref iHeightToAdd);
                SectionTableRow3.AddSubview(vwFloatVolts);

                iVert += iHeightToAdd;
                iSectionHeightId += iHeightToAdd;
                iLayoutVert += iHeightToAdd;

                hdrSubSection4 = BuildSubHeader("O/C Voltage 0.5 Hr [V]", "Pale Orange", 960f, iVert);
                SectionTableRow3.AddSubview(hdrSubSection4);
                iVert += iSectionHdrRowHeight;
                iSectionHeightId += iSectionHdrRowHeight;
                iLayoutVert += iSectionHdrRowHeight;

                //Now add in all the cell headings and details
                DataSet dsOCVolts05 = ITPBattTest.GetBatteryAcceptTestType2Records(m_sPassedId, m_PwrId, m_BankNo, ITPBattTest.sITPBatteryAcceptTestOCVolts05HrTableName);
                UIView vwOCVolts05 = BuildType2Row(iVert, 3, m_iSections,dsOCVolts05,iNoCells, bReadOnly, "Pale Orange", ref iHeightToAdd);
                SectionTableRow3.AddSubview(vwOCVolts05);
                layout.AddSubview(SectionTableRow3);

                iVert += iHeightToAdd;
                iSectionHeightId += iHeightToAdd;
                iLayoutVert += iHeightToAdd;

                //this UILabel is created int BuildPwrIdHeader function
                UILabel hfSectionEquipmentHeight3 = (UILabel)View.ViewWithTag (iSectionHeightTagId * (m_iSections));
                hfSectionEquipmentHeight3.Text = iSectionHeightId.ToString();

                //Resize the main frame for the section
                RectangleF frame3 = SectionTableRow3.Frame;
                frame3.Height = iSectionHeightId;
                SectionTableRow3.Frame = frame3;

                /********************************************************************************/
                //              Short Discharge Tests at 20 Min                                 //
                /********************************************************************************/
                iThisHdrRowHeight = 40f;
                hdrSection5 = BuildSectionHeader(m_iSections, "20 Minute Short Discharge Test. Overall String Voltage: Mandatory to Record All Intervals", iLayoutVert, ref iThisHdrRowHeight,1);
                layout.AddSubview(hdrSection5);

                iLayoutVert += iThisHdrRowHeight;

                UIView SectionTableRow5 = new UIView();
                SectionTableRow5.Frame = new RectangleF(0f,iLayoutVert,1000f,iThisHdrRowHeight);
                SectionTableRow5.Tag = iContainerSectionTagId * (m_iSections);

                iVert = 0.0f; //Reset for this section
                iSectionHeightId = 0.0f; //Reset for this section

                hdrSubSection6 = BuildSubHeader("Discharge Voltage [V] at 20 minutes", "Pale Yellow", 960f, iVert);
                SectionTableRow5.AddSubview(hdrSubSection6);
                iVert += iSectionHdrRowHeight;
                iSectionHeightId += iSectionHdrRowHeight;
                iLayoutVert += iSectionHdrRowHeight;

                //Now add in all the cell headings and details
                DataSet dsDischargeVolts = ITPBattTest.GetBatteryAcceptTestType1Records(m_sPassedId, m_PwrId, m_BankNo, ITPBattTest.sITPBatteryAcceptTestDischargeVoltTableName);
                UIView vwDischargeVolts = BuildType1Row(iVert, 4, m_iSections,dsDischargeVolts, bReadOnly, "Pale Yellow", ref iHeightToAdd);
                SectionTableRow5.AddSubview(vwDischargeVolts);

                iVert += iHeightToAdd;
                iSectionHeightId += iHeightToAdd;
                iLayoutVert += iHeightToAdd;

                hdrSubSection6 = BuildSubHeader("Discharge Current [A] at 20 Minutes", "Pale Orange", 960f, iVert);
                SectionTableRow5.AddSubview(hdrSubSection6);
                iVert += iSectionHdrRowHeight;
                iSectionHeightId += iSectionHdrRowHeight;
                iLayoutVert += iSectionHdrRowHeight;

                //Now add in all the cell headings and details
                DataSet dsDischargeCurrent = ITPBattTest.GetBatteryAcceptTestType1Records(m_sPassedId, m_PwrId, m_BankNo, ITPBattTest.sITPBatteryAcceptTestDischargeCurrentTableName);
                UIView vwDischargeCurrent = BuildType1Row(iVert, 5, m_iSections,dsDischargeCurrent, bReadOnly, "Pale Orange", ref iHeightToAdd);
                SectionTableRow5.AddSubview(vwDischargeCurrent);
                layout.AddSubview(SectionTableRow5);

                iVert += iHeightToAdd;
                iSectionHeightId += iHeightToAdd;
                iLayoutVert += iHeightToAdd;

                //this UILabel is created int BuildPwrIdHeader function
                UILabel hfSectionEquipmentHeight5 = (UILabel)View.ViewWithTag (iSectionHeightTagId * (m_iSections));
                hfSectionEquipmentHeight5.Text = iSectionHeightId.ToString();

                //Resize the main frame for the section
                RectangleF frame5 = SectionTableRow5.Frame;
                frame5.Height = iSectionHeightId;
                SectionTableRow5.Frame = frame5;


                /********************************************************************************/
                //              RESIZING AND HOLDING INFO IN HIDDEN FIELDS                      //
                /********************************************************************************/


                //Resize the scroll frame
                iTotalHeight = iLayoutVert + 300f;
                SizeF layoutSize = new SizeF(1000f, iTotalHeight);
                layout.ContentSize = layoutSize;

                UILabel hfScrollContentHeight = new UILabel();
                hfScrollContentHeight.Text = iTotalHeight.ToString();
                hfScrollContentHeight.Tag = 3;
                hfScrollContentHeight.Hidden = true;
                layout.AddSubview(hfScrollContentHeight);
            }
            catch (Exception except)
            {
                string sTest = except.Message.ToString();
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog (sTest);
            }
        }

        public UIView BuildPwrIdHeader(string sPwrId, int iBankNo)
        {
            float iVert = 0f;
            float iSectionHdrRowHeight = 40f;
            UIView[] arrItems = new UIView[5];

            //Add in the section title and buttons for each section header
            UIView SectionPwrIdRow = new UIView();
            float iSectionPwrIdRowVertTop = iVert;
            SectionPwrIdRow.Frame = new RectangleF(0f,iSectionPwrIdRowVertTop,1000f,iSectionHdrRowHeight);
            SectionPwrIdRow.Tag = iPwrIdHdrRowTagId;


            iUtils.CreateFormGridItem SectionPwrIdLabel = new iUtils.CreateFormGridItem();
            UIView SectionPwrIdLabelVw = new UIView();
            SectionPwrIdLabel.SetDimensions(0f,0f, 100f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            SectionPwrIdLabel.SetLabelText("Power Id");
            SectionPwrIdLabel.SetBorderWidth(0.0f);
            SectionPwrIdLabel.SetFontName("Verdana-Bold");
            SectionPwrIdLabel.SetTextColour("Black");
            SectionPwrIdLabel.SetFontSize(16f);
            SectionPwrIdLabel.SetCellColour("Lavendar");
            SectionPwrIdLabel.SetTag(iPwrIdHdrLabelTagId);
            SectionPwrIdLabelVw = SectionPwrIdLabel.GetLabelCell();
            arrItems[0] = SectionPwrIdLabelVw;

            iUtils.CreateFormGridItem SectionPwrId = new iUtils.CreateFormGridItem();
            UIView SectionPwrIdVw = new UIView();
            SectionPwrId.SetDimensions(100f,0f, 250f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            SectionPwrId.SetLabelText(sPwrId);
            SectionPwrId.SetBorderWidth(0.0f);
            SectionPwrId.SetFontName("Verdana-Bold");
            SectionPwrId.SetTextColour("Black");
            SectionPwrId.SetFontSize(16f);
            SectionPwrId.SetCellColour("Lavendar");
            SectionPwrId.SetTag(iPwrIdHdrTagId);
            SectionPwrIdVw = SectionPwrId.GetLabelCell();
            arrItems[1] = SectionPwrIdVw;

            iUtils.CreateFormGridItem SectionBankNoLabel = new iUtils.CreateFormGridItem();
            UIView SectionBankNoLabelVw = new UIView();
            SectionBankNoLabel.SetDimensions(350f,0f, 100f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            SectionBankNoLabel.SetLabelText("Bank No");
            SectionBankNoLabel.SetBorderWidth(0.0f);
            SectionBankNoLabel.SetFontName("Verdana-Bold");
            SectionBankNoLabel.SetTextColour("Black");
            SectionBankNoLabel.SetFontSize(16f);
            SectionBankNoLabel.SetCellColour("Lavendar");
            SectionBankNoLabel.SetTag(iBankNoHdrLabelTagId);
            SectionBankNoLabelVw = SectionBankNoLabel.GetLabelCell();
            arrItems[2] = SectionBankNoLabelVw;

            iUtils.CreateFormGridItem SectionBankNo = new iUtils.CreateFormGridItem();
            UIView SectionBankNoVw = new UIView();
            SectionBankNo.SetDimensions(450f,0f, 150f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            SectionBankNo.SetLabelText(iBankNo.ToString());
            SectionBankNo.SetBorderWidth(0.0f);
            SectionBankNo.SetFontName("Verdana-Bold");
            SectionBankNo.SetTextColour("Black");
            SectionBankNo.SetFontSize(16f);
            SectionBankNo.SetCellColour("Lavendar");
            SectionBankNo.SetTag(iBankNoHdrTagId);
            SectionBankNoVw = SectionBankNo.GetLabelCell();
            arrItems[3] = SectionBankNoVw;

            iUtils.CreateFormGridItem SectionBlank = new iUtils.CreateFormGridItem();
            UIView SectionBlankVw = new UIView();
            SectionBlank.SetDimensions(600f,0f, 400f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            SectionBlank.SetBorderWidth(0.0f);
            SectionBlank.SetFontName("Verdana-Bold");
            SectionBlank.SetTextColour("Black");
            SectionBlank.SetFontSize(16f);
            SectionBlank.SetCellColour("Lavendar");
            SectionBlank.SetTag(-1);
            SectionBlankVw = SectionBlank.GetLabelCell();
            arrItems[4] = SectionBlankVw;

            SectionPwrIdRow.AddSubviews(arrItems);

            return SectionPwrIdRow;
        }

        public UIView BuildSubHeader(string sHeaderDescription, string sColour, float iWidth, float iVertPositionWithinSection)
        {
            float iSectionHdrRowHeight = 40f;
            UIView[] arrItems = new UIView[1];

            //Add in the section title and buttons for each section header
            UIView SectionSubHdrRow = new UIView();
            SectionSubHdrRow.Frame = new RectangleF(0f,iVertPositionWithinSection,iWidth,iSectionHdrRowHeight);
            SectionSubHdrRow.Tag = iPwrIdHdrRowTagId;


            iUtils.CreateFormGridItem SectionPwrIdLabel = new iUtils.CreateFormGridItem();
            UIView SectionPwrIdLabelVw = new UIView();
            SectionPwrIdLabel.SetDimensions(0f,0f, iWidth, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            SectionPwrIdLabel.SetLabelText(sHeaderDescription);
            SectionPwrIdLabel.SetBorderWidth(0.0f);
            SectionPwrIdLabel.SetFontName("Verdana-Bold");
            SectionPwrIdLabel.SetTextColour("Black");
            SectionPwrIdLabel.SetFontSize(16f);
            SectionPwrIdLabel.SetCellColour(sColour);
            SectionPwrIdLabel.SetTag(iPwrIdHdrLabelTagId);
            SectionPwrIdLabelVw = SectionPwrIdLabel.GetLabelCell();
            arrItems[0] = SectionPwrIdLabelVw;

            SectionSubHdrRow.AddSubviews(arrItems);

            return SectionSubHdrRow;
        }

        public UIView BuildSectionHeader(int iSectionId, string sSectionDesc, float iVertPosition, ref float iSectionHdrRowHeight, int iRows)
        {
            float iVert = iVertPosition;
            UIView[] arrItems4 = new UIView[9];
            bool bFullyCommitted = false;
            bool bHideComplete = false;

            //Add in the section title and buttons for each section header
            UIView SectionEquipmentRow = new UIView();
            float iSectionEquipmentRowVertTop = iVert;
            SectionEquipmentRow.Frame = new RectangleF(0f,iSectionEquipmentRowVertTop,1000f,iSectionHdrRowHeight);
            int iSectionRowId = iSectionTagId * (iSectionId+1);
            SectionEquipmentRow.Tag = iSectionRowId;
//            layout.AddSubview(SectionEquipmentRow);

            UILabel hfSectionEquipment = new UILabel();
            hfSectionEquipment.Text = iSectionId.ToString();
            hfSectionEquipment.Tag = iSectionDBIdTagId * (iSectionId+1);
            hfSectionEquipment.Hidden = true;
            SectionEquipmentRow.AddSubview(hfSectionEquipment);

            UILabel hfSectionCounter = new UILabel();
            hfSectionCounter.Text = (iSectionId + 1).ToString();
            hfSectionCounter.Tag = iSectionCounterTagId * (iSectionId + 1);
            hfSectionCounter.Hidden = true;
            arrItems4[0] = hfSectionCounter;

            iUtils.CreateFormGridItem SectionEquipment = new iUtils.CreateFormGridItem();
            UIView SectionEquipmentVw = new UIView();
            SectionEquipment.SetDimensions(0f,0f, 650f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            SectionEquipment.SetLabelText(sSectionDesc);
            SectionEquipment.SetLabelWrap(1);
            SectionEquipment.SetBorderWidth(0.0f);
            SectionEquipment.SetFontName("Verdana-Bold");
            SectionEquipment.SetTextColour("White");
            SectionEquipment.SetFontSize(12f);
            SectionEquipment.SetCellColour("DarkSlateGrey");
            SectionEquipment.SetTag(iSectionDescTagId * (iSectionId+1));
            SectionEquipmentVw = SectionEquipment.GetLabelCell();
            iSectionHdrRowHeight = SectionEquipment.GetCellHeight();
            arrItems4[1] = SectionEquipmentVw;

            if(SectionFullyCommitted(iSectionId))
            {
                bFullyCommitted = true;
                bHideComplete = false;
            }
            else
            {
                bFullyCommitted = false;
                if(SectionFullyComplete(iSectionId))
                {
                    bHideComplete = false;
                }
                else
                {
                    bHideComplete = true;
                }
            }

            iUtils.CreateFormGridItem SectionCompleteLabel = new iUtils.CreateFormGridItem();
            UIView SectionCompleteLabelVw = new UIView();
            SectionCompleteLabel.SetDimensions(650f,0f, 100f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            if(bFullyCommitted)
            {
                SectionCompleteLabel.SetLabelText("COMMITTED");
            }
            else
            {
                SectionCompleteLabel.SetLabelText("COMPLETED");
            }
            SectionCompleteLabel.SetBorderWidth(0.0f);
            SectionCompleteLabel.SetFontName("Verdana-Bold");
            SectionCompleteLabel.SetTextColour("Bright Yellow");
            SectionCompleteLabel.SetFontSize(12f);
            SectionCompleteLabel.SetCellColour("DarkSlateGrey");
            SectionCompleteLabel.SetTag(iSectionCompleteLabelTagId * (iSectionId+1));
            SectionCompleteLabel.SetHidden(bHideComplete);
            SectionCompleteLabelVw = SectionCompleteLabel.GetLabelCell();
            arrItems4[2] = SectionCompleteLabelVw;

            iUtils.CreateFormGridItem btnSaveEquipment = new iUtils.CreateFormGridItem();
            UIView btnSaveEquipmentVw = new UIView();
            btnSaveEquipment.SetDimensions(750f,0f, 150f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
            btnSaveEquipment.SetLabelText("Save Section");
            btnSaveEquipment.SetBorderWidth(0.0f);
            btnSaveEquipment.SetFontName("Verdana");
            btnSaveEquipment.SetFontSize(12f);
            btnSaveEquipment.SetHidden(true);
            btnSaveEquipment.SetTag(iSaveSectionBtnTagId * (iSectionId+1));
            btnSaveEquipment.SetCellColour("DarkSlateGrey");
            btnSaveEquipmentVw = btnSaveEquipment.GetButtonCell();

            UIButton btnSaveEquipmentButton = new UIButton();
            btnSaveEquipmentButton = btnSaveEquipment.GetButton();
            btnSaveEquipmentButton.TouchUpInside += (sender,e) => {SaveThisSection(sender, e);};

            arrItems4[3] = btnSaveEquipmentVw;

            iUtils.CreateFormGridItem btnExpandEquipment = new iUtils.CreateFormGridItem();
            UIView btnExpandEquipmentVw = new UIView();
            btnExpandEquipment.SetDimensions(900f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
            btnExpandEquipment.SetLabelText("+");
            btnExpandEquipment.SetBorderWidth(0.0f);
            btnExpandEquipment.SetFontName("Verdana");
            btnExpandEquipment.SetFontSize(12f);
            btnExpandEquipment.SetTag(iExpandSectionBtnTagId * (iSectionId+1));
            btnExpandEquipment.SetCellColour("DarkSlateGrey");
            btnExpandEquipmentVw = btnExpandEquipment.GetButtonCell();

            UIButton btnExpandEquipmentButton = new UIButton();
            btnExpandEquipmentButton = btnExpandEquipment.GetButton();
            btnExpandEquipmentButton.Enabled = false;
            btnExpandEquipmentButton.TouchUpInside += (sender,e) => {ExpandSection(sender, e);};

            arrItems4[4] = btnExpandEquipmentVw;

            iUtils.CreateFormGridItem btnContractEquipment = new iUtils.CreateFormGridItem();
            UIView btnContractEquipmentVw = new UIView();
            btnContractEquipment.SetDimensions(950f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
            btnContractEquipment.SetLabelText("-");
            btnContractEquipment.SetBorderWidth(0.0f);
            btnContractEquipment.SetFontName("Verdana");
            btnContractEquipment.SetFontSize(12f);
            btnContractEquipment.SetTag(iContractSectionBtnTagId * (iSectionId+1));
            btnContractEquipment.SetCellColour("DarkSlateGrey");
            btnContractEquipmentVw = btnContractEquipment.GetButtonCell();

            UIButton btnContractEquipmentButton = new UIButton();
            btnContractEquipmentButton = btnContractEquipment.GetButton();
            btnContractEquipmentButton.TouchUpInside += (sender,e) => {ContractSection(sender, e);};

            arrItems4[5] = btnContractEquipmentVw;

            UILabel hfSectionEquipmentHeight = new UILabel();
            hfSectionEquipmentHeight.Tag = iSectionHeightTagId * (iSectionId+1);
            hfSectionEquipmentHeight.Hidden = true;
            hfSectionEquipmentHeight.Text = "0";
            arrItems4[6] = hfSectionEquipmentHeight;

            UILabel hfSectionEquipmentRows = new UILabel();
            hfSectionEquipmentRows.Tag = iSectionRowsTagId * (iSectionId+1);
            hfSectionEquipmentRows.Hidden = true;
            hfSectionEquipmentRows.Text = iRows.ToString();
            arrItems4[7] = hfSectionEquipmentRows;

            UILabel hfSectionEquipmentStatus = new UILabel();
            hfSectionEquipmentStatus.Tag = iSectionStatusTagId * (iSectionId+1);
            hfSectionEquipmentStatus.Hidden = true;
            hfSectionEquipmentStatus.Text = "0";
            arrItems4[8] = hfSectionEquipmentStatus;


            SectionEquipmentRow.AddSubviews(arrItems4);

            m_iSections++;

            return SectionEquipmentRow;
        }

        public UIView BuildType1Row(float iSectionVertical, int iUniqueRowId, int iSectionId, DataSet ds, bool bReadOnly, string sColour, ref float iTotalRowHeight)
        {
            int iNoOfCells = 24; //Here the cell count is text box cells not battery cells (just using the same variable name for convenience)
            int i;
            int j;
            float iHrdRowHeight = 20f;
            float iEditRowHeight = 40f;
            UIView[] arrItems = new UIView[3];
            float iVert = 0.0f;
            UIView vwContainer = new UIView();
            int iColNo = 0;
            double dCellValue = 0.0;
            string sCellValue = "";
            clsLocalUtils clsUtil = new clsLocalUtils();

            string[] sCellColumn = new string[24];
            sCellColumn [0] = "Sec0";
            sCellColumn [1] = "Sec15";
            sCellColumn [2] = "Sec30";
            sCellColumn [3] = "Sec45";
            sCellColumn [4] = "Min1";
            sCellColumn [5] = "Min1_15";
            sCellColumn [6] = "Min1_30";
            sCellColumn [7] = "Min1_45";
            sCellColumn [8] = "Min2";
            sCellColumn [9] = "Min2_15";
            sCellColumn [10] = "Min2_30";
            sCellColumn [11] = "Min2_45";
            sCellColumn [12] = "Min3";
            sCellColumn [13] = "Min4";
            sCellColumn [14] = "Min5";
            sCellColumn [15] = "Min6";
            sCellColumn [16] = "Min7";
            sCellColumn [17] = "Min8";
            sCellColumn [18] = "Min10";
            sCellColumn [19] = "Min12";
            sCellColumn [20] = "Min14";
            sCellColumn [21] = "Min16";
            sCellColumn [22] = "Min18";
            sCellColumn [23] = "Min20";

            string[] sCellHeader = new string[24];
            sCellHeader [0] = "0 sec";
            sCellHeader [1] = "15 sec";
            sCellHeader [2] = "30 sec";
            sCellHeader [3] = "45 sec";
            sCellHeader [4] = "1 min";
            sCellHeader [5] = "1 m 15 s";
            sCellHeader [6] = "1 min 30 s";
            sCellHeader [7] = "1 min 45 s";
            sCellHeader [8] = "2 min";
            sCellHeader [9] = "2 m 15 s";
            sCellHeader [10] = "2 m 30 s";
            sCellHeader [11] = "2 m 45 s";
            sCellHeader [12] = "3 min";
            sCellHeader [13] = "4 min";
            sCellHeader [14] = "5 min";
            sCellHeader [15] = "6 min";
            sCellHeader [16] = "7 min";
            sCellHeader [17] = "8 min";
            sCellHeader [18] = "10 min";
            sCellHeader [19] = "12 min";
            sCellHeader [20] = "14 min";
            sCellHeader [21] = "16 min";
            sCellHeader [22] = "18 min";
            sCellHeader [23] = "20 min";

            for (i=0; i<iNoOfCells; i++)
            {
                j = i;
                if (i == 12)
                {
                    iVert += iHrdRowHeight + iEditRowHeight;
                }

                if (i >= 12)
                {
                    j = i - 12;
                }

                iUtils.CreateFormGridItem lblCellLabel = new iUtils.CreateFormGridItem();
                UIView lblCellLabelVw = new UIView();
                lblCellLabel.SetDimensions(80f * j,iVert, 80f, iHrdRowHeight, 2f, 2f, 2f, 2f);
                lblCellLabel.SetLabelText(sCellHeader[i]);
                lblCellLabel.SetBorderWidth(1.0f);
                lblCellLabel.SetHideBorder(false, false, true, false);
                lblCellLabel.SetFontName("Verdana");
                lblCellLabel.SetFontSize(12f);
                lblCellLabel.SetTag((iType1234CellBaseLabelId * iUniqueRowId) + (i+1));
                lblCellLabel.SetCellColour(sColour);
                lblCellLabelVw = lblCellLabel.GetLabelCell();
                arrItems [0] = lblCellLabelVw;

                iColNo = ds.Tables[0].Columns[sCellColumn[i]].Ordinal;
                sCellValue = ds.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                if(clsUtil.IsNumeric(sCellValue))
                {
                    dCellValue = Convert.ToDouble(sCellValue);
                }
                else
                {
                    dCellValue = 0.0;
                }

                iUtils.CreateFormGridItem lblCell = new iUtils.CreateFormGridItem();
                UIView lblCellVw = new UIView();
                lblCell.SetDimensions(80f * j, iVert + iHrdRowHeight, 80f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblCell.SetLabelText(dCellValue.ToString());
                lblCell.SetBorderWidth(1.0f);
                lblCell.SetFontName("Verdana");
                lblCell.SetFontSize(12f);
                lblCell.SetTag((iType1234CellBaseId * iUniqueRowId) + (i+1));
                lblCell.SetCellColour(sColour);

                lblCellVw = lblCell.GetTextFieldCell();
                UITextField txtCellView = lblCell.GetTextFieldView();
                txtCellView.AutocorrectionType = UITextAutocorrectionType.No;
                txtCellView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtCellView.ReturnKeyType = UIReturnKeyType.Next;
                txtCellView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, (100 * iSectionId));};
                txtCellView.ShouldEndEditing += (sender) => {
                    return ValidateNumberOnly(sender,0, iSectionId - 1);};
                txtCellView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, (100 * iUniqueRowId));};

                if(bReadOnly)
                {
                    txtCellView.Enabled = false;
                }

                arrItems[1] = lblCellVw;

                UILabel hfCurrentCell = new UILabel();
                hfCurrentCell.Text = dCellValue.ToString();
                hfCurrentCell.Tag = (iType1234CellBaseId * iUniqueRowId) + (i+1) + (iType1234CellBaseHiddenId - iType1234CellBaseId);
                hfCurrentCell.Hidden = true;
                arrItems[2] = hfCurrentCell;

                vwContainer.AddSubviews(arrItems);

            }

            iTotalRowHeight = iVert + iHrdRowHeight + iEditRowHeight;
            vwContainer.Frame = new RectangleF(0f,iSectionVertical,1000f,iTotalRowHeight);
            vwContainer.Tag = iType1234CellContainerTagId * iUniqueRowId;
            return vwContainer;
        }

        public UIView BuildType2Row(float iSectionVertical, int iUniqueRowId, int iSectionId, DataSet ds, int iNoOfCells, bool bReadOnly, string sColour, ref float iTotalRowHeight)
        {
            int i;
            int j;
            float iHrdRowHeight = 20f;
            float iEditRowHeight = 40f;
            UIView[] arrItems = new UIView[3];
            float iVert = 0.0f;
            UIView vwContainer = new UIView();
            int iColNo = 0;
            string sCellNo = "";
            double dCellValue = 0.0;
            string sCellValue = "";
            clsLocalUtils clsUtil = new clsLocalUtils();

            for (i=0; i<iNoOfCells; i++)
            {
                sCellNo = (i + 1).ToString();
                j = i;
                if (i == 12)
                {
                    iVert += iHrdRowHeight + iEditRowHeight;
                }

                if (i >= 12)
                {
                    j = i - 12;
                }

                iUtils.CreateFormGridItem lblCellLabel = new iUtils.CreateFormGridItem();
                UIView lblCellLabelVw = new UIView();
                lblCellLabel.SetDimensions(80f * j,iVert, 80f, iHrdRowHeight, 2f, 2f, 2f, 2f);
                lblCellLabel.SetLabelText(sCellNo);
                lblCellLabel.SetBorderWidth(1.0f);
                lblCellLabel.SetHideBorder(false, false, true, false);
                lblCellLabel.SetFontName("Verdana");
                lblCellLabel.SetFontSize(12f);
                lblCellLabel.SetTag((iType1234CellBaseLabelId * iUniqueRowId) + (i+1));
                lblCellLabel.SetCellColour(sColour);
                lblCellLabelVw = lblCellLabel.GetLabelCell();
                arrItems [0] = lblCellLabelVw;

                iColNo = ds.Tables[0].Columns["Cell" + sCellNo].Ordinal;
                sCellValue = ds.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                if(clsUtil.IsNumeric(sCellValue))
                {
                    dCellValue = Convert.ToDouble(sCellValue);
                }
                else
                {
                    dCellValue = 0.0;
                }

                iUtils.CreateFormGridItem lblCell = new iUtils.CreateFormGridItem();
                UIView lblCellVw = new UIView();
                lblCell.SetDimensions(80f * j, iVert + iHrdRowHeight, 80f, iEditRowHeight, 5f, 3f, 5f, 3f);
                lblCell.SetLabelText(dCellValue.ToString());
                lblCell.SetBorderWidth(1.0f);
                lblCell.SetFontName("Verdana");
                lblCell.SetFontSize(12f);
                lblCell.SetTag((iType1234CellBaseId * iUniqueRowId) + (i+1));
                lblCell.SetCellColour(sColour);

                lblCellVw = lblCell.GetTextFieldCell();
                UITextField txtCellView = lblCell.GetTextFieldView();
                txtCellView.AutocorrectionType = UITextAutocorrectionType.No;
                txtCellView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtCellView.ReturnKeyType = UIReturnKeyType.Next;
                txtCellView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, (100 * iSectionId));};
                txtCellView.ShouldEndEditing += (sender) => {
                    return ValidateNumberOnly(sender,0, iSectionId - 1);};
                txtCellView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, (100 * iUniqueRowId));};

                if(bReadOnly)
                {
                    txtCellView.Enabled = false;
                }

                arrItems[1] = lblCellVw;

                UILabel hfCurrentCell = new UILabel();
                hfCurrentCell.Text = dCellValue.ToString();
                hfCurrentCell.Tag = (iType1234CellBaseId * iUniqueRowId) + (i+1) + (iType1234CellBaseHiddenId - iType1234CellBaseId);
                hfCurrentCell.Hidden = true;
                arrItems[2] = hfCurrentCell;

                vwContainer.AddSubviews(arrItems);

            }

            iTotalRowHeight = iVert + iHrdRowHeight + iEditRowHeight;
            vwContainer.Frame = new RectangleF(0f,iSectionVertical,1000f,iTotalRowHeight);
            vwContainer.Tag = iType1234CellContainerTagId * iUniqueRowId;
            return vwContainer;
        }

        public void OpenCellMbVoltageList (object sender, EventArgs e)
        {
            UIButton btnMakeSearch = (UIButton)sender;
            btnMakeSearch.Enabled = false;
            ScreenUtils scnUtils = new ScreenUtils ();
            scnUtils.GetAbsolutePosition (btnMakeSearch);
            float iTop = scnUtils.GetPositionTop ();
            float iLeft = scnUtils.GetPositionLeft ();

            List<string> mylist = new List<string> ();
            clsTabletDB.ITPBatteryCellInfo ITPCellInfo = new clsTabletDB.ITPBatteryCellInfo ();
            string[] sCellMbVoltages;
            sCellMbVoltages = ITPCellInfo.GetBatteryBlockVoltages();
            m_sCellMbVoltages = sCellMbVoltages;
            Array.ForEach (m_sCellMbVoltages, value => mylist.Add (value.ToString ()));

            TableViewSource tabdata = new TableViewSource (mylist, true);
            tabdata.SetFont("Verdana",10f);
            UITableView cmbCellMbVoltages = new UITableView ();

            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                cmbCellMbVoltages.Frame = new RectangleF(iLeft, iTop - 190f, 290f, 200f);
            } 
            else 
            {
                cmbCellMbVoltages.Frame = new RectangleF(iLeft, iTop, 290f, 200f);
            }

            tabdata.SetParent(cmbCellMbVoltages);
            tabdata.SetUpdateFieldType("UILabel");
            UILabel txtVwUpdate = (UILabel)View.ViewWithTag (iCellMbVoltageTagId);
            tabdata.SetLabelViewToUpdate(txtVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);

            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UIButton btnSectionSave = (UIButton)View.ViewWithTag ((m_i20MinSection + 1) * iSaveSectionBtnTagId);
            tabdata.SetSectionSaveButton(btnSectionSave);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((m_i20MinSection + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);

            cmbCellMbVoltages.Source = tabdata;
            iUtils.SESTable thistable = new iUtils.SESTable();
            string sSelectedValue = txtVwUpdate.Text;
            thistable.SetTableSelectedText(cmbCellMbVoltages, sSelectedValue, m_sCellMbVoltages, true);

            //Get the main scroll view
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            scrollVw.AddSubview(cmbCellMbVoltages);
        }

        public void OpenCellMbPostList (object sender, EventArgs e)
        {
            UIButton btnMakeSearch = (UIButton)sender;
            btnMakeSearch.Enabled = false;
            ScreenUtils scnUtils = new ScreenUtils ();
            scnUtils.GetAbsolutePosition (btnMakeSearch);
            float iTop = scnUtils.GetPositionTop ();
            float iLeft = scnUtils.GetPositionLeft ();

            List<string> mylist = new List<string> ();
            clsTabletDB.ITPBatteryCellInfo ITPCellInfo = new clsTabletDB.ITPBatteryCellInfo ();
            string[] sCellMbPosts;
            sCellMbPosts = ITPCellInfo.GetBatteryBlockVoltages();
            m_sCellMbPost = sCellMbPosts;
            Array.ForEach (m_sCellMbPost, value => mylist.Add (value.ToString ()));

            TableViewSource tabdata = new TableViewSource (mylist, true);
            tabdata.SetFont("Verdana",10f);
            UITableView cmbCellMbPosts = new UITableView ();

            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                cmbCellMbPosts.Frame = new RectangleF(iLeft, iTop - 190f, 290f, 200f);
            } 
            else 
            {
                cmbCellMbPosts.Frame = new RectangleF(iLeft, iTop, 290f, 200f);
            }

            tabdata.SetParent(cmbCellMbPosts);
            tabdata.SetUpdateFieldType("UILabel");
            UILabel txtVwUpdate = (UILabel)View.ViewWithTag (iCellMbPostTagId);
            tabdata.SetLabelViewToUpdate(txtVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);

            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UIButton btnSectionSave = (UIButton)View.ViewWithTag ((m_i20MinSection + 1) * iSaveSectionBtnTagId);
            tabdata.SetSectionSaveButton(btnSectionSave);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((m_i20MinSection + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);

            cmbCellMbPosts.Source = tabdata;
            iUtils.SESTable thistable = new iUtils.SESTable();
            string sSelectedValue = txtVwUpdate.Text;
            thistable.SetTableSelectedText(cmbCellMbPosts, sSelectedValue, m_sCellMbPost, true);

            //Get the main scroll view
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            scrollVw.AddSubview(cmbCellMbPosts);
        }

        public void CheckUnsaved ()
        {
        int iSectionId = -1;

            if (m_iValidateType >= 100 && m_iValidateType <= 5000)
            {
                iSectionId = m_iValidateType / 100;
                m_iValidateType = 100;
            }

            //First of all validate anything required
            switch(m_iValidateType)
            {
                case 1: //Inspected By (no validation required)
                    break;
                case 2: //Inspect Date
                    if(!ValidateInspectDate(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 3: //Test Date
                    if(!ValidateTestDate(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 4: //Float Voltage Prior
                case 5: //Charge Period Prior
                case 6: //Battery Capacity
                    if(!ValidateNumberOnly(m_sender, 1, m_i20MinSection))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 100: //Cell type items
                    if(!ValidateNumberOnly(m_sender, 1, iSectionId))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;

            }

            UILabel txtEditStatus = (UILabel)View.ViewWithTag (80);
            int iStatus = Convert.ToInt32 (txtEditStatus.Text);
            if (iStatus == 0) 
            {
                Battery BatteryScreen = new Battery ();
                BatteryScreen = GetBatteryPage ();
                this.NavigationController.PopToViewController (BatteryScreen, true);
            } 
            else 
            {
                //Ask the question
                iUtils.AlertBox alert2 = new iUtils.AlertBox();
                alert2.CreateAlertYesNoCancelDialog();
                alert2.SetAlertMessage("You have unsaved changes. Do you wish to save these changes before going back to the downloaded screen?");
                alert2.ShowAlertBox(); 

                UIAlertView alert3 = alert2.GetAlertDialog();
                alert3.Clicked += (sender2, e2)  => {CheckSaveChangesQuestion(sender2, e2, e2.ButtonIndex);}; 

            }

        }

        public void CheckSaveChangesQuestion (object sender, EventArgs e, int iBtnIndex)
        {
            Battery BatteryScreen = new Battery ();
            BatteryScreen = GetBatteryPage ();
            switch (iBtnIndex) 
            {
                case 0:
                    SaveAllSections();
                    this.NavigationController.PopToViewController (BatteryScreen, true);
                    break;
                case 1:
                    this.NavigationController.PopToViewController (BatteryScreen, true);
                    break;
                case 2:
                    break;
            }
        }

        public bool SetGlobalEditItems(object sender, int iType)
        {
            m_sender = sender;
            m_iValidateType = iType;
            return true;
        }

        public bool ValidateInspectDate (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }

            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }

            UITextField txtInspectDate = (UITextField)sender;
            string sInspectDate = txtInspectDate.Text;
            bool bDateCheck;

//            int iTagId = txtInspectDate.Tag;
            int iHiddenInspectDateId = iInspectedDateHiddenTagId;
            UILabel hfHiddenInspectDate = (UILabel)View.ViewWithTag (iHiddenInspectDateId);

            if(sInspectDate == "")
            {
                UILabel hfHeaderStatus = (UILabel)View.ViewWithTag(iHeaderRowStatusTagId);
                hfHeaderStatus.Text = "1";
                SetSectionValueChanged(m_i20MinSection + 1);
                SetAnyValueChanged(sender, null, m_i20MinSection);
                return true;
            }

            DateClass dt = new DateClass ();
            DateTime dtInspectDate;
            bDateCheck = dt.ValidateDate (sInspectDate, ref dtInspectDate);

            if (!bDateCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid date for the inspection date");
                txtInspectDate.ResignFirstResponder();
                txtInspectDate.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                string sInspectDateReturn = dt.Get_Date_String(dtInspectDate, "dd/mm/yy");
                txtInspectDate.Text = sInspectDateReturn;
                if(hfHiddenInspectDate.Text != sInspectDateReturn)
                {
                    hfHiddenInspectDate.Text = sInspectDateReturn;
                    UILabel hfHeaderStatus = (UILabel)View.ViewWithTag(iHeaderRowStatusTagId);
                    hfHeaderStatus.Text = "1";
                    SetSectionValueChanged(m_i20MinSection + 1);
                    SetAnyValueChanged(sender, null, m_i20MinSection);
                }
                return true;
            }
        }

        public bool ValidateTestDate (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }

            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }

            UITextField txtTestDate = (UITextField)sender;
            string sTestDate = txtTestDate.Text;
            bool bDateCheck;

            //            int iTagId = txtTestDate.Tag;
            int iHiddenTestDateId = iTestDateHiddenTagId;
            UILabel hfHiddenTestDate = (UILabel)View.ViewWithTag (iHiddenTestDateId);

            if(sTestDate == "")
            {
                UILabel hfHeaderStatus = (UILabel)View.ViewWithTag(iHeaderRowStatusTagId);
                hfHeaderStatus.Text = "1";
                SetSectionValueChanged(m_i20MinSection + 1);
                SetAnyValueChanged(sender, null, m_i20MinSection);
                return true;
            }

            DateClass dt = new DateClass ();
            DateTime dtTestDate;
            bDateCheck = dt.ValidateDate (sTestDate, ref dtTestDate);

            if (!bDateCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid date for the test date");
                txtTestDate.ResignFirstResponder();
                txtTestDate.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                string sTestDateReturn = dt.Get_Date_String(dtTestDate, "dd/mm/yy");
                txtTestDate.Text = sTestDateReturn;
                if(hfHiddenTestDate.Text != sTestDateReturn)
                {
                    hfHiddenTestDate.Text = sTestDateReturn;
                    UILabel hfHeaderStatus = (UILabel)View.ViewWithTag(iHeaderRowStatusTagId);
                    hfHeaderStatus.Text = "1";
                    SetSectionValueChanged(m_i20MinSection + 1);
                    SetAnyValueChanged(sender, null, m_i20MinSection);
                }
                return true;
            }
        }

        public void SetCommentsTextChanged(object sender, EventArgs e)
        {
            UITextView edtText = (UITextView)sender;
            int iSection =  m_i20MinSection;
            SetSectionValueChanged(iSection + 1);
            SetAnyValueChanged(sender, null, iSection);
        }

        public bool ValidateNumberOnly(object sender, int iFromBackButton, int iSectionId)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }

            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }

            UITextField txtNumberField = (UITextField)sender;
            string sNumberField = txtNumberField.Text;
            string sNumberReturn = Regex.Replace(sNumberField, @"[^0-9\.]+","");
            txtNumberField.Text = sNumberReturn;

            //Get the hidden value whichhas a tag 100 more
            UILabel lblHiddenNumberField = (UILabel)View.ViewWithTag(txtNumberField.Tag + 100);
            string sNumberFieldHidden = lblHiddenNumberField.Text;

            if (sNumberFieldHidden != sNumberReturn)
            {
                UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag((iSectionId + 1) * iSectionStatusTagId);
                lblUnsavedSectionFlag.Text = "1";
                SetAnyValueChanged(sender, null, iSectionId);
            }

            return true;
        }

        public bool CheckboxChanged(object sender, EventArgs e, int iCheckboxIndex, int iSectionId)
        {
            UISwitch checkbox = (UISwitch)sender;
            SetSectionValueChanged(iSectionId + 1);
            SetAnyValueChanged(sender, null, iSectionId);
            return true;
        }

        public bool SaveThisSection (object sender, EventArgs e)
        {
            UIButton btnSave = (UIButton)sender;
            int iBtnId = btnSave.Tag;

            //Now check to see if we are in a field and haven't ended editing yet
            if(m_iValidateType > 0)
            {
                switch(m_iValidateType)
                {
                    case 1: //Inspected By (no validation required)
                        break;
                    case 2: //Inspect Date
                        if(!ValidateInspectDate(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 3: //Test Date
                        if(!ValidateTestDate(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                }
            }
            return SaveSection(iBtnId);
        }

        //Send through just the section counter NOT the section Id. So 1 NOT 1000000 etc
        public void SetSectionValueChanged(int iSectionId)
        {
            UILabel txtEditStatus = (UILabel)View.ViewWithTag (iSectionId * iSectionStatusTagId);
            txtEditStatus.Text = "1";
        }

        public void SaveAllSections ()
        {
//            //Cycle through each section
//            for (int i = 0; i< m_iSections; i++) 
//            {
//                if(!SaveSection(iSaveSectionBtnTagId * (i+1)))
//                {
//                    iUtils.AlertBox alert = new iUtils.AlertBox();
//                    UILabel lblSectionDesc = (UILabel)View.ViewWithTag (iSectionDescTagId * (i+1));
//                    string sDesc = lblSectionDesc.Text;
//                    alert.CreateErrorAlertDialog("Could not save section " + sDesc + ". Exiting.");
//                    return;
//                }
//            }

        }

        public bool SaveSection (int iBtnId)
        {
            switch (iBtnId)
            {
                default:
                break;
            }
            return true;
        }

        public void SetAnyValueChanged(object sender, EventArgs e, int iSectionId)
        {
            UIView changes = (UIView)View.ViewWithTag (60);
            changes.Hidden = false;
            UILabel txtEditStatus = (UILabel)View.ViewWithTag (80);
            txtEditStatus.Text = "1";

            //Enable the Save section button
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (iSectionId+1));
            btnSave.Hidden = false;

        }

        public void SetAnyValueChangedOff (int iSectionId)
        {
            //Check all sections are saved and if so turn it off
            bool bAllSectionsOff = true;
            for (int i = 0; i< m_iSections; i++) 
            {
                UILabel txtEditStatus = (UILabel)View.ViewWithTag ((i+1)* iSectionStatusTagId);
                int iStatus = Convert.ToInt16(txtEditStatus.Text);
                if(iStatus == 1)
                {
                    bAllSectionsOff = false;
                    break;
                }

            }

            if (bAllSectionsOff) 
            {
                UIView changes = (UIView)View.ViewWithTag (60);
                changes.Hidden = true;
                UILabel txtEditStatus = (UILabel)View.ViewWithTag (80);
                txtEditStatus.Text = "0";
            }

//            ShowCompletedLabels();

            //Disable the Save section button
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (iSectionId+1));
            btnSave.Hidden = true;
            m_iValidateType = -1;
        }


        public void ExpandSection (object sender, EventArgs e)
        {
            UIButton btnExpand = (UIButton)sender;
            int iBtnId = btnExpand.Tag;
            int iSectionId = iContainerSectionTagId * (iBtnId/iExpandSectionBtnTagId);
            UIView vwSection = View.ViewWithTag (iSectionId);
            vwSection.Hidden = false;
            UILabel hfSectionHeight = (UILabel)View.ViewWithTag (iSectionId/iContainerSectionTagId * iSectionHeightTagId);
            float iHeightToMove = (float)Convert.ToDouble( hfSectionHeight.Text);

            //And now move everything below this section down by the section height
            int iSectionNo = iBtnId/iExpandSectionBtnTagId;
            for(int i = iSectionNo; i< m_iSections ; i++)
            {
                iSectionId = iSectionTagId * (i+1);
                UIView vwSection1 = View.ViewWithTag (iSectionId);
                RectangleF frame1 = vwSection1.Frame;
                frame1.Y += iHeightToMove;
                vwSection1.Frame = frame1;

                iSectionId = iContainerSectionTagId * (i+1);
                UIView vwSection2 = View.ViewWithTag (iSectionId);
                RectangleF frame2 = vwSection2.Frame;
                frame2.Y += iHeightToMove;
                vwSection2.Frame = frame2;
            }

            //And increase the content size of the main scroll view by the same amount
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            SizeF layoutSize = scrollVw.ContentSize;
            layoutSize.Height += iHeightToMove;
            scrollVw.ContentSize = layoutSize;

            //And now disable this button and enable the contract button
            btnExpand.Enabled = false;
            UIButton btnContract = (UIButton)View.ViewWithTag (iBtnId / iExpandSectionBtnTagId * iContractSectionBtnTagId);
            btnContract.Enabled = true;
        }

        public void ContractSection (object sender, EventArgs e)
        {
            UIButton btnContract = (UIButton)sender;
            int iBtnId = btnContract.Tag;
            int iSectionId = iContainerSectionTagId * (iBtnId/iContractSectionBtnTagId);
            UIView vwSection = View.ViewWithTag (iSectionId);
            vwSection.Hidden = true;
            UILabel hfSectionHeight = (UILabel)View.ViewWithTag (iSectionId/iContainerSectionTagId * iSectionHeightTagId);
            float iHeightToMove = (float)Convert.ToDouble( hfSectionHeight.Text);

            //And now move everything below this section up by the section height
            int iSectionNo = iBtnId/iContractSectionBtnTagId;
            for(int i = iSectionNo; i< m_iSections ; i++)
            {
                iSectionId = iSectionTagId * (i+1);
                UIView vwSection1 = View.ViewWithTag (iSectionId);
                RectangleF frame1 = vwSection1.Frame;
                frame1.Y -= iHeightToMove;
                vwSection1.Frame = frame1;

                iSectionId = iContainerSectionTagId * (i+1);
                UIView vwSection2 = View.ViewWithTag (iSectionId);
                RectangleF frame2 = vwSection2.Frame;
                frame2.Y -= iHeightToMove;
                vwSection2.Frame = frame2;
            }

            //And reduce the content size of the main scroll view by the same amount
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            SizeF layoutSize = scrollVw.ContentSize;
            layoutSize.Height -= iHeightToMove;
            scrollVw.ContentSize = layoutSize;

            //And now disable this button and enable the expand button
            btnContract.Enabled = false;
            UIButton btnExpand = (UIButton)View.ViewWithTag (iBtnId / iContractSectionBtnTagId * iExpandSectionBtnTagId);
            btnExpand.Enabled = true;
        }

        public bool SectionFullyComplete(int iSectionId)
        {
            bool bReturn = false;
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            switch (iSectionId)
            {
                case 1: //Check if the header is fully committed for this string/bank
                    bReturn = DBQ.ProjectSection10PowerConversionComplete(m_sPassedId);
                    break;

            }

            return bReturn;
        }

        public bool SectionFullyCommitted(int iSectionId)
        {
            bool bReturn = false;
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();

            switch (iSectionId)
            {
                case 1: //Check if the header is fully committed for this string/bank
                    bReturn = DBQ.ProjectSection10PowerConversionFullyCommitted(m_sPassedId);
                    break;

            }

            return bReturn;
        }

        public bool MoveNextTextField(object sender, int iTextFieldIndex)
        {
            UITextField txtField = (UITextField)sender;
            UITextField txtNext;
            int iUniqueRowId = 0;
            int iCellNo = 0;
            int iNextCellId = 0;
            txtField.ResignFirstResponder();
            int iTextTagId = txtField.Tag;

            if (iTextFieldIndex >= 100 && iTextFieldIndex <= 5000) //This allow for about 49 battery test rows
            {
                iUniqueRowId = iTextFieldIndex / 100;
                iCellNo = iTextTagId - (iUniqueRowId * iType1234CellBaseId);
                iTextFieldIndex = 100;
                if (iCellNo >= 24)
                {
                    iNextCellId = (iUniqueRowId * iType1234CellBaseId) + 1; //Go back to the 1st cell
                }
                else
                {
                    iNextCellId = (iUniqueRowId * iType1234CellBaseId) + (iCellNo + 1);
                }
            }

            switch (iTextFieldIndex) 
            {
                case 1: //Coming from inspected by to inspected date
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iInspectedDateTagId);
                    break;
                case 2: //Coming from inspected date to test date
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iTestDateTagId);
                    break;
                case 3: //Coming from test date to float voltage prior
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iFloatVoltPriorTagId);
                    break;

                case 4: //Coming from float record prior back to chareg period prior
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iChargePeriodPriorTagId);
                    break;

                case 5: //Coming from charge period prior to battery capacity
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iBatteryCapacityTagId);
                    break;
                case 6: //Coming from battery capacity to discharge load
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iDischargeLoadTagId);
                    break;

                case 7: //Coming from discharge load back to inspected by
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iInspectedByTagId);
                    break;

                case 100:
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iNextCellId);
                    break;
            }

            txtNext.BecomeFirstResponder();

            return true;
        }

        public HomeScreen GetHomeScreen ()
        {
            int i;
            for (i=0; i<NavigationController.ViewControllers.Length; i++) 
            {
                if(NavigationController.ViewControllers [i].NibName == "HomeScreen")
                {
                    HomeScreen homeScreen = (HomeScreen)NavigationController.ViewControllers [i];
                    return homeScreen;

                }
            }

            //Just in case nothing is discovered in the loop the home screen becomes the 1st screen on the stack
            HomeScreen homeScreenDefault = (HomeScreen)NavigationController.ViewControllers [0];
            return homeScreenDefault;
        }

        public DownloadedITPsScreen GetDownloadedITPsScreen ()
        {
            int i;
            for (i=0; i<NavigationController.ViewControllers.Length; i++) 
            {
                if(NavigationController.ViewControllers [i].NibName == "DownloadedITPsScreen")
                {
                    DownloadedITPsScreen Screen = (DownloadedITPsScreen)NavigationController.ViewControllers [i];
                    return Screen;

                }
            }

            return null;
        }


        public ProjectITPage GetProjectITPPage ()
        {
            int i;
            for (i=0; i<NavigationController.ViewControllers.Length; i++) 
            {
                if(NavigationController.ViewControllers [i].NibName == "ProjectITPage")
                {
                    ProjectITPage Screen = (ProjectITPage)NavigationController.ViewControllers [i];
                    return Screen;

                }
            }

            return null;
        }

        public Battery GetBatteryPage ()
        {
            int i;
            for (i=0; i<NavigationController.ViewControllers.Length; i++) 
            {
                if(NavigationController.ViewControllers [i].NibName == "Battery")
                {
                    Battery Screen = (Battery)NavigationController.ViewControllers [i];
                    return Screen;

                }
            }

            return null;
        }
    }
}

