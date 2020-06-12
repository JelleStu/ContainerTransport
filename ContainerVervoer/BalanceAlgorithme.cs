using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;

namespace ContainerVervoer
{
    public class BalanceAlgorithme
    {
        public List<CooledAndValuable> cvList = new List<CooledAndValuable>();
        public List<CooledContainer> cList = new List<CooledContainer>();
        public List<ValuableContainer> vList = new List<ValuableContainer>();
        public List<NormalContainer> nList = new List<NormalContainer>();

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

        public void FillShip()
        {
            CreateRows();
            PlaceCVContainers();
            PlaceCContainers();
        }

        //This will create a grid
        public void CreateRows()
        {
            ship.CreateRows();
        }

        
        
        // place cooled containers
        public void PlaceCVContainers()
        {
            //Select the first row
            Row FirstRow = ship.ReturnRow(0);
            if (cvList.Count <= FirstRow.Stacks.Count)
            {
                for (int i = 0; i < cvList.Count; i++)
                {
                    
                    FirstRow.AddContainerToStack(cvList[i], i);
                }
            }
            else
            {
                
            }
        }

        public void PlaceCContainers()
        {
            List<CooledContainer> SortedOnWeight = cList.OrderBy(c => c.weight).Reverse().ToList();

            Row firstRow = ship.ReturnRow(0);
            foreach (var stack in firstRow.Stacks)
            {
                foreach (var c in SortedOnWeight)
                {
                    if (stack.CanAddContainer(c))
                    {
                        if (c.placeLow == true)
                        {
                            stack.AddContainerToStackLow(c);
                        }
                        stack.AddContainerToStack(c);
                    }
                }
            }
            
        }

    }
}
