# TASK-0003: Implement LLM Chat Completion Abstraction

## Status

Done

## Related Spec

`specs/llm/chat-completion.spec.md`

## Goal

Define provider-agnostic chat completion contracts so application code can request LLM responses without depending on a concrete provider.

## Scope

Application and domain contracts for chat completion.

## Expected Types

- `ILlmClient`
- `LlmChatRequest`
- `LlmChatResponse`
- `LlmMessage`
- `LlmMessageRole`
- `LlmErrorCode`
- `LlmChatResult`
- `LlmClientOptions`

## Acceptance Criteria

- Core/application code depends on `ILlmClient`, not provider-specific classes.
- Chat requests support multiple messages.
- Message roles include `system`, `user`, `assistant`, and `tool`.
- Requests require a non-empty model name.
- Requests require at least one message.
- LLM calls accept a cancellation token.
- Timeout and model configuration are represented without hardcoded values.
- Failed calls return structured error information.
- Unit tests can use a fake `ILlmClient` without a real LLM provider.
- All tests pass.

## Dependencies

- `TASK-0001: Implement Workspace Boundary Policy` is done.
- `TASK-0002: Define Tool Execution Request Model` is done.

## Evidence

- Domain contracts: `src/LocalCodeAgent.Domain/Llm/`
- Application abstraction: `src/LocalCodeAgent.Application/Llm/ILlmClient.cs`
- Tests: `tests/LocalCodeAgent.Application.Tests/Llm/LlmChatContractsTests.cs`

## Out of Scope

- Agent loop.
- Tool execution.
- File IO.
- Shell execution.
- Real provider calls from unit tests.
- Unrestricted network access.
- Streaming implementation unless explicitly added to the spec.
