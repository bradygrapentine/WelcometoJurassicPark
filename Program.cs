using System;
using System.Linq;
using System.Collections.Generic;
namespace WelcomeToJurassicPark
{
    class Dinosaur
    {
        public string Name { get; set; }
        public string DietType { get; set; }
        public double Weight { get; set; }
        public int EnclosureNumber { get; set; }
        public DateTime WhenAcquired { get; set; } = DateTime.Now;
        public string Description()
        {
            return $"There's a dinosaur named {Name} at the park. {Name} is a {DietType} that weighs {Weight} pounds. {Name} is housed in Enclosure {EnclosureNumber} and was acquired on {WhenAcquired.ToString("MM/dd/yyyy")}.";
        }
    }
    class Program
    {
        static string PromptForString(string prompt)
        {
            Console.Write(prompt);
            var userInput = Console.ReadLine();
            return userInput;
        }

        static int PromptForInteger(string prompt)
        {
            Console.Write(prompt);
            int userInput;
            var isThisGoodInput = Int32.TryParse(Console.ReadLine(), out userInput);

            if (isThisGoodInput)
            {
                return userInput;
            }
            else
            {
                Console.WriteLine("Sorry, that isn't a valid input, I'm using 0 as your answer.");
                return 0;
            }
        }
        static string Menu()
        {
            Console.WriteLine();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("(V)iew all dinosaurs in the park");
            Console.WriteLine("(Vi)ew dinosaurs by enclosure number");
            Console.WriteLine("(A)dd a dinosaur to the park");
            Console.WriteLine("(R)emove a dinosaur from the park");
            Console.WriteLine("(T)ransfer a dinosaur to another enclosure");
            Console.WriteLine("(D)isplay summary of in-house dinosaurs");
            Console.WriteLine("(Di)splay dinosaurs acquired after date");
            Console.WriteLine("(Q)uit the application");
            var choice = Console.ReadLine().ToUpper();
            return choice;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("------------------------------------------------------------------------------------");
            Console.WriteLine(" Welcome to the Dino Database. Hope you still have fingers. You're gonna need them. ");
            Console.WriteLine("------------------------------------------------------------------------------------");

            var dinoList = new List<Dinosaur>();

            var keepGoing = true;
            while (keepGoing)
            {
                var choice = Menu();
                switch (choice)
                {
                    case "V":
                        Console.Clear();
                        Console.WriteLine("");
                        if (dinoList.Count > 0)
                        {
                            var byDateAcquired = dinoList.OrderBy(dino => dino.WhenAcquired);
                            Console.WriteLine("Displaying All Dinosaurs in Database (in the order they were acquired):");
                            Console.WriteLine();
                            foreach (var dino in byDateAcquired)
                            {
                                Console.WriteLine(dino.Description());
                            }
                        }
                        else
                        {

                            Console.WriteLine("There are no dinosaurs in the database!");
                        }
                        break;
                    case "A":
                        Console.Clear();
                        Console.WriteLine();
                        var newDino = new Dinosaur();
                        newDino.Name = PromptForString("What is the new dinosaur's name? ");
                        newDino.DietType = PromptForString($"Is {newDino.Name} an herbivore or a carnivore? ").ToLower();
                        newDino.Weight = PromptForInteger($"How much does {newDino.Name} weigh (in pounds)? ");
                        newDino.EnclosureNumber = PromptForInteger($"What enclosure will {newDino.Name} be living in? ");
                        dinoList.Add(newDino);
                        Console.WriteLine();
                        Console.WriteLine($"{newDino.Name} has been added to the database!");
                        break;
                    case "R":
                        Console.Clear();
                        Console.WriteLine("");
                        var dinoToRemoveName = PromptForString("What dinosaur are you trying to remove? ");
                        Dinosaur dinoToRemove = dinoList.FirstOrDefault(dino => dino.Name == dinoToRemoveName);
                        Console.WriteLine("");
                        if (dinoToRemove == null)
                        {
                            Console.WriteLine($"There are no dinosaurs named {dinoToRemoveName} in the database!");
                        }
                        else
                        {
                            dinoList.Remove(dinoToRemove);
                            Console.WriteLine($"{dinoToRemove.Name} has been removed from the database!");
                        }
                        break;
                    case "T":
                        Console.Clear();
                        Console.WriteLine("");
                        var dinoToTransferName = PromptForString("What dinosaur are you trying to transfer? ");
                        Dinosaur dinoToTransfer = dinoList.FirstOrDefault(dino => dino.Name == dinoToTransferName);
                        Console.WriteLine("");
                        if (dinoToTransfer == null)
                        {
                            Console.WriteLine($"There are no dinosaurs named {dinoToTransferName} in the database!");
                        }
                        else
                        {
                            var newEnclosureNumber = PromptForInteger($"What enclosure will {dinoToTransfer.Name} be transferred to? ");
                            dinoToTransfer.EnclosureNumber = newEnclosureNumber;
                            Console.WriteLine();
                            Console.WriteLine($"{dinoToTransfer.Name} has been transferred to Enclosure {dinoToTransfer.EnclosureNumber}!");
                        }
                        break;
                    case "D":
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("Displaying Summary:");
                        var numHerbivores = dinoList.Count(dino => dino.DietType == "carnivore");
                        var numCarnivores = dinoList.Count(dino => dino.DietType == "herbivore");
                        if (numHerbivores == 1)
                        {
                            Console.WriteLine($"There is {numHerbivores} herbivore in the park!");
                        }
                        else
                        {
                            Console.WriteLine($"There are {numHerbivores} herbivores in the park!");
                        }
                        if (numCarnivores == 1)
                        {
                            Console.WriteLine($"There is {numCarnivores} carnivore in the park!");
                        }
                        else
                        {
                            Console.WriteLine($"There are {numCarnivores} carnivores in the park!");
                        }
                        break;
                    case "Q":
                        Console.Clear();
                        Console.WriteLine("");
                        keepGoing = false;
                        break;
                    case "DI":
                        Console.Clear();
                        Console.WriteLine("");
                        Console.WriteLine("Enter desired date in the following format: Jan 1, 2009");
                        string dateInput = Console.ReadLine();
                        Console.WriteLine("");
                        var parsedDate = DateTime.Parse(dateInput);
                        var dinosAcquiredAfterDate = dinoList.Where(dino => dino.WhenAcquired > parsedDate);
                        if (dinosAcquiredAfterDate.Count() > 0)
                        {
                            Console.WriteLine("Displaying Dinosaurs Acquired After Date:");
                            Console.WriteLine("");
                            foreach (var dino in dinosAcquiredAfterDate)
                            {
                                Console.WriteLine(dino.Description());
                            }
                        }
                        else
                        {
                            Console.WriteLine($"No dinosaurs were acquired after {dateInput}!");
                        }
                        break;
                    case "VI":
                        Console.Clear();
                        Console.WriteLine("");
                        Console.Write("What enclosure would you like to view? ");
                        string enclosureToBeViewed = Console.ReadLine();
                        int enclosureToBeViewedAsInt;
                        var isThisGoodInput = int.TryParse(enclosureToBeViewed, out enclosureToBeViewedAsInt);
                        Console.WriteLine("");
                        var dinosInEnclosure = dinoList.Where(dino => dino.EnclosureNumber == enclosureToBeViewedAsInt);
                        if (isThisGoodInput && dinosInEnclosure.Count() > 0)
                        {
                            Console.WriteLine($"Displaying Dinosaurs in Enclosure {enclosureToBeViewedAsInt}:");
                            Console.WriteLine("");
                            foreach (var dino in dinosInEnclosure)
                            {
                                Console.WriteLine($"There's a dinosaur named {dino.Name} housed in Enclosure {dino.EnclosureNumber}. {dino.Name} is a {dino.DietType} that weighs {dino.Weight} pounds. {dino.Name} was acquired on {dino.WhenAcquired.ToString("MM/dd/yyyy")}.");
                                Console.WriteLine();
                            }
                        }
                        else
                        {
                            Console.WriteLine($"There are no dinosaurs in Enclosure {enclosureToBeViewed}!");
                        }
                        break;
                }
            }
        }
    }
}
