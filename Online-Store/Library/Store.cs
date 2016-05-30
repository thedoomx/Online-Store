using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Apple> Apples { get; set; }
        public virtual List<Banana> Bananas { get; set; }
    }
}
