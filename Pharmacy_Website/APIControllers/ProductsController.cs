using Dominos.Bl;
using Dominos.Models;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Website.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pharmacy_Website.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IProducts _IProd;
        public ProductsController(IProducts pro)
        {
            _IProd = pro;
        }

        // GET: api/<ProductsController>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _IProd.GetAll();
            oApi.Errors = null;
            oApi.StatusCode = "200";

            return oApi;
        }


        // GET api/<ProductsController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _IProd.GetById(id);
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return oApi;
        }

        // GET api/<ProductsController>/5
        [HttpGet("GetByProduct/{productId}")]
        public ApiResponse GetByProduct(int productId)
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _IProd.GetAllItemsData(productId);
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return oApi;
        }


        // GET api/<ProductsController>/5
        [HttpGet("GetByProductWithImag/{productId}")]
        public ProductsModel GetByProductWithImag(int productId)
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _IProd.GetByIdWithImages(productId);
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return _IProd.GetByIdWithImages(productId);
        }


        // POST api/<ProductsController> & it's get all pharmcises in db
        [HttpPost]
        public ApiResponse Post([FromBody] ProductsModel product)
        {
            try
            {
                _IProd.Save(product);
                ApiResponse oApi = new ApiResponse();
                oApi.Data = "Done , it's successfuly save data ";
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

        //[HttpPost , ActionName("Delete")]
        //[Route("Delete")]
        //public void Delete([FromBody] int id)
        //{
        //    _IProd.Delete(id);
        //}

        //[HttpPost]
        //[Route("Delete")]
        //public void Delete([FromBody] int id)
        //{
        //    _IProd.Delete(id);
        //}

        // PUT api/<ProductsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ProductsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
