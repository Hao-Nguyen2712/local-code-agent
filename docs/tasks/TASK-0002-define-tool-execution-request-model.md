# TASK-0002: Define Tool Execution Request Model

## Status

Done

## Related Spec

`specs/tools/tool-execution-request.spec.md`

## Goal

Define deterministic request and result contracts for future tool execution.

## Scope

Domain/Application contracts only.

## Expected Types

- `ToolExecutionRequest`
- `ToolExecutionResult`
- `ToolExecutionStatus`
- `ToolExecutionErrorCode`
- `IToolExecutionPolicy`

## Acceptance Criteria

- `ToolExecutionRequest` requires `ToolName`.
- `ToolExecutionRequest` requires `CorrelationId`.
- `ToolExecutionResult` contains `Status`.
- Failed result contains machine-readable `ErrorCode`.
- Unit tests cover valid request, missing tool name, missing correlation id, success result, failed result.
- All tests pass.

## Evidence

- Request contract: `src/LocalCodeAgent.Domain/Tools/ToolExecutionRequest.cs`
- Result contract: `src/LocalCodeAgent.Domain/Tools/ToolExecutionResult.cs`
- Policy contract: `src/LocalCodeAgent.Application/Tools/IToolExecutionPolicy.cs`
- Tests: `tests/LocalCodeAgent.Application.Tests/Tools/ToolExecutionContractsTests.cs`

## Out of Scope

- Real tool execution.
- File IO.
- Shell execution.
- LLM integration.
- Agent loop.
