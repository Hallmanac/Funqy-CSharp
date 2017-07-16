namespace Funqy.CSharp
{
    public class FunqFactory
    {
        public static FunqResult Ok(string message = null)
        {
            return new FunqResult(true, message);
        }


        public static FunqResult<T> Ok<T>(T result, string message = null)
        {
            return new FunqResult<T>(result, true, message);
        }


        public static FunqResult<T> Ok<T>(FunqResult<T> funqResultResult, string message = null)
        {
            return new FunqResult<T>(funqResultResult.Result, true, message);
        }


        public static FunqResult Fail(string message)
        {
            return new FunqResult(false, message);
        }


        public static FunqResult<T> Fail<T>(string message, T result = default(T))
        {
            return new FunqResult<T>(result, false, message);
        }


        public static FunqResult<T> Fail<T>(string message, FunqResult<T> funqResultResult = null)
        {
            return funqResultResult == null ? new FunqResult<T>(default(T), false, message) : new FunqResult<T>(funqResultResult.Result, false, message);
        }
    }
}