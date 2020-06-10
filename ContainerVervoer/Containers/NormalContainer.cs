using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class NormalContainer : Container
    {
        public NormalContainer(int weight, bool _canHaveContainerOnTop) : base(weight, _canHaveContainerOnTop)
        {
        }

        public NormalContainer() { }
    }
}
