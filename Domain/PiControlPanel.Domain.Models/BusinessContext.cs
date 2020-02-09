﻿namespace PiControlPanel.Domain.Models
{
    using System;

    /// <summary>
    /// Business context containing data required for any business flow.
    /// </summary>
    public class BusinessContext
    {
        /// <summary>
        /// Username of currently logged in user
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Determine whether user is anonymous or not
        /// </summary>
        public bool IsAnonymous { get; set; }
    }
}
