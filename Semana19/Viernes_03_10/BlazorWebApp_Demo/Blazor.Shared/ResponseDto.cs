namespace Blazor.Shared
{
    public class ResponseDto<T>
    {
        public bool IsCorrect { get; set; }
        public string? Message { get; set; }
        public T? Value { get; set; }
    }
}
