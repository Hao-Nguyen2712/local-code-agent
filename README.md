# code-agent-local

A spec-driven local coding agent project for learning how LLM-powered agents interact with tools, memory, policies, and codebases.

## Goals

- Build a local coding agent step by step.
- Use a local LLM provider.
- Keep tool execution controlled and observable.
- Learn agent loop design, tool calling, structured output, memory, and evaluation.
- Follow Spec-Driven Design throughout the project.

## Development Philosophy

This project follows Spec-Driven Design.

Every feature starts with:

1. Specification
2. Acceptance criteria
3. Contract or schema
4. Implementation
5. Tests
6. Evaluation where applicable

## Initial Scope

The first implementation phase includes:

- Workspace boundary policy
- LLM chat completion adapter
- File read tool
- File write tool with diff preview
- Basic CLI
- Agent loop skeleton

## Out of Scope for Early Phase

- Unrestricted shell execution
- Multi-agent architecture
- Vector memory
- Background autonomous execution
- Production deployment