using System;
using System.Threading.Tasks;


namespace Funqy.CSharp_Net45
{
    /// <summary>
    /// Extensions methods that enable chaining the <see cref="FunqResult{T}"/>
    /// </summary>
    public static class ExtendYourFunq
    {
        #region Get Funqy

        /// <summary>
        /// Converts the specified object to a <see cref="FunqResult{T}"/>
        /// </summary>
        public static FunqResult<T> GetFunqy<T>(this T @this)
        {
            return @this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.Ok(@this);
        }


        /// <summary>
        /// Converts the specified object to a <see cref="FunqResult{T}"/>
        /// </summary>
        public static FunqResult<Guid> GetFunqy(this Guid @this)
        {
            return @this == default(Guid)
                ? FunqFactory.Fail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                : FunqFactory.Ok(@this);
        }


        /// <summary>
        /// Converts the specified object to a <see cref="FunqResult{T}"/> in an async manner to allow for async operations
        /// </summary>
        public static async Task<FunqResult<T>> GetFunqyAsync<T>(this T @this)
        {
            return await Task.FromResult(@this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.Ok(@this))
                             .ConfigureAwait(false);
        }


        /// <summary>
        /// Converts the specified object to a <see cref="FunqResult{T}"/> in an async manner to allow for async operations
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="thisTask"></param>
        /// <returns></returns>
        public static async Task<FunqResult<T>> GetFunqyAsync<T>(this Task<T> thisTask)
        {
            var @this = await thisTask;
            return await Task.FromResult(@this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.Ok(@this))
                             .ConfigureAwait(false);
        }


        /// <summary>
        /// Converts the specified object to a <see cref="FunqResult{T}"/> in an async manner to allow for async operations
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static async Task<FunqResult<Guid>> GetFunqyAsync(this Guid @this)
        {
            return await Task.FromResult(@this == default(Guid)
                                             ? FunqFactory.Fail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                                             : FunqFactory.Ok(@this))
                             .ConfigureAwait(false);
        }

        #endregion


