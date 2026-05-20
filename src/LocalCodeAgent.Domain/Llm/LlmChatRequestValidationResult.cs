namespace LocalCodeAgent.Domain.Llm;

public sealed record LlmChatRequestValidationResult(
    bool IsValid,
    LlmChatRequest? Request,
    LlmErrorCode ErrorCode)
{
    public static LlmChatRequestValidationResult Valid(LlmChatRequest request)
    {
        return new LlmChatRequestValidationResult(true, request, LlmErrorCode.None);
    }

    public static LlmChatRequestValidationResult Invalid(LlmErrorCode errorCode)
    {
        return new LlmChatRequestValidationResult(false, null, errorCode);
    }
}
