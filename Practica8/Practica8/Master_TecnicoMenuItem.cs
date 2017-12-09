using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practica8
{

    public class Master_TecnicoMenuItem
    {
        public Master_TecnicoMenuItem()
        {
            TargetType = typeof(Master_TecnicoDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}