using System;
using Foundation;
using UIKit;
using CoreGraphics;

namespace iOSControls
{
    public partial class ViewController : UIViewController, IUITableViewDataSource, IUITableViewDelegate
    {
        string[] controls;
        string selectedControlString;

        protected ViewController(IntPtr handle) : base(handle)
        {

        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.initialUISetup();
            this.controlsListTableview.DataSource = this;
            this.controlsListTableview.Delegate = this;
            this.controlsListTableview.AllowsSelection = true;


        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.initialUISetup();
        }
        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);
        }
        private void initialUISetup()
        {

            this.controlsListTableview.TableFooterView = new UIView(frame: CGRect.Empty);
            controls = new string[] { "Text Fields",
                "Input types - TextFields",
                "Buttons",
                "Label",
                "Toolbar",
                "Status Bar",
                "Navigation Bar",
                "Tab bar",
                "Image View",
                "Scroll View",
                "Table View",
                "Collection View",
                "Split View",
                "Text View",
                "View Transition",
                "Pickers",
                "Switches",
                "Sliders",
                "Alerts",

            };


            //navigationbar change
            this.NavigationController.NavigationBar.BarTintColor = UIColor.White;
            this.NavigationController.NavigationBar.TintColor = UIColor.White;

            this.SetNeedsStatusBarAppearanceUpdate();
            UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }

        public nint RowsInSection(UITableView tableView, nint section)
        {
            if (controls != null)
            {
                return controls.Length;
            }
            else
            {
                return 0;
            }

        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            string cellIdentifier = "ControlsListTableCellID";
            var cell = tableView.DequeueReusableCell(cellIdentifier) as ControlsListTableCell;
            cell.controlTitleLabel.Text = controls[indexPath.Row];

            return cell;

        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public virtual void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine("Row Selected");

            selectedControlString = controls[indexPath.Row].ToString();
            this.PerformSegue("segueToControlShowController", this);

        }

		public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
		{
            base.PrepareForSegue(segue, sender);

            // do first a control on the Identifier for your segue
            if (segue.Identifier.Equals("segueToControlShowController"))
            {
                var viewController = (ControlShowController)segue.DestinationViewController;
                viewController.selectedControlName = selectedControlString;
            }
		}

	}
}
