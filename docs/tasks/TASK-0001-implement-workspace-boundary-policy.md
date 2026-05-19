# TASK-0001: Implement Workspace Boundary Policy

## Status

Done

## Related Spec

`specs/safety/workspace-boundary.spec.md`

## Goal

Implement a deterministic policy that validates whether a requested file path is inside the configured workspace root.

## Scope

Implement in `CodeAgent.Core`.

## Expected Types

- `WorkspaceBoundaryOptions`
- `WorkspacePathRequest`
- `WorkspacePathValidationResult`
- `IWorkspaceBoundaryPolicy`
- `WorkspaceBoundaryPolicy`

## Acceptance Criteria

- Reject absolute paths.
- Reject path traversal.
- Normalize paths before validation.
- Return machine-readable error codes.
- Include unit tests for all acceptance criteria in the spec.

## Evidence

- Implementation: `src/LocalCodeAgent.Application/Safety/WorkspaceBoundaryPolicy.cs`
- Tests: `tests/LocalCodeAgent.Application.Tests/Safety/WorkspaceBoundaryPolicyTests.cs`

## Out of Scope

- Real file reading.
- Real file writing.
- Shell command execution.
- Agent loop.
- LLM integration.