using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
   public abstract class Container
   {
       public int weight { get; set; }
       private bool canHaveContainerOnTop;
       private ContainerStack containerStack;

       public Container() { }

       public Container(int weight, bool _canHaveContainerOnTop)
       {
           weight = weight;
           canHaveContainerOnTop = _canHaveContainerOnTop;
       }
   }
}
