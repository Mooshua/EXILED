// -----------------------------------------------------------------------
// <copyright file="ReportingCheaterEventArgs.cs" company="Exiled Team">
// Copyright (c) Exiled Team. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Exiled.Events.EventArgs
{
    using System;

    using Exiled.API.Features;

    /// <summary>
    /// Contains all information before reporting a cheater.
    /// </summary>
    public class ReportingCheaterEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingCheaterEventArgs"/> class.
        /// </summary>
        /// <param name="issuer"><inheritdoc cref="Issuer"/></param>
        /// <param name="target"><inheritdoc cref="Target"/></param>
        /// <param name="serverPort"><inheritdoc cref="ServerPort"/></param>
        /// <param name="reason"><inheritdoc cref="Reason"/></param>
        /// <param name="isAllowed"><inheritdoc cref="IsAllowed"/></param>
        public ReportingCheaterEventArgs(
            Player issuer,
            Player target,
            int serverPort,
            string reason,
            bool isAllowed = true)
        {
            Issuer = issuer;
            Target = target;
            ServerPort = serverPort;
            Reason = reason;
            IsAllowed = isAllowed;
        }

        /// <summary>
        /// Gets the issuing player.
        /// </summary>
        public Player Issuer { get; }

        /// <summary>
        /// Gets the targeted player.
        /// </summary>
        public Player Target { get; }

        /// <summary>
        /// Gets the server id.
        /// </summary>
        public int ServerPort { get; }

        /// <summary>
        /// Gets or sets the report reason.
        /// </summary>
        public string Reason { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the report will be sent.
        /// </summary>
        public bool IsAllowed { get; set; }
    }
}
