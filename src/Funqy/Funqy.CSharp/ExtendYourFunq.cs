using System;
using System.Threading.Tasks;


namespace Funqy.CSharp
{
    public static class ExtendYourFunq
    {
        #region Get Funqy

        public static Funq<T> GetFunqy<T>(this T @this)
        {
            return @this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.Ok(@this);
        }


        public static Funq<Guid> GetFunqy(this Guid @this)
        {
            return @this == default(Guid)
                       ? FunqFactory.Fail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                       : FunqFactory.Ok(@this);
        }


        public static async Task<Funq<T>> GetFunqyAsync<T>(this T @this)
        {
            return await Task.FromResult(@this == null ? FunqFactory.Fail<T>("Nothing to get Funqy with", null) : FunqFactory.Ok(@this))
                             .ConfigureAwait(false);
        }


        public static async Task<Funq<Guid>> GetFunqyAsync(this Guid @this)
        {
            return await Task.FromResult(@this == default(Guid)
                                             ? FunqFactory.Fail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                                             : FunqFactory.Ok(@this))
                             .ConfigureAwait(false);
        }

        #endregion


        #region Then

        public static Funq<TResult> Then<TResult>(this Funq @this, Func<Funq<TResult>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.Fail<TResult>(@this.Message, null);
            }
            var funq = callback();
            return funq;
        }


        public static Funq<TResult> Then<TInput, TResult>(this Funq<TInput> @this, Func<TInput, Funq<TResult>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.Fail(@this.Message, default(TResult));
            }
            var result = callback(@this.Result);
            return result;
        }


        public static Funq Then(this Funq @this, Func<Funq> callback)
        {
            return @this.IsFailure ? FunqFactory.Fail(@this.Message) : callback();
        }


        public static Funq Then<TInput>(this Funq<TInput> @this, Func<TInput, Funq> callback)
        {
            return @this.IsFailure ? FunqFactory.Fail(@this.Message) : callback(@this.Result);
        }
        #endregion
        
        #region Then Async

        public static async Task<Funq<TResult>> ThenAsync<TResult>(this Funq @this, Func<Task<Funq<TResult>>> callback)
        {
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail<TResult>(@this.Message, null)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<Funq<TResult>> ThenAsync<TInput, TResult>(this Funq<TInput> @this, Func<TInput, Task<Funq<TResult>>>  callback)
        {
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message, default(TResult)));
            }
            var funq = await callback(@this.Result).ConfigureAwait(false);
            return funq;
        }


        public static async Task<Funq> ThenAsync(this Funq @this, Func<Task<Funq>> callback)
        {
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<Funq> ThenAsync<TInput>(this Funq<TInput> @this, Func<TInput, Task<Funq>> callback)
        {
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.Fail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback(@this.Result).ConfigureAwait(false);
            return funq;
        }

        #endregion


        #region Catch 

        public static Funq<TResult> Catch<TResult>(this Funq<TResult> @this, Func<Funq<TResult>> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Result, @this.Message);
            }
            var errorFunq = callback();
            return errorFunq;
        }


        public static Funq<TResult> Catch<TResult>(this Funq<TResult> @this, Func<Funq<TResult>, Funq<TResult>> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Result, @this.Message);
            }
            var errorFunq = callback(@this);
            return errorFunq;
        }


        public static Funq Catch<TResult>(this Funq<TResult> @this, Func<Funq> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.Ok(@this.Message);
            }
            var funq = callback();
            return funq;
        }


        public static Funq Catch(this Funq @this, Func<Funq> callback)
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

        public static async Task<Funq<TResult>> CatchAsync<TResult>(this Funq<TResult> @this, Func<Task<Funq<TResult>>> callback)
        {
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Result, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<Funq<TResult>> CatchAsync<TResult>(this Funq<TResult> @this, Func<Funq<TResult>, Task<Funq<TResult>>> callback)
        {
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Result, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback(@this).ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<Funq> CatchAsync<TResult>(this Funq<TResult> @this, Func<Task<Funq>> callback)
        {
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<Funq> CatchAsync(this Funq @this, Func<Task<Funq>> callback)
        {
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.Ok(@this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }

        #endregion


        #region Finally

        public static Funq<TResult> Finally<TResult>(this Funq<TResult> @this, Func<Funq<TResult>, Funq<TResult>> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.Ok(@this, @this.Message)) : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }


        public static Funq Finally(this Funq @this, Func<Funq, Funq> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.Ok(@this)) : callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }

        #endregion

        #region Finally Async

        public static async Task<Funq<TResult>> FinallyAsync<TResult>(this Funq<TResult> @this, Func<Funq<TResult>, Task<Funq<TResult>>> callback)
        {
            var result = @this.IsSuccessful
                             ? await callback(FunqFactory.Ok(@this, @this.Message)).ConfigureAwait(false)
                             : await callback(FunqFactory.Fail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        public static async Task<Funq> FinallyAsync(this Funq @this, Func<Funq, Task<Funq>> callback)
        {
            var result = @this.IsSuccessful
                             ? await callback(FunqFactory.Ok(@this))
                             : await callback(FunqFactory.Fail(@this.Message, @this));
            return result;
        }

        #endregion
    }
}