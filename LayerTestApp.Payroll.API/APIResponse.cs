namespace LayerTestApp.Payroll.API
{
    public class APIResponse<T>
    {
        public bool IsSucceeded { get; set; }

        public int StatusCode { get; }

        public IEnumerable<string> Errors { get; set; }

        public T Result { get; set; }

        public APIResponse() { }

        public APIResponse(T result)
        {
            IsSucceeded = true;
            StatusCode = StatusCodes.Status200OK;
            Errors = new List<string>();
            Result = result;
        }

        public APIResponse(Exception ex)
        {
            IsSucceeded = false;
            StatusCode = ex.GetType().Name switch
            {
                "AccessDeniedException" => StatusCodes.Status403Forbidden,
                "ResourceNotFoundException" => StatusCodes.Status404NotFound,
                "BadRequestException" => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            Errors = new List<string>() { ex.Message };
            Result = default;
        }

        public APIResponse(IEnumerable<string> errors)
        {
            IsSucceeded = false;
            StatusCode = StatusCodes.Status400BadRequest;
            Errors = errors;
            Result = default;
        }

    }
}
