// 

using System;
using TarnishedTool.Memory;
using TarnishedTool.Services;

namespace TarnishedTool.Utilities;

public static class PatchManager
{
    public static bool Initialize(MemoryService memoryService)
    {
        if (memoryService.TargetProcess == null) return false;
        var module = memoryService.TargetProcess.MainModule;
        var fileVersion = module?.FileVersionInfo.FileVersion;
        var moduleBase = memoryService.BaseAddress;
        
        Console.WriteLine(fileVersion);

        return Offsets.Initialize(fileVersion, moduleBase);
    }
}