// -----------------------------------------------------------------------
// <copyright file="UsingBreakneckSpeedsEventArgs.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs
{
    using System;

    using Exiled.API.Features;
    using Exiled.API.Features.Items;

    /// <summary>
    /// Contains all information before an Scp-173 uses breakneck speeds.
    /// </summary>
    public class UsingBreakneckSpeedsEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UsingBreakneckSpeedsEventArgs"/> class.
        /// </summary>
        /// <param name="player"><inheritdoc cref="Player"/></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed"/></param>
        public UsingBreakneckSpeedsEventArgs(Player player, bool isAllowed = true)
        {
            Player = player;
            IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets the player who's using breakneck speeds.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the player can use breakneck speeds.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}
