namespace AirCheap.Client.DataTransferObjects;

public class EmptyResponseDto
{
    public bool Success { get; set; }

    public IEnumerable<string> Errors { get; set; }
}
