using Dominos.Models;

namespace Pharmacy_Website.Models
{
	public class VmHomePage
	{
		public VmHomePage() {
			lstProducts = new List<ProductsModel>();
			lstPharmcists = new List<PharmcistsModel>();
			lstSuppliers = new List<SupplierModel>();
			lstClassifications = new List<ProdcutsClassificationModel>();
		} 
		public List<ProductsModel> lstProducts { get; set; }
		public List<PharmcistsModel> lstPharmcists { get; set; }
		public List<SupplierModel> lstSuppliers { get; set; }
		public List<ProdcutsClassificationModel> lstClassifications { get; set; }
	}
}
