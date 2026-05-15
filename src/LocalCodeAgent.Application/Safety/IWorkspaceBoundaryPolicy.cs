using LocalCodeAgent.Domain.Safety;

namespace LocalCodeAgent.Application.Safety;

public interface IWorkspaceBoundaryPolicy
{
    WorkspacePathValidationResult Validate(WorkspacePathRequest request);
}
