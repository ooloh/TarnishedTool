// 

using System;
using System.Collections.Generic;
using TarnishedTool.Interfaces;
using TarnishedTool.Models;
using static TarnishedTool.Memory.Offsets;

namespace TarnishedTool.Services;

public class AiService(MemoryService memoryService) : IAiService
{
    public const int ChrInsEntrySize = 0x8;

    #region Public Methods

    public List<ChrInsEntry> GetNearbyChrInsEntries()
    {
        var worldChrMan = memoryService.Read<IntPtr>(WorldChrMan.Base);
        nint begin = memoryService.Read<nint>(worldChrMan + WorldChrMan.ChrInsByUpdatePrioBegin);
        nint end = memoryService.Read<nint>(worldChrMan + WorldChrMan.ChrInsByUpdatePrioEnd);

        var count = (end - begin) / ChrInsEntrySize;
        
        byte[] buffer = memoryService.ReadBytes(begin, (int)(count * ChrInsEntrySize));
        var entries = new List<ChrInsEntry>();
        for (int i = 0; i < count; i++)
        {
            var entry = ParseEntry(buffer, i * ChrInsEntrySize);
            entries.Add(entry);
        }

        return entries;
    }

    public int GetChrIdByChrIns(IntPtr chrIns) =>
        memoryService.Read<int>(chrIns + ChrIns.ChrId);

    public uint GetNpcParamIdByChrIns(IntPtr chrIns) =>
        memoryService.Read<uint>(chrIns + ChrIns.NpcParamId);

    public int GetNpcThinkParamIdByChrIns(IntPtr chrIns) =>
        memoryService.Read<int>(GetAiThinkPtr(chrIns) + ChrIns.AiThinkOffsets.NpcThinkParamId);

    public long GetHandleByChrIns(IntPtr chrIns) => 
        memoryService.Read<long>(chrIns + ChrIns.Handle);
        
    #endregion

    #region Private Methods

    private ChrInsEntry ParseEntry(byte[] buffer, int offset)
    {
        IntPtr chrIns = (IntPtr)BitConverter.ToInt64(buffer, offset);

        return new ChrInsEntry(chrIns);
    }

    private nint GetAiThinkPtr(IntPtr chrIns) =>
        memoryService.FollowPointers(chrIns, [..ChrIns.AiThink], true, false);

    #endregion
}