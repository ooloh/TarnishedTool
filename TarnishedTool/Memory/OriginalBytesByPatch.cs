// 

using static TarnishedTool.Enums.GameVersion;

namespace TarnishedTool.Memory;

public static class OriginalBytesByPatch
{

    public static class AttackInfo
    {
        public static byte[] GetOriginal() => Offsets.Version switch
        {
            Version1_8_0 or Version1_8_1 or Version1_9_0 or Version1_9_1 or Version2_0_1 => [0xF3, 0x44, 0x0F, 0x59, 0xD0],
            _ => [0xF3, 0x44, 0x0F, 0x59, 0xC8]
        };
    }

    public static class NoRunesFromEnemies
    {
        public static byte[] GetOriginal() => Offsets.Version switch
        {
            Version1_8_1 or Version1_9_0 or Version1_9_1 or Version2_0_1 => [0x41, 0xFF, 0x91, 0xB8, 0x05, 0x00, 0x00],
            _ => [0x41, 0xFF, 0x91, 0xC8, 0x05, 0x00, 0x00]
        };
    }
    
}