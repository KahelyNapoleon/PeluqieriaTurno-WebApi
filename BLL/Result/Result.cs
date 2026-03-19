using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Result
{
    public class Result<T> 
    {
        public bool IsValid { get; set; } = true;
        public List<string>? Errors { get; set; } = new List<string>();
        public T? Data { get; set; } 

        public Result() { }

        public static Result<T> Fail(params string[] errors)
        {
            return new Result<T> { IsValid = false, Errors = errors.ToList() };
        }

        public static Result<T> Succes(T data)
        {
            return new Result<T> { IsValid = true, Data = data};
        }
        
    }
}
