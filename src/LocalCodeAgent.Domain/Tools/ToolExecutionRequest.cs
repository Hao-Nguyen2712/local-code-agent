namespace LocalCodeAgent.Domain.Tools;

public sealed record ToolExecutionRequest(string ToolName, string CorrelationId)
{
    public static ToolExecutionRequestValidationResult Create(string toolName, string correlationId)
    {
        if (string.IsNullOrWhiteSpace(toolName))
        {
            return ToolExecutionRequestValidationResult.Invalid(ToolExecutionErrorCode.MissingToolName);
        }

        if (string.IsNullOrWhiteSpace(correlationId))
        {
            return ToolExecutionRequestValidationResult.Invalid(ToolExecutionErrorCode.MissingCorrelationId);
        }

        return ToolExecutionRequestValidationResult.Valid(new ToolExecutionRequest(toolName, correlationId));
    }
}
