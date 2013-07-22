
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
    public partial class Battery : UIViewController
    {
        //Set the tag id constants.
        int iSearchTextTagId = 90;
        int iSearchButtonTagId = 91;
        int iSearchLabelTagId = 92;
        int iSearchTableTagId = 93;
        int iSearchHiddenLabelTagId = 94;
        int iPwrIdRowHiddenLabelTagId = 95;
        int iStringRowHiddenLabelTagId = 96;
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

        //Tags for Battery Section
        int iPwrIdSectionTagId = 10010800;
        int iStringFullRowTagId = 10010900;
        int ihfRow10StatusTagId = 10011100;
        int iPwrIdRowLabelTagId = 10011200;
        int iPwrIdNewBtnTagId = 10011300;
        int ihfPwrIdStringRowsTagId = 10011400;
        int iBankNoLabelTagId = 10011500;
        int iMakeLabelTagId = 10011600;
        int iModelLabelTagId = 10011700;
        int iDOMLabelTagId = 10011800;
        int iFuseCBLabelTagId = 10011900;
        int iRatingLabelTagId = 10012100;
        int iFloorLabelTagId = 10012200;
        int iSuiteLabelTagId = 10012300;
        int iRackLabelTagId = 10012400;
        int iSubrackLabelTagId = 10012500;
        int iEquipTypeLabelTagId = 10012600;
        int iSerialNoLabelTagId = 10012700;
        int iLinkTestLabelTagId = 10012800;
        int i20MinTestLabelTagId = 10012900;
        int iDeleteLabelTagId = 10013100;
        int iStringRowSectionCounterTagId = 10013200;
        int iStringRowPwrIdTagId = 10013300;
        int iStringRowStatusTagId = 10013400;
        int iStringRowAutoIdTagId = 10013500;
        int iStringRowMaximoAssetIdTagId = 10013600;
        int iBankNoTagId = 10013700;
        int iBankNoHiddenTagId = 10013800;
        int iBankNoSearchTagId = 10013900;
        int iBankPlaneTagId = 10014100;
        int iBankMakeTagId = 10014200;
        int iBankMakeSearchTagId = 10014300;
        int iBankModelTagId = 10014400;
        int iSPNHiddenTagId = 10014500;
        int iBankModelSearchTagId = 10014600;
        int iBankDOMTagId = 10014700;
        int iBankFuseOrCBTagId = 10014800;
        int iBankFuseOrCBSearchTagId = 10014900;
        int iBankRatingTagId = 10015100;
        int iFloorTagId = 10015200;
        int iFloorHiddenTagId = 10015300;
        int iFloorSearchTagId = 10015400;
        int iSuiteTagId = 10015500;
        int iSuiteHiddenTagId = 10015600;
        int iSuiteSearchTagId = 10015700;
        int iRackTagId = 10015800;
        int iRackHiddenTagId = 10015900;
        int iRackSearchTagId = 10016100;
        int iSubRackTagId = 10016200;
        int iSubRackHiddenTagId = 10016300;
        int iSubRackSearchTagId = 10016400;
        int iEquipTypeTagId = 10016500;
        int iSerialNoTagId = 10016600;
        int iLinkTestBtnTagId = 10016700;
        int iLinkTestHiddenTagId = 10016800;
        int i20MinTestBtnTagId = 10016900;
        int i20MinTestHiddenTagId = 10017100;
        int iDeleteBatteryStringBtnTagId = 10017200;
        int iHiddenDOMTagId = 10017300;
        int iHiddenRatingTagId = 10017400;
        int iHiddenSerialNoTagId = 10017500;

        int iPwrIdExpandTagId = 10021100;
        int iPwrIdContractTagId = 10021200;
        int iPwrIdSectionInnerTagId = 10021300;
        int iPwrIdHeightTagId  = 10021400;
        int iPwrIdSectionCompleteLabelTagId  = 10021500;

        string m_sSessionId = "";
        string m_sPassedId = "";
        string m_sProjDesc = "";
        int m_iSections = 0;
        int m_iBatterySectionCounter = 0;
        int m_iPwrdIdRows = 0;
        float m_iBatteryRowHeight = 0f;
        string[] m_sBatteryMakes;
        string[] m_sBatteryModels;
        bool m_bSuppressMove = false;
        bool gbSuppressSecondCheck = false;
        UIView m_vwSearch;
        
        UITableView m_cmbSearch;
        UIButton m_btnSearching;
        object m_sender;
        int m_iValidateType;
        
        public enum QuestionsBitMask {NA = 1, No = 2, Yes = 4};
        
        public Battery() : base ("Battery", null)
        {
        }
        

        public override void DidReceiveMemoryWarning ()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning ();
            
            // Release any cached data, images, etc that aren't in use.
        }
        
        public override void ViewDidLoad ()
        {
            string sRtnMsg = "";
            base.ViewDidLoad ();
            UIBarButtonItem mybackbtn = new UIBarButtonItem("Back", UIBarButtonItemStyle.Plain, delegate (object sender, EventArgs e) {CheckUnsaved();});
            NavigationItem.SetHidesBackButton(true, true);
            NavigationItem.SetLeftBarButtonItem(mybackbtn, true);
            
            // Perform any additional setup after loading the view, typically from a nib.
            DrawMenu();
            
            //Load up the data for each section should it not exist
            clsTabletDB.ITPDocumentSection ITPSection = new clsTabletDB.ITPDocumentSection();
            if(!ITPSection.FillLocalITPSections(m_sPassedId, ref sRtnMsg))
            {
                iUtils.AlertBox alert = new iUtils.AlertBox();
                alert.CreateAlertDialog();
                alert.SetAlertMessage(sRtnMsg);
                alert.ShowAlertBox(); 
            }
            try
            {
                DrawOpeningPage();
            }
            catch (Exception ex)
            {
                iUtils.AlertBox alert = new iUtils.AlertBox();
                alert.CreateAlertDialog();
                alert.SetAlertMessage(ex.Message.ToString());
                alert.ShowAlertBox(); 
                
            }
        }
        
