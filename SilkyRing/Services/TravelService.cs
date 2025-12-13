using SilkyRing.Interfaces;
using SilkyRing.Memory;
using SilkyRing.Models;
using SilkyRing.Utilities;
using static SilkyRing.Memory.Offsets;

namespace SilkyRing.Services
{
    public class TravelService(MemoryService memoryService, HookManager hookManager) : ITravelService
    {

        public void Warp(Grace grace)
        {
            var bytes = AsmLoader.GetAsmBytes("GraceWarp");
            AsmHelper.WriteAbsoluteAddresses(bytes, new []
            {
                (WorldChrMan.Base.ToInt64(), 0x0 + 2),
                (grace.GraceEntityId, 0x12 + 2),
                (Functions.GraceWarp, 0x20 + 2)
            });
            
            memoryService.AllocateAndExecute(bytes);
        }
        
    }
}