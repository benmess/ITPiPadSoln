
using System;
using System.Drawing;
using System.Threading.Tasks;

using Mono.Data.Sqlite;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using clsiOS;
using ITPAndroidApp;	
using clsTabletCommon.ITPExternal;

namespace ITPiPadSoln
{
	public partial class DownloadITPScreen : UIViewController
	{
		string m_sSessionId = "";
		string m_sUser = "";
		Task taskA;
		iUtils.ProgressBar progBarQuestionVw = new iUtils.ProgressBar();
		UIView progBarQuestions = new UIView();
		iUtils.ProgressBar progBarTypesVw = new iUtils.ProgressBar();
		UIView progBarTypes = new UIView();
		iUtils.ProgressBar progBarSectionsVw = new iUtils.ProgressBar();
		UIView progBarSections = new UIView();
		iUtils.ProgressBar progBarProjITPHeaderVw = new iUtils.ProgressBar();
		UIView progBarProjITPHeader = new UIView();
		iUtils.ProgressBar progBarProjITPQuestionsVw = new iUtils.ProgressBar();
		UIView progBarProjITPQuestions = new UIView();
		iUtils.ProgressBar progBarInventoryVw = new iUtils.ProgressBar();
		UIView progBarInventoryItems = new UIView();
        iUtils.ProgressBar progBarBatteryFuseTypesVw = new iUtils.ProgressBar();
        UIView progBarBatteryFuseTypesItems = new UIView();
        iUtils.ProgressBar progBarBatteryCellInfoVw = new iUtils.ProgressBar();
        UIView progBarBatteryCellInfoItems = new UIView();
        iUtils.ProgressBar progBarValidHierarchyVw = new iUtils.ProgressBar();
        UIView progBarValidHierarchyItems = new UIView();
        iUtils.ProgressBar progBarProjITPSection10Vw = new iUtils.ProgressBar();
		UIView progBarProjITPSection10= new UIView();
        iUtils.ProgressBar progBarProjITPRFUVw = new iUtils.ProgressBar();
        UIView progBarProjITPRFU = new UIView();
        iUtils.ProgressBar progBarProjBattTestDischCurrentVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestDischCurrent = new UIView();
        iUtils.ProgressBar progBarProjBattTestDischVoltVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestDischVolt = new UIView();
        iUtils.ProgressBar progBarProjBattTestFloatRecordVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestFloatRecord = new UIView();
        iUtils.ProgressBar progBarProjBattTestLink1to3Vw = new iUtils.ProgressBar();
        iUtils.ProgressBar progBarProjBattTestHeaderVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestHeader = new UIView();
        UIView progBarProjBattTestLink1to3 = new UIView();
        iUtils.ProgressBar progBarProjBattTestLink2to3Vw = new iUtils.ProgressBar();
        UIView progBarProjBattTestLink2to3 = new UIView();
        iUtils.ProgressBar progBarProjBattTestLink3to3Vw = new iUtils.ProgressBar();
        UIView progBarProjBattTestLink3to3 = new UIView();

        iUtils.ProgressBar progBarProjBattTestOCVolts05HrVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestOCVolts05Hr = new UIView();

        iUtils.ProgressBar progBarProjBattTestUnpackedVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestUnpacked = new UIView();
        iUtils.ProgressBar progBarProjBattTestVolts5MinVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestVolts5Min = new UIView();
        iUtils.ProgressBar progBarProjBattTestVolts10MinVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestVolts10Min = new UIView();
        iUtils.ProgressBar progBarProjBattTestVolts15MinVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestVolts15Min = new UIView();
        iUtils.ProgressBar progBarProjBattTestVolts20MinVw = new iUtils.ProgressBar();
        UIView progBarProjBattTestVolts20Min = new UIView();

        int iProjIdTag = 100010000;
        int iProjDescTag = 100011000;
        int iDownloadButtonTag = 100012000;
        int iStatusButtonTag = 100013000;
        int iCPTag = 100014000;
        int iProjTypeTag = 100015000;

        int iProjectsInList = 0;
        int giSecureFlag = 0;

		public DownloadITPScreen () : base ("DownloadITPScreen", null)
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

            HomeScreen home = GetHomeScreen();
            giSecureFlag =  home.GetGlobalSecureFlag();

			// Perform any additional setup after loading the view, typically from a nib.
			DrawMenu();
			DrawOpeningPage();
		}
		
//		public override void ViewDidUnload ()
//		{
//			base.ViewDidUnload ();
//			
//			// Clear any references to subviews of the main view in order to
//			// allow the Garbage Collector to collect them sooner.
//			//
//			// e.g. myOutlet.Dispose (); myOutlet = null;
//			
//			ReleaseDesignerOutlets ();
//		}
//		
//		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
//		{
//			// Return true for supported orientations
//			return true;
//		}

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
			lblUsername.SetDimensions(100f,5f, 100f, 25f, 2f, 2f, 2f, 2f);
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
			txtUsername.SetDimensions(200f,5f, 200f, 25f, 2f, 2f, 2f, 2f);
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
			hfSessionId.Frame = new RectangleF(230f,200f,200f,30f);
			hfSessionId.Text = m_sSessionId;
			hfSessionId.Hidden = true;
			arrItems[2] = hfSessionId;

