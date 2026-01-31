// 

namespace TarnishedTool.Models;

public class SpEffectObserve(int target, int spEffectId)
{
    public int Target { get; set; } = target;
    public int SpEffectId { get; set; } = spEffectId;
}