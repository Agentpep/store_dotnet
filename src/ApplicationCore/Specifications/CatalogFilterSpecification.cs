using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace ApplicationCore.Specifications
{

    public class CatalogFilterSpecification : BaseSpecification<CatalogItem>
    {
        public CatalogFilterSpecification(int? brandId, int? typeId, string searchString)
            : base(i => (!brandId.HasValue || i.CatalogBrandId == brandId) &&
                   (!typeId.HasValue || i.CatalogTypeId == typeId) && (searchString == null || Search(i.Name, searchString)))
        {
        }

        private static bool Search(string catalogItemName, string searchString)
        {
            var result = catalogItemName.ToLower().Contains(searchString.ToLower());
            return result;
        }
    }


}
