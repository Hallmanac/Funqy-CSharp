﻿using System;


namespace Funqy.CSharp
{
    /// <summary>
    /// Class to hold the result of a function that wraps an operation which will return a value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FunqResult<T> : FunqResult
    {
        /// <summary>
        /// Constructor for the FunqResult with a Value result
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isSuccessful"></param>
        /// <param name="message"></param>
        public FunqResult(T value, bool isSuccessful, string message = null)
            : base(isSuccessful, message)
        {
            Value = value;
        }


        /// <summary>
        /// Returns whether the <see cref="Value"/> property contains a value and that it's NOT Empty or Whitespace in the case of a string
        /// </summary>
        public bool HasValue
        {
            get
            {
                if (typeof(T) != typeof(string))
                    return Value != null;
                var strVal = Value as string;
                return !string.IsNullOrWhiteSpace(strVal);
            }
        }

        /// <summary>
        /// The Value of the result. This could potentially be nullable. It is recommended that you check
        /// the <see cref="HasValue"/> property before trying to access this value.
        /// </summary>
        public T Value { get; }
    }


    /// <summary>
    /// Class to hold the result of a function that wraps an action operation which will NOT return a value.
    /// </summary>
    public class FunqResult
    {
        /// <summary>
        /// Constructor for a FunqResult without a value
        /// </summary>
        /// <param name="isSuccessful"></param>
        /// <param name="message"></param>
        public FunqResult(bool isSuccessful, string message)
        {
            if (!isSuccessful && string.IsNullOrWhiteSpace(message))
            {
                throw new InvalidOperationException("No error message provided for a non-successful value");
            }
            IsSuccessful = isSuccessful;
            Message = message;
        }


        /// <summary>
        /// Determines whether or not the operation was successful
        /// </summary>
        public bool IsSuccessful { get; }

        /// <summary>
        /// Text that can be used to convey an error or success message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Determines whether the operation was a failure
        /// </summary>
        public bool IsFailure => !IsSuccessful;
    }
}