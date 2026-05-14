# Project Charter: code-agent-local

## 1. Project Summary

`code-agent-local` is a local-first coding agent project built to understand how LLM-powered agents interact with prompts, tools, memory, policies, files, shell commands, and evaluation systems.

The project follows Spec-Driven Design.

No runtime feature should be implemented before its behavior is described by a specification under `specs/`.

## 2. Mission

Build a safe, observable, spec-driven local coding agent framework for learning and controlled code automation.

The project is optimized for:

- Learning how agent systems work
- Controlled tool execution
- Clear architecture
- Safety-first development
- Observability
- Evaluation-driven improvement

## 3. Primary Goals

The project aims to build:

1. A local coding agent that can understand user tasks.
2. A provider-agnostic LLM adapter.
3. A controlled tool execution system.
4. A workspace safety policy layer.
5. A basic agent loop: plan, act, observe, review, answer.
6. A file read/write workflow with diff preview.
7. A command execution policy with allowlists.
8. A memory system for task and project context.
9. An evaluation system for regression testing agent behavior.
10. A local CLI and API for interacting with the agent.

## 4. Non-Goals

The project will not initially build:

- An unrestricted autonomous agent
- A multi-agent system
- A production SaaS platform
- A cloud-hosted agent runtime
- A plugin marketplace
- A fine-tuned model
- An unrestricted shell execution environment
- Auto-commit or auto-push workflows
- A complex UI before the CLI and core runtime are stable
- Vector database memory before basic memory and evaluation are implemented

## 5. Target Users

The main target user is a developer who wants to understand how coding agents work internally.

Secondary users:

- Developers learning LLM application architecture
- Developers experimenting with local models
- Developers studying tool calling and agent safety
- Developers building controlled code automation tools

## 6. System Boundary

The system may interact with:

- Local files inside an approved workspace
- A local or OpenAI-compatible LLM provider
- Git commands allowed by policy
- Shell commands allowed by policy
- Local configuration files
- Local evaluation cases

The system must not access:

- Files outside the configured workspace
- Secrets without explicit permission
- Arbitrary shell commands
- Network resources unless explicitly configured
- External services from Core domain logic

## 7. Architecture Principles

### 7.1 Spec First

Every feature must start from a specification.

Required flow:

1. Write or update spec.
2. Define acceptance criteria.
3. Define schema or contract if needed.
4. Implement the smallest working solution.
5. Add tests.
6. Add evaluation case if agent behavior is affected.
7. Update documentation if architecture changes.

### 7.2 Provider-Agnostic Core

The Core project must not depend on Ollama, OpenAI, shell, file system, database, or external services.

Core defines interfaces.

Infrastructure implements those interfaces.

### 7.3 Safety Before Autonomy

The agent must not receive powerful tools before safety policies exist.

Required safety controls:

- Workspace boundary validation
- Path traversal rejection
- Absolute path rejection
- Command allowlist
- Secret redaction
- Approval before destructive operations
- Diff preview before file writes

### 7.4 Observable Agent Execution

Every agent task should be traceable.

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

### 7.5 Evaluation Over Assumption

Agent quality must be measured with evaluation cases.

Do not assume the agent improved just because the prompt or model changed.

## 8. High-Level Architecture

```text
User
 ↓
CLI / API
 ↓
Application Layer
 ↓
Agent Runner
 ↓
Agent Loop
 ↓
Planner
 ↓
LLM Client
 ↓
Tool Registry
 ↓
Policy Evaluator
 ↓
Tool Executor
 ↓
Observation / Memory / Evaluation