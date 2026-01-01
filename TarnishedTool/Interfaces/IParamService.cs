// 

using System;

namespace TarnishedTool.Interfaces;

public interface IParamService
{
    IntPtr GetParamRow(int tableIndex, int slotIndex, uint rowId);
}