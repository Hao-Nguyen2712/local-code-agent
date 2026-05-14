# ADR-0001: Use Spec-Driven Workflow

## Status

Accepted

## Context

This project builds a local coding agent that can interact with files, tools, shell commands, LLM providers, and project memory.

Agent systems are prone to unclear behavior if implementation starts before expected behavior is specified.

## Decision

The project will use Spec-Driven Design.

Every feature must start from a specification under `specs/`.

The implementation order is:

1. Spec
2. Acceptance criteria
3. Schema or contract
4. Production code
5. Tests
6. Evaluation
7. Documentation update

## Consequences

### Positive

- Reduces vague implementation.
- Makes agent behavior reviewable.
- Makes testing easier.
- Helps prevent unsafe tool execution.
- Supports future evaluation and regression testing.

### Negative

- Slower initial development.
- More documentation overhead.
- Requires discipline to keep specs updated.

## Rules

- No new runtime feature should be implemented without a related spec.
- No architecture change should be made without an ADR.
- If implementation and spec conflict, the spec must be updated or implementation must be corrected.