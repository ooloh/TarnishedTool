// 

namespace SilkyRing.Models;

public class Item
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int StackSize { get; set; } 
    public int MaxStorage { get; set; }
    public string CategoryName { get; set; }
}