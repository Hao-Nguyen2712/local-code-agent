namespace LocalCodeAgent.Domain.Safety;

public sealed record WorkspacePathValidationResult(
    bool IsValid,
    string? ResolvedPath,
    WorkspacePathValidationErrorCode ErrorCode)
{
    /// <summary>
    /// Creates a valid workspace path validation result.
    /// </summary>
    /// <param name="resolvedPath">The resolved path.</param>
    /// <returns>The validation result.</returns>
    public static WorkspacePathValidationResult Valid(string resolvedPath)
    {
        return new WorkspacePathValidationResult(true, resolvedPath, WorkspacePathValidationErrorCode.None);
    }

    /// <summary>
    /// Creates an invalid workspace path validation result.
    /// </summary>
    /// <param name="errorCode">The error code.</param>
    /// <returns>The validation result.</returns>
    public static WorkspacePathValidationResult Invalid(WorkspacePathValidationErrorCode errorCode)
    {
        return new WorkspacePathValidationResult(false, null, errorCode);
    }
}
