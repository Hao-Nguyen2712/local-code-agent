using LocalCodeAgent.Domain.Tools;

namespace LocalCodeAgent.Application.Tools;

public interface IToolExecutionPolicy
{
    ToolExecutionResult Validate(ToolExecutionRequest request);
}
