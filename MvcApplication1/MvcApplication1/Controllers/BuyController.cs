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
                    case "tshirt100":
                        viewModel.Item.name = "T-Shirt Blue";
                        viewModel.Item.price = "20.0";
                        viewModel.Item.quantity = "1";
                        viewModel.Item.currency = "USD";
                        break;
                    case "tshirt101":
                        viewModel.Item.name = "T-Shirt Red";
                        viewModel.Item.price = "20.0";
                        viewModel.Item.quantity = "1";
                        viewModel.Item.currency = "USD";
                        break;
                    default:
                        viewModel.Item = null;
                        break;
                }
            }

            return View(viewModel);
        }

    }
}
