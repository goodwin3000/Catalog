using Catalog.Models;
using CatalogLib.Abstraction;
using CatalogLib.Concrete;
using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Catalog.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        
        IRepository<Product, int> _productRepository;
        IRepository<Category, string> _categoryRepository;
        public AdminController(IRepository<Product,int> productRepo, IRepository<Category, string> categoryRepo)
        {
           
         
            _productRepository = productRepo;
            _categoryRepository = categoryRepo;
        }
        // GET: Admin
        public ActionResult Index(string cat)
        {

            AdminDashBoardModel adbm = new AdminDashBoardModel();
            adbm.Category = _categoryRepository.GetAll().ToList();     
            if (String.IsNullOrEmpty(cat))
            {
                cat = adbm.Category[0].CategoryLinkName;
            }
         
            adbm.Products = _productRepository.GetAll().Where(p => p.CategoryLinkName==cat).ToList();
            ViewBag.cat = cat;
            ViewBag.AdminHeader = "Управление каталогом";        
            ViewBag.Title = ViewBag.AdminHeader;
            return View(adbm);
        }
        #region Catalog function
        [HttpPost]
        public ActionResult CreateCategory(Category cat, HttpPostedFileBase file)
        {
            string imgPath = "/Content/images/category";
            string path = "";
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    path = System.IO.Path.Combine(
                                           Server.MapPath("~" + imgPath), pic);
                    // file is uploaded
                    file.SaveAs(path);
                    cat.CategoryPictureUrl = imgPath + "/" + file.FileName;
                }
                _categoryRepository.Add(cat);
                _categoryRepository.Save();
                // int i = _categoryRepository.FindBy(c => c.CategoryLinkName == cat.CategoryLinkName).SingleOrDefault().no;
                int i = _categoryRepository.FindById(cat.CategoryLinkName).no;
                return Json(i);
            }
      
            return View();
        }
        public PartialViewResult GetCategory()
        {

         
            return PartialView("_GetCategory", _categoryRepository.GetAll().ToList());
        }
        public PartialViewResult CategoryDropDownList(string todo, string selectedValue)
        {
            AdminDropDownCategoryListModel m = new AdminDropDownCategoryListModel();
         
            m.Category = _categoryRepository.GetAll().ToList(); 
            m.SelectedValue = selectedValue;
        
            ViewBag.SelectedValue = todo;
            return PartialView("_CategoryDropDownList", m);
        }

        [HttpPost]
        public ActionResult CreateProduct(Product p, HttpPostedFileBase file)
        {

            string imgPath = "/Content/images/product";
            string path = "";
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    path = System.IO.Path.Combine(
                                           Server.MapPath("~" + imgPath), pic);
                    // file is uploaded
                    file.SaveAs(path);
                    p.ProductPictureUrl = imgPath + "/" + file.FileName;
                }

            
                p.CreatedDate = DateTime.Now;
                _productRepository.Add(p);
                _productRepository.Save();
                int i = p.ProductId;
                return Json(i);
            }
            else
            {
                int wrongData = -2;
                return Json(wrongData);
            }
           
        }
      
        public PartialViewResult GetProductsByCatName(string id)
        {

            ViewBag.cat = id;
            Category cat = _categoryRepository.FindById(id);
            // return PartialView("_GetProducts", _categoryRepository.GetAll().Where(c=>c.CategoryLinkName==id).SingleOrDefault().Products);
            return PartialView("_GetProducts", cat.Products);
        }
        public ActionResult EditCategory(Category cat)
        {
            if (ModelState.IsValid)
            {                
                _categoryRepository.Update(cat);
                _categoryRepository.Save();
                return Json(cat);
            }
            return View();
        }
        public ActionResult DeleteCategory(string catName)
        {
            if (String.IsNullOrEmpty(catName))
            {
                return Json(catName);
            }


            // Category cat = _categoryRepository.FindBy(c => c.CategoryLinkName == catName).SingleOrDefault();
            Category cat = _categoryRepository.FindById(catName);
            _categoryRepository.Delete(cat);
            _categoryRepository.Save();
            return Json(catName);
        }
        public ActionResult EditProduct(Product p)
        {
            if (ModelState.IsValid)
            {
             
                _productRepository.Update(p);
                _productRepository.Save();
                return Json("");
            }
            return View();
        }
        public ActionResult DeleteProduct(int id)
        {
            if (ModelState.IsValid)
            {

                //  Product p= _productRepository.FindBy(pr => pr.ProductId == id).SingleOrDefault();
                Product p = _productRepository.FindById(id);
                _productRepository.Delete(p);
                _productRepository.Save();
                return Json("");
            }
            return View();
        }
        #endregion
       
    }
}