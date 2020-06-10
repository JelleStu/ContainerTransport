using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ContainerVervoer
{
    public class ContainerStack
    {
        public List<Container> containers { get; set; }
        public int x;
        public int weight { get; set; }

        public ContainerStack(int _x)
        {
            x = _x;
            containers = new List<Container>();
        }

        public void AddContainerToStack(Container _container)
        {
            containers.Add(_container);
            weight += _container.weight;
        }

        public bool CanAddContainer(Container _container)
        {
            int weightOnNewContainer = 0; 
            switch (_container)
            {
                case CooledAndValuable cv:
                    return containers.Count == 0;
                    break;

                case CooledContainer cc:
                    foreach (var c in containers)
                    {
                        weightOnNewContainer = weightOnNewContainer + c.weight;
                    }
                    if (weightOnNewContainer >= 120000)
                    {
                        if (getHeaviestContainer().weight > _container.weight)
                        {
                            return true;
                        }
                    }

                    ;
                    break;

                default:
                {
                    return false;
                }
            }
        }

        private Container getHeaviestContainer()
        {
            Container heaviestContainer = null;
            foreach (var container in containers)
            {
                if (container is CooledAndValuable || container is ValuableContainer)
                {
                    heaviestContainer = null;
                }
                else
                {
                    if (heaviestContainer == null)
                    {
                        heaviestContainer = container;
                    }
                    else
                    {
                        if (container.weight > heaviestContainer.weight)
                        {
                            heaviestContainer = container;
                        }
                    }
                }
            }

            return heaviestContainer;
        }

        public int Returncontainers()
        {
            return containers.Count;
        }
    }
}
