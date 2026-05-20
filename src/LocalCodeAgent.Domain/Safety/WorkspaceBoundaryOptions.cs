namespace LocalCodeAgent.Domain.Safety;

/// <summary>
/// Workspace boundary options for validating workspace paths. This includes the workspace root path that defines the boundary for valid workspace paths.
/// </summary>
/// <param name="WorkspaceRoot">The root path of the workspace.</param>
/// <returns>The workspace boundary options.</returns>
public sealed record WorkspaceBoundaryOptions(string WorkspaceRoot);
