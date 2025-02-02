using PetHelp.Domain.Shared;

namespace PetHelp.API.Response;

public record Envelope
{
    private Envelope(object? result, Error? error)
    {
        Result = result;
        ErrorCode = error?.Code;
        ErrorCodeMessage = error?.Message;
        TimeGenerated = DateTime.UtcNow;
    }

    public object? Result { get; }

    public string? ErrorCode { get; }

    public string? ErrorCodeMessage { get; }

    public DateTime TimeGenerated { get; }

    public static Envelope Ok(object? result = null) =>
        new(result, null);

    public static Envelope Error(Error error) =>
        new(null, error);
}