using System;
using System.Threading.Tasks;


namespace Funqy.CSharp
{
    public static class ExtendYourFunq
    {
        #region Get Funqy

        public static FunqResult<T> GetFunqy<T>(this T @this)
        {
            return @this == null ? FunqFactory.ResultFail<T>("Nothing to get Funqy with", null) : FunqFactory.ResultOk(@this);
        }


        public static FunqResult<Guid> GetFunqy(this Guid @this)
        {
            return @this == default(Guid)
                ? FunqFactory.ResultFail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                : FunqFactory.ResultOk(@this);
        }


        public static async Task<FunqResult<T>> GetFunqyAsync<T>(this T @this)
        {
            return await Task.FromResult(@this == null ? FunqFactory.ResultFail<T>("Nothing to get Funqy with", null) : FunqFactory.ResultOk(@this))
                             .ConfigureAwait(false);
        }


        public static async Task<FunqResult<T>> GetFunqyAsync<T>(this Task<T> thisTask)
        {
            var @this = await thisTask;
            return await Task.FromResult(@this == null ? FunqFactory.ResultFail<T>("Nothing to get Funqy with", null) : FunqFactory.ResultOk(@this))
                             .ConfigureAwait(false);
        }


        public static async Task<FunqResult<Guid>> GetFunqyAsync(this Guid @this)
        {
            return await Task.FromResult(@this == default(Guid)
                                             ? FunqFactory.ResultFail<Guid>("The Guid provided to get funqy was the default Guid value and invalid", null)
                                             : FunqFactory.ResultOk(@this))
                             .ConfigureAwait(false);
        }

        #endregion


        #region Then

        public static FunqResult<TOutput> Then<TOutput>(this FunqResult @this, Func<FunqResult<TOutput>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.ResultFail<TOutput>(@this.Message, null);
            }
            var funq = callback();
            return funq;
        }


        public static FunqResult<TOutput> Then<TInput, TOutput>(this FunqResult<TInput> @this, Func<TInput, FunqResult<TOutput>> callback)
        {
            if (@this.IsFailure)
            {
                return FunqFactory.ResultFail(@this.Message, default(TOutput));
            }
            var result = callback(@this.Value);
            return result;
        }


        public static FunqResult Then(this FunqResult @this, Func<FunqResult> callback)
        {
            return @this.IsFailure ? FunqFactory.ResultFail(@this.Message) : callback();
        }


        public static FunqResult Then<TInput>(this FunqResult<TInput> @this, Func<TInput, FunqResult> callback)
        {
            return @this.IsFailure ? FunqFactory.ResultFail(@this.Message) : callback(@this.Value);
        }
        #endregion

        #region Then Async

