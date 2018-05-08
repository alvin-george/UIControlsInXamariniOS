using System;

using Foundation;
using UIKit;

namespace iOSControls
{
    public partial class ControlsListTableCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ControlsListTableCell");
        public static readonly UINib Nib;

        static ControlsListTableCell()
        {
            Nib = UINib.FromName("ControlsListTableCell", NSBundle.MainBundle);
        }

        protected ControlsListTableCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
    }
}
