using Friends.Net.Components.Account.Pages.Manage;
using Microsoft.EntityFrameworkCore;

namespace Friends.Net.Data;

public class ApplicationShortcut
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = "";
    public string URL { get; set; } = "";
    public Guid IconId { get; set; } = Guid.Empty;
    public string Icon { get; set; } = "";
    public string[] SecurityGroups { get; set; } = [];
    public bool IsEmpty()
    {
        return
        string.IsNullOrWhiteSpace(Title) &&
        string.IsNullOrWhiteSpace(URL) &&
        Guid.Empty == IconId && 
        SecurityGroups.Any();
    }
    public static String[] GetVisibleProperties()
    {
        return
        [
            "Title", 
            "Icon", 
            "URL",
            "SecurityGroups", 
        ];
    }
}