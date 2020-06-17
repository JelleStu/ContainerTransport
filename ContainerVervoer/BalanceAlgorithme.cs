using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Runtime.ExceptionServices;
using System.Text;

namespace ContainerVervoer
{
    public class BalanceAlgorithme
    {
        public List<CooledAndValuable> cvList = new List<CooledAndValuable>();
        public List<CooledContainer> cList = new List<CooledContainer>();
        public List<ValuableContainer> vList = new List<ValuableContainer>();
        public List<NormalContainer> nList = new List<NormalContainer>();
        private List<Container> falseList = new List<Container>();

        public Ship ship;

        public void SetShip(Ship _ship)
        {
            ship = _ship;
        }
        public void FillCvList(CooledAndValuable container)
        {
            cvList.Add(container);
        }
        public void FillCList(CooledContainer container)
        {
            cList.Add(container);
        }
        public void FillVList(ValuableContainer container)
        {
            vList.Add(container);

        }
        public void FillNList(NormalContainer container)
        {
            nList.Add(container);
        }

        private void AddContainerToFalseList(Container _container)
        {
            falseList.Add(_container);
        }

        public void FillShip()
        {
            //First create rows
            CreateRows();
            //Place the cooled and valuable containers
            PlaceCVContainers();
            //Place the cooled containers
            PlaceCContainers();
            //Place the valuable containers
            PlaceVContainers();
            //Place normal containers
            PlaceNContainers();
            //Test placement
            ValidatePlacement();
            //Return potential errors
        }

        //This will create a grid
        public void CreateRows()
        {
            ship.CreateRows();
        }

        
        
        // place cooled containers
        private void PlaceCVContainers()
        {
            //Select the first row
            Row FirstRow = ship.ReturnRow(0);
            foreach (var container in cvList)
            {
                foreach (var stack in FirstRow.Stacks)
                {
                    if (stack.CanAddContainer(container))
                    {
                        stack.AddContainerToStack(container);
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                    AddContainerToFalseList(container);
                    cvList.Remove(container);
                }
            }
        }

        private void PlaceCContainers()
        {
            //Get al list with containers sorted on weight (reverse because it will sort on the lowest container).
            List<CooledContainer> containerSortedWeight = cList.OrderBy(c => c.weight).Reverse().ToList();

            //Get the first row, cooled containers can only be placed on the first row. 0 will always be the first row.
            Row firstRow = ship.ReturnRow(0);

            //Sort the Row stacks on the LOWEST weight, i will place the heaviest container on the lowes weight to compensate.
            ContainerStack stack = SortStacksOnWeight(firstRow.Stacks).FirstOrDefault();

            foreach (var container in containerSortedWeight.ToList())
            {
                if (container != null)
                {
                    if (stack != null && stack.CanAddContainer(container))
                    {
                        if (container.placeLow)
                        {
                            stack.AddContainerToStackLow(container);
                            containerSortedWeight.Remove(container);
                        }
                        else
                        {
                            stack.AddContainerToStack(container);
                            containerSortedWeight.Remove(container);
                        }
                    }
                    else
                    {
                        containerSortedWeight.Remove(container);
                        AddContainerToFalseList(container);
                    }
                }
                stack = SortStacksOnWeight(firstRow.Stacks).FirstOrDefault();
            }
        }

        private void PlaceVContainers()
        {
            //Sort the valuable containers on weight. Reverse to get the heaviest.
            List<ValuableContainer> containerSorted = vList.OrderBy(c => c.weight).Reverse().ToList();

            //Get the last row
            Row lastRow = ship.rows.Last();

            //Get the stack with lowest weight
            List<ContainerStack> sortedStacks = SortStacksOnWeight(lastRow.Stacks);
            ContainerStack stack = sortedStacks.FirstOrDefault();

            foreach (var container in containerSorted.ToList())
            {
                if (container != null)
                {
                    if (stack != null && stack.CanAddContainer(container))
                    {
                        stack.AddContainerToStackHigh(container);
                    }
                    else
                    {
                        //Get first row and stacks
                        Row firstRow = ship.ReturnRow(0);
                        //Check if container can be placed on first row
                        foreach (var frstack in firstRow.Stacks)
                        {
                            if (frstack.CanAddContainer(container))
                            {
                                frstack.AddContainerToStackHigh(container);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        AddContainerToFalseList(container);
                        containerSorted.Remove(container);
                        continue;
                    }
                }
                stack = SortStacksOnWeight(lastRow.Stacks).First();
            }
        }

        private void PlaceNContainers()
        {
            //Sort containers on weight
            List<NormalContainer> sortedContainers = nList.OrderByDescending(c => c.weight).ToList(); 

            //In this fucntion i will get the lightest stack, on this stack I will place the heaviest container.
            List<ContainerStack> stacks = sortContainerStacks();
            
            foreach (var container in sortedContainers.ToList())
            {
                var stack = stacks.First();
                if (stack.CanAddContainer(container))
                {
                    if (container.placeLow)
                    {
                        stack.AddContainerToStackLow(container);
                        sortedContainers.Remove(container);
                    }
                    else
                    {
                        stack.AddContainerToStack(container);
                        sortedContainers.Remove(container);
                    }
                }
                else
                {
                    AddContainerToFalseList(container);
                    sortedContainers.Remove(container);
                }
                stacks = sortContainerStacks();
            }
        }

        private List<ContainerStack> SortStacksOnWeight(List<ContainerStack> containerStacks)
        { 
            List<ContainerStack> SortedStack = new List<ContainerStack>();
            return SortedStack = containerStacks.OrderBy(stack => stack.weight).ToList();
        }

        private List<ContainerStack> sortContainerStacks()
        {
            List<Row> rows = ship.rows;

            List<ContainerStack> sortedContainerStack = new List<ContainerStack>();

            //First get all stacks
            List<ContainerStack> allContainerStacks = rows.SelectMany(row => row.Stacks).ToList();

            //Sort stack on weight
            return sortedContainerStack = allContainerStacks.OrderByDescending(s => s.weight).ToList();
        }

        //In this method I will check the placement, will the ship capsize and is it in balance?
        private bool ValidatePlacement()
        {
            if (ship.CheckForCapSize())
            {
                if()
            }
        }
    }
}
