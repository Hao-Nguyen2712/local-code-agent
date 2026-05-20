namespace LocalCodeAgent.Domain.Safety;

public enum WorkspacePathValidationErrorCode
{
    /// <summary>
    /// No error. The workspace path is valid.
    /// </summary>
    None = 0,
    /// <summary>
    /// The workspace root is empty. A workspace root must be specified for validating workspace paths.
    /// </summary>
    EmptyWorkspaceRoot = 1,
    /// <summary>
    /// The path is empty.
    /// </summary>
    EmptyPath = 2,
    /// <summary>
    /// Absolute paths are not allowed.
    /// </summary>
    AbsolutePathNotAllowed = 3,
    /// <summary>
    /// Path traversal is not allowed.
    /// </summary>
    PathTraversalNotAllowed = 4,
    /// <summary>
    /// The path is outside the workspace root.
    /// </summary>
    OutsideWorkspaceRoot = 5,
    /// <summary>
    /// The path is invalid.
    /// </summary>
    InvalidPath = 6
}
