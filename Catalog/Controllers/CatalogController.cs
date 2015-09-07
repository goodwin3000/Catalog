
using Catalog.Models;
using CatalogLib.Abstraction;
using CatalogLib.Concrete;
using CatalogLib.DataBase;
using CatalogLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace Catalog.Controllers
{
   
    public class CatalogController : Controller
    {
    
        IRepository<Category, string> _repository;
        private int _pageSize = 3;

        public CatalogController(IRepository<Category,string> repo)
        {
            _repository = repo;
            _pageSize= int.Parse(ConfigurationManager.AppSettings["pageSize"]);
        
        }
        // GET: Catalog
        public ActionResult Index(string category, int? id,int page=1)
        {
            CatalogCategoryModel m = null;

            if (String.IsNullOrEmpty(category))
            {
                category = _repository.GetAll().FirstOrDefault().CategoryLinkName;
            }  
                Category cat = _repository.FindBy(c => c.CategoryLinkName == category).FirstOrDefault();
                if ((cat == null) || (cat.Published = false))
                {
                    return HttpNotFound();
                }
                if (id.HasValue)
                {                
                    var p = cat.Products.Where(pd => pd.ProductId == id).SingleOrDefault();  
                    if(p==null)
                        return HttpNotFound();
                    return View("ProductDetails", p);
                }
                    m = new CatalogCategoryModel();
                    m.Products = cat.Products.OrderBy(p=>p.ProductName).Skip((page - 1) * _pageSize).Take(_pageSize).ToList();
                    m.Category = cat;
            PaginationModel pm = new PaginationModel()
            {
                CurrentPage = page,
                ItemsPerPage = _pageSize,
                TotalItems = cat.Products.Count(p=>p.Published)
            };
            m.Pagination = pm;
                return View(m);
      
        }

     
    }
}