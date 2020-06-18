using System;


namespace ContainerVervoer
{
    class Program
    {
        private static Ship ship;
        private static ContainerMaker containerMaker;
        private static BalanceAlgorithme balanceAlgorithme;
        static void Main(string[] args)
        {
            var cooledanvaluable = new CooledAndValuable();
            var cooled = new CooledContainer();
            var valuable = new ValuableContainer();
            var normal = new NormalContainer();

            balanceAlgorithme = new BalanceAlgorithme();
            containerMaker = new ContainerMaker(balanceAlgorithme);
            ship = new Ship();
            
            Console.Write("Lengte schip?");
            ship.lenght = Convert.ToInt32(Console.ReadLine());

            Console.Write("Breedte schip?");
            ship.width = Convert.ToInt32(Console.ReadLine());
            balanceAlgorithme.SetShip(ship);

            Console.Write("Hoeveel waardevolle gekoelde containers?");
            containerMaker.CreateContainers(Convert.ToInt32(Console.ReadLine()), cooledanvaluable);
            Console.Write(balanceAlgorithme.cvList.Count.ToString());

            Console.Write("Hoeveel gekoelde containers?");
            containerMaker.CreateContainers(Convert.ToInt32(Console.ReadLine()), cooled);

            Console.Write("Hoeveel waardevolle containers?");
            containerMaker.CreateContainers(Convert.ToInt32(Console.ReadLine()), valuable);

            Console.Write("Hoeveel normale containers?");
            containerMaker.CreateContainers(Convert.ToInt32(Console.ReadLine()), normal);

            Console.Write("Containers plaatsen...");
            balanceAlgorithme.FillShip();
            if (balanceAlgorithme.ValidatePlacement())
            {
                Console.WriteLine("Plaatsing is succesvol!");
            }
            else
            {
                Console.WriteLine("Schip is niet gebalanceerd of te weinig gewicht");
            }

            if (balanceAlgorithme.falseList.Count == 0)
            {
                Console.WriteLine("Alle containers zijn geplaats");
            }
            else
            {
                foreach (var container in balanceAlgorithme.falseList)
                {
                    Console.WriteLine(container.GetType().Name + " " + container.weight.ToString()+ "kon niet worden geplaatst.");
                }
            }


        }
    }
}
