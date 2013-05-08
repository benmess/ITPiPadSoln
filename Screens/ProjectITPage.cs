
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

        //Set the tag id constants.
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
        int iSectionCompleteLabelTagId = 10600;

        //Tags for Battery Section
        int iPwrIdSectionTagId = 10010800;
        int iStringFullRowTagId = 10010900;
        int iPwrIdNewBtnTagId = 10011300;
        int ihfPwrIdStringRowsTagId = 10011400;

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


        string m_sSessionId = "";
		string m_sPassedId = "";
		string m_sProjDesc = "";
		int m_iSections = 0;
        int m_iQuestionSections = 0;
        int m_iBatterySectionCounter = 0;
        int m_iEquipmentSectionCounter = 0;
        int m_iRFUSectionCounter = 0;
        bool m_bSuppressMove = false;

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
                DateClass dt = new DateClass();
                int iColNo = 0;
				int iSectionId = 0;
				bool bYes;
				bool bNo;
				bool bNA;
				string sId = m_sPassedId;
				float iVert = 0f;
				float iSectionHdrRowHeight = 40f;
				float iQuestionHdrRowHeight = 20f;
				float iQuestionRowHeight = 30f;
				float iQuestionRowVert = 0f;
				float iTotalHeight = 0f;
				float iHeightToAdd = iQuestionRowHeight;
				float iHeightToAdd2 = iHeightToAdd;
                bool bDisableRow = false;
                bool bHideComplete = true;
                bool[] bHideSections = new bool[1];
                UIView[] arrItems = new UIView[4];
				UIView[] arrItems2 = new UIView[6];
				UIView[] arrItems3 = new UIView[5];
                UIView[] arrItems4 = new UIView[6];
                UIView[] arrItems6 = new UIView[9];
                UIView[] arrItems7 = new UIView[7];
                UIView[] arrItems14 = new UIView[7];

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
                    m_iQuestionSections = iRows;
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
					Section10.SetDimensions(0f,0f, 300f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
					Section10.SetLabelText("BATTERIES");
					Section10.SetBorderWidth(0.0f);
					Section10.SetFontName("Verdana-Bold");
					Section10.SetTextColour("White");
					Section10.SetFontSize(12f);
					Section10.SetCellColour("DarkSlateGrey");
					Section10.SetTag(iSectionDescTagId * (ii+1));
					Section10Vw = Section10.GetLabelCell();
					arrItems4[0] = Section10Vw;
					
					
                    if(BatteryFullyComplete())
                    {
                        bHideComplete = false;
                    }
                    else
                    {
                        bHideComplete = true;
                    }

                    iUtils.CreateFormGridItem SectionCompleteLabel = new iUtils.CreateFormGridItem();
                    UIView SectionCompleteLabelVw = new UIView();
                    SectionCompleteLabel.SetDimensions(300f,0f, 150f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
                    SectionCompleteLabel.SetLabelText("COMPLETED");
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
					
					arrItems4[2] = btnSave10Vw;
					
					UILabel hfSectionHeight = new UILabel();
					hfSectionHeight.Tag = iSectionHeightTagId * (ii+1);
					hfSectionHeight.Hidden = true;
					hfSectionHeight.Text = "0";
					arrItems4[3] = hfSectionHeight;
					
					UILabel hfSectionRows = new UILabel();
					hfSectionRows.Tag = iSectionRowsTagId * (ii+1);
					hfSectionRows.Hidden = true;
					hfSectionRows.Text = iPwrIdRows.ToString();
					arrItems4[4] = hfSectionRows;
					
					UILabel hfSectionStatus = new UILabel();
					hfSectionStatus.Tag = iSectionStatusTagId * (ii+1);
					hfSectionStatus.Hidden = true;
					hfSectionStatus.Text = "0";
					arrItems4[5] = hfSectionStatus;
					

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
                    SectionEquipment.SetDimensions(0f,0f, 300f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
                    SectionEquipment.SetLabelText("POWER CONVERSION EQUIPMENT");
                    SectionEquipment.SetBorderWidth(0.0f);
                    SectionEquipment.SetFontName("Verdana-Bold");
                    SectionEquipment.SetTextColour("White");
                    SectionEquipment.SetFontSize(12f);
                    SectionEquipment.SetCellColour("DarkSlateGrey");
                    SectionEquipment.SetTag(iSectionDescTagId * (iii+1));
                    SectionEquipmentVw = SectionEquipment.GetLabelCell();
                    arrItems4[0] = SectionEquipmentVw;
                    
                    
                    if(PowerConversionFullyComplete())
                    {
                        bHideComplete = false;
                    }
                    else
                    {
                        bHideComplete = true;
                    }
                    
                    iUtils.CreateFormGridItem SectionCompleteLabel = new iUtils.CreateFormGridItem();
                    UIView SectionCompleteLabelVw = new UIView();
                    SectionCompleteLabel.SetDimensions(300f,0f, 150f, iSectionHdrRowHeight, 4f, 7.5f, 4f, 7.5f);
                    SectionCompleteLabel.SetLabelText("COMPLETED");
                    SectionCompleteLabel.SetBorderWidth(0.0f);
                    SectionCompleteLabel.SetFontName("Verdana-Bold");
                    SectionCompleteLabel.SetTextColour("Bright Yellow");
                    SectionCompleteLabel.SetFontSize(14f);
                    SectionCompleteLabel.SetCellColour("DarkSlateGrey");
                    SectionCompleteLabel.SetTag(iSectionCompleteLabelTagId * (iii+1));
                    SectionCompleteLabel.SetHidden(bHideComplete);
                    SectionCompleteLabelVw = SectionCompleteLabel.GetLabelCell();
                    arrItems4[1] = SectionCompleteLabelVw;

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
                    
                    arrItems4[2] = btnSaveEquipmentVw;
                    
                    UILabel hfSectionEquipmentHeight = new UILabel();
                    hfSectionEquipmentHeight.Tag = iSectionHeightTagId * (iii+1);
                    hfSectionEquipmentHeight.Hidden = true;
                    hfSectionEquipmentHeight.Text = "0";
                    arrItems4[3] = hfSectionEquipmentHeight;
                    
                    UILabel hfSectionEquipmentRows = new UILabel();
                    hfSectionEquipmentRows.Tag = iSectionRowsTagId * (iii+1);
                    hfSectionEquipmentRows.Hidden = true;
                    hfSectionEquipmentRows.Text = iPwrIdRows.ToString();
                    arrItems4[4] = hfSectionEquipmentRows;
                    
                    UILabel hfSectionEquipmentStatus = new UILabel();
                    hfSectionEquipmentStatus.Tag = iSectionStatusTagId * (iii+1);
                    hfSectionEquipmentStatus.Hidden = true;
                    hfSectionEquipmentStatus.Text = "0";
                    arrItems4[5] = hfSectionEquipmentStatus;
                    
                    
                    SectionEquipmentRow.AddSubviews(arrItems4);
                    
                    iVert += iSectionHdrRowHeight;
                    
                    //Now add a new view to this view to hold another view containing all the pwrid info for this section 10
                    UIView PwrIdTableRow = new UIView();
                    PwrIdTableRow.Frame = new RectangleF(0f,iVert,1000f,0f);
                    iSectionId = iContainerSectionTagId * (iii+1);
                    PwrIdTableRow.Tag = iSectionId;
                    layout.AddSubview(PwrIdTableRow);
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
                for(int iiii=0;iiii< m_iQuestionSections; iiii++)
                {
                    if(bHideSections[iiii])
                    {
                        UIButton btnContract = (UIButton)View.ViewWithTag (iContractSectionBtnTagId * (iiii+1));

                        ContractSection(btnContract, null);
                    }
                }

			}
			catch (Exception except)
			{
				string sTest = except.Message.ToString();
                iUtils.AlertBox alert = new iUtils.AlertBox();
                alert.CreateErrorAlertDialog(sTest);
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
            return DBQ.ProjectSection10BatteryPwrIdComplete(m_sPassedId, sPwrId);
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

        public bool BatteryFullyComplete()
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10BatteryComplete(m_sPassedId);
        }
        
        public bool PowerConversionFullyComplete()
        {
            clsTabletDB.ITPDocumentSection DBQ = new clsTabletDB.ITPDocumentSection();
            return DBQ.ProjectSection10PowerConversionComplete(m_sPassedId);
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

