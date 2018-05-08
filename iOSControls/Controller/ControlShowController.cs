using System;
using UIKit;
using System.Linq;
using iOSControls.HelperClass;
using Foundation;
using CoreGraphics;
using ObjCRuntime;
using System.Diagnostics.Contracts;

using iOSControls.CollectionViewCells;

namespace iOSControls
{
    public partial class ControlShowController : UIViewController, IUITextFieldDelegate, IUITableViewDataSource, IUITableViewDelegate, IUICollectionViewDataSource, IUICollectionViewDelegate
    {
        public string selectedControlName;

        protected ControlShowController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

        }
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            this.initialUISetup();

        }
        private void initialUISetup()
        {
            this.showControl(selectedControlName);
            this.controlTitleLabel.Text = selectedControlName;
        }
        public void showControl(string controlName)
        {

            Console.WriteLine(controlName);

            switch (controlName)
            {
                case UICONTROL_NAMES.textField:

                    UITextField textField = new UITextField(new CoreGraphics.CGRect(25, this.View.Frame.Size.Height / 3, this.View.Frame.Size.Width - 50, this.View.Frame.Size.Height / 3));
                    textField.Text = "Hey I am TextField!";
                    textField.BorderStyle = UITextBorderStyle.Line;
                    textField.TextAlignment = UITextAlignment.Center;
                    this.View.AddSubview(textField);

                    break;
                case UICONTROL_NAMES.inputTextField:
                    UITextField inputTextField = new UITextField(new CoreGraphics.CGRect(25, this.View.Frame.Size.Height / 3, this.View.Frame.Size.Width - 50, 50));
                    inputTextField.BorderStyle = UITextBorderStyle.RoundedRect;
                    inputTextField.TextAlignment = UITextAlignment.Center;
                    inputTextField.Placeholder = "Enter your name";
                    inputTextField.Delegate = this;
                    inputTextField.WeakDelegate = this;

                    inputTextField.AddTarget((sender, e) =>
                    {
                        Console.WriteLine("Editing Begin");
                    }, UIControlEvent.EditingDidBegin);

                    inputTextField.AddTarget((sender, e) =>
                    {
                        Console.WriteLine("Editing changed");
                    }, UIControlEvent.EditingChanged);


                    this.View.AddSubview(inputTextField);
                    break;

                case UICONTROL_NAMES.button:

                    UIButton button = new UIButton(new CoreGraphics.CGRect(25, this.View.Frame.Size.Height / 3, this.View.Frame.Size.Width - 50, 50));
                    button.SetTitleColor(UIColor.Black, UIControlState.Normal);
                    button.SetTitle("Sample Button", UIControlState.Normal);
                    button.Layer.BorderColor = UIColor.Brown.CGColor;
                    button.Layer.BorderWidth = 10;
                    button.TitleLabel.TextColor = UIColor.Blue;
                    button.TouchUpInside += delegate
                    {
                        new UIAlertView("Touch1", "TouchUpInside handled", null, "OK", null).Show();
                    };
                    this.View.AddSubview(button);


                    break;
                case UICONTROL_NAMES.toolBar:

                    UIToolbar toolbar = new UIToolbar(new CoreGraphics.CGRect(25, this.View.Frame.Size.Height / 3, this.View.Frame.Size.Width - 50, 50));
                    toolbar.Alpha = 1;

                    UIBarButtonItem barButtonItem = new UIBarButtonItem("Bar1", UIBarButtonItemStyle.Done, null);
                    barButtonItem.Title = "Bar1";

                    UIBarButtonItem barButtonItem2 = new UIBarButtonItem("Bar1", UIBarButtonItemStyle.Bordered, null);
                    barButtonItem.Title = "Bar2";


                    var items = new UIBarButtonItem[2];
                    items[0] = barButtonItem;
                    items[1] = barButtonItem2;

                    toolbar.SetItems(items, true);
                    this.View.AddSubview(toolbar);
                    break;

                case UICONTROL_NAMES.statusBar:

                    UINavigationBar nav = this.NavigationController?.NavigationBar;

                    nav.BarTintColor = UIColor.Red;
                    nav.TintColor = UIColor.White;
                    nav.TitleTextAttributes = new UIStringAttributes()
                    {
                        ForegroundColor = UIColor.Red,
                        KerningAdjustment = 3
                    };


                    this.SetNeedsStatusBarAppearanceUpdate();
                    UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;


                    break;
                case UICONTROL_NAMES.navigationBar:
                    UINavigationBar navigationBar = this.NavigationController?.NavigationBar;
                    navigationBar.BarTintColor = UIColor.LightGray;
                    //navigationBar.TintColor = UIColor.White;
                    navigationBar.TopItem.Title = "Sample Nav";

                    break;
                case UICONTROL_NAMES.tabBar:

                    UITabBar tabBar = new UITabBar(new CoreGraphics.CGRect(25, this.View.Frame.Size.Height / 3, this.View.Frame.Size.Width - 50, 50));

                    var tabBarItems = new UITabBarItem[3];
                    for (int i = 0; i < 3; i++)
                    {

                        var tabBarItem = new UITabBarItem("TAB " + i, null, i);
                        tabBarItems[i] = tabBarItem;
                    }

                    tabBar.Items = tabBarItems;
                    tabBar.ItemSpacing = 10;

                    tabBar.ItemSelected += (object sender, UITabBarItemEventArgs e) =>
                    {
                        Console.WriteLine($"{e.Item} has selected");
                        if (e.Item.Tag == 0)
                        {

                            new UIAlertView(e.Item.Tag.ToString(), "ItemSelected handled", null, "OK", null).Show();
                        }
                        else if (e.Item.Tag == 1)
                        {
                            new UIAlertView(e.Item.Tag.ToString(), "ItemSelected handled", null, "OK", null).Show();
                        }
                        else if (e.Item.Tag == 2)
                        {
                            new UIAlertView(e.Item.Tag.ToString(), "ItemSelected handled", null, "OK", null).Show();
                        }
                    };
                    this.View.AddSubview(tabBar);

                    break;
                case UICONTROL_NAMES.imageView:
                    UIImageView imageView = new UIImageView(new CoreGraphics.CGRect(25, this.View.Frame.Size.Height / 3, this.View.Frame.Size.Width - 50, this.View.Frame.Size.Height / 3));
                    imageView.BackgroundColor = UIColor.Blue;
                    imageView.Image = UIImage.FromFile("mac.png");
                    this.View.AddSubview(imageView);
                    break;

                case UICONTROL_NAMES.scrollView:
                    UIScrollView scrollView = new UIScrollView(new CoreGraphics.CGRect(10, 100, this.View.Frame.Size.Width - 20, this.View.Frame.Size.Height * 1.5));
                    scrollView.AlwaysBounceVertical = true;
                    CGSize size = new CGSize(this.View.Frame.Size.Width - 50, this.View.Frame.Size.Height * 1.5);
                    scrollView.ContentSize = size;
                    scrollView.BackgroundColor = UIColor.LightGray;

                    UILabel scrollText = new UILabel();
                    scrollText.Frame = new CoreGraphics.CGRect(5, scrollView.Frame.Y, scrollView.Frame.Size.Width - 10, scrollView.Frame.Size.Height - 50);
                    scrollText.Text = "By the 4th century BCE, the late Achaemenid period, the inscriptions of Artaxerxes II and Artaxerxes III differ enough from the language of Darius' inscriptions to be called a pre-Middle Persian, or post-Old Persian.[14] Old Persian subsequently evolved into Middle Persian, which is in turn the ancestor of New Persian. Professor Gilbert Lazard, a famous Iranologist and the author of the book Persian Grammar states: The language known as New Persian, which usually is called at this period (early Islamic times) by the name of Parsi-Dari, can be classified linguistically as a continuation of Middle Persian, the official religious and literary language of Sassanian Iran, itself a continuation of Old Persian, the language of the Achaemenids. Unlike the other languages and dialects, ancient and modern, of the Iranian group such as Avestan, Parthian, Soghdian, Kurdish, Pashto, etc., Old, Middle and New Persian represent one and the same language at three states of its history. It had its origin in Fars and is differentiated by dialectical features, still easily recognizable from the dialect prevailing in north-western and eastern Iran. Middle Persian, also sometimes called Pahlavi, is a direct continuation of old Persian and was used as the written official language of the country.[16][17] Comparison of the evolution at each stage of the language shows great simplification in grammar and syntax. However, New Persian is a direct descendent of Middle and Old Persian.[18]";
                    scrollText.Lines = 0;

                    scrollView.AddSubview(scrollText);
                    this.View.AddSubview(scrollView);
                    break;
                case UICONTROL_NAMES.tableView:

                    UITableView tableView = new UITableView();
                    tableView.Frame = new CGRect(0, 100, this.View.Frame.Size.Width, this.View.Frame.Size.Height);
                    tableView.DataSource = this;
                    tableView.Delegate = this;
                    tableView.AllowsSelection = true;
                    this.View.AddSubview(tableView);
                    break;
                case UICONTROL_NAMES.collectionView:
                    UICollectionViewFlowLayout collectionViewFlowLayout = new UICollectionViewFlowLayout();

                    collectionViewFlowLayout.ItemSize = new CGSize(this.View.Bounds.Size.Width, this.View.Bounds.Size.Height);

                    UICollectionView collectionView = new UICollectionView(frame: this.View.Bounds, layout: collectionViewFlowLayout);
                    collectionView.Delegate = this;
                    collectionView.DataSource = this;
                    collectionView.BackgroundColor = UIColor.Cyan;


                    collectionView.RegisterClassForCell(typeof(MyCollectionViewCell), "MyCollectionViewCell");

                    this.View.AddSubview(collectionView);

                    break;
                case UICONTROL_NAMES.splitView:
                    Console.WriteLine("Better try again");
                    break;
                case UICONTROL_NAMES.textView:
                    Console.WriteLine("Better try again");
                    break;
                case UICONTROL_NAMES.viewTransition:
                    Console.WriteLine("Better try again");
                    break;
                case UICONTROL_NAMES.picker:
                    Console.WriteLine("Better try again");
                    break;
                case UICONTROL_NAMES.switches:
                    Console.WriteLine("Better try again");
                    break;
                case UICONTROL_NAMES.sliders:
                    Console.WriteLine("Better try again");
                    break;
                case UICONTROL_NAMES.alerts:
                    Console.WriteLine("Better try again");
                    break;

                default:
                    Console.WriteLine("Invalid grade");
                    break;
            }

        }

        //Input Textfield Delegate Methods

        [Foundation.Export("textFieldShouldBeginEditing:")]
        public virtual Boolean ShouldBeginEditing(UITextField textField)
        {

            return true;
        }

        [Export("textFieldDidBeginEditing:")]
        public virtual void EditingStarted(UITextField textField)
        {

            Console.WriteLine(textField.Text);

        }

        [Foundation.Export("textFieldDidEndEditing:")]
        public virtual void EditingEnded(UITextField textField)
        {

            Console.WriteLine(textField.Text);
        }

        [Foundation.Export("textFieldShouldReturn:")]
        public virtual Boolean ShouldReturn(UITextField textField)
        {

            return true;
        }

        [Export("textField:shouldChangeCharactersInRange:replacementString:")]
        public bool ShouldChangeCharacters(UITextField textField, NSRange range, string replacementString)
        {

            return true;
        }

        //UITableViewDataSource & UITableViewDelegate
        [Export("tableView:heightForRowAtIndexPath:")]
        public nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 70;
        }
        [Export("numberOfSectionsInTableView:")]
        public nint NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            new UIAlertView("Row Touched : " + indexPath.Row.ToString(), "RowSelected handled", null, "OK", null).Show();
        }
        public nint RowsInSection(UITableView tableView, nint section)
        {
            return 30;
        }

        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = new UITableViewCell();


            UILabel label = new UILabel(new CGRect(30, cell.ContentView.Frame.Size.Height / 3, cell.ContentView.Frame.Size.Width - 50, cell.ContentView.Frame.Size.Height / 3));
            label.Text = "Item At Index : " + indexPath.Row.ToString();

            cell.ContentView.AddSubview(label);
            return cell;
        }

        //CollectionViewDataSource
        public nint GetItemsCount(UICollectionView collectionView, nint section)
        {
            return 12;
        }

        public UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            //var cell = collectionView.DequeueReusableCell("MyCollectionViewCell", indexPath);

            var cell = (MyCollectionViewCell)collectionView.DequeueReusableCell("MyCollectionViewCell", indexPath);

            cell.ContentView.BackgroundColor = UIColor.White;
            UILabel label = new UILabel(new CGRect(30, cell.ContentView.Frame.Size.Height / 3, cell.ContentView.Frame.Size.Width - 50, cell.ContentView.Frame.Size.Height / 3));
            label.Text = "Item At Index : " + indexPath.Item.ToString();

            cell.ContentView.AddSubview(label);




            return cell;
        }


        //CollectionView Delegate
        [Export("collectionView:didSelectItemAtIndexPath:")]
        public void ItemSelected(UICollectionView collectionView, NSIndexPath indexPath)
        {
            new UIAlertView("Item Touched : " + indexPath.Row.ToString(), "Item Selcted handled", null, "OK", null).Show();
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


    }
}

