﻿namespace DAPM.ClientApi.Services.Interfaces
{
    public interface IRepositoryService
    {
        public Guid GetRepositoryById(int organizationId, int repositoryId);
        public Guid GetResourcesOfRepository(int organizationId, int repositoryId);
        public Guid PostResourceToRepository(int organizationId, int repositoryId, string name, IFormFile resourceFile);
    }
}
