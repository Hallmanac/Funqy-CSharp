using System;


namespace Funqy.CSharp_Legacy
{
    /// <summary>
    /// Class to help write C# in a more functional style. Coupled with extension methods from ExtendYourFunq.cs.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FunqResult<T> : FunqResult
    {
        public FunqResult(T value, bool isSuccessful, string message = null)
            : base(isSuccessful, message)
        {
            Value = value;
        }


        public T Value { get; }
    }


    /// <summary>
    /// Class to help write C# in a more functional style. Coupled with extension methods from ExtendYourFunq.cs.
    /// </summary>
    public class FunqResult
    {
        public FunqResult(bool isSuccessful, string message)
        {
            if (!isSuccessful && string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException("No error message provided for a non-successful value");
            }
            IsSuccessful = isSuccessful;
            Message = message;
        }


        public bool IsSuccessful { get; }

        public string Message { get; }

        public bool IsFailure => !IsSuccessful;
    }
}