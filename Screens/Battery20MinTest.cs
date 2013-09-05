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
        //Header tags Ids
        int iInspectedByHdrLabel = 10010800;
        int iInspectedDateHdrLabel = 10010900;
        int iTestDateHdrLabel = 10011000;
        int iFloatVoltPriorHdrLabel = 10011100;
        int iChargePeriodPriorHdrLabel = 10011200;

        int iInspectedByTagId = 10011300;
        int iInspectedDateTagId = 10011400;
        int iTestDateTagId = 10011500;
        int iFloatVoltPriorTagId = 10011600;
        int iChargePeriodPriorTagId = 10011700;

        int iCellVoltageHdrLabel = 10011800;

        string m_User = "";
        string m_sSessionId = "";
        string m_sPassedId = "";
        string m_sProjDesc = "";
        string m_PwrId = "";
        int m_BankNo = -1;

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
            int iColNo = 0;
            UIView[] arrItems = new UIView[5];
            UIView[] arrItems2 = new UIView[2];
            clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
            string sInspectDate = "";
            string sInspectDateDisplay = "";

            try
            {
                UIScrollView layout = new UIScrollView();
                layout.Frame = new RectangleF(0f,35f,1000f,620f);
                layout.Tag = 2;

                //Now get the data from the DB
                DataSet arrTestHeader = ITPBattTest.GetBatteryAcceptTestType4KeyRecord(m_sPassedId, "ITPBattAcceptTest_Header", m_PwrId, m_BankNo);

                if(arrTestHeader.Tables[0].Rows.Count > 0)
                {
                    iColNo = arrTestHeader.Tables[0].Columns["InspectionDate"].Ordinal;
                    sInspectDate = arrTestHeader.Tables[0].Rows[0].ItemArray[iColNo].ToString();
                    DateTime dtInspectDate = Convert.ToDateTime(sInspectDate);
                    sInspectDateDisplay = dt.Get_Date_String(dtInspectDate, "dd/mm/yy");
                }


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
                arrItems[0] = lblInspectByLabelVw;

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
                arrItems[1] = lblInspectDateLabelVw;

                iUtils.CreateFormGridItem lblTestDateLabel = new iUtils.CreateFormGridItem();
                UIView lblTestDateLabelVw = new UIView();
                lblTestDateLabel.SetDimensions(400f,iVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
                lblTestDateLabel.SetLabelText("Tested By");
                lblTestDateLabel.SetBorderWidth(1.0f);
                lblTestDateLabel.SetFontName("Verdana");
                lblTestDateLabel.SetFontSize(12f);
                lblTestDateLabel.SetTag(iTestDateHdrLabel);
                lblTestDateLabel.SetCellColour("Pale Yellow");
                lblTestDateLabelVw = lblTestDateLabel.GetLabelCell();
                arrItems[2] = lblTestDateLabelVw;

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
                arrItems[3] = lblFloatVoltPriorLabelVw;

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
                arrItems[4] = lblChargePeriodPriorLabelVw;

                layout.AddSubviews(arrItems);

                iVert+= iRowHeight;

                iUtils.CreateFormGridItem lblInspectBy = new iUtils.CreateFormGridItem();
                UIView lblInspectByVw = new UIView();
                lblInspectBy.SetDimensions(0f,iVert, 200f, iEditRowHeight, 2f, 2f, 2f, 2f);
                lblInspectBy.SetLabelText(m_User);
                lblInspectBy.SetBorderWidth(0.0f);
                lblInspectBy.SetFontName("Verdana");
                lblInspectBy.SetFontSize(12f);
                lblInspectBy.SetTag(iInspectedByHdrLabel);
                lblInspectBy.SetCellColour("Pale Yellow");
                lblInspectByVw = lblInspectBy.GetLabelCell();
                arrItems2[0] = lblInspectByVw;

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
//                txtInspectDateView.ShouldBeginEditing += (sender) => {
//                    return SetGlobalEditItems(sender, 1);};
//                txtInspectDateView.ShouldEndEditing += (sender) => {
//                    return ValidateInspectDate(sender, 1, 0);};
//                txtInspectDateView.ShouldReturn += (sender) => {
//                    return MoveNextTextField(sender, 1);};

//                if(bReadOnly)
//                {
//                    txtInspectDateView.Enabled = false;
//                }
  
                arrItems2 [1] = lblInspectDateVw;

                layout.AddSubviews(arrItems2);

                iVert+= iRowHeight;

                iTotalHeight = iVert + 280f;
                SizeF layoutSize = new SizeF(1000f, iTotalHeight);
                layout.ContentSize = layoutSize;
                View.AddSubview(layout);

            }
            catch (Exception except)
            {
                string sTest = except.Message.ToString();
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog (sTest);
            }
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

