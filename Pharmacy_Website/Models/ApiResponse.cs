namespace Pharmacy_Website.Models
{
    public class ApiResponse
    {
        // data coming from api
        public object Data { get; set; }
        // list of api errors
        public object Errors { get; set; }
        // api status code 200 = success 400 = failed 
        public string StatusCode { get; set; }
    }
}
