# Workspace Boundary Specification

## Status

Implemented

## Goal

Ensure the agent can only access files inside an approved workspace directory.

This policy applies to all tools that read, write, search, patch, delete, or inspect files.

## Scope

Applies to:

- File read tool
- File write tool
- Project search tool
- Git diff tool
- Shell command tool when working directory is involved

Out of scope:

- Real file reading
- Real file writing
- Shell command execution
- Agent loop behavior
- LLM integration

## Definitions

### Workspace Root

The approved root directory where the agent is allowed to operate.

### Relative Path

A path provided relative to the workspace root.

### Path Traversal

Any path that attempts to escape the workspace root using patterns such as `../` or `..\`.

## Functional Requirements

### WR-001: Reject Absolute Paths

The policy must reject absolute paths unless explicitly enabled by configuration.

Example rejected paths:

```text
C:\Users\User\.ssh\id_rsa
\\server\share\secret.txt
/etc/passwd
```

### WR-002: Reject Path Traversal

The policy must reject paths containing `..` as a path segment.

Example rejected paths:

```text
../secret.txt
src/../../secret.txt
src\..\secret.txt
```

### WR-003: Reject Empty Workspace Root

The policy must reject validation when the configured workspace root is empty.

### WR-004: Reject Empty Requested Path

The policy must reject validation when the requested path is empty.

### WR-005: Normalize Resolved Paths

The policy must normalize valid relative paths before returning the resolved path.

Example:

```text
src/./Program.cs
```

must resolve as:

```text
src/Program.cs
```

inside the configured workspace root.

### WR-006: Ensure Resolved Path Stays Inside Workspace Root

After combining the workspace root and requested relative path, the policy must verify that the resolved full path remains inside the workspace root.

### WR-007: Return Machine-Readable Error Codes

Invalid validation results must return a machine-readable `WorkspacePathValidationErrorCode`.

Expected error codes:

- `None`
- `EmptyWorkspaceRoot`
- `EmptyPath`
- `AbsolutePathNotAllowed`
- `PathTraversalNotAllowed`
- `OutsideWorkspaceRoot`
- `InvalidPath`

## Acceptance Criteria

- A relative path inside the workspace returns a valid result with a resolved path.
- A path containing a current-directory segment is normalized.
- Windows absolute paths are rejected.
- UNC paths are rejected.
- Unix absolute paths are rejected.
- Paths containing traversal segments are rejected.
- Empty workspace root is rejected.
- Empty requested path is rejected.
- Invalid paths return machine-readable error codes instead of throwing expected validation exceptions.
