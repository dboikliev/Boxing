using System;
using System.Collections.Generic;
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
        public Guid? AuthenticationToken { get; set; }

        public double Rating { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Prediction> Predictions { get; set; }
    }
}
