using System;
using System.Threading.Tasks;


namespace Funqy.CSharp
{
    public static class ExtendYourFunq
    {
        #region Get Funqy

        public static FunqResult<T> GetFunqy<T>(this T @this)
        {
            return @this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.Ok(@this);
        }


        public static FunqResult<Guid> GetFunqy(this Guid @this)
        {
            return @this == default(Guid)
                       ? FunqFactory.Fail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                       : FunqFactory.Ok(@this);
        }


        public static async Task<FunqResult<T>> GetFunqyAsync<T>(this T @this)
        {
            return await Task.FromResult(@this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.Ok(@this))
                             .ConfigureAwait(false);
        }


        public static async Task<FunqResult<T>> GetFunqyAsync<T>(this Task<T> thisTask)
        {
            var @this = await thisTask;
            return await Task.FromResult(@this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.Ok(@this))
                             .ConfigureAwait(false);
        }


        public static async Task<FunqResult<Guid>> GetFunqyAsync(this Guid @this)
        {
            return await Task.FromResult(@this == default(Guid)
                                             ? FunqFactory.Fail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                                             : FunqFactory.Ok(@this))
                             .ConfigureAwait(false);
        }

        #endregion


        #region Then

        public static FunqResult<TResult> Then<TResult>(this FunqResult @this, Func<FunqResult<TResult>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.Fail<TResult>(@this.Message, null);
            }
            var funq = callback();
            return funq;
        }


        public static FunqResult<TResult> Then<TResult>(this FunqResult<TResult> @this, Func<TResult, FunqResult<TResult>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.Fail(@this.Message, @this.Value);
            }
            var result = callback(@this.Value);
            return result;
        }


        public static FunqResult Then(this FunqResult @this, Func<FunqResult> callback)
        {
            return @this.IsFailure ? FunqFactory.Fail(@this.Message) : callback();
        }


        public static FunqResult Then<TResult>(this FunqResult<TResult> @this, Func<TResult, FunqResult> callback)
        {
            return @this.IsFailure ? FunqFactory.Fail(@this.Message) : callback(@this.Value);
        }
        #endregion
        
        #region Then Async

        public static async Task<FunqResult<TResult>> ThenAsync<TResult>(this FunqResult @this, Func<Task<FunqResult<TResult>>> callback)
        {
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail<TResult>(@this.Message, null)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult<TResult>> ThenAsync<TResult>(this Task<FunqResult> thisTask, Func<Task<FunqResult<TResult>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail<TResult>(@this.Message, null)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult<TResult>> ThenAsync<TResult>(this FunqResult<TResult> @this, Func<TResult, Task<FunqResult<TResult>>>  callback)
        {
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message, default(TResult)));
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult<TResult>> ThenAsync<TResult>(this Task<FunqResult<TResult>> thisTask, Func<TResult, Task<FunqResult<TResult>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message, default(TResult)));
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> ThenAsync(this FunqResult @this, Func<Task<FunqResult>> callback)
        {
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


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


        public static async Task<FunqResult> ThenAsync<TResult>(this FunqResult<TResult> @this, Func<TResult, Task<FunqResult>> callback)
        {
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> ThenAsync<TResult>(this Task<FunqResult<TResult>> thisTask, Func<TResult, Task<FunqResult>> callback)
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

        public static FunqResult<TResult> Catch<TResult>(this FunqResult<TResult> @this, Func<FunqResult<TResult>> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Value, @this.Message);
            }
            var errorFunq = callback();
            return errorFunq;
        }

        public static FunqResult<TResult> Catch<TResult>(this Task<FunqResult<TResult>> @this, Func<FunqResult<TResult>> callback)
        {
            var thisResult = @this.Result;
            if (thisResult.IsSuccessful)
            {
                return FunqFactory.Ok(thisResult.Value, thisResult.Message);
            }
            var errorFunq = callback();
            return errorFunq;
        }

        public static FunqResult<TResult> Catch<TResult>(this FunqResult<TResult> @this, Func<FunqResult<TResult>, FunqResult<TResult>> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Value, @this.Message);
            }
            var errorFunq = callback(@this);
            return errorFunq;
        }


        public static FunqResult<TResult> Catch<TResult>(this Task<FunqResult<TResult>> @this, Func<FunqResult<TResult>, FunqResult<TResult>> callback)
        {
            var thisResult = @this.Result;
            if (thisResult.IsSuccessful)
            {
                return FunqFactory.Ok(thisResult.Value, thisResult.Message);
            }
            var errorFunq = callback(thisResult);
            return errorFunq;
        }


        public static FunqResult Catch<TResult>(this FunqResult<TResult> @this, Func<FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Message);
            }
            var funq = callback();
            return funq;
        }


        public static FunqResult Catch(this FunqResult @this, Func<FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Message);
            }
            var errorFunq = callback();
            return errorFunq;
        }

        #endregion

        #region Catch Async

        public static async Task<FunqResult<TResult>> CatchAsync<TResult>(this FunqResult<TResult> @this, Func<Task<FunqResult<TResult>>> callback)
        {
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Value, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult<TResult>> CatchAsync<TResult>(this Task<FunqResult<TResult>> thisTask, Func<Task<FunqResult<TResult>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Value, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult<TResult>> CatchAsync<TResult>(this FunqResult<TResult> @this, Func<FunqResult<TResult>, Task<FunqResult<TResult>>> callback)
        {
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Value, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback(@this).ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult<TResult>> CatchAsync<TResult>(this Task<FunqResult<TResult>> thisTask, Func<FunqResult<TResult>, Task<FunqResult<TResult>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Value, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback(@this).ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult> CatchAsync<TResult>(this FunqResult<TResult> @this, Func<Task<FunqResult>> callback)
        {
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> CatchAsync<TResult>(this Task<FunqResult<TResult>> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask;
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> CatchAsync(this FunqResult @this, Func<Task<FunqResult>> callback)
        {
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult> CatchAsync(this Task<FunqResult> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask;
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }

        #endregion


        #region Finally

        public static FunqResult<TResult> Finally<TResult>(this FunqResult<TResult> @this, Func<FunqResult<TResult>, FunqResult<TResult>> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.Ok(@this, @this.Message)) : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }


        public static FunqResult Finally(this FunqResult @this, Func<FunqResult, FunqResult> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.Ok(@this)) : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }

        #endregion

        #region Finally Async

        public static async Task<FunqResult<TResult>> FinallyAsync<TResult>(this FunqResult<TResult> @this, Func<FunqResult<TResult>, Task<FunqResult<TResult>>> callback)
        {
            var result = @this.IsSuccessful
                             ? await callback(FunqFactory.Ok(@this, @this.Message)).ConfigureAwait(false)
                             : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        public static async Task<FunqResult> FinallyAsync(this FunqResult @this, Func<FunqResult, Task<FunqResult>> callback)
        {
            var result = @this.IsSuccessful
                             ? await callback(FunqFactory.Ok(@this))
                             : await callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }

        #endregion
    }
}