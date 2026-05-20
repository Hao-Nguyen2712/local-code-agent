namespace LocalCodeAgent.Domain.Llm;

public sealed record LlmMessage(
    LlmMessageRole Role,
    string Content,
    string? Name = null);
