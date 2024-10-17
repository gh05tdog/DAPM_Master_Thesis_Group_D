using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record UserDto
{
    public Guid Id { get; set; }
    
    public UserId ToUserId() => new(Id);
}