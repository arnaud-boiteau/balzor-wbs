using BalzorWbs.Models;

namespace BalzorWbs.Services;

public interface IProjectService
{
    Task<List<WbsProject>> GetProjectsAsync();
    Task<WbsProject?> GetProjectAsync(Guid id);
    Task<WbsProject> CreateProjectAsync(string name);
    Task<bool> DeleteProjectAsync(Guid id);
    Task SaveProjectAsync(WbsProject project);
}
