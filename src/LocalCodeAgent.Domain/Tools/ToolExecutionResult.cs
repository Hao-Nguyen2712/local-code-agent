namespace LocalCodeAgent.Domain.Tools;

public sealed record ToolExecutionResult(
    ToolExecutionStatus Status,
    ToolExecutionErrorCode ErrorCode)
{
    public static ToolExecutionResult Succeeded()
    {
        return new ToolExecutionResult(ToolExecutionStatus.Succeeded, ToolExecutionErrorCode.None);
    }

    public static ToolExecutionResult Failed(ToolExecutionErrorCode errorCode)
    {
        return new ToolExecutionResult(ToolExecutionStatus.Failed, errorCode);
    }
}
