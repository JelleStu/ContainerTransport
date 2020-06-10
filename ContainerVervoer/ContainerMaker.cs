using System;
using System.Collections.Generic;
using System.Text;

namespace ContainerVervoer
{
    public class ContainerMaker
    {
        private int i = 0;
        private BalanceAlgorithme balanceAlgorithm;

        public ContainerMaker(BalanceAlgorithme _balanceAlgorithm)
        {
            balanceAlgorithm = _balanceAlgorithm;
        }

        public void CreateContainers<Container>(int quanity, Container _container)
        {
            while (i < quanity)
            {
                if (_container is CooledAndValuable)
                {
                    var container = new CooledAndValuable(randomSize(), false);
                    balanceAlgorithm.FillCvList(container);
                }
                else if (_container is CooledContainer)
                {
                    var container = new CooledContainer(randomSize(), true);
                    balanceAlgorithm.FillCList(container);
                }
                else if (_container is ValuableContainer)
                {
                    var container = new ValuableContainer(randomSize(), false);
                    balanceAlgorithm.FillVList(container);
                }
                else if (_container is NormalContainer)
                {
                    var container = new NormalContainer(randomSize(), true);
                    balanceAlgorithm.FillNList(container);
                }

                i++;
            }
        }

        private int randomSize()
        {
            var r = new Random();
            return r.Next(4000, 30000);
        }
    }
}
