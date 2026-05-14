# AGENTS.md

## Project Overview

This project is a local coding agent built for learning how LLM-powered agents work.

The agent runs locally, communicates with a local LLM provider, and can gradually use controlled tools such as file reading, file writing, shell commands, project search, and git diff inspection.

The project follows Spec-Driven Design. No production code should be implemented before the relevant specification exists.

## Primary Goal

Build a safe, observable, spec-driven local coding agent.

The project is optimized for learning, correctness, maintainability, and controlled tool usage rather than maximum automation.

## Non-Goals

- Do not build an unrestricted autonomous agent.
- Do not allow unrestricted shell execution.
- Do not modify files outside the approved workspace.
- Do not implement multi-agent orchestration before the single-agent loop is stable.
- Do not introduce vector databases before basic project memory and evaluation exist.

## Required Workflow

For every new feature, follow this order:

1. Write or update the relevant spec under `specs/`.
2. Define acceptance criteria.
3. Define schemas or contracts if structured input/output is needed.
4. Implement the smallest production code required.
5. Add unit tests.
6. Add integration tests if the feature touches file system, shell, LLM, or external process.
7. Add evaluation cases if the feature affects agent behavior.
8. Update documentation if behavior or architecture changes.

## Architecture Rules

The project must separate:

- Core domain logic
- Infrastructure adapters
- Runtime entrypoints
- Contracts and DTOs
- Specifications
- Evaluation cases

The `Core` project must not depend on Ollama, OpenAI, shell, file system implementation, or any concrete infrastructure.

Infrastructure must implement interfaces defined by Core.

## Safety Rules

The agent must:

- Reject path traversal.
- Reject absolute file paths unless explicitly allowed by policy.
- Never read or write outside the configured workspace.
- Never expose secrets in logs or responses.
- Require explicit approval before destructive file operations.
- Prefer diff preview before file writes.
- Use command allowlists for shell execution.

## Coding Standards

- Use C# with modern .NET.
- Prefer immutable models where practical.
- Use dependency injection.
- Use options pattern for configuration.
- Do not hardcode URLs, model names, paths, timeouts, or command names.
- Do not use magic strings or magic numbers.
- Keep classes small and focused.
- Prefer explicit error results over hidden exceptions for expected failures.
- Use English comments only.
- Add comments only when the code intent is not obvious.

## Testing Rules

Every implementation must include tests.

Minimum test layers:

- Unit tests for Core logic.
- Contract tests for schemas and DTOs.
- Integration tests for LLM adapters, file system tools, shell tools, and git tools.
- Evaluation tests for agent behavior.

Do not call a real LLM in unit tests.

## Observability Rules

Agent execution must be traceable.

Important events:

- agent.task.started
- agent.plan.created
- llm.request.sent
- llm.response.received
- tool.call.started
- tool.call.completed
- policy.rejected
- agent.task.completed
- agent.task.failed

Logs must not contain secrets.

## Pull Request / Task Completion Rules

A task is only complete when:

- The related spec exists.
- Acceptance criteria are covered.
- Tests pass.
- Safety impact is considered.
- Documentation is updated if needed.
- No unrelated files are changed.

## Agent Behavior Rules

When working in this repository, the coding agent must:

- Read `AGENTS.md` first.
- Locate the relevant spec before editing code.
- If no spec exists, create or request a spec first.
- Prefer minimal diffs.
- Explain assumptions before implementing.
- Never silently broaden task scope.
- Never change architecture without an ADR.