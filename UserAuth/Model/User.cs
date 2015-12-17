using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserAuth.Model
{
    public class User
    {
        public List<TotalInfo> Infos { get; set; } = new List<TotalInfo>();
        public string Name { get; set; }
    }
}
