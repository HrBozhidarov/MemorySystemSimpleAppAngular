namespace MemorySystem.Controllers.Models.Output
{
#pragma warning disable SA1402 // File may only contain a single type
    public class SuccessResponseModel<TData> : SuccessResponseModel
#pragma warning restore SA1402 // File may only contain a single type
    {
        public TData Data { get; set; }
    }

    public class SuccessResponseModel
    {
        public int StatusCode { get; set; }
    }
}
