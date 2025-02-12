// -----------------------------------------------------------------------
// <copyright file="Destroying.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.Patches.Events.Player
{
#pragma warning disable SA1600

    using System.Collections.Generic;
    using System.Reflection.Emit;
    using System.Runtime.CompilerServices;

    using Exiled.API.Features;
    using Exiled.Events.EventArgs;

    using HarmonyLib;

    using NorthwoodLib.Pools;

    using static HarmonyLib.AccessTools;

    [HarmonyPatch(typeof(ReferenceHub), nameof(ReferenceHub.OnDestroy))]
    internal static class Destroying
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            LocalBuilder player = generator.DeclareLocal(typeof(Player));

            Label cdcLabel = generator.DefineLabel();

            newInstructions[0].labels.Add(cdcLabel);

            newInstructions.InsertRange(0, new CodeInstruction[]
            {
                new(OpCodes.Ldarg_0),
                new(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new[] { typeof(ReferenceHub) })),
                new(OpCodes.Stloc_S, player.LocalIndex),
                new(OpCodes.Ldloc_S, player.LocalIndex),
                new(OpCodes.Ldnull),
                new(OpCodes.Cgt_Un),
                new(OpCodes.Brfalse_S, cdcLabel),
                new(OpCodes.Ldloc_S, player.LocalIndex),
                new(OpCodes.Newobj, GetDeclaredConstructors(typeof(DestroyingEventArgs))[0]),
                new(OpCodes.Call, Method(typeof(Handlers.Player), nameof(Handlers.Player.OnDestroying))),
                new(OpCodes.Call, PropertyGetter(typeof(Player), nameof(Player.Dictionary))),
                new(OpCodes.Ldloc_S, player.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Player), nameof(Player.GameObject))),
                new(OpCodes.Callvirt, Method(typeof(Dictionary<UnityEngine.GameObject, Player>), nameof(Dictionary<UnityEngine.GameObject, Player>.Remove), new[] { typeof(UnityEngine.GameObject) })),
                new(OpCodes.Pop),
                new(OpCodes.Call, PropertyGetter(typeof(Player), nameof(Player.UnverifiedPlayers))),
                new(OpCodes.Ldarg_0),
                new(OpCodes.Callvirt, Method(typeof(ConditionalWeakTable<ReferenceHub, Player>), nameof(ConditionalWeakTable<ReferenceHub, Player>.Remove), new[] { typeof(ReferenceHub) })),
                new(OpCodes.Pop),
                new(OpCodes.Call, PropertyGetter(typeof(Player), nameof(Player.IdsCache))),
                new(OpCodes.Ldloc_S, player.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Player), nameof(Player.Id))),
                new(OpCodes.Callvirt, Method(typeof(Dictionary<int, Player>), nameof(Dictionary<int, Player>.Remove), new[] { typeof(int) })),
                new(OpCodes.Pop),
                new(OpCodes.Ldloc_S, player.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Player), nameof(Player.UserId))),
                new(OpCodes.Ldnull),
                new(OpCodes.Cgt_Un),
                new(OpCodes.Brfalse_S, cdcLabel),
                new(OpCodes.Call, PropertyGetter(typeof(Player), nameof(Player.UserIdsCache))),
                new(OpCodes.Ldloc_S, player.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Player), nameof(Player.UserId))),
                new(OpCodes.Callvirt, Method(typeof(Dictionary<string, Player>), nameof(Dictionary<string, Player>.Remove), new[] { typeof(string) })),
                new(OpCodes.Pop),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}
