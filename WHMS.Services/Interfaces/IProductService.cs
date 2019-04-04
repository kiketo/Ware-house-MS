using WHMSData.Models;

namespace WHMS.Services.Interfaces
{
    public interface IProductService
    {
        Product CreateProduct(string name);
        Product DeleteProduct(string name);
        Product FindByName(string name);
        Product ModifyProductName(string name);
        void SetBuyPrice(int productId, decimal price);
        void SetMargin(int productId, double newMargin);
        void SetSellPrice(Product product);
        void SetSellPriceManually(int productId, decimal price);
    }
}