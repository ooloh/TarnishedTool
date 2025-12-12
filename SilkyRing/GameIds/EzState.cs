// 

namespace SilkyRing.GameIds;

public static class EzState
{
    public class TalkCommand
    {
        public int CommandId { get; }
        public int[] Params { get; }

        public TalkCommand(int commandId, int[] @params)
        {
            CommandId = commandId;
            Params = @params;
        }
    }

    public static class TalkCommands
    {
        public static readonly TalkCommand OpenKaleShop = new(22, [100500, 100524]);
    }
}