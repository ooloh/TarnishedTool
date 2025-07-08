using SilkyRing.Memory;
using SilkyRing.Models;
using SilkyRing.Utilities;
using static SilkyRing.Memory.Offsets;

namespace SilkyRing.Services
{
    public class TravelService
    {
        
        private readonly MemoryIo _memoryIo;
        private readonly HookManager _hookManager;
        public TravelService(MemoryIo memoryIo, HookManager hookManager)
        {
            _memoryIo = memoryIo;
            _hookManager = hookManager;
        }


        public void Warp(WarpLocation warpLocation)
        {
            var bytes = AsmLoader.GetAsmBytes("GraceWarp");
            AsmHelper.WriteAbsoluteAddresses(bytes, new []
            {
                (WorldChrMan.Base.ToInt64(), 0x0 + 2),
                (warpLocation.GraceEntityId, 0x12 + 2),
                (Funcs.GraceWarp, 0x20 + 2)
            });
            
            _memoryIo.AllocateAndExecute(bytes);
        }
    }
}