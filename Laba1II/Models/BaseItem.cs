using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba1II.Models
{
    [Serializable]
    class BaseItem
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public int? NoBaseNumber { get; set; }
        public int? YesBaseNumber { get; set; }
    }
}
