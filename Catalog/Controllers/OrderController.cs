using Catalog.Models;
using CatalogLib.Abstraction;
using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Catalog.Controllers
{
    public class OrderController : Controller
    {
        private IRepository<Order, int> _orderRepository;
        private IRepository<PayOutOption, int> _payOptionsRepository;
        private IRepository<OrderStatus, int> _statusRepository;
        private IRepository<OrderProduct, int> _orderProductRepository;
        private int _pageSize = 3;
        public OrderController(IRepository<Order,int> repo, IRepository<PayOutOption,int> payOptionsRepo,IRepository<OrderStatus,int> statusRepo,IRepository<OrderProduct,int> orderProductRepo)
        {
            _orderRepository = repo;
            _payOptionsRepository = payOptionsRepo;
            _statusRepository = statusRepo;
            _orderProductRepository = orderProductRepo;
            _pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
        }
        // GET: Order
        public ActionResult Index(int page=1)
        {
            if (page < 1)
            {
                page = 1;
            }
          List<Order> orders=  _orderRepository.GetAll().OrderByDescending(o=>o.OrerDate).Skip((page-1)*_pageSize).Take(_pageSize).ToList();
            OrderListViewModel olvm = new OrderListViewModel();
            olvm.Orders = orders;
            olvm.Pagination = new PaginationModel { CurrentPage = page, TotalItems = _orderRepository.GetAll().Count(), ItemsPerPage = _pageSize };
            ViewBag.AdminHeader = "Список заказов";
          
            ViewBag.Title = ViewBag.AdminHeader;
            return View(olvm);
        }
        public ActionResult Details(int id)
        {
           
            Order order = _orderRepository.FindById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.AdminHeader = "Информация о заказе номер: " + order.OrderId;
            ViewBag.Title = ViewBag.AdminHeader;
            return View(order);
        }
        public ActionResult Edit(int id)
        {
            Order order = _orderRepository.FindById(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            EditOrderViewModel model = new EditOrderViewModel();
            model.Order = order;
            model.OrderStatus = _statusRepository.GetAll().ToList();
            model.PayOutOptions = _payOptionsRepository.GetAll().ToList();
            ViewBag.AdminHeader = "Информация о заказе номер: " + order.OrderId;
            ViewBag.Title = ViewBag.AdminHeader;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(EditOrderViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _orderRepository.Update(model.Order);
            _orderRepository.Save();
            model.OrderStatus = _statusRepository.GetAll().ToList();
            model.PayOutOptions = _payOptionsRepository.GetAll().ToList();
            ViewBag.AdminHeader = "Информация о заказе номер: " + model.Order.OrderId;
            ViewBag.Title = ViewBag.AdminHeader;
            return RedirectToAction("Index");
        }
        public ActionResult DeleteProduct(int Id)
        {
            ViewBag.AdminHeader = "Удаление продукта из заказа" ;
            ViewBag.Title = ViewBag.AdminHeader;
            OrderProduct op = _orderProductRepository.FindById(Id);

            return View(op);
        }
        [HttpPost, ActionName("DeleteProduct")]      
        public ActionResult DeleteProductDell(int Id)
        {

            OrderProduct op = _orderProductRepository.FindById(Id);
            _orderProductRepository.Delete(op);
            _orderProductRepository.Save();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteOrder(int Id)
        {
            ViewBag.AdminHeader = "Удаление Заказа";
            ViewBag.Title = ViewBag.AdminHeader;

            Order order = _orderRepository.FindById(Id);

            return View(order);
        }
        [HttpPost, ActionName("DeleteOrder")]
        public ActionResult DeleteOrderDell(int Id)
        {

            Order order = _orderRepository.FindById(Id);
            _orderRepository.Delete(order);
            _orderRepository.Save();
            return RedirectToAction("Index");
        }
    }
}