        public static async Task<FunqResult<TOutput>> ThenAsync<TOutput>(this Task<FunqResult> thisTask, Func<Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.ResultFail<TOutput>(@this.Message, null)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult<TOutput>> ThenAsync<TOutput>(this Task<FunqResult<TOutput>> thisTask, Func<TOutput, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.ResultFail(@this.Message, default(TOutput)));
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult<TOutput>> ThenAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisTask, Func<TInput, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.ResultFail(@this.Message, default(TOutput))).ConfigureAwait(false);
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> ThenAsync(this Task<FunqResult> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.ResultFail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> ThenAsync<TInput>(this Task<FunqResult<TInput>> thisTask, Func<TInput, Task<FunqResult>> callback)
        {
            var @this = await thisTask;
            if (@this.IsFailure)
            {
                return await Task.FromResult(FunqFactory.ResultFail(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback(@this.Value).ConfigureAwait(false);
            return funq;
        }

        #endregion


        #region Catch 

        public static FunqResult<TOutput> Catch<TInput, TOutput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult<TOutput>> callback)
        {
            // Catch should always call its callback. The Implementation of the catch should be responsible for returning an OK or a ResultFail
            var errorFunq = callback(@this);
            return errorFunq;
        }


        public static FunqResult Catch<TInput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.ResultOk(@this.Message);
            }
            var funq = callback(@this);
            return funq;
        }


        public static FunqResult Catch(this FunqResult @this, Func<FunqResult, FunqResult> callback)
        {
            if (@this.IsSuccessful)
            {
                return FunqFactory.ResultOk(@this.Message);
            }
            var errorFunq = callback(@this);
            return errorFunq;
        }

        #endregion

        #region Catch Async

        public static async Task<FunqResult<TOutput>> CatchAsync<TOutput>(this Task<FunqResult<TOutput>> thisTask, Func<Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.ResultOk(@this.Value, @this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult<TOutput>> CatchAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisTask, Func<FunqResult<TInput>, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);

            var errorFunq = await callback(@this).ConfigureAwait(false);
            return errorFunq;
        }


        public static async Task<FunqResult> CatchAsync<TInput>(this Task<FunqResult<TInput>> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.ResultOk(@this.Message)).ConfigureAwait(false);
            }
            var funq = await callback().ConfigureAwait(false);
            return funq;
        }


        public static async Task<FunqResult> CatchAsync(this Task<FunqResult> thisTask, Func<Task<FunqResult>> callback)
        {
            var @this = await thisTask.ConfigureAwait(false);
            if (@this.IsSuccessful)
            {
                return await Task.FromResult(FunqFactory.ResultOk(@this.Message)).ConfigureAwait(false);
            }
            var errorFunq = await callback().ConfigureAwait(false);
            return errorFunq;
        }

        #endregion


        #region Finally

        public static FunqResult<TOutput> Finally<TInput, TOutput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult<TOutput>> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.ResultOk(@this, @this.Message)) : callback(FunqFactory.ResultFail(@this.Message, @this));
            return result;
        }


        public static FunqResult Finally<TInput>(this FunqResult<TInput> @this, Func<FunqResult<TInput>, FunqResult> callback)
        {
            var result = @this.IsSuccessful
                ? callback(FunqFactory.ResultOk(@this, @this.Message))
                : callback(FunqFactory.ResultFail(@this.Message, @this));
            return result;
        }


        public static FunqResult<TOutput> Finally<TOutput>(this FunqResult @this, Func<FunqResult, FunqResult<TOutput>> callback)
        {
            var result = @this.IsSuccessful
                ? callback(FunqFactory.ResultOk(@this.Message))
                : callback(FunqFactory.ResultFail(@this.Message));
            return result;
        }


        public static FunqResult Finally(this FunqResult @this, Func<FunqResult, FunqResult> callback)
        {
            var result = @this.IsSuccessful ? callback(FunqFactory.ResultOk(@this)) : callback(FunqFactory.ResultFail(@this.Message, @this));
            return result;
        }

        #endregion

        #region Finally Async

        public static async Task<FunqResult<TOutput>> FinallyAsync<TInput, TOutput>(this Task<FunqResult<TInput>> thisAsync, Func<FunqResult<TInput>, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                ? await callback(FunqFactory.ResultOk(@this, @this.Message)).ConfigureAwait(false)
                : await callback(FunqFactory.ResultFail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        public static async Task<FunqResult<TOutput>> FinallyAsync<TOutput>(this Task<FunqResult> thisAsync, Func<FunqResult, Task<FunqResult<TOutput>>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                ? await callback(FunqFactory.ResultOk(@this, @this.Message)).ConfigureAwait(false)
                : await callback(FunqFactory.ResultFail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        public static async Task<FunqResult> FinallyAsync<TInput>(this Task<FunqResult<TInput>> thisAsync, Func<FunqResult<TInput>, Task<FunqResult>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                ? await callback(FunqFactory.ResultOk(@this, @this.Message)).ConfigureAwait(false)
                : await callback(FunqFactory.ResultFail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }


        public static async Task<FunqResult> FinallyAsync(this Task<FunqResult> thisAsync, Func<FunqResult, Task<FunqResult>> callback)
        {
            var @this = await thisAsync.ConfigureAwait(false);
            var result = @this.IsSuccessful
                ? await callback(FunqFactory.ResultOk(@this)).ConfigureAwait(false)
                : await callback(FunqFactory.ResultFail(@this.Message, @this)).ConfigureAwait(false);
            return result;
        }

        #endregion
    }
}