# 0001 System Overview

## Status

Accepted

## Context

`code-agent-local` is a local-first coding agent project that follows Spec-Driven Design. The current repository skeleton separates production code, tests, specifications, schemas, documentation, prompts, samples, scripts, evaluation cases, configuration, and a local workspace.

This document records the intended Clean Architecture layering for the current skeleton. It does not introduce workspace boundary policy, LLM integration, or an agent loop.

## Layers

### Domain

`src/LocalCodeAgent.Domain` contains core domain concepts and rules. It must stay independent from infrastructure concerns such as file systems, shell execution, Git, LLM providers, logging sinks, databases, and network clients.

### Application

`src/LocalCodeAgent.Application` coordinates use cases and application services. It may depend on Domain abstractions and contracts, but it should not directly depend on concrete infrastructure adapters.

### Infrastructure

`src/LocalCodeAgent.Infrastructure` will contain concrete adapters for external systems. Infrastructure implements interfaces defined by inner layers and depends inward on Application or Domain, not the other way around.

### Tools

`src/LocalCodeAgent.Tools` is reserved for controlled tool implementations and related abstractions. Tool behavior must remain policy-governed and observable as specifications are added.

### CLI

`src/LocalCodeAgent.Cli` is a runtime entrypoint. It wires configuration, dependency injection, and user interaction while delegating behavior to Application services.

### Tests

`tests/` contains test projects. Unit tests should cover core logic without real LLM calls. Integration tests are used when behavior touches the file system, shell, external processes, LLM adapters, Git, or other infrastructure.

## Dependency Rules

- Domain must not depend on Application, Infrastructure, Tools, CLI, or concrete external providers.
- Application may depend on Domain and shared contracts.
- Infrastructure may depend on Application and Domain to implement declared interfaces.
- CLI may depend on Application and Infrastructure for composition.
- Tests may depend on the projects they verify.
- Specifications under `specs/` describe behavior before production implementation.
- Schemas under `schemas/` define structured contracts when needed.
- Evaluation cases under `evals/` cover agent behavior changes.

## Current Boundary

The repository currently defines the skeleton only. Safety policies, LLM adapters, tool execution, memory, evaluation execution, and agent loop behavior must be specified before implementation.
