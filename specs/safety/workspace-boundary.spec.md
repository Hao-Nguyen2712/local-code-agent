# Workspace Boundary Specification

## Status

Draft

## Goal

Ensure the agent can only access files inside an approved workspace directory.

This policy applies to all tools that read, write, search, patch, delete, or inspect files.

## Scope

Applies to:

- File read tool
- File write tool
- Project search tool
- Git diff tool
- Shell command tool when working directory is involved

## Definitions

### Workspace Root

The approved root directory where the agent is allowed to operate.

### Relative Path

A path provided relative to the workspace root.

### Path Traversal

Any path that attempts to escape the workspace root using patterns such as `../`.

## Functional Requirements

### WR-001: Reject Absolute Paths

The policy must reject absolute paths unless explicitly enabled by configuration.

Example rejected paths:

```text
C:\Users\User\.ssh\id_rsa
/etc/passwd