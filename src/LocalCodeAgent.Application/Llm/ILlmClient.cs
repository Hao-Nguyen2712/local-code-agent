using LocalCodeAgent.Domain.Llm;

namespace LocalCodeAgent.Application.Llm;

public interface ILlmClient
{
    Task<LlmChatResult> CompleteChatAsync(
        LlmChatRequest request,
        CancellationToken cancellationToken);
}
