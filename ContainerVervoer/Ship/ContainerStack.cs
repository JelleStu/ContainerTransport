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
        public int stackNumber;
        public int weight { get; set; }

        public ContainerStack(int _stackNumber)
        {
            stackNumber = _stackNumber;
            containers = new List<Container>();
        }

        public void AddContainerToStack(Container _container)
        {
            if (containers.Count == 0 || containers.Count == 1)
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
            switch (_container.GetType().Name)
            {
                case nameof(CooledAndValuable):
                    if (containers.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case nameof(CooledContainer):
                    if (weight < 120000)
                    {
                        int heaviestcontainer = GetHeaviestContainer();
                        if (heaviestcontainer < _container.weight)
                        {
                            _container.placeLow = true;
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                case nameof(ValuableContainer):
                    if (containers.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return containers.Last().canHaveContainerOnTop;
                    }
                case nameof(NormalContainer):
                    if (containers.Count == 0)
                    {
                        return true;
                    }
                    else
                    {
                        weightOnNewContainer += containers.Sum(c => c.weight);
                        if (weightOnNewContainer < 120000)
                        {
                            int heaviestcontainer = GetHeaviestContainer();
                            if (heaviestcontainer < _container.weight)
                            {
                                _container.placeLow = true;
                                return true;
                            }
                            else
                            {
                                return true;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                default:
                    {
                        return false;
                    }
            }
        }

        private int GetHeaviestContainer()
        {
            int heaviestContainer = 0;
            foreach (var container in containers)
            {
                if (container is CooledAndValuable || container is ValuableContainer)
                {
                    continue;
                }
                else
                {
                    if (heaviestContainer == 0)
                    {
                        heaviestContainer = container.weight;
                    }
                    else
                    {
                        if (container.weight < heaviestContainer)
                        {
                            heaviestContainer = container.weight;
                        }
                    }
                }
            }
            return heaviestContainer;
        }

    }
}
