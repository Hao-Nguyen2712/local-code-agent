namespace LocalCodeAgent.Domain.Safety;

public enum WorkspacePathValidationErrorCode
{
    None = 0,
    EmptyWorkspaceRoot = 1,
    EmptyPath = 2,
    AbsolutePathNotAllowed = 3,
    PathTraversalNotAllowed = 4,
    OutsideWorkspaceRoot = 5,
    InvalidPath = 6
}
