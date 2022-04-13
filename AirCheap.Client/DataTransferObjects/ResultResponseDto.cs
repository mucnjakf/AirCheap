namespace AirCheap.Client.DataTransferObjects;

public class ResultResponseDto<T> where T : class
{
    public bool Success { get; set; }

    public IEnumerable<string> Errors { get; set; }

    public IEnumerable<T> CollectionResult { get; set; }

    public T ObjectResult { get; set; }
}
