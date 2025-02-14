using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IReadOnlyList<Product>> GetProductAsync(string? brands, string? types,string? sort); // passing the parameter for filtering 
        Task<Product?> GetProductByIdAsync(int id);  //"?" this if the product exists or not 

        Task<IReadOnlyList<string>> GetBrandsAsync();
        Task<IReadOnlyList<string>> GetTypeAsync();
        void AddProduct(Product product); // void is used because it does not directly contacted database That is why is "sync"
        void UpdateProduct(Product product);    
        void DeleteProduct(Product product);

        bool ProductExists(int id);
        Task<bool> SaveChangesAsync(); // if anything change it return true


    }
}
