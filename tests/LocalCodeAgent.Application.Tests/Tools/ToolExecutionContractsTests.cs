using LocalCodeAgent.Domain.Tools;

namespace LocalCodeAgent.Application.Tests.Tools;

public sealed class ToolExecutionContractsTests
{
    [Fact]
    public void Create_WhenToolNameAndCorrelationIdAreProvided_ReturnsValidRequest()
    {
        var result = ToolExecutionRequest.Create("file.read", "task-001");

        Assert.True(result.IsValid);
        Assert.NotNull(result.Request);
        Assert.Equal("file.read", result.Request.ToolName);
        Assert.Equal("task-001", result.Request.CorrelationId);
        Assert.Equal(ToolExecutionErrorCode.None, result.ErrorCode);
    }

    [Fact]
    public void Create_WhenToolNameIsMissing_ReturnsMissingToolNameError()
    {
        var result = ToolExecutionRequest.Create(string.Empty, "task-001");

        Assert.False(result.IsValid);
        Assert.Null(result.Request);
        Assert.Equal(ToolExecutionErrorCode.MissingToolName, result.ErrorCode);
    }

    [Fact]
    public void Create_WhenCorrelationIdIsMissing_ReturnsMissingCorrelationIdError()
    {
        var result = ToolExecutionRequest.Create("file.read", string.Empty);

        Assert.False(result.IsValid);
        Assert.Null(result.Request);
        Assert.Equal(ToolExecutionErrorCode.MissingCorrelationId, result.ErrorCode);
    }

    [Fact]
    public void Succeeded_ReturnsSucceededStatusWithoutError()
    {
        var result = ToolExecutionResult.Succeeded();

        Assert.Equal(ToolExecutionStatus.Succeeded, result.Status);
        Assert.Equal(ToolExecutionErrorCode.None, result.ErrorCode);
    }

    [Fact]
    public void Failed_ReturnsFailedStatusWithMachineReadableErrorCode()
    {
        var result = ToolExecutionResult.Failed(ToolExecutionErrorCode.PolicyRejected);

        Assert.Equal(ToolExecutionStatus.Failed, result.Status);
        Assert.Equal(ToolExecutionErrorCode.PolicyRejected, result.ErrorCode);
    }
}
