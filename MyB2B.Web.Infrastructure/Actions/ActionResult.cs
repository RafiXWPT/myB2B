namespace MyB2B.Web.Infrastructure.Actions
{
    public class ActionResult<T> where T: class
    {
        public bool Success { get; }
        public string Message { get; }
        public T Result { get; }

        private ActionResult(bool success, string message, T result)
        {
            Success = success;
            Message = message;
            Result = result;
        }

        public static ActionResult<T> Done() => new ActionResult<T>(true, null, null);
        public static ActionResult<T> Done(string message) => new ActionResult<T>(true, message, null);
        public static ActionResult<T> Done(T result) => new ActionResult<T>(true, null, result);
        public static ActionResult<T> Done(string message, T result) => new ActionResult<T>(true, message, result);

        public static ActionResult<T> Fail(string message) => new ActionResult<T>(false, message, null);
        public static ActionResult<T> Fail(string message, T result) => new ActionResult<T>(false, message, result);

        public override string ToString()
        {
            return $"Success: {Success}, Message: {Message}, Result: {Result}";
        }
    }

    public static class ActionResultExtensions
    {
        public static ActionResult<T> WithEmptyActionResult<T>(this T result, string message = "") where T: class
        {
            return result == null ? ActionResult<T>.Done() : ActionResult<T>.Fail(message);
        }

        public static ActionResult<T> WithAnyActionResult<T>(this T result, string message = "sequence is empty") where T : class
        {
            return result == null ? ActionResult<T>.Fail(message) : ActionResult<T>.Done(result);
        }
    }
}
