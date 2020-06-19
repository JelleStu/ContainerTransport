using System.Collections.Generic;
using System.Security.Cryptography;
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
            ship = new Ship();
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
        public void Place_ValuableAndCooled()
        {
            //In this test I test if i have 1 row but 2 containers the container will be added to the falselist
            List<CooledAndValuable> listContainers = new List<CooledAndValuable>();
            CooledAndValuable container1 = new CooledAndValuable(250);
            CooledAndValuable container2 = new CooledAndValuable(250);
            ship.width = 1;
            ship.length = 2;
            balanceAlgorithme.SetShip(ship);
            listContainers.Add(container1);
            listContainers.Add(container2);
            balanceAlgorithme.cvList = listContainers;

            //Act
            balanceAlgorithme.FillShip();

            //Assert
            Assert.AreEqual(container2, balanceAlgorithme.falseList[0]);
        }

        [TestMethod]
        public void Place_HeaviestContainer_OnBottom()
        {
            //In this test I will test I can place the heaviest container on the bottom I will need 2 containers
            List<NormalContainer> listContainer = new List<NormalContainer>();
            NormalContainer container1 = new NormalContainer(250);
            NormalContainer container2 = new NormalContainer(300);
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

        [TestMethod]
        public void Max_Weight_Test()
        {
            // In this test I will test my algorithm will not place the container if the weight is above 120 ton
            List<NormalContainer> listContainer = new List<NormalContainer>();
            NormalContainer container1 = new NormalContainer(120001);
            NormalContainer container2 = new NormalContainer(250);
            ship.width = 2;
            ship.length = 1;
            balanceAlgorithme.SetShip(ship);
            listContainer.Add(container1);
            listContainer.Add(container2);
            balanceAlgorithme.nList = listContainer;

            //Act
            balanceAlgorithme.FillShip();
            int row1 = ship.rows[0].Stacks[0].weight;
            int row2 = ship.rows[0].Stacks[1].weight;

            //Assert
            Assert.AreEqual(120001, row1);
            Assert.AreEqual(250, row2);
        }

        [TestMethod]
        public void ValuableContainers_placement()
        {
            //in this test I will test if the containers are placed on the first and last row only! To test this we will need 4 containers and 3 rows.
            List<ValuableContainer> listContainer = new List<ValuableContainer>();
            ValuableContainer container1 = new ValuableContainer(250);
            ValuableContainer container2 = new ValuableContainer(250);
            ValuableContainer container3 = new ValuableContainer(250);
            ValuableContainer container4 = new ValuableContainer(250);
            ship.width = 2;
            ship.length = 3;
            balanceAlgorithme.SetShip(ship);
            listContainer.Add(container1);
            listContainer.Add(container2);
            listContainer.Add(container3);
            listContainer.Add(container4);
            balanceAlgorithme.vList = listContainer;

            //act 
            balanceAlgorithme.FillShip();
            
            //Assert
            Assert.AreEqual(250, ship.rows[0].Stacks[0].weight);
            Assert.AreEqual(250, ship.rows[0].Stacks[1].weight);
            Assert.AreEqual(0, ship.rows[1].Stacks[0].weight);
            Assert.AreEqual(0, ship.rows[1].Stacks[1].weight);
            Assert.AreEqual(250, ship.rows[2].Stacks[0].weight);
            Assert.AreEqual(250, ship.rows[2].Stacks[1].weight);
        }

        [TestMethod]
        public void CooledContainers_Placement()
        {
            //In this test I will test if the cooled containers are placed only on the first row. 4 containers, 2 rows.
            List<CooledContainer> listContainers = new List<CooledContainer>();
            CooledContainer container1 = new CooledContainer(250);
            CooledContainer container2 = new CooledContainer(250);
            CooledContainer container3 = new CooledContainer(250);
            CooledContainer container4 = new CooledContainer(250);
            ship.width = 2;
            ship.length = 2;
            balanceAlgorithme.SetShip(ship);
            listContainers.Add(container1);
            listContainers.Add(container2);
            listContainers.Add(container3);
            listContainers.Add(container4);
            balanceAlgorithme.cList = listContainers;

            //Act
            balanceAlgorithme.FillShip();

            //Assert
            Assert.AreEqual(500, ship.rows[0].Stacks[0].weight);
            Assert.AreEqual(500, ship.rows[0].Stacks[1].weight);
            Assert.AreEqual(0, ship.rows[1].Stacks[0].weight);
            Assert.AreEqual(0, ship.rows[1].Stacks[1].weight);
        }

        [TestMethod]
        public void Check_ShipWeight_False()
        {
            //In this test I will check the weight check
            List<NormalContainer> listContainers = new List<NormalContainer>();
            NormalContainer container1 = new NormalContainer(250);
            NormalContainer container2 = new NormalContainer(250);
            ship.width = 2;
            ship.length = 1;
            balanceAlgorithme.SetShip(ship);
            listContainers.Add(container1);
            listContainers.Add(container2);
            balanceAlgorithme.nList = listContainers;

            //Act
            balanceAlgorithme.FillShip();
            

            //Assert
            Assert.AreEqual(false, ship.CheckForCapSize());
        }

        [TestMethod]
        public void Check_ShipWeight_True()
        {
            //In this test I will check the weight check.
            List<NormalContainer> listContainers = new List<NormalContainer>();
            NormalContainer container1= new NormalContainer(60000);
            NormalContainer container2 = new NormalContainer(60000);
            ship.width = 2;
            ship.length = 1;
            balanceAlgorithme.SetShip(ship);
            listContainers.Add(container1);
            listContainers.Add(container2);
            balanceAlgorithme.nList = listContainers;

            //Act
            balanceAlgorithme.FillShip();

            //Assert
            Assert.AreEqual(true, ship.CheckForCapSize());
        }

        [TestMethod]
        public void Check_ShipBalance_False()
        {
            //In this method I will check the ship balance.
            List<NormalContainer> listContainers = new List<NormalContainer>();
            NormalContainer container1 = new NormalContainer(60000);
            NormalContainer container2 = new NormalContainer(60000);
            NormalContainer container3 = new NormalContainer(60000);
            ship.width = 2;
            ship.length = 1;
            balanceAlgorithme.SetShip(ship);
            listContainers.Add(container1);
            listContainers.Add(container2);
            balanceAlgorithme.nList = listContainers;

            //Act
            balanceAlgorithme.FillShip();
            //Now we insert a container on the right side, this will trigger a unbalanced ship.
            ship.rows[0].Stacks[1].AddContainerToStack(container3);
            ship.CheckBalance();
            //Assert
            Assert.AreEqual(false, ship.IsInBalance);
        }
        
        [TestMethod]
        public void Check_ShipBalance_True()
        {
            //In this method I will check the ship balance.
            List<NormalContainer> listContainers = new List<NormalContainer>();
            NormalContainer container1 = new NormalContainer(60000);
            NormalContainer container2 = new NormalContainer(60000);
            NormalContainer container3 = new NormalContainer(60000);
            NormalContainer container4 = new NormalContainer(60000);
            ship.width = 2;
            ship.length = 2;
            balanceAlgorithme.SetShip(ship);
            listContainers.Add(container1);
            listContainers.Add(container2);
            balanceAlgorithme.nList = listContainers;

            //Act
            balanceAlgorithme.FillShip();
            ship.CheckBalance();
            //Assert
            Assert.AreEqual(true, ship.IsInBalance);
        }
    }
}

