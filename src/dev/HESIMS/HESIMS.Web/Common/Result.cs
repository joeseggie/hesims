namespace HESIMS.Web.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public T? Value { get; set; }

        protected Result(bool isSuccess, string errorMessage, T? value)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
            Value = value;
        }

        public static Result<T> Failure(string errorMessage)
        {
            return new Result<T>(false, errorMessage, default);
        }

        public static Result<T> Success(T value)
        {
            return new Result<T>(true, string.Empty, value);
        }
    }
}