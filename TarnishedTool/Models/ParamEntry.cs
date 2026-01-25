// 

using TarnishedTool.Enums;

namespace TarnishedTool.Models;

public record ParamEntry(uint Id, string Name)
{
    public bool HasName => !string.IsNullOrEmpty(DisplayName);
    public string Name { get; } = Name;
    public uint Id { get; } = Id;
    public Param Parent { get; set; } 
    public string ParentName => Parent.ToString();
    public string CustomName { get; set; }
    public string DisplayName => CustomName ?? Name; 
    // note for self: public string display name is always custom name unless custom name is empty then its default name
    
}