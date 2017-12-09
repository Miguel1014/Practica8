using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica8
{

    public class MasterDetailPage1MenuItem
    {
        public MasterDetailPage1MenuItem()
        {
            TargetType = typeof(Vista);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}