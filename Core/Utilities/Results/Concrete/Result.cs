using Core.Utilities.Results.Abstract;

namespace Core.Utilities.Results.Concrete
{
    public class Result:IResult
    {
        public Result(bool success, string message):this(success)
        {
            Message = message;
        }
        public Result(bool success)
        {
            Success = success;
        }

        //get = readonly ^ readonly ler constructor set edilebilir
        public bool Success { get; }
        public string Message { get; }
    }
}
