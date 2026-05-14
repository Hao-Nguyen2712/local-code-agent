namespace LocalCodeAgent.Domain.Safety;

public sealed record WorkspacePathValidationResult(
    bool IsValid,
    string? ResolvedPath,
    WorkspacePathValidationErrorCode ErrorCode)
{
    public static WorkspacePathValidationResult Valid(string resolvedPath)
    {
        return new WorkspacePathValidationResult(true, resolvedPath, WorkspacePathValidationErrorCode.None);
    }

    public static WorkspacePathValidationResult Invalid(WorkspacePathValidationErrorCode errorCode)
    {
        return new WorkspacePathValidationResult(false, null, errorCode);
    }
}
