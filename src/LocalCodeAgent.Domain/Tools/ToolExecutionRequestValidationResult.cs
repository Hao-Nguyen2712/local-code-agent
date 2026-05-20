namespace LocalCodeAgent.Domain.Tools;

public sealed record ToolExecutionRequestValidationResult(
    bool IsValid,
    ToolExecutionRequest? Request,
    ToolExecutionErrorCode ErrorCode)
{
    public static ToolExecutionRequestValidationResult Valid(ToolExecutionRequest request)
    {
        return new ToolExecutionRequestValidationResult(true, request, ToolExecutionErrorCode.None);
    }

    public static ToolExecutionRequestValidationResult Invalid(ToolExecutionErrorCode errorCode)
    {
        return new ToolExecutionRequestValidationResult(false, null, errorCode);
    }
}
