namespace LocalCodeAgent.Domain.Llm;

public sealed record LlmChatResult(
    bool IsSuccess,
    LlmChatResponse? Response,
    LlmErrorCode ErrorCode)
{
    public static LlmChatResult Succeeded(LlmChatResponse response)
    {
        return new LlmChatResult(true, response, LlmErrorCode.None);
    }

    public static LlmChatResult Failed(LlmErrorCode errorCode)
    {
        return new LlmChatResult(false, null, errorCode);
    }
}
