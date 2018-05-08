// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace iOSControls
{
    [Register ("ControlsListTableCell")]
    partial class ControlsListTableCell
    {
        [Outlet]
        public UIKit.UILabel controlTitleLabel { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (controlTitleLabel != null) {
                controlTitleLabel.Dispose ();
                controlTitleLabel = null;
            }
        }
    }
}