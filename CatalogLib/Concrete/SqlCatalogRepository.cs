using CatalogLib.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatalogLib.Models;
using CatalogLib.DataBase;
using System.Data.Entity;

namespace CatalogLib.Concrete
{
    public class SqlCatalogRepository : ICatalogRepository
    {
        DataContext _db;
        public SqlCatalogRepository(DataContext db)
        {
            _db = db;
        }

        public int AddCategory(Category cat)
        {
            if (_db.Categorys.Find(cat.CategoryLinkName) != null)
                return -1;
            if (_db.Categorys.Count() == 0)
            {
                cat.ParentId = null;
                cat.Published = false;
            }
            cat.CreatedDate = DateTime.Now;
            _db.Categorys.Add(cat);
            _db.SaveChanges();
            return 0;
        }

        public int AddProduct(Product p)
        {
            if (_db.Products.Find(p.ProductId) != null)
                return -1;
            if (_db.Categorys.Where(c => c.CategoryLinkName == p.CategoryLinkName).SingleOrDefault().ParentId == null)
            {
                return -2;
            }
            p.CreatedDate = DateTime.Now;
            _db.Products.Add(p);
            _db.SaveChanges();
            return 0;
        }

        public void DeleteCategory(string catLinkName)
        {
            Category c= _db.Categorys.Find(catLinkName);

            if (c.SubCategories.Count > 0)
            {
                return;
            }
            _db.Categorys.Remove(c);
            _db.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            Product p= _db.Products.Find(productId);
            _db.Products.Remove(p);
            _db.SaveChanges();
        }
        public void UpdateCategory(Category cat)
        {
            Category category = _db.Categorys.Find(cat.CategoryLinkName);
            if (category != null)
            {
                category.CategoryDescription = cat.CategoryDescription;
                category.Published = cat.Published;
                category.CategoryName = cat.CategoryName;
                category.CategoryPictureUrl = cat.CategoryPictureUrl;
                if (category.ParentId != null)
                {
                    category.ParentId = cat.ParentId;
                }
                category.CreatedDate = DateTime.Now;
                _db.Entry(category).State = EntityState.Modified;
                _db.SaveChanges();
            } 
           
        }
        public void UpdateProduct(Product p)
        {
            if (_db.Categorys.Where(c=>c.CategoryLinkName==p.CategoryLinkName).SingleOrDefault().ParentId==null)
            {
                return;
            }
            p.CreatedDate = DateTime.Now;
            _db.Entry(p).State = EntityState.Modified;          
            _db.SaveChanges();
        }
        public void UpdateProduct(int productId)
        {
            throw new NotImplementedException();
        }
        public List<Product> GetAllProducts(bool onlyPublished)
        {if (onlyPublished)
                return _db.Products.Where(p => p.Published == true).ToList<Product>();
           return _db.Products.ToList<Product>();
        }

        public Category GetCategoryByLinkName(string linkName)
        {
            if (_db.Categorys.Find(linkName) == null)
                return null;         
                return _db.Categorys.Where(c => c.CategoryLinkName == linkName).SingleOrDefault();
        }

        public List<Category> GetCategorys(bool onlyPublished)
        {
            if (onlyPublished)
                return _db.Categorys.Where(c=>c.Published==true).ToList<Category>();
            return _db.Categorys.ToList<Category>();
        }

        public List<Product> GetProductsByCategory(string cat, bool onlyPublished)
        {
            if (_db.Categorys.Find(cat) == null)
                return null;
            if (onlyPublished)
                return _db.Products.Where(x => (x.Category.CategoryLinkName == cat)&(x.Published==true)).ToList<Product>();
            return _db.Products.Where(x => x.Category.CategoryLinkName == cat).ToList<Product>();
        }

        public Product GetProductById(int? id)
        {
            return _db.Products.Find(id);
        }

        public List<Widget> GetWidgets(bool onlyPublished)
        {
            if (onlyPublished)
           return     _db.Widgets.Where(w => w.Published == true).ToList<Widget>();
           return _db.Widgets.ToList<Widget>();
        }

        public void UpdateWindget(Widget widget)
        {
           
            _db.Entry(widget).State = EntityState.Modified;           
            _db.SaveChanges();
        }

        public int DeleteWidget(int widgetId)
        {
            Widget widget = _db.Widgets.Find(widgetId);
            if (widget == null)
            {
                return -1;
            }
            _db.Widgets.Remove(widget);
            _db.SaveChanges();
            return 0;
        }

        public Widget GetWidgetById(int id)
        {
            Widget w = GetWidgets(false).SingleOrDefault(wi => wi.id == id);
            return w;
        }

        public int AddWidget(Widget widget)
        {
            if (String.IsNullOrEmpty(widget.Style))
                widget.Style = "widget-default";
            _db.Widgets.Add(widget);
            _db.SaveChanges();
            return 0;
        }
        public List<PayOutOption> GetPayOutOptions()
        {
            return _db.PayOutOptions.ToList<PayOutOption>();
        }
       public PayOutOption GetPayOutOptionById(int id)
        {
            return _db.PayOutOptions.Find(id);
        }
        public int AddOrder(Order order)
        {
            _db.Orders.Add(order);
            try
            {
                _db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
            
            return order.OrderId;
        }
        public Order GetOrderById(int orderId)
        {
           return _db.Orders.Find(orderId);
        }
      public  int AddOrderProduct(Order order, Product product, int quantity)
        {
            OrderProduct op = new OrderProduct();
            op.Order = order;
            op.Product = product;
            op.Quantity = quantity;
            _db.Orders.Add(order);
            _db.OrderProducts.Add(op);
            _db.SaveChanges();
            return op.Id;
        }
      public  OrderProduct GetOrderProductById(int orderId)
        {
            return _db.OrderProducts.Find(orderId);
        }
    }
}
