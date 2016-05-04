using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AsyncTest.Communication.Server.Database.Authentication
{
    public class ClientEntity : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        [Index(IsUnique = true)]
        public string SharedSecret { get; set; }
    }
}