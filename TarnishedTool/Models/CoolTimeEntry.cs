// 

namespace TarnishedTool.Models;

public class CoolTimeEntry(int animationId, float timeSinceLastAttack, float cooldown)
{
    public int AnimationId { get; set; } = animationId;
    public float TimeSinceLastAttack { get; set; } = timeSinceLastAttack;
    public float Cooldown { get; set; } = cooldown;
    
    
}