using Dominos.Bl;
using Dominos.Models;
using Microsoft.AspNetCore.Mvc;
using Pharmacy_Website.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pharmacy_Website.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PharmcistsController : ControllerBase
    {
        IPharmicsts _IPharm;
        public PharmcistsController(IPharmicsts pharm)
        {
            _IPharm = pharm;
        }

        // GET: api/<PharmcistsController>
        [HttpGet]
        public ApiResponse Get()
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _IPharm.GetAll();
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return oApi;
        }

        // GET api/<PharmcistsController>/5
        [HttpGet("{id}")]
        public ApiResponse Get(int id)
        {
            ApiResponse oApi = new ApiResponse();
            oApi.Data = _IPharm.GetById(id);
            oApi.Errors = null;
            oApi.StatusCode = "200";
            return oApi;
        }

        // POST api/<PharmcistsController>
        [HttpPost]
        public ApiResponse Post([FromBody] PharmcistsModel pharm)
        {
            try
            {
                _IPharm.Save(pharm);
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

        // DELETE api/<PharmcistsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
