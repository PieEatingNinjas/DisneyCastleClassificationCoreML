// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;

namespace DisneyCastlesCoreML
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton btnAnalyze { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView image { get; set; }

        [Action ("Analyze_Clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void Analyze_Clicked (UIKit.UIButton sender);

        [Action ("LoadImage_Clicked:")]
        [GeneratedCode ("iOS Designer", "1.0")]
        partial void LoadImage_Clicked (UIKit.UIButton sender);

        void ReleaseDesignerOutlets ()
        {
            if (btnAnalyze != null) {
                btnAnalyze.Dispose ();
                btnAnalyze = null;
            }

            if (image != null) {
                image.Dispose ();
                image = null;
            }
        }
    }
}