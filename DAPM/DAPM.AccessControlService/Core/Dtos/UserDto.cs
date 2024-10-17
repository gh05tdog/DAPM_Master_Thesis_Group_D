using DAPM.AccessControlService.Core.Domain.Entities;

namespace DAPM.AccessControlService.Core.DTOs;

public record UserDto
{
    private readonly Guid id;
    
    public UserDto(Guid id)
    {
        this.id = id;
    }
    
    public UserDto(UserId userId)
    {
        id = userId.Id;
    }
    
    public UserId ToUserId() => new(id);
}