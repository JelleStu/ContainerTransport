using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContainerVervoer
{
    public class Row
    {
        public List<ContainerStack> Stacks { get; set; }
        public int row;
        public int size;
        public int rowWeight { get; set; }

        public Row(int StacksAnmount, int _rownumber)
        {
            Stacks = new List<ContainerStack>();
            row = _rownumber;
            size = StacksAnmount;
            for (int i = 0; i < StacksAnmount; i++)
            {
                var stack = new ContainerStack(i);
                Stacks.Add(stack);
            }
        }

        public int GetRowkWeight()
        {
            foreach (var stack in Stacks)
            {
                rowWeight += stack.weight;
            }
            return rowWeight;
        }

        
    }
}
