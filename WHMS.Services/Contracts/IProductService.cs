using WHMSData.Models;

namespace WHMS.Services.Contracts
{
    public interface IProductService
    {
        Product CreateProduct(string name);
        Product ModifyProductName(string name);
        Product DeleteProduct(string name);
        Product FindByName(string name);
        Product SetBuyPrice(int productId, decimal price);
        Product SetMargin(int productId, double newMargin);
        Product SetSellPrice(int productId, decimal price);
    }
}