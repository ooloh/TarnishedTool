// 

using System;

namespace SilkyRing.Enums;

[Flags]
public enum Affinity : ushort
{
    Standard  = 1 << 0,
    Heavy     = 1 << 1,
    Keen      = 1 << 2,
    Quality   = 1 << 3,
    Fire      = 1 << 4,
    FlameArt  = 1 << 5,
    Lightning = 1 << 6,
    Sacred    = 1 << 7,
    Magic     = 1 << 8,
    Cold      = 1 << 9,
    Poison    = 1 << 10,
    Blood     = 1 << 11,
    Occult    = 1 << 12,
}