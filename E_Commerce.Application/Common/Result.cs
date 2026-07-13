using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Application.Common
{
    public class Result
    {
      public  bool IsSuccess {  get; } 
        public IReadOnlyList<Error> Errors { get; }

        public Result(bool IsSuccess, IReadOnlyList<Error> Errors)
        {
            this.IsSuccess = IsSuccess;
            this.Errors = Errors;
        }
        public static Result Ok()
        {
            return new Result(true, Array.Empty<Error>());
        }
        public static Result Fail(Error Errors)
        {
            return new Result(false, new[] { Errors });
        }
        public static Result Fail(IReadOnlyList<Error> Errors)
        {
            return new Result(false, Errors);
        }

        

            
    }
    public class Result<TValue> : Result
    {
        private readonly TValue _value;

        public TValue data => IsSuccess ? _value : throw new InvalidOperationException("Can not Access The Value Of Failed Result");
        private Result(TValue value) : base(true, Array.Empty<Error>())
        {
            _value = value;
        }

        private Result(Error erroe) : base(false, new[] { erroe })
        {

        }
        private Result(IReadOnlyList<Error> errors) : base(false, errors)
        {

        }
        public static Result<TValue> Ok(TValue value)
        {
            return new Result<TValue>(value);
        }
        public static Result<TValue> Fail(Error Errors)
        {
            return new Result<TValue>(Errors);
        }
        public static Result<TValue> Fail(IReadOnlyList<Error> errors)
        {
            return new Result<TValue>(errors);
        }
    }
}