			View.AddSubviews(arrItems);

		}

		public void DrawOpeningPage ()
		{
			try 
			{
                UIScrollView layout = new UIScrollView();
                layout.Frame = new RectangleF(0f,35f,1000f,620f);
                layout.Tag = 2;
                View.AddSubview(layout);
				float iVert = 5.0f;
				float iRowHeight = 50f;
                float iTotalHeight = iRowHeight;

				//Place the headings
				UIView[] arrItems = new UIView[3];

				//Create the headings
				iUtils.CreateFormGridItem lblProjIdHdr = new iUtils.CreateFormGridItem();
				UIView lblProjIdHdrVw = new UIView();
				lblProjIdHdr.SetDimensions(10f,iVert, 150f, iRowHeight, 2f, 2f, 2f, 2f);
				lblProjIdHdr.SetLabelText("Project Id");
				lblProjIdHdr.SetBorderWidth(1.0f);
				lblProjIdHdr.SetFontName("Verdana-Bold");
				lblProjIdHdr.SetFontSize(18f);
				lblProjIdHdr.SetCellColour("DarkSlateGrey");
				lblProjIdHdr.SetTextColour("White");
				lblProjIdHdrVw = lblProjIdHdr.GetLabelCell();
				arrItems[0] = lblProjIdHdrVw;


				iUtils.CreateFormGridItem lblDescHdr = new iUtils.CreateFormGridItem();
				UIView lblDescHdrVw = new UIView();
				lblDescHdr.SetDimensions(159f,iVert, 400f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
				lblDescHdr.SetLabelText("Description");
				lblDescHdr.SetBorderWidth(1.0f);
				lblDescHdr.SetFontName("Verdana-Bold");
				lblDescHdr.SetFontSize(18f);
				lblDescHdr.SetCellColour("DarkSlateGrey");
				lblDescHdr.SetTextColour("White");
				lblDescHdrVw = lblDescHdr.GetLabelCell();
				arrItems[1] = lblDescHdrVw;


				iUtils.CreateFormGridItem lblDownloadHdr = new iUtils.CreateFormGridItem();
				UIView lblDownloadHdrVw = new UIView();
				lblDownloadHdr.SetDimensions(558f,iVert,120f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
				lblDownloadHdr.SetLabelText("Download");
				lblDownloadHdr.SetBorderWidth(1.0f);
				lblDownloadHdr.SetFontName("Verdana-Bold");
				lblDownloadHdr.SetFontSize(18f);
				lblDownloadHdr.SetCellColour("DarkSlateGrey");
				lblDownloadHdr.SetTextColour("White");
				lblDownloadHdrVw = lblDownloadHdr.GetLabelCell();
				arrItems[2] = lblDownloadHdrVw;

                layout.AddSubviews(arrItems);

				//Loop around for each item and place in the psuedo table
				object[] arrITP = GetITPsForDownloadLocal();

                if(arrITP[0].ToString() == "Success")
				{
					object[] arrIdList = (object[])arrITP[1];
					object[] arrDescList = (object[])arrITP[2];
                    object[] arrProjType = (object[])arrITP[3];
                    object[] arrProjCP = (object[])arrITP[4];
                    UIView[] arrItems2 = new UIView[6];
					iProjectsInList = arrIdList.Length;
                    iTotalHeight = (iProjectsInList) * iRowHeight;

					for(int i=0; i< arrIdList.Length; i++)
					{
						iVert += (iRowHeight - 1.0f); //Make it 1 less so that the border does not double up
						iUtils.CreateFormGridItem lblProjId2 = new iUtils.CreateFormGridItem();
						UIView lblProjIdVw = new UIView();
						lblProjId2.SetDimensions(10f,iVert, 150f, iRowHeight, 2f, 2f, 2f, 2f);
						lblProjId2.SetLabelText(arrIdList[i].ToString());
						lblProjId2.SetBorderWidth(1.0f);
						lblProjId2.SetFontName("Verdana");
						lblProjId2.SetFontSize(14f);
                        lblProjId2.SetTag(iProjIdTag * (i+1));
						lblProjIdVw = lblProjId2.GetLabelCell();
						arrItems2[0] = lblProjIdVw;

						iUtils.CreateFormGridItem lblDesc = new iUtils.CreateFormGridItem();
						UIView lblDescVw = new UIView();
						lblDesc.SetDimensions(159f,iVert, 400f, iRowHeight, 2f, 2f, 2f, 2f); //Set left to 1 less so border does not double up
						lblDesc.SetLabelText(arrDescList[i].ToString());
						lblDesc.SetBorderWidth(1.0f);
						lblDesc.SetFontName("Verdana");
						lblDesc.SetFontSize(14f);
                        lblDesc.SetTag(iProjDescTag * (i+1));
						lblDescVw = lblDesc.GetLabelCell();
						arrItems2[1] = lblDescVw;

						iUtils.CreateFormGridItem btnDownload = new iUtils.CreateFormGridItem();
						UIView btnDownloadVw = new UIView();
						btnDownload.SetDimensions(558f,iVert, 120f, iRowHeight, 8f, 4f, 8f, 4f); //Set left to 1 less so border does not double up
						btnDownload.SetLabelText("Download");
						btnDownload.SetBorderWidth(1.0f);
						btnDownload.SetFontName("Verdana");
						btnDownload.SetFontSize(14f);
                        btnDownload.SetTag(iDownloadButtonTag * (i+1));
						btnDownloadVw = btnDownload.GetButtonCell();

						UIButton btnDownloadButton = new UIButton();
						btnDownloadButton = btnDownload.GetButton();
						btnDownloadButton.TouchUpInside += (sender,e) => {DownloadITP(sender, e);};
						arrItems2[2] = btnDownloadVw;

						UILabel btnStatus = new UILabel();
						btnStatus.Tag = iStatusButtonTag * (i+1);
						btnStatus.Text = "0";
						btnStatus.Hidden = true;
						arrItems2[3] = btnStatus;

                        UILabel lblCP = new UILabel();
                        lblCP.Tag = iCPTag * (i+1);
                        lblCP.Text = arrProjCP[i].ToString();
                        lblCP.Hidden = true;
                        arrItems2[4] = lblCP;

                        UILabel lblProjType = new UILabel();
                        lblProjType.Tag = iProjTypeTag * (i+1);
                        lblProjType.Text = arrProjType[i].ToString();
                        lblProjType.Hidden = true;
                        arrItems2[5] = lblProjType;

                        layout.AddSubviews(arrItems2);

					}
				}

                //And reduce the content size of the main scroll view by the same amount
                iTotalHeight += 200f;
                SizeF layoutSize = new SizeF(1000f, iTotalHeight);
                layout.ContentSize = layoutSize;

                progBarQuestions = progBarQuestionVw.CreateProgressBar();
				progBarQuestionVw.SetProgressBarTitle("Downloading suite of questions");

				progBarTypes = progBarTypesVw.CreateProgressBar();
				progBarTypesVw.SetProgressBarTitle("Downloading suite of ITP types");

				progBarSections = progBarSectionsVw.CreateProgressBar();
				progBarSectionsVw.SetProgressBarTitle("Downloading suite of ITP sections");

				progBarInventoryItems = progBarInventoryVw.CreateProgressBar();
				progBarInventoryVw.SetProgressBarTitle("Downloading suite of inventory items");

                progBarBatteryFuseTypesItems = progBarBatteryFuseTypesVw.CreateProgressBar();
                progBarBatteryFuseTypesVw.SetProgressBarTitle("Downloading battery fuse types items");

                progBarBatteryCellInfoItems = progBarBatteryCellInfoVw.CreateProgressBar();
                progBarBatteryCellInfoVw.SetProgressBarTitle("Downloading battery cell info items");

                progBarValidHierarchyItems = progBarValidHierarchyVw.CreateProgressBar();
                progBarValidHierarchyVw.SetProgressBarTitle("Downloading valid hierarchy items");

                progBarProjITPHeader = progBarProjITPHeaderVw.CreateProgressBar();
				progBarProjITPQuestions = progBarProjITPQuestionsVw.CreateProgressBar();
				progBarProjITPSection10 = progBarProjITPSection10Vw.CreateProgressBar();
                progBarProjITPRFU = progBarProjITPRFUVw.CreateProgressBar();

                progBarProjBattTestDischCurrent = progBarProjBattTestDischCurrentVw.CreateProgressBar();
                progBarProjBattTestDischVolt = progBarProjBattTestDischVoltVw.CreateProgressBar();
                progBarProjBattTestFloatRecord = progBarProjBattTestFloatRecordVw.CreateProgressBar();
                progBarProjBattTestHeader = progBarProjBattTestHeaderVw.CreateProgressBar();
                progBarProjBattTestLink1to3 = progBarProjBattTestLink1to3Vw.CreateProgressBar();
                progBarProjBattTestLink2to3 = progBarProjBattTestLink2to3Vw.CreateProgressBar();
                progBarProjBattTestLink3to3 = progBarProjBattTestLink3to3Vw.CreateProgressBar();

                progBarProjBattTestOCVolts05Hr = progBarProjBattTestOCVolts05HrVw.CreateProgressBar();
                progBarProjBattTestUnpacked = progBarProjBattTestUnpackedVw.CreateProgressBar();
                progBarProjBattTestVolts5Min = progBarProjBattTestVolts5MinVw.CreateProgressBar();
                progBarProjBattTestVolts10Min = progBarProjBattTestVolts10MinVw.CreateProgressBar();
                progBarProjBattTestVolts15Min = progBarProjBattTestVolts15MinVw.CreateProgressBar();
                progBarProjBattTestVolts20Min = progBarProjBattTestVolts20MinVw.CreateProgressBar();

				View.Add(progBarQuestions);
				View.Add(progBarTypes);
				View.Add(progBarSections);
				View.Add(progBarProjITPHeader);
				View.Add(progBarProjITPQuestions);
				View.Add(progBarInventoryItems);
                View.Add(progBarBatteryFuseTypesItems);
                View.Add(progBarBatteryCellInfoItems);
                View.Add(progBarValidHierarchyItems);
                View.Add(progBarProjITPSection10);
                View.Add(progBarProjITPRFU);

                View.Add(progBarProjBattTestDischCurrent);
                View.Add(progBarProjBattTestDischVolt);
                View.Add(progBarProjBattTestFloatRecord);
                View.Add(progBarProjBattTestHeader);
                View.Add(progBarProjBattTestLink1to3);
                View.Add(progBarProjBattTestLink2to3);
                View.Add(progBarProjBattTestLink3to3);

                View.Add(progBarProjBattTestOCVolts05Hr);
                View.Add(progBarProjBattTestUnpacked);
                View.Add(progBarProjBattTestVolts5Min);
                View.Add(progBarProjBattTestVolts10Min);
                View.Add(progBarProjBattTestVolts15Min);
                View.Add(progBarProjBattTestVolts20Min);
} 
			catch (Exception except) 
			{
				iUtils.AlertBox alert = new iUtils.AlertBox();
				alert.CreateErrorAlertDialog(except.Message);
			}
		}

		public object[] GetITPsForDownloadLocal ()
		{
			string sSessionId = m_sSessionId;
			string sUser = m_sUser;
			clsITPFramework csITP = new clsITPFramework();
            object[] objListITPs = csITP.GetITPsForDownload(sSessionId, sUser, giSecureFlag);
			return objListITPs;

		}

		public bool SetITPDownloaded(string sId, string sUser, string sSessionId, ref string sRtnMsg)
		{
			clsITPFramework csITP = new clsITPFramework();
            bool bITPDownloaded = csITP.MarkITPDownloaded(sSessionId, sUser, sId, giSecureFlag,ref sRtnMsg);
			return bITPDownloaded;
		}

		public void DownloadITP (object sender, EventArgs e)
		{
			var txtName = (UILabel)View.ViewWithTag (20);
			string sUser = txtName.Text;
			var hfSessionId = (UILabel)View.ViewWithTag (70);
			string sSessionId = hfSessionId.Text;
			UIButton button = (UIButton)sender;

            clsITPFramework ITPFwrk = new clsITPFramework();

            //Check to see if the user is still logged in
            object[] objLoggedIn = ITPFwrk.IsUserLoggedIn(m_sSessionId, m_sUser);

            if (Convert.ToBoolean(objLoggedIn [0]))
            {

                var txtId = (UILabel)View.ViewWithTag(button.Tag / iDownloadButtonTag * iProjIdTag);
                string sId = txtId.Text;

                var txtDesc = (UILabel)View.ViewWithTag(button.Tag / iDownloadButtonTag * iProjDescTag);
                string sDesc = txtDesc.Text;

                var txtCP = (UILabel)View.ViewWithTag(button.Tag / iDownloadButtonTag * iCPTag);
                string sCP = txtCP.Text;

                var txtProjType = (UILabel)View.ViewWithTag(button.Tag / iDownloadButtonTag * iProjTypeTag);
                Int32 iProjType = Convert.ToInt32(txtProjType.Text);

                if (!HasConnectionStatus())
                {
                }
                //Start the downlaod task. It has to be run as a separate thread for some reason otherwise it won't show. What a pain!!!
                taskA = new Task(() => RunDownload(sId, sDesc, button, sUser, sSessionId,iProjType, sCP));
                taskA.Start();

                //Now disable the download button for all projects so the user cannot click whilst an existing download is in progress
                for (int i=0; i < iProjectsInList; i++)
                {
                    UIButton btnDownload = (UIButton)View.ViewWithTag(iDownloadButtonTag * (i + 1));
                    btnDownload.Enabled = false;
                }
            }
            else
            {
                iUtils.AlertBox alert5 = new iUtils.AlertBox();
                alert5.CreateErrorAlertDialog("You are no longer logged in to the SCMS. You must login again before you can upload or backup a project.");
                HomeScreen home = GetHomeScreen();
                home.SetLoginName("Not logged in to SCMS");
                m_sUser = "Not logged in to SCMS";
                home.SetSessionId("");
                m_sSessionId = "";
                UILabel Username = (UILabel)View.ViewWithTag (20);
                Username.Text = "Not logged in to SCMS";
                UILabel Session = (UILabel)View.ViewWithTag (70);
                Session.Text = "";
                home.SetLoggedInStatus("0");
                return;
            }
		}

		//This is all in a separate thread so to display anything you have to use the RunOnUiThread method
        public bool RunDownload(string sId, string sDescription, UIButton button, string sUser, string sSessionId, Int32 iProjType, string sCP)
		{
			try
			{
				string sRtnMsg = ""; 
                if (!DownloadProjectITPInfo(sId, sDescription,sUser, sSessionId, iProjType, sCP))
				{
					this.InvokeOnMainThread(() => {
						iUtils.AlertBox alert = new iUtils.AlertBox();
						alert.CreateAlertDialog();
						alert.SetAlertMessage("Cannot download ITP info for Project " + sId);
						alert.ShowAlertBox(); 
						ReEnableButtons();
                        UILabel btnStatus = (UILabel)View.ViewWithTag (button.Tag /iDownloadButtonTag * iStatusButtonTag);
						button.Enabled = false; btnStatus.Text = "1"; });
					return false;
				}
				else
				{
					if (!SetITPDownloaded(sId, sUser, sSessionId, ref sRtnMsg))
					{
						this.InvokeOnMainThread(() => {
							iUtils.AlertBox alert = new iUtils.AlertBox();
							alert.CreateAlertDialog();
							alert.SetAlertMessage("Cannot mark ITP info for Project " + sId + " as successfully downloaded. Please contact SCMS admin for help.");
							alert.ShowAlertBox(); 
							ReEnableButtons();
                            UILabel btnStatus = (UILabel)View.ViewWithTag (button.Tag /iDownloadButtonTag * iStatusButtonTag);
							button.Enabled = false; btnStatus.Text = "1"; });
						return false;
					}
					else
					{
						this.InvokeOnMainThread(() => {
							iUtils.AlertBox alert = new iUtils.AlertBox();
							alert.CreateAlertDialog();
							alert.SetAlertMessage("ITP info for Project " + sId + " successfully downloaded. You can now work offline.");
							alert.ShowAlertBox(); 
							ReEnableButtons();
                            UILabel btnStatus = (UILabel)View.ViewWithTag (button.Tag /iDownloadButtonTag * iStatusButtonTag);
							button.Enabled = false; btnStatus.Text = "1"; });
						return true;
					}
				}
			}
			catch (Exception ex)
			{

				this.InvokeOnMainThread(() => {
					iUtils.AlertBox alert = new iUtils.AlertBox();
					alert.CreateAlertDialog();
					alert.SetAlertMessage(ex.Message.ToString()); 
					alert.ShowAlertBox(); 
					ReEnableButtons();
				});

				return false;
			}
		}

		public void ReEnableButtons()
		{
			for (int i = 0; i < iProjectsInList; i++)
			{
                    UIButton btnDownload = (UIButton)View.ViewWithTag (iDownloadButtonTag * (i+1));
                    UILabel btnStatus = (UILabel)View.ViewWithTag (iStatusButtonTag * (i+1));
					if (btnStatus.Text == "0")
					{
						btnDownload.Enabled = true;
					}
			}
		}
		
        public bool DownloadProjectITPInfo(string sId, string sDescription, string sUser, string sSessionId, Int32 iProjType, string sCP)
		{
			//First get all the static info
			if (DownloadStaticTables(sUser, sSessionId))
			{
				clsITPFramework csITP = new clsITPFramework();
				clsTabletDB.ITPHeaderTable clsTabDB = new clsTabletDB.ITPHeaderTable();
				clsTabletDB.ITPDocumentSection clsITPSection = new clsTabletDB.ITPDocumentSection();
                clsTabletDB.ITPBatteryTest clsITPBatteryTest = new clsTabletDB.ITPBatteryTest();
				string sRtnMsg = "";
				
				//****************************************************************************************//
				//                      DOCUMENT HEADER                                                   //
				//****************************************************************************************//
                object[] objListITPs = csITP.DownloadITPInfo(sSessionId, sUser, sId, giSecureFlag);
				
				if (objListITPs[0].ToString() == "Success")
				{
					//Get the header info from the website version. This has to exist before you can download.
					if (clsTabDB.TableHeaderDeleteAllRecords(sId, ref sRtnMsg))
					{
						string sITPDocumentHeaderInfo = objListITPs[1].ToString();
						string[] sHeaderInfo = sITPDocumentHeaderInfo.Split('~');
						if (sHeaderInfo[0] == "ITPDocumentHeaderInfo")
						{
							string[] delimiters = new string[] { "||" };
							string[] sHeaderItems = sHeaderInfo[1].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
							int iHeaderCount = sHeaderItems.Length;
                            //First check if the header table exists and if not create it
                            clsTabletDB.ITPHeaderTable ITPDB = new clsTabletDB.ITPHeaderTable();
                            if (ITPDB.CheckHeaderTable())
                            {
    							if (iHeaderCount > 0)
    							{
									this.InvokeOnMainThread(() => { 
										progBarProjITPHeaderVw.SetProgressBarTitle("Downloading ITP header info for project " + sId);
										progBarProjITPHeaderVw.ShowProgressBar(iHeaderCount); 
									});
									for (int i = 0; i < iHeaderCount; i++)
									{
										string[] delimiters2 = new string[] { "^" };
										string[] sHeaderSplitItems = sHeaderItems[i].Split(delimiters2, StringSplitOptions.None);
                                        Array.Resize<string>(ref sHeaderSplitItems, sHeaderSplitItems.Length + 3);
                                        sHeaderSplitItems[sHeaderSplitItems.Length - 3] = sDescription;
                                        sHeaderSplitItems[sHeaderSplitItems.Length - 2] = sCP;
                                        sHeaderSplitItems[sHeaderSplitItems.Length - 1] = iProjType.ToString();
										ITPDB.TableHeaderAddRecord(sHeaderSplitItems);
										this.InvokeOnMainThread(() => { progBarProjITPHeaderVw.UpdateProgressBar(i + 1); });
									}
									this.InvokeOnMainThread(() => { progBarProjITPHeaderVw.CloseProgressBar(); });
								}
							}
						}
						//return true;
					}
					else
					{
						this.InvokeOnMainThread(() => { 
							iUtils.AlertBox alert = new iUtils.AlertBox();
							alert.SetAlertMessage(sRtnMsg);
						    alert.ShowAlertBox(); });
						return false;
					}
					
					//****************************************************************************************//
					//                      QUESTIONNAIRE MASTER                                              //
					//****************************************************************************************//
                    object[] objITPQuestions = csITP.DownloadProjectITPQuestions(sSessionId, sUser, sId, giSecureFlag);
					
					//Get any questions already raised on the website version into the local DB
					if (objITPQuestions[0].ToString() == "Success")
					{
						if (clsITPSection.ITPProjectSectionDeleteAllQuestions(sId, ref sRtnMsg))
						{
							string sITPProjectQuesitonsInfo = objITPQuestions[1].ToString();
							string[] sProjQuestionInfo = sITPProjectQuesitonsInfo.Split('~');
							if (sProjQuestionInfo[0] == "ITPProjectQuestionnaireInfo")
							{
								string[] delimiters3 = new string[] { "||" };
								string[] sProjectQuestions = sProjQuestionInfo[1].Split(delimiters3, StringSplitOptions.RemoveEmptyEntries);
								int iHeaderCount3 = sProjectQuestions.Length;
                                //First check if the question master table exists and if not create it
                                clsTabletDB.ITPDocumentSection ITPQuest = new clsTabletDB.ITPDocumentSection();
                                if (ITPQuest.CheckQuestionTableMst())
                                {
    								if (iHeaderCount3 > 0)
    								{
										this.InvokeOnMainThread(() => { 
											progBarProjITPQuestionsVw.SetProgressBarTitle("Downloading ITP questions for project " + sId);
											progBarProjITPQuestionsVw.ShowProgressBar(iHeaderCount3); 
										});
										for (int i = 0; i < iHeaderCount3; i++)
										{
											string[] delimiters4 = new string[] { "^" };
											string[] sProjectQuestionItems = sProjectQuestions[i].Split(delimiters4, StringSplitOptions.None);
											ITPQuest.ITPProjectQuestionAddRecord(sProjectQuestionItems);
											this.InvokeOnMainThread(() => { progBarProjITPQuestionsVw.UpdateProgressBar(i + 1); });
										}
										this.InvokeOnMainThread(() => { progBarProjITPQuestionsVw.CloseProgressBar(); });
									}
								}
							}
						}
						else
						{
							this.InvokeOnMainThread(() => { 
								iUtils.AlertBox alert = new iUtils.AlertBox();
								alert.SetAlertMessage(sRtnMsg);
								alert.ShowAlertBox(); 
							});
							return false;
						}
					}
					else
					{
						return false;
					} //Close the if block for success on project ITP questions
					
					//****************************************************************************************//
					//                      SECTION 10                                                       //
					//****************************************************************************************//
                    object[] objITPSection10Info = csITP.DownloadProjectITPSection10(sSessionId, sUser, sId, giSecureFlag);
					
					//Get any section 10 info already raised on the website version into the local DB
					if (objITPSection10Info[0].ToString() == "Success")
					{
						if (clsITPSection.ITPProjectSectionDeleteAllSection10Items(sId, ref sRtnMsg))
						{
							string sITPProjectSection10Info = objITPSection10Info[1].ToString();
							string[] sProjSection10Info = sITPProjectSection10Info.Split('~');
							if (sProjSection10Info[0] == "ITPProjectSection10Info")
							{
								string[] delimiters5 = new string[] { "||" };
								string[] sProjectSection10Items = sProjSection10Info[1].Split(delimiters5, StringSplitOptions.RemoveEmptyEntries);
								int iHeaderCount5 = sProjectSection10Items.Length;
                                //First check if the section 10 table exists and if not create it
                                clsTabletDB.ITPDocumentSection ITPSection10 = new clsTabletDB.ITPDocumentSection();
                                if (ITPSection10.CheckSection10Table())
                                {
    								if (iHeaderCount5 > 0)
    								{
										this.InvokeOnMainThread(() => { 
											progBarProjITPSection10Vw.SetProgressBarTitle("Downloading ITP section 10 items for project " + sId);
											progBarProjITPSection10Vw.ShowProgressBar(iHeaderCount5); 
										});
										for (int i = 0; i < iHeaderCount5; i++)
										{
											string[] delimiters6 = new string[] { "^" };
											string[] sProjectSection10ItemArray = sProjectSection10Items[i].Split(delimiters6, StringSplitOptions.None);
											ITPSection10.ITPSection10AddRecord(sProjectSection10ItemArray);
											this.InvokeOnMainThread(() => { progBarProjITPSection10Vw.UpdateProgressBar(i + 1); });
										}
										this.InvokeOnMainThread(() => { progBarProjITPSection10Vw.CloseProgressBar(); });
									}
								}
							}
						}
						else
						{
							this.InvokeOnMainThread(() => { 
								iUtils.AlertBox alert = new iUtils.AlertBox();
								alert.SetAlertMessage(sRtnMsg);
								alert.ShowAlertBox(); 
							});
							return false;
						}
					}
					else
					{
						return false;
					} //Close the if block for success on project ITP section 10

                    //****************************************************************************************//
                    //                      RFU HEADER INFO                                                   //
                    //****************************************************************************************//
                    object[] objITPRFUInfo = csITP.DownloadProjectITPRFU(sSessionId, sUser, sId, giSecureFlag);
                    
                    //Get any RFU info already raised on the website version into the local DB
                    if (objITPRFUInfo[0].ToString() == "Success")
                    {
                        if (clsITPSection.ITPProjectSectionDeleteAllRFUItems(sId, ref sRtnMsg))
                        {
                            string sITPProjectRFUInfo = objITPRFUInfo[1].ToString();
                            string[] sProjRFUInfo = sITPProjectRFUInfo.Split('~');
                            if (sProjRFUInfo[0] == "ITPProjectRFUInfo")
                            {
                                string[] delimiters5 = new string[] { "||" };
                                string[] sProjectRFUItems = sProjRFUInfo[1].Split(delimiters5, StringSplitOptions.RemoveEmptyEntries);
                                int iHeaderCount5 = sProjectRFUItems.Length;
                                //First check if the RFU table exists and if not create it
                                clsTabletDB.ITPDocumentSection ITPRFU = new clsTabletDB.ITPDocumentSection();
                                if (ITPRFU.CheckRFUTable())
                                {
                                    if (iHeaderCount5 > 0)
                                    {
                                        this.InvokeOnMainThread(() => { 
                                            progBarProjITPRFUVw.SetProgressBarTitle("Downloading ITP RFU PwrId items for project " + sId);
                                            progBarProjITPRFUVw.ShowProgressBar(iHeaderCount5); 
                                        });
                                        for (int i = 0; i < iHeaderCount5; i++)
                                        {
                                            string[] delimiters6 = new string[] { "^" };
                                            string[] sProjectRFUItemArray = sProjectRFUItems[i].Split(delimiters6, StringSplitOptions.None);
                                            ITPRFU.ITPRFUAddRecord(sProjectRFUItemArray);
                                            this.InvokeOnMainThread(() => { progBarProjITPRFUVw.UpdateProgressBar(i + 1); });
                                        }
                                        this.InvokeOnMainThread(() => { progBarProjITPRFUVw.CloseProgressBar(); });
                                    }
                                }
                            }

                            //****************************************************************************************//
                            //                      BATTERY DISCHARGE TEST INFO                                                   //
                            //****************************************************************************************//
                            object[] objITPBatteryInfo = csITP.DownloadProjectITPBatteryDischargeTest(sSessionId, sUser, sId, giSecureFlag);

                            //Get any Battery Discharge Test info already raised on the website version into the local DB
                            if (objITPBatteryInfo[0].ToString() == "Success")
                            {
                                string sDischargeCurrentTableName = clsITPBatteryTest.sITPBatteryAcceptTestDischargeCurrentTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sDischargeCurrentTableName,sId, ref sRtnMsg))
                                {
                                    string sDischargeCurrentString = objITPBatteryInfo[1].ToString();
                                    string[] sDischargeCurrentInfo = sDischargeCurrentString.Split('~');
                                    if (sDischargeCurrentInfo[0] == "ITPProjectBattAcceptTest_DischrgCurrent")
                                    {
                                        string[] delimiters16 = new string[] { "||" };
                                        string[] sDischargeCurrentItems = sDischargeCurrentInfo[1].Split(delimiters16, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount16 = sDischargeCurrentItems.Length;
                                        //First check if the discharge current table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_DischargeCurrentTable())
                                        {
                                            if (iHeaderCount16 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestDischCurrentVw.SetProgressBarTitle("Downloading ITP Battery Discharge Test items for project " + sId);
                                                    progBarProjBattTestDischCurrentVw.ShowProgressBar(iHeaderCount16); 
                                                });
                                                for (int i = 0; i < iHeaderCount16; i++)
                                                {
                                                    string[] delimiters17 = new string[] { "^" };
                                                    string[] sBattTestDischCurrentItemArray = sDischargeCurrentItems[i].Split(delimiters17, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestDischCurrentItemArray, sDischargeCurrentTableName, 1);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestDischCurrentVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestDischCurrentVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Discharge Current Load

                                string sDischargeVoltTableName = clsITPBatteryTest.sITPBatteryAcceptTestDischargeVoltTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sDischargeVoltTableName,sId, ref sRtnMsg))
                                {
                                    string sDischargeVoltString = objITPBatteryInfo[2].ToString();
                                    string[] sDischargeVoltInfo = sDischargeVoltString.Split('~');
                                    if (sDischargeVoltInfo[0] == "ITPProjectBattAcceptTest_DischrgVolt")
                                    {
                                        string[] delimiters26 = new string[] { "||" };
                                        string[] sDischargeVoltItems = sDischargeVoltInfo[1].Split(delimiters26, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount26 = sDischargeVoltItems.Length;
                                        //First check if the batt test discharge voltage table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_DischargeVoltTable())
                                        {
                                            if (iHeaderCount26 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestDischVoltVw.SetProgressBarTitle("Downloading ITP Battery Test Discharge Volt items for project " + sId);
                                                    progBarProjBattTestDischVoltVw.ShowProgressBar(iHeaderCount26); 
                                                });
                                                for (int i = 0; i < iHeaderCount26; i++)
                                                {
                                                    string[] delimiters27 = new string[] { "^" };
                                                    string[] sBattTestDischargeVoltItemArray = sDischargeVoltItems[i].Split(delimiters27, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestDischargeVoltItemArray, sDischargeVoltTableName, 1);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestDischVoltVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestDischVoltVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Discharge Volt Load

                                string sFloatRecordTableName = clsITPBatteryTest.sITPBatteryAcceptTestFloatRecordTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sFloatRecordTableName,sId, ref sRtnMsg))
                                {
                                    string sFloatRecordString = objITPBatteryInfo[3].ToString();
                                    string[] sFloatRecordInfo = sFloatRecordString.Split('~');
                                    if (sFloatRecordInfo[0] == "ITPProjectBattAcceptTest_FloatRecord")
                                    {
                                        string[] delimiters36 = new string[] { "||" };
                                        string[] sFloatRecordItems = sFloatRecordInfo[1].Split(delimiters36, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount36 = sFloatRecordItems.Length;
                                        //First check if the batt test float record table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_FloatRecordTable())
                                        {
                                            if (iHeaderCount36 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestFloatRecordVw.SetProgressBarTitle("Downloading ITP Battery Test Float Record items for project " + sId);
                                                    progBarProjBattTestFloatRecordVw.ShowProgressBar(iHeaderCount36); 
                                                });
                                                for (int i = 0; i < iHeaderCount36; i++)
                                                {
                                                    string[] delimiters37 = new string[] { "^" };
                                                    string[] sBattTestFloatRecordItemArray = sFloatRecordItems[i].Split(delimiters37, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestFloatRecordItemArray, sFloatRecordTableName, 2);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestFloatRecordVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestFloatRecordVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Float Record Load

                                string sBattTestHdrTableName = clsITPBatteryTest.sITPBatteryAcceptTestHeaderTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sBattTestHdrTableName,sId, ref sRtnMsg))
                                {
                                    string sBattTestHdrString = objITPBatteryInfo[4].ToString();
                                    string[] sBattTestHdrInfo = sBattTestHdrString.Split('~');
                                    if (sBattTestHdrInfo[0] == "ITPProjectBattAcceptTest_Header")
                                    {
                                        string[] delimiters46 = new string[] { "||" };
                                        string[] sBattTestHdrItems = sBattTestHdrInfo[1].Split(delimiters46, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount46 = sBattTestHdrItems.Length;
                                        //First check if the batt test header table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_HeaderTable())
                                        {
                                            if (iHeaderCount46 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestHeaderVw.SetProgressBarTitle("Downloading ITP Battery Test Header items for project " + sId);
                                                    progBarProjBattTestHeaderVw.ShowProgressBar(iHeaderCount46); 
                                                });
                                                for (int i = 0; i < iHeaderCount46; i++)
                                                {
                                                    string[] delimiters47 = new string[] { "^" };
                                                    string[] sBattTestHeaderItemArray = sBattTestHdrItems[i].Split(delimiters47, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestHeaderItemArray, sBattTestHdrTableName, 4);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestHeaderVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestHeaderVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Header Load

                                string sLink1to3TableName = clsITPBatteryTest.sITPBatteryAcceptTestLink1To3TableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sLink1to3TableName,sId, ref sRtnMsg))
                                {
                                    string sLink1to3String = objITPBatteryInfo[5].ToString();
                                    string[] sLink1to3Info = sLink1to3String.Split('~');
                                    if (sLink1to3Info[0] == "ITPBattAcceptTest_Link1To3")
                                    {
                                        string[] delimiters56 = new string[] { "||" };
                                        string[] sLink1to3Items = sLink1to3Info[1].Split(delimiters56, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount56 = sLink1to3Items.Length;
                                        //First check if the batt test link 1 to 3 table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_Link1To3Table())
                                        {
                                            if (iHeaderCount56 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestLink1to3Vw.SetProgressBarTitle("Downloading ITP Battery Test Link 1 to 3 items for project " + sId);
                                                    progBarProjBattTestLink1to3Vw.ShowProgressBar(iHeaderCount56); 
                                                });
                                                for (int i = 0; i < iHeaderCount56; i++)
                                                {
                                                    string[] delimiters57 = new string[] { "^" };
                                                    string[] sBattTestLink1to3ItemArray = sLink1to3Items[i].Split(delimiters57, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestLink1to3ItemArray, sLink1to3TableName, 3);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestLink1to3Vw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestLink1to3Vw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Link 1 to 3 Load

                                string sLink2to3TableName = clsITPBatteryTest.sITPBatteryAcceptTestLink2To3TableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sLink2to3TableName,sId, ref sRtnMsg))
                                {
                                    string sLink2to3String = objITPBatteryInfo[6].ToString();
                                    string[] sLink2to3Info = sLink2to3String.Split('~');
                                    if (sLink2to3Info[0] == "ITPBattAcceptTest_Link2To3")
                                    {
                                        string[] delimiters66 = new string[] { "||" };
                                        string[] sLink2to3Items = sLink2to3Info[1].Split(delimiters66, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount66 = sLink2to3Items.Length;
                                        //First check if the batt test link 2 to 3 table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_Link2To3Table())
                                        {
                                            if (iHeaderCount66 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestLink2to3Vw.SetProgressBarTitle("Downloading ITP Battery Test Link 2 to 3 items for project " + sId);
                                                    progBarProjBattTestLink2to3Vw.ShowProgressBar(iHeaderCount66); 
                                                });
                                                for (int i = 0; i < iHeaderCount66; i++)
                                                {
                                                    string[] delimiters67 = new string[] { "^" };
                                                    string[] sBattTestLink2to3ItemArray = sLink2to3Items[i].Split(delimiters67, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestLink2to3ItemArray, sLink2to3TableName, 3);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestLink2to3Vw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestLink2to3Vw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Link 2 to 3 Load

                                string sLink3to3TableName = clsITPBatteryTest.sITPBatteryAcceptTestLink3To3TableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sLink3to3TableName,sId, ref sRtnMsg))
                                {
                                    string sLink3to3String = objITPBatteryInfo[7].ToString();
                                    string[] sLink3to3Info = sLink3to3String.Split('~');
                                    if (sLink3to3Info[0] == "ITPBattAcceptTest_Link3To3")
                                    {
                                        string[] delimiters76 = new string[] { "||" };
                                        string[] sLink3to3Items = sLink3to3Info[1].Split(delimiters76, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount76 = sLink3to3Items.Length;
                                        //First check if the batt test link 3 to 3 table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_Link3To3Table())
                                        {
                                            if (iHeaderCount76 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestLink3to3Vw.SetProgressBarTitle("Downloading ITP Battery Test Link 3 to 3 items for project " + sId);
                                                    progBarProjBattTestLink3to3Vw.ShowProgressBar(iHeaderCount76); 
                                                });
                                                for (int i = 0; i < iHeaderCount76; i++)
                                                {
                                                    string[] delimiters77 = new string[] { "^" };
                                                    string[] sBattTestLink3to3ItemArray = sLink3to3Items[i].Split(delimiters77, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestLink3to3ItemArray, sLink3to3TableName, 3);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestLink3to3Vw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestLink3to3Vw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Link 3 to 3 Load

                                string sOCVolts05HrTableName = clsITPBatteryTest.sITPBatteryAcceptTestOCVolts05HrTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sOCVolts05HrTableName,sId, ref sRtnMsg))
                                {
                                    string sOCVolts05HrString = objITPBatteryInfo[8].ToString();
                                    string[] sOCVolts05HrInfo = sOCVolts05HrString.Split('~');
                                    if (sOCVolts05HrInfo[0] == "ITPBattAcceptTest_OCVolts05Hr")
                                    {
                                        string[] delimiters86 = new string[] { "||" };
                                        string[] sOCVolts05HrItems = sOCVolts05HrInfo[1].Split(delimiters86, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount86 = sOCVolts05HrItems.Length;
                                        //First check if the batt test OC Volts 05Hr table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_OCVolts05HrTable())
                                        {
                                            if (iHeaderCount86 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestOCVolts05HrVw.SetProgressBarTitle("Downloading ITP Battery Test OC Volatge at 0.5 hr items for project " + sId);
                                                    progBarProjBattTestOCVolts05HrVw.ShowProgressBar(iHeaderCount86); 
                                                });
                                                for (int i = 0; i < iHeaderCount86; i++)
                                                {
                                                    string[] delimiters87 = new string[] { "^" };
                                                    string[] sBattTestOCVolts05HrItemArray = sOCVolts05HrItems[i].Split(delimiters87, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestOCVolts05HrItemArray, sOCVolts05HrTableName, 2);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestOCVolts05HrVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestOCVolts05HrVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test OC Voltage 05 Hr Load

                                string sUnpackedTableName = clsITPBatteryTest.sITPBatteryAcceptTestUnpackedTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sUnpackedTableName,sId, ref sRtnMsg))
                                {
                                    string sUnpackedString = objITPBatteryInfo[9].ToString();
                                    string[] sUnpackedInfo = sUnpackedString.Split('~');
                                    if (sUnpackedInfo[0] == "ITPBattAcceptTest_UnPacked")
                                    {
                                        string[] delimiters96 = new string[] { "||" };
                                        string[] sUnpackedItems = sUnpackedInfo[1].Split(delimiters96, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount96 = sUnpackedItems.Length;
                                        //First check if the batt test unpacked table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_UnpackedTable())
                                        {
                                            if (iHeaderCount96 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestUnpackedVw.SetProgressBarTitle("Downloading ITP Battery Test Unpacked items for project " + sId);
                                                    progBarProjBattTestUnpackedVw.ShowProgressBar(iHeaderCount96); 
                                                });
                                                for (int i = 0; i < iHeaderCount96; i++)
                                                {
                                                    string[] delimiters97 = new string[] { "^" };
                                                    string[] sBattTestUnpackedItemArray = sUnpackedItems[i].Split(delimiters97, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestUnpackedItemArray, sUnpackedTableName, 2);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestUnpackedVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestUnpackedVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Unpacked Load

                                string sVolts5MinTableName = clsITPBatteryTest.sITPBatteryAcceptTestVolts5MinTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sVolts5MinTableName,sId, ref sRtnMsg))
                                {
                                    string sVolts5MinString = objITPBatteryInfo[10].ToString();
                                    string[] sVolts5MinInfo = sVolts5MinString.Split('~');
                                    if (sVolts5MinInfo[0] == "ITPBattAcceptTest_Volts5Min")
                                    {
                                        string[] delimiters106 = new string[] { "||" };
                                        string[] sVolts5MinItems = sVolts5MinInfo[1].Split(delimiters106, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount106 = sVolts5MinItems.Length;
                                        //First check if the batt test Volts 5 Min table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_Volts5MinTable())
                                        {
                                            if (iHeaderCount106 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestVolts5MinVw.SetProgressBarTitle("Downloading ITP Battery Test Voltage at 5 min items for project " + sId);
                                                    progBarProjBattTestVolts5MinVw.ShowProgressBar(iHeaderCount106); 
                                                });
                                                for (int i = 0; i < iHeaderCount106; i++)
                                                {
                                                    string[] delimiters107 = new string[] { "^" };
                                                    string[] sBattTestVolts5MinItemArray = sVolts5MinItems[i].Split(delimiters107, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestVolts5MinItemArray, sVolts5MinTableName, 2);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestVolts5MinVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestVolts5MinVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Volts 5 Min Load

                                string sVolts10MinTableName = clsITPBatteryTest.sITPBatteryAcceptTestVolts10MinTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sVolts10MinTableName,sId, ref sRtnMsg))
                                {
                                    string sVolts10MinString = objITPBatteryInfo[11].ToString();
                                    string[] sVolts10MinInfo = sVolts10MinString.Split('~');
                                    if (sVolts10MinInfo[0] == "ITPBattAcceptTest_Volts10Min")
                                    {
                                        string[] delimiters116 = new string[] { "||" };
                                        string[] sVolts10MinItems = sVolts10MinInfo[1].Split(delimiters116, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount116 = sVolts10MinItems.Length;
                                        //First check if the batt test Volts 10 Min table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_Volts10MinTable())
                                        {
                                            if (iHeaderCount116 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestVolts10MinVw.SetProgressBarTitle("Downloading ITP Battery Test Voltage at 10 min items for project " + sId);
                                                    progBarProjBattTestVolts10MinVw.ShowProgressBar(iHeaderCount116); 
                                                });
                                                for (int i = 0; i < iHeaderCount116; i++)
                                                {
                                                    string[] delimiters117 = new string[] { "^" };
                                                    string[] sBattTestVolts10MinItemArray = sVolts10MinItems[i].Split(delimiters117, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestVolts10MinItemArray, sVolts10MinTableName, 2);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestVolts10MinVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestVolts10MinVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Volts 10 Min Load

                                string sVolts15MinTableName = clsITPBatteryTest.sITPBatteryAcceptTestVolts15MinTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sVolts15MinTableName,sId, ref sRtnMsg))
                                {
                                    string sVolts15MinString = objITPBatteryInfo[12].ToString();
                                    string[] sVolts15MinInfo = sVolts15MinString.Split('~');
                                    if (sVolts15MinInfo[0] == "ITPBattAcceptTest_Volts15Min")
                                    {
                                        string[] delimiters126 = new string[] { "||" };
                                        string[] sVolts15MinItems = sVolts15MinInfo[1].Split(delimiters126, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount126 = sVolts15MinItems.Length;
                                        //First check if the batt test Volts 15 Min table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_Volts15MinTable())
                                        {
                                            if (iHeaderCount126 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestVolts15MinVw.SetProgressBarTitle("Downloading ITP Battery Test Volateg at 15 min items for project " + sId);
                                                    progBarProjBattTestVolts15MinVw.ShowProgressBar(iHeaderCount126); 
                                                });
                                                for (int i = 0; i < iHeaderCount126; i++)
                                                {
                                                    string[] delimiters127 = new string[] { "^" };
                                                    string[] sBattTestVolts15MinItemArray = sVolts15MinItems[i].Split(delimiters127, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestVolts15MinItemArray, sVolts15MinTableName, 2);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestVolts15MinVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestVolts15MinVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Volts 15 Min Load

                                string sVolts20MinTableName = clsITPBatteryTest.sITPBatteryAcceptTestVolts20MinTableName;
                                if (clsITPBatteryTest.TableITPBatteryAcceptTestDeleteAllRecords(sVolts20MinTableName,sId, ref sRtnMsg))
                                {
                                    string sVolts20MinString = objITPBatteryInfo[13].ToString();
                                    string[] sVolts20MinInfo = sVolts20MinString.Split('~');
                                    if (sVolts20MinInfo[0] == "ITPBattAcceptTest_Volts20Min")
                                    {
                                        string[] delimiters136 = new string[] { "||" };
                                        string[] sVolts20MinItems = sVolts20MinInfo[1].Split(delimiters136, StringSplitOptions.RemoveEmptyEntries);
                                        int iHeaderCount136 = sVolts20MinItems.Length;
                                        //First check if the batt test Volts 20 Min table exists and if not create it
                                        clsTabletDB.ITPBatteryTest ITPBattTest = new clsTabletDB.ITPBatteryTest();
                                        if (ITPBattTest.CheckITPBatteryAcceptTest_Volts20MinTable())
                                        {
                                            if (iHeaderCount136 > 0)
                                            {
                                                this.InvokeOnMainThread(() => { 
                                                    progBarProjBattTestVolts20MinVw.SetProgressBarTitle("Downloading ITP Battery Test Voltage at 20 min items for project " + sId);
                                                    progBarProjBattTestVolts20MinVw.ShowProgressBar(iHeaderCount136); 
                                                });
                                                for (int i = 0; i < iHeaderCount136; i++)
                                                {
                                                    string[] delimiters137 = new string[] { "^" };
                                                    string[] sBattTestVolts20MinItemArray = sVolts20MinItems[i].Split(delimiters137, StringSplitOptions.None);
                                                    ITPBattTest.ITPBattTestAddRecord(sBattTestVolts20MinItemArray, sVolts20MinTableName, 2);
                                                    this.InvokeOnMainThread(() => { progBarProjBattTestVolts20MinVw.UpdateProgressBar(i + 1); });
                                                }
                                                this.InvokeOnMainThread(() => { progBarProjBattTestVolts20MinVw.CloseProgressBar(); });
                                            }
                                        }
                                    }
                                    return true; //Remove this and leave just in the last download
                                }
                                else
                                {
                                    this.InvokeOnMainThread(() => { 
                                        iUtils.AlertBox alert = new iUtils.AlertBox();
                                        alert.SetAlertMessage(sRtnMsg);
                                        alert.ShowAlertBox(); 
                                    });
                                    return false;
                                } //End Battery Test Volts 20 Min Load

                            }
                            else
                            {
                                return false;
                            } //Close the if block for success on Discharge Test Info (this is the main outer block. 
                              //There are several inner blocks for each table)
                        }
                        else
                        {
                            this.InvokeOnMainThread(() => { 
                                iUtils.AlertBox alert = new iUtils.AlertBox();
                                alert.SetAlertMessage(sRtnMsg);
                                alert.ShowAlertBox(); 
                            });
                            return false;
                        } 
                    }
                    else
                    {
                        return false;
                    } //Close the if block for success on project ITP RFU
                }
				else
				{
					return false;
				} //Close the if block for success on project ITP document header
			}
			else
			{
				return false;
			} //Close the if block for success on downlaod of static tables
		}


		public bool DownloadStaticTables(string sUser, string sSessionId)
		{
			string sRtnMsg = "";
			bool bReturn;
			clsTabletDB.ITPStaticTable clsStatic = new clsTabletDB.ITPStaticTable();
			
			clsStatic.CheckStaticTable();
			
			bReturn = FillQuestionnaireMainTable(sSessionId, sUser, ref sRtnMsg);
			
			if (sRtnMsg != "")
			{
				this.InvokeOnMainThread(() => { 
					iUtils.AlertBox alert = new iUtils.AlertBox();
					alert.CreateErrorAlertDialog(sRtnMsg);
				});
				return false;
			}
			
			bReturn = FillITPTypeMainTable(sSessionId, sUser, ref sRtnMsg);
			
			if (sRtnMsg != "")
			{
				this.InvokeOnMainThread(() => { 
					iUtils.AlertBox alert = new iUtils.AlertBox();
					alert.CreateErrorAlertDialog(sRtnMsg);
				});
				return false;
			}
			
			bReturn = FillITPDocumentSectionMainTable(sSessionId, sUser, ref sRtnMsg);
			
			if (sRtnMsg != "")
			{
				this.InvokeOnMainThread(() => { 
					iUtils.AlertBox alert = new iUtils.AlertBox();
					alert.CreateErrorAlertDialog(sRtnMsg);
				});
				return false;
			}
			
			bReturn = FillITPInventoryMainTable(sSessionId, sUser, ref sRtnMsg);
			
			if (sRtnMsg != "")
			{
				this.InvokeOnMainThread(() => { 
					iUtils.AlertBox alert = new iUtils.AlertBox();
					alert.CreateErrorAlertDialog(sRtnMsg);
				});
				return false;
			}

            bReturn = FillITPBatteryFuseTypesTable(sSessionId, sUser, ref sRtnMsg);
            
            if (sRtnMsg != "")
            {
                this.InvokeOnMainThread(() => { 
                    iUtils.AlertBox alert = new iUtils.AlertBox();
                    alert.CreateErrorAlertDialog(sRtnMsg);
                });
                return false;
            }
            
            bReturn = FillITPBatteryCellInfoTable(sSessionId, sUser, ref sRtnMsg);

            if (sRtnMsg != "")
            {
                this.InvokeOnMainThread(() => { 
                    iUtils.AlertBox alert = new iUtils.AlertBox();
                    alert.CreateErrorAlertDialog(sRtnMsg);
                });
                return false;
            }

            bReturn = FillITPValidHierarchyTable(sSessionId, sUser, ref sRtnMsg);
            
            if (sRtnMsg != "")
            {
                this.InvokeOnMainThread(() => { 
                    iUtils.AlertBox alert = new iUtils.AlertBox();
                    alert.CreateErrorAlertDialog(sRtnMsg);
                });
                return false;
            }

            return bReturn;
        }
		
		public bool FillQuestionnaireMainTable(string sSessionId, string sUser, ref string sRtnMsg)
		{
			try
			{
				clsTabletDB.ITPStaticTable Static = new clsTabletDB.ITPStaticTable();
				clsTabletDB.ITPQuestionnaire ITPQuest = new clsTabletDB.ITPQuestionnaire();
				LocalDB DB = new LocalDB();
				string sDocQuestionnaireTableName = ITPQuest.sDocQuestionnaireTableName;
				double dNewVersionNumber = 0.0;
				DateTime dtLastVersionDate;
				//Only do all of this if the version has changed. So get the local version number and compare to that on the DB. If different do all of this. - WRITE LATER as a general function
                bool bNewVersion = Static.IsNewVersionOfTable(sSessionId, sUser, sDocQuestionnaireTableName, giSecureFlag, ref dNewVersionNumber, ref dtLastVersionDate);
				if (!DB.TableExists(sDocQuestionnaireTableName) || bNewVersion)
				{
					clsLocalUtils util = new clsLocalUtils();
                    util.SetSecureFlag(giSecureFlag);
					string sURL = util.GetEnvironment_wbsURL("wbsITP_External");
					wbsITP_External ws = new wbsITP_External();
					ws.Url = sURL;
					object[] objQuestions = ws.GetITPFullQuestionnaireInfo(sSessionId, sUser);
					if (objQuestions[0].ToString() == "Success")
					{
						if (ITPQuest.TableQuestionnaireDeleteAllRecords(ref sRtnMsg))
						{
							string sITPDocumentQuestionnaireInfo = objQuestions[1].ToString();
							string[] sHeaderInfo = sITPDocumentQuestionnaireInfo.Split('~');
							if (sHeaderInfo[0] == "ITPQuestionnaireInfo")
							{
								string[] delimiters = new string[] { "||" };
								string[] sQuestionnaireItems = sHeaderInfo[1].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
								int iQuestionCount = sQuestionnaireItems.Length;
								if (iQuestionCount > 0)
								{
									this.InvokeOnMainThread(() => { progBarQuestionVw.ShowProgressBar(iQuestionCount); });
									//First check if the questionnaire table exists and if not create it
									if (ITPQuest.CheckFullQuestionnaireTable())
									{
										for (int i = 0; i < iQuestionCount; i++)
										{
											string[] delimiters2 = new string[] { "^" };
											string[] sQuestionItems = sQuestionnaireItems[i].Split(delimiters2, StringSplitOptions.None);
											ITPQuest.TableQuestionnaireAddRecord(sQuestionItems);
											this.InvokeOnMainThread(() => {progBarQuestionVw.UpdateProgressBar((i+1));});

										}
									}
								}
							}
						}
						//Update the version number locally
						Static.UpdateVersionNumber(sDocQuestionnaireTableName, dNewVersionNumber);
						this.InvokeOnMainThread(() => { progBarQuestionVw.CloseProgressBar(); });

                        if(!ITPQuest.AddTAndDQuestions())
                        {
                            return false;
                        }
						return true;
					}
					else
					{
						sRtnMsg = objQuestions[1].ToString();
						return false;
					}
				}
				else
				{
					//This means you don't have to fill this static table
					return true;
				}
			}
			catch (Exception ex)
			{
				sRtnMsg = "Failure" + ex.Message.ToString();
				return false;
			}
		}
		
		public bool FillITPTypeMainTable(string sSessionId, string sUser, ref string sRtnMsg)
		{
			try
			{
				clsTabletDB.ITPStaticTable Static = new clsTabletDB.ITPStaticTable();
				clsTabletDB.ITPTypes ITPType = new clsTabletDB.ITPTypes();
				LocalDB DB = new LocalDB();
				string sITPTypeTableName = ITPType.sITPTypeTableName;
				double dNewVersionNumber = 0.0;
				DateTime dtLastVersionDate;

				//Only do all of this if the version has changed. So get the local versoin umber and compare to that on the DB. If different do all of this. - WRITE LATER as a general function
                bool bNewVersion = Static.IsNewVersionOfTable(sSessionId, sUser, sITPTypeTableName, giSecureFlag, ref dNewVersionNumber, ref dtLastVersionDate);
				if (!DB.TableExists(sITPTypeTableName) || bNewVersion)
				{
					clsLocalUtils util = new clsLocalUtils();
                    util.SetSecureFlag(giSecureFlag);
					string sURL = util.GetEnvironment_wbsURL("wbsITP_External");
					wbsITP_External ws = new wbsITP_External();
					ws.Url = sURL;
					object[] objQuestions = ws.GetITPFullITPTypeInfo(sSessionId, sUser);
					if (objQuestions[0].ToString() == "Success")
					{
						if (ITPType.TableITPTypeDeleteAllRecords(ref sRtnMsg))
						{
							string sITPDocumentQuestionnaireInfo = objQuestions[1].ToString();
							string[] sHeaderInfo = sITPDocumentQuestionnaireInfo.Split('~');
							if (sHeaderInfo[0] == "ITPTypeInfo")
							{
								string[] delimiters = new string[] { "||" };
								string[] sQuestionnaireItems = sHeaderInfo[1].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
								int iQuestionCount = sQuestionnaireItems.Length;
								if (iQuestionCount > 0)
								{
									this.InvokeOnMainThread(() => { progBarTypesVw.ShowProgressBar(iQuestionCount); });
									//First check if the ITPType table exists and if not create it
									if (ITPType.CheckFullITPTypeTable())
									{
										for (int i = 0; i < iQuestionCount; i++)
										{
											string[] delimiters2 = new string[] { "^" };
											string[] sQuestionItems = sQuestionnaireItems[i].Split(delimiters2, StringSplitOptions.None);
											ITPType.TableITPTypeAddRecord(sQuestionItems);
											this.InvokeOnMainThread(() => { progBarTypesVw.UpdateProgressBar(i + 1); });
										}
									}
								}
							}
						}
						//Update the version number locally
						Static.UpdateVersionNumber(sITPTypeTableName, dNewVersionNumber);
						this.InvokeOnMainThread(() => { progBarTypesVw.CloseProgressBar(); });
						return true;
					}
					else
					{
						sRtnMsg = objQuestions[1].ToString();
						return false;
					}
				}
				else
				{
					//This means you don't have to fill this static table
					return true;
				}
			}
			catch (Exception ex)
			{
				sRtnMsg = "Failure" + ex.Message.ToString();
				return false;
			}
		}
		
		public bool FillITPDocumentSectionMainTable(string sSessionId, string sUser, ref string sRtnMsg)
		{
			try
			{
				clsTabletDB.ITPStaticTable Static = new clsTabletDB.ITPStaticTable();
				clsTabletDB.ITPDocumentSection ITPDocSection = new clsTabletDB.ITPDocumentSection();
				LocalDB DB = new LocalDB();
				string sITPDocumentSectionTableName = ITPDocSection.sITPDocumentSectionTableName;
				double dNewVersionNumber = 0.0;
				DateTime dtLastVersionDate;
				//Only do all of this if the version has changed. So get the local version umber and compare to that on the DB. If different do all of this. - WRITE LATER as a general function
                bool bNewVersion = Static.IsNewVersionOfTable(sSessionId, sUser, sITPDocumentSectionTableName, giSecureFlag, ref dNewVersionNumber, ref dtLastVersionDate);
				
				if (!DB.TableExists(sITPDocumentSectionTableName) || bNewVersion)
				{
					clsLocalUtils util = new clsLocalUtils();
                    util.SetSecureFlag(giSecureFlag);
					string sURL = util.GetEnvironment_wbsURL("wbsITP_External");
					wbsITP_External ws = new wbsITP_External();
					ws.Url = sURL;
					object[] objQuestions = ws.GetITPFullDocumentSectionInfo(sSessionId, sUser);
					if (objQuestions[0].ToString() == "Success")
					{
						if (ITPDocSection.TableITPDocumentSectionDeleteAllRecords(ref sRtnMsg))
						{
							string sITPDocumentQuestionnaireInfo = objQuestions[1].ToString();
							string[] sHeaderInfo = sITPDocumentQuestionnaireInfo.Split('~');
							if (sHeaderInfo[0] == "ITPDocumentSectionInfo")
							{
								string[] delimiters = new string[] { "||" };
								string[] sDocSectionItems = sHeaderInfo[1].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
								int iQuestionCount = sDocSectionItems.Length;
								if (iQuestionCount > 0)
								{
									this.InvokeOnMainThread(() => { progBarSectionsVw.ShowProgressBar(iQuestionCount); });
									//First check if the ITPType table exists and if not create it
									if (ITPDocSection.CheckFullDocumentSectionTable())
									{
										for (int i = 0; i < iQuestionCount; i++)
										{
											string[] delimiters2 = new string[] { "^" };
											string[] sQuestionItems = sDocSectionItems[i].Split(delimiters2, StringSplitOptions.None);
											ITPDocSection.TableITPDocumentSectionAddRecord(sQuestionItems);
											this.InvokeOnMainThread(() => { progBarSectionsVw.UpdateProgressBar(i + 1); });
										}
									}
								}
							}
						}
						
						//Update the version number locally
						Static.UpdateVersionNumber(sITPDocumentSectionTableName, dNewVersionNumber);
						this.InvokeOnMainThread(() => { progBarSectionsVw.CloseProgressBar(); });
						return true;
					}
					else
					{
						sRtnMsg = objQuestions[1].ToString();
						return false;
					}
				}
				else
				{
					//This means you don't have to fill this static table
					return true;
				}
			}
			catch (Exception ex)
			{
				sRtnMsg = "Failure" + ex.Message.ToString();
				return false;
			}
		}

		public bool FillITPInventoryMainTable(string sSessionId, string sUser, ref string sRtnMsg)
		{
			try
			{
				clsTabletDB.ITPStaticTable Static = new clsTabletDB.ITPStaticTable();
				clsTabletDB.ITPInventory ITPInventory = new clsTabletDB.ITPInventory();
				LocalDB DB = new LocalDB();
				double dNewVersionNumber = 0.0;
				DateTime dtLastVersionDate;
				DateTime dtToday = DateTime.Now;
				string sITPInventoryTableName = ITPInventory.sITPInventoryTableName;

				//Only do all of this if the version has changed. So get the local version number and compare to that on the DB. If different do all of this. - WRITE LATER as a general function
                bool bNewVersion = Static.IsNewVersionOfTable(sSessionId, sUser, sITPInventoryTableName, giSecureFlag, ref dNewVersionNumber, ref dtLastVersionDate);
				TimeSpan ts = dtToday - dtLastVersionDate;
				int iDaysSinceUpdate = ts.Days;
				
				if (!DB.TableExists(sITPInventoryTableName) || bNewVersion || iDaysSinceUpdate >= 30)
				{
					clsLocalUtils util = new clsLocalUtils();
                    util.SetSecureFlag(giSecureFlag);
					string sURL = util.GetEnvironment_wbsURL("wbsITP_External");
					wbsITP_External ws = new wbsITP_External();
					ws.Url = sURL;
					object[] objInventory = ws.GetITPInventoryInfo(sSessionId, sUser);
					if (objInventory[0].ToString() == "Success")
					{
						if (ITPInventory.TableITPInventoryDeleteAllRecords(ref sRtnMsg))
						{
							string sITPInventoryInfo = objInventory[1].ToString();
							string[] sHeaderInfo = sITPInventoryInfo.Split('~');
							if (sHeaderInfo[0] == "ITPInventoryMakeAndModelInfo")
							{
								string[] delimiters = new string[] { "||" };
								string[] sITPInventoryItems = sHeaderInfo[1].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
								int iInventoryCount = sITPInventoryItems.Length;
								if (iInventoryCount > 0)
								{
									this.InvokeOnMainThread(() => { progBarInventoryVw.ShowProgressBar(iInventoryCount); });
									//First check if the ITPInventory table exists and if not create it
									if (ITPInventory.CheckFullITPInventoryTable())
									{
										for (int i = 0; i < iInventoryCount; i++)
										{
											string[] delimiters2 = new string[] { "^" };
											string[] sInventoryItems = sITPInventoryItems[i].Split(delimiters2, StringSplitOptions.None);
											ITPInventory.TableITPInventoryAddRecord(sInventoryItems);
											this.InvokeOnMainThread(() => { progBarInventoryVw.UpdateProgressBar(i + 1); });
										}
									}
								}
							}
						}
						//Update the version number locally
						Static.UpdateVersionNumber(sITPInventoryTableName, dNewVersionNumber);
						this.InvokeOnMainThread(() => { progBarInventoryVw.CloseProgressBar(); });
						return true;
					}
					else
					{
						sRtnMsg = objInventory[1].ToString();
						return false;
					}
				}
				else
				{
					//This means you don't have to fill this static table
					return true;
				}
			}
			catch (Exception ex)
			{
				sRtnMsg = "Failure" + ex.Message.ToString();
				return false;
			}
		}

        public bool FillITPBatteryFuseTypesTable(string sSessionId, string sUser, ref string sRtnMsg)
        {
            try
            {
                clsTabletDB.ITPStaticTable Static = new clsTabletDB.ITPStaticTable();
                clsTabletDB.ITPBatteryFuseTypes ITPBatteryFuseTypes = new clsTabletDB.ITPBatteryFuseTypes();
                LocalDB DB = new LocalDB();
                double dNewVersionNumber = 0.0;
                DateTime dtLastVersionDate;
                string sITPBatteryFuseTypesTableName = ITPBatteryFuseTypes.sITPBatteryFuseTypeTableName;
                
                //Only do all of this if the version has changed. So get the local version number and compare to that on the DB. If different do all of this. - WRITE LATER as a general function
                bool bNewVersion = Static.IsNewVersionOfTable(sSessionId, sUser, sITPBatteryFuseTypesTableName, giSecureFlag, ref dNewVersionNumber, ref dtLastVersionDate);

                if (!DB.TableExists(sITPBatteryFuseTypesTableName) || bNewVersion)
                {
                    clsLocalUtils util = new clsLocalUtils();
                    util.SetSecureFlag(giSecureFlag);
                    string sURL = util.GetEnvironment_wbsURL("wbsITP_External");
                    wbsITP_External ws = new wbsITP_External();
                    ws.Url = sURL;
                    object[] objBatteryFuseTypes = ws.GetITPBatteryFuseTypeInfo(sSessionId, sUser);
                    if (objBatteryFuseTypes[0].ToString() == "Success")
                    {
                        if (ITPBatteryFuseTypes.TableITPBatteryFuseTypeDeleteAllRecords(ref sRtnMsg))
                        {
                            string sITPBatteryFuseTypesInfo = objBatteryFuseTypes[1].ToString();
                            string[] sHeaderInfo = sITPBatteryFuseTypesInfo.Split('~');
                            if (sHeaderInfo[0] == "ITPBatteryFuseTypeInfo")
                            {
                                string[] delimiters = new string[] { "||" };
                                string[] sITPBatteryFuseTypesItems = sHeaderInfo[1].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                int iBatteryFuseTypesCount = sITPBatteryFuseTypesItems.Length;
                                if (iBatteryFuseTypesCount > 0)
                                {
                                    this.InvokeOnMainThread(() => { progBarBatteryFuseTypesVw.ShowProgressBar(iBatteryFuseTypesCount); });
                                    //First check if the ITPBatteryFuseTypes table exists and if not create it
                                    if (ITPBatteryFuseTypes.CheckFullITPBatteryFuseTypeTable())
                                    {
                                        for (int i = 0; i < iBatteryFuseTypesCount; i++)
                                        {
                                            string[] delimiters2 = new string[] { "^" };
                                            string[] sBatteryFuseTypesItems = sITPBatteryFuseTypesItems[i].Split(delimiters2, StringSplitOptions.None);
                                            string sFuseType = sBatteryFuseTypesItems[0];
                                            Array.Resize<string>(ref sBatteryFuseTypesItems, sBatteryFuseTypesItems.Length + 1);
                                            sBatteryFuseTypesItems[sBatteryFuseTypesItems.Length - 1] = sFuseType;
                                            sBatteryFuseTypesItems[0] = i.ToString();
                                            ITPBatteryFuseTypes.TableITPBatteryFuseTypeAddRecord(sBatteryFuseTypesItems);
                                            this.InvokeOnMainThread(() => { progBarBatteryFuseTypesVw.UpdateProgressBar(i + 1); });
                                        }
                                    }
                                }
                            }
                        }
                        //Update the version number locally
                        Static.UpdateVersionNumber(sITPBatteryFuseTypesTableName, dNewVersionNumber);
                        this.InvokeOnMainThread(() => { progBarBatteryFuseTypesVw.CloseProgressBar(); });
                        return true;
                    }
                    else
                    {
                        sRtnMsg = objBatteryFuseTypes[1].ToString();
                        return false;
                    }
                }
                else
                {
                    //This means you don't have to fill this static table
                    return true;
                }
            }
            catch (Exception ex)
            {
                sRtnMsg = "Failure" + ex.Message.ToString();
                return false;
            }
        }

        public bool FillITPBatteryCellInfoTable(string sSessionId, string sUser, ref string sRtnMsg)
        {
            try
            {
                clsTabletDB.ITPStaticTable Static = new clsTabletDB.ITPStaticTable();
                clsTabletDB.ITPBatteryCellInfo ITPBatteryCellInfo = new clsTabletDB.ITPBatteryCellInfo();
                LocalDB DB = new LocalDB();
                double dNewVersionNumber = 0.0;
                DateTime dtLastVersionDate;
                string sITPBatteryCellInfoTableName = ITPBatteryCellInfo.sITPBatteryCellInfoTableName;

                //Only do all of this if the version has changed. So get the local version number and compare to that on the DB. If different do all of this. - WRITE LATER as a general function
                bool bNewVersion = Static.IsNewVersionOfTable(sSessionId, sUser, sITPBatteryCellInfoTableName, giSecureFlag, ref dNewVersionNumber, ref dtLastVersionDate);

                if (!DB.TableExists(sITPBatteryCellInfoTableName) || bNewVersion)
                {
                    clsLocalUtils util = new clsLocalUtils();
                    util.SetSecureFlag(giSecureFlag);
                    string sURL = util.GetEnvironment_wbsURL("wbsITP_External");
                    wbsITP_External ws = new wbsITP_External();
                    ws.Url = sURL;
                    object[] objBatteryCellInfo = ws.GetITPBatteryCellInfo(sSessionId, sUser);
                    if (objBatteryCellInfo[0].ToString() == "Success")
                    {
                        if (ITPBatteryCellInfo.TableITPBatteryCellInfoDeleteAllRecords(ref sRtnMsg))
                        {
                            string sITPBatteryCellInfoInfo = objBatteryCellInfo[1].ToString();
                            string[] sHeaderInfo = sITPBatteryCellInfoInfo.Split('~');
                            if (sHeaderInfo[0] == "ITPBatteryCellInfo")
                            {
                                string[] delimiters = new string[] { "||" };
                                string[] sITPBatteryCellInfoItems = sHeaderInfo[1].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                int iBatteryCellInfoCount = sITPBatteryCellInfoItems.Length;
                                if (iBatteryCellInfoCount > 0)
                                {
                                    this.InvokeOnMainThread(() => { progBarBatteryCellInfoVw.ShowProgressBar(iBatteryCellInfoCount); });
                                    //First check if the ITPBatteryCellInfo table exists and if not create it
                                    if (ITPBatteryCellInfo.CheckFullITPBatteryCellInfoTable())
                                    {
                                        for (int i = 0; i < iBatteryCellInfoCount; i++)
                                        {
                                            string[] delimiters2 = new string[] { "^" };
                                            string[] sBatteryCellInfoItems = sITPBatteryCellInfoItems[i].Split(delimiters2, StringSplitOptions.None);
                                            string sSPN = sBatteryCellInfoItems[0];
                                            string sBatteryType = sBatteryCellInfoItems[1];
                                            string sAmpereHours = sBatteryCellInfoItems[2];
                                            string sCellsPerBlock = sBatteryCellInfoItems[3];
                                            string sVoltsPerCell = sBatteryCellInfoItems[4];
                                            Array.Resize<string>(ref sBatteryCellInfoItems, sBatteryCellInfoItems.Length + 1);
                                            sBatteryCellInfoItems[sBatteryCellInfoItems.Length - 5] = sSPN;
                                            sBatteryCellInfoItems[sBatteryCellInfoItems.Length - 4] = sBatteryType;
                                            sBatteryCellInfoItems[sBatteryCellInfoItems.Length - 3] = sAmpereHours;
                                            sBatteryCellInfoItems[sBatteryCellInfoItems.Length - 2] = sCellsPerBlock;
                                            sBatteryCellInfoItems[sBatteryCellInfoItems.Length - 1] = sVoltsPerCell;
                                            sBatteryCellInfoItems[0] = i.ToString();
                                            ITPBatteryCellInfo.TableITPBatteryCellInfoAddRecord(sBatteryCellInfoItems);
                                            this.InvokeOnMainThread(() => { progBarBatteryCellInfoVw.UpdateProgressBar(i + 1); });
                                        }
                                    }
                                }
                            }
                        }
                        //Update the version number locally
                        Static.UpdateVersionNumber(sITPBatteryCellInfoTableName, dNewVersionNumber);
                        this.InvokeOnMainThread(() => { progBarBatteryCellInfoVw.CloseProgressBar(); });
                        return true;
                    }
                    else
                    {
                        sRtnMsg = objBatteryCellInfo[1].ToString();
                        return false;
                    }
                }
                else
                {
                    //This means you don't have to fill this static table
                    return true;
                }
            }
            catch (Exception ex)
            {
                sRtnMsg = "Failure" + ex.Message.ToString();
                return false;
            }
        }

        public bool FillITPValidHierarchyTable(string sSessionId, string sUser, ref string sRtnMsg)
        {
            try
            {
                clsTabletDB.ITPStaticTable Static = new clsTabletDB.ITPStaticTable();
                clsTabletDB.ITPValidHierarchy ITPValidHierarchy = new clsTabletDB.ITPValidHierarchy();
                LocalDB DB = new LocalDB();
                double dNewVersionNumber = 0.0;
                DateTime dtLastVersionDate;
                string sITPValidHierarchyTableName = ITPValidHierarchy.sITPValidHierarchyTableName;
                
                //Only do all of this if the version has changed. So get the local version number and compare to that on the DB. If different do all of this. - WRITE LATER as a general function
                bool bNewVersion = Static.IsNewVersionOfTable(sSessionId, sUser, sITPValidHierarchyTableName, giSecureFlag, ref dNewVersionNumber, ref dtLastVersionDate);

                if (!DB.TableExists(sITPValidHierarchyTableName) || bNewVersion)
                {
                    clsLocalUtils util = new clsLocalUtils();
                    util.SetSecureFlag(giSecureFlag);
                    string sURL = util.GetEnvironment_wbsURL("wbsITP_External");
                    wbsITP_External ws = new wbsITP_External();
                    ws.Url = sURL;
                    object[] objValidHierarchy = ws.GetITPValidHierarchyInfo(sSessionId, sUser);
                    if (objValidHierarchy[0].ToString() == "Success")
                    {
                        if (ITPValidHierarchy.TableITPValidHierarchyDeleteAllRecords(ref sRtnMsg))
                        {
                            string sITPValidHierarchyInfo = objValidHierarchy[1].ToString();
                            string[] sHeaderInfo = sITPValidHierarchyInfo.Split('~');
                            if (sHeaderInfo[0] == "ITPValidHierarchyInfo")
                            {
                                string[] delimiters = new string[] { "||" };
                                string[] sITPValidHierarchyItems = sHeaderInfo[1].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                int iValidHierarchyCount = sITPValidHierarchyItems.Length;
                                if (iValidHierarchyCount > 0)
                                {
                                    this.InvokeOnMainThread(() => { progBarValidHierarchyVw.ShowProgressBar(iValidHierarchyCount); });
                                    //First check if the ITPValidHierarchy table exists and if not create it
                                    if (ITPValidHierarchy.CheckFullITPValidHierarchyTable())
                                    {
                                        SqliteConnection conn = DB.OpenConnection();
                                        for (int i = 0; i < iValidHierarchyCount; i++)
                                        {
                                            string[] delimiters2 = new string[] { "^" };
                                            string[] sValidHierarchyItems = sITPValidHierarchyItems[i].Split(delimiters2, StringSplitOptions.None);
                                            string sFieldValue = sValidHierarchyItems[0];
                                            string sFieldType = sValidHierarchyItems[1];
                                            Array.Resize<string>(ref sValidHierarchyItems, sValidHierarchyItems.Length + 1);
                                            sValidHierarchyItems[sValidHierarchyItems.Length - 1] = sFieldType;
                                            sValidHierarchyItems[sValidHierarchyItems.Length - 2] = sFieldValue;
                                            sValidHierarchyItems[0] = i.ToString();
                                            ITPValidHierarchy.TableITPValidHierarchyAddRecord(sValidHierarchyItems, conn);
                                            this.InvokeOnMainThread(() => { progBarValidHierarchyVw.UpdateProgressBar(i + 1); });
                                        }
                                        DB.CloseConnection(conn);

                                    }
                                }
                            }
                        }
                        //Update the version number locally
                        Static.UpdateVersionNumber(sITPValidHierarchyTableName, dNewVersionNumber);
                        this.InvokeOnMainThread(() => { progBarValidHierarchyVw.CloseProgressBar(); });
                        return true;
                    }
                    else
                    {
                        sRtnMsg = objValidHierarchy[1].ToString();
                        return false;
                    }
                }
                else
                {
                    //This means you don't have to fill this static table
                    return true;
                }
            }
            catch (Exception ex)
            {
                sRtnMsg = "Failure" + ex.Message.ToString();
                return false;
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

		public bool HasConnectionStatus ()
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

