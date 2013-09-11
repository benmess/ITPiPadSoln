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
    public partial class PowerConversion : UIViewController
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

        //Tags for heading Section
        int iPwrIdSectionTagId = 10010800;
        int ihfRow10StatusTagId = 10011100;
        int iPwrIdRowLabelTagId = 10011200;
        int iPwrIdNewBtnTagId = 10011300;
        int ihfPwrIdStringRowsTagId = 10011400;

        int iPwrIdExpandTagId = 10021100;
        int iPwrIdContractTagId = 10021200;
        int iPwrIdSectionInnerTagId = 10021300;
        int iPwrIdHeightTagId  = 10021400;
        int iPwrIdSectionCompleteLabelTagId  = 10021500;

        //Tags for equipment section
        int iEquipmentFullRowTagId = 10021600;
        int iFloorEquipLabelTagId = 10021700;
        int iSuiteEquipLabelTagId = 10021800;
        int iRackEquipLabelTagId = 10021900;
        int iSubrackEquipLabelTagId = 10022000;
        int iPositionEquipLabelTagId = 10022100;
        int iStringEquipLabelTagId = 10022200;
        int iEquipTypeEquipLabelTagId = 10022300;
        int iDOMEquipLabelTagId = 10022400;
        int iSerialNoEquipLabelTagId = 10022500;
        int iMakeEquipLabelTagId = 10022600;
        int iModelEquipLabelTagId = 10022700;
        int iDeleteEquipLabelTagId = 10022800;
        int iBlank1EquipLabelTagId = 10022900;
        
        int iEquipmentRowSectionCounterTagId = 10013100;
        int iEquipmentRowPwrIdTagId = 10013300;
        int iEquipmentRowStatusTagId = 10013400;
        int iEquipmentRowAutoIdTagId = 10013500;
        int iEquipmentRowMaximoAssetIdTagId = 10013600;
        int iEquipmentTypeTagId = 10013700;
        int iDuplicateTagId = 10013800;
        int iEquipmentMakeTagId = 10014200;
        int iEquipmentMakeSearchTagId = 10014300;
        int iEquipmentModelTagId = 10014400;
        int iEquipmentSPNHiddenTagId = 10014500;
        int iEquipmentModelSearchTagId = 10014600;
        int iEquipmentDOMTagId = 10014700;
        int iEquipmentFloorTagId = 10015200;
        int iEquipmentFloorHiddenTagId = 10015300;
        int iEquipmentFloorSearchTagId = 10015400;
        int iEquipmentSuiteTagId = 10015500;
        int iEquipmentSuiteHiddenTagId = 10015600;
        int iEquipmentSuiteSearchTagId = 10015700;
        int iEquipmentRackTagId = 10015800;
        int iEquipmentRackHiddenTagId = 10015900;
        int iEquipmentRackSearchTagId = 10016100;
        int iEquipmentSubRackTagId = 10016200;
        int iEquipmentSubRackHiddenTagId = 10016300;
        int iEquipmentSubRackSearchTagId = 10016400;
        int iEquipmentPositionTagId = 10016500;
        int iEquipmentPositionHiddenTagId = 10016600;
        int iEquipmentPositionSearchTagId = 10016700;
        int iEquipmentStringTagId = 10016800;
        int iEquipmentStringHiddenTagId = 10016900;
        int iEquipmentStringSearchTagId = 10017000;
        int iEquipmentEquipTypeTagId = 10017100;
        int iEquipmentSerialNoTagId = 10017200;
        int iEquipmentDeleteBtnTagId = 10017400;
        int iEquipmentDOMHiddenTagId = 10017500;
        int iEquipmentSerialNoHiddenTagId = 10017600;
        int iEquipmentMaximoAssetTagId = 10018100;
        int iEquipmentMaximoAssetHiddenTagId = 10018200;
        int iEquipmentMaximoAssetTextViewTagId = 10018300;
        int iEquipmentMaximoLblAssetTagId = 10018400;
        int iEquipmentMaximoLblViewAssetTagId = 10018500;

        bool gbSuppressSecondCheck = false;
        string m_sSessionId = "";
        string m_sPassedId = "";
        string m_sProjDesc = "";
        int m_iSections = 0;
        int m_iEquipmentSectionCounter = 0;
        int m_iEquipmentPwrIds = 0;
        float m_iEquipmentRowHeight = 0f;
        string[] m_sRackMakes;
        string[] m_sRackModels;
        string[] m_sSubRackMakes;
        string[] m_sSubRackModels;
        string[] m_sPositionMakes;

        string[] m_sPositionModels;
        string[] m_sSolarStringMakes;
        string[] m_sSolarStringModels;
        bool m_bSuppressMove = false;
        UIView m_vwSearch;
        
        UITableView m_cmbSearch;
        UIButton m_btnSearching;
        object m_sender;
        int m_iValidateType;


        public PowerConversion() : base ("PowerConversion", null)
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
                float iQuestionRowHeight = 30f;
                float iTotalHeight = 0f;
                float iHeightToAdd = iQuestionRowHeight;
                bool bHideComplete = true;
                bool bHideSectionComplete = true;
                bool bFullyCommitted = false;
                bool bRFUPwrIdCommitted = false;
                UIView[] arrItems4 = new UIView[8];
                UIView[] arrItems5 = new UIView[8];

                
                
                //Get some static data for dropdowns only once so we don't reprocess unecessarily
                clsTabletDB.ITPInventory ITPInventory = new clsTabletDB.ITPInventory();
                string[] sRackMakes = ITPInventory.GetRackMakes();
                m_sRackMakes = sRackMakes;
                string[] sSubRackMakes = ITPInventory.GetSubRackMakes();
                m_sSubRackMakes = sSubRackMakes;
                string[] sPositionMakes = ITPInventory.GetPositionMakes();
                m_sPositionMakes = sPositionMakes;
                string[] sSolarStringMakes = ITPInventory.GetSolarStringMakes();
                m_sSolarStringMakes = sSolarStringMakes;

                UIScrollView layout = new UIScrollView();
                layout.Frame = new RectangleF(0f,35f,1000f,620f);
                layout.Tag = 2;
                clsTabletDB.ITPDocumentSection ITPSection = new clsTabletDB.ITPDocumentSection();

                //******************************************************************************************//
                //                      SECTION 10 (EQUIPMENT)                                              //
                //******************************************************************************************//
                //Get all the PwrId's for this project from ITPSection10
                DataSet arrITPSectionEquipmentPwrIds = ITPSection.GetLocalITPSectionEquipmentPwrIds(sId);
                
                if (arrITPSectionEquipmentPwrIds.Tables.Count > 0)
                {
                    int iii = m_iSections;
                    m_iSections++; //Add an extra one for the batteries section
                    m_iEquipmentSectionCounter = iii;
                    int iPwrIdRows = arrITPSectionEquipmentPwrIds.Tables[0].Rows.Count;
                    
                    //Add in the section title and buttons for each section header
                    UIView SectionEquipmentRow = new UIView();
                    float iSectionEquipmentRowVertTop = iVert;
                    SectionEquipmentRow.Frame = new RectangleF(0f,iSectionEquipmentRowVertTop,1000f,iSectionHdrRowHeight);
                    iSectionId = iSectionTagId * (iii+1);
                    SectionEquipmentRow.Tag = iSectionId;
                    layout.AddSubview(SectionEquipmentRow);
                    
                    UILabel hfSectionEquipment = new UILabel();
                    hfSectionEquipment.Text = "10";
                    hfSectionEquipment.Tag = iSectionDBIdTagId * (iii+1);
                    hfSectionEquipment.Hidden = true;
                    SectionEquipmentRow.AddSubview(hfSectionEquipment);
                    
                    iUtils.CreateFormGridItem SectionEquipment = new iUtils.CreateFormGridItem();
                    UIView SectionEquipmentVw = new UIView();
                    SectionEquipment.SetDimensions(0f,0f, 400f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
                    SectionEquipment.SetLabelText("POWER CONVERSION");
                    SectionEquipment.SetBorderWidth(0.0f);
                    SectionEquipment.SetFontName("Verdana-Bold");
                    SectionEquipment.SetTextColour("White");
                    SectionEquipment.SetFontSize(12f);
                    SectionEquipment.SetCellColour("DarkSlateGrey");
                    SectionEquipment.SetTag(iSectionDescTagId * (iii+1));
                    SectionEquipmentVw = SectionEquipment.GetLabelCell();
                    arrItems4[0] = SectionEquipmentVw;
                    
                    if(PowerConversionFullyCommitted())
                    {
                        bFullyCommitted = true;
                        bHideComplete = false;
                    }
                    else
                    {
                        bFullyCommitted = false;
                        if(PowerConversionFullyComplete())
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
                    SectionCompleteLabel.SetTag(iSectionCompleteLabelTagId * (iii+1));
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
                    btnSaveEquipment.SetTag(iSaveSectionBtnTagId * (iii+1));
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
                    btnExpandEquipment.SetTag(iExpandSectionBtnTagId * (iii+1));
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
                    btnContractEquipment.SetTag(iContractSectionBtnTagId * (iii+1));
                    btnContractEquipment.SetCellColour("DarkSlateGrey");
                    btnContractEquipmentVw = btnContractEquipment.GetButtonCell();
                    
                    UIButton btnContractEquipmentButton = new UIButton();
                    btnContractEquipmentButton = btnContractEquipment.GetButton();
                    btnContractEquipmentButton.TouchUpInside += (sender,e) => {ContractSection(sender, e);};
                    
                    arrItems4[4] = btnContractEquipmentVw;
                    
                    UILabel hfSectionEquipmentHeight = new UILabel();
                    hfSectionEquipmentHeight.Tag = iSectionHeightTagId * (iii+1);
                    hfSectionEquipmentHeight.Hidden = true;
                    hfSectionEquipmentHeight.Text = "0";
                    arrItems4[5] = hfSectionEquipmentHeight;
                    
                    UILabel hfSectionEquipmentRows = new UILabel();
                    hfSectionEquipmentRows.Tag = iSectionRowsTagId * (iii+1);
                    hfSectionEquipmentRows.Hidden = true;
                    hfSectionEquipmentRows.Text = iPwrIdRows.ToString();
                    arrItems4[6] = hfSectionEquipmentRows;
                    
                    UILabel hfSectionEquipmentStatus = new UILabel();
                    hfSectionEquipmentStatus.Tag = iSectionStatusTagId * (iii+1);
                    hfSectionEquipmentStatus.Hidden = true;
                    hfSectionEquipmentStatus.Text = "0";
                    arrItems4[7] = hfSectionEquipmentStatus;
                    
                    
                    SectionEquipmentRow.AddSubviews(arrItems4);
                    
                    iVert += iSectionHdrRowHeight;
                    
                    //Now add a new view to this view to hold another view containing all the pwrid info for this section 10
                    UIView PwrIdTableRow = new UIView();
                    PwrIdTableRow.Frame = new RectangleF(0f,iVert,1000f,iSectionHdrRowHeight);
                    iSectionId = iContainerSectionTagId * (iii+1);
                    PwrIdTableRow.Tag = iSectionId;
                    layout.AddSubview(PwrIdTableRow);
                    float iPwrIdRowVert = 0.0f;
                    float iSectionPwrIdHeight = 0.0f;
                    float iPwrIdRowVertTop = iVert;
                    float iPwrIdRowInnerTop = 0.0f;
                    float iPwrIdRowInnerTop2 = 0.0f;
                    
                    m_iEquipmentPwrIds = iPwrIdRows;

                    for (int jj = 0; jj < iPwrIdRows; jj++)
                    {
                        iPwrIdRowInnerTop2 = 0.0f;
                        UIView vwPwrInternalRowId = new UIView();
                        vwPwrInternalRowId.Frame = new RectangleF(0f,iPwrIdRowVert,1000f,200f); //This will be resized later on
                        vwPwrInternalRowId.Tag = (iPwrIdSectionTagId + (jj+1)) * (iii+1);  
                        
                        
                        UILabel hfRow10Status = new UILabel();
                        hfRow10Status.Text = "0";
                        hfRow10Status.Tag = (ihfRow10StatusTagId + (jj+1)) * (iii+1);
                        hfRow10Status.Hidden = true;
                        arrItems5[0] = hfRow10Status;
                        
                        //Put in the PwrId Label
                        iUtils.CreateFormGridItem rowPwrIdLabel = new iUtils.CreateFormGridItem();
                        UIView rowPwrIdLabelVw = new UIView();
                        iColNo = arrITPSectionEquipmentPwrIds.Tables[0].Columns["PwrId"].Ordinal;
                        string sPwrId = arrITPSectionEquipmentPwrIds.Tables[0].Rows[jj].ItemArray[iColNo].ToString();
                        rowPwrIdLabel.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                        rowPwrIdLabel.SetDimensions(0f,iPwrIdRowVert, 200f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                        rowPwrIdLabel.SetLabelText(sPwrId);
                        rowPwrIdLabel.SetBorderWidth(0.0f);
                        rowPwrIdLabel.SetFontName("Verdana-Bold");
                        rowPwrIdLabel.SetFontSize(18f);
                        rowPwrIdLabel.SetTag((iPwrIdRowLabelTagId + (jj+1)) * (iii+1));
                        
                        if (jj % 2 == 0)
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

                        iUtils.CreateFormGridItem btnNewEquipment = new iUtils.CreateFormGridItem();
                        UIView btnNewEquipmentVw = new UIView();
                        btnNewEquipment.SetDimensions(200f,iPwrIdRowVert, 200f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                        btnNewEquipment.SetLabelText("New Item");
                        btnNewEquipment.SetBorderWidth(0.0f);
                        btnNewEquipment.SetFontName("Verdana");
                        btnNewEquipment.SetFontSize(12f);
                        btnNewEquipment.SetTag((iPwrIdNewBtnTagId + (jj+1)) * (iii+1));
                        if (jj % 2 == 0)
                        {
                            btnNewEquipment.SetCellColour("Pale Yellow");
                        }
                        else
                        {
                            btnNewEquipment.SetCellColour("Pale Orange");
                        }
                        btnNewEquipmentVw = btnNewEquipment.GetButtonCell();
                        
                        UIButton btnNewEquipmentButton = new UIButton();
                        btnNewEquipmentButton = btnNewEquipment.GetButton();
                        btnNewEquipmentButton.TouchUpInside += (sender,e) => {AddNewEquipment(sender, e);};
                        
                        if(bRFUPwrIdCommitted)
                        {
                            btnNewEquipmentButton.Enabled = false;
                        }

                        arrItems5[2] = btnNewEquipmentVw;
                        
                        if(PowerConversionPwrIdComplete(sPwrId)) 
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
                        if (jj % 2 == 0)
                        {
                            PwrIdCompleteLabel.SetCellColour("Pale Yellow");
                        }
                        else
                        {
                            PwrIdCompleteLabel.SetCellColour("Pale Orange");
                        }
                        PwrIdCompleteLabel.SetTag((iPwrIdSectionCompleteLabelTagId + (jj+1)) * (iii+1));
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
                        rowPwrIdBlank.SetTag((iPwrIdRowLabelTagId + (jj+1)) * (iii+1));
                        
                        if (jj % 2 == 0)
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
                        btnExpandPwrId.SetTag((iPwrIdExpandTagId + (jj+1)) * (iii+1));
                        if (jj % 2 == 0)
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
                        btnExpandPwrIdButton.TouchUpInside += (sender,e) => {ExpandPwrId(sender, e, 2);};
                        
                        arrItems5[5] = btnExpandPwrIdVw;
                        
                        iUtils.CreateFormGridItem btnContractPwrId = new iUtils.CreateFormGridItem();
                        UIView btnContractPwrIdVw = new UIView();
                        btnContractPwrId.SetDimensions(950f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                        btnContractPwrId.SetLabelText("-");
                        btnContractPwrId.SetBorderWidth(0.0f);
                        btnContractPwrId.SetFontName("Verdana");
                        btnContractPwrId.SetFontSize(12f);
                        btnContractPwrId.SetTag((iPwrIdContractTagId + (jj+1)) * (iii+1));
                        if (jj % 2 == 0)
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
                        btnContractPwrIdButton.TouchUpInside += (sender,e) => {ContractPwrId(sender, e, 2);};
                        
                        arrItems5[6] = btnContractPwrIdVw;
                        
                        UILabel hfPwrIdSectionHeight = new UILabel();
                        hfPwrIdSectionHeight.Tag = (iPwrIdHeightTagId + (jj+1)) * (iii+1);
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
                        vwPwrInternalRowIdInnner.Tag = (iPwrIdSectionInnerTagId + (jj+1)) * (iii+1);                   
                        vwPwrInternalRowIdInnner.Frame = new RectangleF(0f,iPwrIdRowVertInner,1000f,200f); //This will be resized later on
                        //                        vwPwrInternalRowIdInnner.Hidden = true;  
                        
                        
                        UIView PwrIdHdr = BuildEquipmentHeader(jj, ref iHeightToAdd);
                        PwrIdHdr.Frame = new RectangleF(0f, iPwrIdRowVertInner, 1000f, iHeightToAdd);
                        vwPwrInternalRowIdInnner.AddSubview(PwrIdHdr);
                        vwPwrInternalRowId.AddSubview(vwPwrInternalRowIdInnner);
                        
                        iSectionPwrIdHeight += iHeightToAdd;
                        iPwrIdRowVert += iHeightToAdd;
                        iPwrIdRowVertInner += iHeightToAdd;
                        iVert += iHeightToAdd;
                        
                        //Now for each PwrId get the details for each string
                        DataSet arrITPSection10PwrIdItems = ITPSection.GetLocalITPSection10PwrIdEquipmentDetails(sId, sPwrId);
                        
                        if (arrITPSection10PwrIdItems.Tables.Count > 0)
                        {
                            int iPwrIdItemRows = arrITPSection10PwrIdItems.Tables[0].Rows.Count;
                            //Add the rows to a hidden field so we know how many rows are in each PwrId battery block
                            UILabel hfPwrIdStringRows = new UILabel();
                            hfPwrIdStringRows.Text = iPwrIdItemRows.ToString();
                            hfPwrIdStringRows.Tag = (ihfPwrIdStringRowsTagId + (jj+1)) * (iii+1);
                            hfPwrIdStringRows.Hidden = true;
                            vwPwrInternalRowIdInnner.AddSubview(hfPwrIdStringRows);
                            
                            
                            for (var kk = 0; kk < iPwrIdItemRows; kk++)
                            {
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["AutoId"].Ordinal;
                                int iAutoId = Convert.ToInt32(arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo]);
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["BankNo"].Ordinal;
                                string sBankNo = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Make"].Ordinal;
                                string sMake = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Model"].Ordinal;
                                string sModel = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["SPN"].Ordinal;
                                string sSPN = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["DOM"].Ordinal;
                                string sDOM = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Floor"].Ordinal;
                                string sFloor = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Suite"].Ordinal;
                                string sSuite = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Rack"].Ordinal;
                                string sRack = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["SubRack"].Ordinal;
                                string sSubRack = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Position"].Ordinal;
                                string sPosition = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Equipment_Condition"].Ordinal;
                                string sEquipType = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["SerialBatch"].Ordinal;
                                string sSerialNo = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["tblMaximoPSA_ID"].Ordinal;
                                string sMaximoPSAId = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["tblMaximoTransfer_Eqnum"].Ordinal;
                                string sMaximoTransferId = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
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
                                else if(iMaximoTransferId > 0 || sMaximoTransferId == "0000000000")
                                {
                                    iMaximoAssetId = iMaximoTransferId;
                                }
                                else
                                {
                                    iMaximoAssetId = -1;
                                }
                                
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Equipment_Type"].Ordinal;
                                int iEquipmentType = Convert.ToInt32(arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo]);
                                
                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Duplicate"].Ordinal;
                                int iDuplicate = Convert.ToInt32(arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo]);

                                //Add in the row
                                UIView EquipmentItemRow = BuildEquipmentItemRowDetails(iii, jj, kk, sPwrId, iAutoId,
                                                                                       iMaximoAssetId, sBankNo,
                                                                                       sMake, sModel, sSPN, sDOM,
                                                                                       sFloor, sSuite, sRack, sSubRack, sPosition, 
                                                                                       sEquipType, sSerialNo, iEquipmentType, iDuplicate,
                                                                                       false, bRFUPwrIdCommitted,ref iHeightToAdd);
                                EquipmentItemRow.Frame = new RectangleF(0f, iPwrIdRowVertInner, 1000f, iHeightToAdd);
                                EquipmentItemRow.Tag = iEquipmentFullRowTagId * (jj + 1) + (kk + 1);
                                vwPwrInternalRowIdInnner.AddSubview(EquipmentItemRow);
                                
                                m_iEquipmentRowHeight = iHeightToAdd;
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
                    hfSectionEquipmentHeight.Text = iSectionPwrIdHeight.ToString();
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
                for(int iiii=0;iiii< m_iEquipmentPwrIds; iiii++)
                {
                    UIButton btnContract = (UIButton)View.ViewWithTag ((iPwrIdContractTagId + (iiii+1)) * (m_iEquipmentSectionCounter+1));                        
                    ContractPwrId(btnContract, null, 2);
                }
                                
                
                
            }
            catch (Exception except)
            {
                string sTest = except.Message.ToString();
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog (sTest);
            }
        }

        public UIView BuildEquipmentHeader (int iRowNo, ref float iHeightToAdd)
        {
            iHeightToAdd = 0.0f;
            UIView hdrRow = new UIView();
            float iHdrVert = 0.0f;
            float iRowHeight = 20f;
            UIView[] arrItems = new UIView[3];
            UIView[] arrItems2 = new UIView[10];
            
            //Now put in the 2nd header row of labels
            iUtils.CreateFormGridItem lblFloor = new iUtils.CreateFormGridItem();
            UIView lblFloorVw = new UIView();
            lblFloor.SetDimensions(0f,iHdrVert, 51f, iRowHeight, 2f, 2f, 2f, 2f);
            lblFloor.SetLabelText("Floor");
            lblFloor.SetBorderWidth(1.0f);
            lblFloor.SetFontName("Verdana");
            lblFloor.SetFontSize(12f);
            lblFloor.SetTag(iFloorEquipLabelTagId * (iRowNo+1));
            
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
            lblSuite.SetTag(iSuiteEquipLabelTagId * (iRowNo+1));
            
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
            lblRack.SetTag(iRackEquipLabelTagId * (iRowNo+1));
            
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
            lblSubrack.SetDimensions(140f,iHdrVert, 61f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSubrack.SetLabelText("Subrack");
            lblSubrack.SetBorderWidth(1.0f);
            lblSubrack.SetFontName("Verdana");
            lblSubrack.SetFontSize(12f);
            lblSubrack.SetTag(iSubrackEquipLabelTagId * (iRowNo+1));
            
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
            
            iUtils.CreateFormGridItem lblPosition = new iUtils.CreateFormGridItem();
            UIView lblPositionVw = new UIView();
            lblPosition.SetDimensions(200f,iHdrVert, 51f, iRowHeight, 2f, 2f, 2f, 2f);
            lblPosition.SetLabelText("Posn");
            lblPosition.SetBorderWidth(1.0f);
            lblPosition.SetFontName("Verdana");
            lblPosition.SetFontSize(12f);
            lblPosition.SetTag(iPositionEquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblPosition.SetCellColour("Pale Yellow");
            }
            else
            {
                lblPosition.SetCellColour("Pale Orange");
            }
            
            lblPositionVw = lblPosition.GetLabelCell();
            arrItems2[4] = lblPositionVw;
            
            iUtils.CreateFormGridItem lblString = new iUtils.CreateFormGridItem();
            UIView lblStringVw = new UIView();
            lblString.SetDimensions(250f,iHdrVert, 51f, iRowHeight, 2f, 2f, 2f, 2f);
            lblString.SetLabelText("String");
            lblString.SetBorderWidth(1.0f);
            lblString.SetFontName("Verdana");
            lblString.SetFontSize(12f);
            lblString.SetTag(iStringEquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblString.SetCellColour("Pale Yellow");
            }
            else
            {
                lblString.SetCellColour("Pale Orange");
            }
            
            lblStringVw = lblString.GetLabelCell();
            arrItems2[5] = lblStringVw;
            
            iUtils.CreateFormGridItem lblEquipType = new iUtils.CreateFormGridItem();
            UIView lblEquipTypeVw = new UIView();
            lblEquipType.SetDimensions(300f,iHdrVert, 201f, iRowHeight, 2f, 2f, 2f, 2f);
            lblEquipType.SetLabelText("EquipType");
            lblEquipType.SetBorderWidth(1.0f);
            lblEquipType.SetFontName("Verdana");
            lblEquipType.SetFontSize(12f);
            lblEquipType.SetTag(iEquipTypeEquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblEquipType.SetCellColour("Pale Yellow");
            }
            else
            {
                lblEquipType.SetCellColour("Pale Orange");
            }
            
            lblEquipTypeVw = lblEquipType.GetLabelCell();
            arrItems2[6] = lblEquipTypeVw;
            
            iUtils.CreateFormGridItem lblDOM = new iUtils.CreateFormGridItem();
            UIView lblDOMVw = new UIView();
            lblDOM.SetDimensions(500f,iHdrVert, 81f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblDOM.SetLabelText("DOM");
            lblDOM.SetBorderWidth(1.0f);
            lblDOM.SetFontName("Verdana");
            lblDOM.SetFontSize(12f);
            lblDOM.SetTag(iDOMEquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblDOM.SetCellColour("Pale Yellow");
            }
            else
            {
                lblDOM.SetCellColour("Pale Orange");
            }
            
            lblDOMVw = lblDOM.GetLabelCell();
            arrItems2[7] = lblDOMVw;
            
            iUtils.CreateFormGridItem lblSerialNo = new iUtils.CreateFormGridItem();
            UIView lblSerialNoVw = new UIView();
            lblSerialNo.SetDimensions(580f,iHdrVert, 301f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSerialNo.SetLabelText("SerialNo");
            lblSerialNo.SetBorderWidth(1.0f);
            lblSerialNo.SetFontName("Verdana");
            lblSerialNo.SetFontSize(12f);
            lblSerialNo.SetTag(iSerialNoEquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblSerialNo.SetCellColour("Pale Yellow");
            }
            else
            {
                lblSerialNo.SetCellColour("Pale Orange");
            }
            
            lblSerialNoVw = lblSerialNo.GetLabelCell();
            arrItems2[8] = lblSerialNoVw;
            
            
            iUtils.CreateFormGridItem lblDeleteLabel = new iUtils.CreateFormGridItem();
            UIView lblDeleteLabelVw = new UIView();
            lblDeleteLabel.SetDimensions(880f,iHdrVert, 120f, iRowHeight, 2f, 2f, 2f, 2f);
            lblDeleteLabel.SetLabelText("Delete");
            lblDeleteLabel.SetBorderWidth(1.0f);
            lblDeleteLabel.SetFontName("Verdana");
            lblDeleteLabel.SetFontSize(12f);
            lblDeleteLabel.SetTag(iDeleteEquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblDeleteLabel.SetCellColour("Pale Yellow");
            }
            else
            {
                lblDeleteLabel.SetCellColour("Pale Orange");
            }
            
            lblDeleteLabelVw = lblDeleteLabel.GetLabelCell();
            arrItems2[9] = lblDeleteLabelVw;
            
            hdrRow.AddSubviews(arrItems2);
            
            iHeightToAdd += iRowHeight - 1; //This is because of the 1 pixel overlap of the border (not required on the last one)
            iHdrVert += iRowHeight - 1; //This is because of the 1 pixel overlap of the border (not required on the last one)
            
            iUtils.CreateFormGridItem lblMake = new iUtils.CreateFormGridItem();
            UIView lblMakeVw = new UIView();
            lblMake.SetDimensions(0f,iHdrVert, 401f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblMake.SetLabelText("Make");
            lblMake.SetBorderWidth(1.0f);
            lblMake.SetFontName("Verdana");
            lblMake.SetFontSize(12f);
            lblMake.SetTag(iMakeEquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblMake.SetCellColour("Pale Yellow");
            }
            else
            {
                lblMake.SetCellColour("Pale Orange");
            }
            
            lblMakeVw = lblMake.GetLabelCell();
            arrItems[0] = lblMakeVw;
            
            iUtils.CreateFormGridItem lblModel = new iUtils.CreateFormGridItem();
            UIView lblModelVw = new UIView();
            lblModel.SetDimensions(400f,iHdrVert, 501f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblModel.SetLabelText("Model");
            lblModel.SetBorderWidth(1.0f);
            lblModel.SetFontName("Verdana");
            lblModel.SetFontSize(12f);
            lblModel.SetTag(iModelEquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblModel.SetCellColour("Pale Yellow");
            }
            else
            {
                lblModel.SetCellColour("Pale Orange");
            }
            
            lblModelVw = lblModel.GetLabelCell();
            arrItems[1] = lblModelVw;
            
            iUtils.CreateFormGridItem lblBlank1 = new iUtils.CreateFormGridItem();
            UIView lblBlank1Vw = new UIView();
            lblBlank1.SetDimensions(900f,iHdrVert, 100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBlank1.SetLabelText("Asset Id");
            lblBlank1.SetBorderWidth(1.0f);
            lblBlank1.SetFontName("Verdana");
            lblBlank1.SetFontSize(12f);
            lblBlank1.SetTag(iBlank1EquipLabelTagId * (iRowNo+1));
            
            if (iRowNo % 2 == 0)
            {
                lblBlank1.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBlank1.SetCellColour("Pale Orange");
            }
            
            lblBlank1Vw = lblBlank1.GetLabelCell();
            arrItems[2] = lblBlank1Vw;
            
            hdrRow.AddSubviews(arrItems);
            
            iHeightToAdd += iRowHeight;
            iHdrVert += iRowHeight;
            
            return hdrRow;
        }

        public UIView BuildEquipmentItemRowDetails(int iSectionCounterId, int iPwrIdRowNo, int iEquipRowNo, string sPwrId, 
                                                   int iAutoId, int iMaximoAssetId, string sStringNo,
                                                   string sMake, string sModel, string sSPN, string sDOM,
                                                   string sFloor, string sSuite,string sRack, string sSubRack, 
                                                   string sPosition, string sEquipType, string sSerialNo,
                                                   int iEquipmentType, int iDuplicate, bool bNewRow, bool bReadOnly, ref float iHeightToAdd)
        {
            DateClass dt = new DateClass();
            iHeightToAdd = 0.0f;
            UIView hdrRow = new UIView();
            float iHdrVert = 0.0f;
            float iRowHeight = 40f;
            UIView[] arrItems = new UIView[8];
            UIView[] arrItems2 = new UIView[18];
            UIView[] arrItems3 = new UIView[6];
            UIView[] arrItems4 = new UIView[7];
            UIView vwBlank = new UIView();

            UILabel hfSectionCounter = new UILabel();
            hfSectionCounter.Text = iSectionCounterId.ToString();
            hfSectionCounter.Tag = iEquipmentRowSectionCounterTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfSectionCounter.Hidden = true;
            arrItems4[0] = hfSectionCounter;
            
            UILabel hfPwrId = new UILabel();
            hfPwrId.Text = sPwrId;
            hfPwrId.Tag = iEquipmentRowPwrIdTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfPwrId.Hidden = true;
            arrItems4[1] = hfPwrId;
            
            UILabel hfRowStatus = new UILabel();
            if (bNewRow)
            {
                hfRowStatus.Text = "2"; //2 means new
            }
            else
            {
                hfRowStatus.Text = "0";
            }
            
            hfRowStatus.Tag = iEquipmentRowStatusTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfRowStatus.Hidden = true;
            arrItems4[2] = hfRowStatus;
            
            UILabel hfAutoId = new UILabel();
            hfAutoId.Text = iAutoId.ToString();
            hfAutoId.Tag = iEquipmentRowAutoIdTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfAutoId.Hidden = true;
            arrItems4[3] = hfAutoId;
            
            UILabel hfMaximoAssetId = new UILabel();
            hfMaximoAssetId.Text = iMaximoAssetId.ToString();
            hfMaximoAssetId.Tag = iEquipmentRowMaximoAssetIdTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfMaximoAssetId.Hidden = true;
            arrItems4[4] = hfMaximoAssetId;
            
            UILabel hfEquipmentTypeId = new UILabel();
            hfEquipmentTypeId.Text = iEquipmentType.ToString();
            hfEquipmentTypeId.Tag = iEquipmentTypeTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfEquipmentTypeId.Hidden = true;
            arrItems4[5] = hfEquipmentTypeId;

            UILabel hfDuplicateId = new UILabel();
            hfDuplicateId.Text = iDuplicate.ToString();
            hfDuplicateId.Tag = iDuplicateTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfDuplicateId.Hidden = true;
            arrItems4[6] = hfDuplicateId;

            hdrRow.AddSubviews(arrItems4);

            
            //***************************************************************//
            //              1st Row                                          //
            //***************************************************************//
            iUtils.CreateFormGridItem lblFloor = new iUtils.CreateFormGridItem();
            UIView lblFloorVw = new UIView();
            lblFloor.SetDimensions(0f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblFloor.SetLabelText(sFloor);
            lblFloor.SetBorderWidth(0.0f);
            lblFloor.SetFontName("Verdana");
            lblFloor.SetFontSize(12f);
            lblFloor.SetTag(iEquipmentFloorTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
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
            txtFloorView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 1);};
            txtFloorView.ReturnKeyType = UIReturnKeyType.Next;
            txtFloorView.ShouldEndEditing += (sender) => {
                return ValidateFloor(sender, 0);};
            txtFloorView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 1);};

            if(bReadOnly)
            {
                txtFloorView.Enabled = false;
            }
            arrItems2 [0] = lblFloorVw;
            
            UILabel hfCurrentFloor = new UILabel();
            hfCurrentFloor.Text = sFloor;
            hfCurrentFloor.Tag = iEquipmentFloorHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentFloor.Hidden = true;
            arrItems2 [1] = hfCurrentFloor;
            
            iUtils.CreateFormGridItem lblSuite = new iUtils.CreateFormGridItem();
            UIView lblSuiteVw = new UIView();
            lblSuite.SetDimensions(50f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSuite.SetLabelText(sSuite);
            lblSuite.SetBorderWidth(0.0f);
            lblSuite.SetFontName("Verdana");
            lblSuite.SetFontSize(12f);
            lblSuite.SetTag(iEquipmentSuiteTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
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
                return SetGlobalEditItems(sender, 2);};
            txtSuiteView.ReturnKeyType = UIReturnKeyType.Next;
            txtSuiteView.ShouldEndEditing += (sender) => {
                return ValidateSuite(sender, 0);};
            txtSuiteView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 2);};
            
            if(bReadOnly)
            {
                txtSuiteView.Enabled = false;
            }
            arrItems2 [2] = lblSuiteVw;
            
            UILabel hfCurrentSuite = new UILabel();
            hfCurrentSuite.Text = sSuite;
            hfCurrentSuite.Tag = iEquipmentSuiteHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentSuite.Hidden = true;
            arrItems2 [3] = hfCurrentSuite;
            
            iUtils.CreateFormGridItem lblRack = new iUtils.CreateFormGridItem();
            UIView lblRackVw = new UIView();
            lblRack.SetDimensions(100f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblRack.SetLabelText(sRack);
            lblRack.SetBorderWidth(0.0f);
            lblRack.SetFontName("Verdana");
            lblRack.SetFontSize(12f);
            lblRack.SetTag(iEquipmentRackTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
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
                return SetGlobalEditItems(sender, 3);};
            txtRackView.ReturnKeyType = UIReturnKeyType.Next;
            txtRackView.ShouldEndEditing += (sender) => {
                return ValidateRack(sender, 0);};
            txtRackView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 3);};
            
            if(bReadOnly)
            {
                txtRackView.Enabled = false;
            }
            arrItems2 [4] = lblRackVw;
            
            UILabel hfCurrentRack = new UILabel();
            hfCurrentRack.Text = sRack;
            hfCurrentRack.Tag = iEquipmentRackHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentRack.Hidden = true;
            arrItems2 [5] = hfCurrentRack;

            if(iEquipmentType > 3)
            {
                iUtils.CreateFormGridItem lblSubRack = new iUtils.CreateFormGridItem();
                UIView lblSubRackVw = new UIView();
                lblSubRack.SetDimensions(140f, iHdrVert, 60f, iRowHeight, 2f, 2f, 2f, 2f);
                lblSubRack.SetLabelText(sSubRack);
                lblSubRack.SetBorderWidth(0.0f);
                lblSubRack.SetFontName("Verdana");
                lblSubRack.SetFontSize(12f);
                lblSubRack.SetTag(iEquipmentSubRackTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
                
                if (iPwrIdRowNo % 2 == 0)
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
                    return SetGlobalEditItems(sender, 4);};
                txtSubRackView.ReturnKeyType = UIReturnKeyType.Next;
                txtSubRackView.ShouldEndEditing += (sender) => {
                    return ValidateSubRack(sender, 0);};
                txtSubRackView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 4);};
                
                if(bReadOnly)
                {
                    txtSubRackView.Enabled = false;
                }
                arrItems2 [6] = lblSubRackVw;
                
                UILabel hfCurrentSubRack = new UILabel();
                hfCurrentSubRack.Text = sSubRack;
                hfCurrentSubRack.Tag = iEquipmentSubRackHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
                hfCurrentSubRack.Hidden = true;
                arrItems2 [7] = hfCurrentSubRack;
            }
            else
            {
                iUtils.CreateFormGridItem lblSubRack = new iUtils.CreateFormGridItem();
                UIView lblSubRackVw = new UIView();
                lblSubRack.SetDimensions(140f, iHdrVert, 60f, iRowHeight, 2f, 2f, 2f, 2f);
                lblSubRack.SetBorderWidth(0.0f);
                
                if (iPwrIdRowNo % 2 == 0)
                {
                    lblSubRack.SetCellColour("Pale Yellow");
                }
                else
                {
                    lblSubRack.SetCellColour("Pale Orange");
                }
                
                lblSubRackVw = lblSubRack.GetLabelCell();
                arrItems2 [6] = lblSubRackVw;
                arrItems2 [7] = vwBlank;
            }

            if(iEquipmentType > 4)
            {
                iUtils.CreateFormGridItem lblPosition = new iUtils.CreateFormGridItem();
                UIView lblPositionVw = new UIView();
                lblPosition.SetDimensions(200f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
                lblPosition.SetLabelText(sPosition);
                lblPosition.SetBorderWidth(0.0f);
                lblPosition.SetFontName("Verdana");
                lblPosition.SetFontSize(12f);
                lblPosition.SetTag(iEquipmentPositionTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
                
                if (iPwrIdRowNo % 2 == 0)
                {
                    lblPosition.SetCellColour("Pale Yellow");
                }
                else
                {
                    lblPosition.SetCellColour("Pale Orange");
                }
                
                lblPositionVw = lblPosition.GetTextFieldCell();
                UITextField txtPositionView = lblPosition.GetTextFieldView();
                txtPositionView.AutocorrectionType = UITextAutocorrectionType.No;
                txtPositionView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtPositionView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 5);};
                txtPositionView.ReturnKeyType = UIReturnKeyType.Next;
                txtPositionView.ShouldEndEditing += (sender) => {
                    return ValidatePosition(sender, 0);};
                txtPositionView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 5);};
                
                if(bReadOnly)
                {
                    txtPositionView.Enabled = false;
                }
                arrItems2 [8] = lblPositionVw;
                
                UILabel hfCurrentPosition = new UILabel();
                hfCurrentPosition.Text = sPosition;
                hfCurrentPosition.Tag = iEquipmentPositionHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
                hfCurrentPosition.Hidden = true;
                arrItems2 [9] = hfCurrentPosition;
            }
            else
            {
                iUtils.CreateFormGridItem lblPosition = new iUtils.CreateFormGridItem();
                UIView lblPositionVw = new UIView();
                lblPosition.SetDimensions(200f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
                lblPosition.SetBorderWidth(0.0f);

                if (iPwrIdRowNo % 2 == 0)
                {
                    lblPosition.SetCellColour("Pale Yellow");
                }
                else
                {
                    lblPosition.SetCellColour("Pale Orange");
                }
                
                lblPositionVw = lblPosition.GetLabelCell();
                arrItems2 [8] = lblPositionVw;
                arrItems2 [9] = vwBlank;
            }

            if(iEquipmentType > 5)
            {
                iUtils.CreateFormGridItem lblString = new iUtils.CreateFormGridItem();
                UIView lblStringVw = new UIView();
                lblString.SetDimensions(250f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
                lblString.SetLabelText(sStringNo);
                lblString.SetBorderWidth(0.0f);
                lblString.SetFontName("Verdana");
                lblString.SetFontSize(12f);
                lblString.SetTag(iEquipmentStringTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
                
                if (iPwrIdRowNo % 2 == 0)
                {
                    lblString.SetCellColour("Pale Yellow");
                }
                else
                {
                    lblString.SetCellColour("Pale Orange");
                }
                
                lblStringVw = lblString.GetTextFieldCell();
                UITextField txtStringView = lblString.GetTextFieldView();
                txtStringView.AutocorrectionType = UITextAutocorrectionType.No;
                txtStringView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                txtStringView.ReturnKeyType = UIReturnKeyType.Next;
                txtStringView.ShouldBeginEditing += (sender) => {
                    return SetGlobalEditItems(sender, 6);};
                txtStringView.ShouldEndEditing += (sender) => {
                    return ValidateBankNo(sender, 2, 0);};
                txtStringView.ShouldReturn += (sender) => {
                    return MoveNextTextField(sender, 6);};
                
                if(bReadOnly)
                {
                    txtStringView.Enabled = false;
                }
                arrItems2 [10] = lblStringVw;
                
                UILabel hfCurrentString = new UILabel();
                hfCurrentString.Text = sStringNo;
                hfCurrentString.Tag = iEquipmentStringHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
                hfCurrentString.Hidden = true;
                arrItems2 [11] = hfCurrentString;
            }
            else
            {
                iUtils.CreateFormGridItem lblString = new iUtils.CreateFormGridItem();
                UIView lblStringVw = new UIView();
                lblString.SetDimensions(250f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
                lblString.SetBorderWidth(0.0f);
                
                if (iPwrIdRowNo % 2 == 0)
                {
                    lblString.SetCellColour("Pale Yellow");
                }
                else
                {
                    lblString.SetCellColour("Pale Orange");
                }
                
                lblStringVw = lblString.GetLabelCell();
                arrItems2 [10] = lblStringVw;
                arrItems2 [11] = vwBlank;
            }

            iUtils.CreateFormGridItem radEquipType = new iUtils.CreateFormGridItem();
            UIView radEquipTypeVw = new UIView();
            radEquipType.SetDimensions(300f, iHdrVert, 200f, iRowHeight * 2, 8f, iRowHeight / 2f, 8f, iRowHeight / 2f);
            radEquipType.SetFontName("Verdana");
            radEquipType.SetFontSize(12f);
            radEquipType.SetTag(iEquipmentEquipTypeTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
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
            
            if (bNewRow || iDuplicate < 0)
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

            arrItems2 [12] = radEquipTypeVw;
            
            iUtils.CreateFormGridItem txtDOM = new iUtils.CreateFormGridItem();
            UIView txtDOMVw = new UIView();
            txtDOM.SetDimensions(500f, iHdrVert, 80f, iRowHeight * 2, 2f, iRowHeight / 2f, 2f, iRowHeight / 2f);
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
            txtDOM.SetTag(iEquipmentDOMTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
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
                return SetGlobalEditItems(sender, 7);};
            txtDOMTextView.ShouldEndEditing += (sender) => {
                return ValidateDOM(sender, 0);};
            txtDOMTextView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 7);};

            if(bReadOnly)
            {
                txtDOMTextView.Enabled = false;
            }
            arrItems2 [13] = txtDOMVw;
            
            UILabel hfCurrentDOM = new UILabel();
            hfCurrentDOM.Text = sDOMDisplay;
            hfCurrentDOM.Tag = iEquipmentDOMHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentDOM.Hidden = true;
            arrItems2 [14] = hfCurrentDOM;

            iUtils.CreateFormGridItem lblSerialNo = new iUtils.CreateFormGridItem();
            UIView lblSerialNoVw = new UIView();
            lblSerialNo.SetDimensions(580f, iHdrVert, 300f, iRowHeight * 2f, 2f, iRowHeight / 2f, 2f, iRowHeight / 2f);
            lblSerialNo.SetLabelText(sSerialNo);
            lblSerialNo.SetBorderWidth(0.0f);
            lblSerialNo.SetFontName("Verdana");
            lblSerialNo.SetFontSize(12f);
            lblSerialNo.SetTag(iEquipmentSerialNoTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
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
            arrItems2 [15] = lblSerialNoVw;
            
            UILabel hfCurrentSerialNo = new UILabel();
            hfCurrentSerialNo.Text = sSerialNo;
            hfCurrentSerialNo.Tag = iEquipmentSerialNoHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentSerialNo.Hidden = true;
            arrItems2 [16] = hfCurrentSerialNo;

            iUtils.CreateFormGridItem btnDelete = new iUtils.CreateFormGridItem();
            UIView btnDeleteVw = new UIView();
            btnDelete.SetDimensions(880f, iHdrVert, 120f, iRowHeight * 2f, 8f, iRowHeight / 2f, 8f, iRowHeight / 2f);
            btnDelete.SetLabelText("Delete");
            btnDelete.SetBorderWidth(0.0f);
            btnDelete.SetFontName("Verdana");
            btnDelete.SetFontSize(12f);
            btnDelete.SetTag(iEquipmentDeleteBtnTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
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
                DeleteEquipmentRow(sender, e);};
            
            if (iMaximoAssetId >= 0)
            {
                btnDeleteButton.Enabled = false;
            }

            if(bReadOnly)
            {
                btnDeleteButton.Enabled = false;
            }
            arrItems2[17] = btnDeleteVw;
            
            hdrRow.AddSubviews(arrItems2);
            
            iHeightToAdd += iRowHeight; 
            iHdrVert += iRowHeight; 
            
            //***************************************************************//
            //              2nd Row                                          //
            //***************************************************************//
            
            iUtils.CreateFormGridItem btnFloorSearch = new iUtils.CreateFormGridItem();
            UIView btnFloorSearchVw = new UIView();
            btnFloorSearch.SetDimensions(0f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
            btnFloorSearch.SetLabelText("...");
            btnFloorSearch.SetBorderWidth(0.0f);
            btnFloorSearch.SetFontName("Verdana");
            btnFloorSearch.SetFontSize(12f);
            btnFloorSearch.SetTag(iEquipmentFloorSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
            if (iPwrIdRowNo % 2 == 0)
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
            btnSuiteSearch.SetTag(iEquipmentSuiteSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
            if (iPwrIdRowNo % 2 == 0)
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
            btnRackSearch.SetDimensions(100f,iHdrVert, 40f, iRowHeight, 3f, 4f, 3f, 4f);
            btnRackSearch.SetLabelText("...");
            btnRackSearch.SetBorderWidth(0.0f);
            btnRackSearch.SetFontName("Verdana");
            btnRackSearch.SetFontSize(12f);
            btnRackSearch.SetTag(iEquipmentRackSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
            if (iPwrIdRowNo % 2 == 0)
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
            
            if(iEquipmentType > 3)
            {
                iUtils.CreateFormGridItem btnSubRackSearch = new iUtils.CreateFormGridItem();
                UIView btnSubRackSearchVw = new UIView();
                btnSubRackSearch.SetDimensions(140f,iHdrVert, 60f, iRowHeight, 13f, 4f, 13f, 4f);
                btnSubRackSearch.SetLabelText("...");
                btnSubRackSearch.SetBorderWidth(0.0f);
                btnSubRackSearch.SetFontName("Verdana");
                btnSubRackSearch.SetFontSize(12f);
                btnSubRackSearch.SetTag(iEquipmentSubRackSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
                if (iPwrIdRowNo % 2 == 0)
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
            }
            else
            {
                iUtils.CreateFormGridItem btnSubRackSearch = new iUtils.CreateFormGridItem();
                UIView btnSubRackSearchVw = new UIView();
                btnSubRackSearch.SetDimensions(140f,iHdrVert, 60f, iRowHeight, 13f, 4f, 13f, 4f);
                btnSubRackSearch.SetBorderWidth(0.0f);
                btnSubRackSearch.SetTag(iEquipmentSubRackSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));

                if (iPwrIdRowNo % 2 == 0)
                {
                    btnSubRackSearch.SetCellColour("Pale Yellow");
                }
                else
                {
                    btnSubRackSearch.SetCellColour("Pale Orange");
                }

                btnSubRackSearchVw = btnSubRackSearch.GetLabelCell();
                arrItems3[3] = btnSubRackSearchVw;
            }

            if(iEquipmentType > 4)
            {
                iUtils.CreateFormGridItem btnPositionSearch = new iUtils.CreateFormGridItem();
                UIView btnPositionSearchVw = new UIView();
                btnPositionSearch.SetDimensions(200f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
                btnPositionSearch.SetLabelText("...");
                btnPositionSearch.SetBorderWidth(0.0f);
                btnPositionSearch.SetFontName("Verdana");
                btnPositionSearch.SetFontSize(12f);
                btnPositionSearch.SetTag(iEquipmentPositionSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
                if (iPwrIdRowNo % 2 == 0)
                {
                    btnPositionSearch.SetCellColour("Pale Yellow");
                }
                else
                {
                    btnPositionSearch.SetCellColour("Pale Orange");
                }
                btnPositionSearchVw = btnPositionSearch.GetButtonCell();
                
                UIButton btnPositionSearchButton = new UIButton();
                btnPositionSearchButton = btnPositionSearch.GetButton();
                btnPositionSearchButton.TouchUpInside += (sender,e) => {OpenSearchView(sender, e, 5);};
                
                if(bReadOnly)
                {
                    btnPositionSearchButton.Enabled = false;
                }
                arrItems3[4] = btnPositionSearchVw;
            }
            else
            {
                iUtils.CreateFormGridItem btnPositionSearch = new iUtils.CreateFormGridItem();
                UIView btnPositionSearchVw = new UIView();
                btnPositionSearch.SetDimensions(200f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
                btnPositionSearch.SetBorderWidth(0.0f);
                btnPositionSearch.SetTag(iEquipmentSubRackSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
                
                if (iPwrIdRowNo % 2 == 0)
                {
                    btnPositionSearch.SetCellColour("Pale Yellow");
                }
                else
                {
                    btnPositionSearch.SetCellColour("Pale Orange");
                }
                
                btnPositionSearchVw = btnPositionSearch.GetLabelCell();
                arrItems3[4] = btnPositionSearchVw;
            }

            if(iEquipmentType > 5)
            {
                iUtils.CreateFormGridItem btnStringSearch = new iUtils.CreateFormGridItem();
                UIView btnStringSearchVw = new UIView();
                btnStringSearch.SetDimensions(250f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
                btnStringSearch.SetLabelText("...");
                btnStringSearch.SetBorderWidth(0.0f);
                btnStringSearch.SetFontName("Verdana");
                btnStringSearch.SetFontSize(12f);
                btnStringSearch.SetTag(iEquipmentStringSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
                if (iPwrIdRowNo % 2 == 0)
                {
                    btnStringSearch.SetCellColour("Pale Yellow");
                }
                else
                {
                    btnStringSearch.SetCellColour("Pale Orange");
                }
                btnStringSearchVw = btnStringSearch.GetButtonCell();
                
                UIButton btnStringSearchButton = new UIButton();
                btnStringSearchButton = btnStringSearch.GetButton();
                btnStringSearchButton.TouchUpInside += (sender,e) => {OpenSearchView(sender, e, 7);};
                
                if(bReadOnly)
                {
                    btnStringSearchButton.Enabled = false;
                }
                arrItems3[5] = btnStringSearchVw;
            }
            else
            {
                iUtils.CreateFormGridItem btnStringSearch = new iUtils.CreateFormGridItem();
                UIView btnStringSearchVw = new UIView();
                btnStringSearch.SetDimensions(250f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
                btnStringSearch.SetBorderWidth(0.0f);
                btnStringSearch.SetTag(iEquipmentSubRackSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
                
                if (iPwrIdRowNo % 2 == 0)
                {
                    btnStringSearch.SetCellColour("Pale Yellow");
                }
                else
                {
                    btnStringSearch.SetCellColour("Pale Orange");
                }
                
                btnStringSearchVw = btnStringSearch.GetLabelCell();
                arrItems3[5] = btnStringSearchVw;
            }

            hdrRow.AddSubviews(arrItems3);
            
            iHeightToAdd += iRowHeight; 
            iHdrVert += iRowHeight; 
            
            //***************************************************************//
            //              3rd Row                                          //
            //***************************************************************//
            
            iUtils.CreateFormGridItem lblBankMake = new iUtils.CreateFormGridItem();
            UIView lblBankMakeVw = new UIView();
            lblBankMake.SetDimensions(0f, iHdrVert, 340f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBankMake.SetLabelText(sMake);
            lblBankMake.SetBorderWidth(0.0f);
            lblBankMake.SetFontName("Verdana");
            lblBankMake.SetFontSize(12f);
            lblBankMake.SetTag(iEquipmentMakeTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
            {
                lblBankMake.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBankMake.SetCellColour("Pale Orange");
            }
            
            lblBankMakeVw = lblBankMake.GetLabelCell();
            arrItems[0] = lblBankMakeVw;
            
            iUtils.CreateFormGridItem btnMakeSearch = new iUtils.CreateFormGridItem();
            UIView btnMakeSearchVw = new UIView();
            btnMakeSearch.SetDimensions(340f, iHdrVert, 60f, iRowHeight, 8f, 4f, 8f, 4f);
            btnMakeSearch.SetLabelText("...");
            btnMakeSearch.SetBorderWidth(0.0f);
            btnMakeSearch.SetFontName("Verdana");
            btnMakeSearch.SetFontSize(12f);
            btnMakeSearch.SetTag(iEquipmentMakeSearchTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            if (iPwrIdRowNo % 2 == 0)
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
            arrItems[1] = btnMakeSearchVw;
            
            iUtils.CreateFormGridItem lblBankModel = new iUtils.CreateFormGridItem();
            UIView lblBankModelVw = new UIView();
            lblBankModel.SetDimensions(400f, iHdrVert, 440f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBankModel.SetLabelText(sModel);
            lblBankModel.SetBorderWidth(0.0f);
            lblBankModel.SetFontName("Verdana");
            lblBankModel.SetFontSize(12f);
            lblBankModel.SetTag(iEquipmentModelTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
            if (iPwrIdRowNo % 2 == 0)
            {
                lblBankModel.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBankModel.SetCellColour("Pale Orange");
            }
            
            lblBankModelVw = lblBankModel.GetLabelCell();
            arrItems[2] = lblBankModelVw;
            
            UILabel hfSPN = new UILabel();
            hfSPN.Text = sSPN;
            hfSPN.Tag = iEquipmentSPNHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfSPN.Hidden = true;
            arrItems[3] = hfSPN;
            
            iUtils.CreateFormGridItem btnModelSearch = new iUtils.CreateFormGridItem();
            UIView btnModelSearchVw = new UIView();
            btnModelSearch.SetDimensions(840f, iHdrVert, 60f, iRowHeight, 8f, 4f, 8f, 4f);
            btnModelSearch.SetLabelText("...");
            btnModelSearch.SetBorderWidth(0.0f);
            btnModelSearch.SetFontName("Verdana");
            btnModelSearch.SetFontSize(12f);
            btnModelSearch.SetTag(iEquipmentModelSearchTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            if (iPwrIdRowNo % 2 == 0)
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
            arrItems[4] = btnModelSearchVw;

            bool bAssetTextHidden = true;
            string sMaximoAssetId = iMaximoAssetId.ToString();
            if (sEquipType == "U")
            {
                bAssetTextHidden = false;
                sMaximoAssetId = iMaximoAssetId.ToString().PadLeft(10,'0');
            }

            iUtils.CreateFormGridItem txtMaximoAssetId = new iUtils.CreateFormGridItem();
            UIView txtMaximoAssetIdVw = new UIView();
            txtMaximoAssetId.SetDimensions(900f,iHdrVert, 100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            txtMaximoAssetId.SetLabelText(sMaximoAssetId);
            txtMaximoAssetId.SetBorderWidth(0.0f);
            txtMaximoAssetId.SetFontName("Verdana");
            txtMaximoAssetId.SetFontSize(12f);
            txtMaximoAssetId.SetTextAlignment("right");
            txtMaximoAssetId.SetHidden(bAssetTextHidden);
            txtMaximoAssetId.SetTag(iEquipmentMaximoAssetTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));

            if (iPwrIdRowNo % 2 == 0)
            {
                txtMaximoAssetId.SetCellColour("Pale Yellow");
            }
            else
            {
                txtMaximoAssetId.SetCellColour("Pale Orange");
            }

            txtMaximoAssetIdVw = txtMaximoAssetId.GetTextFieldCell();
            txtMaximoAssetIdVw.Hidden = bAssetTextHidden;
            txtMaximoAssetIdVw.Tag = iEquipmentMaximoAssetTextViewTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);

            UITextField txtMaximoAssetIdView = txtMaximoAssetId.GetTextFieldView();
            txtMaximoAssetIdView.AutocorrectionType = UITextAutocorrectionType.No;
            txtMaximoAssetIdView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtMaximoAssetIdView.ReturnKeyType = UIReturnKeyType.Next;
            txtMaximoAssetIdView.ShouldBeginEditing += (sender) => {
                return SetGlobalEditItems(sender, 9);};
            txtMaximoAssetIdView.ShouldEndEditing += (sender) => {
                return ValidateMaximoAssetId(sender, 0);};
            txtMaximoAssetIdView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 9);};
            
            if(bReadOnly)
            {
                txtMaximoAssetIdView.Enabled = false;
            }

            iUtils.CreateFormGridItem lblMaximoAssetId = new iUtils.CreateFormGridItem();
            UIView lblMaximoAssetIdVw = new UIView();
            lblMaximoAssetId.SetDimensions(900f,iHdrVert, 100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblMaximoAssetId.SetLabelText(sMaximoAssetId);
            lblMaximoAssetId.SetBorderWidth(0.0f);
            lblMaximoAssetId.SetFontName("Verdana");
            lblMaximoAssetId.SetFontSize(12f);
            lblMaximoAssetId.SetTextAlignment("right");
            lblMaximoAssetId.SetHidden(!bAssetTextHidden);
            lblMaximoAssetId.SetTag(iEquipmentMaximoLblAssetTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));

            if (iPwrIdRowNo % 2 == 0)
            {
                lblMaximoAssetId.SetCellColour("Pale Yellow");
            }
            else
            {
                lblMaximoAssetId.SetCellColour("Pale Orange");
            }

            lblMaximoAssetIdVw = lblMaximoAssetId.GetLabelCell();
            lblMaximoAssetIdVw.Hidden = !bAssetTextHidden;
            lblMaximoAssetIdVw.Tag = iEquipmentMaximoLblViewAssetTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);

            arrItems[5] = txtMaximoAssetIdVw;
            arrItems[6] = lblMaximoAssetIdVw;

            UILabel hfCurrentMaximoAssetId = new UILabel();
            hfCurrentMaximoAssetId.Text = sMaximoAssetId;
            hfCurrentMaximoAssetId.Tag = iEquipmentMaximoAssetHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentMaximoAssetId.Hidden = true;
            arrItems[7] = hfCurrentMaximoAssetId;

            hdrRow.AddSubviews(arrItems);
            
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
        
        public void OpenStringList (object sender, EventArgs e)
        {
            UIButton btnBankNoSearch = (UIButton)sender;
            ScreenUtils scnUtils = new ScreenUtils ();
            scnUtils.GetAbsolutePosition (btnBankNoSearch);
            float iTop = scnUtils.GetPositionTop ();
            float iLeft = scnUtils.GetPositionLeft ();
            int iBtnTagId = btnBankNoSearch.Tag;
            int iPwrIdRow = iBtnTagId / iEquipmentStringSearchTagId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iEquipmentStringSearchTagId);
            int iSectionCounterTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow  + iStringRow;
            UILabel hfSectionCounter = (UILabel)View.ViewWithTag (iSectionCounterTagId);
            int iSectionCounterId = Convert.ToInt32 (hfSectionCounter.Text);
            
            clsTabletDB.ITPValidHierarchy ITPHierarchy = new clsTabletDB.ITPValidHierarchy();
            string[] sSolarStringNos = ITPHierarchy.GetValidHierarchy(7);
            
            //Create a list and convert the string array to the list. Why the system cannot take a simple string array is beyond me!!!
            List<string> listStringNo = new List<string> ();
            Array.ForEach (sSolarStringNos, value => listStringNo.Add (value.ToString ()));
            
            TableViewSource tabdata = new TableViewSource (listStringNo, true);
            tabdata.SetFont("Verdana",10f);
            UITableView cmbStringNo = new UITableView ();
            
            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                cmbStringNo.Frame = new RectangleF(iLeft, iTop - 190f, 90f, 200f);
            } 
            else 
            {
                cmbStringNo.Frame = new RectangleF(iLeft, iTop, 90f, 200f);
            }
            
            tabdata.SetParent(cmbStringNo);
            tabdata.SetUpdateFieldType("UITextField");
            UITextField lblVwUpdate = (UITextField)View.ViewWithTag (iEquipmentStringTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetTextFieldToUpdate(lblVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);
            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((iSectionCounterId + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);
            
            cmbStringNo.Source = tabdata;
            iUtils.SESTable thistable = new iUtils.SESTable();
            string sSelectedValue = lblVwUpdate.Text;
            thistable.SetTableSelectedText(cmbStringNo, sSelectedValue, sSolarStringNos, true);
            
            //Get the main scroll view
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            scrollVw.AddSubview(cmbStringNo);
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
            int iPwrIdRow = iBtnTagId / iEquipmentMakeSearchTagId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iEquipmentMakeSearchTagId);
            int iSectionCounterTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow + iStringRow;
            UILabel hfSectionCounter = (UILabel)View.ViewWithTag (iSectionCounterTagId);
            int iSectionCounterId = Convert.ToInt32(hfSectionCounter.Text);
                       
            //Create a list and convert the string array to the list. Why the system cannot take a simple string arary is beyond me!!!
            UILabel lblEquipmentType = (UILabel)View.ViewWithTag (iEquipmentTypeTagId * (iPwrIdRow) + (iStringRow));
            int iEquipmentType = Convert.ToInt32(lblEquipmentType.Text);
            
            List<string> mylist = new List<string> ();
            clsTabletDB.ITPInventory ITPInventory = new clsTabletDB.ITPInventory ();
            string[] sMakes;
            switch(iEquipmentType)
            {
                case 3:
                    sMakes = ITPInventory.GetRackMakes ();
                    m_sRackMakes = sMakes;
                    Array.ForEach (m_sRackMakes, value => mylist.Add (value.ToString ()));
                    break;
                case 4:
                    sMakes = ITPInventory.GetSubRackMakes ();
                    m_sSubRackMakes = sMakes;
                    Array.ForEach (m_sSubRackMakes, value => mylist.Add (value.ToString ()));
                    break;
                case 5:
                    sMakes = ITPInventory.GetPositionMakes ();
                    m_sPositionMakes = sMakes;
                    Array.ForEach (m_sPositionMakes, value => mylist.Add (value.ToString ()));
                    break;
                case 7:
                    sMakes = ITPInventory.GetSolarStringMakes ();
                    m_sSolarStringMakes = sMakes;
                    Array.ForEach (m_sSolarStringMakes, value => mylist.Add (value.ToString ()));
                    break;
                default:
                    sMakes = ITPInventory.GetPositionMakes ();
                    m_sPositionMakes = sMakes;
                    Array.ForEach (m_sPositionMakes, value => mylist.Add (value.ToString ()));
                    break;
            }

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
            UILabel txtVwUpdate = (UILabel)View.ViewWithTag (iEquipmentMakeTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetLabelViewToUpdate(txtVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);
            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UIButton btnSectionSave = (UIButton)View.ViewWithTag ((iSectionCounterId + 1) * iSaveSectionBtnTagId);
            tabdata.SetSectionSaveButton(btnSectionSave);
            UILabel lblUnsavedSectionFlag = (UILabel)View.ViewWithTag ((iSectionCounterId + 1) * iSectionStatusTagId);
            tabdata.SetUnsavedChangesSectionHiddenLabel(lblUnsavedSectionFlag);
            UILabel lblViewModel = (UILabel)View.ViewWithTag (iEquipmentModelTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetMakePostUpdate(1, lblViewModel, btnMakeSearch);
            
            cmbMake.Source = tabdata;
            iUtils.SESTable thistable = new iUtils.SESTable();
            string sSelectedValue = txtVwUpdate.Text;
            switch(iEquipmentType)
            {
                case 3:
                    thistable.SetTableSelectedText(cmbMake, sSelectedValue, m_sRackMakes, true);
                    break;
                case 4:
                    thistable.SetTableSelectedText(cmbMake, sSelectedValue, m_sSubRackMakes, true);
                    break;
                case 5:
                    thistable.SetTableSelectedText(cmbMake, sSelectedValue, m_sPositionMakes, true);
                    break;
                case 7:
                    thistable.SetTableSelectedText(cmbMake, sSelectedValue, m_sSolarStringMakes, true);
                    break;
                default:
                    thistable.SetTableSelectedText(cmbMake, sSelectedValue, m_sPositionMakes, true);
                    break;
            }

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
            int iPwrIdRow = iBtnTagId / iEquipmentModelSearchTagId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iEquipmentModelSearchTagId);
            int iSectionCounterTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow + iStringRow;
            UILabel hfSectionCounter = (UILabel)View.ViewWithTag (iSectionCounterTagId);
            int iSectionCounterId = Convert.ToInt32 (hfSectionCounter.Text);
            UILabel lblSupplier = (UILabel)View.ViewWithTag (iEquipmentMakeTagId * (iPwrIdRow) + (iStringRow));
            string sSupplier = lblSupplier.Text;
            UIButton btnMakeSearch = (UIButton)View.ViewWithTag (iEquipmentMakeSearchTagId * (iPwrIdRow) + (iStringRow));
            btnMakeSearch.Enabled = false;

            if (sSupplier == "") 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("You must select a make before you can select a model");
                return;
            }
            
            UILabel lblEquipmentType = (UILabel)View.ViewWithTag (iEquipmentTypeTagId * (iPwrIdRow) + (iStringRow));
            int iEquipmentType = Convert.ToInt32(lblEquipmentType.Text);

            //Create a list and convert the string array to the list. Why the system cannot take a simple string array is beyond me!!!
            List<string> listModel = new List<string> ();
            clsTabletDB.ITPInventory ITPInventory = new clsTabletDB.ITPInventory ();
            string[] sModels;
            switch(iEquipmentType)
            {
                case 3:
                    sModels = ITPInventory.GetRackModels (sSupplier);
                    m_sRackModels = sModels;
                    Array.ForEach (m_sRackModels, value => listModel.Add (value.ToString ()));
                    break;
                case 4:
                    sModels = ITPInventory.GetSubRackModels (sSupplier);
                    m_sSubRackModels = sModels;
                    Array.ForEach (m_sSubRackModels, value => listModel.Add (value.ToString ()));
                    break;
                case 5:
                    sModels = ITPInventory.GetPositionModels (sSupplier);
                    m_sPositionModels = sModels;
                    Array.ForEach (m_sPositionModels, value => listModel.Add (value.ToString ()));
                    break;
                case 7:
                    sModels = ITPInventory.GetSolarStringModels (sSupplier);
                    m_sSolarStringModels = sModels;
                    Array.ForEach (m_sSolarStringModels, value => listModel.Add (value.ToString ()));
                    break;
                default:
                    sModels = ITPInventory.GetPositionModels (sSupplier);
                    m_sPositionModels = sModels;
                    Array.ForEach (m_sPositionModels, value => listModel.Add (value.ToString ()));
                    break;
            }

            TableViewSource tabdata = new TableViewSource (listModel, true);
            tabdata.SetFont("Verdana",10f);
            UITableView cmbModel = new UITableView ();
            
            //If the bottom of the frame would be outside the main content frame make it go upwards instead of downwards
            UILabel hfContentHeight = (UILabel)View.ViewWithTag (3);
            int iContentHeight = Convert.ToInt32 (hfContentHeight.Text);
            if (iTop + 190f > (float)iContentHeight) 
            {
                if (iLeft + 290f > 1000f) 
                {
                    cmbModel.Frame = new RectangleF(iLeft - 300f, iTop - 190f, 290f, 200f);
                } 
                else 
                {
                    cmbModel.Frame = new RectangleF(iLeft, iTop - 190f, 290f, 200f);
                }
            } 
            else 
            {
                if (iLeft + 290f > 1000f) 
                {
                    cmbModel.Frame = new RectangleF(iLeft - 300f, iTop, 290f, 200f);
                } 
                else 
                {
                    cmbModel.Frame = new RectangleF(iLeft, iTop, 290f, 200f);
                }
            }

            tabdata.SetParent(cmbModel);
            tabdata.SetUpdateFieldType("UILabel");
            UILabel lblVwUpdate = (UILabel)View.ViewWithTag (iEquipmentModelTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetLabelViewToUpdate(lblVwUpdate);
            UIView vwUnsaved = (UIView)View.ViewWithTag (60);
            tabdata.SetUnsavedChangesView(vwUnsaved);
            tabdata.SetShowUnsavedOnChange(true);
            UILabel hfRowStatus = (UILabel)View.ViewWithTag (iEquipmentRowStatusTagId * (iPwrIdRow) + (iStringRow));
            UILabel lblSPN = (UILabel)View.ViewWithTag (iEquipmentSPNHiddenTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetModelPostUpdate(6, hfRowStatus, lblSPN, sSupplier, btnMakeSearch, btnModelSearch); //Here the 6 refers to the post update index and NOT batteries as the equipment type
            
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
            switch(iEquipmentType)
            {
                case 3:
                    thistable.SetTableSelectedText(cmbModel, sSelectedValue, m_sRackModels, true);
                    break;
                case 4:
                    thistable.SetTableSelectedText(cmbModel, sSelectedValue, m_sSubRackModels, true);
                    break;
                case 5:
                    thistable.SetTableSelectedText(cmbModel, sSelectedValue, m_sPositionModels, true);
                    break;
                case 7:
                    thistable.SetTableSelectedText(cmbModel, sSelectedValue, m_sSolarStringModels, true);
                    break;
                default:
                    thistable.SetTableSelectedText(cmbModel, sSelectedValue, m_sPositionModels, true);
                    break;
            }

            //Get the main scroll view
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            scrollVw.AddSubview(cmbModel);
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
                    iSearchTagTypeId = iEquipmentFloorSearchTagId;
                    sType = "floor";
                    break;
                case 2: //Suite
                    iSearchTagTypeId = iEquipmentSuiteSearchTagId;
                    sType = "suite";
                    break;
                case 3: //Rack
                    iSearchTagTypeId = iEquipmentRackSearchTagId;
                    sType = "rack";
                    break;
                case 4: //SubRack
                    iSearchTagTypeId = iEquipmentSubRackSearchTagId;
                    sType = "subrack";
                    break;
                case 5: //Position
                    iSearchTagTypeId = iEquipmentPositionSearchTagId;
                    sType = "position";
                    break;
                case 7: //String
                    iSearchTagTypeId = iEquipmentStringSearchTagId;
                    sType = "solar string";
                    break;

            }
            int iPwrIdRow = iBtnTagId / iSearchTagTypeId;
            int iStringRow = iBtnTagId - (iPwrIdRow * iSearchTagTypeId);
            int iSectionCounterTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow + iStringRow;
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
                    iTypeTagId = iEquipmentFloorTagId;
                    ihfTypeTagId = iEquipmentFloorHiddenTagId;
                    break;
                    
                case 2:
                    iTypeTagId = iEquipmentSuiteTagId;
                    ihfTypeTagId = iEquipmentSuiteHiddenTagId;
                    break;
                    
                case 3:
                    iTypeTagId = iEquipmentRackTagId;
                    ihfTypeTagId = iEquipmentRackHiddenTagId;
                    break;
                    
                case 4:
                    iTypeTagId = iEquipmentSubRackTagId;
                    ihfTypeTagId = iEquipmentSubRackHiddenTagId;
                    break;

                case 5:
                    iTypeTagId = iEquipmentPositionTagId;
                    ihfTypeTagId = iEquipmentPositionHiddenTagId;
                    break;

                case 7:
                    iTypeTagId = iEquipmentStringTagId;
                    ihfTypeTagId = iEquipmentStringHiddenTagId;
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
            UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
            tabdata.SetItemPostUpdate(iSearchTypeId + 1, lblhfVwUpdate, hfRowStatus);
            
            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
            UIButton btnSectionSave = (UIButton)View.ViewWithTag ((iSectionCounterId + 1) * iSaveSectionBtnTagId);
            tabdata.SetSectionSaveButton(btnSectionSave);
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
            int iPwrIdRow =  iTagId/ iEquipmentStringTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentStringTagId);
            int iHiddenBankId =  iEquipmentStringHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenBankNo = (UILabel)View.ViewWithTag (iHiddenBankId);
            
            if (!bBankCheck && sBankNo != "") 
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
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
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
            int iPwrIdRow =  iTagId/ iEquipmentDOMTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentDOMTagId);
            int iHiddenDOMId =  iEquipmentDOMHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenDOM = (UILabel)View.ViewWithTag (iHiddenDOMId);

            if(sDOM == "")
            {
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
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
                string sDOMReturn = dt.Get_Date_String(dtDOM, "dd/mm/yy");
                txtDOM.Text = sDOMReturn;
                if(hfHiddenDOM.Text != sDOMReturn)
                {
                    hfHiddenDOM.Text = sDOMReturn;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                    SetAnyValueChanged(sender, null);
                }
                return true;
            }
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
            int iPwrIdRow =  iTagId/ iEquipmentFloorTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentFloorTagId);
            int iHiddenBankId =  iEquipmentFloorHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenFloor = (UILabel)View.ViewWithTag (iHiddenBankId);
            string sOldFloor = hfHiddenFloor.Text;
            
            if (!bFloorCheck && sFloor != "") 
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
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                    SetAnyValueChanged(sender, null);

                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iEquipmentRowPwrIdTagId * iPwrIdRow + iStringRow;
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
                    int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
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
                            int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;
                            if(sExistingFloor == sOldFloor)
                            {
                                txtFloor.Text = sFloor;

                                int iHiddenFloorId =  iEquipmentFloorHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenFloor = (UILabel)View.ViewWithTag (iHiddenFloorId);
                                hfHiddenFloor.Text = sFloor;

                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdCounter + (i+1));
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
            int iPwrIdRow =  iTagId/ iEquipmentSuiteTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentSuiteTagId);
            int iHiddenBankId =  iEquipmentSuiteHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenSuite = (UILabel)View.ViewWithTag (iHiddenBankId);
            string sOldSuite = hfHiddenSuite.Text;

            if (!bSuiteCheck && sSuite != "") 
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
                if(sOldSuite != sSuite) //Only do something if there has been a chnage
                {
                    hfHiddenSuite.Text = txtSuite.Text;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";

                    SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                    SetAnyValueChanged(sender, null);

                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iEquipmentRowPwrIdTagId * iPwrIdRow + iStringRow;
                        UILabel hfPwrId = (UILabel)View.ViewWithTag (iPwrIdTagId);
                        string sPwrId = hfPwrId.Text;
                        
                        int iFloorId =  iEquipmentFloorTagId * iPwrIdRow + iStringRow;
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
                    int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                    UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                    string sExistingFloor = txtFloor.Text;
                    
                    int iSuiteId =  iEquipmentSuiteTagId * iPwrIdCounter + (i+1);
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
                            int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;
                            
                            int iSuiteId =  iEquipmentSuiteTagId * iPwrIdCounter + (i+1);
                            UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                            string sExistingSuite = txtSuite.Text;
                            
                            if(sExistingFloor == sFloor && sExistingSuite == sOldSuite)
                            {
                                txtSuite.Text = sSuite;
                                
                                int iHiddenSuiteId =  iEquipmentSuiteHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenSuite = (UILabel)View.ViewWithTag (iHiddenSuiteId);
                                hfHiddenSuite.Text = sSuite;
                                
                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdCounter + (i+1));
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
            int iPwrIdRow =  iTagId/ iEquipmentRackTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentRackTagId);
            int iHiddenBankId =  iEquipmentRackHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenRack = (UILabel)View.ViewWithTag (iHiddenBankId);
            string sOldRack = hfHiddenRack.Text;

            if (!bRackCheck && sRack != "") 
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
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                    SetAnyValueChanged(sender, null);
                    
                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iEquipmentRowPwrIdTagId * iPwrIdRow + iStringRow;
                        UILabel hfPwrId = (UILabel)View.ViewWithTag (iPwrIdTagId);
                        string sPwrId = hfPwrId.Text;
                        
                        int iFloorId =  iEquipmentFloorTagId * iPwrIdRow + iStringRow;
                        UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                        string sFloor = txtFloor.Text;
                        
                        int iSuiteId =  iEquipmentSuiteTagId * iPwrIdRow + iStringRow;
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
                    int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                    UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                    string sExistingFloor = txtFloor.Text;
                    
                    int iSuiteId =  iEquipmentSuiteTagId * iPwrIdCounter + (i+1);
                    UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                    string sExistingSuite = txtSuite.Text;
                    
                    int iRackId =  iEquipmentRackTagId * iPwrIdCounter + (i+1);
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
                            int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;
                            
                            int iSuiteId =  iEquipmentSuiteTagId * iPwrIdCounter + (i+1);
                            UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                            string sExistingSuite = txtSuite.Text;
                            
                            int iRackId =  iEquipmentRackTagId * iPwrIdCounter + (i+1);
                            UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                            string sExistingRack = txtRack.Text;
                            
                            if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sOldRack)
                            {
                                txtRack.Text = sRack;
                                
                                int iHiddenRackId =  iEquipmentRackHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenRack = (UILabel)View.ViewWithTag (iHiddenRackId);
                                hfHiddenRack.Text = sRack;
                                
                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdCounter + (i+1));
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
            int iPwrIdRow =  iTagId/ iEquipmentSubRackTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentSubRackTagId);
            int iHiddenBankId =  iEquipmentSubRackHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenSubRack = (UILabel)View.ViewWithTag (iHiddenBankId);
            string sOldSubRack = hfHiddenSubRack.Text;


            if (!bSubRackCheck && sSubRack != "") 
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
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                    SetAnyValueChanged(sender, null);
                    
                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iEquipmentRowPwrIdTagId * iPwrIdRow + iStringRow;
                        UILabel hfPwrId = (UILabel)View.ViewWithTag (iPwrIdTagId);
                        string sPwrId = hfPwrId.Text;
                        
                        int iFloorId =  iEquipmentFloorTagId * iPwrIdRow + iStringRow;
                        UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                        string sFloor = txtFloor.Text;
                        
                        int iSuiteId =  iEquipmentSuiteTagId * iPwrIdRow + iStringRow;
                        UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                        string sSuite = txtSuite.Text;
                        
                        int iRackId =  iEquipmentRackTagId * iPwrIdRow + iStringRow;
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
                    int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                    UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                    string sExistingFloor = txtFloor.Text;
                    
                    int iSuiteId =  iEquipmentSuiteTagId * iPwrIdCounter + (i+1);
                    UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                    string sExistingSuite = txtSuite.Text;
                    
                    int iRackId =  iEquipmentRackTagId * iPwrIdCounter + (i+1);
                    UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                    string sExistingRack = txtRack.Text;

                    int iEquipTypeId = iEquipmentTypeTagId * iPwrIdCounter + (i+1);
                    UILabel lblEquipType = (UILabel)View.ViewWithTag (iEquipTypeId);
                    int iEquipType = Convert.ToInt32(lblEquipType.Text);

                    string sExistingSubRack = "";
                    if(iEquipType > 3)
                    {
                        int iSubRackId =  iEquipmentSubRackTagId * iPwrIdCounter + (i+1);
                        UITextField txtSubRack = (UITextField)View.ViewWithTag (iSubRackId);
                        sExistingSubRack = txtSubRack.Text;
                    }
                    
                    if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sRack && 
                       sExistingSubRack == sSubRack && iEquipType > 3)
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
                            int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;
                            
                            int iSuiteId =  iEquipmentSuiteTagId * iPwrIdCounter + (i+1);
                            UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                            string sExistingSuite = txtSuite.Text;
                            
                            int iRackId =  iEquipmentRackTagId * iPwrIdCounter + (i+1);
                            UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                            string sExistingRack = txtRack.Text;
                            
                            int iSubRackId =  iEquipmentSubRackTagId * iPwrIdCounter + (i+1);
                            UITextField txtSubRack = (UITextField)View.ViewWithTag (iSubRackId);
                            string sExistingSubRack = txtSubRack.Text;

                            if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sRack && 
                               sExistingSubRack == sOldSubRack)
                            {
                                txtSubRack.Text = sSubRack;
                                
                                int iHiddenSubRackId =  iEquipmentSubRackHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenSubRack = (UILabel)View.ViewWithTag (iHiddenSubRackId);
                                hfHiddenSubRack.Text = sSubRack;
                                
                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdCounter + (i+1));
                                hfRowStatus.Text = "1";
                            }
                        }
                    }
                    break;
                case 1: //No
                    break;
            }
        }
        
        public bool ValidatePosition (object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }
            
            UITextField txtPosition = (UITextField)sender;
            string sPosition = txtPosition.Text;
            sPosition = sPosition.ToUpper();
            txtPosition.Text = sPosition;
            clsTabletDB.ITPValidHierarchy ITPValidHierarchy = new clsTabletDB.ITPValidHierarchy();
            bool bPositionCheck = ITPValidHierarchy.IsValidItem(sPosition, 5);
            int iTagId = txtPosition.Tag;
            int iPwrIdRow =  iTagId/ iEquipmentPositionTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentPositionTagId);
            int iHiddenPositionId =  iEquipmentPositionHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenPosition = (UILabel)View.ViewWithTag (iHiddenPositionId);
            string sOldPosition = hfHiddenPosition.Text;

            if (!bPositionCheck && sPosition != "") 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid position or search from the list by using the button underneath");
                txtPosition.Text = hfHiddenPosition.Text;
                txtPosition.ResignFirstResponder();
                txtPosition.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                if(hfHiddenPosition.Text != txtPosition.Text)
                {
                    hfHiddenPosition.Text = txtPosition.Text;
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                    hfRowStatus.Text = "1";
                    SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                    SetAnyValueChanged(sender, null);

                    //Ask the question
                    if(iFromBackButton == 0)
                    {
                        int iSectionTagId = iEquipmentRowSectionCounterTagId * iPwrIdRow + iStringRow;
                        UILabel hfSectionId = (UILabel)View.ViewWithTag (iSectionTagId);
                        int iSectionId = Convert.ToInt32(hfSectionId.Text);
                        
                        int iPwrIdTagId = iEquipmentRowPwrIdTagId * iPwrIdRow + iStringRow;
                        UILabel hfPwrId = (UILabel)View.ViewWithTag (iPwrIdTagId);
                        string sPwrId = hfPwrId.Text;
                        
                        int iFloorId =  iEquipmentFloorTagId * iPwrIdRow + iStringRow;
                        UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                        string sFloor = txtFloor.Text;
                        
                        int iSuiteId =  iEquipmentSuiteTagId * iPwrIdRow + iStringRow;
                        UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                        string sSuite = txtSuite.Text;
                        
                        int iRackId =  iEquipmentRackTagId * iPwrIdRow + iStringRow;
                        UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                        string sRack = txtRack.Text;
                        
                        int iSubRackId =  iEquipmentSubRackTagId * iPwrIdRow + iStringRow;
                        UITextField txtSubRack = (UITextField)View.ViewWithTag (iSubRackId);
                        string sSubRack = txtSubRack.Text;
                        
                        if(sOldPosition != sPosition)
                        {
                            if(CheckSamePositionExists(sFloor, sSuite, sRack, sSubRack, sOldPosition,
                                                       iSectionId, iPwrIdRow, sPwrId, iStringRow))
                            {
                                iUtils.AlertBox alert2 = new iUtils.AlertBox();
                                alert2.CreateAlertYesNoDialog();
                                alert2.SetAlertMessage("Do you wish to change all other items on PwrId " + sPwrId + " on the floor " + 
                                                       sFloor + ", suite " + sSuite + ", rack " + sRack + ", subrack " + sSubRack + 
                                                       " and position " + sOldPosition + " to position " +  sPosition + " ?");
                                
                                alert2.ShowAlertBox(); 
                                
                                UIAlertView alert3 = alert2.GetAlertDialog();
                                alert3.Clicked += (sender2, e2)  => {
                                    CheckPositionChangesQuestion(sender2, e2, e2.ButtonIndex, iStringRow, sPwrId, sFloor, 
                                                                sSuite, sRack, sSubRack, sPosition, sOldPosition, iSectionId, iPwrIdRow);
                                }; 
                            }
                        }
                    }
                }
                return true;
            }
        }
        
        public bool CheckSamePositionExists(string sFloor, string sSuite, string sRack, string sSubRack, string sPosition, int iSectionIdCounter, 
                                           int iPwrIdCounter, string sPwrId, int iSourceRow)
        {
            int iRowsTagId = (ihfPwrIdStringRowsTagId + (iPwrIdCounter)) * (iSectionIdCounter+1);
            UILabel hfRowsCounter = (UILabel)View.ViewWithTag (iRowsTagId);
            int iRows = Convert.ToInt32(hfRowsCounter.Text);
            
            for(int i=0;i<iRows;i++)
            {
                if((i+1) != iSourceRow)
                {
                    int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                    UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                    string sExistingFloor = txtFloor.Text;
                    
                    int iSuiteId =  iEquipmentSuiteTagId * iPwrIdCounter + (i+1);
                    UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                    string sExistingSuite = txtSuite.Text;
                    
                    int iRackId =  iEquipmentRackTagId * iPwrIdCounter + (i+1);
                    UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                    string sExistingRack = txtRack.Text;
                    
                    int iEquipTypeId = iEquipmentTypeTagId * iPwrIdCounter + (i+1);
                    UILabel lblEquipType = (UILabel)View.ViewWithTag (iEquipTypeId);
                    int iEquipType = Convert.ToInt32(lblEquipType.Text);
                    
                    string sExistingSubRack = "";
                    if(iEquipType > 3)
                    {
                        int iSubRackId =  iEquipmentSubRackTagId * iPwrIdCounter + (i+1);
                        UITextField txtSubRack = (UITextField)View.ViewWithTag (iSubRackId);
                        sExistingSubRack = txtSubRack.Text;
                    }
                    

                    string sExistingPosition = "";
                    if(iEquipType > 4)
                    {
                        int iPositionId =  iEquipmentPositionTagId * iPwrIdCounter + (i+1);
                        UITextField txtPosition = (UITextField)View.ViewWithTag (iPositionId);
                        sExistingPosition = txtPosition.Text;
                    }

                    if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sRack && 
                       sExistingSubRack == sSubRack &&  sExistingPosition == sPosition && iEquipType > 4)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public void CheckPositionChangesQuestion (object sender, EventArgs e, int iBtnIndex, int iSourceRow, 
                                                 string sPwrId, string sFloor, string sSuite, string sRack, 
                                                 string sSubRack, string sPosition, string sOldPosition, 
                                                 int iSectionIdCounter, int iPwrIdCounter)
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
                            int iFloorId =  iEquipmentFloorTagId * iPwrIdCounter + (i+1);
                            UITextField txtFloor = (UITextField)View.ViewWithTag (iFloorId);
                            string sExistingFloor = txtFloor.Text;
                            
                            int iSuiteId =  iEquipmentSuiteTagId * iPwrIdCounter + (i+1);
                            UITextField txtSuite = (UITextField)View.ViewWithTag (iSuiteId);
                            string sExistingSuite = txtSuite.Text;
                            
                            int iRackId =  iEquipmentRackTagId * iPwrIdCounter + (i+1);
                            UITextField txtRack = (UITextField)View.ViewWithTag (iRackId);
                            string sExistingRack = txtRack.Text;
                            
                            int iSubRackId =  iEquipmentSubRackTagId * iPwrIdCounter + (i+1);
                            UITextField txtSubRack = (UITextField)View.ViewWithTag (iSubRackId);
                            string sExistingSubRack = txtSubRack.Text;
                            
                            int iPositionId =  iEquipmentPositionTagId * iPwrIdCounter + (i+1);
                            UITextField txtPosition = (UITextField)View.ViewWithTag (iPositionId);
                            string sExistingPosition = txtPosition.Text;
                            
                            if(sExistingFloor == sFloor && sExistingSuite == sSuite && sExistingRack == sRack && 
                               sExistingSubRack == sSubRack &&  sExistingPosition == sOldPosition)
                            {
                                txtPosition.Text = sPosition;
                                
                                int iHiddenPositionId =  iEquipmentPositionHiddenTagId * iPwrIdCounter + (i+1);
                                UILabel hfHiddenPosition = (UILabel)View.ViewWithTag (iHiddenPositionId);
                                hfHiddenPosition.Text = sPosition;
                                
                                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdCounter + (i+1));
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
            int iPwrIdRow =  iTagId/ iEquipmentEquipTypeTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentEquipTypeTagId);
            
            int iAnswerIndex = radGroup.SelectedSegment;
            string sAnswer = "";

            if (iAnswerIndex >= 0)
            {
                sAnswer = radGroup.TitleAt(iAnswerIndex);
            }
            else
            {
                sAnswer = "";
            }
            
            UIView txtMaximoAssetIdView = (UIView)View.ViewWithTag(iEquipmentMaximoAssetTextViewTagId * (iPwrIdRow) + (iStringRow));
            UITextField txtMaximoAssetId = (UITextField)View.ViewWithTag(iEquipmentMaximoAssetTagId * (iPwrIdRow) + (iStringRow));
            UIView lblMaximoAssetIdView = (UIView)View.ViewWithTag(iEquipmentMaximoLblViewAssetTagId  * (iPwrIdRow) + (iStringRow));
            UILabel lblMaximoAssetId = (UILabel)View.ViewWithTag(iEquipmentMaximoLblAssetTagId * (iPwrIdRow) + (iStringRow));

            switch (sAnswer)
            {
                case "New": //Turn off the text box and turn on the label view
                    txtMaximoAssetIdView.Hidden = true;
                    txtMaximoAssetId.Hidden = true;
                    lblMaximoAssetIdView.Hidden = false;
                    lblMaximoAssetId.Hidden = false;
                    break;
                case "Used":
                    txtMaximoAssetIdView.Hidden = false;
                    txtMaximoAssetId.Hidden = false;
                    lblMaximoAssetIdView.Hidden = true;
                    lblMaximoAssetId.Hidden = true;
                    break;
                default:
                    txtMaximoAssetIdView.Hidden = true;
                    txtMaximoAssetId.Hidden = true;
                    lblMaximoAssetIdView.Hidden = false;
                    lblMaximoAssetId.Hidden = false;
                    break;
            }

            UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
            hfRowStatus.Text = "1";
            SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
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
            int iPwrIdRow =  iTagId/ iEquipmentSerialNoTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentSerialNoTagId);
            int iHiddenSerialNoId =  iEquipmentSerialNoHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenSerialNo = (UILabel)View.ViewWithTag (iHiddenSerialNoId);

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
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                SetAnyValueChanged(sender, null);
            }
            return true;
        }

        public bool ValidateMaximoAssetId(object sender, int iFromBackButton)
        {
            if(gbSuppressSecondCheck)
            {
                return true;
            }
            
            if(iFromBackButton == 1)
            {
                gbSuppressSecondCheck = true;
            }
            
            UITextField txtMaximoAssetId = (UITextField)sender;
            string sMaximoAssetId = txtMaximoAssetId.Text;
            sMaximoAssetId = Regex.Replace(sMaximoAssetId, @"[^\d]+","");
            sMaximoAssetId = sMaximoAssetId.PadLeft(10,'0');
            int iTagId = txtMaximoAssetId.Tag;
            int iPwrIdRow =  iTagId/ iEquipmentMaximoAssetTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentMaximoAssetTagId);
            int iHiddenMaxAssetId =  iEquipmentMaximoAssetHiddenTagId * iPwrIdRow + iStringRow;
            UILabel hfHiddenMaxAssetId = (UILabel)View.ViewWithTag (iHiddenMaxAssetId);
            txtMaximoAssetId.Text = sMaximoAssetId;

            if (sMaximoAssetId.Length > 10)
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("The maximo asset id number cannot be more than 10 characters. It has been truncated. Please enter a valid 10 character or less asset number.");
                string sMaximoAssetReturn = sMaximoAssetId.Substring(0,10);
                txtMaximoAssetId.Text = sMaximoAssetReturn;
                txtMaximoAssetId.ResignFirstResponder();
                txtMaximoAssetId.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            }
            
            if(hfHiddenMaxAssetId.Text != sMaximoAssetId)
            {
                hfHiddenMaxAssetId.Text = sMaximoAssetId;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
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
        
        public void DeleteEquipmentRow(object sender, EventArgs e)
        {
            string sRtnMsg = "";
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            UIButton btnDelete = (UIButton)sender;
            int iTagId = btnDelete.Tag;
            int iPwrIdRow = iTagId / iEquipmentDeleteBtnTagId;
            int iStringRow = iTagId - (iPwrIdRow * iEquipmentDeleteBtnTagId);
            
            UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * (iPwrIdRow) + (iStringRow));
            int iRowStatus = Convert.ToInt32(hfRowStatus.Text);
            
            UILabel hfAutoId = (UILabel)View.ViewWithTag(iEquipmentRowAutoIdTagId * (iPwrIdRow) + (iStringRow));
            int iAutoId = Convert.ToInt32(hfAutoId.Text);
            
            UILabel hfMaximoAssetId = (UILabel)View.ViewWithTag(iEquipmentRowMaximoAssetIdTagId * (iPwrIdRow) + (iStringRow));
            string sMaximoId = hfMaximoAssetId.Text;
            if (sMaximoId == "" || sMaximoId == "0")
            {
                sMaximoId = "-1";
            }
            int iMaximoAssetId = Convert.ToInt32(sMaximoId);
            
            UILabel hfPwrId = (UILabel)View.ViewWithTag(iEquipmentRowPwrIdTagId * (iPwrIdRow) + (iStringRow));
            string sPwrId = hfPwrId.Text;
            
            UILabel hfEquipmentType = (UILabel)View.ViewWithTag(iEquipmentTypeTagId * (iPwrIdRow) + (iStringRow));
            int iEquipmentType = Convert.ToInt32(hfEquipmentType.Text);

            string sRack = "";
            string sSubRack = "";
            string sPosition = "";
            string sString = "";
            string sMessage = "";

            UITextField txtFloor = (UITextField)View.ViewWithTag(iEquipmentFloorTagId * (iPwrIdRow) + (iStringRow));
            string sFloor = txtFloor.Text;

            UITextField txtSuite = (UITextField)View.ViewWithTag(iEquipmentSuiteTagId * (iPwrIdRow) + (iStringRow));
            string sSuite = txtSuite.Text;

            switch(iEquipmentType)
            {
                case 3: //Rack
                    UITextField txtRack = (UITextField)View.ViewWithTag(iEquipmentRackTagId * (iPwrIdRow) + (iStringRow));
                    sRack = txtRack.Text;
                    sMessage = ", Floor " + sFloor + ", Suite " + sSuite + " and Rack " + sRack;
                    break;
                case 4: //SubRack
                    UITextField txtRack1 = (UITextField)View.ViewWithTag(iEquipmentRackTagId * (iPwrIdRow) + (iStringRow));
                    sRack = txtRack1.Text;
                    UITextField txtSubRack1 = (UITextField)View.ViewWithTag(iEquipmentSubRackTagId * (iPwrIdRow) + (iStringRow));
                    sSubRack = txtSubRack1.Text;
                    sMessage = ", Floor " + sFloor + ", Suite " + sSuite + ", Rack " + sRack + " and SubRack " + sSubRack;
                    break;
                case 5: //Position
                    UITextField txtRack2 = (UITextField)View.ViewWithTag(iEquipmentRackTagId * (iPwrIdRow) + (iStringRow));
                    sRack = txtRack2.Text;
                    UITextField txtSubRack2 = (UITextField)View.ViewWithTag(iEquipmentSubRackTagId * (iPwrIdRow) + (iStringRow));
                    sSubRack = txtSubRack2.Text;
                    UITextField txtPosition2 = (UITextField)View.ViewWithTag(iEquipmentPositionTagId * (iPwrIdRow) + (iStringRow));
                    sPosition = txtPosition2.Text;
                    sMessage = ", Floor " + sFloor + ", Suite " + sSuite + ", Rack " + sRack + " ,SubRack " + sSubRack + " and Position " + sPosition;
                    break;
                case 7: //String
                    UITextField txtRack3 = (UITextField)View.ViewWithTag(iEquipmentRackTagId * (iPwrIdRow) + (iStringRow));
                    sRack = txtRack3.Text;
                    UITextField txtSubRack3 = (UITextField)View.ViewWithTag(iEquipmentSubRackTagId * (iPwrIdRow) + (iStringRow));
                    sSubRack = txtSubRack3.Text;
                    UITextField txtPosition3 = (UITextField)View.ViewWithTag(iEquipmentPositionTagId * (iPwrIdRow) + (iStringRow));
                    sPosition = txtPosition3.Text;
                    UITextField txtString3 = (UITextField)View.ViewWithTag(iEquipmentStringTagId * (iPwrIdRow) + (iStringRow));
                    sString = txtString3.Text;
                    sMessage = ", Floor " + sFloor + ", Suite " + sSuite + ", Rack " + sRack + " ,SubRack " + sSubRack + " ,Position " + sPosition + " and Solar String " + sString;
                    break;
            }
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
                        alert.CreateErrorAlertDialog("Could not delete power conversion item on project " + m_sPassedId + ", Power Id " + sPwrId + sMessage);
                        return;
                    }
                }
                
                //Remove the line from the page (well hide it really so all the loops still work)
                UIView vwStringRow = (UIView)View.ViewWithTag(iEquipmentFullRowTagId * (iPwrIdRow) + (iStringRow));
                vwStringRow.Hidden = true;
                hfRowStatus.Text = "3"; //Means deleted, so no save required
                ReduceHeightAfter(m_iEquipmentRowHeight, iPwrIdRow, iStringRow, 2);
                
                UIView vwPwrInternalRowId = (UIView)View.ViewWithTag((iPwrIdSectionInnerTagId + (iPwrIdRow)) * (m_iEquipmentSectionCounter+1));
                RectangleF frame1 = vwPwrInternalRowId.Frame;
                frame1.Height -= m_iEquipmentRowHeight;
                vwPwrInternalRowId.Frame = frame1;

                //Now increase the view height for this new row (The whole section height is handled in the ReduceHeightAfter function)
                UILabel hfPwrIdSectionHeight = (UILabel)View.ViewWithTag((iPwrIdHeightTagId + iPwrIdRow ) * (m_iEquipmentSectionCounter + 1));
                int iPwrIdHeight = Convert.ToInt32(hfPwrIdSectionHeight.Text);
                hfPwrIdSectionHeight.Text = (iPwrIdHeight - m_iEquipmentRowHeight).ToString();

                //Set the unsaved tags on (do this even though the record is removed for consistency)
                SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                SetAnyValueChanged(sender, null);
            }
            return;
        }
        
        //The section type tells us whether we are doing a contract on a battery (1) or a power conversion equipment (2) PwrId
        //You can use this for expand as well. Just send across a negative iHeightToReduce
        public void ReduceHeightAfter(float iHeightToReduce, int iPwrIdRow, int iStringRow, int iSectionType)
        {
            UILabel hfThisPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + iPwrIdRow) * (m_iEquipmentSectionCounter + 1));
            int iTotalStrings = Convert.ToInt32(hfThisPwrIdStringRows.Text);
            int i;
            int jSectionType = -1;
            
            switch(iSectionType)
            {
                case 1:
                    jSectionType = -1;
                    break;
                case 2:
                    jSectionType = m_iEquipmentSectionCounter;
                    break;
                default:
                    jSectionType = m_iEquipmentSectionCounter;
                    break;
                    
            }
            
            for(i = iStringRow ; i< iTotalStrings ; i++)
            {
                //Move any string rows afterwards up by the height
                UIView vwStringRow = (UIView)View.ViewWithTag(iEquipmentFullRowTagId * (iPwrIdRow) + (i + 1));
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
        
        public void AddNewEquipment (object sender, EventArgs e)
        {
            UIButton btnAddNew = (UIButton)sender;
            int iTagId = btnAddNew.Tag;
            int iPwrIdRow = iTagId /(m_iEquipmentSectionCounter+1)  - iPwrIdNewBtnTagId; //This is 1 based

            iUtils.AlertBox alert2 = new iUtils.AlertBox();
            alert2.CreateAlertRackSubRackPositionStringDialog();
            alert2.SetAlertMessage("Are you adding a rack, subrack, position item or solar panel string?");
            alert2.ShowAlertBox(); 
            
            UIAlertView alert3 = alert2.GetAlertDialog();
            alert3.Clicked += (sender2, e2)  => {AddNewItem(sender2, e2, e2.ButtonIndex, iPwrIdRow);}; 

        }

        public void AddNewItem (object sender, EventArgs e, int iBtnIndex, int iPwrIdRow)
        {
            int iEquipmentType = 5;
            float iHeightToAdd = 0.0f;
            UILabel hfThisPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + iPwrIdRow) * (m_iEquipmentSectionCounter + 1));
            int iTotalStrings = Convert.ToInt32(hfThisPwrIdStringRows.Text);
            UILabel hfPwrId = (UILabel)View.ViewWithTag((iPwrIdRowLabelTagId + iPwrIdRow) * (m_iEquipmentSectionCounter+1));
            string sPwrId = hfPwrId.Text;

            switch (iBtnIndex) 
            {
                case 0: //Rack
                    iEquipmentType = 3;
                    break;
                case 1: //SubRack
                    iEquipmentType = 4;
                    break;
                case 2: //Postion
                    iEquipmentType = 5;
                    break;
                case 3: //String
                    iEquipmentType = 7;
                    break;
            }

            UIView EquipmentItemRow = BuildEquipmentItemRowDetails(m_iEquipmentSectionCounter, iPwrIdRow - 1, 
                                                                   iTotalStrings, sPwrId, -1,
                                                                   -1, "",
                                                                   "", "", "", "",
                                                                   "", "", "", "", "", 
                                                                   "N", "", iEquipmentType, -1, 
                                                                   false, false,ref iHeightToAdd);
            
            //Get the position of the last row in this internal pwrId battery block
            UIView vwPwrInternalRowId = (UIView)View.ViewWithTag((iPwrIdSectionInnerTagId + (iPwrIdRow)) * (m_iEquipmentSectionCounter+1));
            float iPwrIdRowVert = vwPwrInternalRowId.Frame.Height;
            EquipmentItemRow.Frame = new RectangleF(0f, iPwrIdRowVert, 1000f, iHeightToAdd);
            EquipmentItemRow.Tag = iEquipmentFullRowTagId * (iPwrIdRow) + (iTotalStrings + 1);
            vwPwrInternalRowId.AddSubview(EquipmentItemRow);
            RectangleF frame1 = vwPwrInternalRowId.Frame;
            frame1.Height += iHeightToAdd;
            vwPwrInternalRowId.Frame = frame1;

            //Now increase the view height for this new row (the whole section height is managed in the ReduceHeightAfter function)
            UILabel hfPwrIdSectionHeight = (UILabel)View.ViewWithTag((iPwrIdHeightTagId + iPwrIdRow ) * (m_iEquipmentSectionCounter + 1));
            int iPwrIdHeight = Convert.ToInt32(hfPwrIdSectionHeight.Text);
            hfPwrIdSectionHeight.Text = (iPwrIdHeight + iHeightToAdd).ToString();

            //Now increase the number of strings in the PwrId by 1
            iTotalStrings++;
            hfThisPwrIdStringRows.Text = iTotalStrings.ToString();
            ReduceHeightAfter(-iHeightToAdd, iPwrIdRow, iTotalStrings, 2);
            
            //Set the unsaved tags on
            SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
            SetAnyValueChanged(sender, null);
            
            //Take off the completed or committed flags and also on the questions screen
            UILabel lblCompleted = (UILabel)View.ViewWithTag (iSectionCompleteLabelTagId * (m_iEquipmentSectionCounter + 1));
            lblCompleted.Hidden = true;
            UILabel lblPwrIdComplete = (UILabel)View.ViewWithTag ((iPwrIdSectionCompleteLabelTagId + (iPwrIdRow)) * (m_iEquipmentSectionCounter+1));
            lblPwrIdComplete.Hidden = true;
            ProjectITPage QuestionsScreen = new ProjectITPage ();
            QuestionsScreen = GetProjectITPPage ();
            QuestionsScreen.SetPowerConversionCompleted(false);

            //And move to the position
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag(2);
            float iTotalPosn = iPwrIdRowVert + scrollVw.ContentOffset.Y;
            PointF posn = new PointF(0f, iTotalPosn);
            scrollVw.SetContentOffset(posn, true);
            
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
                    jSectionType = -1;
                    break;
                case 2:
                    jSectionType = m_iEquipmentSectionCounter;
                    break;
                default:
                    jSectionType = m_iEquipmentSectionCounter;
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
                    jSectionType = -1;
                    break;
                case 2:
                    jSectionType = m_iEquipmentSectionCounter;
                    break;
                default:
                    jSectionType = m_iEquipmentSectionCounter;
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
        
        public bool PowerConversionFullyComplete()
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10PowerConversionComplete(m_sPassedId);
        }

        public bool PowerConversionPwrIdComplete(string sPwrId)
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10PwrIdPowerConversionComplete(m_sPassedId, sPwrId);
        }

        public bool PowerConversionFullyCommitted()
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10PowerConversionFullyCommitted(m_sPassedId);
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
                    case 1: //Floor
                        if(!ValidateFloor(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 2: //Suite
                        if(!ValidateSuite(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 3: //Rack
                        if(!ValidateRack(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 4: //SubRack
                        if(!ValidateSubRack(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 5: //Position
                        if(!ValidatePosition(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 6: //Solar String
                        if(!ValidateBankNo(m_sender, 2, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 7: //DOM
                        if(!ValidateDOM(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 8: //Serial No
                        if(!ValidateSerialNo(m_sender, 1))
                        {
                            gbSuppressSecondCheck = false;
                            return false;
                        }
                        break;
                    case 9: //Maximo Asset Id
                        if(!ValidateMaximoAssetId(m_sender, 1))
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
            return SavePowerConvSection(iBtnId);
        }
        
        public bool SavePowerConvSection(int iBtnId)
        {
            int i;
            int j;
            int iAutoId;
            string sId = m_sPassedId;
            clsTabletDB.ITPDocumentSection DB = new clsTabletDB.ITPDocumentSection();
            string[] sItemValues = new string[24];
            string sString = "";
            string sSubRack = "";
            string sPosition = "";
            bool bTransferIdBad = false;

            //Get the number of PwrId's
            UILabel hfSectionPwrIds = (UILabel)View.ViewWithTag(iSectionRowsTagId * (m_iEquipmentSectionCounter + 1));
            int iTotalPwrIds = Convert.ToInt32(hfSectionPwrIds.Text);
            bool bResetSectionFlag = true;
            
            for (i=0; i<iTotalPwrIds; i++)
            {
                //For each battery string block in this PwrId save it if necessary
                UILabel hfThisPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + (i + 1)) * (m_iEquipmentSectionCounter + 1));
                int iTotalStrings = Convert.ToInt32(hfThisPwrIdStringRows.Text);
                for (j=0; j<iTotalStrings; j++)
                {
                    UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * (i + 1) + (j + 1));
                    int iRowStatus = Convert.ToInt32(hfRowStatus.Text);
                    
                    if (iRowStatus == 1 || iRowStatus == 2 || iRowStatus == 3)
                    {
                        UILabel hfAutoId = (UILabel)View.ViewWithTag(iEquipmentRowAutoIdTagId * (i + 1) + (j + 1));
                        string sAutoId = hfAutoId.Text;
                        if (sAutoId == "")
                        {
                            iAutoId = -1;
                        }
                        else
                        {
                            iAutoId = Convert.ToInt32(sAutoId);
                        }

                        //Get the type so that we only look at the right level of hierarchy
                        UILabel hfEquipmentType = (UILabel)View.ViewWithTag(iEquipmentTypeTagId * (i + 1) + (j + 1));
                        int iEquipType = Convert.ToInt32(hfEquipmentType.Text);
                                                
                        UILabel lblPwrId = (UILabel)View.ViewWithTag(iEquipmentRowPwrIdTagId * (i + 1) + (j + 1));
                        string sPwrId = lblPwrId.Text;

                        UITextField txtFloor = (UITextField)View.ViewWithTag(iEquipmentFloorTagId * (i + 1) + (j + 1));
                        string sFloor = txtFloor.Text;
                        UITextField txtSuite = (UITextField)View.ViewWithTag(iEquipmentSuiteTagId * (i + 1) + (j + 1));
                        string sSuite = txtSuite.Text;
                        UITextField txtRack = (UITextField)View.ViewWithTag(iEquipmentRackTagId * (i + 1) + (j + 1));
                        string sRack = txtRack.Text;
                        if(iEquipType >= 4)
                        {
                            UITextField txtSubRack = (UITextField)View.ViewWithTag(iEquipmentSubRackTagId * (i + 1) + (j + 1));
                            sSubRack = txtSubRack.Text;
                        }

                        if(iEquipType >= 5)
                        {
                            UITextField txtPosition = (UITextField)View.ViewWithTag(iEquipmentPositionTagId * (i + 1) + (j + 1));
                            sPosition = txtPosition.Text;
                        }

                        if(iEquipType >= 6)
                        {
                            UITextField txtString = (UITextField)View.ViewWithTag(iEquipmentStringTagId * (i + 1) + (j + 1));
                            sString = txtString.Text;
                        }

                        UILabel lblMake = (UILabel)View.ViewWithTag(iEquipmentMakeTagId * (i + 1) + (j + 1));
                        string sMake = lblMake.Text;
                        UILabel lblModel = (UILabel)View.ViewWithTag(iEquipmentModelTagId * (i + 1) + (j + 1));
                        string sModel = lblModel.Text;
                        UILabel lblSPN = (UILabel)View.ViewWithTag(iEquipmentSPNHiddenTagId * (i + 1) + (j + 1));
                        string sSPN = lblSPN.Text;
                        UITextField txtSerialNo = (UITextField)View.ViewWithTag(iEquipmentSerialNoTagId * (i + 1) + (j + 1));
                        string sSerialNo = txtSerialNo.Text;
                        UITextField txtDOM = (UITextField)View.ViewWithTag(iEquipmentDOMTagId * (i + 1) + (j + 1));
                        string sDOM = txtDOM.Text;
                        if (txtDOM.Text == "" || txtDOM.Text == "0")
                        {
                            sDOM = "01/01/1900";
                        }
                        
                        string sFuseOrCB = "";
                        string sRating = "0";

                        string sLinkTest = "0";
                        string s20MinTest = "0";

                        UISegmentedControl radGrp = (UISegmentedControl)View.ViewWithTag(iEquipmentEquipTypeTagId * (i + 1) + (j + 1));
                        int iAnswerIndex = radGrp.SelectedSegment;  
                        string sAnswer = "";
                        string sEquipType = "";
                        
                        UILabel hfMaximoAssetId = (UILabel)View.ViewWithTag(iEquipmentMaximoAssetHiddenTagId * (i + 1) + (j + 1));
                        string sMaximoAssetId = hfMaximoAssetId.Text;
                        string sTransferAssetId = "";
                        string sPSAAssetId = "";

                        UILabel hfDuplicate = (UILabel)View.ViewWithTag(iDuplicateTagId * (i + 1) + (j + 1));
                        string sDuplicate = hfDuplicate.Text;


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

                        bTransferIdBad = false;

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
                                if(sTransferAssetId == "-1")
                                {
                                    bTransferIdBad= true;
                                }
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
                        sItemValues [2] = sString; //This comes first because it was set up for battery strings initially
                        sItemValues [3] = sFloor;
                        sItemValues [4] = sSuite;
                        sItemValues [5] = sRack;
                        sItemValues [6] = sSubRack;
                        sItemValues [7] = sPosition; //There is no position for a battery string but of course there could be for a power conversion item
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
                        sItemValues [22] = iEquipType.ToString();
                        sItemValues [23] = iRowStatus.ToString();
                        
                        if (sMake == "" || sModel == "" || sEquipType == "" || iEquipType == -1 || bTransferIdBad)
                        {
                            bResetSectionFlag = false;
                            iUtils.AlertBox alert = new iUtils.AlertBox();
                            alert.CreateAlertDialog();
                            if(bTransferIdBad)
                            {
                                alert.SetAlertMessage("An item in PwrId " + sPwrId + " is not fully filled out. You must provide an Asset Id for used equipment typically " + 
                                                      "found from Structure Builder. If you do not know the asset id, please provide a number between 0 and 10 " +
                                                      "(it will be padded with zeros to make a 10 character id) and the " + 
                                                      "relevant asset id will be determined at final RFU submission stage. " +
                                                      "The system cannot save this item.");
                            }
                            else
                            {
                                alert.SetAlertMessage("An item in PwrId " + sPwrId + " is not fully filled out. The system cannot save this item.");
                            }
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
                UILabel hfSectionStatus = (UILabel)View.ViewWithTag(iSectionStatusTagId * (m_iEquipmentSectionCounter + 1));
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

        //Send through just the section counter NOT the section Id. So 1 NOT 1000000 etc
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
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (m_iEquipmentSectionCounter+1));
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
            UIButton btnSave = (UIButton)View.ViewWithTag (iSaveSectionBtnTagId * (m_iEquipmentSectionCounter+1));
            btnSave.Hidden = true;
            m_iValidateType = -1;
        }
        
        public void ShowCompletedLabels()
        {
            int i;
            UILabel hfSectionPwrIds = (UILabel)View.ViewWithTag(iSectionRowsTagId * (m_iEquipmentSectionCounter+ 1));
            int iTotalPwrIds = Convert.ToInt32(hfSectionPwrIds.Text);
            UILabel lblCompleted = (UILabel)View.ViewWithTag (iSectionCompleteLabelTagId * (m_iEquipmentSectionCounter + 1));
            ProjectITPage QuestionsScreen = new ProjectITPage ();
            QuestionsScreen = GetProjectITPPage ();
            if(PowerConversionFullyComplete())
            {
                lblCompleted.Hidden = false;
                
                for(i = 0 ; i< iTotalPwrIds ; i++)
                {
                    UILabel lblPwrIdComplete = (UILabel)View.ViewWithTag ((iPwrIdSectionCompleteLabelTagId + (i+1)) * (m_iEquipmentSectionCounter+1));
                    lblPwrIdComplete.Hidden = false;
                }
                
                //Now also show it on the calling ITP screen
                QuestionsScreen.SetPowerConversionCompleted(true);
            }
            else
            {
                lblCompleted.Hidden = true;
                for(i = 0 ; i< iTotalPwrIds ; i++)
                {
                    UILabel lblPwrId = (UILabel)View.ViewWithTag ((iPwrIdRowLabelTagId + (i+1)) * (m_iEquipmentSectionCounter+1));
                    string sPwrId = lblPwrId.Text;
                    UILabel lblPwrIdComplete = (UILabel)View.ViewWithTag ((iPwrIdSectionCompleteLabelTagId + (i+1)) * (m_iEquipmentSectionCounter+1));
                    if(PowerConversionPwrIdComplete(sPwrId))
                    {
                        lblPwrIdComplete.Hidden = false;
                    }
                    else
                    {
                        lblPwrIdComplete.Hidden = true;
                    }
                }

                //Now also show it on the calling ITP screen
                QuestionsScreen.SetPowerConversionCompleted(false);

            }
        }

        public void CheckUnsaved ()
        {
            //First of all validate anything required
            switch(m_iValidateType)
            {
                case 1: //Floor
                    if(!ValidateFloor(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 2: //Suite
                    if(!ValidateSuite(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 3: //Rack
                    if(!ValidateRack(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 4: //SubRack
                    if(!ValidateSubRack(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 5: //Position
                    if(!ValidatePosition(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 6: //Solar String
                    if(!ValidateBankNo(m_sender, 2, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 7: //DOM
                    if(!ValidateDOM(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 8: //Serial No
                    if(!ValidateSerialNo(m_sender, 1))
                    {
                        gbSuppressSecondCheck = false;
                        return;
                    }
                    break;
                case 9: //Maximo Asset Id
                    if(!ValidateMaximoAssetId(m_sender, 1))
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
            int iSectionCounterId = m_iEquipmentSectionCounter;
            bool bMaximoAssetIdOn = false;
            
            switch (iTextFieldIndex)
            {
                case 1:
                    iTextTagId = iEquipmentFloorTagId;
                    break;
                case 2:
                    iTextTagId = iEquipmentSuiteTagId;
                    break;
                case 3:
                    iTextTagId = iEquipmentRackTagId;
                    break;
                case 4:
                    iTextTagId = iEquipmentSubRackTagId;
                    break;
                case 5:
                    iTextTagId = iEquipmentPositionTagId;
                    break;
                case 6:
                    iTextTagId = iEquipmentStringTagId;
                    break;
                case 7:
                    iTextTagId = iEquipmentDOMTagId;
                    break;
                case 8:
                    iTextTagId = iEquipmentSerialNoTagId;
                    break;
                case 9:
                    iTextTagId = iEquipmentMaximoAssetTagId;
                    break;
            }
            
            int iPwrIdRow;
            int iStringRow;
            UILabel hfPwrIdStringRows;
            int iTotalStringRows;
            iPwrIdRow = iTagId / iTextTagId;
            iStringRow = iTagId - (iPwrIdRow * iTextTagId);
            iSectionCounterId = m_iEquipmentSectionCounter;
            hfPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + iPwrIdRow) * (iSectionCounterId + 1)); 
            iTotalStringRows = Convert.ToInt32(hfPwrIdStringRows.Text);

            //Get the equipment type because sometimes we need to skip some fields
            UILabel lblEquipmentType = (UILabel)View.ViewWithTag (iEquipmentTypeTagId * (iPwrIdRow) + (iStringRow));
            int iEquipmentType = Convert.ToInt32(lblEquipmentType.Text);

            //Get the equipment type because sometimes we need to skip some fields
            UITextField txtMaximoAssetId = (UITextField)View.ViewWithTag (iEquipmentMaximoAssetTagId * (iPwrIdRow) + (iStringRow));
            if(txtMaximoAssetId != null)
            {
                bMaximoAssetIdOn = true;
            }

            switch (iTextFieldIndex) 
            {
                case 1: //Coming from floor and going to suite
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    txtNext = (UITextField)View.ViewWithTag (iEquipmentSuiteTagId * (iPwrIdRow) + (iStringRow));
                    break;
                case 2: //Coming from suite and going to rack
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    txtNext = (UITextField)View.ViewWithTag (iEquipmentRackTagId * (iPwrIdRow) + (iStringRow));
                    break;
                case 3: //Coming from rack and going to subrack
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    switch(iEquipmentType)
                    {
                        case 3:
                            txtNext = (UITextField)View.ViewWithTag (iEquipmentDOMTagId * (iPwrIdRow) + (iStringRow));
                            break;
                        default:
                            txtNext = (UITextField)View.ViewWithTag (iEquipmentSubRackTagId * (iPwrIdRow) + (iStringRow));
                            break;
                    }

                    
                    break;
                case 4: //Coming from subrack and going to position
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    switch(iEquipmentType)
                    {
                        case 4:
                            txtNext = (UITextField)View.ViewWithTag (iEquipmentDOMTagId * (iPwrIdRow) + (iStringRow));
                            break;
                        default:
                            txtNext = (UITextField)View.ViewWithTag (iEquipmentPositionTagId * (iPwrIdRow) + (iStringRow));
                            break;
                    }
                    break;
                case 5: //Coming from position and going to string
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    switch(iEquipmentType)
                    {
                        case 5:
                            txtNext = (UITextField)View.ViewWithTag (iEquipmentDOMTagId * (iPwrIdRow) + (iStringRow));
                            break;
                        default:
                            txtNext = (UITextField)View.ViewWithTag (iEquipmentStringTagId * (iPwrIdRow) + (iStringRow));
                            break;
                    }
                    break;
                case 6: //This means you are coming from the string no and going to the DOM
                    if(m_bSuppressMove) //This is required on the validate because the end editing and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    txtNext = (UITextField)View.ViewWithTag (iEquipmentDOMTagId * (iPwrIdRow) + (iStringRow)); 
                    break;
                case 7: //Coming from DOM and going to serial number
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    txtNext = (UITextField)View.ViewWithTag (iEquipmentSerialNoTagId * (iPwrIdRow) + (iStringRow)); //Go to the next string, hence the + 1 here
                    break;
                case 8: //Coming from serial number and going to floor
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }

                    if(bMaximoAssetIdOn)
                    {
                        txtNext = (UITextField)View.ViewWithTag (iEquipmentMaximoAssetTagId * (iPwrIdRow) + (iStringRow)); //Go to the next item
                        break;
                    }
                    else
                    {
                        //Make sure we are not on the last string because there is no extra row to go to so go to the first one again
                        if((iStringRow + 1) > iTotalStringRows)
                        {
                            txtNext = (UITextField)View.ViewWithTag (iEquipmentFloorTagId * (iPwrIdRow) + 1); //Cycle back to the first row
                        }
                        else
                        {
                            txtNext = (UITextField)View.ViewWithTag (iEquipmentFloorTagId * (iPwrIdRow) + (iStringRow + 1)); //Go to the next string, hence the + 1 here
                        }
                    }
                    break;
                case 9: //Coming from maximo asset id and going to floor
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    //Make sure we are not on the last string because there is no extra row to go to so go to the first one again
                    if((iStringRow + 1) > iTotalStringRows)
                    {
                        txtNext = (UITextField)View.ViewWithTag (iEquipmentFloorTagId * (iPwrIdRow) + 1); //Cycle back to the first row
                    }
                    else
                    {
                        txtNext = (UITextField)View.ViewWithTag (iEquipmentFloorTagId * (iPwrIdRow) + (iStringRow + 1)); //Go to the next string, hence the + 1 here
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
        }    
    }
}


