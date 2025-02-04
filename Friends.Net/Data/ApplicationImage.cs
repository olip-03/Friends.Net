using Microsoft.EntityFrameworkCore;

namespace Friends.Net.Data
{
    /// <summary>
    /// Image information to be used for App images. Class is used by the Settings page to determin when an image is actually clicked 
    /// </summary>
    public class ApplicationImage(string name)
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Base64 { get; set; } = "";
        public override string ToString()
        {
            return name;
        }
    }
}
