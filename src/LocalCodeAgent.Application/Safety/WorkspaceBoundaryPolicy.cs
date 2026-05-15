using LocalCodeAgent.Domain.Safety;

namespace LocalCodeAgent.Application.Safety;

public sealed class WorkspaceBoundaryPolicy : IWorkspaceBoundaryPolicy
{
    private static readonly char[] PathSeparators = ['/', '\\'];

    private readonly string _workspaceRoot;

    public WorkspaceBoundaryPolicy(WorkspaceBoundaryOptions options)
    {
        _workspaceRoot = options.WorkspaceRoot;
    }

    public WorkspacePathValidationResult Validate(WorkspacePathRequest request)
    {
        if (string.IsNullOrWhiteSpace(_workspaceRoot))
        {
            return WorkspacePathValidationResult.Invalid(WorkspacePathValidationErrorCode.EmptyWorkspaceRoot);
        }

        if (string.IsNullOrWhiteSpace(request.Path))
        {
            return WorkspacePathValidationResult.Invalid(WorkspacePathValidationErrorCode.EmptyPath);
        }

        if (IsAbsolutePath(request.Path))
        {
            return WorkspacePathValidationResult.Invalid(WorkspacePathValidationErrorCode.AbsolutePathNotAllowed);
        }

        if (ContainsPathTraversal(request.Path))
        {
            return WorkspacePathValidationResult.Invalid(WorkspacePathValidationErrorCode.PathTraversalNotAllowed);
        }

        try
        {
            var workspaceRoot = NormalizeDirectory(_workspaceRoot);
            var resolvedPath = Path.GetFullPath(Path.Combine(workspaceRoot, request.Path));

            if (!IsInsideWorkspace(workspaceRoot, resolvedPath))
            {
                return WorkspacePathValidationResult.Invalid(WorkspacePathValidationErrorCode.OutsideWorkspaceRoot);
            }

            return WorkspacePathValidationResult.Valid(resolvedPath);
        }
        catch (ArgumentException)
        {
            return WorkspacePathValidationResult.Invalid(WorkspacePathValidationErrorCode.InvalidPath);
        }
        catch (NotSupportedException)
        {
            return WorkspacePathValidationResult.Invalid(WorkspacePathValidationErrorCode.InvalidPath);
        }
        catch (PathTooLongException)
        {
            return WorkspacePathValidationResult.Invalid(WorkspacePathValidationErrorCode.InvalidPath);
        }
    }

    private static bool ContainsPathTraversal(string path)
    {
        return path.Split(PathSeparators, StringSplitOptions.RemoveEmptyEntries)
            .Any(segment => segment == "..");
    }

    private static bool IsAbsolutePath(string path)
    {
        if (Path.IsPathRooted(path))
        {
            return true;
        }

        return IsWindowsDriveRootedPath(path) || IsUncPath(path);
    }

    private static bool IsWindowsDriveRootedPath(string path)
    {
        return path.Length >= 3
            && char.IsAsciiLetter(path[0])
            && path[1] == ':'
            && PathSeparators.Contains(path[2]);
    }

    private static bool IsUncPath(string path)
    {
        return path.StartsWith(@"\\", StringComparison.Ordinal)
            || path.StartsWith("//", StringComparison.Ordinal);
    }

    private static string NormalizeDirectory(string path)
    {
        return Path.TrimEndingDirectorySeparator(Path.GetFullPath(path));
    }

    private static bool IsInsideWorkspace(string workspaceRoot, string resolvedPath)
    {
        if (string.Equals(workspaceRoot, resolvedPath, GetPathComparison()))
        {
            return true;
        }

        return resolvedPath.StartsWith(
            workspaceRoot + Path.DirectorySeparatorChar,
            GetPathComparison());
    }

    private static StringComparison GetPathComparison()
    {
        return OperatingSystem.IsWindows()
            ? StringComparison.OrdinalIgnoreCase
            : StringComparison.Ordinal;
    }
}
