using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timsort
{
    public class Structure
    {
        private int _index;
        private int _length;

        public Structure(int index, int length)
        {
            _index = index;
            _length = length;
        }

        public int GetIndex()
        {
            return _index;
        }
        
        public int GetLength()
        {
            return _length;
        }

    }
}
