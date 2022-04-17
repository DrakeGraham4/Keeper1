using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Keeper1.Models
{
    public class VaultKeep
    {
        public int Id { get; set; }

        public int KeepId { get; set; }
        public int VaultId { get; set; }

        public string CreatorId { get; set; }
        public Account? Creator { get; set; }
    }

    public class VKViewModel : Keep
    {
        public int VaultKeepId { get; set; }
    }
}