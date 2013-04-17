
using System;
using System.Drawing;
using System.Data;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using clsiOS;
using nspTabletCommon;	
using clsTabletCommon.ITPExternal;
using System.Collections.Generic;

namespace ITPiPadSoln
{
	public partial class ProjectITPage : UIViewController
	{
        iUtils.ActivityIndicator prog = new iUtils.ActivityIndicator();
        UIView progVw = new UIView();
        Task taskA;
        Task taskB;

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
		int iQuestionHdrTagId = 10007;
		int iAnswerHdrTagId = 10008;
		int iCommentsHdrTagId = 10009;
		int iSectionHeightTagId = 10010;
		int iSectionRowsTagId = 10011;
		int iSectionStatusTagId = 10012;
		int ihfRowStatusTagId = 10100;
		int ihfAutoRowTagId = 10200;
		int iQuestionRowTagId = 10300;
		int iAnswerGroupTagId = 10400;
		int iCommentsTagId = 10500;

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

        //Tags for RFU section at theend of the screen
        int iRFUPwrIdHdrLabelTagId = 10019100;
        int iRFUDesignLoadHdrLabelTagId = 10019200;
        int iRFUCutoverLoadHdrLabelTagId = 10019300;
        int iRFUCutoverDateHdrLabelTagId = 10019400;
        int iRFUDecommissionedHdrLabelTagId = 10019500;
        int iRFUCommissionedHdrLabelTagId = 10019600;
        int iRFUSaveRFUHdrLabelTagId = 10019700;
        int ihfRowRFUStatusTagId = 10020100;
        int iRFUPwrIdRowLabelTagId = 10020200;
        int iRFUDesignLoadRowLabelTagId = 10020300; 
        int iRFUCutoverLoadRowLabelTagId = 10020400; 
        int iRFUCutoverDateRowLabelTagId = 10020500;
        int iRFUDecommissionRowCheckTagId = 10020600;
        int iRFUCommissionRowCheckTagId = 10020700;
        int iRFUButtonSaveTagId = 10020800;
        int ihfRowRFUBatteryCapacityTagId = 10020900;
        int iPwrIdExpandTagId = 10021100;
        int iPwrIdContractTagId = 10021200;
        int iPwrIdSectionInnerTagId = 10021300;
        int iPwrIdHeightTagId  = 10021400;

        //Tags for equipment section
        int iEquipmentFullRowTagId = 10021500;
        int iFloorEquipLabelTagId = 10021600;
        int iSuiteEquipLabelTagId = 10021700;
        int iRackEquipLabelTagId = 10021800;
        int iSubrackEquipLabelTagId = 10021900;
        int iPositionEquipLabelTagId = 10022000;
        int iStringEquipLabelTagId = 10022100;
        int iEquipTypeEquipLabelTagId = 10022200;
        int iDOMEquipLabelTagId = 10022300;
        int iSerialNoEquipLabelTagId = 10022400;
        int iMakeEquipLabelTagId = 10022500;
        int iModelEquipLabelTagId = 10022600;
        int iDeleteEquipLabelTagId = 10022700;
        int iBlank1EquipLabelTagId = 10022800;

        int iEquipmentRowSectionCounterTagId = 10013200;
        int iEquipmentRowPwrIdTagId = 10013300;
        int iEquipmentRowStatusTagId = 10013400;
        int iEquipmentRowAutoIdTagId = 10013500;
        int iEquipmentRowMaximoAssetIdTagId = 10013600;
        int iEquipmentBankNoTagId = 10013700;
        int iEquipmentBankNoHiddenTagId = 10013800;
        int iEquipmentBankNoSearchTagId = 10013900;
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
        int iEquipmentPositionTagId = 10016200;
        int iEquipmentPositionHiddenTagId = 10016300;
        int iEquipmentPositionSearchTagId = 10016400;
        int iEquipmentEquipTypeTagId = 10016500;
        int iEquipmentSerialNoTagId = 10016600;
        int iEquipmentTypeHiddenTagId = 10016800; //This is the integer to signify whether the item is a rack, position, string etc (1 = floor, 2 = suite, 3 = rack ...)
        int iEquipmentDeleteBatteryStringBtnTagId = 10017200;

        string m_sSessionId = "";
		string m_sPassedId = "";
		string m_sProjDesc = "";
		int m_iSections = 0;
        int m_iBatterySectionCounter = 0;
        int m_iEquipmentSectionCounter = 0;
        int m_iEquipmentPwrIds = 0;
        int m_iRFUSectionCounter = 0;
        float m_iBatteryRowHeight = 0f;
        float m_iEquipmentRowHeight = 0f;
        string m_sUser = "";
        string[] m_sBatteryMakes;
        string[] m_sBatteryModels;
        bool m_bSuppressMove = false;
        UIView m_vwSearch;

        UITableView m_cmbSearch;
        UIButton m_btnSearching;

		public enum QuestionsBitMask {NA = 1, No = 2, Yes = 4};

