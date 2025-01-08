using Microsoft.AspNetCore.Identity;

namespace Friends.Net.Data;

public sealed class ApplicationGroup(string name) : IdentityRole(name)
{
    public string[] SecurityGroupBinding { get; set; } = [];
    public ApplicationGroup() : this("")
    {
        
    }
    public bool IsEmpty()
    {
        return
        string.IsNullOrWhiteSpace(Name) &&
        !SecurityGroupBinding.Any();
    }
    public static String[] GetVisibleProperties()
    {
        return
        [
            "Name", 
            "SecurityGroupBinding"
        ];
    }
}
