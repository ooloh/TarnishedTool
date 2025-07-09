using System;
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


        public void Warp(Grace grace)
        {
            var bytes = AsmLoader.GetAsmBytes("GraceWarp");
            AsmHelper.WriteAbsoluteAddresses(bytes, new []
            {
                (WorldChrMan.Base.ToInt64(), 0x0 + 2),
                (grace.GraceEntityId, 0x12 + 2),
                (Funcs.GraceWarp, 0x20 + 2)
            });
            
            _memoryIo.AllocateAndExecute(bytes);
        }

        public void UnlockGrace(Grace grace)
        {
            var bytes = AsmLoader.GetAsmBytes("GraceUnlock");
            AsmHelper.WriteAbsoluteAddresses(bytes, new []
            {
                (_memoryIo.ReadInt64(VirtualMemFlag.Base), 0x4 + 2 ),
                (grace.FlagId, 0xE + 2),
                (GraceUnlock: Funcs.SetEvent, 0x38 + 2)
            });
            _memoryIo.AllocateAndExecute(bytes);
        }

        public void Test()
        {
            
        }
    }
}