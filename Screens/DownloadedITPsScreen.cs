using System;
using System.Drawing;
using System.Data;
using System.Threading.Tasks;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

using clsiOS;
using nspTabletCommon;	
using clsTabletCommon.ITPExternal;

namespace ITPiPadSoln
{
	public partial class DownloadedITPsScreen : UIViewController
	{
		//Set the tag id constants. Don't use multiples of 1000 because then you can get a clash. Offset by a prime ideally
		int iProjectIdTagId = 10113;
		int iProjectDescTagId = 11041;
		int iOpenBtnTagId = 12027;
		int iUploadBtnTagId = 13219;
		int iProjStatusTagId = 14417;
		int iOpenBtnStatusTagId = 15037;
		int iUploadBtnStatusTagId = 16053;
		int iRemoveBtnTagId = 17019;
		int iRemoveBtnStatusTagId = 18007;
        int iBackupBtnTagId = 19031;

		string m_sSessionId = "";
		string m_sUser = "";
		string m_PassedId = "";
		string m_PassedDesc = "";
		int m_iProjectsInList = 0;
		Task taskA;

//		UIActivityIndicatorView prog = new UIActivityIndicatorView();
		iUtils.ActivityIndicator prog = new iUtils.ActivityIndicator();
		UIView progVw = new UIView();

		public DownloadedITPsScreen () : base ("DownloadedITPsScreen", null)
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
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
			DrawMenu();
			DrawOpeningPage();
			progVw = prog.CreateActivityIndicator();
			
			
			View.Add(progVw);
		}
		
		public void DrawMenu()
		{
			UIView[] arrItems = new UIView[3];
			string sUsername = "";
			HomeScreen home = GetHomeScreen();
			sUsername = home.GetLoginName();
			m_sUser = sUsername;
			m_sSessionId = home.GetSessionId();
			
			//Create a table view
			iUtils.CreateFormGridItem lblUsername = new iUtils.CreateFormGridItem();
			UIView lblUsernameVw = new UIView();
			lblUsername.SetDimensions(100f,40f, 100f, 25f, 2f, 2f, 2f, 2f);
			lblUsername.SetLabelText("Username:");
			lblUsername.SetBorderWidth(0.0f);
			lblUsername.SetFontName("Verdana");
			lblUsername.SetFontSize(12f);
			lblUsername.SetCellColour("Wheat");
			lblUsername.SetTag(10);
			lblUsernameVw = lblUsername.GetLabelCell();
			arrItems[0] = lblUsernameVw;
			
			iUtils.CreateFormGridItem txtUsername = new iUtils.CreateFormGridItem();
			UIView txtUsernameVw = new UIView();
			txtUsername.SetDimensions(200f,40f, 200f, 25f, 2f, 2f, 2f, 2f);
			txtUsername.SetLabelText(sUsername);
			txtUsername.SetBorderWidth(0.0f);
			txtUsername.SetFontName("Verdana");
			txtUsername.SetFontSize(12f);
			txtUsername.SetCellColour("Wheat");
			txtUsername.SetTag(20);
			txtUsernameVw = txtUsername.GetLabelCell();
			arrItems[1] = txtUsernameVw;
			
			UILabel hfSessionId = new UILabel();
			hfSessionId.Tag = 70;
			hfSessionId.Frame = new RectangleF(230f,100f,200f,30f);
			hfSessionId.Text = m_sSessionId;
			hfSessionId.Hidden = true;
			arrItems[2] = hfSessionId;
			
			View.AddSubviews(arrItems);
			
		}

