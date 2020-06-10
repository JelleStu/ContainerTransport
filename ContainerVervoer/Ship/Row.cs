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

        public Row(int StacksAnmount, int _rownumber)
        {
            Stacks = new List<ContainerStack>();
            row = _rownumber;
            size = StacksAnmount;
            for (int i = 0; i < StacksAnmount; i++)
            {
                var stack = new ContainerStack(row);
                Stacks.Add(stack);
            }
            
        }

        public void AddContainerToStack(Container container, int _stack)
        {
          var stack = Stacks.FirstOrDefault(s => s.x == _stack);
          if (stack.CanAddContainer(container))
          {
              stack.AddContainerToStack(container);
          }
        }

        
    }
}
