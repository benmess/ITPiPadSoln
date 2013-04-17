
using System;
using System.Drawing;
using System.Net;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using nspTabletCommon;	
using clsTabletCommon.ITPExternal;
using clsiOS;

namespace ITPiPadSoln
{
	public partial class LoginScreen : UIViewController
	{
		public LoginScreen () : base ("LoginScreen", null)
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
			BuildLoginScreen();
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

		public void BuildLoginScreen()
		{
			//create a login text box and password text box and button
			UIView[] arrItems = new UIView[6];

			UILabel lblLogin = new UILabel();
			lblLogin.Tag = 500;
			lblLogin.Frame = new RectangleF(120f,100f,100f,30f);
			lblLogin.Text = "Login Name:";
			arrItems[0] = lblLogin;

			UITextField txtLogin = new UITextField();
			txtLogin.BorderStyle = UITextBorderStyle.RoundedRect;
			txtLogin.Tag = 1000;
			txtLogin.Frame = new RectangleF(230f,100f,200f,30f);
			txtLogin.AutocapitalizationType = UITextAutocapitalizationType.None;
			arrItems[1] = txtLogin;

			UILabel lblPass = new UILabel();
			lblPass.Tag = 1500;
			lblPass.Frame = new RectangleF(120f,150f,100f,30f);
			lblPass.Text = "Password:";
			arrItems[2] = lblPass;

			UITextField txtPass = new UITextField();
			txtPass.BorderStyle = UITextBorderStyle.RoundedRect;
			txtPass.Tag = 2000;
			txtPass.Frame = new RectangleF(230f,150f,200f,30f);
			txtPass.AutocapitalizationType = UITextAutocapitalizationType.None;
			txtPass.SecureTextEntry = true;
			//txtPass.EnablesReturnKeyAutomatically = true;
			txtPass.AllTouchEvents += (sender, e) => {DetectEnterKey(sender,e);};
			arrItems[3] = txtPass;

			var btnLogin = UIButton.FromType(UIButtonType.RoundedRect);
			btnLogin.Frame = new RectangleF(200f,200f,100f,30f);
			btnLogin.SetTitle("Login", UIControlState.Normal);
			btnLogin.TouchUpInside += (sender,e) => {LogintoSCMS(sender, e);};
			arrItems[4] = btnLogin;

			UILabel hfLoggedIn = new UILabel();
			hfLoggedIn.Tag = 3000;
			hfLoggedIn.Frame = new RectangleF(120f,200f,100f,30f);
			hfLoggedIn.Hidden = true;
			arrItems[5] = hfLoggedIn;

			View.AddSubviews(arrItems);

			txtLogin.BecomeFirstResponder();
		}

		public void LogintoSCMS (object sender, EventArgs e)
		{
			var txtName = (UITextField)View.ViewWithTag (1000);
			string sName = txtName.Text;

			var txtPass = (UITextField)View.ViewWithTag (2000);
			string sPass = txtPass.Text;

            try{
			clsLocalUtils util = new clsLocalUtils ();
			string sURL = util.GetEnvironment_wbsURL ("wbsITP_Exernal");
			wbsITP_External ws = new wbsITP_External ();
			ws.Url = sURL;
			ws.CookieContainer = new CookieContainer ();
//                object[] sTest = ws.GetITPBatteryFuseTypeInfo("","gmorris");
			string sSessionId = ws.CookieLogin (sName, sPass);

			HomeScreen homeScreen = (HomeScreen)NavigationController.ViewControllers [0];
			if (sSessionId != "") {
				var hfLoggedIn = (UILabel)View.ViewWithTag (3000);
				hfLoggedIn.Text = "1";
				homeScreen.SetLoginName (sName);
				homeScreen.SetSessionId (sSessionId);
				homeScreen.SetLoggedInStatus("1");
				NavigationController.PopToViewController (homeScreen, true);
				//this.Dispose();

			} 
			else 
			{
				iUtils.AlertBox alert = new iUtils.AlertBox();
				alert.CreateErrorAlertDialog("Incorrect username and/or password");
				homeScreen.SetLoginName ("");
				homeScreen.SetSessionId ("");
				homeScreen.SetLoggedInStatus("0");
			}
            }
            catch(Exception ex)
            {
                string sRtn = ex.Message.ToString();
            }

		}

		public void DetectEnterKey(object sender, EventArgs e)
		{

		}
	}
}

