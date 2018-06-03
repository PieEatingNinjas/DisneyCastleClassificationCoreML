using System;
using CoreFoundation;
using CoreML;
using Foundation;
using UIKit;
using Vision;


namespace DisneyCastlesCoreML
{
    public partial class ViewController : UIViewController
    {
        UIImagePickerController picker;
        VNCoreMLRequest ClassificationRequest;

        UIImage pickedImage = null;

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            LoadMLModel();
        }

        void LoadMLModel()
        {
            // Load the ML model
            var assetPath = NSBundle.MainBundle.GetUrlForResource("44105f291f4648b2b0ad7d42d639cb20", "mlmodelc");
            var mlModel = MLModel.Create(assetPath, out NSError mlErr);
            var vModel = VNCoreMLModel.FromMLModel(mlModel, out NSError vnErr);

            ClassificationRequest = new VNCoreMLRequest(vModel, HandleClassification);
        }

        void HandleClassification(VNRequest request, NSError error)
        {
            try
            {
                var observations = request.GetResults<VNClassificationObservation>();

                var best = observations[0]; // first/best classification result

                DispatchQueue.MainQueue.DispatchAsync(() =>
                {
                    var alert = UIAlertController.Create($"We think it's the castle in {best.Identifier}",
                                                         $"We are { best.Confidence * 100f:#.00}% confident",
                                                         UIAlertControllerStyle.Alert);
                    alert.AddAction(UIAlertAction.Create("Thanks!", UIAlertActionStyle.Default, (obj) => { }));
                    PresentViewController(alert, true, null);
                });
            }
            catch (Exception ex)
            {
                //¯\_(ツ)_/¯
            }
        }

        partial void LoadImage_Clicked(UIButton sender)
        {
            picker = new UIImagePickerController();
            picker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            picker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
            picker.FinishedPickingMedia += Picker_FinishedPickingMedia;
            PresentViewController(picker, animated: true, completionHandler: null);
        }

        void Picker_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
        {
            NSUrl referenceURL = e.Info[new NSString("UIImagePickerControllerReferenceUrl")] as NSUrl;

            pickedImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
            if (pickedImage != null)
            {
                image.Image = pickedImage;
                btnAnalyze.Hidden = false;
            }
            picker.DismissModalViewController(true);
        }

        partial void Analyze_Clicked(UIButton sender)
        {
            //load image
            var handler = new VNImageRequestHandler(pickedImage.CGImage, new VNImageOptions());
            DispatchQueue.DefaultGlobalQueue.DispatchAsync(() =>
            {
                handler.Perform(new VNRequest[] { ClassificationRequest }, out NSError err);
            });
        }
    }
}
