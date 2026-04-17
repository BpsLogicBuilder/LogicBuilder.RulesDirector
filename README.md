# LogicBuilder.RulesDirector

[![Build Status](https://github.com/BpsLogicBuilder/LogicBuilder.RulesDirector/actions/workflows/ci.yml/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.RulesDirector/actions/workflows/ci.yml)
[![CodeQL](https://github.com/BpsLogicBuilder/LogicBuilder.RulesDirector/actions/workflows/github-code-scanning/codeql/badge.svg)](https://github.com/BpsLogicBuilder/LogicBuilder.RulesDirector/actions/workflows/github-code-scanning/codeql)
[![codecov](https://codecov.io/gh/BpsLogicBuilder/LogicBuilder.RulesDirector/graph/badge.svg?token=A0QOZI37NA)](https://codecov.io/gh/BpsLogicBuilder/LogicBuilder.RulesDirector)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=BpsLogicBuilder_LogicBuilder.RulesDirector&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=BpsLogicBuilder_LogicBuilder.RulesDirector)
[![NuGet](https://img.shields.io/nuget/v/LogicBuilder.RulesDirector.svg)](https://www.nuget.org/packages/LogicBuilder.RulesDirector)

A .NET Standard 2.0 library that manages flow state and rule engine execution for applications built with [LogicBuilder](https://github.com/BpsLogicBuilder/LogicBuilder).

## Overview

LogicBuilder.RulesDirector provides the runtime infrastructure for executing business logic flows defined visually in the LogicBuilder application. It serves as the orchestration layer that:

- **Manages Flow State**: Tracks the current position in a flow by maintaining module names, page indices, and shape indices
- **Coordinates Rule Execution**: Switches between rule engines as the flow navigates between different modules
- **Maintains Navigation History**: Preserves calling module stacks to support nested module invocation and returns
- **Provides Flow Progress Tracking**: Records the execution path through flow diagrams for debugging and auditing
- **Supports State Backup/Restore**: Enables save points and rollback functionality for complex workflows

## Key Components

### DirectorBase

The abstract base class that defines the core flow management contract:

- **Driver Property**: Encodes the current position in the flow (format: `{ShapeIndex}P{PageIndex}`)
- **Selection Property**: Handles multiple-choice branching logic within flows
- **Module Management**: Tracks module begin/end events and maintains module call stacks
- **Flow Backup**: Captures and restores complete flow state via `FlowBackupData` objects
- **Progress Tracking**: Maintains a list of visited flow positions for visibility
- Automatically retrieves and executes the appropriate ruleset when modules change
- Manages module invocation stacks for nested flow calls
- Handles flow initialization via `StartInitialFlow()`
- Coordinates with `IRulesCache` to access compiled rule engines

### Supporting Interfaces

- **IFlowActivity**: The execution context that connects the Director to application-specific logic
- **IRulesCache**: Repository for accessing compiled rule engines by module name
- **Progress**: Event-driven collection of flow execution steps


## How It Works

1. **Flow Initialization**: `StartInitialFlow()` is called with a module name
2. **Rule Execution**: The Director retrieves the ruleset from the cache and executes it
3. **State Tracking**: As rules fire, the `Driver` property is updated with shape/page coordinates
4. **Module Navigation**: When a rule invokes a new module, `ModuleBeginName` triggers:
   - Current state is pushed onto the calling module stack
   - The new module's ruleset is loaded and executed
5. **Module Return**: When `ModuleEndName` is set, state is restored from the stack
6. **Progress Logging**: Each state change is recorded in the `Progress` collection

## Flow State Encoding

The `Driver` property encodes position as: `{ShapeIndex}P{PageIndex}`

Example: `"15P2"` means Shape 15 on Page 2 of the flow diagram

## Use Cases

- Business process automation systems
- Rules-based workflow engines
- Decision management applications
- Dynamic form flows and wizards
- State machine implementations

## Requirements

- .NET Standard 2.0 or higher
- Compatible with .NET Framework 4.6.1+ and .NET Core 2.0+
- CLS-compliant for cross-language interoperability

## Related Projects

- [LogicBuilder](https://github.com/BpsLogicBuilder/LogicBuilder) - Visual flow diagram designer and code generator