//        public override void ViewDidUnload ()
//        {
//            base.ViewDidUnload ();
//            
//            // Clear any references to subviews of the main view in order to
//            // allow the Garbage Collector to collect them sooner.
//            //
//            // e.g. myOutlet.Dispose (); myOutlet = null;
//            
//            ReleaseDesignerOutlets ();
//        }
//        
//        public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
//        {
//            // Return true for supported orientations
//            return true;
//        }
  
        public void DrawMenu()
        {
            UIView[] arrItems = new UIView[8];
            string sUsername = "";
            HomeScreen home = GetHomeScreen();
            sUsername = home.GetLoginName();
            m_sSessionId = home.GetSessionId();
            DownloadedITPsScreen downloadedScreen =   GetDownloadedITPsScreen();
            m_sPassedId = downloadedScreen.GetSelectedProjectId();
            m_sProjDesc = downloadedScreen.GetSelectedProjectDesc();
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
            try
            {
                int iColNo = 0;
                int iSectionId = 0;
                string sId = m_sPassedId;
                float iVert = 0f;
                float iPwrIdRowVertInner = 0f;
                float iSectionHdrRowHeight = 40f;
                float iTotalHeight = 0f;
                float iHeightToAdd = 0f;
                bool bHideComplete = true;
                bool bHideSectionComplete = true;
                bool bFullyCommitted = false;
                bool bRFUPwrIdCommitted = false;
                UIView[] arrItems4 = new UIView[8];
                UIView[] arrItems5 = new UIView[8];

                //Get some static data for dropdowns only once so we don't reprocess unecessarily
                clsTabletDB.ITPInventory ITPInventory = new clsTabletDB.ITPInventory();
                string[] sBatteryMakes = ITPInventory.GetBatteryMakes();
                m_sBatteryMakes = sBatteryMakes;
                
                UIScrollView layout = new UIScrollView();
                layout.Frame = new RectangleF(0f,35f,1000f,620f);
                layout.Tag = 2;
                clsTabletDB.ITPDocumentSection ITPSection = new clsTabletDB.ITPDocumentSection();
                //******************************************************************************************//
                //                      SECTION 10 (BATTERIES)                                              //
                //******************************************************************************************//
                //Get all the PwrId's for this project from ITPSection10
                DataSet arrITPSection10PwrIds = ITPSection.GetLocalITPSection10PwrIds(sId, 6);
                
                if (arrITPSection10PwrIds.Tables.Count > 0)
                {
                    int ii = 0;
                    m_iSections++; //Add an extra one for the batteries section
                    m_iBatterySectionCounter = ii;
                    int iPwrIdRows = arrITPSection10PwrIds.Tables[0].Rows.Count;
                    m_iPwrdIdRows = iPwrIdRows;
                    
                    //Add in the section title and buttons for each section header
                    UIView Section10Row = new UIView();
                    float iSection10RowVertTop = iVert;
                    Section10Row.Frame = new RectangleF(0f,iSection10RowVertTop,1000f,iSectionHdrRowHeight);
                    iSectionId = iSectionTagId * (ii+1);
                    Section10Row.Tag = iSectionId;
                    layout.AddSubview(Section10Row);
                    
                    UILabel hfSection10 = new UILabel();
                    hfSection10.Text = "10";
                    hfSection10.Tag = iSectionDBIdTagId * (ii+1);
                    hfSection10.Hidden = true;
                    Section10Row.AddSubview(hfSection10);
                    
                    iUtils.CreateFormGridItem Section10 = new iUtils.CreateFormGridItem();
                    UIView Section10Vw = new UIView();
                    Section10.SetDimensions(0f,0f, 400f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
                    Section10.SetLabelText("BATTERIES");
                    Section10.SetBorderWidth(0.0f);
                    Section10.SetFontName("Verdana-Bold");
                    Section10.SetTextColour("White");
                    Section10.SetFontSize(12f);
                    Section10.SetCellColour("DarkSlateGrey");
                    Section10.SetTag(iSectionDescTagId * (ii+1));
                    Section10Vw = Section10.GetLabelCell();
                    arrItems4[0] = Section10Vw;
                    
                    if(BatteryFullyCommitted())
                    {
                        bFullyCommitted = true;
                        bHideComplete = false;
                    }
                    else
                    {
                        bFullyCommitted = false;
                        if(BatteryFullyComplete())
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
                    SectionCompleteLabel.SetFontSize(14f);
                    SectionCompleteLabel.SetCellColour("DarkSlateGrey");
                    SectionCompleteLabel.SetTag(iSectionCompleteLabelTagId * (ii+1));
                    SectionCompleteLabel.SetHidden(bHideComplete);
                    SectionCompleteLabelVw = SectionCompleteLabel.GetLabelCell();
                    arrItems4[1] = SectionCompleteLabelVw;
                    
                    iUtils.CreateFormGridItem btnSave10 = new iUtils.CreateFormGridItem();
                    UIView btnSave10Vw = new UIView();
                    btnSave10.SetDimensions(550f,0f, 150f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                    btnSave10.SetLabelText("Save Section");
                    btnSave10.SetBorderWidth(0.0f);
                    btnSave10.SetFontName("Verdana");
                    btnSave10.SetFontSize(12f);
                    btnSave10.SetHidden(true);
                    btnSave10.SetTag(iSaveSectionBtnTagId * (ii+1));
                    btnSave10.SetCellColour("DarkSlateGrey");
                    btnSave10Vw = btnSave10.GetButtonCell();
                    
                    UIButton btnSave10Button = new UIButton();
                    btnSave10Button = btnSave10.GetButton();
                    btnSave10Button.TouchUpInside += (sender,e) => {SaveThisSection(sender, e);};
                    
                    arrItems4[2] = btnSave10Vw;
                    
                    iUtils.CreateFormGridItem btnExpand10 = new iUtils.CreateFormGridItem();
                    UIView btnExpand10Vw = new UIView();
                    btnExpand10.SetDimensions(700f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                    btnExpand10.SetLabelText("+");
                    btnExpand10.SetBorderWidth(0.0f);
                    btnExpand10.SetFontName("Verdana");
                    btnExpand10.SetFontSize(12f);
                    btnExpand10.SetTag(iExpandSectionBtnTagId * (ii+1));
                    btnExpand10.SetCellColour("DarkSlateGrey");
                    btnExpand10Vw = btnExpand10.GetButtonCell();
                    
                    UIButton btnExpand10Button = new UIButton();
                    btnExpand10Button = btnExpand10.GetButton();
                    btnExpand10Button.Enabled = false;
                    btnExpand10Button.TouchUpInside += (sender,e) => {ExpandSection(sender, e);};
                    
                    arrItems4[3] = btnExpand10Vw;
                    
                    iUtils.CreateFormGridItem btnContract10 = new iUtils.CreateFormGridItem();
                    UIView btnContract10Vw = new UIView();
                    btnContract10.SetDimensions(750f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                    btnContract10.SetLabelText("-");
                    btnContract10.SetBorderWidth(0.0f);
                    btnContract10.SetFontName("Verdana");
                    btnContract10.SetFontSize(12f);
                    btnContract10.SetTag(iContractSectionBtnTagId * (ii+1));
                    btnContract10.SetCellColour("DarkSlateGrey");
                    btnContract10Vw = btnContract10.GetButtonCell();
                    
                    UIButton btnContract10Button = new UIButton();
                    btnContract10Button = btnContract10.GetButton();
                    btnContract10Button.TouchUpInside += (sender,e) => {ContractSection(sender, e);};
                    
                    arrItems4[4] = btnContract10Vw;
                    
                    UILabel hfSectionHeight = new UILabel();
                    hfSectionHeight.Tag = iSectionHeightTagId * (ii+1);
                    hfSectionHeight.Hidden = true;
                    hfSectionHeight.Text = "0";
                    arrItems4[5] = hfSectionHeight;
                    
                    UILabel hfSectionRows = new UILabel();
                    hfSectionRows.Tag = iSectionRowsTagId * (ii+1);
                    hfSectionRows.Hidden = true;
                    hfSectionRows.Text = iPwrIdRows.ToString();
                    arrItems4[6] = hfSectionRows;
                    
                    UILabel hfSectionStatus = new UILabel();
                    hfSectionStatus.Tag = iSectionStatusTagId * (ii+1);
                    hfSectionStatus.Hidden = true;
                    hfSectionStatus.Text = "0";
                    arrItems4[7] = hfSectionStatus;
                    
                    
                    Section10Row.AddSubviews(arrItems4);
                    
                    iVert += iSectionHdrRowHeight;
                    
                    //Now add a new view to this view to hold another view containing all the pwrid info for this section 10
                    UIView PwrIdTableRow = new UIView();
                    PwrIdTableRow.Frame = new RectangleF(0f,iVert,1000f,iSectionHdrRowHeight);
                    iSectionId = iContainerSectionTagId * (ii+1);
                    PwrIdTableRow.Tag = iSectionId;
                    layout.AddSubview(PwrIdTableRow);
                    float iPwrIdRowVert = 0.0f;
                    float iSectionPwrIdHeight = 0.0f;
                    float iPwrIdRowVertTop = iVert;
                    float iPwrIdRowInnerTop = 0.0f;
                    float iPwrIdRowInnerTop2 = 0.0f;
                    
                    for (var j = 0; j < iPwrIdRows; j++)
                    {
                        iPwrIdRowInnerTop2 = 0.0f;
                        UIView vwPwrInternalRowId = new UIView();
                        vwPwrInternalRowId.Frame = new RectangleF(0f,iPwrIdRowVert,1000f,200f); //This will be resized later on
                        vwPwrInternalRowId.Tag = (iPwrIdSectionTagId + (j+1)) * (ii+1);                   
                        
                        UILabel hfRow10Status = new UILabel();
                        hfRow10Status.Text = "0";
                        hfRow10Status.Tag = (ihfRow10StatusTagId + (j+1)) * (ii+1);
                        hfRow10Status.Hidden = true;
                        arrItems5[0] = hfRow10Status;
                        
                        //Put in the PwrId Label
                        iUtils.CreateFormGridItem rowPwrIdLabel = new iUtils.CreateFormGridItem();
                        UIView rowPwrIdLabelVw = new UIView();
                        iColNo = arrITPSection10PwrIds.Tables[0].Columns["PwrId"].Ordinal;
                        string sPwrId = arrITPSection10PwrIds.Tables[0].Rows[j].ItemArray[iColNo].ToString();
                        rowPwrIdLabel.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                        rowPwrIdLabel.SetDimensions(0f,iPwrIdRowVert, 200f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                        rowPwrIdLabel.SetLabelText(sPwrId);
                        rowPwrIdLabel.SetBorderWidth(0.0f);
                        rowPwrIdLabel.SetFontName("Verdana-Bold");
                        rowPwrIdLabel.SetFontSize(18f);
                        rowPwrIdLabel.SetTag((iPwrIdRowLabelTagId + (j+1)) * (ii+1));
                        
                        if (j % 2 == 0)
                        {
                            rowPwrIdLabel.SetCellColour("Pale Yellow");
                        }
                        else
                        {
                            rowPwrIdLabel.SetCellColour("Pale Orange");
                        }
                        
                        rowPwrIdLabelVw = rowPwrIdLabel.GetLabelCell();
                        iHeightToAdd = iSectionHdrRowHeight;
                        arrItems5[1] = rowPwrIdLabelVw;

                        bRFUPwrIdCommitted = RFUPwrIdCommitted(sPwrId);
                        
                        iUtils.CreateFormGridItem btnNewBatteryString = new iUtils.CreateFormGridItem();
                        UIView btnNewBatteryStringVw = new UIView();
                        btnNewBatteryString.SetDimensions(200f,iPwrIdRowVert, 200f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                        btnNewBatteryString.SetLabelText("New Battery Block/String");
                        btnNewBatteryString.SetBorderWidth(0.0f);
                        btnNewBatteryString.SetFontName("Verdana");
                        btnNewBatteryString.SetFontSize(12f);
                        btnNewBatteryString.SetTag((iPwrIdNewBtnTagId + (j+1)) * (ii+1));
                        if (j % 2 == 0)
                        {
                            btnNewBatteryString.SetCellColour("Pale Yellow");
                        }
                        else
                        {
                            btnNewBatteryString.SetCellColour("Pale Orange");
                        }
                        btnNewBatteryStringVw = btnNewBatteryString.GetButtonCell();
                        
                        UIButton btnNewBatteryStringButton = new UIButton();
                        btnNewBatteryStringButton = btnNewBatteryString.GetButton();
                        btnNewBatteryStringButton.TouchUpInside += (sender,e) => {AddNewBatteryString(sender, e);};
                        if(bRFUPwrIdCommitted)
                        {
                            btnNewBatteryStringButton.Enabled = false;
                        }
                        arrItems5[2] = btnNewBatteryStringVw;

                        if(BatteryPwrIdComplete(sPwrId)) 
                        {
                            bHideSectionComplete = false;
                        }
                        else
                        {
                            bHideSectionComplete = true;
                        }
                        iUtils.CreateFormGridItem PwrIdCompleteLabel = new iUtils.CreateFormGridItem();
                        UIView PwrIdCompleteLabelVw = new UIView();
                        PwrIdCompleteLabel.SetDimensions(400f,0f, 150f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
                        if(bRFUPwrIdCommitted)
                        {
                            PwrIdCompleteLabel.SetLabelText("COMMITTED");
                            bHideSectionComplete = false;
                        }
                        else
                        {
                            PwrIdCompleteLabel.SetLabelText("COMPLETED");
                        }

                        PwrIdCompleteLabel.SetBorderWidth(0.0f);
                        PwrIdCompleteLabel.SetFontName("Verdana-Bold");
                        PwrIdCompleteLabel.SetTextColour("Royal Blue");
                        PwrIdCompleteLabel.SetFontSize(14f);
                        if (j % 2 == 0)
                        {
                            PwrIdCompleteLabel.SetCellColour("Pale Yellow");
                        }
                        else
                        {
                            PwrIdCompleteLabel.SetCellColour("Pale Orange");
                        }
                        PwrIdCompleteLabel.SetTag((iPwrIdSectionCompleteLabelTagId + (j+1)) * (ii+1));
                        PwrIdCompleteLabel.SetHidden(bHideSectionComplete);
                        PwrIdCompleteLabelVw = PwrIdCompleteLabel.GetLabelCell();
                        arrItems5[3] = PwrIdCompleteLabelVw;

                        iUtils.CreateFormGridItem rowPwrIdBlank = new iUtils.CreateFormGridItem();
                        UIView rowPwrIdBlankVw = new UIView();
                        rowPwrIdBlank.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                        rowPwrIdBlank.SetDimensions(550f,iPwrIdRowVert, 350f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                        rowPwrIdBlank.SetLabelText("");
                        rowPwrIdBlank.SetBorderWidth(0.0f);
                        rowPwrIdBlank.SetFontName("Verdana");
                        rowPwrIdBlank.SetFontSize(12f);
                        rowPwrIdBlank.SetTag((iPwrIdRowLabelTagId + (j+1)) * (ii+1));
                        
                        if (j % 2 == 0)
                        {
                            rowPwrIdBlank.SetCellColour("Pale Yellow");
                        }
                        else
                        {
                            rowPwrIdBlank.SetCellColour("Pale Orange");
                        }
                        
                        rowPwrIdBlankVw = rowPwrIdBlank.GetLabelCell();
                        arrItems5[4] = rowPwrIdBlankVw;
                        
                        iUtils.CreateFormGridItem btnExpandPwrId = new iUtils.CreateFormGridItem();
                        UIView btnExpandPwrIdVw = new UIView();
                        btnExpandPwrId.SetDimensions(900f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                        btnExpandPwrId.SetLabelText("+");
                        btnExpandPwrId.SetBorderWidth(0.0f);
                        btnExpandPwrId.SetFontName("Verdana");
                        btnExpandPwrId.SetFontSize(12f);
                        btnExpandPwrId.SetTag((iPwrIdExpandTagId + (j+1)) * (ii+1));
                        if (j % 2 == 0)
                        {
                            btnExpandPwrId.SetCellColour("Pale Yellow");
                        }
                        else
                        {
                            btnExpandPwrId.SetCellColour("Pale Orange");
                        }
                        btnExpandPwrIdVw = btnExpandPwrId.GetButtonCell();
                        
                        UIButton btnExpandPwrIdButton = new UIButton();
                        btnExpandPwrIdButton = btnExpandPwrId.GetButton();
                        btnExpandPwrIdButton.Enabled = false;
                        btnExpandPwrIdButton.TouchUpInside += (sender,e) => {ExpandPwrId(sender, e, 1);};
                        
                        arrItems5[5] = btnExpandPwrIdVw;
                        
                        iUtils.CreateFormGridItem btnContractPwrId = new iUtils.CreateFormGridItem();
                        UIView btnContractPwrIdVw = new UIView();
                        btnContractPwrId.SetDimensions(950f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                        btnContractPwrId.SetLabelText("-");
                        btnContractPwrId.SetBorderWidth(0.0f);
                        btnContractPwrId.SetFontName("Verdana");
                        btnContractPwrId.SetFontSize(12f);
                        btnContractPwrId.SetTag((iPwrIdContractTagId + (j+1)) * (ii+1));
                        if (j % 2 == 0)
                        {
                            btnContractPwrId.SetCellColour("Pale Yellow");
                        }
                        else
                        {
                            btnContractPwrId.SetCellColour("Pale Orange");
                        }
                        btnContractPwrIdVw = btnContractPwrId.GetButtonCell();
                        
                        UIButton btnContractPwrIdButton = new UIButton();
                        btnContractPwrIdButton = btnContractPwrId.GetButton();
                        btnContractPwrIdButton.TouchUpInside += (sender,e) => {ContractPwrId(sender, e, 1);};
                        
                        arrItems5[6] = btnContractPwrIdVw;
                        
                        UILabel hfPwrIdSectionHeight = new UILabel();
                        hfPwrIdSectionHeight.Tag = (iPwrIdHeightTagId + (j+1)) * (ii+1);
                        hfPwrIdSectionHeight.Hidden = true;
                        hfPwrIdSectionHeight.Text = "0";
                        arrItems5[7] = hfPwrIdSectionHeight;
                        
                        iHeightToAdd = iSectionHdrRowHeight;
                        
                        //Now add the row details into the view
                        vwPwrInternalRowId.AddSubviews(arrItems5);
                        
                        iSectionPwrIdHeight += iHeightToAdd;
                        iPwrIdRowVert += iHeightToAdd;
                        iVert += iHeightToAdd;
                        iPwrIdRowInnerTop2 += iHeightToAdd;
                        
                        iPwrIdRowVertInner = 0f;
                        UIView vwPwrInternalRowIdInnner = new UIView();
                        vwPwrInternalRowIdInnner.Tag = (iPwrIdSectionInnerTagId + (j+1)) * (ii+1);                   
                        vwPwrInternalRowIdInnner.Frame = new RectangleF(0f,iPwrIdRowVertInner,1000f,200f); //This will be resized later on
                        
                        UIView PwrIdHdr = BuildBatteryHeader(j, ref iHeightToAdd);
                        PwrIdHdr.Frame = new RectangleF(0f, iPwrIdRowVertInner, 1000f, iHeightToAdd);
                        vwPwrInternalRowIdInnner.AddSubview(PwrIdHdr);
                        vwPwrInternalRowId.AddSubview(vwPwrInternalRowIdInnner);
                        
                        iSectionPwrIdHeight += iHeightToAdd;
                        iPwrIdRowVert += iHeightToAdd;
                        iPwrIdRowVertInner += iHeightToAdd;
                        iVert += iHeightToAdd;
                        
                        //Now for each PwrId get the details for each string
                        DataSet arrITPSection10PwrIdStrings = ITPSection.GetLocalITPSection10PwrIdStringDetails(sId, sPwrId);
                        
                        if (arrITPSection10PwrIdStrings.Tables.Count > 0)
                        {
                            int iPwrIdStringRows = arrITPSection10PwrIdStrings.Tables[0].Rows.Count;
                            //Add the rows to a hidden field so we know how many rows are in each PwrId battery block
                            UILabel hfPwrIdStringRows = new UILabel();
                            hfPwrIdStringRows.Text = iPwrIdStringRows.ToString();
                            hfPwrIdStringRows.Tag = (ihfPwrIdStringRowsTagId + (j+1)) * (ii+1);
                            hfPwrIdStringRows.Hidden = true;
                            vwPwrInternalRowIdInnner.AddSubview(hfPwrIdStringRows);
                            
                            
                            for (var k = 0; k < iPwrIdStringRows; k++)
                            {
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["AutoId"].Ordinal;
                                int iAutoId = Convert.ToInt32(arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo]);
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["BankNo"].Ordinal;
                                string sBankNo = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                string sBankPlane = sPwrId.Substring(sPwrId.Length - 2, 1);
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["Make"].Ordinal;
                                string sMake = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["Model"].Ordinal;
                                string sModel = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["SPN"].Ordinal;
                                string sSPN = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["DOM"].Ordinal;
                                string sDOM = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["FuseOrCB"].Ordinal;
                                string sFuseOrCB = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["RatingAmps"].Ordinal;
                                string sRatingAmps = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["Floor"].Ordinal;
                                string sFloor = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["Suite"].Ordinal;
                                string sSuite = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["Rack"].Ordinal;
                                string sRack = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["SubRack"].Ordinal;
                                string sSubRack = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["Equipment_Condition"].Ordinal;
                                string sEquipType = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["SerialBatch"].Ordinal;
                                string sSerialNo = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["tblMaximoPSA_ID"].Ordinal;
                                string sMaximoPSAId = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["tblMaximoTransfer_Eqnum"].Ordinal;
                                string sMaximoTransferId = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                if(sMaximoPSAId == "" || sMaximoPSAId == "0")
                                {
                                    sMaximoPSAId = "-1";
                                }
                                if(sMaximoTransferId == "" || sMaximoTransferId == "0")
                                {
                                    sMaximoTransferId = "-1";
                                }
                                int iMaximoPSAId = Convert.ToInt32(sMaximoPSAId);
                                int iMaximoTransferId = Convert.ToInt32(sMaximoTransferId);
                                int iMaximoAssetId = -1;
                                
                                if(iMaximoPSAId > 0)
                                {
                                    iMaximoAssetId = iMaximoPSAId;
                                }
                                else if(iMaximoTransferId > 0)
                                {
                                    iMaximoAssetId = iMaximoTransferId;
                                }
                                else
                                {
                                    iMaximoAssetId = -1;
                                }
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["LinkTest"].Ordinal;
                                string sLinkTest = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                int iLinkTest = 0;
                                if(sLinkTest == "" || sLinkTest == "0")
                                {
                                    iLinkTest = 0;
                                }
                                else
                                {
                                    iLinkTest = Convert.ToInt32(sLinkTest);
                                }
                                
                                iColNo = arrITPSection10PwrIdStrings.Tables[0].Columns["BatteryTest"].Ordinal;
                                string sBatteryTest = arrITPSection10PwrIdStrings.Tables[0].Rows[k].ItemArray[iColNo].ToString();
                                int iBatteryTest = 0;
                                if(sBatteryTest == "" || sBatteryTest == "0")
                                {
                                    iBatteryTest = 0;
                                }
                                else
                                {
                                    iBatteryTest = Convert.ToInt32(sBatteryTest);
                                }
                                
                                //Add in the row
                                UIView BatteryStringRow = BuildBatteryStringRowDetails(ii, j, k, sPwrId, iAutoId,
                                                                                       iMaximoAssetId, sBankNo,
                                                                                       sBankPlane, sMake, sModel, sSPN, sDOM,
                                                                                       sFuseOrCB, sRatingAmps, sFloor, 
                                                                                       sSuite, sRack, sSubRack, sEquipType,
                                                                                       sSerialNo, iLinkTest, iBatteryTest,
                                                                                       false, bRFUPwrIdCommitted,ref iHeightToAdd);
                                BatteryStringRow.Frame = new RectangleF(0f, iPwrIdRowVertInner, 1000f, iHeightToAdd);
                                BatteryStringRow.Tag = iStringFullRowTagId * (j + 1) + (k + 1);
                                vwPwrInternalRowIdInnner.AddSubview(BatteryStringRow);
                                
                                m_iBatteryRowHeight = iHeightToAdd;
                                iSectionPwrIdHeight += iHeightToAdd;
                                iPwrIdRowVert += iHeightToAdd;
                                iPwrIdRowVertInner += iHeightToAdd;
                                iVert += iHeightToAdd;
                                
                            }
                            
                            hfPwrIdSectionHeight.Text = iPwrIdRowVertInner.ToString();
                            vwPwrInternalRowIdInnner.Frame = new RectangleF(0f, iPwrIdRowInnerTop2, 1000f, iPwrIdRowVertInner);
                            vwPwrInternalRowId.Frame = new RectangleF(0f, iPwrIdRowInnerTop, 1000f, iPwrIdRowVert);
                            PwrIdTableRow.AddSubview(vwPwrInternalRowId);
                            iPwrIdRowInnerTop += iPwrIdRowVert;
                            //iPwrIdRowInnerTop2 += iPwrIdRowVertInner;
                            iPwrIdRowVert = 0f;
                        }
                        
                    }
                    //Now resize the UIView that is effectively the container for the battery info for this section
                    //And also store this height in a hidden field for use in the contract and expand functions
                    PwrIdTableRow.Frame = new RectangleF(0f,iPwrIdRowVertTop,1000f,iSectionPwrIdHeight);
                    hfSectionHeight.Text = iSectionPwrIdHeight.ToString();
                }
                
                
                iTotalHeight = iVert + 280f;
                SizeF layoutSize = new SizeF(1000f, iTotalHeight);
                layout.ContentSize = layoutSize;
                
                UILabel hfScrollContentHeight = new UILabel();
                hfScrollContentHeight.Text = iTotalHeight.ToString();
                hfScrollContentHeight.Tag = 3;
                hfScrollContentHeight.Hidden = true;
                layout.AddSubview(hfScrollContentHeight);
                View.AddSubview(layout);

                //Contract all the power conversion PwrIds
                for(int iiii=0;iiii< m_iPwrdIdRows; iiii++)
                {
                    UILabel lblPwrId = (UILabel)View.ViewWithTag ((iPwrIdRowLabelTagId + (iiii+1)) * (m_iBatterySectionCounter+1));                        
                    string sPwrId = lblPwrId.Text;
                    bRFUPwrIdCommitted = RFUPwrIdCommitted(sPwrId);
                    if(bRFUPwrIdCommitted)
                    {
                        UIButton btnContract = (UIButton)View.ViewWithTag ((iPwrIdContractTagId + (iiii+1)) * (m_iBatterySectionCounter+1));                        
                        ContractPwrId(btnContract, null, 1);
                    }
                }


            }
            catch (Exception except)
            {
                string sTest = except.Message.ToString();
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog (sTest);
            }
        }
        
        public UIView BuildBatteryHeader (int iRowNo, ref float iHeightToAdd)
        {
            iHeightToAdd = 0.0f;
            UIView hdrRow = new UIView();
            float iHdrVert = 0.0f;
            float iRowHeight = 20f;
            UIView[] arrItems = new UIView[6];
            UIView[] arrItems2 = new UIView[9];
            
            iUtils.CreateFormGridItem lblBankNo = new iUtils.CreateFormGridItem();
            UIView lblBankNoVw = new UIView();
            lblBankNo.SetDimensions(0f,iHdrVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
            lblBankNo.SetLabelText("Bank No.");
            lblBankNo.SetBorderWidth(1.0f);
            lblBankNo.SetFontName("Verdana");
            lblBankNo.SetFontSize(12f);
            lblBankNo.SetTag(iBankNoLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblBankNo.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBankNo.SetCellColour("Pale Orange");
            }
            
            lblBankNoVw = lblBankNo.GetLabelCell();
            arrItems[0] = lblBankNoVw;
            
            iUtils.CreateFormGridItem lblMake = new iUtils.CreateFormGridItem();
            UIView lblMakeVw = new UIView();
            lblMake.SetDimensions(199f,iHdrVert, 200f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblMake.SetLabelText("Make");
            lblMake.SetBorderWidth(1.0f);
            lblMake.SetFontName("Verdana");
            lblMake.SetFontSize(12f);
            lblMake.SetTag(iMakeLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblMake.SetCellColour("Pale Yellow");
            }
            else
            {
                lblMake.SetCellColour("Pale Orange");
            }
            
            lblMakeVw = lblMake.GetLabelCell();
            arrItems[1] = lblMakeVw;
            
            iUtils.CreateFormGridItem lblModel = new iUtils.CreateFormGridItem();
            UIView lblModelVw = new UIView();
            lblModel.SetDimensions(398f,iHdrVert, 300f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblModel.SetLabelText("Model");
            lblModel.SetBorderWidth(1.0f);
            lblModel.SetFontName("Verdana");
            lblModel.SetFontSize(12f);
            lblModel.SetTag(iModelLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblModel.SetCellColour("Pale Yellow");
            }
            else
            {
                lblModel.SetCellColour("Pale Orange");
            }
            
            lblModelVw = lblModel.GetLabelCell();
            arrItems[2] = lblModelVw;
            
            iUtils.CreateFormGridItem lblDOM = new iUtils.CreateFormGridItem();
            UIView lblDOMVw = new UIView();
            lblDOM.SetDimensions(697f,iHdrVert, 80f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblDOM.SetLabelText("DOM");
            lblDOM.SetBorderWidth(1.0f);
            lblDOM.SetFontName("Verdana");
            lblDOM.SetFontSize(12f);
            lblDOM.SetTag(iDOMLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblDOM.SetCellColour("Pale Yellow");
            }
            else
            {
                lblDOM.SetCellColour("Pale Orange");
            }
            
            lblDOMVw = lblDOM.GetLabelCell();
            arrItems[3] = lblDOMVw;
            
            iUtils.CreateFormGridItem lblFuse = new iUtils.CreateFormGridItem();
            UIView lblFuseVw = new UIView();
            lblFuse.SetDimensions(776f,iHdrVert, 100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblFuse.SetLabelText("Fuse");
            lblFuse.SetBorderWidth(1.0f);
            lblFuse.SetFontName("Verdana");
            lblFuse.SetFontSize(12f);
            lblFuse.SetTag(iFuseCBLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblFuse.SetCellColour("Pale Yellow");
            }
            else
            {
                lblFuse.SetCellColour("Pale Orange");
            }
            
            lblFuseVw = lblFuse.GetLabelCell();
            arrItems[4] = lblFuseVw;
            
            iUtils.CreateFormGridItem lblRating = new iUtils.CreateFormGridItem();
            UIView lblRatingVw = new UIView();
            lblRating.SetDimensions(875f,iHdrVert, 125f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblRating.SetLabelText("Ratings");
            lblRating.SetBorderWidth(1.0f);
            lblRating.SetFontName("Verdana");
            lblRating.SetFontSize(12f);
            lblRating.SetTag(iRatingLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblRating.SetCellColour("Pale Yellow");
            }
            else
            {
                lblRating.SetCellColour("Pale Orange");
            }
            
            lblRatingVw = lblRating.GetLabelCell();
            arrItems[5] = lblRatingVw;
            
            hdrRow.AddSubviews(arrItems);
            
            iHeightToAdd += iRowHeight - 1; //This is because of the 1 pixel overlap of the border (not required on the last one)
            iHdrVert += iRowHeight - 1; //This is because of the 1 pixel overlap of the border (not required on the last one)
            
            //Now put in the 2nd header row of labels
            iUtils.CreateFormGridItem lblFloor = new iUtils.CreateFormGridItem();
            UIView lblFloorVw = new UIView();
            lblFloor.SetDimensions(0f,iHdrVert, 51f, iRowHeight, 2f, 2f, 2f, 2f);
            lblFloor.SetLabelText("Floor");
            lblFloor.SetBorderWidth(1.0f);
            lblFloor.SetFontName("Verdana");
            lblFloor.SetFontSize(12f);
            lblFloor.SetTag(iFloorLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblFloor.SetCellColour("Pale Yellow");
            }
            else
            {
                lblFloor.SetCellColour("Pale Orange");
            }
            
            lblFloorVw = lblFloor.GetLabelCell();
            arrItems2[0] = lblFloorVw;
            
            iUtils.CreateFormGridItem lblSuite = new iUtils.CreateFormGridItem();
            UIView lblSuiteVw = new UIView();
            lblSuite.SetDimensions(50f,iHdrVert, 51f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSuite.SetLabelText("Suite");
            lblSuite.SetBorderWidth(1.0f);
            lblSuite.SetFontName("Verdana");
            lblSuite.SetFontSize(12f);
            lblSuite.SetTag(iSuiteLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblSuite.SetCellColour("Pale Yellow");
            }
            else
            {
                lblSuite.SetCellColour("Pale Orange");
            }
            
            lblSuiteVw = lblSuite.GetLabelCell();
            arrItems2[1] = lblSuiteVw;
            
            iUtils.CreateFormGridItem lblRack = new iUtils.CreateFormGridItem();
            UIView lblRackVw = new UIView();
            lblRack.SetDimensions(100f,iHdrVert, 41f, iRowHeight, 2f, 2f, 2f, 2f);
            lblRack.SetLabelText("Rack");
            lblRack.SetBorderWidth(1.0f);
            lblRack.SetFontName("Verdana");
            lblRack.SetFontSize(12f);
            lblRack.SetTag(iRackLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblRack.SetCellColour("Pale Yellow");
            }
            else
            {
                lblRack.SetCellColour("Pale Orange");
            }
            
            lblRackVw = lblRack.GetLabelCell();
            arrItems2[2] = lblRackVw;
            
            iUtils.CreateFormGridItem lblSubrack = new iUtils.CreateFormGridItem();
            UIView lblSubrackVw = new UIView();
            lblSubrack.SetDimensions(140f,iHdrVert, 60f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSubrack.SetLabelText("Subrack");
            lblSubrack.SetBorderWidth(1.0f);
            lblSubrack.SetFontName("Verdana");
            lblSubrack.SetFontSize(12f);
            lblSubrack.SetTag(iSubrackLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblSubrack.SetCellColour("Pale Yellow");
            }
            else
            {
                lblSubrack.SetCellColour("Pale Orange");
            }
            
            lblSubrackVw = lblSubrack.GetLabelCell();
            arrItems2[3] = lblSubrackVw;
            
            iUtils.CreateFormGridItem lblEquipType = new iUtils.CreateFormGridItem();
            UIView lblEquipTypeVw = new UIView();
            lblEquipType.SetDimensions(199f,iHdrVert, 200f, iRowHeight, 2f, 2f, 2f, 2f);
            lblEquipType.SetLabelText("EquipType");
            lblEquipType.SetBorderWidth(1.0f);
            lblEquipType.SetFontName("Verdana");
            lblEquipType.SetFontSize(12f);
            lblEquipType.SetTag(iEquipTypeLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblEquipType.SetCellColour("Pale Yellow");
            }
            else
            {
                lblEquipType.SetCellColour("Pale Orange");
            }
            
            lblEquipTypeVw = lblEquipType.GetLabelCell();
            arrItems2[4] = lblEquipTypeVw;
            
            iUtils.CreateFormGridItem lblSerialNo = new iUtils.CreateFormGridItem();
            UIView lblSerialNoVw = new UIView();
            lblSerialNo.SetDimensions(398f,iHdrVert, 300f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSerialNo.SetLabelText("SerialNo");
            lblSerialNo.SetBorderWidth(1.0f);
            lblSerialNo.SetFontName("Verdana");
            lblSerialNo.SetFontSize(12f);
            lblSerialNo.SetTag(iSerialNoLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblSerialNo.SetCellColour("Pale Yellow");
            }
            else
            {
                lblSerialNo.SetCellColour("Pale Orange");
            }
            
            lblSerialNoVw = lblSerialNo.GetLabelCell();
            arrItems2[5] = lblSerialNoVw;
            
            iUtils.CreateFormGridItem lblLinkTest = new iUtils.CreateFormGridItem();
            UIView lblLinkTestVw = new UIView();
            lblLinkTest.SetDimensions(697f,iHdrVert, 80f, iRowHeight, 2f, 2f, 2f, 2f);
            lblLinkTest.SetLabelText("LinkTest");
            lblLinkTest.SetBorderWidth(1.0f);
            lblLinkTest.SetFontName("Verdana");
            lblLinkTest.SetFontSize(12f);
            lblLinkTest.SetTag(iLinkTestLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblLinkTest.SetCellColour("Pale Yellow");
            }
            else
            {
                lblLinkTest.SetCellColour("Pale Orange");
            }
            
            lblLinkTestVw = lblLinkTest.GetLabelCell();
            arrItems2[6] = lblLinkTestVw;
            
            iUtils.CreateFormGridItem lbl20MinTest = new iUtils.CreateFormGridItem();
            UIView lbl20MinTestVw = new UIView();
            lbl20MinTest.SetDimensions(776f,iHdrVert, 100f, iRowHeight, 2f, 2f, 2f, 2f);
            lbl20MinTest.SetLabelText("20MinTest");
            lbl20MinTest.SetBorderWidth(1.0f);
            lbl20MinTest.SetFontName("Verdana");
            lbl20MinTest.SetFontSize(12f);
            lbl20MinTest.SetTag(i20MinTestLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lbl20MinTest.SetCellColour("Pale Yellow");
            }
            else
            {
                lbl20MinTest.SetCellColour("Pale Orange");
            }
            
            lbl20MinTestVw = lbl20MinTest.GetLabelCell();
            arrItems2[7] = lbl20MinTestVw;
            
            iUtils.CreateFormGridItem lblDeleteLabel = new iUtils.CreateFormGridItem();
            UIView lblDeleteLabelVw = new UIView();
            lblDeleteLabel.SetDimensions(875f,iHdrVert, 125f, iRowHeight, 2f, 2f, 2f, 2f);
            lblDeleteLabel.SetLabelText("Delete");
            lblDeleteLabel.SetBorderWidth(1.0f);
            lblDeleteLabel.SetFontName("Verdana");
            lblDeleteLabel.SetFontSize(12f);
            lblDeleteLabel.SetTag(iDeleteLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblDeleteLabel.SetCellColour("Pale Yellow");
            }
            else
            {
                lblDeleteLabel.SetCellColour("Pale Orange");
            }
            
            lblDeleteLabelVw = lblDeleteLabel.GetLabelCell();
            arrItems2[8] = lblDeleteLabelVw;
            
            hdrRow.AddSubviews(arrItems2);
            
            iHeightToAdd += iRowHeight;
            iHdrVert += iRowHeight;
            
            return hdrRow;
        }
        
        public UIView BuildBatteryStringRowDetails(int iSectionCounterId, int iRowNo, int iStringRow, string sPwrId, 
                                                   int iAutoId, int iMaximoAssetId, string sBankNo,
                                                   string sBankPlane, string sMake, string sModel, string sSPN, string sDOM,
                                                   string sFuseOrCB, string sRatingAmps, string sFloor, string sSuite,
                                                   string sRack, string sSubRack, string sEquipType, string sSerialNo,
                                                   int iLinkTestStatus, int i20MinTest, 
                                                   bool bNewRow, bool bReadOnly, ref float iHeightToAdd)
        {
            DateClass dt = new DateClass();
            iHeightToAdd = 0.0f;
            UIView hdrRow = new UIView();
            float iHdrVert = 0.0f;
            float iRowHeight = 40f;
            UIView[] arrItems = new UIView[20];
            UIView[] arrItems2 = new UIView[16];
            UIView[] arrItems3 = new UIView[4];
            
            UILabel hfSectionCounter = new UILabel();
            hfSectionCounter.Text = iSectionCounterId.ToString();
            hfSectionCounter.Tag = iStringRowSectionCounterTagId * (iRowNo + 1) + (iStringRow + 1);
            hfSectionCounter.Hidden = true;
            arrItems [0] = hfSectionCounter;
            
            UILabel hfPwrId = new UILabel();
            hfPwrId.Text = sPwrId;
            hfPwrId.Tag = iStringRowPwrIdTagId * (iRowNo + 1) + (iStringRow + 1);
            hfPwrId.Hidden = true;
            arrItems [1] = hfPwrId;
            
            UILabel hfRowStatus = new UILabel();
            if (bNewRow)
            {
                hfRowStatus.Text = "2"; //2 means new
            }
            else
            {
                hfRowStatus.Text = "0";
            }
            
            hfRowStatus.Tag = iStringRowStatusTagId * (iRowNo + 1) + (iStringRow + 1);
            hfRowStatus.Hidden = true;
            arrItems [2] = hfRowStatus;
            
            UILabel hfAutoId = new UILabel();
            hfAutoId.Text = iAutoId.ToString();
            hfAutoId.Tag = iStringRowAutoIdTagId * (iRowNo + 1) + (iStringRow + 1);
            hfAutoId.Hidden = true;
            arrItems [3] = hfAutoId;
            
            UILabel hfMaximoAssetId = new UILabel();
            hfMaximoAssetId.Text = iMaximoAssetId.ToString();
            hfMaximoAssetId.Tag = iStringRowMaximoAssetIdTagId * (iRowNo + 1) + (iStringRow + 1);
            hfMaximoAssetId.Hidden = true;
            arrItems [4] = hfMaximoAssetId;
            
            iUtils.CreateFormGridItem lblBankNo = new iUtils.CreateFormGridItem();
            UIView lblBankNoVw = new UIView();
            lblBankNo.SetDimensions(0f, iHdrVert, 100f, iRowHeight, 2f, 2f, 2f, 2f);
            lblBankNo.SetLabelText(sBankNo);
            lblBankNo.SetBorderWidth(0.0f);
            lblBankNo.SetFontName("Verdana");
            lblBankNo.SetFontSize(12f);
            lblBankNo.SetTag(iBankNoTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblBankNo.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBankNo.SetCellColour("Pale Orange");
            }
            
            lblBankNoVw = lblBankNo.GetTextFieldCell();
            UITextField txtBankNoView = lblBankNo.GetTextFieldView();
            txtBankNoView.AutocorrectionType = UITextAutocorrectionType.No;
            txtBankNoView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtBankNoView.ReturnKeyType = UIReturnKeyType.Next;
            txtBankNoView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 1);};
            txtBankNoView.ShouldEndEditing += (sender) => {
                return ValidateBankNo(sender, 1, 0);};
            txtBankNoView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 1);};

            if(bReadOnly)
            {
                txtBankNoView.Enabled = false;
            }
            arrItems [5] = lblBankNoVw;
            
            UILabel hfCurrentBankNo = new UILabel();
            hfCurrentBankNo.Text = sBankNo;
            hfCurrentBankNo.Tag = iBankNoHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hfCurrentBankNo.Hidden = true;
            arrItems [6] = hfCurrentBankNo;
            
            iUtils.CreateFormGridItem btnBankNoSearch = new iUtils.CreateFormGridItem();
            UIView btnBankNoSearchVw = new UIView();
            btnBankNoSearch.SetDimensions(100f, iHdrVert, 60f, iRowHeight, 8f, 4f, 8f, 4f);
            btnBankNoSearch.SetLabelText("...");
            btnBankNoSearch.SetBorderWidth(0.0f);
            btnBankNoSearch.SetFontName("Verdana");
            btnBankNoSearch.SetFontSize(12f);
            btnBankNoSearch.SetTag(iBankNoSearchTagId * (iRowNo + 1) + (iStringRow + 1));
            if (iRowNo % 2 == 0)
            {
                btnBankNoSearch.SetCellColour("Pale Yellow");
            }
            else
            {
                btnBankNoSearch.SetCellColour("Pale Orange");
            }
            btnBankNoSearchVw = btnBankNoSearch.GetButtonCell();
            
            UIButton btnBankNoSearchButton = new UIButton();
            btnBankNoSearchButton = btnBankNoSearch.GetButton();
            btnBankNoSearchButton.TouchUpInside += (sender,e) => {
                OpenBankNoList(sender, e);};
            
            if(bReadOnly)
            {
                btnBankNoSearchButton.Enabled = false;
            }

            arrItems [7] = btnBankNoSearchVw;
            
            iUtils.CreateFormGridItem lblBankPlane = new iUtils.CreateFormGridItem();
            UIView lblBankPlaneVw = new UIView();
            lblBankPlane.SetDimensions(160f, iHdrVert, 40f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBankPlane.SetLabelText(sBankPlane);
            lblBankPlane.SetBorderWidth(0.0f);
            lblBankPlane.SetFontName("Verdana");
            lblBankPlane.SetFontSize(12f);
            lblBankPlane.SetTag(iBankPlaneTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblBankPlane.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBankPlane.SetCellColour("Pale Orange");
            }
            
            lblBankPlaneVw = lblBankPlane.GetLabelCell();
            arrItems [8] = lblBankPlaneVw;
            
            iUtils.CreateFormGridItem lblBankMake = new iUtils.CreateFormGridItem();
            UIView lblBankMakeVw = new UIView();
            lblBankMake.SetDimensions(199f, iHdrVert, 140f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBankMake.SetLabelText(sMake);
            lblBankMake.SetBorderWidth(0.0f);
            lblBankMake.SetFontName("Verdana");
            lblBankMake.SetFontSize(12f);
            lblBankMake.SetTag(iBankMakeTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblBankMake.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBankMake.SetCellColour("Pale Orange");
            }
            
            lblBankMakeVw = lblBankMake.GetLabelCell();
            arrItems [9] = lblBankMakeVw;
            
            iUtils.CreateFormGridItem btnMakeSearch = new iUtils.CreateFormGridItem();
            UIView btnMakeSearchVw = new UIView();
            btnMakeSearch.SetDimensions(339f, iHdrVert, 60f, iRowHeight, 8f, 4f, 8f, 4f);
            btnMakeSearch.SetLabelText("...");
            btnMakeSearch.SetBorderWidth(0.0f);
            btnMakeSearch.SetFontName("Verdana");
            btnMakeSearch.SetFontSize(12f);
            btnMakeSearch.SetTag(iBankMakeSearchTagId * (iRowNo + 1) + (iStringRow + 1));
            if (iRowNo % 2 == 0)
            {
                btnMakeSearch.SetCellColour("Pale Yellow");
            }
            else
            {
                btnMakeSearch.SetCellColour("Pale Orange");
            }
            btnMakeSearchVw = btnMakeSearch.GetButtonCell();
            
            UIButton btnMakeSearchButton = new UIButton();
            btnMakeSearchButton = btnMakeSearch.GetButton();
            btnMakeSearchButton.TouchUpInside += (sender,e) => {
                OpenMakeList(sender, e);};
            
            if(bReadOnly)
            {
                btnMakeSearchButton.Enabled = false;
            }
            arrItems [10] = btnMakeSearchVw;
            
            iUtils.CreateFormGridItem lblBankModel = new iUtils.CreateFormGridItem();
            UIView lblBankModelVw = new UIView();
            lblBankModel.SetDimensions(398f, iHdrVert, 240f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBankModel.SetLabelText(sModel);
            lblBankModel.SetBorderWidth(0.0f);
            lblBankModel.SetFontName("Verdana");
            lblBankModel.SetFontSize(12f);
            lblBankModel.SetTag(iBankModelTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblBankModel.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBankModel.SetCellColour("Pale Orange");
            }
            
            lblBankModelVw = lblBankModel.GetLabelCell();
            arrItems [11] = lblBankModelVw;
            
            UILabel hfSPN = new UILabel();
            hfSPN.Text = sSPN;
            hfSPN.Tag = iSPNHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hfSPN.Hidden = true;
            arrItems [12] = hfSPN;
            
            iUtils.CreateFormGridItem btnModelSearch = new iUtils.CreateFormGridItem();
            UIView btnModelSearchVw = new UIView();
            btnModelSearch.SetDimensions(638f, iHdrVert, 60f, iRowHeight, 8f, 4f, 8f, 4f);
            btnModelSearch.SetLabelText("...");
            btnModelSearch.SetBorderWidth(0.0f);
            btnModelSearch.SetFontName("Verdana");
            btnModelSearch.SetFontSize(12f);
            btnModelSearch.SetTag(iBankModelSearchTagId * (iRowNo + 1) + (iStringRow + 1));
            if (iRowNo % 2 == 0)
            {
                btnModelSearch.SetCellColour("Pale Yellow");
            }
            else
            {
                btnModelSearch.SetCellColour("Pale Orange");
            }
            btnModelSearchVw = btnModelSearch.GetButtonCell();
            
            UIButton btnModelSearchButton = new UIButton();
            btnModelSearchButton = btnModelSearch.GetButton();
            btnModelSearchButton.TouchUpInside += (sender,e) => {
                OpenModelList(sender, e);};
            
            if(bReadOnly)
            {
                btnModelSearchButton.Enabled = false;
            }
            arrItems [13] = btnModelSearchVw;
            
            iUtils.CreateFormGridItem txtDOM = new iUtils.CreateFormGridItem();
            UIView txtDOMVw = new UIView();
            txtDOM.SetDimensions(697f, iHdrVert, 80f, iRowHeight, 2f, 2f, 2f, 2f);
            if (sDOM == "0")
            {
                sDOM = "01/01/1900";
            }
            string sDOMDisplay;
            if(sDOM == "")
            {
                sDOMDisplay = sDOM;
            }
            else
            {
                DateTime dtDOM = Convert.ToDateTime(sDOM);
                sDOMDisplay = dt.Get_Date_String(dtDOM, "dd/mm/yy");
            }
            txtDOM.SetLabelText(sDOMDisplay);
            txtDOM.SetBorderWidth(0.0f);
            txtDOM.SetFontName("Verdana");
            txtDOM.SetFontSize(12f);
            txtDOM.SetTag(iBankDOMTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                txtDOM.SetCellColour("Pale Yellow");
            }
            else
            {
                txtDOM.SetCellColour("Pale Orange");
            }
            
            txtDOMVw = txtDOM.GetTextFieldCell();
            UITextField txtDOMTextView = txtDOM.GetTextFieldView();
            txtDOMTextView.AutocorrectionType = UITextAutocorrectionType.No;
            txtDOMTextView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtDOMTextView.ReturnKeyType = UIReturnKeyType.Next;
            txtDOMTextView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 2);};
            txtDOMTextView.ShouldEndEditing += (sender) => {
                return ValidateDOM(sender, 0);};
            txtDOMTextView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 2);};
            
            if(bReadOnly)
            {
                txtDOMTextView.Enabled = false;
            }
            arrItems[14] = txtDOMVw;
            
            UILabel hfCurrentDOM = new UILabel();
            hfCurrentDOM.Text = sDOMDisplay;
            hfCurrentDOM.Tag = iHiddenDOMTagId * (iRowNo + 1) + (iStringRow + 1);
            hfCurrentDOM.Hidden = true;
            arrItems[15] = hfCurrentDOM;

            iUtils.CreateFormGridItem lblBankFuseOrCB = new iUtils.CreateFormGridItem();
            UIView lblBankFuseOrCBVw = new UIView();
            lblBankFuseOrCB.SetDimensions(776f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBankFuseOrCB.SetLabelText(sFuseOrCB);
            lblBankFuseOrCB.SetBorderWidth(0.0f);
            lblBankFuseOrCB.SetFontName("Verdana");
            lblBankFuseOrCB.SetFontSize(12f);
            lblBankFuseOrCB.SetTag(iBankFuseOrCBTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblBankFuseOrCB.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBankFuseOrCB.SetCellColour("Pale Orange");
            }
            
            lblBankFuseOrCBVw = lblBankFuseOrCB.GetLabelCell();
            arrItems[16] = lblBankFuseOrCBVw;
            
            iUtils.CreateFormGridItem btnFuseOrCBSearch = new iUtils.CreateFormGridItem();
            UIView btnFuseOrCBSearchVw = new UIView();
            btnFuseOrCBSearch.SetDimensions(826f, iHdrVert, 50f, iRowHeight, 3f, 4f, 3f, 4f);
            btnFuseOrCBSearch.SetLabelText("...");
            btnFuseOrCBSearch.SetBorderWidth(0.0f);
            btnFuseOrCBSearch.SetFontName("Verdana");
            btnFuseOrCBSearch.SetFontSize(12f);
            btnFuseOrCBSearch.SetTag(iBankFuseOrCBSearchTagId * (iRowNo + 1) + (iStringRow + 1));
            if (iRowNo % 2 == 0)
            {
                btnFuseOrCBSearch.SetCellColour("Pale Yellow");
            }
            else
            {
                btnFuseOrCBSearch.SetCellColour("Pale Orange");
            }
            btnFuseOrCBSearchVw = btnFuseOrCBSearch.GetButtonCell();
            
            UIButton btnFuseOrCBSearchButton = new UIButton();
            btnFuseOrCBSearchButton = btnFuseOrCBSearch.GetButton();
            btnFuseOrCBSearchButton.TouchUpInside += (sender,e) => {
                OpenFuseOrCBList(sender, e);};
            
            if(bReadOnly)
            {
                btnFuseOrCBSearchButton.Enabled = false;
            }
            arrItems[17] = btnFuseOrCBSearchVw;
            
            iUtils.CreateFormGridItem txtRating = new iUtils.CreateFormGridItem();
            UIView txtRatingVw = new UIView();
            txtRating.SetDimensions(876f, iHdrVert, 125f, iRowHeight, 2f, 2f, 2f, 2f);
            txtRating.SetLabelText(sRatingAmps);
            txtRating.SetBorderWidth(0.0f);
            txtRating.SetFontName("Verdana");
            txtRating.SetFontSize(12f);
            txtRating.SetTag(iBankRatingTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                txtRating.SetCellColour("Pale Yellow");
            }
            else
            {
                txtRating.SetCellColour("Pale Orange");
            }
            
            txtRatingVw = txtRating.GetTextFieldCell();
            UITextField txtRatingTextView = txtRating.GetTextFieldView();
            txtRatingTextView.AutocorrectionType = UITextAutocorrectionType.No;
            txtRatingTextView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtRatingTextView.ReturnKeyType = UIReturnKeyType.Next;
            txtRatingTextView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 3);};
            txtRatingTextView.ShouldEndEditing += (sender) => {
                return ValidateRating(sender, 0);};
            txtRatingTextView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 3);};
            
            if(bReadOnly)
            {
                txtRatingTextView.Enabled = false;
            }
            arrItems[18] = txtRatingVw;
            
            UILabel hfCurrentRating = new UILabel();
            hfCurrentRating.Text = sRatingAmps;
            hfCurrentRating.Tag = iHiddenRatingTagId * (iRowNo + 1) + (iStringRow + 1);
            hfCurrentRating.Hidden = true;
            arrItems[19] = hfCurrentRating;

            hdrRow.AddSubviews(arrItems);
            
            iHeightToAdd += iRowHeight; 
            iHdrVert += iRowHeight; 
            
            //***************************************************************//
            //              2nd Row                                          //
            //***************************************************************//
            iUtils.CreateFormGridItem lblFloor = new iUtils.CreateFormGridItem();
            UIView lblFloorVw = new UIView();
            lblFloor.SetDimensions(0f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblFloor.SetLabelText(sFloor);
            lblFloor.SetBorderWidth(0.0f);
            lblFloor.SetFontName("Verdana");
            lblFloor.SetFontSize(12f);
            lblFloor.SetTag(iFloorTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblFloor.SetCellColour("Pale Yellow");
            }
            else
            {
                lblFloor.SetCellColour("Pale Orange");
            }
            
            lblFloorVw = lblFloor.GetTextFieldCell();
            UITextField txtFloorView = lblFloor.GetTextFieldView();
            txtFloorView.AutocorrectionType = UITextAutocorrectionType.No;
            txtFloorView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtFloorView.ReturnKeyType = UIReturnKeyType.Next;
            txtFloorView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 4);};
            txtFloorView.ShouldEndEditing += (sender) => {
                return ValidateFloor(sender, 0);};
            txtFloorView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 4);};
            
            if(bReadOnly)
            {
                txtFloorView.Enabled = false;
            }
            arrItems2 [0] = lblFloorVw;
            
            UILabel hfCurrentFloor = new UILabel();
            hfCurrentFloor.Text = sFloor;
            hfCurrentFloor.Tag = iFloorHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hfCurrentFloor.Hidden = true;
            arrItems2 [1] = hfCurrentFloor;
            
            iUtils.CreateFormGridItem lblSuite = new iUtils.CreateFormGridItem();
            UIView lblSuiteVw = new UIView();
            lblSuite.SetDimensions(50f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSuite.SetLabelText(sSuite);
            lblSuite.SetBorderWidth(0.0f);
            lblSuite.SetFontName("Verdana");
            lblSuite.SetFontSize(12f);
            lblSuite.SetTag(iSuiteTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblSuite.SetCellColour("Pale Yellow");
            }
            else
            {
                lblSuite.SetCellColour("Pale Orange");
            }
            
            lblSuiteVw = lblSuite.GetTextFieldCell();
            UITextField txtSuiteView = lblSuite.GetTextFieldView();
            txtSuiteView.AutocorrectionType = UITextAutocorrectionType.No;
            txtSuiteView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSuiteView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 5);};
            txtSuiteView.ReturnKeyType = UIReturnKeyType.Next;
            txtSuiteView.ShouldEndEditing += (sender) => {
                return ValidateSuite(sender, 0);};
            txtSuiteView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 5);};
            
            if(bReadOnly)
            {
                txtSuiteView.Enabled = false;
            }
            arrItems2 [2] = lblSuiteVw;
            
            UILabel hfCurrentSuite = new UILabel();
            hfCurrentSuite.Text = sSuite;
            hfCurrentSuite.Tag = iSuiteHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hfCurrentSuite.Hidden = true;
            arrItems2 [3] = hfCurrentSuite;
            
            iUtils.CreateFormGridItem lblRack = new iUtils.CreateFormGridItem();
            UIView lblRackVw = new UIView();
            lblRack.SetDimensions(100f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblRack.SetLabelText(sRack);
            lblRack.SetBorderWidth(0.0f);
            lblRack.SetFontName("Verdana");
            lblRack.SetFontSize(12f);
            lblRack.SetTag(iRackTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblRack.SetCellColour("Pale Yellow");
            }
            else
            {
                lblRack.SetCellColour("Pale Orange");
            }
            
            lblRackVw = lblRack.GetTextFieldCell();
            UITextField txtRackView = lblRack.GetTextFieldView();
            txtRackView.AutocorrectionType = UITextAutocorrectionType.No;
            txtRackView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtRackView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 6);};
            txtRackView.ReturnKeyType = UIReturnKeyType.Next;
            txtRackView.ShouldEndEditing += (sender) => {
                return ValidateRack(sender, 0);};
            txtRackView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 6);};
            
            if(bReadOnly)
            {
                txtRackView.Enabled = false;
            }
            arrItems2 [4] = lblRackVw;
            
            UILabel hfCurrentRack = new UILabel();
            hfCurrentRack.Text = sRack;
            hfCurrentRack.Tag = iRackHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hfCurrentRack.Hidden = true;
            arrItems2 [5] = hfCurrentRack;
            
            iUtils.CreateFormGridItem lblSubRack = new iUtils.CreateFormGridItem();
            UIView lblSubRackVw = new UIView();
            lblSubRack.SetDimensions(150f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSubRack.SetLabelText(sSubRack);
            lblSubRack.SetBorderWidth(0.0f);
            lblSubRack.SetFontName("Verdana");
            lblSubRack.SetFontSize(12f);
            lblSubRack.SetTag(iSubRackTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblSubRack.SetCellColour("Pale Yellow");
            }
            else
            {
                lblSubRack.SetCellColour("Pale Orange");
            }
            
            lblSubRackVw = lblSubRack.GetTextFieldCell();
            UITextField txtSubRackView = lblSubRack.GetTextFieldView();
            txtSubRackView.AutocorrectionType = UITextAutocorrectionType.No;
            txtSubRackView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSubRackView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 7);};
            txtSubRackView.ReturnKeyType = UIReturnKeyType.Next;
            txtSubRackView.ShouldEndEditing += (sender) => {
                return ValidateSubRack(sender, 0);};
            txtSubRackView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 7);};
            
            if(bReadOnly)
            {
                txtSubRackView.Enabled = false;
            }
            arrItems2 [6] = lblSubRackVw;
            
            UILabel hfCurrentSubRack = new UILabel();
            hfCurrentSubRack.Text = sSubRack;
            hfCurrentSubRack.Tag = iSubRackHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hfCurrentSubRack.Hidden = true;
            arrItems2 [7] = hfCurrentSubRack;
            
            iUtils.CreateFormGridItem radEquipType = new iUtils.CreateFormGridItem();
            UIView radEquipTypeVw = new UIView();
            radEquipType.SetDimensions(199f, iHdrVert, 200f, iRowHeight * 2, 8f, iRowHeight / 2f, 8f, iRowHeight / 2f);
            radEquipType.SetFontName("Verdana");
            radEquipType.SetFontSize(12f);
            radEquipType.SetTag(iEquipTypeTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                radEquipType.SetCellColour("Pale Yellow");
            }
            else
            {
                radEquipType.SetCellColour("Pale Orange");
            }
            
            radEquipTypeVw = radEquipType.GetRadioButtonCell();
            
            UISegmentedControl radEquipTypeRadio = new UISegmentedControl();
            radEquipTypeRadio = radEquipType.GetRadioGroup();
            radEquipTypeRadio.ValueChanged += (sender,e) => {
                SetStringEquipTypeChanged(sender, e);};
            
            radEquipTypeRadio.InsertSegment("New", 0, false);
            radEquipTypeRadio.InsertSegment("Used", 1, false);
            
            if (sEquipType == "N")
            {
                radEquipTypeRadio.SelectedSegment = 0;
            }
            else
            {
                radEquipTypeRadio.SelectedSegment = 1;
            }
            
            if (bNewRow || iMaximoAssetId < 0)
            {
                radEquipTypeRadio.Enabled = true;
            }
            else
            {
                radEquipTypeRadio.Enabled = false;
            }
            
            if(bReadOnly)
            {
                radEquipTypeRadio.Enabled = false;
            }

            arrItems2 [8] = radEquipTypeVw;
            
            iUtils.CreateFormGridItem lblSerialNo = new iUtils.CreateFormGridItem();
            UIView lblSerialNoVw = new UIView();
            lblSerialNo.SetDimensions(398f, iHdrVert, 300f, iRowHeight * 2f, 2f, iRowHeight / 2f, 2f, iRowHeight / 2f);
            lblSerialNo.SetLabelText(sSerialNo);
            lblSerialNo.SetBorderWidth(0.0f);
            lblSerialNo.SetFontName("Verdana");
            lblSerialNo.SetFontSize(12f);
            lblSerialNo.SetTag(iSerialNoTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                lblSerialNo.SetCellColour("Pale Yellow");
            }
            else
            {
                lblSerialNo.SetCellColour("Pale Orange");
            }
            
            lblSerialNoVw = lblSerialNo.GetTextFieldCell();
            UITextField txtSerialNoView = lblSerialNo.GetTextFieldView();
            txtSerialNoView.AutocorrectionType = UITextAutocorrectionType.No;
            txtSerialNoView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSerialNoView.ReturnKeyType = UIReturnKeyType.Next;
            txtSerialNoView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 8);};
            txtSerialNoView.ShouldEndEditing += (sender) => {
                return ValidateSerialNo(sender, 0);};
            txtSerialNoView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 8);};
            
            if(bReadOnly)
            {
                txtSerialNoView.Enabled = false;
            }
            arrItems2 [9] = lblSerialNoVw;
            
            UILabel hfCurrentSerialNo = new UILabel();
            hfCurrentSerialNo.Text = sSerialNo;
            hfCurrentSerialNo.Tag = iHiddenSerialNoTagId * (iRowNo + 1) + (iStringRow + 1);
            hfCurrentSerialNo.Hidden = true;
            arrItems2 [10] = hfCurrentSerialNo;

            iUtils.CreateFormGridItem btnLinkTest = new iUtils.CreateFormGridItem();
            UIView btnLinkTestVw = new UIView();
            btnLinkTest.SetDimensions(697f, iHdrVert, 80f, iRowHeight * 2f, 8f, iRowHeight / 2f, 8f, iRowHeight / 2f);
            btnLinkTest.SetLabelText("Link Test");
            btnLinkTest.SetBorderWidth(0.0f);
            btnLinkTest.SetFontName("Verdana");
            btnLinkTest.SetFontSize(12f);
            btnLinkTest.SetTag(iLinkTestBtnTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                btnLinkTest.SetCellColour("Pale Yellow");
            }
            else
            {
                btnLinkTest.SetCellColour("Pale Orange");
            }
            
            btnLinkTestVw = btnLinkTest.GetButtonCell();
            
            UIButton btnLinkTestButton = new UIButton();
            btnLinkTestButton = btnLinkTest.GetButton();
            btnLinkTestButton.TouchUpInside += (sender,e) => {
                OpenLinkTest(sender, e);};
            
            if(bReadOnly)
            {
                btnLinkTestButton.Enabled = false;
            }
            arrItems2 [11] = btnLinkTestVw;
            
            UILabel hfLinkTestStatus = new UILabel();
            hfLinkTestStatus.Text = iLinkTestStatus.ToString();
            hfLinkTestStatus.Tag = iLinkTestHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hfLinkTestStatus.Hidden = true;
            arrItems2 [12] = hfLinkTestStatus;
            
            iUtils.CreateFormGridItem btn20MinTest = new iUtils.CreateFormGridItem();
            UIView btn20MinTestVw = new UIView();
            btn20MinTest.SetDimensions(776f, iHdrVert, 100f, iRowHeight * 2f, 8f, iRowHeight / 2f, 8f, iRowHeight / 2f);
            btn20MinTest.SetLabelText("20Min Test");
            btn20MinTest.SetBorderWidth(0.0f);
            btn20MinTest.SetFontName("Verdana");
            btn20MinTest.SetFontSize(12f);
            btn20MinTest.SetTag(i20MinTestBtnTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                btn20MinTest.SetCellColour("Pale Yellow");
            }
            else
            {
                btn20MinTest.SetCellColour("Pale Orange");
            }
            
            btn20MinTestVw = btn20MinTest.GetButtonCell();
            
            UIButton btn20MinTestButton = new UIButton();
            btn20MinTestButton = btn20MinTest.GetButton();
            btn20MinTestButton.TouchUpInside += (sender,e) => {
                Open20MinTest(sender, e);};
            
            if(bReadOnly)
            {
                btn20MinTestButton.Enabled = false;
            }
            arrItems2 [13] = btn20MinTestVw;
            
            UILabel hf20MinTestStatus = new UILabel();
            hf20MinTestStatus.Text = i20MinTest.ToString();
            hf20MinTestStatus.Tag = i20MinTestHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hf20MinTestStatus.Hidden = true;
            arrItems2 [14] = hf20MinTestStatus;
            
            iUtils.CreateFormGridItem btnDelete = new iUtils.CreateFormGridItem();
            UIView btnDeleteVw = new UIView();
            btnDelete.SetDimensions(875f, iHdrVert, 125f, iRowHeight * 2f, 8f, iRowHeight / 2f, 8f, iRowHeight / 2f);
            btnDelete.SetLabelText("Delete");
            btnDelete.SetBorderWidth(0.0f);
            btnDelete.SetFontName("Verdana");
            btnDelete.SetFontSize(12f);
            btnDelete.SetTag(iDeleteBatteryStringBtnTagId * (iRowNo + 1) + (iStringRow + 1));
            
            if (iRowNo % 2 == 0)
            {
                btnDelete.SetCellColour("Pale Yellow");
            }
            else
            {
                btnDelete.SetCellColour("Pale Orange");
            }
            
            btnDeleteVw = btnDelete.GetButtonCell();
            
            UIButton btnDeleteButton = new UIButton();
            btnDeleteButton = btnDelete.GetButton();
            btnDeleteButton.TouchUpInside += (sender,e) => {
                DeleteBatteryString(sender, e);};
            
            if (iMaximoAssetId > 0)
            {
                btnDeleteButton.Enabled = false;
            }
            if(bReadOnly)
            {
                btnDeleteButton.Enabled = false;
            }
            arrItems2[15] = btnDeleteVw;
            
            hdrRow.AddSubviews(arrItems2);
            
            iHeightToAdd += iRowHeight; 
            iHdrVert += iRowHeight; 
            
            //***************************************************************//
            //              3rd Row                                          //
            //***************************************************************//
            
            iUtils.CreateFormGridItem btnFloorSearch = new iUtils.CreateFormGridItem();
            UIView btnFloorSearchVw = new UIView();
            btnFloorSearch.SetDimensions(0f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
            btnFloorSearch.SetLabelText("...");
            btnFloorSearch.SetBorderWidth(0.0f);
            btnFloorSearch.SetFontName("Verdana");
            btnFloorSearch.SetFontSize(12f);
            btnFloorSearch.SetTag(iFloorSearchTagId * (iRowNo+1) + (iStringRow+1));
            if (iRowNo % 2 == 0)
            {
                btnFloorSearch.SetCellColour("Pale Yellow");
            }
            else
            {
                btnFloorSearch.SetCellColour("Pale Orange");
            }
            btnFloorSearchVw = btnFloorSearch.GetButtonCell();
            
            UIButton btnFloorSearchButton = new UIButton();
            btnFloorSearchButton = btnFloorSearch.GetButton();
            btnFloorSearchButton.TouchUpInside += (sender,e) => {OpenSearchView(sender, e, 1);};
            
            if(bReadOnly)
            {
                btnFloorSearchButton.Enabled = false;
            }
            arrItems3[0] = btnFloorSearchVw;
            
            iUtils.CreateFormGridItem btnSuiteSearch = new iUtils.CreateFormGridItem();
            UIView btnSuiteSearchVw = new UIView();
            btnSuiteSearch.SetDimensions(50f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
            btnSuiteSearch.SetLabelText("...");
            btnSuiteSearch.SetBorderWidth(0.0f);
            btnSuiteSearch.SetFontName("Verdana");
            btnSuiteSearch.SetFontSize(12f);
            btnSuiteSearch.SetTag(iSuiteSearchTagId * (iRowNo+1) + (iStringRow+1));
            if (iRowNo % 2 == 0)
            {
                btnSuiteSearch.SetCellColour("Pale Yellow");
            }
            else
            {
                btnSuiteSearch.SetCellColour("Pale Orange");
            }
            btnSuiteSearchVw = btnSuiteSearch.GetButtonCell();
            
            UIButton btnSuiteSearchButton = new UIButton();
            btnSuiteSearchButton = btnSuiteSearch.GetButton();
            btnSuiteSearchButton.TouchUpInside += (sender,e) => {OpenSearchView(sender, e, 2);};
            
            if(bReadOnly)
            {
                btnSuiteSearchButton.Enabled = false;
            }
            arrItems3[1] = btnSuiteSearchVw;
            
            iUtils.CreateFormGridItem btnRackSearch = new iUtils.CreateFormGridItem();
            UIView btnRackSearchVw = new UIView();
            btnRackSearch.SetDimensions(100f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
            btnRackSearch.SetLabelText("...");
            btnRackSearch.SetBorderWidth(0.0f);
            btnRackSearch.SetFontName("Verdana");
            btnRackSearch.SetFontSize(12f);
            btnRackSearch.SetTag(iRackSearchTagId * (iRowNo+1) + (iStringRow+1));
            if (iRowNo % 2 == 0)
            {
                btnRackSearch.SetCellColour("Pale Yellow");
            }
            else
            {
                btnRackSearch.SetCellColour("Pale Orange");
            }
            btnRackSearchVw = btnRackSearch.GetButtonCell();
            
            UIButton btnRackSearchButton = new UIButton();
            btnRackSearchButton = btnRackSearch.GetButton();
            btnRackSearchButton.TouchUpInside += (sender,e) => {OpenSearchView(sender, e, 3);};
            
            if(bReadOnly)
            {
                btnRackSearchButton.Enabled = false;
            }
            arrItems3[2] = btnRackSearchVw;
            
            iUtils.CreateFormGridItem btnSubRackSearch = new iUtils.CreateFormGridItem();
            UIView btnSubRackSearchVw = new UIView();
            btnSubRackSearch.SetDimensions(150f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
            btnSubRackSearch.SetLabelText("...");
            btnSubRackSearch.SetBorderWidth(0.0f);
            btnSubRackSearch.SetFontName("Verdana");
            btnSubRackSearch.SetFontSize(12f);
            btnSubRackSearch.SetTag(iSubRackSearchTagId * (iRowNo+1) + (iStringRow+1));
            if (iRowNo % 2 == 0)
            {
                btnSubRackSearch.SetCellColour("Pale Yellow");
            }
            else
            {
                btnSubRackSearch.SetCellColour("Pale Orange");
            }
            btnSubRackSearchVw = btnSubRackSearch.GetButtonCell();
            
            UIButton btnSubRackSearchButton = new UIButton();
            btnSubRackSearchButton = btnSubRackSearch.GetButton();
            btnSubRackSearchButton.TouchUpInside += (sender,e) => {OpenSearchView(sender, e, 4);};
            
            if(bReadOnly)
            {
                btnSubRackSearchButton.Enabled = false;
            }
            arrItems3[3] = btnSubRackSearchVw;
            
            hdrRow.AddSubviews(arrItems3);
            
            iHeightToAdd += iRowHeight; 
            iHdrVert += iRowHeight; 
            
            //Now draw a 1 pixel horizontal line
            UIView vwHorizLine = new UIView();
            vwHorizLine.Frame = new RectangleF(0f,iHdrVert, 1000f, 1f);
            vwHorizLine.BackgroundColor = UIColor.FromRGBA(0,0,0,255);
            
            iHeightToAdd += 1f;
            iHdrVert += 1f;
            
            hdrRow.AddSubview(vwHorizLine);
            
            return hdrRow;
        }
        
        
        public void OpenBankNoList (object sender, EventArgs e)
        {
            UIButton btnBankNoSearch = (UIButton)sender;
            ScreenUtils scnUtils = new ScreenUtils ();
            scnUtils.GetAbsolutePosition (btnBankNoSearch);
            float iTop = scnUtils.GetPositionTop ();
            float iLeft = scnUtils.GetPositionLeft ();
            int iBtnTagId = btnBankNoSearch.Tag;
            int iPwrIdRow = iBtnTagId / iBankNoSearchTagId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iBankNoSearchTagId);
            int iSectionCounterTagId = iStringRowSectionCounterTagId * iPwrIdRow  + iStringRow;
            UILabel hfSectionCounter = (UILabel)View.ViewWithTag (iSectionCounterTagId);
            int iSectionCounterId = Convert.ToInt32 (hfSectionCounter.Text);
            
            clsTabletDB.ITPValidHierarchy ITPHierarchy = new clsTabletDB.ITPValidHierarchy();
            string[] sBatteryBankNos = ITPHierarchy.GetValidHierarchy(6);
            
            //Create a list and convert the string array to the list. Why the system cannot take a simple string array is beyond me!!!
            List<string> listBankNo = new List<string> ();
            Array.ForEach (sBatteryBankNos, value => listBankNo.Add (value.ToString ()));
            
            TableViewSource tabdata = new TableViewSource (listBankNo, true);
            tabdata.SetFont("Verdana",10f);
            UITableView cmbBankNo = new UITableView ();
            
            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                cmbBankNo.Frame = new RectangleF(iLeft, iTop - 190f, 90f, 200f);
            } 
            else 
            {
                cmbBankNo.Frame = new RectangleF(iLeft, iTop, 90f, 200f);
            }
            
            tabdata.SetParent(cmbBankNo);
            tabdata.SetUpdateFieldType("UITextField");
            UITextField lblVwUpdate = (UITextField)View.ViewWithTag (iBankNoTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetTextFieldToUpdate(lblVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);
            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((iSectionCounterId + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);
            
            cmbBankNo.Source = tabdata;
            iUtils.SESTable thistable = new iUtils.SESTable();
            string sSelectedValue = lblVwUpdate.Text;
            thistable.SetTableSelectedText(cmbBankNo, sSelectedValue, sBatteryBankNos, true);
            
            //Get the main scroll view
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            scrollVw.AddSubview(cmbBankNo);
        }
        
        public void OpenMakeList (object sender, EventArgs e)
        {
            UIButton btnMakeSearch = (UIButton)sender;
            btnMakeSearch.Enabled = false;
            ScreenUtils scnUtils = new ScreenUtils ();
            scnUtils.GetAbsolutePosition (btnMakeSearch);
            float iTop = scnUtils.GetPositionTop ();
            float iLeft = scnUtils.GetPositionLeft ();
            int iBtnTagId = btnMakeSearch.Tag;
            int iPwrIdRow = iBtnTagId / iBankMakeSearchTagId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iBankMakeSearchTagId);
            int iSectionCounterTagId = iStringRowSectionCounterTagId * iPwrIdRow + iStringRow;
            UILabel hfSectionCounter = (UILabel)View.ViewWithTag (iSectionCounterTagId);
            int iSectionCounterId = Convert.ToInt32(hfSectionCounter.Text);
            
            
            //Create a list and convert the string array to the list. Why the system cannot take a simple string arary is beyond me!!!
            List<string> mylist = new List<string> ();
            Array.ForEach (m_sBatteryMakes, value => mylist.Add (value.ToString ()));
            
            TableViewSource tabdata = new TableViewSource (mylist, true);
            tabdata.SetFont("Verdana",10f);
            UITableView cmbMake = new UITableView ();
            
            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                cmbMake.Frame = new RectangleF(iLeft, iTop - 190f, 290f, 200f);
            } 
            else 
            {
                cmbMake.Frame = new RectangleF(iLeft, iTop, 290f, 200f);
            }
            
            tabdata.SetParent(cmbMake);
            tabdata.SetUpdateFieldType("UILabel");
            UILabel txtVwUpdate = (UILabel)View.ViewWithTag (iBankMakeTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetLabelViewToUpdate(txtVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);
            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((iSectionCounterId + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);
            UIButton btnSectionSave = (UIButton)View.ViewWithTag ((iSectionCounterId + 1) * iSaveSectionBtnTagId);
            tabdata.SetSectionSaveButton(btnSectionSave);
            UILabel lblViewModel = (UILabel)View.ViewWithTag (iBankModelTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetMakePostUpdate(1, lblViewModel, btnMakeSearch);
            
            cmbMake.Source = tabdata;
            iUtils.SESTable thistable = new iUtils.SESTable();
            string sSelectedValue = txtVwUpdate.Text;
            thistable.SetTableSelectedText(cmbMake, sSelectedValue, m_sBatteryMakes, true);
            
            //Get the main scroll view
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            scrollVw.AddSubview(cmbMake);
        }
        
        public void OpenModelList (object sender, EventArgs e)
        {
            UIButton btnModelSearch = (UIButton)sender;
            btnModelSearch.Enabled = false;
            ScreenUtils scnUtils = new ScreenUtils ();
            scnUtils.GetAbsolutePosition (btnModelSearch);
            float iTop = scnUtils.GetPositionTop ();
            float iLeft = scnUtils.GetPositionLeft ();
            int iBtnTagId = btnModelSearch.Tag;
            int iPwrIdRow = iBtnTagId / iBankModelSearchTagId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iBankModelSearchTagId);
            int iSectionCounterTagId = iStringRowSectionCounterTagId * iPwrIdRow + iStringRow;
            UILabel hfSectionCounter = (UILabel)View.ViewWithTag (iSectionCounterTagId);
            int iSectionCounterId = Convert.ToInt32 (hfSectionCounter.Text);
            UILabel lblSupplier = (UILabel)View.ViewWithTag (iBankMakeTagId * (iPwrIdRow) + (iStringRow));
            string sSupplier = lblSupplier.Text;
            UIButton btnMakeSearch = (UIButton)View.ViewWithTag (iBankMakeSearchTagId * (iPwrIdRow) + (iStringRow));
            btnMakeSearch.Enabled = false;

            if (sSupplier == "") 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("You must select a make before you can select a model");
                return;
            }
            
            clsTabletDB.ITPInventory ITPInventory = new clsTabletDB.ITPInventory ();
            string[] sBatteryModels = ITPInventory.GetBatteryModels (sSupplier);
            m_sBatteryModels = sBatteryModels;

            //Create a list and convert the string array to the list. Why the system cannot take a simple string array is beyond me!!!
            List<string> listModel = new List<string> ();
            Array.ForEach (m_sBatteryModels, value => listModel.Add (value.ToString ()));
            
            TableViewSource tabdata = new TableViewSource (listModel, true);
            tabdata.SetFont("Verdana",10f);
            UITableView cmbModel = new UITableView ();
            
            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                cmbModel.Frame = new RectangleF(iLeft, iTop - 190f, 290f, 200f);
            } 
            else 
            {
                cmbModel.Frame = new RectangleF(iLeft, iTop, 290f, 200f);
            }
            
            tabdata.SetParent(cmbModel);
            tabdata.SetUpdateFieldType("UILabel");
            UILabel lblVwUpdate = (UILabel)View.ViewWithTag (iBankModelTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetLabelViewToUpdate(lblVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);
            UILabel hfRowStatus = (UILabel)View.ViewWithTag (iStringRowStatusTagId * (iPwrIdRow) + (iStringRow));
            UILabel lblSPN = (UILabel)View.ViewWithTag (iSPNHiddenTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetModelPostUpdate(6, hfRowStatus, lblSPN, sSupplier, btnMakeSearch, btnModelSearch);
            
            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UIButton btnSectionSave = (UIButton)View.ViewWithTag ((iSectionCounterId + 1) * iSaveSectionBtnTagId);
            tabdata.SetSectionSaveButton(btnSectionSave);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((iSectionCounterId + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);
            
            cmbModel.Source = tabdata;
            iUtils.SESTable thistable = new iUtils.SESTable();
            string sSelectedValue = lblVwUpdate.Text;
            thistable.SetTableSelectedText(cmbModel, sSelectedValue, m_sBatteryModels, true);
            
            //Get the main scroll view
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            scrollVw.AddSubview(cmbModel);
        }
        
        public void OpenFuseOrCBList (object sender, EventArgs e)
        {
            UIButton btnFuseOrCBSearch = (UIButton)sender;
            ScreenUtils scnUtils = new ScreenUtils ();
            scnUtils.GetAbsolutePosition (btnFuseOrCBSearch);
            float iTop = scnUtils.GetPositionTop ();
            float iLeft = scnUtils.GetPositionLeft ();
            int iBtnTagId = btnFuseOrCBSearch.Tag;
            int iPwrIdRow = iBtnTagId / iBankFuseOrCBSearchTagId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iBankFuseOrCBSearchTagId);
            int iSectionCounterTagId = iStringRowSectionCounterTagId * iPwrIdRow  + iStringRow;
            UILabel hfSectionCounter = (UILabel)View.ViewWithTag (iSectionCounterTagId);
            int iSectionCounterId = Convert.ToInt32 (hfSectionCounter.Text);
            
            clsTabletDB.ITPBatteryFuseTypes ITPFuseTypes = new clsTabletDB.ITPBatteryFuseTypes();
            string[] sBatteryFuseOrCBs = ITPFuseTypes.GetBatteryFuseTypes();
            
            //Create a list and convert the string array to the list. Why the system cannot take a simple string array is beyond me!!!
            List<string> listFuseOrCB = new List<string> ();
            Array.ForEach (sBatteryFuseOrCBs, value => listFuseOrCB.Add (value.ToString ()));
            
            TableViewSource tabdata = new TableViewSource (listFuseOrCB, true);
            tabdata.SetFont("Verdana",10f);
            UITableView cmbFuseOrCB = new UITableView ();
            
            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                cmbFuseOrCB.Frame = new RectangleF(iLeft, iTop - 190f, 90f, 100f);
            } 
            else 
            {
                cmbFuseOrCB.Frame = new RectangleF(iLeft, iTop, 90f, 100f);
            }
            
            tabdata.SetParent(cmbFuseOrCB);
            tabdata.SetUpdateFieldType("UILabel");
            UILabel lblVwUpdate = (UILabel)View.ViewWithTag (iBankFuseOrCBTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetLabelViewToUpdate(lblVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);
            UILabel hfRowStatus = (UILabel)View.ViewWithTag (iStringRowStatusTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetFusePostUpdate(7, hfRowStatus);
            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((iSectionCounterId + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);
            
            cmbFuseOrCB.Source = tabdata;
            iUtils.SESTable thistable = new iUtils.SESTable();
            string sSelectedValue = lblVwUpdate.Text;
            thistable.SetTableSelectedText(cmbFuseOrCB, sSelectedValue, sBatteryFuseOrCBs, true);
            
            //Get the main scroll view
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            scrollVw.AddSubview(cmbFuseOrCB);
        }
        
        public void OpenSearchView (object sender, EventArgs e, int iSearchType)
        {
            UIButton btnSearch = (UIButton)sender;
            ScreenUtils scnUtils = new ScreenUtils ();
            scnUtils.GetAbsolutePosition (btnSearch);
            float iTop = scnUtils.GetPositionTop ();
            float iLeft = scnUtils.GetPositionLeft ();
            int iBtnTagId = btnSearch.Tag;
            int iSearchTagTypeId = -1;
            string sType = "";
            bool bBuildSearchView = false;
            
            switch (iSearchType) 
            {
                case 1: //Floor
                    iSearchTagTypeId = iFloorSearchTagId;
                    sType = "floor";
                    break;
                case 2: //Suite
                    iSearchTagTypeId = iSuiteSearchTagId;
                    sType = "suite";
                    break;
                case 3: //Rack
                    iSearchTagTypeId = iRackSearchTagId;
                    sType = "rack";
                    break;
                case 4: //SubRack
                    iSearchTagTypeId = iSubRackSearchTagId;
                    sType = "subrack";
                    break;
                    
            }
            int iPwrIdRow = iBtnTagId / iSearchTagTypeId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iSearchTagTypeId);
            int iSectionCounterTagId = iStringRowSectionCounterTagId * iPwrIdRow + iStringRow;
            UILabel hfSectionCounter = (UILabel)View.ViewWithTag (iSectionCounterTagId);
            int iSectionCounterId = Convert.ToInt32 (hfSectionCounter.Text);
            
            //Now show the search box and button
            if (m_vwSearch == null) 
            {
                UIView vwSearch = new UIView ();
                m_vwSearch = vwSearch;
                bBuildSearchView = true;
            }
            
            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                m_vwSearch.Frame = new RectangleF (iLeft, iTop - 190f, 400f, 300f);
            } 
            else 
            {
                m_vwSearch.Frame = new RectangleF (iLeft, iTop, 400f, 300f);
            }
            m_vwSearch.Layer.BorderWidth = 1f;
            m_vwSearch.BackgroundColor = UIColor.FromRGBA (238, 238, 238, 255);
            
            if (bBuildSearchView) 
            {
                UILabel lblSearch = new UILabel ();
                lblSearch.Frame = new RectangleF (2f, 2f, 396f, 40f);
                lblSearch.Text = "Show " + sType + " items containing :";
                lblSearch.BackgroundColor = UIColor.FromRGBA (238, 238, 238, 255);
                lblSearch.Tag = iSearchLabelTagId;
                m_vwSearch.AddSubview (lblSearch);
                
                UILabel hfSearch = new UILabel ();
                hfSearch.Text = iSearchType.ToString();
                hfSearch.Tag = iSearchHiddenLabelTagId;
                hfSearch.Hidden = true;
                m_vwSearch.AddSubview (hfSearch);
                
                UILabel hfPwrIdRow = new UILabel ();
                hfPwrIdRow.Text = iPwrIdRow.ToString();
                hfPwrIdRow.Tag = iPwrIdRowHiddenLabelTagId;
                hfPwrIdRow.Hidden = true;
                m_vwSearch.AddSubview (hfPwrIdRow);
                
                UILabel hfStringRow = new UILabel ();
                hfStringRow.Text = iStringRow.ToString();
                hfStringRow.Tag = iStringRowHiddenLabelTagId;
                hfStringRow.Hidden = true;
                m_vwSearch.AddSubview (hfStringRow);
                
                UITextField txtSearch = new UITextField ();
                txtSearch.Frame = new RectangleF (2f, 44f, 198f, 40f);
                txtSearch.BackgroundColor = UIColor.FromRGBA (255, 255, 255, 255);
                txtSearch.Tag = iSearchTextTagId;
                txtSearch.AutocorrectionType = UITextAutocorrectionType.No;
                txtSearch.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                m_vwSearch.AddSubview (txtSearch);
                
                UIButton btnSearching = UIButton.FromType (UIButtonType.RoundedRect);
                btnSearching.Frame = new RectangleF (210f, 44f, 180f, 40f);
                btnSearching.SetTitle ("Search", UIControlState.Normal);
                btnSearching.Font = UIFont.FromName ("Verdana", 12f);
                btnSearching.Layer.BorderColor = UIColor.Black.CGColor;
                btnSearching.Tag = iSearchButtonTagId;
                m_btnSearching = btnSearching;
                m_btnSearching.TouchUpInside += (sender2,e2) => {
                    OpenItemsList (sender, e, m_vwSearch,iSectionCounterId);};
            } 
            else 
            {
                UIButton btnSearching = (UIButton)View.ViewWithTag (iSearchButtonTagId);
                UILabel lblSearching = (UILabel)View.ViewWithTag (iSearchLabelTagId);
                lblSearching.Text = "Show " + sType + " items containing :";
                UILabel hfSearching = (UILabel)View.ViewWithTag (iSearchHiddenLabelTagId);
                hfSearching.Text = iSearchType.ToString();
                UILabel hfPwrIdRow = (UILabel)View.ViewWithTag (iPwrIdRowHiddenLabelTagId);
                hfPwrIdRow.Text = iPwrIdRow.ToString();
                UILabel hfStringRow = (UILabel)View.ViewWithTag (iStringRowHiddenLabelTagId);
                hfStringRow.Text = iStringRow.ToString();
                UITextField txtSearching = (UITextField)View.ViewWithTag (iSearchTextTagId);
                txtSearching.Text = "";
                m_btnSearching = btnSearching;
                m_cmbSearch.Hidden = true;
            }
            
            
            if (bBuildSearchView) 
            {
                m_vwSearch.AddSubview (m_btnSearching);
                
                //Get the main scroll view
                UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
                scrollVw.AddSubview (m_vwSearch);
            }
            else 
            {
                m_vwSearch.Hidden = false;
            }
            
            
        }
        
        public void OpenItemsList(object sender, EventArgs e, UIView vwSearch, int iSectionCounterId)
        {
            clsTabletDB.ITPValidHierarchy ITPHierarchy = new clsTabletDB.ITPValidHierarchy();
            UILabel hfSearching = (UILabel)View.ViewWithTag (iSearchHiddenLabelTagId);
            int iSearchTypeId = Convert.ToInt32(hfSearching.Text);
            UILabel hfPwrIdRow = (UILabel)View.ViewWithTag (iPwrIdRowHiddenLabelTagId);
            int iPwrIdRow = Convert.ToInt32(hfPwrIdRow.Text);
            UILabel hfStringRow = (UILabel)View.ViewWithTag (iStringRowHiddenLabelTagId);
            int iStringRow = Convert.ToInt32(hfStringRow.Text);
            UITextField txtSearch = (UITextField)View.ViewWithTag(90);
            string sSearchText = txtSearch.Text;
            string[] sItems = ITPHierarchy.GetValidHierarchySearch(iSearchTypeId, sSearchText);
            int iTypeTagId = -1;
            int ihfTypeTagId = -1;
            
            switch (iSearchTypeId)
            {
                case 1:
                    iTypeTagId = iFloorTagId;
                    ihfTypeTagId = iFloorHiddenTagId;
                    break;
                    
                case 2:
                    iTypeTagId = iSuiteTagId;
                    ihfTypeTagId = iSuiteHiddenTagId;
                    break;
                    
                case 3:
                    iTypeTagId = iRackTagId;
                    ihfTypeTagId = iRackHiddenTagId;
                    break;
                    
                case 4:
                    iTypeTagId = iSubRackTagId;
                    ihfTypeTagId = iSubRackHiddenTagId;
                    break;
            }
            //Create a list and convert the string array to the list. Why the system cannot take a simple string array is beyond me!!!
            List<string> listItems = new List<string>();
            Array.ForEach(sItems, value => listItems.Add(value.ToString()));
            
            TableViewSource tabdata = new TableViewSource(listItems, true);
            tabdata.SetFont("Verdana", 10f);
            if (m_cmbSearch == null)
            {
                UITableView cmbItems = new UITableView();
                cmbItems.Frame = new RectangleF(100f, 88f, 90f, 200f);
                cmbItems.Tag = iSearchTableTagId; 
                m_cmbSearch = cmbItems;
            }
            
            m_cmbSearch.Hidden = false;
            tabdata.SetParent(m_cmbSearch);
            tabdata.SetUpdateFieldType("UITextField");
            UITextField lblVwUpdate = (UITextField)View.ViewWithTag (iTypeTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetTextFieldToUpdate(lblVwUpdate);
            UILabel lblhfVwUpdate = (UILabel)View.ViewWithTag (ihfTypeTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetSearchView(vwSearch);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);
            UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
            tabdata.SetItemPostUpdate(iSearchTypeId + 1, lblhfVwUpdate, hfRowStatus);
            
            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((iSectionCounterId + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);
            m_cmbSearch.Source = tabdata;
            vwSearch.AddSubview(m_cmbSearch);
        }

        public bool SetGlobalEditItems(object sender, int iType)
        {
            m_sender = sender;
            m_iValidateType = iType;
            return true;
        }

        //Here iType means 1 = Batteries, 2 = Solar strings
        public bool ValidateBankNo (object sender, int iType, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }
            
            UITextField txtBankNo = (UITextField)sender;
            string sBankNo = txtBankNo.Text;
            sBankNo = sBankNo.ToUpper();
            txtBankNo.Text = sBankNo;
            clsTabletDB.ITPValidHierarchy ITPValidHierarchy = new clsTabletDB.ITPValidHierarchy();
            bool bBankCheck = ITPValidHierarchy.IsValidItem(sBankNo, iType + 5);
            int iTagId = txtBankNo.Tag;
            int iPwrIdRow =  iTagId/ iBankNoTagId;
            int iStringRow = iTagId - (iPwrIdRow * iBankNoTagId);
            int iHiddenBankId =  iBankNoHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenBankNo = (UILabel)View.ViewWithTag (iHiddenBankId);
            
            if (!bBankCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid bank no or select from the list by using the button to the immediate right");
                txtBankNo.Text = hfHiddenBankNo.Text;
                txtBankNo.ResignFirstResponder();
                txtBankNo.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                if(hfHiddenBankNo.Text != txtBankNo.Text)
                {
                    hfHiddenBankNo.Text = txtBankNo.Text;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iBatterySectionCounter + 1);
                    SetAnyValueChanged(sender, null);
                }
                return true;
            }
        }
        
        public bool ValidateDOM (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }
            
            UITextField txtDOM = (UITextField)sender;
            string sDOM = txtDOM.Text;
            bool bDateCheck;
            int iTagId = txtDOM.Tag;
            int iPwrIdRow =  iTagId/ iBankDOMTagId;
            int iStringRow = iTagId - (iPwrIdRow * iBankDOMTagId);
            int iHiddenDOMId =  iHiddenDOMTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenDOM = (UILabel)View.ViewWithTag (iHiddenDOMId);

            if(sDOM == "")
            {
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
            }

            DateClass dt = new DateClass ();
            DateTime dtDOM;
            bDateCheck = dt.ValidateDate (sDOM, ref dtDOM);

            if (!bDateCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid date for the date of manufacture");
                txtDOM.ResignFirstResponder();
                txtDOM.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                if(hfHiddenDOM.Text != txtDOM.Text)
                {
                    string sDOMReturn = dt.Get_Date_String(dtDOM, "dd/mm/yy");
                    txtDOM.Text = sDOMReturn;
                    hfHiddenDOM.Text = sDOMReturn;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iBatterySectionCounter + 1);
                    SetAnyValueChanged(sender, null);
                }
                return true;
            }
        }
        
        public bool ValidateRating (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }
            
            UITextField txtRating = (UITextField)sender;
            string sRating = txtRating.Text;
            int iTagId = txtRating.Tag;
            int iPwrIdRow =  iTagId/ iBankRatingTagId;
            int iStringRow = iTagId - (iPwrIdRow * iBankRatingTagId);
            int iHiddenRatingId =  iHiddenRatingTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenRating = (UILabel)View.ViewWithTag (iHiddenRatingId);

            string sRatingReturn = Regex.Replace(sRating, @"[^\d]+","");
            txtRating.Text = sRatingReturn;

            if(hfHiddenRating.Text != sRatingReturn)
            {
                hfHiddenRating.Text = sRatingReturn;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
            }

            return true;
        }
        
        public bool ValidateFloor (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }

            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }


            UITextField txtFloor = (UITextField)sender;
            string sFloor = txtFloor.Text;
            sFloor = sFloor.ToUpper();
            txtFloor.Text = sFloor;
            clsTabletDB.ITPValidHierarchy ITPValidHierarchy = new clsTabletDB.ITPValidHierarchy();
            bool bFloorCheck = ITPValidHierarchy.IsValidItem(sFloor, 1);
            int iTagId = txtFloor.Tag;
            int iPwrIdRow =  iTagId/ iFloorTagId;
            int iStringRow = iTagId - (iPwrIdRow * iFloorTagId);
            int iHiddenBankId =  iFloorHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenFloor = (UILabel)View.ViewWithTag (iHiddenBankId);
            string sOldFloor = hfHiddenFloor.Text;

            if (!bFloorCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid floor or search from the list by using the button underneath");
                txtFloor.Text = hfHiddenFloor.Text;
                txtFloor.ResignFirstResponder();
                txtFloor.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                if(txtFloor.Text != hfHiddenFloor.Text)
                {
                    hfHiddenFloor.Text = txtFloor.Text;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iBatterySectionCounter + 1);
                    SetAnyValueChanged(sender, null);

                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iStringRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iStringRowPwrIdTagId * iPwrIdRow + iStringRow;
                        UILabel hfPwrId = (UILabel)View.ViewWithTag (iPwrIdTagId);
                        string sPwrId = hfPwrId.Text;

                        if(sOldFloor != sFloor)
                        {
                            if(CheckSameFloorExists(sOldFloor,iSectionId, iPwrIdRow, sPwrId, iStringRow))
                            {
                                iUtils.AlertBox alert2 = new iUtils.AlertBox();
                                alert2.CreateAlertYesNoDialog();
                                alert2.SetAlertMessage("Do you wish to change all other items on PwrId " + sPwrId + " on the floor " + sOldFloor + 
                                                       " to floor " +  sFloor + " ?");
                                alert2.ShowAlertBox(); 
                                
                                UIAlertView alert3 = alert2.GetAlertDialog();
                                alert3.Clicked += (sender2, e2)  => {CheckFloorChangesQuestion(sender2, e2, e2.ButtonIndex, iStringRow, sPwrId, sFloor, sOldFloor, iSectionId, iPwrIdRow);}; 
                            }
                        }
                    }
                }
                return true;
            }
        }
        
        public bool CheckSameFloorExists(string sFloor, int iSectionIdCounter, int iPwrIdCounter, string sPwrId, int iSourceRow)
        {
            int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
            UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
            int iRows = Convert.ToInt32(hfRowsCounter.Text);
            
            for(int i=0;i<iRows;i++)
            {
                if((i+1) != iSourceRow)
                {
                    int iFloorId =  iFloorTagId * iPwrIdCounter + (i+1);
                    UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                    string sExistingFloor = txtFloor.Text;
                    if(sExistingFloor == sFloor)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public void CheckFloorChangesQuestion (object sender, EventArgs e, int iBtnIndex, int iSourceRow, string sPwrId, string sFloor, string sOldFloor, int iSectionIdCounter, int iPwrIdCounter)
        {
            switch (iBtnIndex) 
            {
                case 0: //Yes
                    int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
                    UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
                    int iRows = Convert.ToInt32(hfRowsCounter.Text);
                    
                    for(int i=0;i<iRows;i++)
                    {
                        if((i+1) != iSourceRow)
                        {
                            int iFloorId =  iFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;
                            if(sExistingFloor == sOldFloor)
                            {
                                txtFloor.Text = sFloor;
                                
                                int iHiddenFloorId =  iFloorHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenFloor = (UILabel)View.ViewWithTag (iHiddenFloorId);
                                hfHiddenFloor.Text = sFloor;

                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdCounter + (i+1));
                                hfRowStatus.Text = "1";
                            }
                        }
                    }
                    break;
                case 1: //No
                    break;
            }
        }

        public bool ValidateSuite (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }

            UITextField txtSuite = (UITextField)sender;
            string sSuite = txtSuite.Text;
            sSuite = sSuite.ToUpper();
            txtSuite.Text = sSuite;
            clsTabletDB.ITPValidHierarchy ITPValidHierarchy = new clsTabletDB.ITPValidHierarchy();
            bool bSuiteCheck = ITPValidHierarchy.IsValidItem(sSuite, 2);
            int iTagId = txtSuite.Tag;
            int iPwrIdRow =  iTagId/ iSuiteTagId;
            int iStringRow = iTagId - (iPwrIdRow * iSuiteTagId);
            int iHiddenBankId =  iSuiteHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenSuite = (UILabel)View.ViewWithTag (iHiddenBankId);
            string sOldSuite = hfHiddenSuite.Text;

            if (!bSuiteCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid suite or search from the list by using the button underneath");
                txtSuite.Text = hfHiddenSuite.Text;
                txtSuite.ResignFirstResponder();
                txtSuite.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                if(hfHiddenSuite.Text != txtSuite.Text)
                {
                    hfHiddenSuite.Text = txtSuite.Text;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iBatterySectionCounter + 1);
                    SetAnyValueChanged(sender, null);

                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iStringRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iStringRowPwrIdTagId * iPwrIdRow + iStringRow;
                        UILabel hfPwrId = (UILabel)View.ViewWithTag (iPwrIdTagId);
                        string sPwrId = hfPwrId.Text;
                        
                        int iFloorId =  iFloorTagId * iPwrIdRow + iStringRow;
                        UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                        string sFloor = txtFloor.Text;

                        if(sOldSuite != sSuite)
                        {
                            if(CheckSameSuiteExists(sFloor, sOldSuite,iSectionId, iPwrIdRow, sPwrId, iStringRow))
                            {
                                iUtils.AlertBox alert2 = new iUtils.AlertBox();
                                alert2.CreateAlertYesNoDialog();
                                alert2.SetAlertMessage("Do you wish to change all other items on PwrId " + sPwrId + " on the floor " + 
                                                       sFloor + " and the suite " + sOldSuite + " to suite " +  sSuite + " ?");
                                alert2.ShowAlertBox(); 
                                
                                UIAlertView alert3 = alert2.GetAlertDialog();
                                alert3.Clicked += (sender2, e2)  => {
                                    CheckSuiteChangesQuestion(sender2, e2, e2.ButtonIndex, iStringRow, sPwrId, sFloor, 
                                                              sSuite, sOldSuite, iSectionId, iPwrIdRow);
                                }; 
                            }
                        }
                    }
                }
                return true;
            }
        }
        
        public bool CheckSameSuiteExists(string sFloor, string sSuite, int iSectionIdCounter, int iPwrIdCounter, string sPwrId, int iSourceRow)
        {
            int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
            UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
            int iRows = Convert.ToInt32(hfRowsCounter.Text);
            
            for(int i=0;i<iRows;i++)
            {
                if((i+1) != iSourceRow)
                {
                    int iFloorId =  iFloorTagId * iPwrIdCounter + (i+1);
                    UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                    string sExistingFloor = txtFloor.Text;

                    int iSuiteId =  iSuiteTagId * iPwrIdCounter + (i+1);
                    UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                    string sExistingSuite = txtSuite.Text;

                    if(sExistingFloor == sFloor && sExistingSuite == sSuite)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public void CheckSuiteChangesQuestion (object sender, EventArgs e, int iBtnIndex, int iSourceRow, string sPwrId, string sFloor, string sSuite, string sOldSuite, int iSectionIdCounter, int iPwrIdCounter)
        {
            switch (iBtnIndex) 
            {
                case 0: //Yes
                    int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
                    UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
                    int iRows = Convert.ToInt32(hfRowsCounter.Text);
                    
                    for(int i=0;i<iRows;i++)
                    {
                        if((i+1) != iSourceRow)
                        {
                            int iFloorId =  iFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;

                            int iSuiteId =  iSuiteTagId * iPwrIdCounter + (i+1);
                            UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                            string sExistingSuite = txtSuite.Text;

                            if(sExistingFloor == sFloor && sExistingSuite == sOldSuite)
                            {
                                txtSuite.Text = sSuite;
                                
                                int iHiddenSuiteId =  iSuiteHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenSuite = (UILabel)View.ViewWithTag (iHiddenSuiteId);
                                hfHiddenSuite.Text = sSuite;
                                
                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdCounter + (i+1));
                                hfRowStatus.Text = "1";
                            }
                        }
                    }
                    break;
                case 1: //No
                    break;
            }
        }
        
        public bool ValidateRack (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }
            
            UITextField txtRack = (UITextField)sender;
            string sRack = txtRack.Text;
            sRack = sRack.ToUpper();
            txtRack.Text = sRack;
            clsTabletDB.ITPValidHierarchy ITPValidHierarchy = new clsTabletDB.ITPValidHierarchy();
            bool bRackCheck = ITPValidHierarchy.IsValidItem(sRack, 3);
            int iTagId = txtRack.Tag;
            int iPwrIdRow =  iTagId/ iRackTagId;
            int iStringRow = iTagId - (iPwrIdRow * iRackTagId);
            int iHiddenBankId =  iRackHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenRack = (UILabel)View.ViewWithTag (iHiddenBankId);
            string sOldRack = hfHiddenRack.Text;

            if (!bRackCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid rack or search from the list by using the button underneath");
                txtRack.Text = hfHiddenRack.Text;
                txtRack.ResignFirstResponder();
                txtRack.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                if(hfHiddenRack.Text != txtRack.Text)
                {
                    hfHiddenRack.Text = txtRack.Text;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iBatterySectionCounter + 1);
                    SetAnyValueChanged(sender, null);

                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iStringRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iStringRowPwrIdTagId * iPwrIdRow + iStringRow;
                        UILabel hfPwrId = (UILabel)View.ViewWithTag (iPwrIdTagId);
                        string sPwrId = hfPwrId.Text;
                        
                        int iFloorId =  iFloorTagId * iPwrIdRow + iStringRow;
                        UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                        string sFloor = txtFloor.Text;

                        int iSuiteId =  iSuiteTagId * iPwrIdRow + iStringRow;
                        UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                        string sSuite = txtSuite.Text;

                        if(sOldRack != sRack)
                        {
                            if(CheckSameRackExists(sFloor, sSuite, sOldRack,iSectionId, iPwrIdRow, sPwrId, iStringRow))
                            {
                                iUtils.AlertBox alert2 = new iUtils.AlertBox();
                                alert2.CreateAlertYesNoDialog();
                                alert2.SetAlertMessage("Do you wish to change all other items on PwrId " + sPwrId + " on the floor " + 
                                                       sFloor + ", suite " + sSuite + " and rack " + sOldRack + " to rack " +  sRack + " ?");
                                alert2.ShowAlertBox(); 
                                
                                UIAlertView alert3 = alert2.GetAlertDialog();
                                alert3.Clicked += (sender2, e2)  => {
                                    CheckRackChangesQuestion(sender2, e2, e2.ButtonIndex, iStringRow, sPwrId, sFloor, 
                                                              sSuite, sRack, sOldRack, iSectionId, iPwrIdRow);
                                }; 
                            }
                        }
                    }
                }
                return true;
            }
        }
        
        public bool CheckSameRackExists(string sFloor, string sSuite, string sRack, int iSectionIdCounter, int iPwrIdCounter, string sPwrId, int iSourceRow)
        {
            int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
            UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
            int iRows = Convert.ToInt32(hfRowsCounter.Text);
            
            for(int i=0;i<iRows;i++)
            {
                if((i+1) != iSourceRow)
                {
                    int iFloorId =  iFloorTagId * iPwrIdCounter + (i+1);
                    UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                    string sExistingFloor = txtFloor.Text;
                    
                    int iSuiteId =  iSuiteTagId * iPwrIdCounter + (i+1);
                    UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                    string sExistingSuite = txtSuite.Text;
                    
                    int iRackId =  iRackTagId * iPwrIdCounter + (i+1);
                    UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                    string sExistingRack = txtRack.Text;
                    
                    if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sRack)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public void CheckRackChangesQuestion (object sender, EventArgs e, int iBtnIndex, int iSourceRow, 
                                              string sPwrId, string sFloor, string sSuite, string sRack, 
                                              string sOldRack, int iSectionIdCounter, int iPwrIdCounter)
        {
            switch (iBtnIndex) 
            {
                case 0: //Yes
                    int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
                    UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
                    int iRows = Convert.ToInt32(hfRowsCounter.Text);
                    
                    for(int i=0;i<iRows;i++)
                    {
                        if((i+1) != iSourceRow)
                        {
                            int iFloorId =  iFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;
                            
                            int iSuiteId =  iSuiteTagId * iPwrIdCounter + (i+1);
                            UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                            string sExistingSuite = txtSuite.Text;
                            
                            int iRackId =  iRackTagId * iPwrIdCounter + (i+1);
                            UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                            string sExistingRack = txtRack.Text;
                            
                            if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sOldRack)
                            {
                                txtRack.Text = sRack;
                                
                                int iHiddenRackId =  iRackHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenRack = (UILabel)View.ViewWithTag (iHiddenRackId);
                                hfHiddenRack.Text = sRack;
                                
                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdCounter + (i+1));
                                hfRowStatus.Text = "1";
                            }
                        }
                    }
                    break;
                case 1: //No
                    break;
            }
        }
        
        public bool ValidateSubRack (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }
            
            UITextField txtSubRack = (UITextField)sender;
            string sSubRack = txtSubRack.Text;
            sSubRack = sSubRack.ToUpper();
            txtSubRack.Text = sSubRack;
            clsTabletDB.ITPValidHierarchy ITPValidHierarchy = new clsTabletDB.ITPValidHierarchy();
            bool bSubRackCheck = ITPValidHierarchy.IsValidItem(sSubRack, 4);
            int iTagId = txtSubRack.Tag;
            int iPwrIdRow =  iTagId/ iSubRackTagId;
            int iStringRow = iTagId - (iPwrIdRow * iSubRackTagId);
            int iHiddenBankId =  iSubRackHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenSubRack = (UILabel)View.ViewWithTag (iHiddenBankId);
            string sOldSubRack = hfHiddenSubRack.Text;

            if (!bSubRackCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid subrack or search from the list by using the button underneath");
                txtSubRack.Text = hfHiddenSubRack.Text;
                txtSubRack.ResignFirstResponder();
                txtSubRack.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                if(hfHiddenSubRack.Text != txtSubRack.Text)
                {
                    hfHiddenSubRack.Text = txtSubRack.Text;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iBatterySectionCounter + 1);
                    SetAnyValueChanged(sender, null);

                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iStringRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iStringRowPwrIdTagId * iPwrIdRow + iStringRow;
                        UILabel hfPwrId = (UILabel)View.ViewWithTag (iPwrIdTagId);
                        string sPwrId = hfPwrId.Text;
                        
                        int iFloorId =  iFloorTagId * iPwrIdRow + iStringRow;
                        UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                        string sFloor = txtFloor.Text;
                        
                        int iSuiteId =  iSuiteTagId * iPwrIdRow + iStringRow;
                        UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                        string sSuite = txtSuite.Text;
                        
                        int iRackId =  iRackTagId * iPwrIdRow + iStringRow;
                        UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                        string sRack = txtRack.Text;

                        if(sOldSubRack != sSubRack)
                        {
                            if(CheckSameSubRackExists(sFloor, sSuite, sRack, sOldSubRack,iSectionId, iPwrIdRow, sPwrId, iStringRow))
                            {
                                iUtils.AlertBox alert2 = new iUtils.AlertBox();
                                alert2.CreateAlertYesNoDialog();
                                alert2.SetAlertMessage("Do you wish to change all other items on PwrId " + sPwrId + " on the floor " + 
                                                       sFloor + ", suite " + sSuite + ", rack " + sRack + 
                                                       " and subrack " + sOldSubRack + " to subrack " +  sSubRack + " ?");

                                alert2.ShowAlertBox(); 
                                
                                UIAlertView alert3 = alert2.GetAlertDialog();
                                alert3.Clicked += (sender2, e2)  => {
                                    CheckSubRackChangesQuestion(sender2, e2, e2.ButtonIndex, iStringRow, sPwrId, sFloor, 
                                                             sSuite, sRack, sSubRack, sOldSubRack, iSectionId, iPwrIdRow);
                                }; 
                            }
                        }
                    }
                }
                return true;
            }
        }
        
        public bool CheckSameSubRackExists(string sFloor, string sSuite, string sRack, string sSubRack, int iSectionIdCounter, 
                                           int iPwrIdCounter, string sPwrId, int iSourceRow)
        {
            int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
            UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
            int iRows = Convert.ToInt32(hfRowsCounter.Text);
            
            for(int i=0;i<iRows;i++)
            {
                if((i+1) != iSourceRow)
                {
                    int iFloorId =  iFloorTagId * iPwrIdCounter + (i+1);
                    UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                    string sExistingFloor = txtFloor.Text;
                    
                    int iSuiteId =  iSuiteTagId * iPwrIdCounter + (i+1);
                    UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                    string sExistingSuite = txtSuite.Text;
                    
                    int iRackId =  iRackTagId * iPwrIdCounter + (i+1);
                    UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                    string sExistingRack = txtRack.Text;

                    int iSubRackId =  iSubRackTagId * iPwrIdCounter + (i+1);
                    UITextField txtSubRack = (UITextField)View.ViewWithTag (iSubRackId);
                    string sExistingSubRack = txtSubRack.Text;
                    
                    if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sRack && sExistingSubRack == sSubRack)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public void CheckSubRackChangesQuestion (object sender, EventArgs e, int iBtnIndex, int iSourceRow, 
                                                 string sPwrId, string sFloor, string sSuite, string sRack, 
                                                 string sSubRack, string sOldSubRack, int iSectionIdCounter, 
                                                 int iPwrIdCounter)
        {
            switch (iBtnIndex) 
            {
                case 0: //Yes
                    int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
                    UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
                    int iRows = Convert.ToInt32(hfRowsCounter.Text);
                    
                    for(int i=0;i<iRows;i++)
                    {
                        if((i+1) != iSourceRow)
                        {
                            int iFloorId =  iFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;
                            
                            int iSuiteId =  iSuiteTagId * iPwrIdCounter + (i+1);
                            UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                            string sExistingSuite = txtSuite.Text;
                            
                            int iRackId =  iRackTagId * iPwrIdCounter + (i+1);
                            UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                            string sExistingRack = txtRack.Text;
                            
                            int iSubRackId =  iSubRackTagId * iPwrIdCounter + (i+1);
                            UITextField txtSubRack = (UITextField)View.ViewWithTag (iSubRackId);
                            string sExistingSubRack = txtSubRack.Text;
                            
                            if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sRack && 
                               sExistingSubRack == sOldSubRack)
                            {
                                txtSubRack.Text = sSubRack;
                                
                                int iHiddenSubRackId =  iSubRackHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenSubRack = (UILabel)View.ViewWithTag (iHiddenSubRackId);
                                hfHiddenSubRack.Text = sSubRack;
                                
                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdCounter + (i+1));
                                hfRowStatus.Text = "1";
                            }
                        }
                    }
                    break;
                case 1: //No
                    break;
            }
        }
        
        public bool SetStringEquipTypeChanged (object sender, EventArgs e)
        {
            UISegmentedControl radGroup = (UISegmentedControl)sender;
            int iTagId = radGroup.Tag;
            int iPwrIdRow =  iTagId/ iEquipTypeTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipTypeTagId);
            
            UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
            hfRowStatus.Text = "1";
            SetSectionValueChanged(m_iBatterySectionCounter + 1);
            SetAnyValueChanged(sender, null);
            return true;
        }
        
        public bool ValidateSerialNo(object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }

            UITextField txtSerialNo = (UITextField)sender;
            string sSerialNo = txtSerialNo.Text;
            int iTagId = txtSerialNo.Tag;
            int iPwrIdRow =  iTagId/ iSerialNoTagId;
            int iStringRow = iTagId - (iPwrIdRow * iSerialNoTagId);
            int iHiddenSerialId =  iHiddenSerialNoTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenSerialNo = (UILabel)View.ViewWithTag (iHiddenSerialId);

            if (sSerialNo.Length > 15)
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("The serial number cannot be more than 15 characters. It has been truncated. Please enter a valid 15 character or less serial number. Often many of the initial characters can be omitted.");
                string sSerialNoReturn = sSerialNo.Substring(0,15);
                txtSerialNo.Text = sSerialNoReturn;
                txtSerialNo.ResignFirstResponder();
                txtSerialNo.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            }

            if(hfHiddenSerialNo.Text != sSerialNo)
            {
                hfHiddenSerialNo.Text = sSerialNo;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
            }
            return true;
        }
        
        //Open a new page with the link test details
        public void OpenLinkTest(object sender, EventArgs e)
        {
            return;
        }
        
        //Open a new page with the 20 minute test details
        public void Open20MinTest(object sender, EventArgs e)
        {
            return;
        }
        
        public void DeleteBatteryString(object sender, EventArgs e)
        {
            string sRtnMsg = "";
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            UIButton btnDelete = (UIButton)sender;
            int iTagId = btnDelete.Tag;
            int iPwrIdRow = iTagId / iDeleteBatteryStringBtnTagId;
            int iStringRow = iTagId - (iPwrIdRow * iDeleteBatteryStringBtnTagId);
            
            UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * (iPwrIdRow) + (iStringRow));
            int iRowStatus = Convert.ToInt32(hfRowStatus.Text);
            
            UILabel hfAutoId = (UILabel)View.ViewWithTag(iStringRowAutoIdTagId * (iPwrIdRow) + (iStringRow));
            int iAutoId = Convert.ToInt32(hfAutoId.Text);
            
            UILabel hfMaximoAssetId = (UILabel)View.ViewWithTag(iStringRowMaximoAssetIdTagId * (iPwrIdRow) + (iStringRow));
            string sMaximoId = hfMaximoAssetId.Text;
            if (sMaximoId == "" || sMaximoId == "0")
            {
                sMaximoId = "-1";
            }
            int iMaximoAssetId = Convert.ToInt32(sMaximoId);
            
            UILabel hfPwrId = (UILabel)View.ViewWithTag(iStringRowPwrIdTagId * (iPwrIdRow) + (iStringRow));
            string sPwrId = hfPwrId.Text;
            
            UITextField txtBankNo = (UITextField)View.ViewWithTag(iBankNoTagId * (iPwrIdRow) + (iStringRow));
            string sBankNo = txtBankNo.Text;
            if (sBankNo == "")
            {
                sBankNo = "0";
            }
            int iBankNo = Convert.ToInt32(sBankNo);
            
            //You can only delete an item you have added and these have iMaximoAssetId = -1
            if (iMaximoAssetId < 0)
            {
                //This means the current row is either not changed or has been updated, so it has to be removed from the DB
                if (iRowStatus == 0 || iRowStatus == 1)
                {
                    //Don't actaully delete at this stage, just mark as deleted. Delete on the upload success.
                    if(!DBQ.ITPProjectSectionDeleteSection10Item(m_sPassedId, iAutoId, false, ref sRtnMsg))
                    {
                        iUtils.AlertBox alert = new iUtils.AlertBox();
                        alert.CreateErrorAlertDialog("Could not delete battery string record on project " + m_sPassedId + ", Power Id " + sPwrId + " and bank number " + iBankNo.ToString());
                        return;
                    }
                }
                
                //Remove the line from the page (well hide it really so all the loops still work)
                UIView vwStringRow = (UIView)View.ViewWithTag(iStringFullRowTagId * (iPwrIdRow) + (iStringRow));
                vwStringRow.Hidden = true;
                hfRowStatus.Text = "3"; //Means deleted, so no save required
                ReduceHeightAfter(m_iBatteryRowHeight, iPwrIdRow, iStringRow, 1);
                
                UIView vwPwrInternalRowId = (UIView)View.ViewWithTag((iPwrIdSectionInnerTagId + (iPwrIdRow)) * (m_iBatterySectionCounter+1));
                RectangleF frame1 = vwPwrInternalRowId.Frame;
                frame1.Height -= m_iBatteryRowHeight;
                vwPwrInternalRowId.Frame = frame1;

                //Now increase the view height for this new row (The whole section height is handled in the ReduceHeightAfter function)
                UILabel hfPwrIdSectionHeight = (UILabel)View.ViewWithTag((iPwrIdHeightTagId + iPwrIdRow ) * (m_iBatterySectionCounter + 1));
                int iPwrIdHeight = Convert.ToInt32(hfPwrIdSectionHeight.Text);
                hfPwrIdSectionHeight.Text = (iPwrIdHeight - m_iBatteryRowHeight).ToString();

                //Set the unsaved tags on (do this even though the record is removed for consistency)
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
            }
            return;
        }
        
        //The section type tells us whether we are doing a contract on a battery (1) or a power conversion equipment (2) PwrId
        //You can use this for expand as well. Just send across a negative iHeightToReduce
        public void ReduceHeightAfter(float iHeightToReduce, int iPwrIdRow, int iStringRow, int iSectionType)
        {
            UILabel hfThisPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + iPwrIdRow) * (m_iBatterySectionCounter + 1));
            int iTotalStrings = Convert.ToInt32(hfThisPwrIdStringRows.Text);
            int i;
            int jSectionType = -1;
            
            switch(iSectionType)
            {
                case 1:
                    jSectionType = m_iBatterySectionCounter;
                    break;
                default:
                    jSectionType = m_iBatterySectionCounter;
                    break;
                    
            }
            
            for(i = iStringRow ; i< iTotalStrings ; i++)
            {
                //Move any string rows afterwards up by the height
                UIView vwStringRow = (UIView)View.ViewWithTag(iStringFullRowTagId * (iPwrIdRow) + (i + 1));
                RectangleF frame1 = vwStringRow.Frame;
                frame1.Y -= iHeightToReduce;
                vwStringRow.Frame = frame1;
            }
            
            //Reduce the height of this particular PwrId section
            UIView vwSectionThis = (UIView)View.ViewWithTag((iPwrIdSectionTagId + (iPwrIdRow)) * (jSectionType + 1));
            RectangleF frameThis = vwSectionThis.Frame;
            frameThis.Height -= iHeightToReduce;
            vwSectionThis.Frame = frameThis;
            
            UILabel hfSectionPwrIds = (UILabel)View.ViewWithTag(iSectionRowsTagId * (jSectionType+ 1));
            int iTotalPwrIds = Convert.ToInt32(hfSectionPwrIds.Text);
            
            for(i = iPwrIdRow ; i< iTotalPwrIds ; i++)
            {
                //Move any full PwrId string section by the amount
                UIView vwSection = (UIView)View.ViewWithTag((iPwrIdSectionTagId + (i + 1)) * (jSectionType + 1));
                RectangleF frame1 = vwSection.Frame;
                frame1.Y -= iHeightToReduce;
                vwSection.Frame = frame1;
            }
            
            //Reduce the height of the battery/equipment section (this reduces the height it does NOT move the view/block at all)
            int iSectionId = (jSectionType + 1) * iContainerSectionTagId;
            UIView vwSection1 = View.ViewWithTag (iSectionId);
            RectangleF frame2 = vwSection1.Frame;
            frame2.Height -= iHeightToReduce;
            vwSection1.Frame = frame2;
            UILabel hfBatterySectionHeight = (UILabel)View.ViewWithTag(iSectionHeightTagId * (jSectionType + 1));
            int iBatterySectionHeight = Convert.ToInt32(hfBatterySectionHeight.Text);
            iBatterySectionHeight -= Convert.ToInt32(iHeightToReduce);
            hfBatterySectionHeight.Text = iBatterySectionHeight.ToString();
            
            //And reduce the content size of the main scroll view by the same amount
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            SizeF layoutSize = scrollVw.ContentSize;
            layoutSize.Height -= iHeightToReduce;
            scrollVw.ContentSize = layoutSize;
        }
        
        //The section type tells us whether we are doing an expand on a battery (1) or a power conversion equipment (2) PwrId
        public void ExpandPwrId(object sender, EventArgs e, int iSectionType)
        {
            //Get the PwrId section id and the height, hide the PwrId section and then reduce everything after that
            UIButton btnExpand = (UIButton)sender;
            int iTagId = btnExpand.Tag;
            int jSectionType = -1;
            
            switch(iSectionType)
            {
                case 1:
                    jSectionType = m_iBatterySectionCounter;
                    break;
                default:
                    jSectionType = m_iBatterySectionCounter;
                    break;
                    
            }
            int iPwrIdRow = iTagId /(jSectionType+1)  - iPwrIdExpandTagId; //This is 1 based
            UILabel hfThisPwrIdHeight = (UILabel)View.ViewWithTag((iPwrIdHeightTagId + iPwrIdRow) * (jSectionType + 1));
            int iHeight = Convert.ToInt32(hfThisPwrIdHeight.Text);
            UIView vwPwrIdSectionInner = (UIView)View.ViewWithTag((iPwrIdSectionInnerTagId + iPwrIdRow) * (jSectionType + 1));
            vwPwrIdSectionInner.Hidden = false;
            ReduceHeightAfter(-iHeight, iPwrIdRow, 0, iSectionType);
            
            //And now enable the - button and new string button, disable the + button
            btnExpand.Enabled = false;
            UILabel lblPwrId = (UILabel)View.ViewWithTag ((iPwrIdRowLabelTagId + (iPwrIdRow)) * (jSectionType+1));                        
            string sPwrId = lblPwrId.Text;
            bool bRFUPwrIdCommitted = RFUPwrIdCommitted(sPwrId);
            if(!bRFUPwrIdCommitted)
            {
                UIButton btnNewString = (UIButton)View.ViewWithTag((iPwrIdNewBtnTagId + iPwrIdRow) * (jSectionType + 1));
                btnNewString.Enabled = true;
            }
            UIButton btnContract = (UIButton)View.ViewWithTag((iPwrIdContractTagId + iPwrIdRow) * (jSectionType + 1));
            btnContract.Enabled = true;
        }
        
        //The section type tells us whether we are doing a contract on a battery (1) or a power conversion equipment (2) PwrId
        public void ContractPwrId(object sender, EventArgs e, int iSectionType)
        {
            //Get the PwrId section id and the height, hide the PwrId section and then reduce everything after that
            UIButton btnContract = (UIButton)sender;
            int iTagId = btnContract.Tag;
            int jSectionType = -1;
            
            switch(iSectionType)
            {
                case 1:
                    jSectionType = m_iBatterySectionCounter;
                    break;
                default:
                    jSectionType = m_iBatterySectionCounter;
                    break;
                    
            }
            int iPwrIdRow = iTagId /(jSectionType+1)  - iPwrIdContractTagId; //This is 1 based
            UILabel hfThisPwrIdHeight = (UILabel)View.ViewWithTag((iPwrIdHeightTagId + iPwrIdRow) * (jSectionType + 1));
            int iHeight = Convert.ToInt32(hfThisPwrIdHeight.Text);
            UIView vwPwrIdSectionInner = (UIView)View.ViewWithTag((iPwrIdSectionInnerTagId + iPwrIdRow) * (jSectionType + 1));
            vwPwrIdSectionInner.Hidden = true;
            ReduceHeightAfter(iHeight, iPwrIdRow, 0, iSectionType);
            
            //And now disable the - button and new string button, enable the + button
            btnContract.Enabled = false;
            UIButton btnNewString = (UIButton)View.ViewWithTag((iPwrIdNewBtnTagId + iPwrIdRow) * (jSectionType + 1));
            btnNewString.Enabled = false;
            UIButton btnExpand = (UIButton)View.ViewWithTag((iPwrIdExpandTagId + iPwrIdRow) * (jSectionType + 1));
            btnExpand.Enabled = true;
            
            
        }
        
        public void AddNewBatteryString (object sender, EventArgs e)
        {
            UIButton btnAddNew = (UIButton)sender;
            int iTagId = btnAddNew.Tag;
            int iPwrIdRow = iTagId /(m_iBatterySectionCounter+1)  - iPwrIdNewBtnTagId; //This is 1 based
            UILabel hfThisPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + iPwrIdRow) * (m_iBatterySectionCounter + 1));
            int iTotalStrings = Convert.ToInt32(hfThisPwrIdStringRows.Text);
            UILabel hfPwrId = (UILabel)View.ViewWithTag((iPwrIdRowLabelTagId + iPwrIdRow) * (m_iBatterySectionCounter+1));
            string sPwrId = hfPwrId.Text;
            float iHeightToAdd = 0.0f;
            
            UIView BatteryStringRow = BuildBatteryStringRowDetails(m_iBatterySectionCounter, iPwrIdRow - 1, iTotalStrings, 
                                                                   sPwrId, -1, -1, "", "", "","", "", "", "", "", "" ,"" , 
                                                                   "","","N","", 0, 0, true, false, ref iHeightToAdd);
            //Get the position of the last row in this internal pwrId battery block
            UIView vwPwrInternalRowId = (UIView)View.ViewWithTag((iPwrIdSectionInnerTagId + (iPwrIdRow)) * (m_iBatterySectionCounter+1));
            float iPwrIdRowVert = vwPwrInternalRowId.Frame.Height;
            BatteryStringRow.Frame = new RectangleF(0f, iPwrIdRowVert, 1000f, iHeightToAdd);
            BatteryStringRow.Tag = iStringFullRowTagId * (iPwrIdRow) + (iTotalStrings + 1);
            vwPwrInternalRowId.AddSubview(BatteryStringRow);
            RectangleF frame1 = vwPwrInternalRowId.Frame;
            frame1.Height += iHeightToAdd;
            vwPwrInternalRowId.Frame = frame1;

            //Now increase the view height for this new row (the whole section height is managed in the ReduceHeightAfter function)
            UILabel hfPwrIdSectionHeight = (UILabel)View.ViewWithTag((iPwrIdHeightTagId + iPwrIdRow ) * (m_iBatterySectionCounter + 1));
            int iPwrIdHeight = Convert.ToInt32(hfPwrIdSectionHeight.Text);
            hfPwrIdSectionHeight.Text = (iPwrIdHeight + iHeightToAdd).ToString();

            //Now increase the number of strings in the PwrId by 1
            iTotalStrings++;
            hfThisPwrIdStringRows.Text = iTotalStrings.ToString();
            ReduceHeightAfter(-iHeightToAdd, iPwrIdRow, iTotalStrings, 1);
            
            //Set the unsaved tags on
            SetSectionValueChanged(m_iBatterySectionCounter + 1);
            SetAnyValueChanged(sender, null);
            
            //Take off the completed or committed flags and also on the questions screen
            UILabel lblCompleted = (UILabel)View.ViewWithTag (iSectionCompleteLabelTagId * (m_iBatterySectionCounter + 1));
            lblCompleted.Hidden = true;
            UILabel lblPwrIdComplete = (UILabel)View.ViewWithTag ((iPwrIdSectionCompleteLabelTagId + (iPwrIdRow)) * (m_iBatterySectionCounter+1));
            lblPwrIdComplete.Hidden = true;
            ProjectITPage QuestionsScreen = new ProjectITPage ();
            QuestionsScreen = GetProjectITPPage ();
            QuestionsScreen.SetBatteryCompleted(false);

            //Enable the Save section button
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (m_iBatterySectionCounter+1));
            btnSave.Hidden = false;
            m_iValidateType = -1;

            //And move to the position
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            float iTotalPosn = iPwrIdRowVert + scrollVw.ContentOffset.Y;
            PointF posn = new PointF(0f, iTotalPosn);
            scrollVw.SetContentOffset(posn, true);
            
            
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

        public bool BatteryFullyComplete()
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10BatteryComplete(m_sPassedId);
        }
        
        public bool BatteryFullyCommitted()
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10BatteryFullyCommitted(m_sPassedId);
        }

        public bool BatteryPwrIdComplete(string sPwrId)
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10BatteryPwrIdComplete(m_sPassedId, sPwrId);
        }

        public bool RFUPwrIdCommitted(string sPwrId)
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSectionRFUPwrIdCommitted(m_sPassedId, sPwrId);
        }
        
        public void SaveAllSections ()
        {
            //Cycle through each section
            for (int i = 0; i< m_iSections; i++) 
            {
                if(!SaveSection(iSaveSectionBtnTagId * (i+1)))
                {
                    iUtils.AlertBox alert = new iUtils.AlertBox();
                    UILabel lblSectionDesc = (UILabel)View.ViewWithTag (iSectionDescTagId * (i+1));
                    string sDesc = lblSectionDesc.Text;
                    alert.CreateErrorAlertDialog("Could not save section " + sDesc + ". Exiting.");
                    return;
                }
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
                    case 1: //Bank No
                        if(!ValidateBankNo(m_sender,1, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 2: //DOM
                        if(!ValidateDOM(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 3: //Rating
                        if(!ValidateRating(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 4: //Floor
                        if(!ValidateFloor(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 5: //Suite
                        if(!ValidateSuite(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 6: //Rack
                        if(!ValidateRack(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 7: //SubRack
                        if(!ValidateSubRack(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 8: //SerialNo
                        if(!ValidateSerialNo(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                }
            }
            return SaveSection(iBtnId);
        }
        
        public bool SaveSection (int iBtnId)
        {
            return SaveBatterySection(iBtnId);
        }
        
        public bool SaveBatterySection(int iBtnId)
        {
            int i;
            int j;
            int iAutoId;
            string sId = m_sPassedId;
            clsTabletDB.ITPDocumentSection DB = new clsTabletDB.ITPDocumentSection();
            string[] sItemValues = new string[24];
            
            //Get the number of PwrId's
            UILabel hfSectionPwrIds = (UILabel)View.ViewWithTag(iSectionRowsTagId * (m_iBatterySectionCounter + 1));
            int iTotalPwrIds = Convert.ToInt32(hfSectionPwrIds.Text);
            bool bResetSectionFlag = true;
            
            for (i=0; i<iTotalPwrIds; i++)
            {
                //For each battery string block in this PwrId save it if necessary
                UILabel hfThisPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + (i + 1)) * (m_iBatterySectionCounter + 1));
                int iTotalStrings = Convert.ToInt32(hfThisPwrIdStringRows.Text);
                for (j=0; j<iTotalStrings; j++)
                {
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * (i + 1) + (j + 1));
                    int iRowStatus = Convert.ToInt32(hfRowStatus.Text);
                    
                    if (iRowStatus == 1 || iRowStatus == 2 || iRowStatus == 3)
                    {
                        UILabel hfAutoId = (UILabel)View.ViewWithTag(iStringRowAutoIdTagId * (i + 1) + (j + 1));
                        string sAutoId = hfAutoId.Text;
                        if (sAutoId == "")
                        {
                            iAutoId = -1;
                        }
                        else
                        {
                            iAutoId = Convert.ToInt32(sAutoId);
                        }
                        
                        UILabel lblPwrId = (UILabel)View.ViewWithTag(iStringRowPwrIdTagId * (i + 1) + (j + 1));
                        string sPwrId = lblPwrId.Text;
                        UITextField txtBankNo = (UITextField)View.ViewWithTag(iBankNoTagId * (i + 1) + (j + 1));
                        string sBankNo = txtBankNo.Text;
                        UITextField txtFloor = (UITextField)View.ViewWithTag(iFloorTagId * (i + 1) + (j + 1));
                        string sFloor = txtFloor.Text;
                        UITextField txtSuite = (UITextField)View.ViewWithTag(iSuiteTagId * (i + 1) + (j + 1));
                        string sSuite = txtSuite.Text;
                        UITextField txtRack = (UITextField)View.ViewWithTag(iRackTagId * (i + 1) + (j + 1));
                        string sRack = txtRack.Text;
                        UITextField txtSubRack = (UITextField)View.ViewWithTag(iSubRackTagId * (i + 1) + (j + 1));
                        string sSubRack = txtSubRack.Text;
                        UILabel lblMake = (UILabel)View.ViewWithTag(iBankMakeTagId * (i + 1) + (j + 1));
                        string sMake = lblMake.Text;
                        UILabel lblModel = (UILabel)View.ViewWithTag(iBankModelTagId * (i + 1) + (j + 1));
                        string sModel = lblModel.Text;
                        UILabel lblSPN = (UILabel)View.ViewWithTag(iSPNHiddenTagId * (i + 1) + (j + 1));
                        string sSPN = lblSPN.Text;
                        UITextField txtSerialNo = (UITextField)View.ViewWithTag(iSerialNoTagId * (i + 1) + (j + 1));
                        string sSerialNo = txtSerialNo.Text;
                        UITextField txtDOM = (UITextField)View.ViewWithTag(iBankDOMTagId * (i + 1) + (j + 1));
                        string sDOM = txtDOM.Text;
                        if (txtDOM.Text == "0")
                        {
                            sDOM = "01/01/1900";
                        }
                        
                        UILabel lblFuseOrCB = (UILabel)View.ViewWithTag(iBankFuseOrCBTagId * (i + 1) + (j + 1));
                        string sFuseOrCB = lblFuseOrCB.Text;
                        UITextField txtRating = (UITextField)View.ViewWithTag(iBankRatingTagId * (i + 1) + (j + 1));
                        string sRating = txtRating.Text;
                        if (txtRating.Text == "")
                        {
                            sRating = "0";
                        }
                        
                        UILabel hfLinkTestStatus = (UILabel)View.ViewWithTag(iLinkTestHiddenTagId * (i + 1) + (j + 1));
                        string sLinkTest = hfLinkTestStatus.Text;
                        if (sLinkTest == "")
                        {
                            sLinkTest = "0";
                        }
                        UILabel hf20MinTestStatus = (UILabel)View.ViewWithTag(i20MinTestHiddenTagId * (i + 1) + (j + 1));
                        string s20MinTest = hf20MinTestStatus.Text;
                        if (s20MinTest == "")
                        {
                            s20MinTest = "0";
                        }
                        
                        
                        UISegmentedControl radGrp = (UISegmentedControl)View.ViewWithTag(iEquipTypeTagId * (i + 1) + (j + 1));
                        int iAnswerIndex = radGrp.SelectedSegment;  
                        string sAnswer = "";
                        string sEquipType = "";
                        
                        UILabel hfMaximoAssetId = (UILabel)View.ViewWithTag(iStringRowMaximoAssetIdTagId * (i + 1) + (j + 1));
                        string sMaximoAssetId = hfMaximoAssetId.Text;
                        string sTransferAssetId = "";
                        string sPSAAssetId = "";
                        string sDuplicate = "0";
                        
                        if (s20MinTest == "")
                        {
                            s20MinTest = "0";
                        }
                        
                        if (iAnswerIndex >= 0)
                        {
                            sAnswer = radGrp.TitleAt(iAnswerIndex);
                        }
                        else
                        {
                            sAnswer = "";
                        }
                        
                        switch (sAnswer)
                        {
                            case "New":
                                sEquipType = "N";
                                sTransferAssetId = "";
                                sPSAAssetId = sMaximoAssetId;
                                if (sPSAAssetId == "-1")
                                {
                                    sDuplicate = "-1";
                                }
                                break;
                            case "Used":
                                sEquipType = "U";
                                sTransferAssetId = sMaximoAssetId;
                                sPSAAssetId = "";
                                break;
                            default:
                                sEquipType = "N";
                                sTransferAssetId = "";
                                sPSAAssetId = sMaximoAssetId;
                                if (sPSAAssetId == "-1")
                                {
                                    sDuplicate = "-1";
                                }
                                break;
                        }
                        
                        string sCurrentDateAndTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                        
                        //Get all the info for this string
                        sItemValues [0] = sId;
                        sItemValues [1] = sPwrId;
                        sItemValues [2] = sBankNo;
                        sItemValues [3] = sFloor;
                        sItemValues [4] = sSuite;
                        sItemValues [5] = sRack;
                        sItemValues [6] = sSubRack;
                        sItemValues [7] = ""; //There is no position for a battery string
                        sItemValues [8] = sMake;
                        sItemValues [9] = sModel;
                        sItemValues [10] = sSerialNo;
                        sItemValues [11] = sDOM;
                        sItemValues [12] = sFuseOrCB;
                        sItemValues [13] = sRating;
                        sItemValues [14] = sLinkTest;
                        sItemValues [15] = s20MinTest;
                        sItemValues [16] = sCurrentDateAndTime;
                        sItemValues [17] = sTransferAssetId;
                        sItemValues [18] = sPSAAssetId;
                        sItemValues [19] = sEquipType;
                        sItemValues [20] = sSPN;
                        sItemValues [21] = sDuplicate;
                        sItemValues [22] = "6"; //The equipemnt type is 6 for a battery string
                        sItemValues [23] = iRowStatus.ToString();
                        
                        if (sMake == "" || sModel == "" || sBankNo == "" || sEquipType == "")
                        {
                            bResetSectionFlag = false;
                            iUtils.AlertBox alert = new iUtils.AlertBox();
                            alert.CreateAlertDialog();
                            alert.SetAlertMessage("An item in PwrId " + sPwrId + " is not fully filled out. The system cannot save this item.");
                            alert.ShowAlertBox(); 
                        }
                        else
                        {
                            //Update or insert into the local DB
                            if (DB.ITPSection10SetRecord(sId, ref iAutoId, sItemValues))
                            {
                                //Update the row status and the autoid
                                hfAutoId.Text = iAutoId.ToString();
                                hfRowStatus.Text = "0";
                            }
                        }
                    }
                    
                }
                
            }
            //Reset the section flag
            if (bResetSectionFlag)
            {
                UILabel hfSectionStatus = (UILabel)View.ViewWithTag(iSectionStatusTagId * (m_iBatterySectionCounter + 1));
                hfSectionStatus.Text = "0";
                SetAnyValueChangedOff();
                gbSuppressSecondCheck = false;
                return true;
            }
            else
            {
                gbSuppressSecondCheck = false;
                return false;
            }
        }
        
        //Send through just the section counter NOT the section Id. So 1 NOT 10000 etc
        public void SetSectionValueChanged(int iSectionId)
        {
            UILabel txtEditStatus = (UILabel)View.ViewWithTag (iSectionId * iSectionStatusTagId);
            txtEditStatus.Text = "1";
        }
        
        public void SetAnyValueChanged(object sender, EventArgs e)
        {
            UIView changes = (UIView)View.ViewWithTag (60);
            changes.Hidden = false;
            UILabel txtEditStatus = (UILabel)View.ViewWithTag (80);
            txtEditStatus.Text = "1";

            //Enable the Save section button
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (m_iBatterySectionCounter+1));
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

            ShowCompletedLabels();

            //Disable the Save section button
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (m_iBatterySectionCounter+1));
            btnSave.Hidden = true;
            m_iValidateType = -1;
        }

        public void ShowCompletedLabels()
        {
            int i;
            UILabel hfSectionPwrIds = (UILabel)View.ViewWithTag(iSectionRowsTagId * (m_iBatterySectionCounter+ 1));
            int iTotalPwrIds = Convert.ToInt32(hfSectionPwrIds.Text);
            UILabel lblCompleted = (UILabel)View.ViewWithTag (iSectionCompleteLabelTagId * (m_iBatterySectionCounter + 1));
            ProjectITPage QuestionsScreen = new ProjectITPage ();
            QuestionsScreen = GetProjectITPPage ();
            if(BatteryFullyComplete())
            {
                lblCompleted.Hidden = false;

                for(i = 0 ; i< iTotalPwrIds ; i++)
                {
                    UILabel lblPwrIdComplete = (UILabel)View.ViewWithTag ((iPwrIdSectionCompleteLabelTagId + (i+1)) * (m_iBatterySectionCounter+1));
                    lblPwrIdComplete.Hidden = false;
                }

                //Now also show it on the calling ITP screen
                QuestionsScreen.SetBatteryCompleted(true);
            }
            else
            {
                lblCompleted.Hidden = true;
                for(i = 0 ; i< iTotalPwrIds ; i++)
                {
                    UILabel lblPwrId = (UILabel)View.ViewWithTag ((iPwrIdRowLabelTagId + (i+1)) * (m_iBatterySectionCounter+1));
                    string sPwrId = lblPwrId.Text;
                    UILabel lblPwrIdComplete = (UILabel)View.ViewWithTag ((iPwrIdSectionCompleteLabelTagId + (i+1)) * (m_iBatterySectionCounter+1));
                    if(BatteryPwrIdComplete(sPwrId))
                    {
                        lblPwrIdComplete.Hidden = false;
                    }
                    else
                    {
                        lblPwrIdComplete.Hidden = true;
                    }
                }
                //Now also show it on the calling ITP screen
                QuestionsScreen.SetBatteryCompleted(false);
            }
        }

        public void CheckUnsaved ()
        {
            //First of all validate anything required
            switch(m_iValidateType)
            {
                case 1: //Bank No
                    if(!ValidateBankNo(m_sender,1, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 2: //DOM
                    if(!ValidateDOM(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 3: //Rating
                    if(!ValidateRating(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 4: //Floor
                    if(!ValidateFloor(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 5: //Suite
                    if(!ValidateSuite(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 6: //Rack
                    if(!ValidateRack(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 7: //SubRack
                    if(!ValidateSubRack(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 8: //SerialNo
                    if(!ValidateSerialNo(m_sender, 1))
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
                ProjectITPage QuestionsScreen = new ProjectITPage ();
                QuestionsScreen = GetProjectITPPage ();
                this.NavigationController.PopToViewController (QuestionsScreen, true);
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
            ProjectITPage QuestionsScreen = new ProjectITPage ();
            QuestionsScreen = GetProjectITPPage ();
            switch (iBtnIndex) 
            {
                case 0:
                    SaveAllSections();
                    this.NavigationController.PopToViewController (QuestionsScreen, true);
                    break;
                case 1:
                    this.NavigationController.PopToViewController (QuestionsScreen, true);
                    break;
                case 2:
                    break;
            }
        }
        
        public bool MoveNextTextField(object sender, int iTextFieldIndex)
        {
            UITextField txtField = (UITextField)sender;
            UITextField txtNext;
            txtField.ResignFirstResponder();
            int iTagId = txtField.Tag;
            int iTextTagId = 0;
            int iSectionCounterId = m_iBatterySectionCounter;
            
            switch (iTextFieldIndex)
            {
                case 1:
                    iTextTagId = iBankNoTagId;
                    break;
                case 2:
                    iTextTagId = iBankDOMTagId;
                    break;
                case 3:
                    iTextTagId = iBankRatingTagId;
                    break;
                case 4:
                    iTextTagId = iFloorTagId;
                    break;
                case 5:
                    iTextTagId = iSuiteTagId;
                    break;
                case 6:
                    iTextTagId = iRackTagId;
                    break;
                case 7:
                    iTextTagId = iSubRackTagId;
                    break;
                case 8:
                    iTextTagId = iSerialNoTagId;
                    break;
            }
            
            int iPwrIdRow;
            int iStringRow;
            UILabel hfPwrIdStringRows;
            int iTotalStringRows;
            iPwrIdRow = iTagId / iTextTagId;
            iStringRow = iTagId - (iPwrIdRow * iTextTagId);
            iSectionCounterId = m_iBatterySectionCounter;
            hfPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + iPwrIdRow) * (iSectionCounterId + 1)); 
            iTotalStringRows = Convert.ToInt32(hfPwrIdStringRows.Text);
            switch (iTextFieldIndex) 
            {
                case 1: //This means you are coming from the bank no and going to the DOM
                    if(m_bSuppressMove) //This is required on the validate because the end editing and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    txtNext = (UITextField)View.ViewWithTag (iBankDOMTagId * (iPwrIdRow) + (iStringRow)); 
                    break;
                case 2: //Coming from DOM and going to rating
                    if(m_bSuppressMove) //This is required on the validate because the end editing and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    txtNext = (UITextField)View.ViewWithTag (iBankRatingTagId * (iPwrIdRow) + (iStringRow));
                    break;
                case 3: //Coming from rating and going to floor
                    txtNext = (UITextField)View.ViewWithTag (iFloorTagId * (iPwrIdRow) + (iStringRow));
                    break;
                case 4: //Coming from floor and going to suite
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    txtNext = (UITextField)View.ViewWithTag (iSuiteTagId * (iPwrIdRow) + (iStringRow));
                    break;
                case 5: //Coming from suite and going to rack
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    txtNext = (UITextField)View.ViewWithTag (iRackTagId * (iPwrIdRow) + (iStringRow));
                    break;
                case 6: //Coming from rack and going to subrack
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    txtNext = (UITextField)View.ViewWithTag (iSubRackTagId * (iPwrIdRow) + (iStringRow));
                    break;
                case 7: //Coming from subrack and going to serial number
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    txtNext = (UITextField)View.ViewWithTag (iSerialNoTagId * (iPwrIdRow) + (iStringRow)); //Go to the next string, hence the + 1 here
                    break;
                case 8: //Coming from serial number and going to bank no
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    //Make sure we are not on the last string because there is no extra row to go to so go to the first one again
                    if((iStringRow + 1) > iTotalStringRows)
                    {
                        txtNext = (UITextField)View.ViewWithTag (iBankNoTagId * (iPwrIdRow) + 1); //Cycle back to the first row
                    }
                    else
                    {
                        txtNext = (UITextField)View.ViewWithTag (iBankNoTagId * (iPwrIdRow) + (iStringRow + 1)); //Go to the next string, hence the + 1 here
                    }
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
        
        public bool GetConnectionStatus ()
        {
            
            NetworkStatus iNetStatus = Reachability.InternetConnectionStatus ();
            
            if (iNetStatus == NetworkStatus.NotReachable) 
            {
                return false;
            }
            
            return true;
        }    }
}

