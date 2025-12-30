// 

using System;
using System.Threading;
using System.Threading.Tasks;
using TarnishedTool.Interfaces;
using static TarnishedTool.GameIds.EzState;
using static TarnishedTool.Memory.Offsets;

namespace TarnishedTool.Services;

public class FlaskService(IEzStateService ezStateService, MemoryService memoryService) : IFlaskService
{
    public const int CrimsonFlaskBaseId = 1000;
    public const int SacredTearId = 10020;
    public const int GoodsItemType = 3;
    public const int MaxUpgradeTextId = 13040000;
    public const int NoFlasksTextId = 13040150;
    public const int NoSacredTearsTextId = 20011000;
    public const int UpgradeConfirmationTextId = 20011001;
    public const int UpgradeSuccessTextId = 13040020;

    private const int DialogResultPending = -1;
    private const int DialogResultConfirm = 1;

    public enum DialogResult
    {
        Confirm,
        Cancel
    }

    public async Task TryUpgradeFlask(CancellationToken ct = default)
    {
        ClearDialogResult();

        int flaskLevel = ezStateService.EnvQuery(EnvQueries.GetFlaskLevel).IntValue;

        if (flaskLevel > 13)
        {
            ezStateService.ExecuteTalkCommand(TalkCommands.OpenDialog(7, MaxUpgradeTextId, 1, 0, 1));
            return;
        }

        int currentFlaskId = FindFlaskItemId(CrimsonFlaskBaseId);
        if (currentFlaskId < 0)
        {
            ezStateService.ExecuteTalkCommand(TalkCommands.OpenDialog(7, NoFlasksTextId, 1, 0, 1));
            return;
        }

        ezStateService.ExecuteTalkCommand(TalkCommands.OpenDialog(8, UpgradeConfirmationTextId, 3, 4, 2));

        var result = await WaitForDialogResult(ct);

        if (result == DialogResult.Cancel) return;

        if (!HasItem(GoodsItemType, SacredTearId))
        {
            ezStateService.ExecuteTalkCommand(TalkCommands.OpenDialog(7, NoSacredTearsTextId, 1, 0, 1));
            return;
        }

        ezStateService.ExecuteTalkCommand(TalkCommands.UpgradeFlask(flaskLevel + 1));

        ezStateService.ExecuteTalkCommand(TalkCommands.PlayerInventoryChange(GoodsItemType, SacredTearId, -1));

        ReplaceFlasks();
        SyncFlaskCharges();
        ezStateService.ExecuteTalkCommand(TalkCommands.OpenDialog(7, UpgradeSuccessTextId, 1, 0, 1));
    }

    private void ClearDialogResult()
    {
        var ptr = memoryService.FollowPointers(MenuMan.Base,
            [MenuMan.PopupMenu, (int)MenuMan.PopupMenuOffsets.DialogResult], false);
        memoryService.WriteInt32(ptr, -1);
    }

    private async Task<DialogResult> WaitForDialogResult(CancellationToken ct, int timeoutMs = 30000)
    {
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        cts.CancelAfter(timeoutMs);
        try
        {
            var ptr = memoryService.FollowPointers(MenuMan.Base,
                [MenuMan.PopupMenu, (int)MenuMan.PopupMenuOffsets.DialogResult], false);

            while (!cts.IsCancellationRequested)
            {
                var value = memoryService.ReadInt32(ptr);

                if (value != DialogResultPending)
                {
                    return value == DialogResultConfirm ? DialogResult.Confirm : DialogResult.Cancel;
                }

                await Task.Delay(16, cts.Token);
            }

            return DialogResult.Cancel;
        }
        catch (OperationCanceledException) when (!ct.IsCancellationRequested)
        {
            return DialogResult.Cancel;
        }
    }

    private int FindFlaskItemId(int baseId)
    {
        for (int level = 0; level <= 12; level++)
        {
            int id = baseId + (level * 2);
            if (HasItem(GoodsItemType, id)) return id;
            if (HasItem(GoodsItemType, id + 1)) return id + 1;
        }

        return -1;
    }

    private bool HasItem(int goodsType, int id) =>
        ezStateService.EnvQuery(EnvQueries.DoesPlayerHaveItem, goodsType, id).IntValue == 1;
    
    private void ReplaceFlasks()
    {
        int[] flaskIds = { 1000, 1001, 1050, 1051 };
    
        for (int level = 0; level <= 12; level++)
        {
            foreach (int baseId in flaskIds)
            {
                int oldId = baseId + (level * 2);
                if (HasItem(GoodsItemType, oldId))
                {
                    ezStateService.ExecuteTalkCommand(TalkCommands.ReplaceTool(oldId, oldId + 2, 1));
                }
            }
        }
    }
    
    private void SyncFlaskCharges()
    {
        int flaskLevel = ezStateService.EnvQuery(EnvQueries.GetFlaskLevel).IntValue;
    
        // mode2: 0 = Crimson (HP), 1 = Cerulean (FP)
        foreach (int flaskType in new[] { 0, 1 })
        {
            int allocation = ezStateService.EnvQuery(EnvQueries.GetEstusAllocation, flaskType).IntValue;
            if (allocation < 0) continue;
            
            int flaskId = 1001 + (flaskType * 50) + (flaskLevel * 2);
        
            int maxIterations = 20;
    
            while (!CompareInventoryCount(GoodsItemType, flaskId, allocation) && maxIterations-- > 0)
            {
                ezStateService.ExecuteTalkCommand(TalkCommands.PlayerInventoryChange(GoodsItemType, flaskId, 1));
            }
        }
    }

    private bool CompareInventoryCount(int goodsType, int id, int amount) =>
        ezStateService.EnvQuery(EnvQueries.ComparePlayerInventoryNumber, goodsType, id, 4, amount, 0).IntValue == 1;
}