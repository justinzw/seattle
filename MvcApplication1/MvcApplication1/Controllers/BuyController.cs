using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BuyController : Controller
    {
        //
        // GET: /Buy/

        public ActionResult Index(string id)
        {
            var viewModel = new BuyViewModel();

            if (!string.IsNullOrWhiteSpace(id))
            {
                viewModel.Item = new PayPal.Api.Payments.Item();
                switch (id.ToLowerInvariant())
                {
                    case "3848203":
                        viewModel.Item.name = "Paypal Tshirt";
                        viewModel.Item.price = "20.0";
                        viewModel.Item.quantity = "1";
                        viewModel.Item.currency = "USD";
                        break;
                    case "3842203":
                        viewModel.Item.name = "Release the Kraken Tshirt";
                        viewModel.Item.price = "20.0";
                        viewModel.Item.quantity = "1";
                        viewModel.Item.currency = "USD";
                        break;
                    default:
                        viewModel.Item = null;
                        break;
                }
            }
            var storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=battlehackfinal;AccountKey=M/8ixre2UT2TWaa4CmnhknWpEchbpFi6qNQ8bn9LG9OJlWWDzM6xMNZBkNmDtN0M78fNjQ6KW7aksn+oO8yZzw==");
            
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference("images");

            var blobs = container.ListBlobs()
                .OrderByDescending(b => b.Uri).Skip(0).Take(3);
            viewModel.Photos = new string[3];
            for(int i=0; i<3; i++)
            {
                viewModel.Photos[i] = blobs.ElementAt(i).Uri.ToString();
            }
            return View(viewModel);
        }

    }
}
