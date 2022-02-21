using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoutiqueDataConnection.Entities
{
    public class ProductDetailsClass
    {
        public int ProductId { get; set; }
        public string ProductType { get; set; }
        public string ProductPrice { get; set; }
        public string ProductFabric { get; set; }
        public string ProductSize { get; set; }
    }
    public class ProductClass
    {
        public int BoutiqueId { get; set; }
        public string BoutiqueName { get; set; }
        public string BoutiqueAddress { get; set; }
        public string BoutiqueContact { get; set; }
        public int ProductId { get; set; }
    }
}

namespace BoutiqueDataConnection.DataLayer
{
    using BoutiqueDataConnection.Entities;
    using System.Collections.Generic;
    using BoutiqueDataConnection.DataComponents;
    using System.Linq;
    public interface IProductDB
    {
        void AddNewProduct(ProductClass product);
        void UpdateProduct(ProductClass product);
        void DeleteProduct(int pId);
        List<ProductClass> GetAllProducts();
    }

    public static class ProductFactory
    {
        public static IProductDB CreateComponent()
        {
            return new ProductDB();
        }
    }
    class ProductDB : IProductDB
    {
        private ProductClass do_Convert(Product tableData)
        {
            return new ProductClass
            {
                BoutiqueId = tableData.BoutiqueId,
                BoutiqueName = tableData.BoutiqueName,
                BoutiqueAddress = tableData.BoutiqueAddress,
                BoutiqueContact = tableData.BoutiqueContact,
                ProductId = Convert.ToInt32(tableData.ProductId)
            };
        }
        private Product do_Convert(ProductClass tableData)
        {
            return new Product
            {
                BoutiqueId = tableData.BoutiqueId,
                BoutiqueName = tableData.BoutiqueName,
                BoutiqueAddress = tableData.BoutiqueAddress,
                BoutiqueContact = tableData.BoutiqueContact,
                ProductId = Convert.ToInt32(tableData.ProductId)
            };
        }
        public void AddNewProduct(ProductClass product)
        {
            var pRecord = do_Convert(product);
            pRecord.ProductId = 0;//We dont pass the Patient ID as it is auto generated. 
            var context = new BoutiqueEntities();
            context.Products.Add(pRecord);
            context.SaveChanges();
            //throw new NotImplementedException();
        }

        public void DeleteProduct(int pId)
        {
            throw new NotImplementedException("");
        }

        public List<ProductClass> GetAllProducts()
        {
            var context = new BoutiqueEntities();
            var records = (from rec in context.Products
                           select new ProductClass
                           {
                               BoutiqueId = rec.BoutiqueId,
                               BoutiqueName = rec.BoutiqueName,
                               BoutiqueAddress = rec.BoutiqueAddress,
                               BoutiqueContact = rec.BoutiqueContact,
                               ProductId = Convert.ToInt32(rec.ProductId)
                           }).ToList();
            return records;
        }

        public void UpdateProduct(ProductClass details)
        {
            var data = do_Convert(details);
            var context = new BoutiqueEntities();
            var selected = context.Products.FirstOrDefault((p) => p.ProductId == data.ProductId);
            selected = data;
            context.SaveChanges();
        }
    }
}
