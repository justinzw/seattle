using Battlehack.Models;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.BackgroundRemoval;
using Microsoft.Speech.Recognition;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace Battlehack
{
    /// <summary>
    /// Interaction logic for ModelTransform.xaml
    /// </summary>
    public partial class ModelTransform : Page
    {
        private CloudStorageAccount storageAccount;
        private Joint? previousJoint;

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0)
            {
                switch (e.Result.Text.ToLowerInvariant())
                {
                    case "design":
                        // this.sensor.AllFramesReady -= SensorAllFramesReady;
                        // viewModel = new TransformViewModel(view1, 0);
                        // viewModel.FileOpen();
                        // this.DataContext = viewModel;
                        //((TransformViewModel)this.DataContext).FileOpen();
                        // this.EnableSensor(this.sensor);
                        // ChooseSkeleton();
                        viewModel.ChangeShirt();
                        SmsText.Text = String.Format("SMS 384220{0} to 206-745-481", viewModel.currentindex);
                        break;
                    case "background":
                        if (this.SpaceNeedle.Visibility == System.Windows.Visibility.Hidden)
                        {
                            this.MaskedColor3.Visibility = System.Windows.Visibility.Hidden;
                            this.SpaceNeedle.Visibility = System.Windows.Visibility.Visible;
                            this.Macklemore.Visibility = System.Windows.Visibility.Hidden;
                        }
                        else if (this.Macklemore.Visibility == System.Windows.Visibility.Hidden)
                        {
                            this.SpaceNeedle.Visibility = System.Windows.Visibility.Hidden;
                            this.Macklemore.Visibility = System.Windows.Visibility.Visible;
                        }
                        break;
                    case "photo":
                        this.TakeScreenShot();
                        break;
                }
                Console.WriteLine("Speech recognized: " + e.Result.Text);
            }
        }
        private void ModelTransformLoaded(object sender, RoutedEventArgs e)
        {

            var window = Window.GetWindow(this);
            window.KeyDown += MainWindow_KeyDown;
        }

        public ModelTransform()
        {
            InitializeComponent();

            // Register a handler for the SpeechRecognized event.
            Speech.Engine.SpeechRecognized += sre_SpeechRecognized;
            // Start asynchronous, continuous speech recognition.
            Speech.Engine.RecognizeAsync(RecognizeMode.Multiple);

            storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=battlehackfinal;AccountKey=M/8ixre2UT2TWaa4CmnhknWpEchbpFi6qNQ8bn9LG9OJlWWDzM6xMNZBkNmDtN0M78fNjQ6KW7aksn+oO8yZzw==");
            this.sensor = KinectSensor.KinectSensors[0];

            if (null != this.sensor)
            {

                this.EnableSensor(sensor);


                // Start the sensor!
                try
                {
                    this.sensor.Start();
                }
                catch (IOException)
                {
                    this.sensor = null;
                }
            }


            



            //// initialize the sensor chooser and UI
            //this.sensorChooser = new KinectSensorChooser();
            //this.sensorChooserUi.KinectSensorChooser = this.sensorChooser;
            //this.sensorChooser.KinectChanged += this.SensorChooserOnKinectChanged;
            //this.sensorChooser.Start();
            viewModel = new TransformViewModel(view1);
            viewModel.FileOpen();
            this.DataContext = viewModel;
            //((TransformViewModel)this.DataContext).FileOpen();
        }

        private void CallFriend()
        {
            this.sensor2 = this.sensor = KinectSensor.KinectSensors[1];
            this.EnableSensor2(sensor2);
            this.sensor2.Start();
        }
        /// <summary>
        /// Format we will use for the depth stream
        /// </summary>
        private const DepthImageFormat DepthFormat = DepthImageFormat.Resolution320x240Fps30;

        /// <summary>
        /// Format we will use for the color stream
        /// </summary>
        private const ColorImageFormat ColorFormat = ColorImageFormat.RgbResolution640x480Fps30;

        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private WriteableBitmap foregroundBitmap;

        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private WriteableBitmap foregroundBitmap2;


        /// <summary>
        /// Bitmap that will hold color information
        /// </summary>
        private WriteableBitmap foregroundBitmap3;


        /// <summary>
        /// Active Kinect sensor
        /// </summary>
        private KinectSensorChooser sensorChooser;

        /// <summary>
        /// Our core library which does background 
        /// </summary>
        private BackgroundRemovedColorStream backgroundRemovedColorStream;

        /// <summary>
        /// Our core library which does background 
        /// </summary>
        private BackgroundRemovedColorStream backgroundRemovedColorStream2;

        /// <summary>
        /// Intermediate storage for the skeleton data received from the sensor
        /// </summary>
        private Skeleton[] skeletons;

        /// <summary>
        /// Intermediate storage for the skeleton data received from the sensor
        /// </summary>
        private Skeleton[] skeletons2;

        /// <summary>
        /// the skeleton that is currently tracked by the app
        /// </summary>
        private int currentlyTrackedSkeletonId;

        /// <summary>
        /// Track whether Dispose has been called
        /// </summary>
        private bool disposed;


        private KinectSensor sensor;

        private KinectSensor sensor2;
        private TransformViewModel viewModel;


        void   MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Space)
            {
                ////viewModel.ChangeShirt();
                //this.sensor.AllFramesReady -= SensorAllFramesReady;
                //this.sensor.Stop();
                ////this.skeletons = new Skeleton[this.sensor.SkeletonStream.FrameSkeletonArrayLength];              
                //viewModel = new TransformViewModel(view1, 0);
                //viewModel.FileOpen();
                //this.DataContext = viewModel;
                //((TransformViewModel)this.DataContext).FileOpen();
                //this.sensor.AllFramesReady += this.SensorAllFramesReady;
                //this.sensor.Start();

                viewModel.ChangeShirt();
                SmsText.Text = String.Format("SMS 384220{0} to 206-745-481", viewModel.currentindex);
            }
            else if (e.Key == System.Windows.Input.Key.B)
            {

                if (this.SpaceNeedle.Visibility == System.Windows.Visibility.Hidden)
                {
                    this.MaskedColor3.Visibility = System.Windows.Visibility.Hidden;
                    this.SpaceNeedle.Visibility = System.Windows.Visibility.Visible;
                    this.Macklemore.Visibility = System.Windows.Visibility.Hidden;
                }
                else if (this.Macklemore.Visibility == System.Windows.Visibility.Hidden)
                {
                    this.SpaceNeedle.Visibility = System.Windows.Visibility.Hidden;
                    this.Macklemore.Visibility = System.Windows.Visibility.Visible;
                }
            }
            else if (e.Key == System.Windows.Input.Key.F)
            {

                CallFriend();
            }
            else if (e.Key == System.Windows.Input.Key.U)
            {
                try
                {

                    this.sensor2.AllFramesReady -= this.SensorAllFramesReady2;
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            else if (e.Key == System.Windows.Input.Key.I)
            {
                try
                {
                    this.sensor2.AllFramesReady += this.SensorAllFramesReady2;
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
        }


        ~ModelTransform()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Dispose the allocated frame buffers and reconstruction.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            // This object will be cleaned up by the Dispose method.
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Frees all memory associated with the FusionImageFrame.
        /// </summary>
        /// <param name="disposing">Whether the function was called from Dispose.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (null != this.backgroundRemovedColorStream)
                {
                    this.backgroundRemovedColorStream.Dispose();
                    this.backgroundRemovedColorStream = null;
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Execute shutdown tasks
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.sensorChooser.Stop();
            this.sensorChooser = null;
        }

        bool nobackground = true;
        private byte[] colorPixelData;
        /// <summary>
        /// Event handler for Kinect sensor's DepthFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorAllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            // in the middle of shutting down, or lingering events from previous sensor, do nothing here.
            //if (null == this.sensorChooser || null == this.sensorChooser.Kinect || this.sensorChooser.Kinect != sender)
            //{
            //    return;
            //}
            if (nobackground)
            {

                using (var colorFrame = e.OpenColorImageFrame())
                {
                    if (null != colorFrame)
                    {
                        this.colorPixelData = new byte[colorFrame.PixelDataLength];

                        colorFrame.CopyPixelDataTo(this.colorPixelData);

                        if (foregroundBitmap3 == null)
                        {
                            this.foregroundBitmap3 = new WriteableBitmap(colorFrame.Width, colorFrame.Height, 96, 96, PixelFormats.Bgr32, null);
                            this.MaskedColor3.Source = this.foregroundBitmap3;
                        }
                        this.foregroundBitmap3.WritePixels(new Int32Rect(0, 0, colorFrame.Width, colorFrame.Height), this.colorPixelData, colorFrame.Width * 4, 0);
                    }
                }

            }

            try
            {
                using (var depthFrame = e.OpenDepthImageFrame())
                {
                    if (null != depthFrame)
                    {

                        this.backgroundRemovedColorStream.ProcessDepth(depthFrame.GetRawPixelData(), depthFrame.Timestamp);
                    }
                }

                using (var colorFrame = e.OpenColorImageFrame())
                {
                    if (null != colorFrame)
                    {
                        this.backgroundRemovedColorStream.ProcessColor(colorFrame.GetRawPixelData(), colorFrame.Timestamp);
                    }
                }

                using (var skeletonFrame = e.OpenSkeletonFrame())
                {
                    if (null != skeletonFrame)
                    {
                        skeletonFrame.CopySkeletonDataTo(this.skeletons);
                        this.backgroundRemovedColorStream.ProcessSkeleton(this.skeletons, skeletonFrame.Timestamp);
                    }
                }

                this.ChooseSkeleton();
            }
            catch (InvalidOperationException)
            {
                // Ignore the exception. 
            }
        }

        /// <summary>
        /// Event handler for Kinect sensor's DepthFrameReady event
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void SensorAllFramesReady2(object sender, AllFramesReadyEventArgs e)
        {
            // in the middle of shutting down, or lingering events from previous sensor, do nothing here.
            //if (null == this.sensorChooser || null == this.sensorChooser.Kinect || this.sensorChooser.Kinect != sender)
            //{
            //    return;
            //}

            try
            {
                using (var depthFrame = e.OpenDepthImageFrame())
                {
                    if (null != depthFrame)
                    {
                        this.backgroundRemovedColorStream2.ProcessDepth(depthFrame.GetRawPixelData(), depthFrame.Timestamp);
                    }
                }

                using (var colorFrame = e.OpenColorImageFrame())
                {
                    if (null != colorFrame)
                    {
                        this.backgroundRemovedColorStream2.ProcessColor(colorFrame.GetRawPixelData(), colorFrame.Timestamp);
                    }
                }

                using (var skeletonFrame = e.OpenSkeletonFrame())
                {
                    if (null != skeletonFrame)
                    {
                        skeletonFrame.CopySkeletonDataTo(this.skeletons2);
                        this.backgroundRemovedColorStream2.ProcessSkeleton(this.skeletons2, skeletonFrame.Timestamp);
                    }
                }

                this.ChooseSkeleton2();
            }
            catch (InvalidOperationException)
            {
                // Ignore the exception. 
            }
        }

        /// <summary>
        /// Handle the background removed color frame ready event. The frame obtained from the background removed
        /// color stream is in RGBA format.
        /// </summary>
        /// <param name="sender">object that sends the event</param>
        /// <param name="e">argument of the event</param>
        private void BackgroundRemovedFrameReadyHandler(object sender, BackgroundRemovedColorFrameReadyEventArgs e)
        {
            using (var backgroundRemovedFrame = e.OpenBackgroundRemovedColorFrame())
            {
                if (backgroundRemovedFrame != null)
                {
                    if (null == this.foregroundBitmap || this.foregroundBitmap.PixelWidth != backgroundRemovedFrame.Width
                        || this.foregroundBitmap.PixelHeight != backgroundRemovedFrame.Height)
                    {
                        this.foregroundBitmap = new WriteableBitmap(backgroundRemovedFrame.Width, backgroundRemovedFrame.Height, 96.0, 96.0, PixelFormats.Bgra32, null);

                        // Set the image we display to point to the bitmap where we'll put the image data
                        this.MaskedColor.Source = this.foregroundBitmap;
                    }

                    // Write the pixel data into our bitmap
                    this.foregroundBitmap.WritePixels(
                        new Int32Rect(0, 0, this.foregroundBitmap.PixelWidth, this.foregroundBitmap.PixelHeight),
                        backgroundRemovedFrame.GetRawPixelData(),
                        this.foregroundBitmap.PixelWidth * sizeof(int),
                        0);
                }
            }

        }


        /// <summary>
        /// Handle the background removed color frame ready event. The frame obtained from the background removed
        /// color stream is in RGBA format.
        /// </summary>
        /// <param name="sender">object that sends the event</param>
        /// <param name="e">argument of the event</param>
        private void BackgroundRemovedFrameReadyHandler2(object sender, BackgroundRemovedColorFrameReadyEventArgs e)
        {
            using (var backgroundRemovedFrame = e.OpenBackgroundRemovedColorFrame())
            {
                if (backgroundRemovedFrame != null)
                {
                    if (null == this.foregroundBitmap2 || this.foregroundBitmap2.PixelWidth != backgroundRemovedFrame.Width
                        || this.foregroundBitmap2.PixelHeight != backgroundRemovedFrame.Height)
                    {
                        this.foregroundBitmap2 = new WriteableBitmap(backgroundRemovedFrame.Width, backgroundRemovedFrame.Height, 96.0, 96.0, PixelFormats.Bgra32, null);

                        // Set the image we display to point to the bitmap where we'll put the image data
                        this.MaskedColor2.Source = this.foregroundBitmap2;
                    }

                    // Write the pixel data into our bitmap
                    this.foregroundBitmap2.WritePixels(
                        new Int32Rect(0, 0, this.foregroundBitmap2.PixelWidth, this.foregroundBitmap2.PixelHeight),
                        backgroundRemovedFrame.GetRawPixelData(),
                        this.foregroundBitmap2.PixelWidth * sizeof(int),
                        0);
                }
            }
        }
        /// <summary>
        /// Use the sticky skeleton logic to choose a player that we want to set as foreground. This means if the app
        /// is tracking a player already, we keep tracking the player until it leaves the sight of the camera, 
        /// and then pick the closest player to be tracked as foreground.
        /// </summary>
        private void ChooseSkeleton()
        {
            var isTrackedSkeltonVisible = false;
            var nearestDistance = float.MaxValue;
            var nearestSkeleton = 0;

            foreach (var skel in this.skeletons)
            {
                if (null == skel)
                {
                    continue;
                }

                if (skel.TrackingState != SkeletonTrackingState.Tracked)
                {
                    continue;
                }

                if (skel.TrackingId == this.currentlyTrackedSkeletonId)
                {
                    isTrackedSkeltonVisible = true;
                    this.SkelTo3Dimage(skel);
                    break;
                }

                if (skel.Position.Z < nearestDistance)
                {
                    nearestDistance = skel.Position.Z;
                    nearestSkeleton = skel.TrackingId;
                }
            }

            if (!isTrackedSkeltonVisible && nearestSkeleton != 0)
            {
                this.backgroundRemovedColorStream.SetTrackedPlayer(nearestSkeleton);
                this.currentlyTrackedSkeletonId = nearestSkeleton;
            }
        }

        /// <summary>
        /// Use the sticky skeleton logic to choose a player that we want to set as foreground. This means if the app
        /// is tracking a player already, we keep tracking the player until it leaves the sight of the camera, 
        /// and then pick the closest player to be tracked as foreground.
        /// </summary>
        private void ChooseSkeleton2()
        {
            var isTrackedSkeltonVisible = false;
            var nearestDistance = float.MaxValue;
            var nearestSkeleton = 0;

            foreach (var skel in this.skeletons2)
            {
                if (null == skel)
                {
                    continue;
                }

                if (skel.TrackingState != SkeletonTrackingState.Tracked)
                {
                    continue;
                }

                if (skel.TrackingId == this.currentlyTrackedSkeletonId)
                {
                    isTrackedSkeltonVisible = true;
                    // this.SkelTo3Dimage(skel);
                    break;
                }

                if (skel.Position.Z < nearestDistance)
                {
                    nearestDistance = skel.Position.Z;
                    nearestSkeleton = skel.TrackingId;
                }
            }

            if (!isTrackedSkeltonVisible && nearestSkeleton != 0)
            {
                this.backgroundRemovedColorStream2.SetTrackedPlayer(nearestSkeleton);
                this.currentlyTrackedSkeletonId = nearestSkeleton;
            }
        }

        private void SkelTo3Dimage(Skeleton skel)
        {
            float X = skel.BoneOrientations[JointType.Spine].AbsoluteRotation.Quaternion.X;
            float Y = skel.BoneOrientations[JointType.Spine].AbsoluteRotation.Quaternion.Y;
            float Z = skel.BoneOrientations[JointType.Spine].AbsoluteRotation.Quaternion.Z;
            float W = skel.BoneOrientations[JointType.Spine].AbsoluteRotation.Quaternion.W;
            //Console.WriteLine(X.ToString() + Y.ToString() + Z.ToString() + W);

            TranslateTransform3D translateTransform = new TranslateTransform3D();
            var leftHand = skel.Joints[JointType.ShoulderLeft];
            var rightHand = skel.Joints[JointType.ShoulderRight];
            var distance = 2 * Math.Sqrt(Math.Pow(leftHand.Position.X - rightHand.Position.X, 2) + Math.Pow(leftHand.Position.Y - rightHand.Position.Y, 2) + Math.Pow(leftHand.Position.Z - rightHand.Position.Z, 2));

            translateTransform.OffsetX = skel.Joints[JointType.ShoulderCenter].Position.X * 3;
            translateTransform.OffsetY = skel.Joints[JointType.HipCenter].Position.Y * 2;
            translateTransform.OffsetZ = -distance;

            ////ScaleTransform3D scaleTransform = new ScaleTransform3D();
            ////scaleTransform.ScaleX = distance;
            ////scaleTransform.ScaleY = distance;
            ////scaleTransform.ScaleZ = distance * 1.5;

            Quaternion quaternion = new Quaternion(X, Y, Z, W);
            QuaternionRotation3D myQuaternionRotation3D = new QuaternionRotation3D(quaternion);
            RotateTransform3D myRotateTransform3D = new RotateTransform3D();
            myRotateTransform3D.Rotation = myQuaternionRotation3D;

            AxisAngleRotation3D myAxisAngleRotation3D = new AxisAngleRotation3D();
            double axisX = quaternion.Axis.X;
            double axisY = quaternion.Axis.Y;
            double axisZ = -quaternion.Axis.Z;

            myAxisAngleRotation3D.Axis = new Vector3D(axisX, axisY, axisZ);
            myAxisAngleRotation3D.Angle = quaternion.Angle;
            myRotateTransform3D.Rotation = myAxisAngleRotation3D;

            //Console.WriteLine("Axis {0}, Angle {1}", quaternion.Axis.ToString(), quaternion.Angle.ToString());
            //Console.WriteLine("{0}, {1}, {2}", quaternion.Axis.X.ToString("F"), quaternion.Axis.Y.ToString("F"), quaternion.Axis.Z.ToString("F"));
            //Console.WriteLine("{0}, {1}", translateTransform.OffsetX, translateTransform.OffsetY);

            Transform3DGroup myTransform3DGroup = new Transform3DGroup();
            myTransform3DGroup.Children.Add(myRotateTransform3D.Inverse as Transform3D);
            myTransform3DGroup.Children.Add(translateTransform);
            //myTransform3DGroup.Children.Add(scaleTransform);

            this.Dress.Transform = (Transform3D)myTransform3DGroup;
        }

        /// <summary>
        /// Enables the chosen sensor
        /// </summary>

        private void EnableSensor(KinectSensor NewSensor)
        {
            if (NewSensor != null)
            {
                try
                {
                    NewSensor.DepthStream.Enable(DepthFormat);
                    NewSensor.ColorStream.Enable(ColorFormat);
                    NewSensor.SkeletonStream.Enable();

                    this.backgroundRemovedColorStream = new BackgroundRemovedColorStream(NewSensor);
                    this.backgroundRemovedColorStream.Enable(ColorFormat, DepthFormat);

                    // Allocate space to put the depth, color, and skeleton data we'll receive
                    if (null == this.skeletons)
                    {
                        this.skeletons = new Skeleton[NewSensor.SkeletonStream.FrameSkeletonArrayLength];
                    }

                    // Add an event handler to be called when the background removed color frame is ready, so that we can
                    // composite the image and output to the app
                    this.backgroundRemovedColorStream.BackgroundRemovedFrameReady += this.BackgroundRemovedFrameReadyHandler;

                    // Add an event handler to be called whenever there is new depth frame data
                    NewSensor.AllFramesReady += this.SensorAllFramesReady;

                }
                catch (Exception e)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }
        }

        /// <summary>
        /// Enables the chosen sensor
        /// </summary>

        private void EnableSensor2(KinectSensor NewSensor)
        {
            if (NewSensor != null)
            {
                try
                {
                    NewSensor.DepthStream.Enable(DepthFormat);
                    NewSensor.ColorStream.Enable(ColorFormat);
                    NewSensor.SkeletonStream.Enable();

                    this.backgroundRemovedColorStream2 = new BackgroundRemovedColorStream(NewSensor);
                    this.backgroundRemovedColorStream2.Enable(ColorFormat, DepthFormat);

                    // Allocate space to put the depth, color, and skeleton data we'll receive
                    if (null == this.skeletons2)
                    {
                        this.skeletons2 = new Skeleton[NewSensor.SkeletonStream.FrameSkeletonArrayLength];
                    }

                    // Add an event handler to be called when the background removed color frame is ready, so that we can
                    // composite the image and output to the app
                    this.backgroundRemovedColorStream2.BackgroundRemovedFrameReady += this.BackgroundRemovedFrameReadyHandler2;

                    // Add an event handler to be called whenever there is new depth frame data
                    NewSensor.AllFramesReady += this.SensorAllFramesReady2;


                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }
        }


        /// <summary>
        /// Handles the user clicking on the screenshot button
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void ButtonScreenshotClick(object sender, RoutedEventArgs e)
        {
            TakeScreenShot();
        }
        private void TakeScreenShot()
        {
            try
            {
                var time = DateTime.Now.ToString("yyyy-MM-dd-hh'-'mm'-'ss", CultureInfo.CurrentUICulture.DateTimeFormat);
                RenderTargetBitmap targetBitmap =
                    new RenderTargetBitmap((int)EntireGrid.ActualWidth,
                                           (int)EntireGrid.ActualHeight,
                                           96d, 96d,
                                           PixelFormats.Default);
                targetBitmap.Render(EntireGrid);

                CroppedBitmap crop = new CroppedBitmap(targetBitmap, new Int32Rect((int)(270), (int)(200), (int)(1000), (int)(600)));

                BitmapEncoder pngEncoder = new PngBitmapEncoder();
                pngEncoder.Frames.Add(BitmapFrame.Create(crop));

                var myPhotos = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
                var path = System.IO.Path.Combine(myPhotos, "KinectSnapshot-" + time + ".png");

                // save file to disk
                // write the new file to disk

                using (var fs = new FileStream(path, FileMode.Create))
                {
                    pngEncoder.Save(fs);
                }

                // write the file to azure

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve reference to a previously created container.
                CloudBlobContainer container = blobClient.GetContainerReference("images");

                // Retrieve reference to a blob named "myblob".
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(string.Format("zscreenshots_{0}.png", time));

                // Create or overwrite the "myblob" blob with contents from a local file.
                using (var fileStream = System.IO.File.OpenRead(path))
                {
                    blockBlob.UploadFromStream(fileStream);
                }

            }
            catch (IOException)
            {

            }
        }

        /// <summary>
        /// Handles the checking or unchecking of the near mode combo box
        /// </summary>
        /// <param name="sender">object sending the event</param>
        /// <param name="e">event arguments</param>
        private void CheckBoxNearModeChanged(object sender, RoutedEventArgs e)
        {
            if (null == this.sensorChooser || null == this.sensorChooser.Kinect)
            {
                return;
            }
            this.nobackground = !this.nobackground;
            // will not function on non-Kinect for Windows devices

        }

        bool x = true;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (x)
            {
                System.Windows.Controls.Canvas.SetZIndex(this.MaskedColor3, -1);
                x = !x;
            }
            else
            {
                System.Windows.Controls.Canvas.SetZIndex(this.MaskedColor3, 1);
                x = !x;
            }


        }
    }
}
