namespace LocalCodeAgent.Domain.Safety;
/// <summary>
/// Represents a request to validate a workspace path.
/// </summary>
/// <param name="Path">The path to validate.</param>
/// <returns>The workspace path request.</returns>
public sealed record WorkspacePathRequest(string Path);
