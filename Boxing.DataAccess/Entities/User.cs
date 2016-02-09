using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Boxing.Core.DataAccess.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        [ForeignKey("Login")]
        [Index(IsUnique = true)]
        public int? LoginId { get; set; }
        public virtual Login Login { get; set; }

        public double Rating { get; set; }
    }
}
