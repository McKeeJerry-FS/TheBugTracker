using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBugTracker.Models;

namespace TheBugTracker.Services.Interfaces
{
    public interface IBTProjectService
    {
        public Task AddNewProjectAsync(BTProject project);
        public Task<bool> AddProjectManagerAsync(string userId, int projectId);
        public Task<bool> AddUserToProjectAsync(string userId, int projectId);
        public Task ArchiveProjectAsync(BTProject project);
        public Task<List<BTProject>> GetAllProjectsByCompany(int companyId);
        public Task<List<BTProject>> GetAllProjectsByPriority(int companyId, string priorityName);
        public Task<List<BTUser>> GetAllProjectMembersExceptPMAsync(int projectId);
        public Task<List<BTProject>> GetArchivedProjectsByCompany(int companyId);
        public Task<List<BTUser>> GetDevelopersOnProjectAsync(int projectId);
        public Task<BTUser> GetProjectManagerAsync(int projectId);
        public Task<List<BTUser>> GetProjectMembersByRoleAsync(int projectId, string role);
        public Task<BTProject> GetProjectByIdAsync(int projectId, int companyId);
        public Task<List<BTUser>> GetSubmittersOnProjectAsync(int projectId);
        public Task<List<BTUser>> GetUsersNotOnProjectAsync(int projectId, int companyId);
        public Task<List<BTProject>> GetUserProjectsAsync(string userId);
        public Task<bool> IsUserOnProjectAsync(string userId, int projectId);
        public Task<int> LookupProjectPriorityId(string priorityName);
        public Task RemoveProjectManagerAsync(int projectId);
        public Task RemoveUsersFromProjectByRoleAsync(string role, int projectId);
        public Task RemoveUserFromProjectAsync(string userId, int projectId);
        public Task UpdateProjectAsync(BTProject project);








    }
}
