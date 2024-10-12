namespace Dominos.Models
{
    internal class VmProductDetails
    {
        public VmProductDetails() {
            Product = new ProductsModel();
            lstImages = new List<ImagesModel>();
        }   
        public ProductsModel Product { get; set; }
        public List<ImagesModel> lstImages { get; set; }

    }
}
