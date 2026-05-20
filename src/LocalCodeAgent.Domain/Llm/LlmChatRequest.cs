namespace LocalCodeAgent.Domain.Llm;

public sealed record LlmChatRequest(
    string Model,
    IReadOnlyList<LlmMessage> Messages,
    double? Temperature = null)
{
    public static LlmChatRequestValidationResult Create(
        string model,
        IReadOnlyList<LlmMessage> messages,
        double? temperature = null)
    {
        if (string.IsNullOrWhiteSpace(model))
        {
            return LlmChatRequestValidationResult.Invalid(LlmErrorCode.MissingModel);
        }

        if (messages.Count == 0)
        {
            return LlmChatRequestValidationResult.Invalid(LlmErrorCode.MissingMessages);
        }

        if (messages.Any(message => string.IsNullOrWhiteSpace(message.Content)))
        {
            return LlmChatRequestValidationResult.Invalid(LlmErrorCode.InvalidMessage);
        }

        if (temperature is < 0 or > 2)
        {
            return LlmChatRequestValidationResult.Invalid(LlmErrorCode.InvalidTemperature);
        }

        return LlmChatRequestValidationResult.Valid(new LlmChatRequest(model, messages, temperature));
    }
}
