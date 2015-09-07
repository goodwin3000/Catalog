
using Catalog.Models;
using CatalogLib.Abstraction;
using CatalogLib.Concrete;
using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Catalog.Controllers
{
  public  class HomeController:Controller
    {
       // ICatalogRepository repository;
        IRepository<Category, string> _categoryRepository;
        private IRepository<Widget, int> _widgetRepository;

        public HomeController(IRepository<Category, string> catRepo, IRepository<Widget, int> widgetRepo)
        {
            //  repository = new SqlCatalogRepository(new CatalogLib.DataBase.DataContext());
            _categoryRepository = catRepo;
            _widgetRepository = widgetRepo;
        }
        public ActionResult Index()
        {
            // repository.GetAllProducts(true);
            HomePageModel model = new HomePageModel();
            // model.Widgets = repository.GetWidgets(true);
            model.Widgets = _widgetRepository.GetAll().Where(w=>w.Published==true).ToList();
            return View(model);
        }
        [ChildActionOnly]
        public PartialViewResult GetCategory()
        {

            // return PartialView(repository.GetCategorys(false));
            return PartialView(_categoryRepository.GetAll().ToList());
        }
    }
}