		public void DrawOpeningPage ()
		{
			try 
			{
				int iDownloaded;
				int iOpenBtnStatus = 1; //Default is on
				int iUploadBtnStatus = 0; //Default is off
				int iRemoveBtnStatus = 0; //Default is off
				string sStatusText = "";
				int iColNo = 0;
				string sId = "";
				int iOpenBtnId = -1;

				float iVert = 100.0f;
				float iRowHeight = 50f;
				
				//Place the headings
				UIView[] arrItems = new UIView[7];
				
				//Create the headings
				iUtils.CreateFormGridItem lblProjIdHdr = new iUtils.CreateFormGridItem();
				UIView lblProjIdHdrVw = new UIView();
				lblProjIdHdr.SetDimensions(10f,iVert, 100f, iRowHeight, 2f, 2f, 2f, 2f);
				lblProjIdHdr.SetLabelText("Project Id");
				lblProjIdHdr.SetBorderWidth(1.0f);
				lblProjIdHdr.SetFontName("Verdana-Bold");
				lblProjIdHdr.SetFontSize(12f);
				lblProjIdHdr.SetCellColour("DarkSlateGrey");
				lblProjIdHdr.SetTextColour("White");
				lblProjIdHdr.SetTextAlignment("centre");
				lblProjIdHdrVw = lblProjIdHdr.GetLabelCell();
				arrItems[0] = lblProjIdHdrVw;
				
				
				iUtils.CreateFormGridItem lblDescHdr = new iUtils.CreateFormGridItem();
				UIView lblDescHdrVw = new UIView();
				lblDescHdr.SetDimensions(109f,iVert, 250f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
				lblDescHdr.SetLabelText("Description");
				lblDescHdr.SetBorderWidth(1.0f);
				lblDescHdr.SetFontName("Verdana-Bold");
				lblDescHdr.SetFontSize(12f);
				lblDescHdr.SetCellColour("DarkSlateGrey");
				lblDescHdr.SetTextColour("White");
				lblDescHdr.SetTextAlignment("centre");
				lblDescHdrVw = lblDescHdr.GetLabelCell();
				arrItems[1] = lblDescHdrVw;
				
				
				iUtils.CreateFormGridItem lblOpenHdr = new iUtils.CreateFormGridItem();
				UIView lblOpenHdrVw = new UIView();
				lblOpenHdr.SetDimensions(358f,iVert,100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
				lblOpenHdr.SetLabelText("Open");
				lblOpenHdr.SetBorderWidth(1.0f);
				lblOpenHdr.SetFontName("Verdana-Bold");
				lblOpenHdr.SetFontSize(12f);
				lblOpenHdr.SetCellColour("DarkSlateGrey");
				lblOpenHdr.SetTextColour("White");
				lblOpenHdr.SetTextAlignment("centre");
				lblOpenHdrVw = lblOpenHdr.GetLabelCell();
				arrItems[2] = lblOpenHdrVw;
				
				iUtils.CreateFormGridItem lblUploadHdr = new iUtils.CreateFormGridItem();
				UIView lblUploadHdrVw = new UIView();
				lblUploadHdr.SetDimensions(457f,iVert,100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
				lblUploadHdr.SetLabelText("Upload");
				lblUploadHdr.SetBorderWidth(1.0f);
				lblUploadHdr.SetFontName("Verdana-Bold");
				lblUploadHdr.SetFontSize(12f);
				lblUploadHdr.SetCellColour("DarkSlateGrey");
				lblUploadHdr.SetTextColour("White");
				lblUploadHdr.SetTextAlignment("centre");
				lblUploadHdrVw = lblUploadHdr.GetLabelCell();
				arrItems[3] = lblUploadHdrVw;

                iUtils.CreateFormGridItem lblBackupHdr = new iUtils.CreateFormGridItem();
                UIView lblBackupHdrVw = new UIView();
                lblBackupHdr.SetDimensions(556f,iVert,100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
                lblBackupHdr.SetLabelText("Backup");
                lblBackupHdr.SetBorderWidth(1.0f);
                lblBackupHdr.SetFontName("Verdana-Bold");
                lblBackupHdr.SetFontSize(12f);
                lblBackupHdr.SetCellColour("DarkSlateGrey");
                lblBackupHdr.SetTextColour("White");
                lblBackupHdr.SetTextAlignment("centre");
                lblBackupHdrVw = lblBackupHdr.GetLabelCell();
                arrItems[4] = lblBackupHdrVw;

                iUtils.CreateFormGridItem lblRemoveHdr = new iUtils.CreateFormGridItem();
				UIView lblRemoveHdrVw = new UIView();
				lblRemoveHdr.SetDimensions(655f,iVert,100f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
				lblRemoveHdr.SetLabelText("Remove");
				lblRemoveHdr.SetBorderWidth(1.0f);
				lblRemoveHdr.SetFontName("Verdana-Bold");
				lblRemoveHdr.SetFontSize(12f);
				lblRemoveHdr.SetCellColour("DarkSlateGrey");
				lblRemoveHdr.SetTextColour("White");
				lblRemoveHdr.SetTextAlignment("centre");
				lblRemoveHdrVw = lblRemoveHdr.GetLabelCell();
				arrItems[5] = lblRemoveHdrVw;

				iUtils.CreateFormGridItem lblStatusHdr = new iUtils.CreateFormGridItem();
				UIView lblStatusHdrVw = new UIView();
				lblStatusHdr.SetDimensions(754f,iVert,250f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
				lblStatusHdr.SetLabelText("Status");
				lblStatusHdr.SetBorderWidth(1.0f);
				lblStatusHdr.SetFontName("Verdana-Bold");
				lblStatusHdr.SetFontSize(12f);
				lblStatusHdr.SetCellColour("DarkSlateGrey");
				lblStatusHdr.SetTextColour("White");
				lblStatusHdr.SetTextAlignment("centre");
				lblStatusHdrVw = lblStatusHdr.GetLabelCell();
				arrItems[6] = lblStatusHdrVw;

				View.AddSubviews(arrItems);
				
				//Loop around for each item and place in the psuedo table
				DataSet arrITP = GetITPsDownloadedLocal();
				if(arrITP.Tables.Count >0)
				{
					UIView[] arrItems2 = new UIView[10];
					int iRows = arrITP.Tables[0].Rows.Count;
					m_iProjectsInList = iRows;

					for (int i = 0; i < iRows; i++)
					{
						sStatusText = "";

						iVert += (iRowHeight - 1.0f); //Make it 1 less so that the border does not double up
						iUtils.CreateFormGridItem lblProjId = new iUtils.CreateFormGridItem();
						UIView lblProjIdVw = new UIView();
						lblProjId.SetDimensions(10f,iVert, 100f, iRowHeight, 2f, 2f, 2f, 2f);
						iColNo = arrITP.Tables[0].Columns["ID"].Ordinal;
						sId = arrITP.Tables[0].Rows[i].ItemArray[iColNo].ToString();
						lblProjId.SetLabelText(sId);
						lblProjId.SetBorderWidth(1.0f);
						lblProjId.SetFontName("Verdana");
						lblProjId.SetFontSize(12f);
						lblProjId.SetTag(iProjectIdTagId * (i+1));
						lblProjIdVw = lblProjId.GetLabelCell();
						arrItems2[0] = lblProjIdVw;
						
						iUtils.CreateFormGridItem lblDesc = new iUtils.CreateFormGridItem();
						UIView lblDescVw = new UIView();
						lblDesc.SetDimensions(109f,iVert, 250f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
						iColNo = arrITP.Tables[0].Columns["ProjectDesc"].Ordinal;
						lblDesc.SetLabelText(arrITP.Tables[0].Rows[i].ItemArray[iColNo].ToString());
						lblDesc.SetBorderWidth(1.0f);
						lblDesc.SetFontName("Verdana");
						lblDesc.SetFontSize(12f);
						lblDesc.SetTag(iProjectDescTagId * (i+1));
						lblDescVw = lblDesc.GetLabelCell();
						arrItems2[1] = lblDescVw;

						
						//If uploaded back to main DB then disable the open button
						iColNo = arrITP.Tables[0].Columns["Downloaded"].Ordinal;
						iDownloaded = Convert.ToInt16(arrITP.Tables[0].Rows[i].ItemArray[iColNo]);

						iUtils.CreateFormGridItem btnOpen = new iUtils.CreateFormGridItem();
						UIView btnOpenVw = new UIView();
						btnOpen.SetDimensions(358f,iVert, 100f, iRowHeight, 8f, 4f, 8f, 4f); //Set left to 1 less so border does not double up
						btnOpen.SetLabelText("Open");
						btnOpen.SetBorderWidth(1.0f);
						btnOpen.SetFontName("Verdana");
						btnOpen.SetFontSize(14f);
						iOpenBtnId = iOpenBtnTagId * (i+1);
						btnOpen.SetTag(iOpenBtnId);
						btnOpenVw = btnOpen.GetButtonCell();
						
						UIButton btnOpenButton = new UIButton();
						btnOpenButton = btnOpen.GetButton();
						btnOpenButton.TouchUpInside += (sender,e) => {OpenITP(sender, e);};
						iOpenBtnStatus = 1;
						if (iDownloaded == 1)
						{
							btnOpenButton.Enabled = false;
							sStatusText += "Uploaded already. You can remove the project or you must download again to make changes.";
							iOpenBtnStatus = 0;
							iRemoveBtnStatus = 1;
						}
						else
						{
							sStatusText += "Ready for modification, backup or upload.";
						}

						arrItems2[2] = btnOpenVw;

						iUtils.CreateFormGridItem btnUpload = new iUtils.CreateFormGridItem();
						UIView btnUploadVw = new UIView();
						btnUpload.SetDimensions(457f,iVert, 100f, iRowHeight, 8f, 4f, 8f, 4f); //Set left to 1 less so border does not double up
						btnUpload.SetLabelText("Upload");
						btnUpload.SetBorderWidth(1.0f);
						btnUpload.SetFontName("Verdana");
						btnUpload.SetFontSize(14f);
						btnUpload.SetTag(iUploadBtnTagId * (i+1));
						btnUploadVw = btnUpload.GetButtonCell();
						
						UIButton btnUploadButton = new UIButton();
						btnUploadButton = btnUpload.GetButton();
						btnUploadButton.TouchUpInside += (sender,e) => {UploadITPQuestion(sender, e, 1);};

						iUploadBtnStatus = 1;

                        iUtils.CreateFormGridItem btnBackup = new iUtils.CreateFormGridItem();
                        UIView btnBackupVw = new UIView();
                        btnBackup.SetDimensions(556f,iVert, 100f, iRowHeight, 8f, 4f, 8f, 4f); //Set left to 1 less so border does not double up
                        btnBackup.SetLabelText("Backup");
                        btnBackup.SetBorderWidth(1.0f);
                        btnBackup.SetFontName("Verdana");
                        btnBackup.SetFontSize(14f);
                        btnBackup.SetTag(iBackupBtnTagId * (i+1));
                        btnBackupVw = btnBackup.GetButtonCell();
                        
                        UIButton btnBackupButton = new UIButton();
                        btnBackupButton = btnBackup.GetButton();
                        btnBackupButton.TouchUpInside += (sender,e) => {UploadITPQuestion(sender, e, 2);};
                        
                        //iBackupBtnStatus = 1;

                        iUtils.CreateFormGridItem btnRemove = new iUtils.CreateFormGridItem();
						UIView btnRemoveVw = new UIView();
						btnRemove.SetDimensions(655f,iVert, 100f, iRowHeight, 8f, 4f, 8f, 4f); //Set left to 1 less so border does not double up
						btnRemove.SetLabelText("Remove");
						btnRemove.SetBorderWidth(1.0f);
						btnRemove.SetFontName("Verdana");
						btnRemove.SetFontSize(14f);
						btnRemove.SetTag(iRemoveBtnTagId * (i+1));
						btnRemoveVw = btnRemove.GetButtonCell();
						
						UIButton btnRemoveButton = new UIButton();
						btnRemoveButton = btnRemove.GetButton();
						btnRemoveButton.TouchUpInside += (sender,e) => {RemoveITP(sender, e);};
						btnRemoveButton.Enabled = false;

						//If not logged in then you cannot upload
						if (m_sUser == "Not logged in to SCMS" )
						{
							btnUploadButton.Enabled = false;
                            btnBackupButton.Enabled = false;
                            iUploadBtnStatus = 0;
							if (iDownloaded != 1)
							{
								sStatusText += "You must login to upload.";
							}
						}
						
						if (iDownloaded == 1)
						{
							btnUploadButton.Enabled = false;
                            btnBackupButton.Enabled = false;
                            iUploadBtnStatus = 0;
							btnRemoveButton.Enabled = true;
							iRemoveBtnStatus = 1;
						}

						arrItems2[3] = btnUploadVw;
                        arrItems2[4] = btnBackupVw;
                        arrItems2[5] = btnRemoveVw;

						iUtils.CreateFormGridItem lblStatus = new iUtils.CreateFormGridItem();
						UIView lblStatusVw = new UIView();
						lblStatus.SetDimensions(754f,iVert, 250f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
						lblStatus.SetLabelText(sStatusText);
						lblStatus.SetBorderWidth(1.0f);
						lblStatus.SetFontName("Verdana");
						lblStatus.SetFontSize(12f);
						lblStatus.SetTag(iProjStatusTagId * (i+1));
						lblStatusVw = lblStatus.GetLabelCell();
						arrItems2[6] = lblStatusVw;

						UILabel hfOpenBtnStatus = new UILabel();
						hfOpenBtnStatus.Tag = (iOpenBtnStatusTagId * (i+1));
						hfOpenBtnStatus.Text = iOpenBtnStatus.ToString();
						hfOpenBtnStatus.Hidden = true;
						arrItems2[7] = hfOpenBtnStatus;

						UILabel hfUploadBtnStatus = new UILabel();
						hfUploadBtnStatus.Tag = (iUploadBtnStatusTagId * (i+1));
						hfUploadBtnStatus.Text = iUploadBtnStatus.ToString();
						hfUploadBtnStatus.Hidden = true;
						arrItems2[8] = hfUploadBtnStatus;

						UILabel hfRemoveBtnStatus = new UILabel();
						hfRemoveBtnStatus.Tag = (iRemoveBtnStatusTagId * (i+1));
						hfRemoveBtnStatus.Text = iRemoveBtnStatus.ToString();
						hfRemoveBtnStatus.Hidden = true;
						arrItems2[9] = hfRemoveBtnStatus;

						View.AddSubviews(arrItems2);
						
					}
				}
								

			} 
			catch (Exception except) 
			{
				iUtils.AlertBox alert = new iUtils.AlertBox();
				alert.CreateErrorAlertDialog(except.Message);
			}
		}

		public DataSet GetITPsDownloadedLocal()
		{
			string sRtnMsg = "";
			clsITPFramework csITP = new clsITPFramework();
			DataSet objListITPs = csITP.GetITPsDownloaded(ref sRtnMsg);
			return objListITPs;
		}

		public void OpenITP (object sender, EventArgs e)
		{
			//Show the progress indicator and position at top left of button
			UIButton btnOpen = (UIButton)sender;
			prog.SetActivityIndicatorTitle("Opening ITP");
			ScreenUtils scnUtils = new ScreenUtils();
			scnUtils.GetAbsolutePosition(btnOpen);
			float iTop = scnUtils.GetPositionTop();
			float iLeft = scnUtils.GetPositionLeft();
			prog.SetActivityIndicatorPosition(iLeft,iTop);
			prog.ShowActivityIndicator();
			prog.StartAnimating();

			//Disable all other buttons
			DisableButtons();

			taskA = new Task (() => OpenITPTask (sender, e));
			taskA.Start ();
		}

		public void OpenITPTask(object sender, EventArgs e)
		{
			this.InvokeOnMainThread(() => 
			{
				UIButton btnOpen = (UIButton)sender;
				int iOpenITPId = btnOpen.Tag;
				UILabel projId = (UILabel)View.ViewWithTag (iOpenITPId/iOpenBtnTagId*iProjectIdTagId);
				string sId = projId.Text;
				m_PassedId = sId;
				UILabel projDesc = (UILabel)View.ViewWithTag (iOpenITPId/iOpenBtnTagId*iProjectDescTagId);
				string sProjDesc = projDesc.Text;
				m_PassedDesc = sProjDesc;
				ProjectITPage projITPScreen = new ProjectITPage();
//				this.NavigationController.NavigationBarHidden = true;
				this.NavigationController.PushViewController(projITPScreen, true);
				prog.StopAnimating();
				prog.CloseActivityIndicator();
				ReEnableButtons();
			});
		}

		void UploadITPQuestion(object sender, EventArgs e, int iMarkUploadType)
		{
			UIButton btnClicked = (UIButton)sender;
			int iClicked = btnClicked.Tag;
			UILabel projId = (UILabel)View.ViewWithTag (iClicked/iUploadBtnTagId*iProjectIdTagId);
			string sId = projId.Text;
            int iOpenBtnId = -1;
            if(iMarkUploadType == 1)
            {
			    iOpenBtnId = iClicked/iUploadBtnTagId*iOpenBtnTagId; 
            }
            else
            {
                iOpenBtnId = iClicked/iBackupBtnTagId*iOpenBtnTagId; 
            }

			var bConnStatus = GetConnectionStatus ();

			if (!bConnStatus)
			{
				iUtils.AlertBox alert = new iUtils.AlertBox();
				alert.CreateAlertDialog();
				alert.SetAlertMessage("The is no network coverage so you cannot upload at this stage");
				alert.ShowAlertBox(); 
				return;
			}
            else
            {
                //Has the project been made available online again and if so you cannot upload

            }
			
            if(iMarkUploadType == 1)
            {
    			iUtils.AlertBox alert2 = new iUtils.AlertBox();
    			alert2.CreateAlertYesNoDialog();
    			alert2.SetAlertMessage("This will upload ITP info for Project " + sId + " and lock you out from any further changes. Do you wish to continue?");
    			alert2.ShowAlertBox(); 
                UIAlertView alert3 = alert2.GetAlertDialog();
                alert3.Clicked += (sender2, e2)  => {CheckUploadQuestion(sender2, e2, e2.ButtonIndex, sId, iOpenBtnId, 0);}; 
            }
            else
            {
                iUtils.AlertBox alert2 = new iUtils.AlertBox();
                alert2.CreateAlertYesNoDialog();
                alert2.SetAlertMessage("This will backup ITP info for Project " + sId + " and allow you to make further changes once complete. Do you wish to continue?");
                alert2.ShowAlertBox(); 
                UIAlertView alert3 = alert2.GetAlertDialog();
                alert3.Clicked += (sender2, e2)  => {CheckUploadQuestion(sender2, e2, e2.ButtonIndex, sId, iOpenBtnId, 1);}; 
            }

			return;
		}

		public void CheckUploadQuestion (object sender, EventArgs e, int iBtnIndex, string sId, int iOpenBtnId, int iUploadOrBackup)
		{
			switch (iBtnIndex) 
			{
				case 0:
                    UploadITP(sId, iOpenBtnId, iUploadOrBackup);
					break;
				case 1:
					break;
			}
		}

        bool UploadITP(string sId, int iOpenBtnId, int iUploadOrBackup)
		{
			string sUser = m_sUser;
			string sSessionId = m_sSessionId;
			clsITPFramework ITPFwrk = new clsITPFramework();
			string sRtnMsg = "";
            bool bUpload = ITPFwrk.UploadITPInfo(sSessionId, sUser, sId, iUploadOrBackup, ref sRtnMsg);
			if (bUpload && sRtnMsg == "")
			{
				//Now also disable the open button for this project
                if(iUploadOrBackup == 0)
                {
    				UIButton btnOpen = (UIButton)View.ViewWithTag (iOpenBtnId);
    				btnOpen.Enabled = false;
    				UILabel hfbtnOpenStatus = (UILabel)View.ViewWithTag (iOpenBtnId/iOpenBtnTagId * iOpenBtnStatusTagId);
    				hfbtnOpenStatus.Text = "0";
                }

				UIButton btnUpload = (UIButton)View.ViewWithTag (iOpenBtnId/ iOpenBtnTagId * iUploadBtnTagId);
				btnUpload.Enabled = false;
				UILabel hfbtnUploadStatus = (UILabel)View.ViewWithTag (iOpenBtnId/iOpenBtnTagId * iUploadBtnStatusTagId);
				hfbtnUploadStatus.Text = "0";

                //Enable the remove button
                UIButton btnRemove = (UIButton)View.ViewWithTag (iOpenBtnId/ iOpenBtnTagId * iRemoveBtnTagId);
                btnRemove.Enabled = true;
                UILabel hfbtnRemoveStatus = (UILabel)View.ViewWithTag (iOpenBtnId/iOpenBtnTagId * iRemoveBtnStatusTagId);
                hfbtnRemoveStatus.Text = "1";

                //Change the status text too
                if(iUploadOrBackup == 0)
                {
    				UILabel lblStatus = (UILabel)View.ViewWithTag (iOpenBtnId/ iOpenBtnTagId * iProjStatusTagId);
    				lblStatus.Text = "Uploaded already. You must download again to make changes.";
                }

				iUtils.AlertBox alert = new iUtils.AlertBox();
				alert.CreateAlertDialog();
                if(iUploadOrBackup == 0)
                {
				    alert.SetAlertMessage("ITP info for Project " + sId + " successfully uploaded.");
                }
                else
                {
                    alert.SetAlertMessage("ITP info for Project " + sId + " successfully backed up.");
                }
				alert.ShowAlertBox(); 
			}
			else
			{
				iUtils.AlertBox alert = new iUtils.AlertBox();
				alert.CreateAlertDialog();
				alert.SetAlertMessage(sRtnMsg);
				alert.ShowAlertBox(); 
			}
			
			return bUpload;
		}

		public void RemoveITP (object sender, EventArgs e)
		{
			clsTabletDB.ITPCollection coll = new clsTabletDB.ITPCollection ();
			UIButton btnClicked = (UIButton)sender;
			int iClicked = btnClicked.Tag;
			UILabel projId = (UILabel)View.ViewWithTag (iClicked / iRemoveBtnTagId * iProjectIdTagId);
			string sId = projId.Text;
			string sRtnMsg = "";

			if (!coll.ITPLocalRemove (sId, ref sRtnMsg)) 
			{
				iUtils.AlertBox alert = new iUtils.AlertBox ();
				alert.CreateAlertDialog ();
				alert.SetAlertMessage (sRtnMsg);
				alert.ShowAlertBox (); 
			} 
			else //Disable the remove, open and upload buttons for this row
			{
				UIButton btnOpen = (UIButton)View.ViewWithTag (iOpenBtnTagId * iClicked / iRemoveBtnTagId);
				UIButton btnUpload = (UIButton)View.ViewWithTag (iUploadBtnTagId * iClicked / iRemoveBtnTagId);
				UIButton btnRemove = (UIButton)View.ViewWithTag (iRemoveBtnTagId * iClicked / iRemoveBtnTagId);
				btnOpen.Enabled = false;
				btnUpload.Enabled = false;
				btnRemove.Enabled = false;

				UILabel hfbtnOpenStatus = (UILabel)View.ViewWithTag (iOpenBtnStatusTagId * iClicked / iRemoveBtnTagId);
				UILabel hfbtnUploadStatus = (UILabel)View.ViewWithTag (iUploadBtnStatusTagId * iClicked / iRemoveBtnTagId);
				UILabel hfbtnRemoveStatus = (UILabel)View.ViewWithTag (iRemoveBtnStatusTagId * iClicked / iRemoveBtnTagId);
				hfbtnOpenStatus.Text = "0";
				hfbtnUploadStatus.Text = "0";
				hfbtnRemoveStatus.Text = "0";

				//Update the status text too
				UILabel lblStatus = (UILabel)View.ViewWithTag (iClicked/ iRemoveBtnTagId * iProjStatusTagId);
				lblStatus.Text = "Removed completely. Reopening thsi screen will remove the project from this listing.";
				
				iUtils.AlertBox alert = new iUtils.AlertBox();
				alert.CreateAlertDialog();
				alert.SetAlertMessage("ITP info for Project " + sId + " successfully removed.");
				alert.ShowAlertBox(); 
			}
		}

		public void DisableButtons()
		{
			for (int i = 0; i < m_iProjectsInList; i++)
			{
				UIButton btnOpen = (UIButton)View.ViewWithTag (iOpenBtnTagId * (i+1));
				UIButton btnUpload = (UIButton)View.ViewWithTag (iUploadBtnTagId * (i+1));
                UIButton btnBackup = (UIButton)View.ViewWithTag (iBackupBtnTagId * (i+1));
                UIButton btnRemove = (UIButton)View.ViewWithTag (iRemoveBtnTagId * (i+1));
				btnOpen.Enabled = false;
				btnUpload.Enabled = false;
                btnBackup.Enabled = false;
                btnRemove.Enabled = false;
			}
		}

		public void ReEnableButtons()
		{
			for (int i = 0; i < m_iProjectsInList; i++)
			{
				UIButton btnOpen = (UIButton)View.ViewWithTag (iOpenBtnTagId * (i+1));
				UIButton btnUpload = (UIButton)View.ViewWithTag (iUploadBtnTagId * (i+1));
                UIButton btnBackup = (UIButton)View.ViewWithTag (iBackupBtnTagId * (i+1));
                UIButton btnRemove = (UIButton)View.ViewWithTag (iRemoveBtnTagId * (i+1));
				UILabel hfbtnOpenStatus = (UILabel)View.ViewWithTag (iOpenBtnStatusTagId * (i+1));
				UILabel hfbtnUploadStatus = (UILabel)View.ViewWithTag (iUploadBtnStatusTagId * (i+1));
                UILabel hfbtnRemoveStatus = (UILabel)View.ViewWithTag (iRemoveBtnStatusTagId * (i+1));
				if (hfbtnOpenStatus.Text == "1")
				{
					btnOpen.Enabled = true;
				}
				if (hfbtnUploadStatus.Text == "1")
				{
					btnUpload.Enabled = true;
                    btnBackup.Enabled = true;
				}
				if (hfbtnRemoveStatus.Text == "1")
				{
					btnRemove.Enabled = true;
				}
			}
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

		public bool GetConnectionStatus ()
		{
			
			NetworkStatus iNetStatus = Reachability.InternetConnectionStatus ();
			
			if (iNetStatus == NetworkStatus.NotReachable) 
			{
				return false;
			}
			
			return true;
		}

		public string GetSelectedProjectId()
		{
			return m_PassedId;
		}

		public string GetSelectedProjectDesc()
		{
			return m_PassedDesc;
		}
	}
}

