using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_1_lineal
{
    class StackClass
    {
        public ListClass list = new ListClass();

        public void Push(string data)
        {
            list.Add(data, list.GetAmountOfNodes());
        }


        public string Pop ()
        {
            string result = list.GetDataOnPos(list.GetAmountOfNodes() - 1);
            list.Delete(list.GetAmountOfNodes() - 1);
            return result;
        }

        public string Peek()
        {
             return list.GetDataOnPos(list.GetAmountOfNodes() - 1);
        }

        public bool Contains(string data)
        {
            if (list.Find(data) != -1) return true;
            else return false;
        }

        public int AmountOfEl()
        {
            return list.GetAmountOfNodes();
        }

        
        public void Finish()
        {
            list.Dispose();
        }
    }
}
