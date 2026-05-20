namespace LocalCodeAgent.Domain.Llm;

public sealed record LlmClientOptions(string Model, TimeSpan Timeout);
