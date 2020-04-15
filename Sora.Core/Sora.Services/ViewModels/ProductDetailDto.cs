namespace Sora.Services.ViewModels
{
    public class ProductDetailDto
    {
        public int ICProductDetailID { get; set; }

        public decimal ICProductDetailQty { get; set; }

        public decimal ICProductDetailPrice { get; set; }

        public decimal ICProductDetailTotalAmout { get; set; }

        public ProductViewModel Product { get; set; }
    }
}
