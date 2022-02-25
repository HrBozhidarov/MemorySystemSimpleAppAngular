using MemorySystem.Common;
using MemorySystem.Common.Infrastructure.AutomapperSettings;
using MemorySystem.Controllers.Models.Output;
using MemorySystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace MemorySystem.Controllers
{
    public class ResponseController : ApiController
    {
        public IActionResult ResponseResult<TResult>(Result<TResult> result)
        {
            if (result.IfHasError)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = result.ErrorMessage,
                    StatusCode = StatusCodeConstants.BadRequest,
                });
            }

            return this.Ok(new SuccessResponseModel<TResult>
            {
                Data = Mapper.Map<TResult>(result.Data),
                StatusCode = StatusCodeConstants.Ok,
            });
        }

        public IActionResult ResponseResult(Result result)
        {
            if (result.IfHasError)
            {
                return this.BadRequest(new ErrorResponseModel
                {
                    ErrorMessage = result.ErrorMessage,
                    StatusCode = StatusCodeConstants.BadRequest,
                });
            }

            return this.Ok(new SuccessResponseModel { StatusCode = StatusCodeConstants.Ok });
        }
    }
}
