namespace Sora.Services.ViewModels
{
    public class ProductViewModel
    {
        public int? ICProductID { get; set; }

        public int? FK_ICProductGroupID { get; set; }

        public int? FK_MESpecialistID { get; set; }

        public string ICProductType { get; set; }

        public string ICProductName { get; set; }

        public string ICProductTitle { get; set; }

        public string ICProductContent { get; set; }

        public bool ICProductIsShowWeb { get; set; }

        public decimal ICProductPrice { get; set; }

        public string ICProductPicture { get; set; }

        public bool ICProductIsDetail { get; set; }

        public ProductGroupViewModel ProductGroup { get; set; }

        public ShortSpecialistDto Specialist { get; set; }

        public string ChildProductString { get; set; } = "[]";

        public ProductDetailDto[] ProductDetails { get; set; }
    }
}
