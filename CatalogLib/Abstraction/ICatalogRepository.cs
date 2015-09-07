using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Abstraction
{
   public interface ICatalogRepository
    {
        List<Product> GetProductsByCategory(string cat, bool onlyPublished);
        List<Product> GetAllProducts(bool onlyPublished);
        List<Category> GetCategorys(bool onlyPublished);
        Category GetCategoryByLinkName(string linkName);
        int AddCategory(Category cat);
        int AddProduct(Product p);
     
        void UpdateProduct(int productId);
        void UpdateProduct(Product p);
        void UpdateCategory(Category cat);
        void DeleteProduct(int productId);       
        void DeleteCategory(string catLinkName);
        Product GetProductById(int? id);
        List<Widget> GetWidgets(bool onlyPublished);
        void UpdateWindget(Widget widget);
        int DeleteWidget(int widgetId);
        Widget GetWidgetById(int id);
        int AddWidget(Widget widget);
        List<PayOutOption> GetPayOutOptions();
        PayOutOption GetPayOutOptionById(int id);
        int AddOrder(Order order);
        Order GetOrderById(int orderId);
        //Связывает продукт и заказ, возвращает номер связи
        int AddOrderProduct(Order order, Product product,int quantity);
        OrderProduct GetOrderProductById(int orderId);
    }
}
