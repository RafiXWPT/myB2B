using System;

namespace MyB2B.Domain.Results
{
    public static class ResultExtensions
    {
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            return result ? func() : result;
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<T> func)
        {
            return result ? Result.Ok(func()) : Result.Fail<T>(result.Error);
        }

        public static Result<T> OnSuccess<T>(this Result result, Func<Result<T>> func)
        {
            return result ? func() : Result.Fail<T>(result.Error);
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result)
            {
                action();
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFail)
            {
                action();
            }

            return result;
        }

        public static Result OnFailure(this Result result, Action<string> action)
        {
            if (result.IsFail)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result OnSuccess<T>(this Result<T> result, Func<T, Result> func)
        {
            return result ? func(result.Value) : Result.Fail(result.Error);
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<Result<K>> func)
        {
            return result ? func() : Result.Fail<K>(result.Error);
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, K> func)
        {
            return result ? Result.Ok(func(result.Value)) : Result.Fail<K>(result.Error);
        }

        public static Result<K> OnSuccess<T, K>(this Result<T> result, Func<T, Result<K>> func)
        {
            return result ? func(result.Value) : Result.Fail<K>(result.Error);
        }


        public static Result<T> OnSuccess<T>(this Result<T> result, Action action)
        {
            if (result)
            {
                action();
            }

            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action action)
        {
            if (result.IsFail)
            {
                action();
            }

            return result;
        }

        public static Result<T> OnFailure<T>(this Result<T> result, Action<string> action)
        {
            if (result.IsFail)
            {
                action(result.Error);
            }

            return result;
        }

        public static Result OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, Result> func) where TError : class
        {
            return result ? func(result.Value) : Result.Fail<TNewValue, TError>(result.Error);
        }

        public static Result<T> OnSuccess<T>(this Result<T> result, Action<T> action)
        {
            if (result)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, TNewValue> func) where TError : class
        {
            return result
                ? Result.Ok<TNewValue, TError>(func(result.Value))
                : Result.Fail<TNewValue, TError>(result.Error);
        }

        public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, Result<TNewValue, TError>> func) where TError : class
        {
            return result.IsFail ? Result.Fail<TNewValue, TError>(result.Error) : func(result.Value);
        }

        public static Result<TNewValue, TError> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<Result<TNewValue, TError>> func) where TError : class
        {
            return result ? func() : Result.Fail<TNewValue, TError>(result.Error);
        }

        public static Result<TNewValue> OnSuccess<TValue, TNewValue, TError>(this Result<TValue, TError> result,
            Func<TValue, Result<TNewValue>> func) where TError : class
        {
            return result ? func(result.Value) : Result.Fail<TNewValue, TError>(result.Error);
        }

        public static Result<TValue, TError> OnSuccess<TValue, TError>(this Result<TValue, TError> result,
            Action<TValue> action) where TError : class
        {
            if (result)
            {
                action(result.Value);
            }

            return result;
        }

        public static Result<TValue, TError> OnFailure<TValue, TError>(this Result<TValue, TError> result, Action action) where TError : class
        {
            if (result.IsFail)
            {
                action();
            }

            return result;
        }

        public static Result<TValue, TError> OnFailure<TValue, TError>(this Result<TValue, TError> result, Action<TError> action) where TError : class
        {
            if (result.IsFail)
            {
                action(result.Error);
            }

            return result;
        }

    }
}
