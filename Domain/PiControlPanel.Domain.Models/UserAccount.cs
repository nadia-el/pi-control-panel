namespace PiControlPanel.Domain.Models
{
    using System;

    public class UserAccount
    {
        public Guid ID { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
