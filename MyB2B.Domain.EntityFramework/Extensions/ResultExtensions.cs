using System;
using System.Collections.Generic;
using System.Text;
using MyB2B.Domain.Results;

namespace MyB2B.Domain.EntityFramework.Extensions
{
    public static class ResultExtensions
    {
        public static Result<TValue> SaveContext<TValue>(this Result<TValue> result, MyB2BContext context)
        {
            if (result)
            {
                context.SaveChanges();
            }

            return result;
        }
    }
}
