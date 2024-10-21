namespace DAPM.Test.EndToEnd.Models;

public record PostRepositoryResult(
    ItemIds ItemIds,
    string ItemType,
    bool Succeeded,
    string Message
);