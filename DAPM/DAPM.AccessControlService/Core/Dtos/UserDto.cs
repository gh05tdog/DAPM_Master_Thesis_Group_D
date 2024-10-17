using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.Dtos;

public record UserDto
{
    public readonly Guid Id;
    
    public UserDto(Guid id)
    {
        this.Id = id;
    }
    
    public UserDto(UserId userId)
    {
        Id = userId.Id;
    }
    
    public UserId ToUserId() => new(Id);
}