using Dominos.Bl;
using Dominos.Models;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Website.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pharmacy_Website.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdcutsClassificationController : ControllerBase
    {
        IClassification _IClassific;
        public ProdcutsClassificationController(IClassification calss)
        {
            _IClassific = calss;
        }


        // GET: api/<ProdcutsClassificationController>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _IClassific.GetAll();
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return oApi;
        }

        // GET api/<ProdcutsClassificationController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _IClassific.GetById(id);
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return oApi;
        }

        // POST api/<ProdcutsClassificationController>
        [HttpPost]
        public ApiResponse Post([FromBody] ProdcutsClassificationModel classific)
        {
            try
            {
                _IClassific.Save(classific);
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

        // PUT api/<ProdcutsClassificationController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ProdcutsClassificationController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
