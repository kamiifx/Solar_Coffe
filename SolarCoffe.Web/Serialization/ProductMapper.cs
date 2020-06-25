using SolarCoffe.Web.ViewModels;

namespace SolarCoffe.Web.Serialization
{
    public static class ProductMapper
    {
        //Maps a Product data model to a productModel view model
        public static ProductModel SerializeProductModel(Data.Models.Product product)
        {
            return new ProductModel
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsArchived = product.IsArchived
            };
        }

        public static Data.Models.Product SerializeProductModel(ProductModel product)
        {
            return new Data.Models.Product
            {
                Id = product.Id,
                CreatedOn = product.CreatedOn,
                UpdatedOn = product.UpdatedOn,
                Price = product.Price,
                Name = product.Name,
                Description = product.Description,
                IsArchived = product.IsArchived
            };
        }
    }
}