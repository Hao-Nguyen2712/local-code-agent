namespace LocalCodeAgent.Domain.Tools;

public enum ToolExecutionErrorCode
{
    None = 0,
    MissingToolName = 1,
    MissingCorrelationId = 2,
    PolicyRejected = 3,
    UnknownTool = 4,
    InvalidRequest = 5
}
