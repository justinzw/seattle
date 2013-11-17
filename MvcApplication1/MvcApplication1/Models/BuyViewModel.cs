using Microsoft.WindowsAzure.Storage.Blob;
using PayPal.Api.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class BuyViewModel
    {
        public Item Item;
        public string MerchantId;

        public IEnumerable<ICloudBlob> Photos;
    }
}