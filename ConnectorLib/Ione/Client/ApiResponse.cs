namespace ConnectorLib.Ione.Client
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public static bool IsValid (string responseText, out string errorMessage)
        {

            errorMessage = "";
            return true;
            
            //Todo: Validation 
            
            // var responseResult = JsonConvert.DeserializeObject<ApiResponse>(responseText);
            // errorMessage = responseResult.Message;
            // return responseResult.StatusCode == 200;
        }
    }
}
