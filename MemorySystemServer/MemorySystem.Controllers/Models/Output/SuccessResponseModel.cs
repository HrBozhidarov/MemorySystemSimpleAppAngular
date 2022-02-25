namespace MemorySystem.Controllers.Models.Output
{
    public class SuccessResponseModel<TData> : SuccessResponseModel
    {
        public TData Data { get; set; }
    }

    public class SuccessResponseModel
    {
        public int StatusCode { get; set; }
    }
}
