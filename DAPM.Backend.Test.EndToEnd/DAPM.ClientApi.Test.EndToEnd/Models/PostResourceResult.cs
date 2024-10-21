namespace DAPM.Test.EndToEnd.Models;

public record PostResourceResult(
    ItemIds ItemIds,
    string ItemType,
    bool Succeeded,
    string Message
);