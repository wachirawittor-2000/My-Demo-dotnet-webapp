namespace My_Demo_webapp.Models
{
    public class ResponseModel
    {
        public string Status { get; set; }
        public object Result { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
