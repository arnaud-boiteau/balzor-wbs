using BalzorWbs.Models;

namespace BalzorWbs.Services;

public class InMemoryProjectService : IProjectService
{
    private static readonly List<WbsProject> _projects = CreateSeed();

    public Task<List<WbsProject>> GetProjectsAsync()
    {
        var copy = _projects.Select(CloneProject).ToList();
        return Task.FromResult(copy);
    }

    public Task<WbsProject?> GetProjectAsync(Guid id)
    {
        var project = _projects.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(project is null ? null : CloneProject(project));
    }

    public Task<WbsProject> CreateProjectAsync(string name)
    {
        var project = new WbsProject
        {
            Id = Guid.NewGuid(),
            Name = string.IsNullOrWhiteSpace(name) ? $"Nouveau projet {DateTime.Now:HHmmss}" : name.Trim(),
            RootTasks = new List<WbsTask>()
        };

        _projects.Add(CloneProject(project));
        return Task.FromResult(project);
    }

    public Task<bool> DeleteProjectAsync(Guid id)
    {
        var removed = _projects.RemoveAll(p => p.Id == id) > 0;
        return Task.FromResult(removed);
    }

    public Task SaveProjectAsync(WbsProject project)
    {
        var existingIndex = _projects.FindIndex(p => p.Id == project.Id);
        if (existingIndex >= 0)
        {
            _projects[existingIndex] = CloneProject(project);
        }
        else
        {
            _projects.Add(CloneProject(project));
        }

        return Task.CompletedTask;
    }

    private static List<WbsProject> CreateSeed()
    {
        var project = new WbsProject
        {
            Id = Guid.NewGuid(),
            Name = "Projet Démo",
            RootTasks =
            [
                new WbsTask
                {
                    Title = "Conception",
                    Children =
                    [
                        new WbsTask { Title = "Expression de besoins", Charge = 20, Consumed = 6, Raf = 12, DeliveryDate = DateTime.Today.AddDays(7) },
                        new WbsTask { Title = "Spécifications", Charge = 16, Consumed = 4, Raf = 12, DeliveryDate = DateTime.Today.AddDays(12) },
                    ]
                },
                new WbsTask
                {
                    Title = "Réalisation",
                    Children =
                    [
                        new WbsTask { Title = "Développement UI", Charge = 40, Consumed = 10, Raf = 30, DeliveryDate = DateTime.Today.AddDays(25) },
                        new WbsTask { Title = "Développement API", Charge = 32, Consumed = 8, Raf = 24, DeliveryDate = DateTime.Today.AddDays(22) },
                        new WbsTask { Title = "Tests", Charge = 18, Consumed = 0, Raf = 18, DeliveryDate = DateTime.Today.AddDays(30) }
                    ]
                }
            ]
        };

        return new List<WbsProject> { CloneProject(project) };
    }

    private static WbsProject CloneProject(WbsProject project)
    {
        return new WbsProject
        {
            Id = project.Id,
            Name = project.Name,
            RootTasks = project.RootTasks.Select(CloneTask).ToList()
        };
    }

    private static WbsTask CloneTask(WbsTask task)
    {
        return new WbsTask
        {
            Id = task.Id,
            Title = task.Title,
            Charge = task.Charge,
            Consumed = task.Consumed,
            Raf = task.Raf,
            DeliveryDate = task.DeliveryDate,
            Children = task.Children.Select(CloneTask).ToList()
        };
    }
}