		public ProjectITPage () : base ("ProjectITPage", null)
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
                progVw = prog.CreateActivityIndicator();
                View.Add(progVw);
            }
            catch (Exception ex)
            {
                iUtils.AlertBox alert = new iUtils.AlertBox();
                alert.CreateAlertDialog();
                alert.SetAlertMessage(ex.Message.ToString());
                alert.ShowAlertBox(); 

            }
		}

		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return true;
		}
		public void DrawMenu()
		{
			UIView[] arrItems = new UIView[8];
			string sUsername = "";
            HomeScreen home = GetHomeScreen();
			sUsername = home.GetLoginName();
			m_sUser = sUsername;
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
                DateClass dt = new DateClass();
                int iColNo = 0;
				int iSectionId = 0;
				bool bYes;
				bool bNo;
				bool bNA;
				string sId = m_sPassedId;
				float iVert = 0f;
                float iPwrIdRowVertInner = 0f;
				float iSectionHdrRowHeight = 40f;
				float iQuestionHdrRowHeight = 20f;
				float iQuestionRowHeight = 30f;
				float iQuestionRowVert = 0f;
				float iTotalHeight = 0f;
				float iHeightToAdd = iQuestionRowHeight;
				float iHeightToAdd2 = iHeightToAdd;
                bool bDisableRow = false;
                bool[] bHideSections = new bool[1];
                UIView[] arrItems = new UIView[4];
				UIView[] arrItems2 = new UIView[6];
				UIView[] arrItems3 = new UIView[5];
                UIView[] arrItems4 = new UIView[5];
                UIView[] arrItems5 = new UIView[7];
                UIView[] arrItems6 = new UIView[9];
                UIView[] arrItems7 = new UIView[7];
                UIView[] arrItems14 = new UIView[7];



                //Get some static data for dropdowns only once so we don't reprocess unecessarily
                clsTabletDB.ITPInventory ITPInventory = new clsTabletDB.ITPInventory();
                string[] sBatteryMakes = ITPInventory.GetBatteryMakes();
                m_sBatteryMakes = sBatteryMakes;

				UIScrollView layout = new UIScrollView();
				layout.Frame = new RectangleF(0f,35f,1000f,620f);
				layout.Tag = 2;
				clsTabletDB.ITPDocumentSection ITPSection = new clsTabletDB.ITPDocumentSection();

				//Get all the sections and place a table layout for each one
				DataSet arrITPSections = ITPSection.GetLocalITPSections(sId);

				
				if (arrITPSections.Tables.Count > 0)
				{
					int iRows = arrITPSections.Tables[0].Rows.Count;
					m_iSections = iRows;
                    Array.Resize<bool>(ref bHideSections, iRows);

					for (int i = 0; i < iRows; i++)
					{
                        bHideSections[i] = false;

						//Add in a view for each section.
						UIView SectionRow = new UIView();
						iSectionId = iSectionTagId * (i+1);
						SectionRow.Tag = iSectionId;
						SectionRow.Frame = new RectangleF(0f,iVert,1000f,iSectionHdrRowHeight);

						layout.AddSubview(SectionRow);

						//Add in the section title and buttons for each section header
						UILabel hfSection = new UILabel();
						iColNo = arrITPSections.Tables[0].Columns["SectionId"].Ordinal;
						hfSection.Text = arrITPSections.Tables[0].Rows[i].ItemArray[iColNo].ToString();
						hfSection.Tag = iSectionDBIdTagId * (i+1);
						hfSection.Hidden = true;
						SectionRow.AddSubview(hfSection);

						iUtils.CreateFormGridItem Section = new iUtils.CreateFormGridItem();
						UIView SectionVw = new UIView();
						iColNo = arrITPSections.Tables[0].Columns["Name"].Ordinal;
						Section.SetDimensions(0f,0f, 550f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
						Section.SetLabelText(arrITPSections.Tables[0].Rows[i].ItemArray[iColNo].ToString());
						Section.SetBorderWidth(0.0f);
						Section.SetFontName("Verdana-Bold");
						Section.SetTextColour("White");
						Section.SetFontSize(12f);
						Section.SetCellColour("DarkSlateGrey");
						Section.SetTag(iSectionDescTagId * (i+1));
						SectionVw = Section.GetLabelCell();
						arrItems[0] = SectionVw;


						iUtils.CreateFormGridItem btnSave = new iUtils.CreateFormGridItem();
						UIView btnSaveVw = new UIView();
						btnSave.SetDimensions(550f,0f, 150f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
						btnSave.SetLabelText("Save Section");
						btnSave.SetBorderWidth(0.0f);
						btnSave.SetFontName("Verdana");
						btnSave.SetFontSize(12f);
						btnSave.SetTag(iSaveSectionBtnTagId * (i+1));
						btnSave.SetCellColour("DarkSlateGrey");
						btnSaveVw = btnSave.GetButtonCell();

						UIButton btnSaveButton = new UIButton();
						btnSaveButton = btnSave.GetButton();
						btnSaveButton.TouchUpInside += (sender,e) => {SaveThisSection(sender, e);};

						arrItems[1] = btnSaveVw;

						iUtils.CreateFormGridItem btnExpand = new iUtils.CreateFormGridItem();
						UIView btnExpandVw = new UIView();
						btnExpand.SetDimensions(700f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
						btnExpand.SetLabelText("+");
						btnExpand.SetBorderWidth(0.0f);
						btnExpand.SetFontName("Verdana");
						btnExpand.SetFontSize(12f);
						btnExpand.SetTag(iExpandSectionBtnTagId * (i+1));
						btnExpand.SetCellColour("DarkSlateGrey");
						btnExpandVw = btnExpand.GetButtonCell();
						
						UIButton btnExpandButton = new UIButton();
						btnExpandButton = btnExpand.GetButton();
                        btnExpandButton.Enabled = false;
						btnExpandButton.TouchUpInside += (sender,e) => {ExpandSection(sender, e);};
						
						arrItems[2] = btnExpandVw;

						iUtils.CreateFormGridItem btnContract = new iUtils.CreateFormGridItem();
						UIView btnContractVw = new UIView();
						btnContract.SetDimensions(750f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
						btnContract.SetLabelText("-");
						btnContract.SetBorderWidth(0.0f);
						btnContract.SetFontName("Verdana");
						btnContract.SetFontSize(12f);
						btnContract.SetTag(iContractSectionBtnTagId * (i+1));
						btnContract.SetCellColour("DarkSlateGrey");
						btnContractVw = btnContract.GetButtonCell();
						
						UIButton btnContractButton = new UIButton();
						btnContractButton = btnContract.GetButton();
						btnContractButton.TouchUpInside += (sender,e) => {ContractSection(sender, e);};
						
						arrItems[3] = btnContractVw;

						SectionRow.AddSubviews(arrItems);

						iVert += iSectionHdrRowHeight;

						//Now add a new view to this view to hold another view containing all the questions for this section
						UIView QuestionsTableRow = new UIView();
						float iQuestionRowVertTop = iVert;
						QuestionsTableRow.Frame = new RectangleF(0f,iQuestionRowVertTop,1000f,iQuestionHdrRowHeight);
						iSectionId = iContainerSectionTagId * (i+1);
						QuestionsTableRow.Tag = iSectionId;
						layout.AddSubview(QuestionsTableRow);


						//Get all the questions in this section and place a row for each one
						iColNo = arrITPSections.Tables[0].Columns["SectionId"].Ordinal;
						int iDBSectionId = Convert.ToInt32( arrITPSections.Tables[0].Rows[i].ItemArray[iColNo]);
						iColNo = arrITPSections.Tables[0].Columns["QuestionType"].Ordinal;
						int iQuestionTypes = Convert.ToInt32(arrITPSections.Tables[0].Rows[i].ItemArray[iColNo]);
						
						DataSet arrITPSectionQuestions = ITPSection.GetLocalITPSectionQuestions(sId, iDBSectionId);
						
						if (arrITPSectionQuestions.Tables.Count > 0)
						{
							int iQuestionRows = arrITPSectionQuestions.Tables[0].Rows.Count;

							//Put in the header row
							iUtils.CreateFormGridItem lblQuestionHdr = new iUtils.CreateFormGridItem();
							UIView lblQuestionHdrVw = new UIView();
							lblQuestionHdr.SetDimensions(0f,0f, 400f, iQuestionHdrRowHeight, 4f, 1f, 4f, 1f);
							lblQuestionHdr.SetLabelText("Question");
							lblQuestionHdr.SetBorderWidth(0.0f);
							lblQuestionHdr.SetFontName("Verdana-Bold");
							lblQuestionHdr.SetTextAlignment("Centre");
							lblQuestionHdr.SetFontSize(12f);
							lblQuestionHdr.SetCellColour("Pale Yellow");
							lblQuestionHdr.SetTag(iQuestionHdrTagId * (i+1));
							lblQuestionHdrVw = lblQuestionHdr.GetLabelCell();
							arrItems2[0] = lblQuestionHdrVw;

							iUtils.CreateFormGridItem lblAnswerHdr = new iUtils.CreateFormGridItem();
							UIView lblAnswerHdrVw = new UIView();
							lblAnswerHdr.SetDimensions(400f,0f, 200f, iQuestionHdrRowHeight, 4f, 1f, 4f, 1f);
							lblAnswerHdr.SetLabelText("Answer");
							lblAnswerHdr.SetBorderWidth(0.0f);
							lblAnswerHdr.SetFontName("Verdana-Bold");
							lblAnswerHdr.SetTextAlignment("Centre");
							lblAnswerHdr.SetFontSize(12f);
							lblAnswerHdr.SetCellColour("Pale Yellow");
							lblAnswerHdr.SetTag(iAnswerHdrTagId * (i+1));
							lblAnswerHdrVw = lblAnswerHdr.GetLabelCell();
							arrItems2[1] = lblAnswerHdrVw;

							iUtils.CreateFormGridItem lblCommentsHdr = new iUtils.CreateFormGridItem();
							UIView lblCommentsHdrVw = new UIView();
							lblCommentsHdr.SetDimensions(600f,0f, 200f, iQuestionHdrRowHeight, 4f, 1f, 4f, 1f);
							lblCommentsHdr.SetLabelText("Comments");
							lblCommentsHdr.SetBorderWidth(0.0f);
							lblCommentsHdr.SetFontName("Verdana-Bold");
							lblCommentsHdr.SetTextAlignment("Centre");
							lblCommentsHdr.SetFontSize(12f);
							lblCommentsHdr.SetCellColour("Pale Yellow");
							lblCommentsHdr.SetTag(iCommentsHdrTagId * (i+1));
							lblCommentsHdrVw = lblCommentsHdr.GetLabelCell();
							arrItems2[2] = lblCommentsHdrVw;
							layout.AddSubview(QuestionsTableRow);
							
							float iSectionQuestionsHeight = iQuestionHdrRowHeight;
							UILabel hfSectionHeight = new UILabel();
							hfSectionHeight.Tag = iSectionHeightTagId * (i+1);
							hfSectionHeight.Hidden = true;
							arrItems2[3] = hfSectionHeight;

							UILabel hfSectionRows = new UILabel();
							hfSectionRows.Tag = iSectionRowsTagId * (i+1);
							hfSectionRows.Hidden = true;
							hfSectionRows.Text = iQuestionRows.ToString();
							arrItems2[4] = hfSectionRows;

							UILabel hfSectionStatus = new UILabel();
							hfSectionStatus.Tag = iSectionStatusTagId * (i+1);
							hfSectionStatus.Hidden = true;
							hfSectionStatus.Text = "0";
							arrItems2[5] = hfSectionStatus;

							QuestionsTableRow.AddSubviews(arrItems2);

							iVert += iQuestionHdrRowHeight;
							iQuestionRowVert = iQuestionHdrRowHeight;
                            bool bSectionFullyCompleted = true;

							for (int j = 0; j < iQuestionRows; j++)
							{

								UILabel hfRowStatus = new UILabel();
								hfRowStatus.Text = "0";
								hfRowStatus.Tag = (ihfRowStatusTagId + (j+1)) * (i+1);
								hfRowStatus.Hidden = true;
								arrItems3[0] = hfRowStatus;

                                iColNo = arrITPSectionQuestions.Tables[0].Columns["AutoId"].Ordinal;
                                string sAutoId = arrITPSectionQuestions.Tables[0].Rows[j].ItemArray[iColNo].ToString();
                                iColNo = arrITPSectionQuestions.Tables[0].Columns["Question"].Ordinal;
                                string sQuestion = arrITPSectionQuestions.Tables[0].Rows[j].ItemArray[iColNo].ToString();

                                UILabel hfAutoId = new UILabel();
                                hfAutoId.Text = sAutoId;
								hfAutoId.Tag = (ihfAutoRowTagId + (j+1)) * (i+1);
								hfAutoId.Hidden = true;
								arrItems3[1] = hfAutoId;

								//Put in the question
								iUtils.CreateFormGridItem rowQuestion = new iUtils.CreateFormGridItem();
								UIView rowQuestionVw = new UIView();
								rowQuestion.SetLabelWrap(1); //This means the text will be wrapped in the label
								rowQuestion.SetDimensions(0f,iQuestionRowVert, 400f, iQuestionRowHeight, 2f, 2.5f, 2f, 2.5f);
                                rowQuestion.SetLabelText(sQuestion);
								rowQuestion.SetBorderWidth(0.0f);
								rowQuestion.SetFontName("Verdana");
								rowQuestion.SetFontSize(12f);
								rowQuestion.SetTag((iQuestionRowTagId + (j+1)) * (i+1));

								if (j % 2 == 0)					
								{
									rowQuestion.SetCellColour("Pale Blue");
								}
								else
								{
									rowQuestion.SetCellColour("Sky Blue");
								}

								rowQuestionVw = rowQuestion.GetLabelCell();
								iHeightToAdd = rowQuestion.GetCellHeight();

								//Put in the answer
								iColNo = arrITPSectionQuestions.Tables[0].Columns["Yes"].Ordinal;
								bYes = Convert.ToBoolean(arrITPSectionQuestions.Tables[0].Rows[j].ItemArray[iColNo]);

								iColNo = arrITPSectionQuestions.Tables[0].Columns["No"].Ordinal;
								bNo = Convert.ToBoolean(arrITPSectionQuestions.Tables[0].Rows[j].ItemArray[iColNo]);

								iColNo = arrITPSectionQuestions.Tables[0].Columns["NA"].Ordinal;
								bNA = Convert.ToBoolean(arrITPSectionQuestions.Tables[0].Rows[j].ItemArray[iColNo]);

								iUtils.CreateFormGridItem radGrp = new iUtils.CreateFormGridItem();
								UIView radGrpVw = new UIView();
								radGrp.SetDimensions(400f,iQuestionRowVert, 200f, iQuestionRowHeight, 2f, 2.5f, 2f, 2.5f);
								radGrp.SetFontName("Verdana");
								radGrp.SetFontSize(12f);
								radGrp.SetTag((iAnswerGroupTagId + (j+1)) * (i+1));

								if (j % 2 == 0)
								{
									radGrp.SetCellColour("Pale Blue");
								}
								else
								{
									radGrp.SetCellColour("Sky Blue");
								}

								radGrpVw = radGrp.GetRadioButtonCell();

								UISegmentedControl radGrpRadio = new UISegmentedControl();
								radGrpRadio = radGrp.GetRadioGroup();
								radGrpRadio.TouchUpInside += (sender,e) => {SetRowRadioChanged(sender, e);};
                                radGrpRadio.ValueChanged += (sender,e) => {SetRowRadioChanged(sender, e);};

								QuestionsBitMask mask = (QuestionsBitMask)iQuestionTypes;
								int iPos = 0;
                                bool bQuestionSet = false;
								
								if ((mask & QuestionsBitMask.Yes) == QuestionsBitMask.Yes)
								{
									radGrpRadio.InsertSegment("Yes", iPos,false);
									if(bYes)
									{
										radGrpRadio.SelectedSegment = iPos;
                                        bQuestionSet= true; 
									}
									iPos++;
								}

								if ((mask & QuestionsBitMask.No) == QuestionsBitMask.No)
								{
									radGrpRadio.InsertSegment("No", iPos,false);
									if(bNo)
									{
										radGrpRadio.SelectedSegment = iPos;
                                        bQuestionSet= true; 
                                    }
									iPos++;
								}

								if ((mask & QuestionsBitMask.NA) == QuestionsBitMask.NA)
								{
									radGrpRadio.InsertSegment("N/A", iPos,false);
									if(bNA)
									{
										radGrpRadio.SelectedSegment = iPos;
                                        bQuestionSet= true; 
                                    }
								}

                                if(bQuestionSet)
                                {
                                    radGrpRadio.Enabled = false;
                                }
                                else
                                {
                                    bSectionFullyCompleted = false;
                                }

								//Put in the comments
								iUtils.CreateFormGridItem rowComments = new iUtils.CreateFormGridItem();
								UIView rowCommentsVw = new UIView();
								iColNo = arrITPSectionQuestions.Tables[0].Columns["Comments"].Ordinal;
								rowComments.SetLabelWrap(1); //This means the text will be wrapped in the label
								rowComments.SetDimensions(600f,iQuestionRowVert, 200f, iQuestionRowHeight, 2f, 2.5f, 2f, 2.5f);
								rowComments.SetLabelText(arrITPSectionQuestions.Tables[0].Rows[j].ItemArray[iColNo].ToString());
								rowComments.SetBorderWidth(0.0f);
                                rowComments.SetFontName("Verdana");
								rowComments.SetFontSize(12f);
								rowComments.SetTag((iCommentsTagId + (j+1)) * (i+1)); //
								
								if (j % 2 == 0)
								{
									rowComments.SetCellColour("Pale Blue");
								}
								else
								{
									rowComments.SetCellColour("Sky Blue");
								}
								
								rowCommentsVw = rowComments.GetTextCell();
								iHeightToAdd2 = rowComments.GetCellHeight();
								UITextView rowCommentsTextVw = new UITextView();
								rowCommentsTextVw = rowComments.GetTextView();
								rowCommentsTextVw.Changed += (sender,e) => {SetRowEditTextChanged(sender, e);};

                                if(bQuestionSet)
                                {
                                    rowCommentsTextVw.Editable = false;
                                }

                                if(iHeightToAdd2 > iHeightToAdd)
								{
									rowQuestion.SetDimensions(0f,iQuestionRowVert, 400f, iHeightToAdd2, 2f, 2.5f, 2f, 2.5f);
									rowQuestion.ResetCellViewDimensions(rowQuestionVw);
									radGrp.SetDimensions(400f,iQuestionRowVert, 200f, iHeightToAdd2, 2f, 2.5f, 2f, 2.5f);
									radGrp.ResetCellViewDimensions(radGrpVw);
								}
								else
								{
									radGrp.SetDimensions(400f,iQuestionRowVert, 200f, iHeightToAdd, 2f, 2.5f, 2f, 2.5f);
									radGrp.ResetCellViewDimensions(radGrpVw);
									rowComments.SetDimensions(600f,iQuestionRowVert, 200f, iHeightToAdd, 2f, 2.5f, 2f, 2.5f);
									rowComments.ResetCellViewDimensions(rowCommentsVw);
									rowComments.ResetCellTextViewDimensions(rowCommentsTextVw);
								}

								arrItems3[2] = rowQuestionVw;
								arrItems3[3] = radGrpVw;
								arrItems3[4] = rowCommentsVw;

								QuestionsTableRow.AddSubviews(arrItems3);

								iSectionQuestionsHeight += iHeightToAdd;
								iQuestionRowVert += iHeightToAdd;
								iVert += iHeightToAdd;

							}	

							//Now resize the UIView that is effectively the container for the questions for this section
							//And also store this height in a hidden field for use in the contract and expand functions
							QuestionsTableRow.Frame = new RectangleF(0f,iQuestionRowVertTop,1000f,iSectionQuestionsHeight);
							hfSectionHeight.Text = iSectionQuestionsHeight.ToString();

                            if(bSectionFullyCompleted)
                            {
                                bHideSections[i] = true;
                            }
						}
					}

				}

				//******************************************************************************************//
				//                      SECTION 10 (BATTERIES)                                              //
				//******************************************************************************************//
				//Get all the PwrId's for this project from ITPSection10
				DataSet arrITPSection10PwrIds = ITPSection.GetLocalITPSection10PwrIds(sId, 6);

				if (arrITPSection10PwrIds.Tables.Count > 0)
				{
					int ii = arrITPSections.Tables[0].Rows.Count;
					m_iSections++; //Add an extra one for the batteries section
                    m_iBatterySectionCounter = ii;
					int iPwrIdRows = arrITPSection10PwrIds.Tables[0].Rows.Count;

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
					Section10.SetDimensions(0f,0f, 450f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
					Section10.SetLabelText("BATTERIES");
					Section10.SetBorderWidth(0.0f);
					Section10.SetFontName("Verdana-Bold");
					Section10.SetTextColour("White");
					Section10.SetFontSize(12f);
					Section10.SetCellColour("DarkSlateGrey");
					Section10.SetTag(iSectionDescTagId * (ii+1));
					Section10Vw = Section10.GetLabelCell();
					arrItems4[0] = Section10Vw;
					
					
					iUtils.CreateFormGridItem btnSave10 = new iUtils.CreateFormGridItem();
					UIView btnSave10Vw = new UIView();
					btnSave10.SetDimensions(450f,0f, 350f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
					btnSave10.SetLabelText("Open Battery Details Screen");
					btnSave10.SetBorderWidth(0.0f);
					btnSave10.SetFontName("Verdana");
					btnSave10.SetFontSize(12f);
					btnSave10.SetTag(iSaveSectionBtnTagId * (ii+1));
					btnSave10.SetCellColour("DarkSlateGrey");
					btnSave10Vw = btnSave10.GetButtonCell();
					
					UIButton btnSave10Button = new UIButton();
					btnSave10Button = btnSave10.GetButton();
					btnSave10Button.TouchUpInside += (sender,e) => {OpenBatteries(sender, e);};
					
					arrItems4[1] = btnSave10Vw;
					
//					iUtils.CreateFormGridItem lblBlank10 = new iUtils.CreateFormGridItem();
//					UIView lblBlank10Vw = new UIView();
//					lblBlank10.SetDimensions(700f,0f, 100f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
//					lblBlank10.SetLabelText("");
//					lblBlank10.SetBorderWidth(0.0f);
//					lblBlank10.SetFontName("Verdana");
//					lblBlank10.SetFontSize(12f);
//					lblBlank10.SetTag(iExpandSectionBtnTagId * (ii+1));
//					lblBlank10.SetCellColour("DarkSlateGrey");
//                    lblBlank10Vw = lblBlank10.GetLabelCell();										
//					arrItems4[2] = lblBlank10Vw;
//					
//					iUtils.CreateFormGridItem btnContract10 = new iUtils.CreateFormGridItem();
//					UIView btnContract10Vw = new UIView();
//					btnContract10.SetDimensions(750f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
//					btnContract10.SetLabelText("-");
//					btnContract10.SetBorderWidth(0.0f);
//					btnContract10.SetFontName("Verdana");
//                    btnContract10.SetFontSize(12f);
//					btnContract10.SetTag(iContractSectionBtnTagId * (ii+1));
//					btnContract10.SetCellColour("DarkSlateGrey");
//					btnContract10Vw = btnContract10.GetButtonCell();
//					
//					UIButton btnContract10Button = new UIButton();
//					btnContract10Button = btnContract10.GetButton();
//					btnContract10Button.TouchUpInside += (sender,e) => {ContractSection(sender, e);};
//					
//					arrItems4[3] = btnContract10Vw;
					
					UILabel hfSectionHeight = new UILabel();
					hfSectionHeight.Tag = iSectionHeightTagId * (ii+1);
					hfSectionHeight.Hidden = true;
					hfSectionHeight.Text = "0";
					arrItems4[2] = hfSectionHeight;
					
					UILabel hfSectionRows = new UILabel();
					hfSectionRows.Tag = iSectionRowsTagId * (ii+1);
					hfSectionRows.Hidden = true;
					hfSectionRows.Text = iPwrIdRows.ToString();
					arrItems4[3] = hfSectionRows;
					
					UILabel hfSectionStatus = new UILabel();
					hfSectionStatus.Tag = iSectionStatusTagId * (ii+1);
					hfSectionStatus.Hidden = true;
					hfSectionStatus.Text = "0";
					arrItems4[4] = hfSectionStatus;
					

					Section10Row.AddSubviews(arrItems4);
					
					iVert += iSectionHdrRowHeight;

					//Now add a new view to this view to hold another view containing all the pwrid info for this section 10
					UIView PwrIdTableRow = new UIView();
					PwrIdTableRow.Frame = new RectangleF(0f,iVert,1000f,0f);
					iSectionId = iContainerSectionTagId * (ii+1);
					PwrIdTableRow.Tag = iSectionId;
					layout.AddSubview(PwrIdTableRow);
				}
				

                //******************************************************************************************//
                //                      SECTION 10 (EQUIPMENT)                                              //
                //******************************************************************************************//
                //Get all the PwrId's for this project from ITPSection10
                DataSet arrITPSectionEquipmentPwrIds = ITPSection.GetLocalITPSectionEquipmentPwrIds(sId);
                
                if (arrITPSectionEquipmentPwrIds.Tables.Count > 0)
                {
                    int iii = m_iSections;
                    m_iSections++; //Add an extra one for the equipment section
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
                    SectionEquipment.SetDimensions(0f,0f, 450f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
                    SectionEquipment.SetLabelText("POWER CONVERSION EQUIPMENT");
                    SectionEquipment.SetBorderWidth(0.0f);
                    SectionEquipment.SetFontName("Verdana-Bold");
                    SectionEquipment.SetTextColour("White");
                    SectionEquipment.SetFontSize(12f);
                    SectionEquipment.SetCellColour("DarkSlateGrey");
                    SectionEquipment.SetTag(iSectionDescTagId * (iii+1));
                    SectionEquipmentVw = SectionEquipment.GetLabelCell();
                    arrItems4[0] = SectionEquipmentVw;
                    
                    
                    iUtils.CreateFormGridItem btnSaveEquipment = new iUtils.CreateFormGridItem();
                    UIView btnSaveEquipmentVw = new UIView();
                    btnSaveEquipment.SetDimensions(450f,0f, 350f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                    btnSaveEquipment.SetLabelText("Open Power Conversion Equipment Screen");
                    btnSaveEquipment.SetBorderWidth(0.0f);
                    btnSaveEquipment.SetFontName("Verdana");
                    btnSaveEquipment.SetFontSize(12f);
                    btnSaveEquipment.SetTag(iSaveSectionBtnTagId * (iii+1));
                    btnSaveEquipment.SetCellColour("DarkSlateGrey");
                    btnSaveEquipmentVw = btnSaveEquipment.GetButtonCell();
                    
                    UIButton btnSaveEquipmentButton = new UIButton();
                    btnSaveEquipmentButton = btnSaveEquipment.GetButton();
                    btnSaveEquipmentButton.TouchUpInside += (sender,e) => {OpenPowerConversion(sender, e);};
                    
                    arrItems4[1] = btnSaveEquipmentVw;
                    
//                    iUtils.CreateFormGridItem btnExpandEquipment = new iUtils.CreateFormGridItem();
//                    UIView btnExpandEquipmentVw = new UIView();
//                    btnExpandEquipment.SetDimensions(700f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
//                    btnExpandEquipment.SetLabelText("+");
//                    btnExpandEquipment.SetBorderWidth(0.0f);
//                    btnExpandEquipment.SetFontName("Verdana");
//                    btnExpandEquipment.SetFontSize(12f);
//                    btnExpandEquipment.SetTag(iExpandSectionBtnTagId * (iii+1));
//                    btnExpandEquipment.SetCellColour("DarkSlateGrey");
//                    btnExpandEquipmentVw = btnExpandEquipment.GetButtonCell();
//                    
//                    UIButton btnExpandEquipmentButton = new UIButton();
//                    btnExpandEquipmentButton = btnExpandEquipment.GetButton();
//                    btnExpandEquipmentButton.Enabled = false;
//                    btnExpandEquipmentButton.TouchUpInside += (sender,e) => {ExpandSection(sender, e);};
//                    
//                    arrItems14[2] = btnExpandEquipmentVw;
//                    
//                    iUtils.CreateFormGridItem btnContractEquipment = new iUtils.CreateFormGridItem();
//                    UIView btnContractEquipmentVw = new UIView();
//                    btnContractEquipment.SetDimensions(750f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
//                    btnContractEquipment.SetLabelText("-");
//                    btnContractEquipment.SetBorderWidth(0.0f);
//                    btnContractEquipment.SetFontName("Verdana");
//                    btnContractEquipment.SetFontSize(12f);
//                    btnContractEquipment.SetTag(iContractSectionBtnTagId * (iii+1));
//                    btnContractEquipment.SetCellColour("DarkSlateGrey");
//                    btnContractEquipmentVw = btnContractEquipment.GetButtonCell();
//                    
//                    UIButton btnContractEquipmentButton = new UIButton();
//                    btnContractEquipmentButton = btnContractEquipment.GetButton();
//                    btnContractEquipmentButton.TouchUpInside += (sender,e) => {ContractSection(sender, e);};
//                    
//                    arrItems14[3] = btnContractEquipmentVw;
                    
                    UILabel hfSectionEquipmentHeight = new UILabel();
                    hfSectionEquipmentHeight.Tag = iSectionHeightTagId * (iii+1);
                    hfSectionEquipmentHeight.Hidden = true;
                    hfSectionEquipmentHeight.Text = "0";
                    arrItems4[2] = hfSectionEquipmentHeight;
                    
                    UILabel hfSectionEquipmentRows = new UILabel();
                    hfSectionEquipmentRows.Tag = iSectionRowsTagId * (iii+1);
                    hfSectionEquipmentRows.Hidden = true;
                    hfSectionEquipmentRows.Text = iPwrIdRows.ToString();
                    arrItems4[3] = hfSectionEquipmentRows;
                    
                    UILabel hfSectionEquipmentStatus = new UILabel();
                    hfSectionEquipmentStatus.Tag = iSectionStatusTagId * (iii+1);
                    hfSectionEquipmentStatus.Hidden = true;
                    hfSectionEquipmentStatus.Text = "0";
                    arrItems4[4] = hfSectionEquipmentStatus;
                    
                    
                    SectionEquipmentRow.AddSubviews(arrItems4);
                    
                    iVert += iSectionHdrRowHeight;
                    
                    //Now add a new view to this view to hold another view containing all the pwrid info for this section 10
                    UIView PwrIdTableRow = new UIView();
                    PwrIdTableRow.Frame = new RectangleF(0f,iVert,1000f,0f);
                    iSectionId = iContainerSectionTagId * (iii+1);
                    PwrIdTableRow.Tag = iSectionId;
                    layout.AddSubview(PwrIdTableRow);
//                    float iPwrIdRowVert = 0.0f;
//                    float iSectionPwrIdHeight = 0.0f;
//                    float iPwrIdRowVertTop = iVert;
//                    float iPwrIdRowInnerTop = 0.0f;
//                    float iPwrIdRowInnerTop2 = 0.0f;
//
//                    //Temporray
//                    //iPwrIdRows = 3;
//                    m_iEquipmentPwrIds = iPwrIdRows;
//                    for (int jj = 0; jj < iPwrIdRows; jj++)
//                    {
//                        iPwrIdRowInnerTop2 = 0.0f;
//                        UIView vwPwrInternalRowId = new UIView();
//                        vwPwrInternalRowId.Frame = new RectangleF(0f,iPwrIdRowVert,1000f,200f); //This will be resized later on
//                        vwPwrInternalRowId.Tag = (iPwrIdSectionTagId + (jj+1)) * (iii+1);  
//
//                        
//                        UILabel hfRow10Status = new UILabel();
//                        hfRow10Status.Text = "0";
//                        hfRow10Status.Tag = (ihfRow10StatusTagId + (jj+1)) * (iii+1);
//                        hfRow10Status.Hidden = true;
//                        arrItems5[0] = hfRow10Status;
//                        
//                        //Put in the PwrId Label
//                        iUtils.CreateFormGridItem rowPwrIdLabel = new iUtils.CreateFormGridItem();
//                        UIView rowPwrIdLabelVw = new UIView();
//                        iColNo = arrITPSection10PwrIds.Tables[0].Columns["PwrId"].Ordinal;
//                        string sPwrId = arrITPSection10PwrIds.Tables[0].Rows[jj].ItemArray[iColNo].ToString();
//                        rowPwrIdLabel.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
//                        rowPwrIdLabel.SetDimensions(0f,iPwrIdRowVert, 200f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
//                        rowPwrIdLabel.SetLabelText(sPwrId);
//                        rowPwrIdLabel.SetBorderWidth(0.0f);
//                        rowPwrIdLabel.SetFontName("Verdana-Bold");
//                        rowPwrIdLabel.SetFontSize(18f);
//                        rowPwrIdLabel.SetTag((iPwrIdRowLabelTagId + (jj+1)) * (iii+1));
//                        
//                        if (jj % 2 == 0)
//                        {
//                            rowPwrIdLabel.SetCellColour("Pale Yellow");
//                        }
//                        else
//                        {
//                            rowPwrIdLabel.SetCellColour("Pale Orange");
//                        }
//                        
//                        rowPwrIdLabelVw = rowPwrIdLabel.GetLabelCell();
//                        iHeightToAdd = iSectionHdrRowHeight;
//                        arrItems5[1] = rowPwrIdLabelVw;
//                        
//                        iUtils.CreateFormGridItem btnNewEquipment = new iUtils.CreateFormGridItem();
//                        UIView btnNewEquipmentVw = new UIView();
//                        btnNewEquipment.SetDimensions(200f,iPwrIdRowVert, 350f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
//                        btnNewEquipment.SetLabelText("New Item");
//                        btnNewEquipment.SetBorderWidth(0.0f);
//                        btnNewEquipment.SetFontName("Verdana");
//                        btnNewEquipment.SetFontSize(12f);
//                        btnNewEquipment.SetTag((iPwrIdNewBtnTagId + (jj+1)) * (iii+1));
//                        if (jj % 2 == 0)
//                        {
//                            btnNewEquipment.SetCellColour("Pale Yellow");
//                        }
//                        else
//                        {
//                            btnNewEquipment.SetCellColour("Pale Orange");
//                        }
//                        btnNewEquipmentVw = btnNewEquipment.GetButtonCell();
//                        
//                        UIButton btnNewEquipmentButton = new UIButton();
//                        btnNewEquipmentButton = btnNewEquipment.GetButton();
//                        btnNewEquipmentButton.TouchUpInside += (sender,e) => {AddNewEquipment(sender, e);};
//                        
//                        arrItems5[2] = btnNewEquipmentVw;
//                        
//                        iUtils.CreateFormGridItem rowPwrIdBlank = new iUtils.CreateFormGridItem();
//                        UIView rowPwrIdBlankVw = new UIView();
//                        rowPwrIdBlank.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
//                        rowPwrIdBlank.SetDimensions(550f,iPwrIdRowVert, 350f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
//                        rowPwrIdBlank.SetLabelText("");
//                        rowPwrIdBlank.SetBorderWidth(0.0f);
//                        rowPwrIdBlank.SetFontName("Verdana");
//                        rowPwrIdBlank.SetFontSize(12f);
//                        rowPwrIdBlank.SetTag((iPwrIdRowLabelTagId + (jj+1)) * (iii+1));
//                        
//                        if (jj % 2 == 0)
//                        {
//                            rowPwrIdBlank.SetCellColour("Pale Yellow");
//                        }
//                        else
//                        {
//                            rowPwrIdBlank.SetCellColour("Pale Orange");
//                        }
//                        
//                        rowPwrIdBlankVw = rowPwrIdBlank.GetLabelCell();
//                        arrItems5[3] = rowPwrIdBlankVw;
//                        
//                        iUtils.CreateFormGridItem btnExpandPwrId = new iUtils.CreateFormGridItem();
//                        UIView btnExpandPwrIdVw = new UIView();
//                        btnExpandPwrId.SetDimensions(900f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
//                        btnExpandPwrId.SetLabelText("+");
//                        btnExpandPwrId.SetBorderWidth(0.0f);
//                        btnExpandPwrId.SetFontName("Verdana");
//                        btnExpandPwrId.SetFontSize(12f);
//                        btnExpandPwrId.SetTag((iPwrIdExpandTagId + (jj+1)) * (iii+1));
//                        if (jj % 2 == 0)
//                        {
//                            btnExpandPwrId.SetCellColour("Pale Yellow");
//                        }
//                        else
//                        {
//                            btnExpandPwrId.SetCellColour("Pale Orange");
//                        }
//                        btnExpandPwrIdVw = btnExpandPwrId.GetButtonCell();
//                        
//                        UIButton btnExpandPwrIdButton = new UIButton();
//                        btnExpandPwrIdButton = btnExpandPwrId.GetButton();
//                        btnExpandPwrIdButton.Enabled = false;
//                        btnExpandPwrIdButton.TouchUpInside += (sender,e) => {ExpandPwrId(sender, e, 2);};
//                        
//                        arrItems5[4] = btnExpandPwrIdVw;
//                        
//                        iUtils.CreateFormGridItem btnContractPwrId = new iUtils.CreateFormGridItem();
//                        UIView btnContractPwrIdVw = new UIView();
//                        btnContractPwrId.SetDimensions(950f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
//                        btnContractPwrId.SetLabelText("-");
//                        btnContractPwrId.SetBorderWidth(0.0f);
//                        btnContractPwrId.SetFontName("Verdana");
//                        btnContractPwrId.SetFontSize(12f);
//                        btnContractPwrId.SetTag((iPwrIdContractTagId + (jj+1)) * (iii+1));
//                        if (jj % 2 == 0)
//                        {
//                            btnContractPwrId.SetCellColour("Pale Yellow");
//                        }
//                        else
//                        {
//                            btnContractPwrId.SetCellColour("Pale Orange");
//                        }
//                        btnContractPwrIdVw = btnContractPwrId.GetButtonCell();
//                        
//                        UIButton btnContractPwrIdButton = new UIButton();
//                        btnContractPwrIdButton = btnContractPwrId.GetButton();
//                        btnContractPwrIdButton.TouchUpInside += (sender,e) => {ContractPwrId(sender, e, 2);};
//                        
//                        arrItems5[5] = btnContractPwrIdVw;
//                        
//                        UILabel hfPwrIdSectionHeight = new UILabel();
//                        hfPwrIdSectionHeight.Tag = (iPwrIdHeightTagId + (jj+1)) * (iii+1);
//                        hfPwrIdSectionHeight.Hidden = true;
//                        hfPwrIdSectionHeight.Text = "0";
//                        arrItems5[6] = hfPwrIdSectionHeight;
//                        
//                        iHeightToAdd = iSectionHdrRowHeight;
//                        
//                        //Now add the row details into the view
//                        vwPwrInternalRowId.AddSubviews(arrItems5);
//                        
//                        iSectionPwrIdHeight += iHeightToAdd;
//                        iPwrIdRowVert += iHeightToAdd;
//                        iVert += iHeightToAdd;
//                        iPwrIdRowInnerTop2 += iHeightToAdd;
//                        
//                        iPwrIdRowVertInner = 0f;
//                        UIView vwPwrInternalRowIdInnner = new UIView();
//                        vwPwrInternalRowIdInnner.Tag = (iPwrIdSectionInnerTagId + (jj+1)) * (iii+1);                   
//                        vwPwrInternalRowIdInnner.Frame = new RectangleF(0f,iPwrIdRowVertInner,1000f,200f); //This will be resized later on
////                        vwPwrInternalRowIdInnner.Hidden = true;  
//
//                        
//                        UIView PwrIdHdr = BuildEquipmentHeader(jj, ref iHeightToAdd);
//                        PwrIdHdr.Frame = new RectangleF(0f, iPwrIdRowVertInner, 1000f, iHeightToAdd);
//                        vwPwrInternalRowIdInnner.AddSubview(PwrIdHdr);
//                        vwPwrInternalRowId.AddSubview(vwPwrInternalRowIdInnner);
//                        
//                        iSectionPwrIdHeight += iHeightToAdd;
//                        iPwrIdRowVert += iHeightToAdd;
//                        iPwrIdRowVertInner += iHeightToAdd;
//                        iVert += iHeightToAdd;
//                        
//                        //Now for each PwrId get the details for each string
//                        DataSet arrITPSection10PwrIdItems = ITPSection.GetLocalITPSection10PwrIdEquipmentDetails(sId, sPwrId);
//                        
//                        if (arrITPSection10PwrIdItems.Tables.Count > 0)
//                        {
//                            int iPwrIdItemRows = arrITPSection10PwrIdItems.Tables[0].Rows.Count;
//                            //Add the rows to a hidden field so we know how many rows are in each PwrId battery block
//                            UILabel hfPwrIdStringRows = new UILabel();
//                            hfPwrIdStringRows.Text = iPwrIdItemRows.ToString();
//                            hfPwrIdStringRows.Tag = (ihfPwrIdStringRowsTagId + (jj+1)) * (iii+1);
//                            hfPwrIdStringRows.Hidden = true;
//                            vwPwrInternalRowIdInnner.AddSubview(hfPwrIdStringRows);
//                            
//                            
//                            for (var kk = 0; kk < iPwrIdItemRows; kk++)
//                            {
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["AutoId"].Ordinal;
//                                int iAutoId = Convert.ToInt32(arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo]);
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["BankNo"].Ordinal;
//                                string sBankNo = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Make"].Ordinal;
//                                string sMake = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Model"].Ordinal;
//                                string sModel = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["SPN"].Ordinal;
//                                string sSPN = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["DOM"].Ordinal;
//                                string sDOM = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Floor"].Ordinal;
//                                string sFloor = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Suite"].Ordinal;
//                                string sSuite = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Rack"].Ordinal;
//                                string sRack = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["SubRack"].Ordinal;
//                                string sSubRack = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Position"].Ordinal;
//                                string sPosition = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Equipment_Condition"].Ordinal;
//                                string sEquipType = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["SerialBatch"].Ordinal;
//                                string sSerialNo = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["tblMaximoPSA_ID"].Ordinal;
//                                string sMaximoPSAId = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["tblMaximoTransfer_Eqnum"].Ordinal;
//                                string sMaximoTransferId = arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo].ToString();
//                                if(sMaximoPSAId == "" || sMaximoPSAId == "0")
//                                {
//                                    sMaximoPSAId = "-1";
//                                }
//                                if(sMaximoTransferId == "" || sMaximoTransferId == "0")
//                                {
//                                    sMaximoTransferId = "-1";
//                                }
//                                int iMaximoPSAId = Convert.ToInt32(sMaximoPSAId);
//                                int iMaximoTransferId = Convert.ToInt32(sMaximoTransferId);
//                                int iMaximoAssetId = -1;
//                                
//                                if(iMaximoPSAId > 0)
//                                {
//                                    iMaximoAssetId = iMaximoPSAId;
//                                }
//                                else if(iMaximoTransferId > 0)
//                                {
//                                    iMaximoAssetId = iMaximoTransferId;
//                                }
//                                else
//                                {
//                                    iMaximoAssetId = -1;
//                                }
//
//                                iColNo = arrITPSection10PwrIdItems.Tables[0].Columns["Equipment_Type"].Ordinal;
//                                int iEquipmentType = Convert.ToInt32(arrITPSection10PwrIdItems.Tables[0].Rows[kk].ItemArray[iColNo]);
//
//                                //Add in the row
//                                UIView EquipmentItemRow = BuildEquipmentItemRowDetails(iii, jj, kk, sPwrId, iAutoId,
//                                                                                       iMaximoAssetId, sBankNo,
//                                                                                       sMake, sModel, sSPN, sDOM,
//                                                                                       sFloor, sSuite, sRack, sSubRack, sPosition, 
//                                                                                       sEquipType, sSerialNo, iEquipmentType,
//                                                                                       false, ref iHeightToAdd);
//                                EquipmentItemRow.Frame = new RectangleF(0f, iPwrIdRowVertInner, 1000f, iHeightToAdd);
//                                EquipmentItemRow.Tag = iEquipmentFullRowTagId * (jj + 1) + (kk + 1);
//                                vwPwrInternalRowIdInnner.AddSubview(EquipmentItemRow);
//
//                                m_iEquipmentRowHeight = iHeightToAdd;
//                                iSectionPwrIdHeight += iHeightToAdd;
//                                iPwrIdRowVert += iHeightToAdd;
//                                iPwrIdRowVertInner += iHeightToAdd;
//                                iVert += iHeightToAdd;
//
//                            }
//
//                            hfPwrIdSectionHeight.Text = iPwrIdRowVertInner.ToString();
//                            vwPwrInternalRowIdInnner.Frame = new RectangleF(0f, iPwrIdRowInnerTop2, 1000f, iPwrIdRowVertInner);
//                            vwPwrInternalRowId.Frame = new RectangleF(0f, iPwrIdRowInnerTop, 1000f, iPwrIdRowVert);
//                            PwrIdTableRow.AddSubview(vwPwrInternalRowId);
//                            iPwrIdRowInnerTop += iPwrIdRowVert;
//                            //iPwrIdRowInnerTop2 += iPwrIdRowVertInner;
//                            iPwrIdRowVert = 0f;
//                        }
//                        
//                    }
//                    //Now resize the UIView that is effectively the container for the battery info for this section
//                    //And also store this height in a hidden field for use in the contract and expand functions
//                    PwrIdTableRow.Frame = new RectangleF(0f,iPwrIdRowVertTop,1000f,iSectionPwrIdHeight);
//                    hfSectionEquipmentHeight.Text = iSectionPwrIdHeight.ToString();
                }

                //******************************************************************************************//
                //                      SECTION RFU                                                         //
                //******************************************************************************************//
                //Get all the PwrId's for this project from ITPRFU
                DataSet arrITPRFUs = ITPSection.GetLocalITPRFUPwrIds(sId);
                
                if (arrITPRFUs.Tables.Count > 0)
                {
                    float iRFURowVert = 0.0f;
                    float iSectionRFUHeight = 0.0f;
                    float iRFURowVertTop = iVert;
                    float iRFURowInnerTop = 0.0f;

                    int ii = m_iSections;
                    m_iSections++; //Add an extra one for the RFU section
                    m_iRFUSectionCounter = ii; //Here ii and m_iSections are different by 1. If we add more sections after this it will be different later on
                    int iPwrIdRowsRFU = arrITPRFUs.Tables[0].Rows.Count;
                    
                    //Add in the section title and buttons for each section header
                    UIView SectionRFURow = new UIView();
                    float iSection10RowVertTop = iVert;
                    SectionRFURow.Frame = new RectangleF(0f,iSection10RowVertTop,1000f,iSectionHdrRowHeight);
                    iSectionId = iSectionTagId * (ii+1);
                    SectionRFURow.Tag = iSectionId;
                    layout.AddSubview(SectionRFURow);
                    
                    UILabel hfSectionRFU = new UILabel();
                    hfSectionRFU.Text = "RFU";
                    hfSectionRFU.Tag = iSectionDBIdTagId * (ii+1);
                    hfSectionRFU.Hidden = true;
                    SectionRFURow.AddSubview(hfSectionRFU);
                    
                    iUtils.CreateFormGridItem SectionRFU = new iUtils.CreateFormGridItem();
                    UIView SectionRFUVw = new UIView();
                    SectionRFU.SetDimensions(0f,0f, 550f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
                    SectionRFU.SetLabelText("READY FOR USE (RFU)");
                    SectionRFU.SetBorderWidth(0.0f);
                    SectionRFU.SetFontName("Verdana-Bold");
                    SectionRFU.SetTextColour("White");
                    SectionRFU.SetFontSize(12f);
                    SectionRFU.SetCellColour("DarkSlateGrey");
                    SectionRFU.SetTag(iSectionDescTagId * (ii+1));
                    SectionRFUVw = SectionRFU.GetLabelCell();
                    arrItems14[0] = SectionRFUVw;
                    
                    
                    iUtils.CreateFormGridItem btnSaveRFU = new iUtils.CreateFormGridItem();
                    UIView btnSaveRFUVw = new UIView();
                    btnSaveRFU.SetDimensions(550f,0f, 150f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                    btnSaveRFU.SetLabelText("Save Section");
                    btnSaveRFU.SetBorderWidth(0.0f);
                    btnSaveRFU.SetFontName("Verdana");
                    btnSaveRFU.SetFontSize(12f);
                    btnSaveRFU.SetTag(iSaveSectionBtnTagId * (ii+1));
                    btnSaveRFU.SetCellColour("DarkSlateGrey");
                    btnSaveRFUVw = btnSaveRFU.GetButtonCell();
                    
                    UIButton btnSaveRFUButton = new UIButton();
                    btnSaveRFUButton = btnSaveRFU.GetButton();
                    btnSaveRFUButton.TouchUpInside += (sender,e) => {SaveThisSection(sender, e);};
                    
                    arrItems14[1] = btnSaveRFUVw;
                    
                    iUtils.CreateFormGridItem btnExpandRFU = new iUtils.CreateFormGridItem();
                    UIView btnExpandRFUVw = new UIView();
                    btnExpandRFU.SetDimensions(700f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                    btnExpandRFU.SetLabelText("+");
                    btnExpandRFU.SetBorderWidth(0.0f);
                    btnExpandRFU.SetFontName("Verdana");
                    btnExpandRFU.SetFontSize(12f);
                    btnExpandRFU.SetTag(iExpandSectionBtnTagId * (ii+1));
                    btnExpandRFU.SetCellColour("DarkSlateGrey");
                    btnExpandRFUVw = btnExpandRFU.GetButtonCell();
                    
                    UIButton btnExpandRFUButton = new UIButton();
                    btnExpandRFUButton = btnExpandRFU.GetButton();
                    btnExpandRFUButton.Enabled = false;
                    btnExpandRFUButton.TouchUpInside += (sender,e) => {ExpandSection(sender, e);};
                    
                    arrItems14[2] = btnExpandRFUVw;
                    
                    iUtils.CreateFormGridItem btnContractRFU = new iUtils.CreateFormGridItem();
                    UIView btnContractRFUVw = new UIView();
                    btnContractRFU.SetDimensions(750f,0f, 50f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                    btnContractRFU.SetLabelText("-");
                    btnContractRFU.SetBorderWidth(0.0f);
                    btnContractRFU.SetFontName("Verdana");
                    btnContractRFU.SetFontSize(12f);
                    btnContractRFU.SetTag(iContractSectionBtnTagId * (ii+1));
                    btnContractRFU.SetCellColour("DarkSlateGrey");
                    btnContractRFUVw = btnContractRFU.GetButtonCell();
                    
                    UIButton btnContractRFUButton = new UIButton();
                    btnContractRFUButton = btnContractRFU.GetButton();
                    btnContractRFUButton.TouchUpInside += (sender,e) => {ContractSection(sender, e);};
                    
                    arrItems14[3] = btnContractRFUVw;
                    
                    UILabel hfSectionHeight = new UILabel();
                    hfSectionHeight.Tag = iSectionHeightTagId * (ii+1);
                    hfSectionHeight.Hidden = true;
                    hfSectionHeight.Text = "0";
                    arrItems14[4] = hfSectionHeight;
                    
                    UILabel hfSectionRows = new UILabel();
                    hfSectionRows.Tag = iSectionRowsTagId * (ii+1);
                    hfSectionRows.Hidden = true;
                    hfSectionRows.Text = iPwrIdRowsRFU.ToString();
                    arrItems14[5] = hfSectionRows;
                    
                    UILabel hfSectionStatus = new UILabel();
                    hfSectionStatus.Tag = iSectionStatusTagId * (ii+1);
                    hfSectionStatus.Hidden = true;
                    hfSectionStatus.Text = "0";
                    arrItems14[6] = hfSectionStatus;
                    
                    
                    SectionRFURow.AddSubviews(arrItems14);
                    
                    iVert += iSectionHdrRowHeight;
                    iRFURowVertTop += iSectionHdrRowHeight;

                    //Now add a new view to this view to hold another view containing all the pwrid info for this RFU section
                    UIView RFUTableRow = new UIView();
                    RFUTableRow.Frame = new RectangleF(0f,iRFURowVertTop,1000f,iSectionHdrRowHeight);
                    iSectionId = iContainerSectionTagId * (ii+1);
                    RFUTableRow.Tag = iSectionId;
                    layout.AddSubview(RFUTableRow);

                    //Put in the header
                    UIView vwPwrHdrInternalRowId = new UIView();
                    vwPwrHdrInternalRowId.Frame = new RectangleF(0f,iRFURowVert,1000f,iSectionHdrRowHeight); //This will be resized later on

                    iUtils.CreateFormGridItem rowPwrIdHdrLbl = new iUtils.CreateFormGridItem();
                    UIView rowPwrIdHdrLblVw = new UIView();
                    rowPwrIdHdrLbl.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                    rowPwrIdHdrLbl.SetDimensions(0f,iRFURowVert, 100f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                    rowPwrIdHdrLbl.SetLabelText("Power Id");
                    rowPwrIdHdrLbl.SetTextAlignment("centre");
                    rowPwrIdHdrLbl.SetBorderWidth(0.0f);
                    rowPwrIdHdrLbl.SetFontName("Verdana-Bold");
                    rowPwrIdHdrLbl.SetFontSize(14f);
                    rowPwrIdHdrLbl.SetTag((iRFUPwrIdHdrLabelTagId) * (ii+1));                   
                    rowPwrIdHdrLbl.SetCellColour("Pale Yellow");

                    rowPwrIdHdrLblVw = rowPwrIdHdrLbl.GetLabelCell();
                    iHeightToAdd = iSectionHdrRowHeight;
                    arrItems7[0] = rowPwrIdHdrLblVw;

                    iUtils.CreateFormGridItem rowDesignLoadHdrLbl = new iUtils.CreateFormGridItem();
                    UIView rowDesignLoadHdrLblVw = new UIView();
                    rowDesignLoadHdrLbl.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                    rowDesignLoadHdrLbl.SetDimensions(100f,iRFURowVert, 100f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                    rowDesignLoadHdrLbl.SetLabelText("Design Load");
                    rowDesignLoadHdrLbl.SetTextAlignment("centre");
                    rowDesignLoadHdrLbl.SetBorderWidth(0.0f);
                    rowDesignLoadHdrLbl.SetFontName("Verdana-Bold");
                    rowDesignLoadHdrLbl.SetFontSize(14f);
                    rowDesignLoadHdrLbl.SetTag((iRFUDesignLoadHdrLabelTagId) * (ii+1));                   
                    rowDesignLoadHdrLbl.SetCellColour("Pale Yellow");
                    rowDesignLoadHdrLblVw = rowDesignLoadHdrLbl.GetLabelCell();
                    iHeightToAdd = iSectionHdrRowHeight;
                    arrItems7[1] = rowDesignLoadHdrLblVw;

                    iUtils.CreateFormGridItem rowCutoverLoadHdrLbl = new iUtils.CreateFormGridItem();
                    UIView rowCutoverLoadHdrLblVw = new UIView();
                    rowCutoverLoadHdrLbl.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                    rowCutoverLoadHdrLbl.SetDimensions(200f,iRFURowVert, 100f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                    rowCutoverLoadHdrLbl.SetLabelText("Cutover Load");
                    rowCutoverLoadHdrLbl.SetTextAlignment("centre");
                    rowCutoverLoadHdrLbl.SetBorderWidth(0.0f);
                    rowCutoverLoadHdrLbl.SetFontName("Verdana-Bold");
                    rowCutoverLoadHdrLbl.SetFontSize(14f);
                    rowCutoverLoadHdrLbl.SetTag((iRFUCutoverLoadHdrLabelTagId) * (ii+1));                   
                    rowCutoverLoadHdrLbl.SetCellColour("Pale Yellow");
                    
                    rowCutoverLoadHdrLblVw = rowCutoverLoadHdrLbl.GetLabelCell();
                    iHeightToAdd = iSectionHdrRowHeight;
                    arrItems7[2] = rowCutoverLoadHdrLblVw;

                    iUtils.CreateFormGridItem rowCutoverDateHdrLbl = new iUtils.CreateFormGridItem();
                    UIView rowCutoverDateHdrLblVw = new UIView();
                    rowCutoverDateHdrLbl.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                    rowCutoverDateHdrLbl.SetDimensions(300f,iRFURowVert, 100f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                    rowCutoverDateHdrLbl.SetLabelText("Cutover Date");
                    rowCutoverDateHdrLbl.SetTextAlignment("centre");
                    rowCutoverDateHdrLbl.SetBorderWidth(0.0f);
                    rowCutoverDateHdrLbl.SetFontName("Verdana-Bold");
                    rowCutoverDateHdrLbl.SetFontSize(14f);
                    rowCutoverDateHdrLbl.SetTag((iRFUCutoverDateHdrLabelTagId) * (ii+1));                   
                    rowCutoverDateHdrLbl.SetCellColour("Pale Yellow");
                    
                    rowCutoverDateHdrLblVw = rowCutoverDateHdrLbl.GetLabelCell();
                    iHeightToAdd = iSectionHdrRowHeight;
                    arrItems7[3] = rowCutoverDateHdrLblVw;

                    iUtils.CreateFormGridItem rowDecommissionedHdrLbl = new iUtils.CreateFormGridItem();
                    UIView rowDecommissionedHdrLblVw = new UIView();
                    rowDecommissionedHdrLbl.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                    rowDecommissionedHdrLbl.SetDimensions(400f,iRFURowVert, 150f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                    rowDecommissionedHdrLbl.SetLabelText("Decommissioned");
                    rowDecommissionedHdrLbl.SetTextAlignment("centre");
                    rowDecommissionedHdrLbl.SetBorderWidth(0.0f);
                    rowDecommissionedHdrLbl.SetFontName("Verdana-Bold");
                    rowDecommissionedHdrLbl.SetFontSize(14f);
                    rowDecommissionedHdrLbl.SetTag((iRFUDecommissionedHdrLabelTagId) * (ii+1));                   
                    rowDecommissionedHdrLbl.SetCellColour("Pale Yellow");
                    
                    rowDecommissionedHdrLblVw = rowDecommissionedHdrLbl.GetLabelCell();
                    iHeightToAdd = iSectionHdrRowHeight;
                    arrItems7[4] = rowDecommissionedHdrLblVw;

                    iUtils.CreateFormGridItem rowCommissionedHdrLbl = new iUtils.CreateFormGridItem();
                    UIView rowCommissionedHdrLblVw = new UIView();
                    rowCommissionedHdrLbl.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                    rowCommissionedHdrLbl.SetDimensions(550f,iRFURowVert, 150f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                    rowCommissionedHdrLbl.SetLabelText("Commissioned");
                    rowCommissionedHdrLbl.SetTextAlignment("centre");
                    rowCommissionedHdrLbl.SetBorderWidth(0.0f);
                    rowCommissionedHdrLbl.SetFontName("Verdana-Bold");
                    rowCommissionedHdrLbl.SetFontSize(14f);
                    rowCommissionedHdrLbl.SetTag((iRFUCommissionedHdrLabelTagId) * (ii+1));                   
                    rowCommissionedHdrLbl.SetCellColour("Pale Yellow");
                    
                    rowCommissionedHdrLblVw = rowCommissionedHdrLbl.GetLabelCell();
                    iHeightToAdd = iSectionHdrRowHeight;
                    arrItems7[5] = rowCommissionedHdrLblVw;

                    iUtils.CreateFormGridItem rowSaveRFUHdrLbl = new iUtils.CreateFormGridItem();
                    UIView rowSaveRFUHdrLblVw = new UIView();
                    rowSaveRFUHdrLbl.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                    rowSaveRFUHdrLbl.SetDimensions(700f,iRFURowVert, 130f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                    rowSaveRFUHdrLbl.SetLabelText("Commit RFU");
                    rowSaveRFUHdrLbl.SetTextAlignment("centre");
                    rowSaveRFUHdrLbl.SetBorderWidth(0.0f);
                    rowSaveRFUHdrLbl.SetFontName("Verdana-Bold");
                    rowSaveRFUHdrLbl.SetFontSize(14f);
                    rowSaveRFUHdrLbl.SetTag((iRFUSaveRFUHdrLabelTagId) * (ii+1));                   
                    rowSaveRFUHdrLbl.SetCellColour("Pale Yellow");
                    
                    rowSaveRFUHdrLblVw = rowSaveRFUHdrLbl.GetLabelCell();
                    iHeightToAdd = iSectionHdrRowHeight;
                    arrItems7[6] = rowSaveRFUHdrLblVw;

                    //Now add the row details into the view
                    vwPwrHdrInternalRowId.AddSubviews(arrItems7);

                    iSectionRFUHeight += iHeightToAdd;
                    iRFURowVert += iHeightToAdd;
                    iVert += iHeightToAdd;
                    
                    
                    vwPwrHdrInternalRowId.Frame = new RectangleF(0f, iRFURowInnerTop, 1000f, iRFURowVert);
                    RFUTableRow.AddSubview(vwPwrHdrInternalRowId);
                    iRFURowInnerTop += iRFURowVert;
                    iRFURowVert = 0f;


                    for (var j = 0; j < iPwrIdRowsRFU; j++)
                    {
                        UIView vwPwrInternalRowId = new UIView();
                        vwPwrInternalRowId.Frame = new RectangleF(0f,iRFURowVert,1000f,200f); //This will be resized later on
                        vwPwrInternalRowId.Tag = (iPwrIdSectionTagId + (j+1)) * (ii+1);                   
                        
                        iColNo = arrITPRFUs.Tables[0].Columns["PwrId"].Ordinal;
                        string sPwrId = arrITPRFUs.Tables[0].Rows[j].ItemArray[iColNo].ToString();
                        iColNo = arrITPRFUs.Tables[0].Columns["CutoverLoad"].Ordinal;
                        string sCutoverLoad = arrITPRFUs.Tables[0].Rows[j].ItemArray[iColNo].ToString();
                        iColNo = arrITPRFUs.Tables[0].Columns["CutoverDate"].Ordinal;
                        string sCutoverDate = arrITPRFUs.Tables[0].Rows[j].ItemArray[iColNo].ToString();
                        iColNo = arrITPRFUs.Tables[0].Columns["Decommission"].Ordinal;
                        int iDecommission = Convert.ToInt32(arrITPRFUs.Tables[0].Rows[j].ItemArray[iColNo]);
                        iColNo = arrITPRFUs.Tables[0].Columns["Commission"].Ordinal;
                        int iCommission = Convert.ToInt32(arrITPRFUs.Tables[0].Rows[j].ItemArray[iColNo]);

                        if(sCutoverLoad != "0" && sCutoverLoad != "" &&
                           sCutoverDate != "0" && sCutoverDate != "" &&
                           (iDecommission == 1 || iCommission == 1))
                        {
                            bDisableRow = true;
                        }
                        else
                        {
                            bDisableRow = false;
                        }
                        
                        UILabel hfRowRFUStatus = new UILabel();
                        hfRowRFUStatus.Text = "0";
                        hfRowRFUStatus.Tag = (ihfRowRFUStatusTagId + (j+1)) * (ii+1);
                        hfRowRFUStatus.Hidden = true;
                        arrItems6[0] = hfRowRFUStatus;
                        
                        //Put in the PwrId Label
                        iUtils.CreateFormGridItem rowPwrIdLabel = new iUtils.CreateFormGridItem();
                        UIView rowPwrIdLabelVw = new UIView();
                        rowPwrIdLabel.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                        rowPwrIdLabel.SetDimensions(0f,iRFURowVert, 100f, iSectionHdrRowHeight, 2f, 2.5f, 2f, 2.5f);
                        rowPwrIdLabel.SetLabelText(sPwrId);
                        rowPwrIdLabel.SetBorderWidth(0.0f);
                        rowPwrIdLabel.SetFontName("Verdana");
                        rowPwrIdLabel.SetFontSize(14f);
                        rowPwrIdLabel.SetTag((iRFUPwrIdRowLabelTagId + (j+1)) * (ii+1));
                        
                        if (j % 2 == 0)
                        {
                            rowPwrIdLabel.SetCellColour("Pale Blue");
                        }
                        else
                        {
                            rowPwrIdLabel.SetCellColour("Sky Blue");
                        }
                        
                        rowPwrIdLabelVw = rowPwrIdLabel.GetLabelCell();
                        iHeightToAdd = iSectionHdrRowHeight;
                        arrItems6[1] = rowPwrIdLabelVw;
                        
                        //Put in the PwrId Label
                        iUtils.CreateFormGridItem rowDesignLoadLabel = new iUtils.CreateFormGridItem();
                        UIView rowDesignLoadLabelVw = new UIView();
                        iColNo = arrITPRFUs.Tables[0].Columns["DesignLoad"].Ordinal;
                        string sDesignLoad = arrITPRFUs.Tables[0].Rows[j].ItemArray[iColNo].ToString();
                        rowDesignLoadLabel.SetLabelWrap(0); //This means the text will NOT be wrapped in the label
                        rowDesignLoadLabel.SetDimensions(100f,iRFURowVert, 100f, iSectionHdrRowHeight, 20f, 2.5f, 20f, 2.5f);
                        rowDesignLoadLabel.SetLabelText(sDesignLoad);
                        rowDesignLoadLabel.SetTextAlignment("right");
                        rowDesignLoadLabel.SetBorderWidth(0.0f);
                        rowDesignLoadLabel.SetFontName("Verdana");
                        rowDesignLoadLabel.SetFontSize(14f);
                        rowDesignLoadLabel.SetTag((iRFUDesignLoadRowLabelTagId + (j+1)) * (ii+1));
                        
                        if (j % 2 == 0)
                        {
                            rowDesignLoadLabel.SetCellColour("Pale Blue");
                        }
                        else
                        {
                            rowDesignLoadLabel.SetCellColour("Sky Blue");
                        }
                        
                        rowDesignLoadLabelVw = rowDesignLoadLabel.GetLabelCell();
                        iHeightToAdd = iSectionHdrRowHeight;
                        arrItems6[2] = rowDesignLoadLabelVw;

                        iUtils.CreateFormGridItem txtCutoverLoad = new iUtils.CreateFormGridItem();
                        UIView txtCutoverLoadVw = new UIView();
                        txtCutoverLoad.SetDimensions(200f,iRFURowVert, 100f, iSectionHdrRowHeight, 15f, 2.5f, 15f, 2.5f);
                        txtCutoverLoad.SetLabelText(sCutoverLoad);
                        txtCutoverLoad.SetTextAlignment("right");
                        txtCutoverLoad.SetBorderWidth(0.0f);
                        txtCutoverLoad.SetFontName("Verdana");
                        txtCutoverLoad.SetFontSize(14f);
                        txtCutoverLoad.SetTag((iRFUCutoverLoadRowLabelTagId + (j+1)) * (ii+1));
                        
                        if (j % 2 == 0)
                        {
                            txtCutoverLoad.SetCellColour("Pale Blue");
                        }
                        else
                        {
                            txtCutoverLoad.SetCellColour("Sky Blue");
                        }
                        
                        txtCutoverLoadVw = txtCutoverLoad.GetTextFieldCell();
                        UITextField txtCutoverLoadView = txtCutoverLoad.GetTextFieldView();
                        txtCutoverLoadView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                        txtCutoverLoadView.ReturnKeyType = UIReturnKeyType.Next;
                        txtCutoverLoadView.ShouldEndEditing += (sender) => {
                            return ValidateCutoverLoad(sender);};
                        txtCutoverLoadView.ShouldReturn += (sender) => {
                            return MoveNextTextField(sender, 9);};

                        if(bDisableRow)
                        {
                            txtCutoverLoadView.Enabled = false;
                        }
                        arrItems6[3] = txtCutoverLoadVw;

                        iUtils.CreateFormGridItem txtCutoverDate = new iUtils.CreateFormGridItem();
                        UIView txtCutoverDateVw = new UIView();
                        txtCutoverDate.SetDimensions(300f,iRFURowVert, 100f, iSectionHdrRowHeight, 8f, 2.5f, 8f, 2.5f);
                        if (sCutoverDate == "" || sCutoverDate == "0")
                        {
                            sCutoverDate = "01/01/1900";
                        }
                        DateTime dtCutover = Convert.ToDateTime(sCutoverDate);
                        string sCutoverDisplay = dt.Get_Date_String(dtCutover, "dd/mm/yy");
                        txtCutoverDate.SetLabelText(sCutoverDisplay);
                        txtCutoverDate.SetTextAlignment("right");
                        txtCutoverDate.SetBorderWidth(0.0f);
                        txtCutoverDate.SetFontName("Verdana");
                        txtCutoverDate.SetFontSize(14f);
                        txtCutoverDate.SetTag((iRFUCutoverDateRowLabelTagId + (j+1)) * (ii+1));
                        
                        if (j % 2 == 0)
                        {
                            txtCutoverDate.SetCellColour("Pale Blue");
                        }
                        else
                        {
                            txtCutoverDate.SetCellColour("Sky Blue");
                        }
                        
                        txtCutoverDateVw = txtCutoverDate.GetTextFieldCell();
                        UITextField txtCutoverDateView = txtCutoverDate.GetTextFieldView();
                        txtCutoverDateView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
                        txtCutoverDateView.ReturnKeyType = UIReturnKeyType.Next;
                        txtCutoverDateView.ShouldEndEditing += (sender) => {
                            return ValidateCutoverDate(sender);};
                        txtCutoverDateView.ShouldReturn += (sender) => {
                            return MoveNextTextField(sender, 10);};

                        if(bDisableRow)
                        {
                            txtCutoverDateView.Enabled = false;
                        }

                        arrItems6[4] = txtCutoverDateVw;

                        iUtils.CreateFormGridItem chkDecommission = new iUtils.CreateFormGridItem();
                        UIView chkDecommissionVw = new UIView();
                        chkDecommission.SetDimensions(400f,iRFURowVert, 150f, iSectionHdrRowHeight, 30f, 2.5f, 30f, 2.5f);
                        bool bDecommission = false;
                        if (iDecommission > 0)
                        {
                            bDecommission = true;
                        }
                        chkDecommission.SetCheckboxOnOff(bDecommission);
                        chkDecommission.SetBorderWidth(0.0f);
                      

                        chkDecommission.SetSwitchType(2);
                        chkDecommission.SetTag((iRFUDecommissionRowCheckTagId + (j+1)) * (ii+1));
                        
                        if (j % 2 == 0)
                        {
                            chkDecommission.SetCellColour("Pale Blue");
                        }
                        else
                        {
                            chkDecommission.SetCellColour("Sky Blue");
                        }
                        

                        chkDecommissionVw = chkDecommission.GetCheckboxCell();
                        UISwitch chkDecommissionCheck = chkDecommission.GetCheckbox();
                        chkDecommissionCheck.ValueChanged += (sender,e) => {CheckboxChanged(sender, e, 1);};

                        if(bDisableRow)
                        {
                            chkDecommissionCheck.Enabled = false;
                        }

                        arrItems6[5] = chkDecommissionVw;

                        iUtils.CreateFormGridItem chkCommission = new iUtils.CreateFormGridItem();
                        UIView chkCommissionVw = new UIView();
                        chkCommission.SetDimensions(550f,iRFURowVert, 150f, iSectionHdrRowHeight, 30f, 2.5f, 30f, 2.5f);
                        bool bCommission = false;
                        if (iCommission > 0)
                        {
                            bCommission = true;
                        }
                        chkCommission.SetCheckboxOnOff(bCommission);
                        chkCommission.SetBorderWidth(0.0f);
                        chkCommission.SetSwitchType(2);
                        chkCommission.SetTag((iRFUCommissionRowCheckTagId + (j+1)) * (ii+1));
                        
                        if (j % 2 == 0)
                        {
                            chkCommission.SetCellColour("Pale Blue");
                        }
                        else
                        {
                            chkCommission.SetCellColour("Sky Blue");
                        }
                        
                        chkCommissionVw = chkCommission.GetCheckboxCell();
                        UISwitch chkCommissionCheck = chkCommission.GetCheckbox();
                        chkCommissionCheck.ValueChanged += (sender,e) => {CheckboxChanged(sender, e, 2);};

                        if(bDisableRow)
                        {
                            chkCommissionCheck.Enabled = false;
                        }
                        
                        arrItems6[6] = chkCommissionVw;

                        iUtils.CreateFormGridItem btnRFU = new iUtils.CreateFormGridItem();
                        UIView btnRFUVw = new UIView();
                        btnRFU.SetDimensions(700f,iRFURowVert, 130f, iSectionHdrRowHeight, 8f, 4f, 8f, 4f);
                        if(bDisableRow)
                        {
                            btnRFU.SetLabelText("Committed");
                        }
                        else
                        {
                            btnRFU.SetLabelText("Commit RFU");
                        }
                        btnRFU.SetBorderWidth(0.0f);
                        btnRFU.SetFontName("Verdana");
                        btnRFU.SetFontSize(14f);
                        btnRFU.SetTag((iRFUButtonSaveTagId + (j+1)) * (ii+1));

                        if (j % 2 == 0)
                        {
                            btnRFU.SetCellColour("Pale Blue");
                        }
                        else
                        {
                            btnRFU.SetCellColour("Sky Blue");
                        }

                        btnRFUVw = btnRFU.GetButtonCell();
                        
                        UIButton btnRFUButton = new UIButton();
                        btnRFUButton = btnRFU.GetButton();
                        btnRFUButton.TouchUpInside += (sender,e) => {CommitRFU(sender, e);};

                        if(bDisableRow)
                        {
                            btnRFUButton.Enabled = false;
                        }

                        arrItems6[7] = btnRFUVw;
                        

                        iColNo = arrITPRFUs.Tables[0].Columns["BatteryCapacity"].Ordinal;
                        string sBatteryCapacity = arrITPRFUs.Tables[0].Rows[j].ItemArray[iColNo].ToString();
                        UILabel hfRFUBatteryCapacity = new UILabel();
                        hfRFUBatteryCapacity.Text = sBatteryCapacity;
                        hfRFUBatteryCapacity.Tag = (ihfRowRFUBatteryCapacityTagId + (j+1)) * (ii+1);
                        hfRFUBatteryCapacity.Hidden = true;
                        arrItems6[8] = hfRFUBatteryCapacity;
                        
                        //Now add the row details into the view
                        vwPwrInternalRowId.AddSubviews(arrItems6);

                        iSectionRFUHeight += iHeightToAdd;
                        iRFURowVert += iHeightToAdd;
                        iVert += iHeightToAdd;

                            
                        vwPwrInternalRowId.Frame = new RectangleF(0f, iRFURowInnerTop, 1000f, iRFURowVert);
                        RFUTableRow.AddSubview(vwPwrInternalRowId);
                        iRFURowInnerTop += iRFURowVert;
                        iRFURowVert = 0f;

                    }
                    //Now resize the UIView that is effectively the container for the RFU info for this section
                    //And also store this height in a hidden field for use in the contract and expand functions
                    RFUTableRow.Frame = new RectangleF(0f,iRFURowVertTop,1000f,iSectionRFUHeight);
                    hfSectionHeight.Text = iSectionRFUHeight.ToString();
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

                //Now determine what is to be contrated by default
                for(int iiii=0;iiii< m_iSections; iiii++)
                {
                    if(bHideSections[iiii])
                    {
                        UIButton btnContract = (UIButton)View.ViewWithTag (iContractSectionBtnTagId * (iiii+1));

                        ContractSection(btnContract, null);
                    }
                }

//                //Contract all the power conversion PwrIds
//                for(int iiii=0;iiii< m_iEquipmentPwrIds; iiii++)
//                {
//                    UIButton btnContract = (UIButton)View.ViewWithTag ((iPwrIdContractTagId + (iiii+1)) * (m_iEquipmentSectionCounter+1));                        
//                    ContractPwrId(btnContract, null, 2);
//                }



			}
			catch (Exception except)
			{
				string sTest = except.Message.ToString();
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
            lblBlank1.SetLabelText("");
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



		public UIView BuildBatteryStringRowDetails(int iSectionCounterId, int iRowNo, int iStringRow, string sPwrId, 
                                                   int iAutoId, int iMaximoAssetId, string sBankNo,
                                                   string sBankPlane, string sMake, string sModel, string sSPN, string sDOM,
                                                   string sFuseOrCB, string sRatingAmps, string sFloor, string sSuite,
                                                   string sRack, string sSubRack, string sEquipType, string sSerialNo,
                                                   int iLinkTestStatus, int i20MinTest, 
                                                   bool bNewRow, ref float iHeightToAdd)
        {
            DateClass dt = new DateClass();
            iHeightToAdd = 0.0f;
            UIView hdrRow = new UIView();
            float iHdrVert = 0.0f;
            float iRowHeight = 40f;
            UIView[] arrItems = new UIView[18];
            UIView[] arrItems2 = new UIView[15];
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
            txtBankNoView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtBankNoView.ReturnKeyType = UIReturnKeyType.Next;
            txtBankNoView.ShouldEndEditing += (sender) => {
                return ValidateBankNo(sender, 1);};
            txtBankNoView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 1);};

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
            
            arrItems [13] = btnModelSearchVw;

            iUtils.CreateFormGridItem txtDOM = new iUtils.CreateFormGridItem();
            UIView txtDOMVw = new UIView();
            txtDOM.SetDimensions(697f, iHdrVert, 80f, iRowHeight, 2f, 2f, 2f, 2f);
            if (sDOM == "" || sDOM == "0")
            {
                sDOM = "01/01/1900";
            }
            DateTime dtDOM = Convert.ToDateTime(sDOM);
            string sDOMDisplay = dt.Get_Date_String(dtDOM, "dd/mm/yy");
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
            txtDOMTextView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtDOMTextView.ReturnKeyType = UIReturnKeyType.Next;
            txtDOMTextView.ShouldEndEditing += (sender) => {
                return ValidateDOM(sender);};
            txtDOMTextView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 2);};

            arrItems [14] = txtDOMVw;


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
            arrItems [15] = lblBankFuseOrCBVw;
            
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
            
            arrItems [16] = btnFuseOrCBSearchVw;

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
            txtRatingTextView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtRatingTextView.ReturnKeyType = UIReturnKeyType.Next;
            txtRatingTextView.ShouldEndEditing += (sender) => {
                return ValidateRating(sender);};
            txtRatingTextView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 3);};
            
            arrItems [17] = txtRatingVw;

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
            txtFloorView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtFloorView.ReturnKeyType = UIReturnKeyType.Next;
            txtFloorView.ShouldEndEditing += (sender) => {
                return ValidateFloor(sender);};
            txtFloorView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 4);};
            
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
            txtSuiteView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSuiteView.ReturnKeyType = UIReturnKeyType.Next;
            txtSuiteView.ShouldEndEditing += (sender) => {
                return ValidateSuite(sender);};
            txtSuiteView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 5);};
            
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
            txtRackView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtRackView.ReturnKeyType = UIReturnKeyType.Next;
            txtRackView.ShouldEndEditing += (sender) => {
                return ValidateRack(sender);};
            txtRackView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 6);};
            
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
            txtSubRackView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSubRackView.ReturnKeyType = UIReturnKeyType.Next;
            txtSubRackView.ShouldEndEditing += (sender) => {
                return ValidateSubRack(sender);};
            txtSubRackView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 7);};
            
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
            radEquipTypeRadio.TouchUpInside += (sender,e) => {
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
            txtSerialNoView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSerialNoView.ReturnKeyType = UIReturnKeyType.Next;
            txtSerialNoView.ShouldEndEditing += (sender) => {
                return ValidateSerialNo(sender);};
            txtSerialNoView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 8);};
            
            arrItems2 [9] = lblSerialNoVw;

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
            
            arrItems2 [10] = btnLinkTestVw;

            UILabel hfLinkTestStatus = new UILabel();
            hfLinkTestStatus.Text = iLinkTestStatus.ToString();
            hfLinkTestStatus.Tag = iLinkTestHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hfLinkTestStatus.Hidden = true;
            arrItems2 [11] = hfLinkTestStatus;

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
            
            arrItems2 [12] = btn20MinTestVw;

            UILabel hf20MinTestStatus = new UILabel();
            hf20MinTestStatus.Text = i20MinTest.ToString();
            hf20MinTestStatus.Tag = i20MinTestHiddenTagId * (iRowNo + 1) + (iStringRow + 1);
            hf20MinTestStatus.Hidden = true;
            arrItems2 [13] = hf20MinTestStatus;

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
            arrItems2[14] = btnDeleteVw;

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


        public UIView BuildEquipmentItemRowDetails(int iSectionCounterId, int iPwrIdRowNo, int iEquipRowNo, string sPwrId, 
                                                   int iAutoId, int iMaximoAssetId, string sStringNo,
                                                   string sMake, string sModel, string sSPN, string sDOM,
                                                   string sFloor, string sSuite,string sRack, string sSubRack, 
                                                   string sPosition, string sEquipType, string sSerialNo,
                                                   int iEquipmentType,bool bNewRow, ref float iHeightToAdd)
        {
            DateClass dt = new DateClass();
            iHeightToAdd = 0.0f;
            UIView hdrRow = new UIView();
            float iHdrVert = 0.0f;
            float iRowHeight = 40f;
            UIView[] arrItems = new UIView[6];
            UIView[] arrItems2 = new UIView[16];
            UIView[] arrItems3 = new UIView[6];
            
            UILabel hfSectionCounter = new UILabel();
            hfSectionCounter.Text = iSectionCounterId.ToString();
            hfSectionCounter.Tag = iEquipmentRowSectionCounterTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfSectionCounter.Hidden = true;
            arrItems[0] = hfSectionCounter;
            
            UILabel hfPwrId = new UILabel();
            hfPwrId.Text = sPwrId;
            hfPwrId.Tag = iEquipmentRowPwrIdTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfPwrId.Hidden = true;
            arrItems[1] = hfPwrId;
            
            UILabel hfRowStatus = new UILabel();
            if (bNewRow)
            {
                hfRowStatus.Text = "2"; //2 means new
            }
            else
            {
                hfRowStatus.Text = "0";
            }
            
            hfRowStatus.Tag = iStringRowStatusTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfRowStatus.Hidden = true;
            arrItems [2] = hfRowStatus;
            
            UILabel hfAutoId = new UILabel();
            hfAutoId.Text = iAutoId.ToString();
            hfAutoId.Tag = iStringRowAutoIdTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfAutoId.Hidden = true;
            arrItems [3] = hfAutoId;
            
            UILabel hfMaximoAssetId = new UILabel();
            hfMaximoAssetId.Text = iMaximoAssetId.ToString();
            hfMaximoAssetId.Tag = iStringRowMaximoAssetIdTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfMaximoAssetId.Hidden = true;
            arrItems [4] = hfMaximoAssetId;
            


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
            lblFloor.SetTag(iFloorTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            txtFloorView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtFloorView.ReturnKeyType = UIReturnKeyType.Next;
            txtFloorView.ShouldEndEditing += (sender) => {
                return ValidateFloor(sender);};
            txtFloorView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 4);};
            
            arrItems2 [0] = lblFloorVw;
            
            UILabel hfCurrentFloor = new UILabel();
            hfCurrentFloor.Text = sFloor;
            hfCurrentFloor.Tag = iFloorHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentFloor.Hidden = true;
            arrItems2 [1] = hfCurrentFloor;
            
            iUtils.CreateFormGridItem lblSuite = new iUtils.CreateFormGridItem();
            UIView lblSuiteVw = new UIView();
            lblSuite.SetDimensions(50f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSuite.SetLabelText(sSuite);
            lblSuite.SetBorderWidth(0.0f);
            lblSuite.SetFontName("Verdana");
            lblSuite.SetFontSize(12f);
            lblSuite.SetTag(iSuiteTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            txtSuiteView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSuiteView.ReturnKeyType = UIReturnKeyType.Next;
            txtSuiteView.ShouldEndEditing += (sender) => {
                return ValidateSuite(sender);};
            txtSuiteView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 5);};
            
            arrItems2 [2] = lblSuiteVw;
            
            UILabel hfCurrentSuite = new UILabel();
            hfCurrentSuite.Text = sSuite;
            hfCurrentSuite.Tag = iSuiteHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentSuite.Hidden = true;
            arrItems2 [3] = hfCurrentSuite;
            
            iUtils.CreateFormGridItem lblRack = new iUtils.CreateFormGridItem();
            UIView lblRackVw = new UIView();
            lblRack.SetDimensions(100f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblRack.SetLabelText(sRack);
            lblRack.SetBorderWidth(0.0f);
            lblRack.SetFontName("Verdana");
            lblRack.SetFontSize(12f);
            lblRack.SetTag(iRackTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            txtRackView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtRackView.ReturnKeyType = UIReturnKeyType.Next;
            txtRackView.ShouldEndEditing += (sender) => {
                return ValidateRack(sender);};
            txtRackView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 6);};
            
            arrItems2 [4] = lblRackVw;
            
            UILabel hfCurrentRack = new UILabel();
            hfCurrentRack.Text = sRack;
            hfCurrentRack.Tag = iRackHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentRack.Hidden = true;
            arrItems2 [5] = hfCurrentRack;
            
            iUtils.CreateFormGridItem lblSubRack = new iUtils.CreateFormGridItem();
            UIView lblSubRackVw = new UIView();
            lblSubRack.SetDimensions(140f, iHdrVert, 60f, iRowHeight, 2f, 2f, 2f, 2f);
            lblSubRack.SetLabelText(sSubRack);
            lblSubRack.SetBorderWidth(0.0f);
            lblSubRack.SetFontName("Verdana");
            lblSubRack.SetFontSize(12f);
            lblSubRack.SetTag(iSubRackTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            txtSubRackView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSubRackView.ReturnKeyType = UIReturnKeyType.Next;
            txtSubRackView.ShouldEndEditing += (sender) => {
                return ValidateSubRack(sender);};
            txtSubRackView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 7);};
            
            arrItems2 [6] = lblSubRackVw;
            
            UILabel hfCurrentSubRack = new UILabel();
            hfCurrentSubRack.Text = sSubRack;
            hfCurrentSubRack.Tag = iSubRackHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentSubRack.Hidden = true;
            arrItems2 [7] = hfCurrentSubRack;

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
            txtPositionView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtPositionView.ReturnKeyType = UIReturnKeyType.Next;
            txtPositionView.ShouldEndEditing += (sender) => {
                return ValidatePosition(sender);};
            txtPositionView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 7);};
            
            arrItems2 [8] = lblPositionVw;

            UILabel hfCurrentPosition = new UILabel();
            hfCurrentPosition.Text = sPosition;
            hfCurrentPosition.Tag = iEquipmentPositionHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentPosition.Hidden = true;
            arrItems2 [9] = hfCurrentPosition;

            iUtils.CreateFormGridItem lblString = new iUtils.CreateFormGridItem();
            UIView lblStringVw = new UIView();
            lblString.SetDimensions(250f, iHdrVert, 50f, iRowHeight, 2f, 2f, 2f, 2f);
            lblString.SetLabelText(sStringNo);
            lblString.SetBorderWidth(0.0f);
            lblString.SetFontName("Verdana");
            lblString.SetFontSize(12f);
            lblString.SetTag(iEquipmentBankNoTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            txtStringView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtStringView.ReturnKeyType = UIReturnKeyType.Next;
            txtStringView.ShouldEndEditing += (sender) => {
                return ValidateBankNo(sender, 2);};
            txtStringView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 7);};
            
            arrItems2 [10] = lblStringVw;

            UILabel hfCurrentString = new UILabel();
            hfCurrentString.Text = sStringNo;
            hfCurrentString.Tag = iEquipmentBankNoHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfCurrentString.Hidden = true;
            arrItems2 [11] = hfCurrentString;
            

            iUtils.CreateFormGridItem radEquipType = new iUtils.CreateFormGridItem();
            UIView radEquipTypeVw = new UIView();
            radEquipType.SetDimensions(300f, iHdrVert, 200f, iRowHeight * 2, 8f, iRowHeight / 2f, 8f, iRowHeight / 2f);
            radEquipType.SetFontName("Verdana");
            radEquipType.SetFontSize(12f);
            radEquipType.SetTag(iEquipTypeTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            radEquipTypeRadio.TouchUpInside += (sender,e) => {
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
            
            arrItems2 [12] = radEquipTypeVw;
            
            iUtils.CreateFormGridItem txtDOM = new iUtils.CreateFormGridItem();
            UIView txtDOMVw = new UIView();
            txtDOM.SetDimensions(500f, iHdrVert, 80f, iRowHeight * 2, 2f, iRowHeight / 2f, 2f, iRowHeight / 2f);
            if (sDOM == "" || sDOM == "0")
            {
                sDOM = "01/01/1900";
            }
            DateTime dtDOM = Convert.ToDateTime(sDOM);
            string sDOMDisplay = dt.Get_Date_String(dtDOM, "dd/mm/yy");
            txtDOM.SetLabelText(sDOMDisplay);
            txtDOM.SetBorderWidth(0.0f);
            txtDOM.SetFontName("Verdana");
            txtDOM.SetFontSize(12f);
            txtDOM.SetTag(iBankDOMTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            txtDOMTextView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtDOMTextView.ReturnKeyType = UIReturnKeyType.Next;
            txtDOMTextView.ShouldEndEditing += (sender) => {
                return ValidateDOM(sender);};
            txtDOMTextView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 2);};
            
            arrItems2 [13] = txtDOMVw;
            

            iUtils.CreateFormGridItem lblSerialNo = new iUtils.CreateFormGridItem();
            UIView lblSerialNoVw = new UIView();
            lblSerialNo.SetDimensions(580f, iHdrVert, 300f, iRowHeight * 2f, 2f, iRowHeight / 2f, 2f, iRowHeight / 2f);
            lblSerialNo.SetLabelText(sSerialNo);
            lblSerialNo.SetBorderWidth(0.0f);
            lblSerialNo.SetFontName("Verdana");
            lblSerialNo.SetFontSize(12f);
            lblSerialNo.SetTag(iSerialNoTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            txtSerialNoView.KeyboardType = UIKeyboardType.NumbersAndPunctuation;
            txtSerialNoView.ReturnKeyType = UIReturnKeyType.Next;
            txtSerialNoView.ShouldEndEditing += (sender) => {
                return ValidateSerialNo(sender);};
            txtSerialNoView.ShouldReturn += (sender) => {
                return MoveNextTextField(sender, 8);};
            
            arrItems2 [14] = lblSerialNoVw;
            
            iUtils.CreateFormGridItem btnDelete = new iUtils.CreateFormGridItem();
            UIView btnDeleteVw = new UIView();
            btnDelete.SetDimensions(880f, iHdrVert, 120f, iRowHeight * 2f, 8f, iRowHeight / 2f, 8f, iRowHeight / 2f);
            btnDelete.SetLabelText("Delete");
            btnDelete.SetBorderWidth(0.0f);
            btnDelete.SetFontName("Verdana");
            btnDelete.SetFontSize(12f);
            btnDelete.SetTag(iDeleteBatteryStringBtnTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
                DeleteBatteryString(sender, e);};
            
            if (iMaximoAssetId > 0)
            {
                btnDeleteButton.Enabled = false;
            }
            arrItems2[15] = btnDeleteVw;
            
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
            btnFloorSearch.SetTag(iFloorSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
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
            
            arrItems3[0] = btnFloorSearchVw;
            
            iUtils.CreateFormGridItem btnSuiteSearch = new iUtils.CreateFormGridItem();
            UIView btnSuiteSearchVw = new UIView();
            btnSuiteSearch.SetDimensions(50f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
            btnSuiteSearch.SetLabelText("...");
            btnSuiteSearch.SetBorderWidth(0.0f);
            btnSuiteSearch.SetFontName("Verdana");
            btnSuiteSearch.SetFontSize(12f);
            btnSuiteSearch.SetTag(iSuiteSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
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
            
            arrItems3[1] = btnSuiteSearchVw;
            
            iUtils.CreateFormGridItem btnRackSearch = new iUtils.CreateFormGridItem();
            UIView btnRackSearchVw = new UIView();
            btnRackSearch.SetDimensions(100f,iHdrVert, 40f, iRowHeight, 3f, 4f, 3f, 4f);
            btnRackSearch.SetLabelText("...");
            btnRackSearch.SetBorderWidth(0.0f);
            btnRackSearch.SetFontName("Verdana");
            btnRackSearch.SetFontSize(12f);
            btnRackSearch.SetTag(iRackSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
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
            
            arrItems3[2] = btnRackSearchVw;
            
            iUtils.CreateFormGridItem btnSubRackSearch = new iUtils.CreateFormGridItem();
            UIView btnSubRackSearchVw = new UIView();
            btnSubRackSearch.SetDimensions(140f,iHdrVert, 60f, iRowHeight, 13f, 4f, 13f, 4f);
            btnSubRackSearch.SetLabelText("...");
            btnSubRackSearch.SetBorderWidth(0.0f);
            btnSubRackSearch.SetFontName("Verdana");
            btnSubRackSearch.SetFontSize(12f);
            btnSubRackSearch.SetTag(iSubRackSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
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
            
            arrItems3[3] = btnSubRackSearchVw;
            
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
            btnPositionSearchButton.TouchUpInside += (sender,e) => {OpenSearchView(sender, e, 4);};
            
            arrItems3[4] = btnPositionSearchVw;

            iUtils.CreateFormGridItem btnStringSearch = new iUtils.CreateFormGridItem();
            UIView btnStringSearchVw = new UIView();
            btnStringSearch.SetDimensions(250f,iHdrVert, 50f, iRowHeight, 8f, 4f, 8f, 4f);
            btnStringSearch.SetLabelText("...");
            btnStringSearch.SetBorderWidth(0.0f);
            btnStringSearch.SetFontName("Verdana");
            btnStringSearch.SetFontSize(12f);
            btnStringSearch.SetTag(iEquipmentBankNoSearchTagId * (iPwrIdRowNo+1) + (iEquipRowNo+1));
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
            btnStringSearchButton.TouchUpInside += (sender,e) => {OpenSearchView(sender, e, 4);};
            
            arrItems3[5] = btnStringSearchVw;

            hdrRow.AddSubviews(arrItems3);
            
            iHeightToAdd += iRowHeight; 
            iHdrVert += iRowHeight; 
            
            //***************************************************************//
            //              3nd Row                                          //
            //***************************************************************//
            
            iUtils.CreateFormGridItem lblBankMake = new iUtils.CreateFormGridItem();
            UIView lblBankMakeVw = new UIView();
            lblBankMake.SetDimensions(0f, iHdrVert, 340f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBankMake.SetLabelText(sMake);
            lblBankMake.SetBorderWidth(0.0f);
            lblBankMake.SetFontName("Verdana");
            lblBankMake.SetFontSize(12f);
            lblBankMake.SetTag(iBankMakeTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            btnMakeSearch.SetTag(iBankMakeSearchTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
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
            
            arrItems[1] = btnMakeSearchVw;
            
            iUtils.CreateFormGridItem lblBankModel = new iUtils.CreateFormGridItem();
            UIView lblBankModelVw = new UIView();
            lblBankModel.SetDimensions(400f, iHdrVert, 440f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBankModel.SetLabelText(sModel);
            lblBankModel.SetBorderWidth(0.0f);
            lblBankModel.SetFontName("Verdana");
            lblBankModel.SetFontSize(12f);
            lblBankModel.SetTag(iBankModelTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
            
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
            hfSPN.Tag = iSPNHiddenTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1);
            hfSPN.Hidden = true;
            arrItems[3] = hfSPN;
            
            iUtils.CreateFormGridItem btnModelSearch = new iUtils.CreateFormGridItem();
            UIView btnModelSearchVw = new UIView();
            btnModelSearch.SetDimensions(840f, iHdrVert, 60f, iRowHeight, 8f, 4f, 8f, 4f);
            btnModelSearch.SetLabelText("...");
            btnModelSearch.SetBorderWidth(0.0f);
            btnModelSearch.SetFontName("Verdana");
            btnModelSearch.SetFontSize(12f);
            btnModelSearch.SetTag(iBankModelSearchTagId * (iPwrIdRowNo + 1) + (iEquipRowNo + 1));
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
            
            arrItems[4] = btnModelSearchVw;

            iUtils.CreateFormGridItem lblBlank2 = new iUtils.CreateFormGridItem();
            UIView lblBlank2Vw = new UIView();
            lblBlank2.SetDimensions(900f,iHdrVert, 100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
            lblBlank2.SetLabelText("");
            lblBlank2.SetBorderWidth(0.0f);
            lblBlank2.SetFontName("Verdana");
            lblBlank2.SetFontSize(12f);
            lblBlank2.SetTag(iBlank1EquipLabelTagId * (iPwrIdRowNo+1));
            
            if (iPwrIdRowNo % 2 == 0)
            {
                lblBlank2.SetCellColour("Pale Yellow");
            }
            else
            {
                lblBlank2.SetCellColour("Pale Orange");
            }
            
            lblBlank2Vw = lblBlank2.GetLabelCell();
            arrItems[5] = lblBlank2Vw;

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
            UILabel lblViewModel = (UILabel)View.ViewWithTag (iBankModelTagId * (iPwrIdRow) + (iStringRow));
            tabdata.SetMakePostUpdate(1, lblViewModel);

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

            if (sSupplier == "") {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("You must select a make before you can select a model");
                return;
            }

            if (m_sBatteryModels == null) 
            {
                clsTabletDB.ITPInventory ITPInventory = new clsTabletDB.ITPInventory ();
                string[] sBatteryModels = ITPInventory.GetBatteryModels (sSupplier);
                m_sBatteryModels = sBatteryModels;
            }

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
            tabdata.SetModelPostUpdate(6, hfRowStatus, lblSPN, sSupplier);

            //Also set the section flag to 1 that it has changed and the overall flag that it has changed
            UILabel lblUnsavedFlag = (UILabel)View.ViewWithTag (80);
            tabdata.SetUnsavedChangesHiddenLabel(lblUnsavedFlag);
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

        //Here iType means 1 = Batteries, 2 = Solar strings
        public bool ValidateBankNo (object sender, int iType)
        {
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
                hfHiddenBankNo.Text = txtBankNo.Text;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
            }
        }

        public bool ValidateDOM (object sender)
        {
            UITextField txtDOM = (UITextField)sender;
            string sDOM = txtDOM.Text;
            DateClass dt = new DateClass ();
            DateTime dtDOM;
            bool bDateCheck = dt.ValidateDate (sDOM, ref dtDOM);
            int iTagId = txtDOM.Tag;
            int iPwrIdRow =  iTagId/ iBankDOMTagId;
            int iStringRow = iTagId - (iPwrIdRow * iBankDOMTagId);

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
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
            }
        }

        public bool ValidateRating (object sender)
        {
            UITextField txtRating = (UITextField)sender;
            string sRating = txtRating.Text;
            int iTagId = txtRating.Tag;
            int iPwrIdRow =  iTagId/ iBankRatingTagId;
            int iStringRow = iTagId - (iPwrIdRow * iBankRatingTagId);

            string sRatingReturn = Regex.Replace(sRating, @"[^\d]+","");
            txtRating.Text = sRatingReturn;

            UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
            hfRowStatus.Text = "1";
            SetSectionValueChanged(m_iBatterySectionCounter + 1);
            SetAnyValueChanged(sender, null);
            return true;
        }

        public bool ValidateFloor (object sender)
        {
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
                hfHiddenFloor.Text = txtFloor.Text;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
            }
        }

        public bool ValidateSuite (object sender)
        {
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
                hfHiddenSuite.Text = txtSuite.Text;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
            }
        }

        public bool ValidateRack (object sender)
        {
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
                hfHiddenRack.Text = txtRack.Text;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
            }
        }

        public bool ValidateSubRack (object sender)
        {
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
                hfHiddenSubRack.Text = txtSubRack.Text;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iBatterySectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
            }
        }

        public bool ValidatePosition (object sender)
        {
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
            
            if (!bPositionCheck) 
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
                hfHiddenPosition.Text = txtPosition.Text;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag(iEquipmentRowStatusTagId * iPwrIdRow + iStringRow);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iEquipmentSectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
            }
        }

        public bool ValidateCutoverLoad (object sender)
        {
            UITextField txtCutoverLoad = (UITextField)sender;
            string sRating = txtCutoverLoad.Text;
            int iTagId = txtCutoverLoad.Tag;
            int iSection =  m_iRFUSectionCounter + 1;
            int iStringRow = iTagId/iSection - iRFUCutoverLoadRowLabelTagId;
            
            string sLoadReturn = Regex.Replace(sRating, @"[^\d]+","");
            txtCutoverLoad.Text = sLoadReturn;
            
            UILabel hfRowStatus = (UILabel)View.ViewWithTag((ihfRowRFUStatusTagId + iStringRow) * iSection);
            hfRowStatus.Text = "1";
            SetSectionValueChanged(m_iRFUSectionCounter + 1);
            SetAnyValueChanged(sender, null);
            return true;
        }

        public bool ValidateCutoverDate (object sender)
        {
            UITextField txtCODate = (UITextField)sender;
            string sCODate = txtCODate.Text;
            DateClass dt = new DateClass ();
            DateTime dtCO;
            bool bDateCheck = dt.ValidateDate (sCODate, ref dtCO);
            int iTagId = txtCODate.Tag;
            int iSection =  m_iRFUSectionCounter + 1;
            int iStringRow = iTagId/iSection - iRFUCutoverDateRowLabelTagId;

            if (!bDateCheck) 
            {
                iUtils.AlertBox alert = new iUtils.AlertBox ();
                alert.CreateErrorAlertDialog ("Please enter a valid date for the cutover date");
                txtCODate.ResignFirstResponder();
                txtCODate.BecomeFirstResponder();
                m_bSuppressMove = true;
                return false;
            } 
            else 
            {
                string sCOReturn = dt.Get_Date_String(dtCO, "dd/mm/yy");
                txtCODate.Text = sCOReturn;
                UILabel hfRowStatus = (UILabel)View.ViewWithTag((ihfRowRFUStatusTagId + iStringRow) * iSection);
                hfRowStatus.Text = "1";
                SetSectionValueChanged(m_iRFUSectionCounter + 1);
                SetAnyValueChanged(sender, null);
                return true;
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

        public bool ValidateSerialNo(object sender)
        {
            UITextField txtSerialNo = (UITextField)sender;
            string sSerialNo = txtSerialNo.Text;
            int iTagId = txtSerialNo.Tag;
            int iPwrIdRow =  iTagId/ iSerialNoTagId;
            int iStringRow = iTagId - (iPwrIdRow * iSerialNoTagId);
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
            UILabel hfRowStatus = (UILabel)View.ViewWithTag(iStringRowStatusTagId * iPwrIdRow + iStringRow);
            hfRowStatus.Text = "1";
            SetSectionValueChanged(m_iBatterySectionCounter + 1);
            SetAnyValueChanged(sender, null);
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
                case 2:
                    jSectionType = m_iEquipmentSectionCounter;
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

            if(iSectionType == 1)
            {
                //Move up the Equipment block including the header
                int iSectionEquipmentHdrId = (m_iEquipmentSectionCounter + 1) * iSectionTagId;
                UIView vwSectionEquip2 = View.ViewWithTag (iSectionEquipmentHdrId);
                RectangleF frameEquip3 = vwSectionEquip2.Frame;
                frameEquip3.Y -= iHeightToReduce;
                vwSectionEquip2.Frame = frameEquip3;
                
                int iSectionEquipmentId = (m_iEquipmentSectionCounter + 1) * iContainerSectionTagId;
                UIView vwSectionEquip3 = View.ViewWithTag (iSectionEquipmentId);
                RectangleF frameEquip4 = vwSectionEquip3.Frame;
                frameEquip4.Y -= iHeightToReduce;
                vwSectionEquip3.Frame = frameEquip4;
            }

            //Move up the RFU block including the header
            int iSectionRFUHdrId = (m_iRFUSectionCounter + 1) * iSectionTagId;
            UIView vwSection2 = View.ViewWithTag (iSectionRFUHdrId);
            RectangleF frame3 = vwSection2.Frame;
            frame3.Y -= iHeightToReduce;
            vwSection2.Frame = frame3;

            int iSectionRFUId = (m_iRFUSectionCounter + 1) * iContainerSectionTagId;
            UIView vwSection3 = View.ViewWithTag (iSectionRFUId);
            RectangleF frame4 = vwSection3.Frame;
            frame4.Y -= iHeightToReduce;
            vwSection3.Frame = frame4;


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
                case 2:
                    jSectionType = m_iEquipmentSectionCounter;
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
            UIButton btnNewString = (UIButton)View.ViewWithTag((iPwrIdNewBtnTagId + iPwrIdRow) * (jSectionType + 1));
            btnNewString.Enabled = true;
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
                case 2:
                    jSectionType = m_iEquipmentSectionCounter;
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
                                                                   "","","N","", 0, 0, true, ref iHeightToAdd);
            //Get the position of the last row in this internal pwrId battery block
            UIView vwPwrInternalRowId = (UIView)View.ViewWithTag((iPwrIdSectionTagId + (iPwrIdRow)) * (m_iBatterySectionCounter+1));
            float iPwrIdRowVert = vwPwrInternalRowId.Frame.Height;
            BatteryStringRow.Frame = new RectangleF(0f, iPwrIdRowVert, 1000f, iHeightToAdd);
            BatteryStringRow.Tag = iStringFullRowTagId * (iPwrIdRow) + (iTotalStrings + 1);
            vwPwrInternalRowId.AddSubview(BatteryStringRow);

            //Now increase the number of strings in the PwrId by 1
            iTotalStrings++;
            hfThisPwrIdStringRows.Text = iTotalStrings.ToString();
            ReduceHeightAfter(-iHeightToAdd, iPwrIdRow, iTotalStrings, 1);

            //Set the unsaved tags on
            SetSectionValueChanged(m_iBatterySectionCounter + 1);
            SetAnyValueChanged(sender, null);

            //And move to the position
            UIScrollView scrollVw = (UIScrollView)View.ViewWithTag (2);
            UIView vwPwrId = (UIView)View.ViewWithTag(iContainerSectionTagId * (m_iBatterySectionCounter+1));
            float iTotalPosn = iPwrIdRowVert + scrollVw.ContentOffset.Y;
            PointF posn = new PointF(0f, iTotalPosn);
            scrollVw.SetContentOffset(posn, true);


        }

        public void AddNewEquipment (object sender, EventArgs e)
        {

        }
		public void SavePwrIdInfo (object sender, EventArgs e)
		{
			UIButton btnSave = (UIButton)sender;
			int iBtnId = btnSave.Tag;
			int iSectionId = iSectionTagId * (iBtnId/iSaveSectionBtnTagId);
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

        public bool CheckboxChanged(object sender, EventArgs e, int iCheckboxIndex)
        {
            UISwitch checkbox = (UISwitch)sender;
            int iTagId = checkbox.Tag;
            int iSection =  m_iRFUSectionCounter + 1;
            int iStringRow = 1;

            switch(iCheckboxIndex)
            {
                case 1:
                    iStringRow = iTagId/iSection - iRFUDecommissionRowCheckTagId;
                    break;

                case 2:
                    iStringRow = iTagId/iSection - iRFUCommissionRowCheckTagId;
                    break;
            }

            UILabel hfRowStatus = (UILabel)View.ViewWithTag((ihfRowRFUStatusTagId + iStringRow) * iSection);
            hfRowStatus.Text = "1";
            SetSectionValueChanged(m_iRFUSectionCounter + 1);
            SetAnyValueChanged(sender, null);
            return true;
        }

        public bool CommitRFU(object sender, EventArgs e)
        {
            UIButton btnRFUSave = (UIButton)sender;
            int iTagId = btnRFUSave.Tag;
            int iSectionId = m_iRFUSectionCounter + 1;
            int iStringRow = iTagId / iSectionId - iRFUButtonSaveTagId;
            bool bReturn = false;

            //Before we do anything we must check if the RFU is complete
            if (RFUComplete(iStringRow))
            {
                bReturn = SaveRFURow(m_sPassedId, iStringRow - 1, true);
            }
            else
            {
                bReturn = false;
            }

            return bReturn;
        }

        public bool RFUComplete(int iStringRow)
        {
            int iSectionId = m_iRFUSectionCounter;
            bool bQuestion = QuestionsComplete();
            UILabel lblPwrId = (UILabel)View.ViewWithTag((iRFUPwrIdRowLabelTagId + iStringRow) * (iSectionId+1));
            string sPwrId = lblPwrId.Text;
            bool bBatteries = BatteryPwrIdComplete(sPwrId);

            if (!bQuestion && !bBatteries)
            {
                iUtils.AlertBox alert = new iUtils.AlertBox();
                alert.CreateErrorAlertDialog("Some battery information on PwrId " + sPwrId + " is incomplete and not all questions have been answered. You cannot commit the RFU at this stage.");
                return false;
            }

            if (!bBatteries)
            {
                iUtils.AlertBox alert = new iUtils.AlertBox();
                alert.CreateErrorAlertDialog("Some battery information on PwrId " + sPwrId + " is incomplete. You cannot commit the RFU at this stage.");
                return false;
            }

            if (!bQuestion)
            {
                iUtils.AlertBox alert = new iUtils.AlertBox();
                alert.CreateErrorAlertDialog("Not all questions have been answered. You cannot commit the RFU at this stage.");
                return false;
            }

            return true;

        }

        public bool QuestionsComplete()
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectQuestionsFullyAnswered(m_sPassedId);
        }

        public bool BatteryPwrIdComplete(string sPwrId)
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10PwrIdComplete(m_sPassedId, sPwrId);
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
			return SaveSection(iBtnId);
		}

		public bool SaveSection (int iBtnId)
        {
            UILabel hfSectionsRows = (UILabel)View.ViewWithTag (iBtnId / iSaveSectionBtnTagId * iSectionRowsTagId);
            int iRows = Convert.ToInt32 (hfSectionsRows.Text);
            int i;
            int iAnswer = 2;
            int iAnswerIndex;
            string sAnswer;
            string sComments;
            int iSectionNo = iBtnId / iSaveSectionBtnTagId;

            if (iSectionNo == m_iBatterySectionCounter + 1) 
            {
                return SaveBatterySection(iBtnId);

            }

            if (iSectionNo == m_iRFUSectionCounter + 1) 
            {
                return SaveRFUSection(iBtnId);
                
            }

            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
			UILabel hfDBSectionId = (UILabel)View.ViewWithTag (iSectionDBIdTagId * iSectionNo);
			int iDBSectionId = Convert.ToInt32(hfDBSectionId.Text);

			for (i = 0; i < iRows; i++) 
			{
				UILabel hfRowStatus = (UILabel)View.ViewWithTag ((ihfRowStatusTagId + (i+1)) * iSectionNo);
				int iRowStatus = Convert.ToInt32(hfRowStatus.Text);
				
				if (iRowStatus == 1)
				{
					UISegmentedControl radGrp = (UISegmentedControl)View.ViewWithTag ((iAnswerGroupTagId + (i+1)) * iSectionNo);
					iAnswerIndex = radGrp.SelectedSegment;  
					if(iAnswerIndex >=0)
					{
						sAnswer = radGrp.TitleAt(iAnswerIndex);
					}
					else
					{
						sAnswer = "";
					}

					switch(sAnswer)
					{
						case "Yes":
							iAnswer = 0;
							break;
						case "No":
							iAnswer = 1;
							break;
						case "N/A":
							iAnswer = 2;
							break;
						default:
							iAnswer = -1;
							break;
					}

					UITextView txtComments = (UITextView)View.ViewWithTag ((iCommentsTagId + (i+1)) * iSectionNo);
					sComments = txtComments.Text;
					UILabel hfAutoId = (UILabel)View.ViewWithTag ((ihfAutoRowTagId + (i+1)) * iSectionNo);
					int iAutoId = Convert.ToInt32(hfAutoId.Text);
					DBQ.SetLocalITPSectionQuestion(iAutoId, m_sPassedId, iDBSectionId, iAnswer, sComments);
					hfRowStatus.Text = "0";
				}
			}

			UILabel hfSectionStatus = (UILabel)View.ViewWithTag (iSectionStatusTagId * iSectionNo);
			hfSectionStatus.Text = "0";
			SetAnyValueChangedOff();
			return true;
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
                        if (txtDOM.Text == "" || txtDOM.Text == "0")
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
                            case "User":
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
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveRFUSection(int iBtnId)
        {
            int i;
            string sId = m_sPassedId;

            //Get the number of PwrId's
            UILabel hfSectionPwrIds = (UILabel)View.ViewWithTag(iSectionRowsTagId * (m_iRFUSectionCounter + 1));
            int iTotalPwrIds = Convert.ToInt32(hfSectionPwrIds.Text);
            bool bResetSectionFlag = true;
            
            for (i=0; i<iTotalPwrIds; i++)
            {
                //For each RFU  block in this PwrId save it if necessary
                UILabel hfRFURowStatus = (UILabel)View.ViewWithTag((ihfRowRFUStatusTagId + (i+1)) * (m_iRFUSectionCounter+1));
                int iRowStatus = Convert.ToInt32(hfRFURowStatus.Text);
                    
                if (iRowStatus == 1)
                {
                    SaveRFURow(sId, i, false);
                }
                    

            }
            //Reset the section flag
            if (bResetSectionFlag)
            {
                UILabel hfSectionStatus = (UILabel)View.ViewWithTag(iSectionStatusTagId * (m_iRFUSectionCounter + 1));
                hfSectionStatus.Text = "0";
                SetAnyValueChangedOff();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SaveRFURow(string sId, int iRow, bool bCheckSectionStatus)
        {
            clsTabletDB.ITPDocumentSection DB = new clsTabletDB.ITPDocumentSection();
            string[] sItemValues = new string[9];
            int i;

            UILabel hfRFURowStatus = (UILabel)View.ViewWithTag((ihfRowRFUStatusTagId + (iRow + 1)) * (m_iRFUSectionCounter + 1));
            int iRowStatus = Convert.ToInt32(hfRFURowStatus.Text);
            UILabel lblPwrId = (UILabel)View.ViewWithTag((iRFUPwrIdRowLabelTagId + (iRow + 1)) * (m_iRFUSectionCounter + 1));
            string sPwrId = lblPwrId.Text;
            UILabel lblDesignLoad = (UILabel)View.ViewWithTag((iRFUDesignLoadRowLabelTagId + (iRow + 1)) * (m_iRFUSectionCounter + 1));
            string sDesignLoad = lblDesignLoad.Text;
            UITextField txtCutoverLoad = (UITextField)View.ViewWithTag((iRFUCutoverLoadRowLabelTagId + (iRow + 1)) * (m_iRFUSectionCounter + 1));
            string sCutoverLoad = txtCutoverLoad.Text;
            UITextField txtCutoverDate = (UITextField)View.ViewWithTag((iRFUCutoverDateRowLabelTagId + (iRow + 1)) * (m_iRFUSectionCounter + 1));
            string sCutoverDate = txtCutoverDate.Text;
            UISwitch chkDecommission = (UISwitch)View.ViewWithTag((iRFUDecommissionRowCheckTagId + (iRow + 1)) * (m_iRFUSectionCounter + 1));
            bool bDecommission = chkDecommission.On;
            int iDecommission;
            if (bDecommission)
            {
                iDecommission = 1;
            }
            else
            {
                iDecommission = 0;
            }
            UISwitch chkCommission = (UISwitch)View.ViewWithTag((iRFUCommissionRowCheckTagId + (iRow + 1)) * (m_iRFUSectionCounter + 1));
            bool bCommission = chkCommission.On;
            int iCommission;
            if (bCommission)
            {
                iCommission = 1;
            }
            else
            {
                iCommission = 0;
            }
            string sCurrentDateAndTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            UILabel hfBatteryCapacity = (UILabel)View.ViewWithTag((ihfRowRFUBatteryCapacityTagId + (iRow + 1)) * (m_iRFUSectionCounter + 1));
            string sBatteryCapacity = hfBatteryCapacity.Text;

//            string[] colRFUItemsNames = { "ID", "PWRID", "DesignLoad", "CutoverLoad", "CutoverDate", "Decommission", 
//                "Commission", "Audit_DateStamp", "BatteryCapacity"};
            
            //Get all the info for this RFU row
            sItemValues [0] = sId;
            sItemValues [1] = sPwrId;
            sItemValues [2] = sDesignLoad;
            sItemValues [3] = sCutoverLoad;
            sItemValues [4] = sCutoverDate;
            sItemValues [5] = iDecommission.ToString();
            sItemValues [6] = iCommission.ToString();
            sItemValues [7] = sCurrentDateAndTime;
            sItemValues [8] = sBatteryCapacity;

            if (sCutoverLoad == "" || sCutoverDate == "" || (iDecommission == 0 && iCommission == 0) ||
                (iDecommission == 1 && iCommission == 1))
            {
                iUtils.AlertBox alert = new iUtils.AlertBox();
                alert.CreateAlertDialog();
                alert.SetAlertMessage("The Power Id " + sPwrId + " RFU info is not complete. You cannot commit the RFU at this stage.");
                alert.ShowAlertBox(); 
                return false;
            }
            //Update or insert into the local DB
            if (DB.ITPRFUSetRecord(sId, sPwrId, sItemValues))
            {
                //Update the row status
                hfRFURowStatus.Text = "0";
            }
            else
            {
                return false;
            }

            //Now check to see if the section should be marked as complete and ultimately the whole page
            if (bCheckSectionStatus)
            {
                UILabel hfSectionPwrIds = (UILabel)View.ViewWithTag(iSectionRowsTagId * (m_iRFUSectionCounter + 1));
                int iTotalPwrIds = Convert.ToInt32(hfSectionPwrIds.Text);
                bool bResetSectionFlag = true;

                for (i=0; i<iTotalPwrIds; i++)
                {
                    UILabel hfRFURowStatus1 = (UILabel)View.ViewWithTag((ihfRowRFUStatusTagId + (i + 1)) * (m_iRFUSectionCounter + 1));
                    int iRowStatus1 = Convert.ToInt32(hfRFURowStatus1.Text);
                    if(iRowStatus1 == 1)
                    {
                        bResetSectionFlag = false;
                        break;
                    }

                }

                if(bResetSectionFlag)
                {
                    UILabel hfSectionStatus = (UILabel)View.ViewWithTag(iSectionStatusTagId * (m_iRFUSectionCounter + 1));
                    hfSectionStatus.Text = "0";
                    SetAnyValueChangedOff();
                    return true;
                }
            }

            return true;
        }

        public void SetRowRadioChanged(object sender, EventArgs e)
		{
			UISegmentedControl radGrp = (UISegmentedControl)sender;
			int iSenderId = radGrp.Tag;
			int iSectionNo = iSenderId /iAnswerGroupTagId; //This it the number of the section (1 based)
			int iRowNo = (iSenderId - (iSectionNo * iAnswerGroupTagId))/iSectionNo; //This is the row number in the section (1 based)
			int iStatusId = (ihfRowStatusTagId + iRowNo) * iSectionNo;
			UILabel txtRowStatus = (UILabel)View.ViewWithTag (iStatusId);
			txtRowStatus.Text = "1";
			SetSectionValueChanged(iSectionNo);
			SetAnyValueChanged(sender, e);
		}

		public void SetRowEditTextChanged(object sender, EventArgs e)
		{
			UITextView edtText = (UITextView)sender;
			int iSenderId = edtText.Tag;
			int iSectionNo = iSenderId /iCommentsTagId; //This it the number of the section (1 based)
			int iRowNo = (iSenderId - (iSectionNo * iCommentsTagId))/iSectionNo; //This is the row number in the section (1 based)
			int iStatusId = (ihfRowStatusTagId + iRowNo) * iSectionNo;
			UILabel txtRowStatus = (UILabel)View.ViewWithTag (iStatusId);
			txtRowStatus.Text = "1";
			SetSectionValueChanged(iSectionNo);
			SetAnyValueChanged(sender, e);
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
		}

		public void CheckUnsaved ()
		{
			UILabel txtEditStatus = (UILabel)View.ViewWithTag (80);
			int iStatus = Convert.ToInt32 (txtEditStatus.Text);
			if (iStatus == 0) 
			{
				DownloadedITPsScreen downloadScreen = new DownloadedITPsScreen ();
				downloadScreen = GetDownloadedITPsScreen ();
				this.NavigationController.PopToViewController (downloadScreen, true);
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
			DownloadedITPsScreen downloadScreen = new DownloadedITPsScreen ();
			downloadScreen = GetDownloadedITPsScreen ();
			switch (iBtnIndex) 
			{
			case 0:
				SaveAllSections();
				this.NavigationController.PopToViewController (downloadScreen, true);
				break;
			case 1:
				this.NavigationController.PopToViewController (downloadScreen, true);
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
                case 9:
                    iTextTagId = iRFUCutoverLoadRowLabelTagId;
                    break;
                case 10:
                    iTextTagId = iRFUCutoverDateRowLabelTagId;
                    break;
            }

            int iPwrIdRow;
            int iStringRow;
            UILabel hfPwrIdStringRows;
            int iTotalStringRows;
            if (iTextFieldIndex <= 8)
            {
                iPwrIdRow = iTagId / iTextTagId;
                iStringRow = iTagId - (iPwrIdRow * iTextTagId);
                iSectionCounterId = m_iBatterySectionCounter;
                hfPwrIdStringRows = (UILabel)View.ViewWithTag((ihfPwrIdStringRowsTagId + iPwrIdRow) * (iSectionCounterId + 1)); 
                iTotalStringRows = Convert.ToInt32(hfPwrIdStringRows.Text);
            }
            else
            {
                iSectionCounterId = m_iRFUSectionCounter;
                iPwrIdRow = iTagId / iTextTagId;
                iStringRow = iTagId/iPwrIdRow - iTextTagId;
                hfPwrIdStringRows = (UILabel)View.ViewWithTag((iSectionRowsTagId) * iPwrIdRow); 
                iTotalStringRows = Convert.ToInt32(hfPwrIdStringRows.Text);
            }
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

                case 9: //Coming from Cutover Load to Cutover Date
                    txtNext = (UITextField)View.ViewWithTag ((iRFUCutoverDateRowLabelTagId + iStringRow) * iPwrIdRow);
                    break;
                case 10: //Coming from Cutover Date to Cutover Load
                    if(m_bSuppressMove) //This is required on the validate because the endediting and return delegates both fire
                    {
                        m_bSuppressMove = false;
                        return false;
                    }
                    
                    //Make sure we are not on the last RFU PwrId because there is no extra row to go to so go to the first one again
                    if((iStringRow + 1) > iTotalStringRows)
                    {
                        txtNext = (UITextField)View.ViewWithTag ((iRFUCutoverLoadRowLabelTagId + 1) * iPwrIdRow); //Cycle back to the first row
                    }
                    else
                    {
                        txtNext = (UITextField)View.ViewWithTag ((iRFUCutoverLoadRowLabelTagId + iStringRow + 1) * iPwrIdRow); //Go to the next string, hence the + 1 here
                    }
                    break;
                    

            }

            txtNext.BecomeFirstResponder();

            return true;
        }

        public void OpenBatteries(object sender, EventArgs e)
        {
            //Show the progress indicator and position at top left of button
            UIButton btnOpen = (UIButton)sender;
            prog.SetActivityIndicatorTitle("Open Batteries");
            ScreenUtils scnUtils = new ScreenUtils();
            scnUtils.GetAbsolutePosition(btnOpen);
            float iTop = scnUtils.GetPositionTop();
            float iLeft = scnUtils.GetPositionLeft();
            prog.SetActivityIndicatorPosition(iLeft,iTop);
            prog.ShowActivityIndicator();
            prog.StartAnimating();
            
            //Disable all other buttons
//            DisableButtons();
            
            taskA = new Task (() => OpenBatteriesTask (sender, e));
            taskA.Start ();
        }

        public void OpenBatteriesTask(object sender, EventArgs e)
        {
            this.InvokeOnMainThread(() => 
                                    {
                Battery projBatteryScreen = new Battery();
                this.NavigationController.PushViewController(projBatteryScreen, true);
                prog.StopAnimating();
                prog.CloseActivityIndicator();
                //ReEnableButtons();
            });
        }

        public void OpenPowerConversion(object sender, EventArgs e)
        {
            //Show the progress indicator and position at top left of button
            UIButton btnOpen = (UIButton)sender;
            prog.SetActivityIndicatorTitle("Open Pwr Conv");
            ScreenUtils scnUtils = new ScreenUtils();
            scnUtils.GetAbsolutePosition(btnOpen);
            float iTop = scnUtils.GetPositionTop();
            float iLeft = scnUtils.GetPositionLeft();
            prog.SetActivityIndicatorPosition(iLeft,iTop);
            prog.ShowActivityIndicator();
            prog.StartAnimating();
            
            //Disable all other buttons
            //            DisableButtons();
            
            taskA = new Task (() => OpenPowerConversionTask (sender, e));
            taskA.Start ();
        }
        
        public void OpenPowerConversionTask(object sender, EventArgs e)
        {
            this.InvokeOnMainThread(() => 
                                    {
                PowerConversion projPwrConvScreen = new PowerConversion();
                this.NavigationController.PushViewController(projPwrConvScreen, true);
                prog.StopAnimating();
                prog.CloseActivityIndicator();
                //ReEnableButtons();
            });
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

