using Dominos.Bl;
using Dominos.Models;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Website.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pharmacy_Website.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        ISupplier _Supplier;
        public SuppliersController(ISupplier sup)
        {
            _Supplier = sup;
        }


        // GET: api/<SuppliersController>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _Supplier.GetAll();
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return oApi;
        }

        // GET api/<SuppliersController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _Supplier.GetById(id);
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return oApi;
        }

        // POST api/<SuppliersController>
        [HttpPost]
        public ApiResponse Post([FromBody] SupplierModel supp)
        {
            try
            {
                _Supplier.Save(supp);
                ApiResponse oApi = new ApiResponse();
                oApi.Data = "The data has been saved successfully";
                oApi.Errors = null;
                oApi.StatusCode = "200";
                return oApi;
            }
            catch (Exception ex)
            {
                ApiResponse oApi = new ApiResponse();
                oApi.Data = "Error , Not Save";
                oApi.Errors = null;
                oApi.StatusCode = "502";
                return oApi;
            }
        }

        // POST api/<SuppliersController> / Delete / id
        [HttpPost]
        [Route("Delete")]
        public void Delete([FromBody] int id)
        {
            _Supplier.Delete(id);
        }
    }
}
