# LLM Chat Completion Specification

## Status

Implemented

## Goal

Define provider-agnostic chat completion contracts so application code can request LLM responses without depending on a concrete provider.

The Domain and Application projects must depend only on internal abstractions and contracts, not provider-specific SDKs, HTTP clients, or runtime adapters.

## Scope

Applies to:

- Chat request contracts
- Chat response contracts
- Chat message roles
- Structured LLM result and error contracts
- `ILlmClient` abstraction
- Client options needed by the abstraction
- Fake LLM clients used in unit tests

Out of scope:

- Ollama adapter implementation
- OpenAI-compatible adapter implementation
- Real provider calls
- Agent loop behavior
- Tool execution
- File IO
- Shell execution
- Streaming behavior

## Definitions

### LLM Client

An application-facing abstraction that accepts chat requests and returns structured chat results.

### Chat Message

A single message in a chat request or response. Each message has a role and non-empty content.

### Model

The configured model name used for a chat request. Model names must come from request or options data, not hardcoded constants in core code.

### Timeout

The maximum duration allowed for an LLM request. Timeout values must be represented as configuration data, not hardcoded constants.

## Functional Requirements

### LLM-001: Provider-Agnostic Abstraction

Application code must depend on an `ILlmClient` abstraction. Domain and Application code must not reference provider-specific classes.

### LLM-002: Support Chat Messages

The request must support multiple messages.

Supported roles:

- `system`
- `user`
- `assistant`
- `tool`

### LLM-003: Require Model Name

A chat request must require a non-empty model name.

### LLM-004: Require Messages

A chat request must require at least one message.

### LLM-005: Require Message Content

A chat message must require non-empty content.

### LLM-006: Support Cancellation

Every LLM request must accept a cancellation token through `ILlmClient`.

### LLM-007: Support Timeout Configuration

Timeout must be represented by client options. Timeout values must not be hardcoded.

### LLM-008: Return Structured Error

LLM request validation and failed calls must return machine-readable error codes.

### LLM-009: Keep Streaming Out Of Scope

The existing chat request schema may include a `stream` field for future compatibility, but this task does not implement streaming behavior.

### LLM-010: Support Optional Temperature

The request contract may include optional temperature. If provided, temperature must be between `0` and `2`.

## Expected Types

- `ILlmClient`
- `LlmChatRequest`
- `LlmChatRequestValidationResult`
- `LlmChatResponse`
- `LlmMessage`
- `LlmMessageRole`
- `LlmErrorCode`
- `LlmChatResult`
- `LlmClientOptions`

## Acceptance Criteria

- A valid request with a non-empty model and at least one message succeeds.
- Missing model name is rejected.
- Empty message list is rejected.
- Missing message content is rejected.
- Temperature below `0` or above `2` is rejected.
- Message roles include `system`, `user`, `assistant`, and `tool`.
- Success result contains a response and no error.
- Failed result contains a machine-readable error code.
- `ILlmClient` accepts a cancellation token.
- Unit tests can use a fake `ILlmClient` without a real LLM provider.
- Domain and Application code do not require provider-specific dependencies.
- All tests pass.

## Future Work

Provider adapters should be implemented by later tasks under Infrastructure, such as:

- Ollama chat completion adapter
- OpenAI-compatible chat completion adapter
