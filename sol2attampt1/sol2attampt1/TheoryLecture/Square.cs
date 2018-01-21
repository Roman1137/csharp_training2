using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sol2attampt1
{
    class Square:Figure
    {
        private int size;
       
        public Square(int size)
        {
            this.size = size;
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }
      

    }
}
