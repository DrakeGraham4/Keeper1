using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper1.Models
{
    public class Vault
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrivate { get; set; }
        public string CreatorId { get; set; }
        public Account? Creator { get; set; }
    }
}