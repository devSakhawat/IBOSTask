namespace Api.Error;

public class ApiResponse
{
   public ApiResponse(int statusCode, string message = null)
   {
      StatusCode = statusCode;
      Message = message ?? GetDefaultMessageForStatusCode(statusCode);
   }

   public int StatusCode { get; set; }
   public string Message { get; set; }

   private string GetDefaultMessageForStatusCode(int statusCode)
   {
      return statusCode switch
      {
         200 => "SuccessfulRecord successfully saved.",
         400 => "A bad request, you have made!",
         401 => "Authorized, you are not!",
         403 => "Already exists",
         404 => "Resource found, it was not!",
         409 => "Conflict!",
         500 => "Internal server error!",
         _ => null
      };
   }
}
