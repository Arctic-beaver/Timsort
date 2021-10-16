using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timsort
{
    class StackClass
    {
        public ListClass list = new ListClass();

        public void Push(Structure data)
        {
            list.Add(data, list.GetAmountOfNodes());
        }


        public Structure Pop()
        {
            Structure result = list.GetDataOnPos(list.GetAmountOfNodes() - 1);
            list.Delete(list.GetAmountOfNodes() - 1);
            return result;
        }

        public Structure Peek()
        {
             return list.GetDataOnPos(list.GetAmountOfNodes() - 1);
        }

        /*public bool Contains(string data)
        {
            if (list.Find(data) != -1) return true;
            else return false;
        }*/

        public int AmountOfEl()
        {
            return list.GetAmountOfNodes();
        }

        
        public void Finish()
        {
            list.Free();
        }
    }
}
