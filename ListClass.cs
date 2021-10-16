using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Timsort
{
    public class ListClass
    {
        static Node head;
        static int amount_of_nodes = 0;

        public int chosen_pos;
        public string chosen_el;

        public Node GetHead()
        {
            return head;
        }

        public int GetAmountOfNodes()
        {
            return amount_of_nodes;
        }

        public Node GetLast()
        {
            Node shovel = head;
            while (shovel.Next != null)
            {
                shovel = shovel.Next;
            }
            return shovel;
        }

        public override string ToString()
        {
            string list_to_str = "";
            Node shovel = head;
            int counter = 0;
            while (shovel.Next != null)
            {
                shovel = shovel.Next;
                list_to_str += shovel.Data;
                list_to_str += " -> ";
                if (counter % 10 == 0 && counter != 0) list_to_str += "\n";
                counter += 1;
                
            }
            return list_to_str;
        }
        public Node GetOnPos(int position)
        {
            Node shovel = head;
            for (int i = 0; i <= position; i++)
            {
                shovel = shovel.Next;
            }
            return shovel;
        }

        public Structure GetDataOnPos(int position)
        {
            Node shovel = head;
            for (int i = 0; i <= position; i++)
            {
                shovel = shovel.Next;
            }
            return shovel.Data;
        }

       /* public int Find(string element)
        {
            int position = 0;

            Node shovel = head;
            while (shovel.Next != null)
            {
                shovel = shovel.Next;
                if (shovel.Data == element) return position;
                position += 1;
            }
            //if we don't have such an element
            return -1;
        }*/

        public void CreateNode(Node previous, Structure data, bool is_end)
        {
            //to the end or center
            Node new_node = new Node();
            new_node.Data = data;
            if (!is_end)
            {
              new_node.Next = previous.Next;
            }
            
            previous.Next = new_node;
        }

        public void CreateNode(Structure data)
        {
            //to head
            Node new_node = new Node();
            new_node.Data = data;

            if (amount_of_nodes != 0)
            {
                new_node.Next = head.Next;
            }
            else head = new Node();
            head.Next = new_node;
        }

        public void Add(Structure data, int position)
        {
            if (position == 0)
            {
                //to the head
                CreateNode(data);
            }
            else if (position == amount_of_nodes)
            {
                //to the end
                CreateNode(GetOnPos(position - 1), data, true);
            }
            else
            {
                CreateNode(GetOnPos(position - 1), data, false);
            }
            amount_of_nodes += 1;
        }

        public void Delete(int position)
        {
            Node shovel = head;
            //find previous;
            for (int i = 0; i < position; i++)
            {
                shovel = shovel.Next;
            }
            shovel.Next = shovel.Next.Next;
            amount_of_nodes -= 1;
        }
        
        public void Free()
        {
            head = null;
            amount_of_nodes = 0;
        }
    }
}
