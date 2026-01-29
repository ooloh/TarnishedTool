// 

using System;
using System.Collections.Generic;
using TarnishedTool.Models;

namespace TarnishedTool.Interfaces;

public interface IAiService
{
    List<ChrInsEntry> GetNearbyChrInsEntries();
    int GetChrIdByChrIns(IntPtr chrIns);
    uint GetNpcParamIdByChrIns(IntPtr chrIns);
    int GetNpcThinkParamIdByChrIns(IntPtr chrIns);
    long GetHandleByChrIns(IntPtr chrIns);
    
}