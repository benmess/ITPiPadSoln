
using System;
using System.Drawing;
using System.Net;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.SystemConfiguration;
using MonoTouch.CoreFoundation;

using clsiOS;

namespace ITPiPadSoln
{
	public partial class HomeScreen : UIViewController
	{
		string m_sLogin = "Not logged in to SCMS";
		string m_sSessionId = "";
		string m_sLoggedIn = "";
		UIImage imageRedBlock = UIImage.FromFile("Images/Red_Block.jpg");
		UIImage imageGreenBlock = UIImage.FromFile("Images/Green_Block.JPG");

		public HomeScreen () : base ("HomeScreen", null)
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
			//Add 4 buttons on the main home page
//			UIScrollView vwScroll = new UIScrollView();
//			vwScroll.Frame = new RectangleF(0f, 0f, 1000f, 1000f);
//			vwScroll.MaximumZoomScale = 5.0f;
//			vwScroll.MinimumZoomScale = 0.5f;

			UIView[] arrButts = new UIView[10];


			UILabel lblLogin = new UILabel ();
			lblLogin.Tag = 500;
			lblLogin.Frame = new RectangleF (120f, 100f, 100f, 30f);
			lblLogin.Text = "Login Name:";
			arrButts [0] = lblLogin;

			UILabel lblLoginName = new UILabel ();
			lblLoginName.Tag = 600;
			lblLoginName.Frame = new RectangleF (230f, 100f, 200f, 30f);
			lblLoginName.Text = m_sLogin;
			arrButts [1] = lblLoginName;

			UILabel lblConnStatus = new UILabel ();
			lblConnStatus.Tag = 650;
			lblConnStatus.Frame = new RectangleF (450f, 100f, 150f, 30f);
			lblConnStatus.Text = "Connection Status:";
			arrButts [2] = lblConnStatus;

			var bConnStatus = GetConnectionStatus ();
			UIImageView imgConnectionStatus = new UIImageView ();
			if (!bConnStatus) 
			{
				imgConnectionStatus.Image = imageRedBlock;
			} 
			else 
			{
				imgConnectionStatus.Image = imageGreenBlock;
			}
			imgConnectionStatus.Frame = new RectangleF (620f, 100f, 30f, 30f);
			arrButts[3] = imgConnectionStatus;

			UILabel hfSessionId = new UILabel();
			hfSessionId.Tag = 700;
			hfSessionId.Frame = new RectangleF(230f,100f,200f,30f);
			hfSessionId.Text = m_sSessionId;
			hfSessionId.Hidden = true;
			arrButts[4] = hfSessionId;

			UILabel hfLoggedIn = new UILabel();
			hfLoggedIn.Tag = 800;
			hfLoggedIn.Frame = new RectangleF(230f,100f,200f,30f);
			hfLoggedIn.Text = m_sLoggedIn;
			hfLoggedIn.Hidden = true;
			arrButts[5] = hfLoggedIn;

			var btnExit = UIButton.FromType(UIButtonType.Custom);
			btnExit.Frame = new RectangleF(120f,150f,100f,30f);
			btnExit.SetTitle("Exit App", UIControlState.Normal);
			btnExit.BackgroundColor = UIColor.FromRGBA(255,70,74,250);
			btnExit.SetTitleColor(UIColor.White,UIControlState.Normal);
			btnExit.Font = UIFont.FromName("Verdana-Bold", 12f);
			btnExit.Layer.CornerRadius = 8;
			btnExit.TouchUpInside += (sender,e) => {ExitApp(sender, e);};
			arrButts[6] = btnExit;

			var btnLogin = UIButton.FromType(UIButtonType.RoundedRect);
			btnLogin.Frame = new RectangleF(230f,150f,100f,30f);
			btnLogin.SetTitle("Login", UIControlState.Normal);
			btnLogin.TouchUpInside += (sender,e) => {OpenLoginScreen(sender, e);};
			arrButts[7] = btnLogin;

			var btnDownload = UIButton.FromType(UIButtonType.RoundedRect);
			btnDownload.Frame = new RectangleF(340f,150f,200f,30f);
			btnDownload.SetTitle("Download ITPs", UIControlState.Normal);
			btnDownload.TouchUpInside += (sender,e) => {OpenDownloadITPScreen(sender, e);};
			arrButts[8] = btnDownload;

			var btnOpenDownload = UIButton.FromType(UIButtonType.RoundedRect);
			btnOpenDownload.Frame = new RectangleF(550f,150f,250f,30f);
			btnOpenDownload.SetTitle("Open Downloaded ITPs", UIControlState.Normal);
			btnOpenDownload.TouchUpInside += (sender,e) => {OpenDownloadedITPsScreen(sender, e);};
			arrButts[9] = btnOpenDownload;
			
			View.AddSubviews(arrButts);
//			View.Add(vwScroll);

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

		public void ExitApp (object sender, EventArgs e)
		{
			//this.Dispose();
			System.Environment.Exit(0);
		}

		public void OpenDownloadITPScreen (object sender, EventArgs e)
		{
			var hfLoggedIn = (UILabel)View.ViewWithTag (800);
			string sLoginStatus = hfLoggedIn.Text;

			if (sLoginStatus == "1") 
			{
				DownloadITPScreen downloadScreen = new DownloadITPScreen ();
				this.NavigationController.PushViewController (downloadScreen, true);
			} 
			else 
			{
				LoginScreen loginScreen = new LoginScreen ();
				this.NavigationController.PushViewController (loginScreen, true);
			}
		}

		public void OpenDownloadedITPsScreen (object sender, EventArgs e)
		{
			DownloadedITPsScreen downloadScreen = new DownloadedITPsScreen ();
			this.NavigationController.PushViewController (downloadScreen, true);
		}

		public void OpenLoginScreen(object sender, EventArgs e)
		{
			LoginScreen loginScreen = new LoginScreen();
			this.NavigationController.PushViewController(loginScreen, true);
		}

		public void SetLoginName(string sLogin)
		{
			m_sLogin = sLogin;
			var txtName = (UILabel)View.ViewWithTag (600);
			txtName.Text = m_sLogin;
		}

		public string GetLoginName()
		{
			var txtName = (UILabel)View.ViewWithTag (600);
			return txtName.Text;
		}

		public void SetSessionId(string sSessionId)
		{
			m_sSessionId = sSessionId;
			var hfSessionId = (UILabel)View.ViewWithTag (700);
			hfSessionId.Text = m_sSessionId;
		}
	
		public string GetSessionId()
		{
			var hfSessionId = (UILabel)View.ViewWithTag (700);
			return hfSessionId.Text;
		}

		public void SetLoggedInStatus(string sStatus)
		{
			m_sLoggedIn = sStatus;
			var hfLoggedIn = (UILabel)View.ViewWithTag (800);
			hfLoggedIn.Text = m_sLoggedIn;
		}

		public string GetLoggedInStatus()
		{
			var hfLoggedIn = (UILabel)View.ViewWithTag (800);
			return hfLoggedIn.Text;
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



