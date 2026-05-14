# LLM Chat Completion Specification

## Status

Draft

## Goal

Define how the application sends chat messages to an LLM provider and receives responses.

The Core project must depend only on an internal abstraction, not on a specific provider.

## Scope

Applies to:

- Ollama adapter
- OpenAI-compatible adapter
- Mock LLM client used in tests

## Functional Requirements

### LLM-001: Provider-Agnostic Core

Core must define an `ILlmClient` abstraction.

Infrastructure must provide concrete implementations.

### LLM-002: Support Chat Messages

The request must support multiple messages with roles.

Supported roles:

- system
- user
- assistant
- tool

### LLM-003: Support Cancellation

Every LLM request must accept a cancellation token.

### LLM-004: Support Timeout Configuration

Timeout must be configurable.

Timeout values must not be hardcoded.

### LLM-005: Support Model Configuration

Model name must be configurable.

Model names must not be hardcoded in Core.

### LLM-006: Return Structured Error

LLM failures must return structured error information.

## Acceptance Criteria

### AC-001

Given a configured model and base URL
When a chat request is sent
Then the infrastructure adapter sends the request to the configured provider.

### AC-002

Given an LLM timeout
When the provider does not respond in time
Then the request fails with a timeout error.

### AC-003

Given Core project
Then it must not reference Ollama-specific classes.

### AC-004

Given unit tests
Then they must be able to use a fake `ILlmClient`.

## Design Notes

Use Adapter Pattern for concrete LLM providers.

Use Dependency Inversion Principle to keep Core independent from provider-specific infrastructure.