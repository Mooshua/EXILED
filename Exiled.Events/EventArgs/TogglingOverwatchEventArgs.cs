﻿// -----------------------------------------------------------------------
// <copyright file="TogglingOverwatchEventArgs.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs
{
    using Exiled.API.Features;

    /// <summary>
    /// Contains all information before a player toggles the overwatch.
    /// </summary>
    public class TogglingOverwatchEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TogglingOverwatchEventArgs"/> class.
        /// </summary>
        /// <param name="player"><inheritdoc cref="Player"/></param>
        /// <param name="newValue"><inheritdoc cref="IsEnabled"/></param>
        public TogglingOverwatchEventArgs(Player player, bool newValue)
        {
            Player = player;
            IsEnabled = newValue;
            IsAllowed = true;
        }

        /// <summary>
        /// Gets the player that is toggling overwatch.
        /// </summary>
        public Player Player { get; }

        /// <summary>
        /// Gets or sets a value indicating whether overwatch will be enabled or not.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the event is allowed or not.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}
