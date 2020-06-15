using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;

namespace ContainerVervoer
{
    public class ContainerStack
    {
        public List<Container> containers { get; set; }
        public int x;
        public int stackNumber;
        public int weight { get; set; }

        public ContainerStack(int _x, int _stackNumber)
        {
            x = _x;
            stackNumber = _stackNumber;
            containers = new List<Container>();
        }

        public void AddContainerToStack(Container _container)
        {
            if (containers.Count == 0)
            {
                containers.Add(_container);
            }
            else
            {
                int position = containers.Count - 1;
                containers.Insert(position, _container);
            }
            weight += _container.weight;
        }

        public void AddContainerToStackLow(Container _container)
        {
            containers.Insert(0, _container);
            weight += _container.weight;
        }
        
        public void AddContainerToStackHigh(Container _container)
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
                    if (containers.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case CooledContainer cc:
                    weightOnNewContainer += containers.Sum(c => c.weight);
                    if (weightOnNewContainer >= 120000) return false;
                    if (getHeaviestContainer() > _container.weight || getHeaviestContainer() == 0)
                    {
                        _container.placeLow = true;
                        return true;
                    }
                    return false;
                    break;
                case ValuableContainer vc:
                    if (containers.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return containers.Last().canHaveContainerOnTop;
                    }
                    break;

                case NormalContainer nc:
                    if (containers.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        weightOnNewContainer += containers.Sum(c => c.weight);
                        if (weightOnNewContainer < 120000)
                        {
                            if (getHeaviestContainer() > _container.weight || getHeaviestContainer() == 0)
                            {
                                _container.placeLow = true;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    break;
                default:
                {
                    return false;
                }
            }
        }

        private int getHeaviestContainer()
        {
            int heaviestContainer = 0;
            foreach (var container in containers)
            {
                if (container is CooledAndValuable || container is ValuableContainer)
                {
                    heaviestContainer = 0;
                }
                else
                {
                    if (heaviestContainer == 0)
                    {
                        heaviestContainer = container.weight;
                    }
                    else
                    {
                        if (container.weight > heaviestContainer)
                        {
                            heaviestContainer = container.weight;
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
