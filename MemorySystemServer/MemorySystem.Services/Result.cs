namespace MemorySystem.Services
{
    using System;
    using System.Text.Json.Serialization;

    public class Result
    {
        [JsonIgnore]
        public static Result Success => new Result();

        public string ErrorMessage { get; protected set; }

        [JsonIgnore]
        public bool IfHaveError => !string.IsNullOrWhiteSpace(this.ErrorMessage);

        public static Result Error(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentException("Error message can not be null or empty");
            }

            return new Result { ErrorMessage = errorMessage };
        }
    }

#pragma warning disable SA1402 // File may only contain a single type
    public class Result<TData> : Result
#pragma warning restore SA1402 // File may only contain a single type
    {
        public TData Data { get; private set; }

        public static new Result<TData> Success(TData data) => new Result<TData> { Data = data };

        public static new Result<TData> Error(string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(errorMessage))
            {
                throw new ArgumentException("Error message can not be null or empty");
            }

            return new Result<TData> { ErrorMessage = errorMessage };
        }
    }
}
