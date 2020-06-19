using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContainerVervoer;
namespace ContainerVervoer.Test
{
    [TestClass]
    public class ValidateTests
    {
        private BalanceAlgorithme balanceAlgorithme;
        private Ship ship;
        private ContainerStack stack;
        [TestInitialize]
        public void Initialize()
        {
            balanceAlgorithme = new BalanceAlgorithme();
            stack = new ContainerStack(0);
        }

        //In this test I will try to place 2 valuable containers to 1 stack. This cannot happen.
        [TestMethod]
        public void Test_Place_TwoValuableContainers_To_Stack()
        {
            //Arrange
            //First we need a container stack and 2 valuable containers. The weight doesn't matter, you can only place
            //1 valuable container on a stack.
            ValuableContainer container1 = new ValuableContainer(250);
            ValuableContainer container2 = new ValuableContainer(250);
            bool bcontainer1 = false;
            bool bcontainer2 = false;

            //Act
            if (stack.CanAddContainer(container1))
            {
                stack.AddContainerToStack(container1);
                bcontainer1 = true;
            }
            if (stack.CanAddContainer(container2))
            {
                stack.AddContainerToStack(container2);
                bcontainer2 = true;
            }

            //Assert
            //The first bool should be true, the second false.
            Assert.AreEqual(true, bcontainer1);
            Assert.AreEqual(false, bcontainer2);
        }

        [TestMethod]
        public void Test_Place_Valuable_On_CooledAndValuable()
        {
            //In this test method we will test if we can place a valuable container on top of cooled & valuable container
            CooledAndValuable container1 = new CooledAndValuable(250);
            ValuableContainer container2 = new ValuableContainer(250);
            bool bcontainer1 = false;
            bool bcontainer2 = false;

            //Act we place the cooledvalueble first.
            if (stack.CanAddContainer(container1))
            {
                stack.AddContainerToStack(container1);
                bcontainer1 = true;
            }

            if (stack.CanAddContainer(container2))
            {
                 stack.AddContainerToStack(container2);
                 bcontainer2 = true;
            }

            //Assert
            Assert.AreEqual(true, bcontainer1);
            Assert.AreEqual(false, bcontainer2);
        }

        [TestMethod]
        public void Place_HeaviestContainer_OnBottom()
        {
            //In this test I will test I can place the heaviest container on the bottom I will need 2 containers
            List<NormalContainer> listContainer = new List<NormalContainer>();
            NormalContainer container1 = new NormalContainer(250);
            NormalContainer container2 = new NormalContainer(300);
            ship = new Ship();
            ship.width = 1;
            ship.length = 1;
            balanceAlgorithme.SetShip(ship);
            listContainer.Add(container1);
            listContainer.Add(container2);
            balanceAlgorithme.nList = listContainer;

            //Act
            balanceAlgorithme.FillShip();
            int containerUnder = ship.rows[0].Stacks[0].containers[0].weight;
            int containerAbove = ship.rows[0].Stacks[0].containers[1].weight;

            //Arrange
            Assert.AreEqual(container2.weight, containerUnder);
            Assert.AreEqual(container1.weight, containerAbove);
        }

        
    }
}