        #region Then

        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/>.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static FunqResult<TOutput> Then<TOutput>(this FunqResult @this, Func<FunqResult<TOutput>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.Fail<TOutput>(@this.Message, null);
            }
            var funq = callback();
            return funq;
        }


        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/>.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static FunqResult<TOutput> Then<TInput, TOutput>(this FunqResult<TInput> @this, Func<TInput, FunqResult<TOutput>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.Fail(@this.Message, default(TOutput));
            }
            var result = callback(@this.Value);
            return result;
        }


        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/>.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static FunqResult Then(this FunqResult @this, Func<FunqResult> callback)
        {
            return @this.IsFailure ? FunqFactory.Fail(@this.Message) : callback();
        }


        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/>.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static FunqResult Then<TInput>(this FunqResult<TInput> @this, Func<TInput, FunqResult> callback)
        {
            return @this.IsFailure ? FunqFactory.Fail(@this.Message) : callback(@this.Value);
        }
        #endregion

        #region Then Async

        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/> in an async manner to allow for Async usage.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static async Task<FunqResult<TOutput>> ThenAsync<TOutput>(this Task<FunqResult> thisTask, Func<Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail<TOutput>(@this.Message, null)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/> in an async manner to allow for Async usage.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static async Task<FunqResult<TOutput>> ThenAsync<TOutput>(this Task<FunqResult<TOutput>> thisTask, Func<TOutput, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message, default(TOutput)));
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }


        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/> in an async manner to allow for Async usage.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static async Task<FunqResult<TOutput>> ThenAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisTask, Func<TInput, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message, default(TOutput))).ConfigureAwait(false);
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }


        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/> in an async manner to allow for Async usage.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static async Task<FunqResult> ThenAsync(this Task<FunqResult> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        /// <summary>
        /// Chains a method onto a <see cref="FunqResult{T}"/> in an async manner to allow for Async usage.
        /// </summary>
        /// <param name="callback">Method to be used as long as <see cref="FunqResult.IsSuccessful"/> is true</param>
        public static async Task<FunqResult> ThenAsync<TInput>(this Task<FunqResult<TInput>> thisTask, Func<TInput, Task<FunqResult>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }

        #endregion


        #region Catch 

        /// <summary>
        /// This is a way to chain a error handling callback method onto the end of the method chain. The "Catch" will always call the callback 
        /// method parameter. The callback itself is responsible for whether or not to pass along a success or fail.
        /// 
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult<TOutput> Catch<TInput, TOutput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult<TOutput>> callback)
        {
            // Catch should always call its callback. The Implementation of the catch callback should be responsible for returning an OK or a Fail
            var errorFunq = callback(@this);
            return errorFunq;
        }


        /// <summary>
        /// This is a way to chain a error handling callback method onto the end of the method chain. The "Catch" will always call the callback 
        /// method parameter. The callback itself is responsible for whether or not to pass along a success or fail.
        /// 
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult Catch<TInput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Message);
            }
            var funq = callback(@this);
            return funq;
        }


        /// <summary>
        /// This is a way to chain a error handling callback method onto the end of the method chain. The "Catch" will always call the callback 
        /// method parameter. The callback itself is responsible for whether or not to pass along a success or fail.
        /// 
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult Catch(this FunqResult @this, Func<FunqResult, FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Message);
            }
            var errorFunq = callback(@this);
            return errorFunq;
        }

        #endregion

        #region Catch Async

        /// <summary>
        /// This is an async way to chain a error handling callback method onto the end of the method chain. The "Catch" will always call the callback 
        /// method parameter. The callback itself is responsible for whether or not to pass along a success or fail.  
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult<TOutput>> CatchAsync<TOutput>(this Task<FunqResult<TOutput>> thisTask, Func<Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Value, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }


        /// <summary>
        /// This is an async way to chain a error handling callback method onto the end of the method chain. The "Catch" will always call the callback 
        /// method parameter. The callback itself is responsible for whether or not to pass along a success or fail.  
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult<TOutput>> CatchAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisTask, Func<FunqResult<TInput>, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);

            var errorFunq = await callback(@this).ConfigureAwait(false);
            return errorFunq;
        }


        /// <summary>
        /// This is an async way to chain a error handling callback method onto the end of the method chain. The "Catch" will always call the callback 
        /// method parameter. The callback itself is responsible for whether or not to pass along a success or fail.  
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult> CatchAsync<TInput>(this Task<FunqResult<TInput>> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        /// <summary>
        /// This is an async way to chain a error handling callback method onto the end of the method chain. The "Catch" will always call the callback 
        /// method parameter. The callback itself is responsible for whether or not to pass along a success or fail.  
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult> CatchAsync(this Task<FunqResult> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }

        #endregion


        #region Finally

        /// <summary>
        /// This is a way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult<TOutput> Finally<TInput, TOutput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult<TOutput>> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.Ok(@this, @this.Message)) : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }



        /// <summary>
        /// This is a way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult Finally<TInput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult> callback)
        {
            var result = @this.IsSuccessful
                ? callback(FunqFactory.Ok(@this, @this.Message))
                : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }



        /// <summary>
        /// This is a way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult<TOutput> Finally<TOutput>(this FunqResult @this, Func<FunqResult, FunqResult<TOutput>> callback)
        {
            var result = @this.IsSuccessful
                ? callback(FunqFactory.Ok(@this.Message))
                : callback(FunqFactory.Fail(@this.Message));
            return result;
        }


        /// <summary>
        /// This is a way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult Finally(this FunqResult @this, Func<FunqResult, FunqResult> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.Ok(@this)) : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }


        /// <summary>
        /// This is a way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult<TInput> Finally<TInput>(this FunqResult<TInput> @this, Action<FunqResult<TInput>> callback)
        {
            if (@this.IsSuccessful)
            {
                var okResult = FunqFactory.Ok(@this, @this.Message);
                callback(okResult);
                return okResult;
            }
            var failResult = FunqFactory.Fail(@this.Message, @this);
            callback(failResult);
            return failResult;
        }


        /// <summary>
        /// This is a way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static FunqResult Finally(this FunqResult @this, Action<FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                var okResult = FunqFactory.Ok(@this, @this.Message);
                callback(okResult);
                return okResult;
            }
            var failResult = FunqFactory.Fail(@this.Message);
            callback(failResult);
            return failResult;
        }

        #endregion

        #region Finally Async

        /// <summary>
        /// This is an async way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult<TOutput>> FinallyAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisAsync, Func<FunqResult<TInput>, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                ? await callback(FunqFactory.Ok(@this, @this.Message)).ConfigureAwait(false)
                : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        /// <summary>
        /// This is an async way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult<TOutput>> FinallyAsync<TOutput>(this Task<FunqResult> thisAsync, Func<FunqResult, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                ? await callback(FunqFactory.Ok(@this, @this.Message)).ConfigureAwait(false)
                : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        /// <summary>
        /// This is an async way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult> FinallyAsync<TInput>(this Task<FunqResult<TInput>> thisAsync, Func<FunqResult<TInput>, Task<FunqResult>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                ? await callback(FunqFactory.Ok(@this, @this.Message)).ConfigureAwait(false)
                : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        /// <summary>
        /// This is an async way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult> FinallyAsync(this Task<FunqResult> thisAsync, Func<FunqResult, Task<FunqResult>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                ? await callback(FunqFactory.Ok(@this)).ConfigureAwait(false)
                : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        /// <summary>
        /// This is an async way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult<TInput>> Finally<TInput>(this Task<FunqResult<TInput>> thisAsync, Func<FunqResult<TInput>, Task> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                var okResult = FunqFactory.Ok(@this, @this.Message);
                await callback(okResult).ConfigureAwait(false);
                return okResult;
            }
            var failResult = FunqFactory.Fail(@this.Message, @this);
            await callback(failResult).ConfigureAwait(false);
            return failResult;
        }


        /// <summary>
        /// This is an async way to chain a "Finally" handling callback method onto the end of the method chain. You can think of this as being 
        /// similar to a "finally" call in .Net. The "Finally" will always call the callback method parameter. The callback itself can
        /// be responsible for whether or not to pass along a success or fail. There are also overloads that will simply call the callback
        /// and return the current success/fail status.
        /// <para>
        /// For example: When there is an error and there is a way for the error to be corrected or an attempt to correct the error can be made. The Catch 
        /// chained method can correct the error and then allow the method chain to continue in a successful manner.
        /// </para>
        /// <para>
        /// Another example is where the Catch method can log an error.
        /// </para>
        /// </summary>
        /// <param name="callback">Method to be called (no matter what).</param>
        public static async Task<FunqResult> Finally(this Task<FunqResult> thisAsync, Func<FunqResult, Task> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                var okResult = FunqFactory.Ok(@this, @this.Message);
                await callback(okResult).ConfigureAwait(false);
                return okResult;
            }
            var failResult = FunqFactory.Fail(@this.Message);
            await callback(failResult).ConfigureAwait(false);
            return failResult;
        }

        #endregion
    }
}
