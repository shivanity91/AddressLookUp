namespace Api.Common.Validation
{
    public class ValidationErrorModel
    {
        public ValidationErrorModel(string code, string message, IEnumerable<Error> errors)
        {
            Code = code;
            Message = message;
            Errors = errors;
        }

        public ValidationErrorModel(string message, IEnumerable<Error> errors) : this(null, message, errors)
        {
        }

        public ValidationErrorModel(string code, string message, Error error) : this(code, message, new[] { error })
        {
        }

        public ValidationErrorModel(string code, string message, string errorFieldId, string errorMessage) : this(code, message, new[] { new Error(errorFieldId, errorMessage) })
        {
        }

        public ValidationErrorModel(string code, string message, string errorMessage) : this(code, message, new[] { new Error(errorMessage) })
        {
        }

        public ValidationErrorModel(string code, string message) : this(code, message, Enumerable.Empty<Error>())
        {
        }

        public string Code { get; }
        public string Message { get; }
        public IEnumerable<Error> Errors { get; }
    }

    public class Error
    {
        public Error(string message) : this(null, message)
        {

        }

        public Error(string field, string message)
        {
            Message = message;
            Field = field is not null ? field : null;
        }

        public string Message { get; }
        public string Field { get; }
    }
}
