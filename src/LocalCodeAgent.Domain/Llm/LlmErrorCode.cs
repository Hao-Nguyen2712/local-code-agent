namespace LocalCodeAgent.Domain.Llm;

public enum LlmErrorCode
{
    None = 0,
    MissingModel = 1,
    MissingMessages = 2,
    InvalidMessage = 3,
    InvalidTemperature = 4,
    Timeout = 5,
    ProviderFailure = 6,
    Cancelled = 7,
    InvalidRequest = 8
}
