﻿// -----------------------------------------------------------------------
// <copyright file="PlayerInventorySee.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.CustomItems.Patches
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Reflection.Emit;

    using CommandSystem.Commands.RemoteAdmin;

    using Exiled.API.Features.Items;
    using Exiled.CustomItems.API.Features;

    using HarmonyLib;

    using InventorySystem.Items;

    using NorthwoodLib.Pools;

    using static HarmonyLib.AccessTools;

    /// <summary>
    /// Patches <see cref="PlayerInventoryCommand.Execute"/>.
    /// Adds the CustomItem support.
    /// </summary>
    [HarmonyPatch(typeof(PlayerInventoryCommand), nameof(PlayerInventoryCommand.Execute))]
    public class PlayerInventorySee
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstruction = ListPool<CodeInstruction>.Shared.Rent(instructions);

            LocalBuilder item = generator.DeclareLocal(typeof(Item));
            LocalBuilder customItem = generator.DeclareLocal(typeof(CustomItem));

            int offset = 0;
            int index = newInstruction.FindIndex(i => i.opcode == OpCodes.Ldfld && (FieldInfo)i.operand == Field(typeof(ItemBase), nameof(ItemBase.ItemTypeId))) + offset;

            Label continueLabel = generator.DefineLabel();
            Label checkLabel = generator.DefineLabel();
            Label endLabel = generator.DefineLabel();

            newInstruction[index + 4].labels.Add(continueLabel);

            newInstruction.RemoveRange(index, 2);

            newInstruction.InsertRange(index, new[]
            {
                new(OpCodes.Call,  Method(typeof(Item), nameof(Item.Get))),
                new(OpCodes.Dup),
                new(OpCodes.Stloc_S, item.LocalIndex),
                new(OpCodes.Brfalse_S, continueLabel),
                new(OpCodes.Ldloc_S, item.LocalIndex),
                new(OpCodes.Ldloca_S, customItem.LocalIndex),
                new(OpCodes.Call, Method(typeof(CustomItem), nameof(CustomItem.TryGet), new[] { typeof(Item), typeof(CustomItem).MakeByRefType() })),
                new(OpCodes.Brfalse_S, checkLabel),
                new(OpCodes.Ldloc_S, customItem.LocalIndex),
                new(OpCodes.Callvirt, PropertyGetter(typeof(CustomItem), nameof(CustomItem.Name))),
                new(OpCodes.Br_S, endLabel),
                new CodeInstruction(OpCodes.Ldloc_S, item.LocalIndex).WithLabels(checkLabel),
                new(OpCodes.Callvirt, PropertyGetter(typeof(Item), nameof(Item.Type))),
                new(OpCodes.Box, typeof(ItemType)),
                new CodeInstruction(OpCodes.Nop).WithLabels(endLabel),
            });

            for (int z = 0; z < newInstruction.Count; z++)
                yield return newInstruction[z];

            ListPool<CodeInstruction>.Shared.Return(newInstruction);
        }
    }
}
