# Cyclomatic Complexity Analysis Report
## Mediatr Architecture

**Analysis Date:** March 27, 2026
**Methodology:** Cyclomatic Complexity (CC) calculation based on code structure analysis
**Scope:** Complete dependency tree (Domain → Application → Infrastructure → REST API)

---

## Domain Layer

| Class Name | CC | Complexity Level |
|------------|----|-----------|
| `TodosByFilterSpecification` | 10 | High |
| `SimplerTodo` | 10 | High |
| `TodoByIdSpecification` | 3 | Medium |
| `CompleteTodoCommandValidator` | 1 | Low |
| `CompleteTodoCommand` | 1 | Low |
| `CreateTodoCommandValidator` | 1 | Low |
| `CreateTodoCommand` | 1 | Low |
| `RemoveTodoCommandValidator` | 1 | Low |
| `RemoveTodoCommand` | 1 | Low |
| `UpdateTodoCommandValidator` | 1 | Low |
| `UpdateTodoCommand` | 1 | Low |
| `Todo` | 1 | Low |

**Layer Summary:**
- Classes: 12
- Total CC: 32
- Average CC/Class: 2.67
- Highest CC: 10

## Application Layer

| Class Name | CC | Complexity Level |
|------------|----|-----------|
| `CommandHandlers` | 1 | Low |
| `CompleteTodoCommand` | 1 | Low |
| `CreateTodoCommand` | 1 | Low |
| `RemoveTodoCommand` | 1 | Low |
| `UpdateTodoCommand` | 1 | Low |
| `FetchTodosByFilterQuery` | 1 | Low |
| `GetTodoByIdQuery` | 1 | Low |
| `QueryHandlers` | 1 | Low |
| `IApplication` | 1 | Low |

**Layer Summary:**
- Classes: 9
- Total CC: 9
- Average CC/Class: 1.00
- Highest CC: 1

## Infrastructure Layer

| Class Name | CC | Complexity Level |
|------------|----|-----------|
| `ApplicationContext` | 1 | Low |
| `TodoConfiguration` | 1 | Low |
| `Repository` | 1 | Low |

**Layer Summary:**
- Classes: 3
- Total CC: 3
- Average CC/Class: 1.00
- Highest CC: 1

## Rest API Layer

| Class Name | CC | Complexity Level |
|------------|----|-----------|
| `ExceptionHandlingMiddleware` | 3 | Medium |
| `ValidationBehaviour` | 3 | Medium |
| `TodoEndpoints` | 1 | Low |
| `EndpointRouteBuilderExtensions` | 1 | Low |
| `UpdateTodoRequest` | 1 | Low |
| `UpdateTodoRequestBody` | 1 | Low |
| `CompleteTodoRequest` | 1 | Low |
| `CompleteTodoRequestBody` | 1 | Low |

**Layer Summary:**
- Classes: 8
- Total CC: 12
- Average CC/Class: 1.50
- Highest CC: 3

---

## Summary Statistics

### By Layer

| Layer | Classes | Total CC | Avg CC |
|-------|---------|----------|--------|
| Domain | 12 | 32 | 2.67 |
| Application | 9 | 9 | 1.00 |
| Infrastructure | 3 | 3 | 1.00 |
| Rest API | 8 | 12 | 1.50 |

### Project Total

| Metric | Value |
|--------|-------|
| Total Classes/Interfaces/Structs | 32 |
| Total Cyclomatic Complexity | 56 |
| Average CC per Type | 1.75 |
| Maintainability | ✅ Good |

### Complexity Distribution

| Complexity Level | Count | Percentage |
|------------------|-------|------------|
| Low (1-2) | 27 | 84.4% |
| Medium (3-5) | 3 | 9.4% |
| High (6-10) | 2 | 6.2% |
| Very High (11+) | 0 | 0.0% |

### Recommendations

✅ **Excellent code structure!** Low complexity indicates:
- Easy to understand and maintain
- Minimal testing overhead
- Low defect potential
