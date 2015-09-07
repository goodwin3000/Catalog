using Catalog.Models;
using CatalogLib.Abstraction;
using CatalogLib.DataBase;
using CatalogLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalog.Controllers
{

    public class ShopController : Controller
    {
        IRepository<Product, int> _productRepository;
        IRepository<PayOutOption, int> _payRepository;
        IRepository<OrderProduct, int> _orderProuctRepository;
        IRepository<Order, int> _orderRepository;
        private IRepository<OrderStatus, int> _orderStatusRepository;

        public ShopController(IRepository<Product, int> repo, IRepository<PayOutOption, int> payRepo, IRepository<OrderProduct, int> orderProuctRepo, IRepository<Order, int> orderRepo, IRepository<OrderStatus, int> orderStatusRepo)
        {
            _productRepository = repo;
            _payRepository = payRepo;
            _orderProuctRepository = orderProuctRepo;
            _orderRepository = orderRepo;
            _orderStatusRepository = orderStatusRepo;
        }
        //[HttpGet]
        //public ActionResult Buy(int? productId)
        //{
        //    if (productId.HasValue)
        //    {
        //        Product p = _productRepository.FindBy(pp => pp.ProductId == productId).SingleOrDefault();
        //        if (p != null)
        //        {
        //            BuyViewModel model = new BuyViewModel();
        //            model.Price = p.ProductPrice;
        //            model.ProductName = p.ProductName;
        //            model.PayoutOptions = _payRepository.GetAll().ToList();
        //            return View(model);
        //        }
        //        else
        //        {
        //            return HttpNotFound();
        //        }
        //    }
        //    return HttpNotFound();
        //}
        //[HttpPost]
        //public ActionResult Buy(BuyViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        model.PayoutOptions = _payRepository.GetAll().ToList();
        //        return View(model);
        //    }

        //    Order o = new Order();
        //    o.CustomerFullName = model.FullName;
        //    o.Email = model.Email;
        //    o.Phone = model.Phone;
        //    o.PostAddress = model.PostAddress;
        //    o.OrerDate = DateTime.Now;
        //    o.PayOutId = _payRepository.FindBy(po => po.id == model.SelectedPayOutId).SingleOrDefault().id;
        //    //  o.OrderStatus = _orderStatusRepository.GetAll().OrderBy(s => s.id).FirstOrDefault();
        //    o.OrderStatusId = _orderStatusRepository.GetAll().Min(x => x.id);
        //    Product p = _productRepository.FindBy(pp => pp.ProductId == model.ProductId).SingleOrDefault();

        //    OrderProduct op = new OrderProduct();
        //    op.Order = o;
        //    op.ProductId = p.ProductId;
        //    op.Quantity = model.Quantity;

        //    _orderRepository.Add(o);
        //    _orderProuctRepository.Add(op);
        //    _orderProuctRepository.Save();

        //    int id = op.Id;
        //    return RedirectToAction("OrderSummary", new { id = id });
        //}
        public ActionResult Order(Cart cart)
        {
            if (cart.CartItems.Count != 0)
            {
                ShopOrderViewModel model = new ShopOrderViewModel();
                model.PayoutOptions = _payRepository.GetAll().ToList();

                return View(model);
            }
            else
            {
                return RedirectToAction("index", "Catalog");
            }
        }
        [HttpPost]
        public ActionResult Order(Cart cart, ShopOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.PayoutOptions = _payRepository.GetAll().ToList();
                return View(model);
            }
            Order o = new Order();
            o.CustomerFullName = model.FullName;
            o.Email = model.Email;
            o.Phone = model.Phone;
            o.PostAddress = model.PostAddress;
            o.OrerDate = DateTime.Now;
            o.PayOutId = _payRepository.FindBy(po => po.id == model.SelectedPayOutId).SingleOrDefault().id;
            //  o.OrderStatus = _orderStatusRepository.GetAll().OrderBy(s => s.id).FirstOrDefault();
            o.OrderStatusId = _orderStatusRepository.GetAll().Min(x => x.id);
            _orderRepository.Add(o);
            if (cart.CartItems.Count > 0)
            {
                foreach (CartItem item in cart.CartItems)
                {
                    OrderProduct op = new OrderProduct();
                    op.Order = o;
                    op.ProductId = item.Product.ProductId;
                    op.Quantity = item.Quantity;
                    _orderProuctRepository.Add(op);
                }

            } 
            _orderProuctRepository.Save();

            int id = o.OrderId;
            return RedirectToAction("OrderSummary", new { id = id });
        }
        public JsonResult AddToCart(Cart cart, int productId, int quantity)
        {
            Product p = _productRepository.FindById(productId);
            if (p != null)
            {
                cart.Add(p, quantity);
                return Json("0");

            }
            return Json("-1");
        }
        public JsonResult RemoveFromCart(Cart cart, int productId)
        {
            Product p = _productRepository.FindById(productId);
            if (p != null)
            {
                cart.Remove(p);
                return Json("0");
            }
            else
            {
                return Json("-1");
            }
               
        }
        public ActionResult Cart(Cart cart)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView("_cart", cart);
            }
            return View(cart);
        }
        public ActionResult OrderSummary(Cart cart ,int id)
        {

           
            Order o = _orderRepository.FindById(id);
            if (o != null)
            {
                cart.Clear();
                return View(o);
            }
            return HttpNotFound();
        }
    }
   
    
}