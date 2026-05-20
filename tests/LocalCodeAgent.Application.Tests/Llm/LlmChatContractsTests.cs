using LocalCodeAgent.Application.Llm;
using LocalCodeAgent.Domain.Llm;

namespace LocalCodeAgent.Application.Tests.Llm;

public sealed class LlmChatContractsTests
{
    [Fact]
    public void Create_WhenModelAndMessagesAreProvided_ReturnsValidRequest()
    {
        var messages = new[] { new LlmMessage(LlmMessageRole.User, "Hello") };

        var result = LlmChatRequest.Create("llama3", messages);

        Assert.True(result.IsValid);
        Assert.NotNull(result.Request);
        Assert.Equal("llama3", result.Request.Model);
        Assert.Equal(messages, result.Request.Messages);
        Assert.Equal(LlmErrorCode.None, result.ErrorCode);
    }

    [Fact]
    public void Create_WhenModelIsMissing_ReturnsMissingModelError()
    {
        var result = LlmChatRequest.Create(string.Empty, [new LlmMessage(LlmMessageRole.User, "Hello")]);

        Assert.False(result.IsValid);
        Assert.Null(result.Request);
        Assert.Equal(LlmErrorCode.MissingModel, result.ErrorCode);
    }

    [Fact]
    public void Create_WhenMessagesAreEmpty_ReturnsMissingMessagesError()
    {
        var result = LlmChatRequest.Create("llama3", []);

        Assert.False(result.IsValid);
        Assert.Null(result.Request);
        Assert.Equal(LlmErrorCode.MissingMessages, result.ErrorCode);
    }

    [Fact]
    public void Create_WhenMessageContentIsMissing_ReturnsInvalidMessageError()
    {
        var result = LlmChatRequest.Create("llama3", [new LlmMessage(LlmMessageRole.User, string.Empty)]);

        Assert.False(result.IsValid);
        Assert.Null(result.Request);
        Assert.Equal(LlmErrorCode.InvalidMessage, result.ErrorCode);
    }

    [Theory]
    [InlineData(-0.1)]
    [InlineData(2.1)]
    public void Create_WhenTemperatureIsOutsideSupportedRange_ReturnsInvalidTemperatureError(double temperature)
    {
        var result = LlmChatRequest.Create("llama3", [new LlmMessage(LlmMessageRole.User, "Hello")], temperature);

        Assert.False(result.IsValid);
        Assert.Null(result.Request);
        Assert.Equal(LlmErrorCode.InvalidTemperature, result.ErrorCode);
    }

    [Fact]
    public void MessageRole_ContainsSupportedRoles()
    {
        Assert.Equal(0, (int)LlmMessageRole.System);
        Assert.Equal(1, (int)LlmMessageRole.User);
        Assert.Equal(2, (int)LlmMessageRole.Assistant);
        Assert.Equal(3, (int)LlmMessageRole.Tool);
    }

    [Fact]
    public void Succeeded_ReturnsResponseWithoutError()
    {
        var response = new LlmChatResponse(new LlmMessage(LlmMessageRole.Assistant, "Hi"));

        var result = LlmChatResult.Succeeded(response);

        Assert.True(result.IsSuccess);
        Assert.Equal(response, result.Response);
        Assert.Equal(LlmErrorCode.None, result.ErrorCode);
    }

    [Fact]
    public void Failed_ReturnsStructuredErrorCode()
    {
        var result = LlmChatResult.Failed(LlmErrorCode.ProviderFailure);

        Assert.False(result.IsSuccess);
        Assert.Null(result.Response);
        Assert.Equal(LlmErrorCode.ProviderFailure, result.ErrorCode);
    }

    [Fact]
    public async Task CompleteChatAsync_CanUseFakeClientWithoutRealProvider()
    {
        var request = LlmChatRequest.Create("llama3", [new LlmMessage(LlmMessageRole.User, "Hello")]).Request!;
        using var cancellationTokenSource = new CancellationTokenSource();
        ILlmClient client = new FakeLlmClient();

        var result = await client.CompleteChatAsync(request, cancellationTokenSource.Token);

        Assert.True(result.IsSuccess);
        Assert.Equal("Fake response", result.Response!.Message.Content);
    }

    [Fact]
    public void LlmClientOptions_RepresentsModelAndTimeoutConfiguration()
    {
        var options = new LlmClientOptions("llama3", TimeSpan.FromSeconds(30));

        Assert.Equal("llama3", options.Model);
        Assert.Equal(TimeSpan.FromSeconds(30), options.Timeout);
    }

    private sealed class FakeLlmClient : ILlmClient
    {
        public Task<LlmChatResult> CompleteChatAsync(
            LlmChatRequest request,
            CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            return Task.FromResult(LlmChatResult.Succeeded(
                new LlmChatResponse(new LlmMessage(LlmMessageRole.Assistant, "Fake response"))));
        }
    }
}
