using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CatalogLib.DataBase;
using CatalogLib.Models;
using CatalogLib.Abstraction;
using Catalog.Models;

namespace Catalog.Controllers
{
    [Authorize(Roles = "Admin")]
    public class WidgetsController : Controller
    {
        IRepository<Widget, int> _widgetRepository;

        public WidgetsController(IRepository<Widget, int> widgetRepository)
        {
            _widgetRepository = widgetRepository;
        }

        // GET: Widgets
        public ActionResult Index()
        {
            MainPageConfigurationModel mpcfg = new MainPageConfigurationModel();
            mpcfg.Widgets = _widgetRepository.GetAll().ToList();
            ViewBag.AdminHeader = "Настойка главной страницы";
          
            ViewBag.Title = ViewBag.AdminHeader;
            return View(mpcfg);
        }
        public ActionResult EditWidget(int? id)
        {
            ViewBag.AdminHeader = "Редактирование Widget";
            ViewBag.Title = ViewBag.AdminHeader;
            if (id.HasValue)
            {
                Widget w = _widgetRepository.FindById(id.Value);

               // Widget w = repository.GetWidgetById(id.Value);
                return View(w);
            }
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditWidget(Widget widget)
        {
            if (ModelState.IsValid)
            {
                //  repository.UpdateWindget(widget);
                _widgetRepository.Update(widget);
                _widgetRepository.Save();

                return RedirectToAction("Index");
            }
            return View(widget);
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string vImagePath = String.Empty;
            string vMessage = String.Empty;
            string vFilePath = String.Empty;
            string vOutput = String.Empty;
            string url = "";
            try
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    string path = "Content/images/widget/" + upload.FileName;
                    url = Request.Url.GetLeftPart(UriPartial.Authority) + "/" + path;
                    string fileName = upload.FileName;
                    vMessage = url;
                    string patha = AppDomain.CurrentDomain.BaseDirectory + "Content/images/widget/";
                    upload.SaveAs(patha + fileName);
                }
            }
            catch
            {
                vMessage = "There was an issue uploading";
            }
            vOutput = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\", \"" + vMessage + "\");</script></body></html>";
            return Content(vOutput);
        }

        // GET: Widgets/Delete/5
        public ActionResult DeleteWidget(int? id)
        {
            ViewBag.AdminHeader = "Удаление";
            ViewBag.Title = ViewBag.AdminHeader;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // Widget widget = repository.GetWidgetById(id.Value);
            Widget widget = _widgetRepository.FindById(id.Value);
            if (widget == null)
            {
                return HttpNotFound();
            }
            return View(widget);
        }
        [HttpPost, ActionName("DeleteWidget")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            // repository.DeleteWidget(id);
            Widget widget = _widgetRepository.FindById(id);
            _widgetRepository.Delete(widget);
            _widgetRepository.Save();
            return RedirectToAction("Index");
        }
        public ActionResult CreateWidget(WidgetPosition? position, int? columnId)
        {
            ViewBag.AdminHeader = "Добавить Widget";
            ViewBag.Title = ViewBag.AdminHeader;
            ViewBag.ColumnId = columnId;
            ViewBag.Position = position;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult CreateWidget([Bind(Include = "Position,id,Title,Text,HasLinkBtn,Link,ColumnNumber,Style,Preority,Published")] Widget widget)
        {
            if (ModelState.IsValid)
            {
                _widgetRepository.Add(widget);
                _widgetRepository.Save();
             //   repository.AddWidget(widget);
                return RedirectToAction("index");
            }

            return View(widget);
        }
    }
   
}
