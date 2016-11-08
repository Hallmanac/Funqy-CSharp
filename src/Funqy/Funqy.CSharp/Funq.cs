using System;


namespace Funqy.CSharp
{
    public class Funq<T> : Funq
    {
        public Funq(T result, bool isSuccessful, string message = null)
            : base(isSuccessful, message)
        {
            Result = result;
        }


        public T Result { get; }
    }


    public class Funq
    {
        public Funq(bool isSuccessful, string message)
        {
            if (!isSuccessful && string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException("No error message provided for a non-successful result");
            }
            IsSuccessful = isSuccessful;
            Message = message;
        }


        public bool IsSuccessful { get; }

        public string Message { get; }

        public bool IsFailure => !IsSuccessful;
    }
}