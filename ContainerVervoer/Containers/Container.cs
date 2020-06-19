using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
   public abstract class Container
   {
       public int weight { get; set; }
       public bool canHaveContainerOnTop { get; set; }
       public bool placeLow { get; set; }

       public Container() { }

       public Container(int _weight)
       {
           weight = _weight;
       }
   }
}
