using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices;
using System.Windows.Threading;
using ZXing;
using System.Windows.Media.Imaging;
using PayPal.Checkout;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Windows.Media;

namespace InStorePurchaseApp
{
    public class ShoppingItem
    {
        public string Id { get; set; }
        public uint Quantity { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
    }
    public partial class QRScanner : PhoneApplicationPage
    {
        private PhotoCamera phoneCamera;
        private DispatcherTimer scanTimer;
        private IBarcodeReader barCodeReader;
        private WriteableBitmap previewBuffer;
        private ShoppingItem item;

        private BuyNow buyNow;

        public QRScanner()
        {
            InitializeComponent();
        }


        protected override void OnNavigatingFrom(System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            //we're navigating away from this page, we won't be scanning any barcodes
            scanTimer.Stop();

            if (phoneCamera != null)
            {
                // Cleanup
                phoneCamera.Dispose();
                phoneCamera.Initialized -= cameraInitialized;
                CameraButtons.ShutterKeyHalfPressed -= cameraShutterKeyHalfPressed;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Initialize the camera object
            phoneCamera = new PhotoCamera();
            phoneCamera.Initialized += cameraInitialized;

            CameraButtons.ShutterKeyHalfPressed += cameraShutterKeyHalfPressed;

            //Display the camera feed in the UI
            ViewFinder.SetSource(phoneCamera);

            scanTimer = new DispatcherTimer();
            scanTimer.Interval = TimeSpan.FromMilliseconds(250);
            scanTimer.Tick += (o, arg) => scanForBarcode();

            base.OnNavigatedTo(e);
        }

        private void scanForBarcode()
        {
            //grab a camera snapshot
            phoneCamera.GetPreviewBufferArgb32(previewBuffer.Pixels);
            previewBuffer.Invalidate();

            //scan the captured snapshot for barcodes
            //if a barcode is found, the ResultFound event will fire
            barCodeReader.Decode(previewBuffer);
        }

        private void cameraInitialized(object sender, CameraOperationCompletedEventArgs e)
        {
            if (e.Succeeded)
            {
                this.Dispatcher.BeginInvoke(delegate()
                {
                    phoneCamera.FlashMode = FlashMode.Off;
                    previewBuffer = new WriteableBitmap((int)phoneCamera.PreviewResolution.Width, (int)phoneCamera.PreviewResolution.Height);

                    barCodeReader = new BarcodeReader();

                    var supportedBarcodeFormats = new List<BarcodeFormat>();
                    supportedBarcodeFormats.Add(BarcodeFormat.QR_CODE);
                    barCodeReader.Options.PossibleFormats = supportedBarcodeFormats;

                    barCodeReader.Options.TryHarder = true;

                    barCodeReader.ResultFound += barCodeReaderResultFound;
                    scanTimer.Start();
                });
            }
            else
            {
                Dispatcher.BeginInvoke(() =>
                {
                    MessageBox.Show("Unable to initialize the camera");
                });
            }
        }

        private void barCodeReaderResultFound(Result obj)
        {
            tbBarcodeType.Text = obj.BarcodeFormat.ToString();
            tbBarcodeData.Text = obj.Text;

            // Example: { "Id": "1", "Price":"10.00", "Description":"Test description", "Quantity": 2}
            // http://www.qrstuff.com/
            item = JsonConvert.DeserializeObject<ShoppingItem>(obj.Text);

            prePurchaseButton.Visibility = System.Windows.Visibility.Collapsed;
            purchaseButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void cameraShutterKeyHalfPressed(object sender, EventArgs e)
        {
            phoneCamera.Focus();
        }

        private async void puchaseButtonClick(object sender, RoutedEventArgs e)
        {
            buyNow = new BuyNow("enduser_biz@gmail.com");
            buyNow.UseSandbox = true;
            // Set the currency to use US Dollars
            buyNow.Currency = "USD";

            // Use the ItemBuilder to create a new example item
            PayPal.Checkout.ItemBuilder itemBuilder = new PayPal.Checkout.ItemBuilder("Example Item")
                .ID(item.Id)
                .Price(item.Price)
                .Description(item.Description)
                .Quantity(item.Quantity);

            // Add the item to the purchase,
            buyNow.AddItem(itemBuilder.Build());

            // Attach event handlers so you will be notified of important events
            // The BuyNow interface provides 5 events - Start, Auth, Cancel, Complete and Error
            // See http://paypal.github.io/Windows8SDK/csharp.html#Events for more
            buyNow.Error += new EventHandler<PayPal.Checkout.Event.ErrorEventArgs>((source, eventArg) =>
            {
                this.tbBarcodeType.Text = "There was an error processing your payment: " + eventArg.Message;
            });
            buyNow.Complete += new EventHandler<PayPal.Checkout.Event.CompleteEventArgs>((source, eventArg) =>
            {
                this.tbBarcodeType.Text = "Payment is complete. Transaction id: " + eventArg.TransactionID;
            });
            buyNow.Cancel += new EventHandler<PayPal.Checkout.Event.CancelEventArgs>((source, eventArg) =>
            {
                this.tbBarcodeType.Text = "Payment was canceled by the user.";
            });

            // Launch the secure PayPal interface. This is an asynchronous method
            await buyNow.Execute();

        }
    }
}