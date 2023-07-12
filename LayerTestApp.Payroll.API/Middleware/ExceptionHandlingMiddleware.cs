namespace LayerTestApp.Payroll.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger Logger;
        private readonly RequestDelegate RequestDelegate;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, RequestDelegate requestDelegate)
        {
            Logger = logger;
            RequestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await RequestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        public Task HandleException(HttpContext context, Exception ex)
        {
            Logger.LogError("Error:{errorMessage}", ex.Message);
            int statusCode = ex.GetType().Name switch
            {
                "AccessDeniedException" => StatusCodes.Status403Forbidden,
                "ResourceNotFoundException" => StatusCodes.Status404NotFound,
                "BadRequestException" => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

            List<string> errors = new() { ex.Message };
            // var json = JsonConvert.SerializeObject(new APIResponse<string>(errors));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsJsonAsync(new APIResponse<string>(errors));
        }

    }
}
