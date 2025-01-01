using Microsoft.EntityFrameworkCore;

namespace Friends.Net.Data;

public class ApplicationShortcut
{
    public int Id { get; set; } = 0;
    public string Title { get; set; } = "";
    public string URL { get; set; } = "";
    public string Icon { get; set; } = "";
    public List<ApplicationGroup> SecurityGroups { get; set; } = [];
    
    public static string[] GetVisibleProperties()
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