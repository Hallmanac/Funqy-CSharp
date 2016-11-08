namespace Funqy.CSharp
{
    public class FunqFactory
    {
        public static Funq Ok(string message = null)
        {
            return new Funq(true, message);
        }


        public static Funq<T> Ok<T>(T result, string message = null)
        {
            return new Funq<T>(result, true, message);
        }


        public static Funq<T> Ok<T>(Funq<T> funqResult, string message = null)
        {
            return new Funq<T>(funqResult.Result, true, message);
        }


        public static Funq Fail(string message)
        {
            return new Funq(false, message);
        }


        public static Funq<T> Fail<T>(string message, T result = default(T))
        {
            return new Funq<T>(result, false, message);
        }


        public static Funq<T> Fail<T>(string message, Funq<T> funqResult = null)
        {
            return funqResult == null ? new Funq<T>(default(T), false, message) : new Funq<T>(funqResult.Result, false, message);
        }
    }
}