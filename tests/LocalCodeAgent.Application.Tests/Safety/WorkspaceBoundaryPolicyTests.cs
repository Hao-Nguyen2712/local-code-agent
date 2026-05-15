using LocalCodeAgent.Application.Safety;
using LocalCodeAgent.Domain.Safety;

namespace LocalCodeAgent.Application.Tests.Safety;

public sealed class WorkspaceBoundaryPolicyTests
{
    [Fact]
    public void Validate_WhenPathIsRelative_ReturnsResolvedPathInsideWorkspace()
    {
        var workspaceRoot = CreateWorkspaceRoot();
        var policy = CreatePolicy(workspaceRoot);

        var result = policy.Validate(new WorkspacePathRequest("src/Program.cs"));

        Assert.True(result.IsValid);
        Assert.Equal(WorkspacePathValidationErrorCode.None, result.ErrorCode);
        Assert.Equal(Path.Combine(workspaceRoot, "src", "Program.cs"), result.ResolvedPath);
    }

    [Fact]
    public void Validate_WhenPathContainsCurrentDirectorySegment_NormalizesResolvedPath()
    {
        var workspaceRoot = CreateWorkspaceRoot();
        var policy = CreatePolicy(workspaceRoot);

        var result = policy.Validate(new WorkspacePathRequest("src/./Program.cs"));

        Assert.True(result.IsValid);
        Assert.Equal(Path.Combine(workspaceRoot, "src", "Program.cs"), result.ResolvedPath);
    }

    [Fact]
    public void Validate_WhenPathIsWindowsAbsolute_ReturnsAbsolutePathError()
    {
        var policy = CreatePolicy(CreateWorkspaceRoot());

        var result = policy.Validate(new WorkspacePathRequest(@"C:\Users\User\.ssh\id_rsa"));

        Assert.False(result.IsValid);
        Assert.Null(result.ResolvedPath);
        Assert.Equal(WorkspacePathValidationErrorCode.AbsolutePathNotAllowed, result.ErrorCode);
    }

    [Fact]
    public void Validate_WhenPathIsUncAbsolute_ReturnsAbsolutePathError()
    {
        var policy = CreatePolicy(CreateWorkspaceRoot());

        var result = policy.Validate(new WorkspacePathRequest(@"\\server\share\secret.txt"));

        Assert.False(result.IsValid);
        Assert.Null(result.ResolvedPath);
        Assert.Equal(WorkspacePathValidationErrorCode.AbsolutePathNotAllowed, result.ErrorCode);
    }

    [Fact]
    public void Validate_WhenPathIsUnixAbsolute_ReturnsAbsolutePathError()
    {
        var policy = CreatePolicy(CreateWorkspaceRoot());

        var result = policy.Validate(new WorkspacePathRequest("/etc/passwd"));

        Assert.False(result.IsValid);
        Assert.Null(result.ResolvedPath);
        Assert.Equal(WorkspacePathValidationErrorCode.AbsolutePathNotAllowed, result.ErrorCode);
    }

    [Theory]
    [InlineData("../secret.txt")]
    [InlineData("src/../../secret.txt")]
    [InlineData(@"src\..\secret.txt")]
    public void Validate_WhenPathContainsTraversal_ReturnsTraversalError(string requestedPath)
    {
        var policy = CreatePolicy(CreateWorkspaceRoot());

        var result = policy.Validate(new WorkspacePathRequest(requestedPath));

        Assert.False(result.IsValid);
        Assert.Null(result.ResolvedPath);
        Assert.Equal(WorkspacePathValidationErrorCode.PathTraversalNotAllowed, result.ErrorCode);
    }

    [Fact]
    public void Validate_WhenWorkspaceRootIsEmpty_ReturnsEmptyWorkspaceRootError()
    {
        var policy = CreatePolicy(string.Empty);

        var result = policy.Validate(new WorkspacePathRequest("src/Program.cs"));

        Assert.False(result.IsValid);
        Assert.Null(result.ResolvedPath);
        Assert.Equal(WorkspacePathValidationErrorCode.EmptyWorkspaceRoot, result.ErrorCode);
    }

    [Fact]
    public void Validate_WhenPathIsEmpty_ReturnsEmptyPathError()
    {
        var policy = CreatePolicy(CreateWorkspaceRoot());

        var result = policy.Validate(new WorkspacePathRequest(string.Empty));

        Assert.False(result.IsValid);
        Assert.Null(result.ResolvedPath);
        Assert.Equal(WorkspacePathValidationErrorCode.EmptyPath, result.ErrorCode);
    }

    private static WorkspaceBoundaryPolicy CreatePolicy(string workspaceRoot)
    {
        return new WorkspaceBoundaryPolicy(new WorkspaceBoundaryOptions(workspaceRoot));
    }

    private static string CreateWorkspaceRoot()
    {
        return Path.Combine(Path.GetTempPath(), "local-code-agent-workspace");
    }
}
