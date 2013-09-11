using System;
using System.Drawing;
using System.Data;
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

        //Header tags Ids
        int iHeaderRowStatusTagId = 10010700;
        int iInspectedByHdrLabel = 10010800;
        int iInspectedDateHdrLabel = 10010900;
        int iTestDateHdrLabel = 10011000;
        int iFloatVoltPriorHdrLabel = 10011100;
        int iChargePeriodPriorHdrLabel = 10011200;

        int iInspectedByTagId = 10011300;
        int iInspectedDateTagId = 10011400;
        int iInspectedDateHiddenTagId = 10011500;
        int iTestDateTagId = 10011600;
        int iTestDateHiddenTagId = 10011700;
        int iFloatVoltPriorTagId = 10011800;
        int iChargePeriodPriorTagId = 10011900;

        int iCellVoltageHdrLabel = 10012000;

        string m_User = "";
        string m_sSessionId = "";
        string m_sPassedId = "";
        string m_sProjDesc = "";
        string m_PwrId = "";
        int m_BankNo = -1;
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
            float iTotalHeight = 0f;
            float iVert = 0.0f;
            float iRowHeight = 20f;
            float iEditRowHeight = 40f;
            float iSectionHdrRowHeight = 40f;
            float iSectionHeightId = 0f;
            int iColNo = 0;
            UIView[] arrItems = new UIView[7];
            UIView[] arrItems2 = new UIView[6];
            UIView hdrSection;
            clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
            string sInspectedBy = "";
            string sInspectDate = "";
            string sInspectDateDisplay = "";
            string sTestDate = "";
            string sTestDateDisplay = "";
            bool bReadOnly = false;

            try
            {
                UIScrollView layout = new UIScrollView();
                layout.Frame = new RectangleF(0f,35f,1000f,620f);
                layout.Tag = 2;
                View.AddSubview(layout);

                hdrSection = BuildSectionHeader(m_iSections, "20 min Test Header Info", iVert, iSectionHdrRowHeight,1);
                layout.AddSubview(hdrSection);

                UIView SectionTableRow = new UIView();
                SectionTableRow.Frame = new RectangleF(0f,iSectionHdrRowHeight,1000f,iSectionHdrRowHeight);
                SectionTableRow.Tag = iContainerSectionTagId * (m_iSections);

                iVert = 0f;
                iSectionHeightId = 0f;

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

                iUtils.CreateFormGridItem lblInspectBy = new iUtils.CreateFormGridItem();
                UIView lblInspectByVw = new UIView();
                lblInspectBy.SetDimensions(0f, iVert, 200f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblInspectBy.SetLabelText(sInspectedBy);
                lblInspectBy.SetBorderWidth(0.0f);
                lblInspectBy.SetFontName("Verdana");
                lblInspectBy.SetFontSize(12f);
                lblInspectBy.SetTag(iInspectedByTagId);
                lblInspectBy.SetCellColour("Pale Yellow");

                lblInspectByVw = lblInspectBy.GetTextFieldCell();
                UITextField txtInspectByView = lblInspectBy.GetTextFieldView();
                txtInspectByView.AutocorrectionType = UITextAutocorrectionType.No;
                txtInspectByView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtInspectByView.ReturnKeyType = UIReturnKeyType.Next;
                //                txtInspectByView.ShouldBeginEditing += (sender) => {
                //                    return SetGlobalEditItems(sender, 1);};
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
                hfCurrentInspectBy.Tag = iInspectedDateHiddenTagId;
                hfCurrentInspectBy.Hidden = true;
                arrItems2 [1] = hfCurrentInspectBy;

                iUtils.CreateFormGridItem lblInspectDate = new iUtils.CreateFormGridItem();
                UIView lblInspectDateVw = new UIView();
                lblInspectDate.SetDimensions(200f, iVert, 200f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblInspectDate.SetLabelText(sInspectDateDisplay);
                lblInspectDate.SetBorderWidth(0.0f);
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
                    return SetGlobalEditItems(sender, 1);};
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
                lblTestedDate.SetDimensions(400f, iVert, 200f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblTestedDate.SetLabelText(sTestDateDisplay);
                lblTestedDate.SetBorderWidth(0.0f);
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
                    return SetGlobalEditItems(sender, 2);};
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

                SectionTableRow.AddSubviews(arrItems2);

                iVert+= iEditRowHeight;
                iSectionHeightId += iEditRowHeight;

                //Resize the main frame for the section
                RectangleF frame1 = SectionTableRow.Frame;
                frame1.Height = iSectionHeightId;
                SectionTableRow.Frame = frame1;

                layout.AddSubview(SectionTableRow);
//                SectionTableRow.Frame = new RectangleF(0f,0f,1000f, iSectionHeightId);
                UILabel hfSectionEquipmentHeight = (UILabel)View.ViewWithTag (iSectionHeightTagId * (m_iSections));
                hfSectionEquipmentHeight.Text = iSectionHeightId.ToString();

                //Resize the scroll frame
                iTotalHeight = iVert + 280f;
                SizeF layoutSize = new SizeF(1000f, iTotalHeight);
                layout.ContentSize = layoutSize;

            }
            catch (Exception except)
            {
                string sTest = except.Message.ToString();
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog (sTest);
            }
        }

        //The SectionId is a number for the section 1, 2, 3 etc
        public UIView BuildSectionHeader(int iSectionId, string sSectionDesc, float iVertPosition, float iSectionHdrRowHeight, int iRows)
        {
            float iVert = iVertPosition;
            UIView[] arrItems4 = new UIView[8];
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

            iUtils.CreateFormGridItem SectionEquipment = new iUtils.CreateFormGridItem();
            UIView SectionEquipmentVw = new UIView();
            SectionEquipment.SetDimensions(0f,0f, 400f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
            SectionEquipment.SetLabelText(sSectionDesc);
            SectionEquipment.SetBorderWidth(0.0f);
            SectionEquipment.SetFontName("Verdana-Bold");
            SectionEquipment.SetTextColour("White");
            SectionEquipment.SetFontSize(12f);
            SectionEquipment.SetCellColour("DarkSlateGrey");
            SectionEquipment.SetTag(iSectionDescTagId * (iSectionId+1));
            SectionEquipmentVw = SectionEquipment.GetLabelCell();
            arrItems4[0] = SectionEquipmentVw;

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
            SectionCompleteLabel.SetDimensions(400f,0f, 150f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
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
            arrItems4[1] = SectionCompleteLabelVw;

            iUtils.CreateFormGridItem btnSaveEquipment = new iUtils.CreateFormGridItem();
            UIView btnSaveEquipmentVw = new UIView();
            btnSaveEquipment.SetDimensions(550f,0f, 150f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
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

            arrItems4[2] = btnSaveEquipmentVw;

            iUtils.CreateFormGridItem btnExpandEquipment = new iUtils.CreateFormGridItem();
            UIView btnExpandEquipmentVw = new UIView();
            btnExpandEquipment.SetDimensions(700f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
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

            arrItems4[3] = btnExpandEquipmentVw;

            iUtils.CreateFormGridItem btnContractEquipment = new iUtils.CreateFormGridItem();
            UIView btnContractEquipmentVw = new UIView();
            btnContractEquipment.SetDimensions(750f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
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

            arrItems4[4] = btnContractEquipmentVw;

            UILabel hfSectionEquipmentHeight = new UILabel();
            hfSectionEquipmentHeight.Tag = iSectionHeightTagId * (iSectionId+1);
            hfSectionEquipmentHeight.Hidden = true;
            hfSectionEquipmentHeight.Text = "0";
            arrItems4[5] = hfSectionEquipmentHeight;

            UILabel hfSectionEquipmentRows = new UILabel();
            hfSectionEquipmentRows.Tag = iSectionRowsTagId * (iSectionId+1);
            hfSectionEquipmentRows.Hidden = true;
            hfSectionEquipmentRows.Text = iRows.ToString();
            arrItems4[6] = hfSectionEquipmentRows;

            UILabel hfSectionEquipmentStatus = new UILabel();
            hfSectionEquipmentStatus.Tag = iSectionStatusTagId * (iSectionId+1);
            hfSectionEquipmentStatus.Hidden = true;
            hfSectionEquipmentStatus.Text = "0";
            arrItems4[7] = hfSectionEquipmentStatus;


            SectionEquipmentRow.AddSubviews(arrItems4);

            m_iSections++;

            return SectionEquipmentRow;
        }

        public void CheckUnsaved ()
        {
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
                SetAnyValueChanged(sender, null);
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
                    SetAnyValueChanged(sender, null);
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
                SetAnyValueChanged(sender, null);
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
                    SetAnyValueChanged(sender, null);
                }
                return true;
            }
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
                    case 1: //Inspect Date
                        if(!ValidateInspectDate(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 2: //Test Date
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

        public void SetAnyValueChanged(object sender, EventArgs e)
        {
            UIView changes = (UIView)View.ViewWithTag (60);
            changes.Hidden = false;
            UILabel txtEditStatus = (UILabel)View.ViewWithTag (80);
            txtEditStatus.Text = "1";

            //Enable the Save section button
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (m_i20MinSection+1));
            btnSave.Hidden = false;

        }

        public void SetAnyValueChangedOff ()
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
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (m_i20MinSection+1));
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
            txtField.ResignFirstResponder();
            int iTagId = txtField.Tag;
            int iTextTagId = 0;
            int iSectionCounterId = m_i20MinSection;

            switch (iTextFieldIndex)
            {
                case 1:
                    iTextTagId = iInspectedByTagId;
                    break;
                case 2:
                    iTextTagId = iInspectedDateTagId;
                    break;
                case 3:
                    iTextTagId = iTestDateTagId;
                    break;
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
                case 3: //Coming from test date back ot inspected by
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    txtNext = (UITextField)View.ViewWithTag (iInspectedByTagId);
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

