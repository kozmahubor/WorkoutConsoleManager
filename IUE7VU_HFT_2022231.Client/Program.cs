using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using ConsoleTools;
using IUE7VU_HFT_2022231.Models;
using static IUE7VU_HFT_2022231.Models.Enum;

namespace IUE7VU_HFT_2022231.Client
{
    internal class Program
    {
        static RestService rest;
        static readonly string line = "\n-----------------------------------------------------";
        #region Crud
        static void Create(string entity)
        {
            if (entity == "Membership")
            {
                Membership membershipCreated = new Membership();
                Console.Write("Add a new membership!\nEnter a Memberships type:" +
                    "\nDaily = 0\nWeekly = 1\nMonthly = 2\n");
                membershipCreated.MembershipType = (MembershipTypes)int.Parse(Console.ReadLine());
                Console.Write("Enter a Memberships colour:" +
                    "\nRed = 0\nGreen = 1\nBlue = 2\nPink = 3\n");
                membershipCreated.MembershipTicketColour = (MembershipTicketColours)int.Parse(Console.ReadLine());
                Console.WriteLine("Enter person id:");
                rest.Post(membershipCreated, $"/membership/{int.Parse(Console.ReadLine())}");
            }
            else if (entity == "Trainer")
            {
                Trainer trainerCreated = new Trainer();
                Console.WriteLine("Enter trainers name:\n");
                trainerCreated.TrainerName = Console.ReadLine();
                Console.WriteLine("Enter trainers age:\n");
                trainerCreated.TrainerAge = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter trainers gender:" +
                    "\nMale = 0\nFemale = 1\n");
                trainerCreated.TrainerGender = (Gender)int.Parse(Console.ReadLine());
                rest.Post(trainerCreated, "/trainer");
            }
            else if (entity == "Person")
            {
                Person personCreated = new Person();
                Console.WriteLine("Enter your name!\n");
                personCreated.PersonName = Console.ReadLine();
                Console.WriteLine("Enter your age!\n");
                personCreated.PersonAge = int.Parse(Console.ReadLine());
                Console.WriteLine("Add you gender:" +
                    "\nMale = 0\nFemale = 1\n");
                personCreated.PersonGender = (Gender)int.Parse(Console.ReadLine());
                Console.WriteLine("Chose a trainer by ID:\n");
                foreach (var item in rest.Get<Trainer>("/trainer"))
                {
                    Console.WriteLine($"Trainer id: {item.TrainerId}" +
                                      $"\nTrainer name: {item.TrainerName}");
                }
                personCreated.TrainerId = int.Parse(Console.ReadLine());
                rest.Post(personCreated, "/person");
            }
            else if (entity == "Workout")
            {
                Workout workoutCreated = new Workout();

                Console.WriteLine("Enter the muscle you trained(only 1):" +
                    "\nChest = 0\nBack = 1\nShoulders = 2\nLegs = 3\nBiceps = 4\nTriceps = 5\n");
                workoutCreated.MuscleTypes = (MuscleTypes)int.Parse(Console.ReadLine());
                workoutCreated.WorkoutDay = DateTime.Today;
                Console.WriteLine("Enter the time you spent lifting:\n");
                workoutCreated.WorkoutTime_Weights = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter the time you spent doing cardio:\n");
                workoutCreated.WorkoutTime_Cardio = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter the difficulty of your workout:" +
                    "\nEasy = 0\nMedium = 1\nHard = 2\nExtreme = 3\n");
                workoutCreated.WorkoutDifficulty = (WorkoutDifficulty)int.Parse(Console.ReadLine());
                Console.WriteLine("Enter person id:");
                rest.Post(workoutCreated, $"/workout/{int.Parse(Console.ReadLine())}");
            }
            else
            {
            }
            Console.WriteLine($"\n{entity.ToUpper()} CREATED!");
            Console.Write("Press Enter to contunie...");
            Console.ReadLine();
        }
        static void List(string entity)
        {
            Console.WriteLine($"List of [{entity}]");
            if (entity == "Membership")
            {
                List<Membership> memberships = rest.Get<Membership>("/membership");
                foreach (var item in memberships)
                {
                    Console.WriteLine($"\nPerson ID: {item.PersonId}" +
                                      $"\nMembership ID: {item.MembershipId}" +
                                      $"\nMembership type: {item.MembershipType}" +
                                      $"\nMembership begins: {item.MembershipDurationBegin:MM/dd/yyyy}" +
                                      $"\nMembership ends: {item.MembershipDurationEnd:MM/dd/yyyy}" +
                                      $"\nMembership ticket colour: {item.MembershipTicketColour}{line}{line}");
                }
            }
            else if (entity == "Workout")
            {
                List<Workout> workouts = rest.Get<Workout>("/workout");
                foreach (var item in workouts)
                {
                    Console.WriteLine($"\nPerson ID: {item.PersonId}" +
                                      $"\nWorkout ID: {item.WorkoutId}" +
                                      $"\nMuscle trained: {item.MuscleTypes}" +
                                      $"\nDay of workout: {item.WorkoutDay}" +
                                      $"\nTime spent lifting: {item.WorkoutTime_Weights}" +
                                      $"\nTime spent doing cardio: {item.WorkoutTime_Cardio}" +
                                      $"\nWorkout difficult:{item.WorkoutDifficulty}{line}{line}");
                }
            }
            else if (entity == "Person")
            {
                List<Person> person = rest.Get<Person>("/person");
                foreach (var item in person)
                {
                    Console.WriteLine($"Person ID: {item.PersonId}" +
                                      $"\nPerson name: {item.PersonName}" +
                                      $"\nPerson age: {item.PersonAge}" +
                                      $"\nPerson gender: {item.PersonGender}" +
                                      $"\nPerson trainer: {item.Trainer.TrainerId}" +
                                      $"\nPerson trainer name: {item.Trainer.TrainerName}{line}{line}");
                }
            }
            else if (entity == "Trainer")
            {
                List<Trainer> trainers = rest.Get<Trainer>("/trainer");
                foreach (var item in trainers)
                {
                    Console.WriteLine($"Person ID: {item.TrainerId}" +
                                      $"\nTrainer name: {item.TrainerName}" +
                                      $"\nTrainer age: {item.TrainerAge}" +
                                      $"\nTrainer gender: {item.TrainerGender}{line}{line}");
                }
            }
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
        static void Update(string entity)
        {
            Console.Write($"Enter a {entity}'s ID to update: ");
            int id = int.Parse(Console.ReadLine());
            if (entity == "Membership")
            {
                Membership old = rest.Get<Membership>(id, "/membership");
                Console.Write($"Set New memberhip type [old: {old.MembershipType}]:" +
                              $"\nEnter a Memberships type:" +
                              $"\nDaily = 0\nWeekly = 1\nMonthly = 2\n");
                old.MembershipType = (MembershipTypes)int.Parse(Console.ReadLine());
                Console.Write($"Set New Colour [old: {old.MembershipTicketColour}]:" +
                    $"\nRed = 0\nGreen = 1\nBlue = 2\nPink = 3\n");
                old.MembershipTicketColour = (MembershipTicketColours)int.Parse(Console.ReadLine());

                rest.Put(old, $"/membership/{id}");
            }
            else if (entity == "Trainer")
            {
                Trainer old = rest.Get<Trainer>(id, "/trainer");
                Console.Write($"Set new name [old: {old.TrainerName}]:");
                old.TrainerName = Console.ReadLine();
                Console.Write($"Set new age [old: {old.TrainerAge}]:\n");
                old.TrainerAge = int.Parse(Console.ReadLine());
                Console.Write($"Set new gender [old: {old.TrainerGender}]:\nMale = 0\nFemale = 1\n");
                old.TrainerGender = (Gender)int.Parse(Console.ReadLine());

                rest.Put(old, $"/trainer/{id}");
            }
            else if (entity == "Person")
            {
                Person old = rest.Get<Person>(id, "/person");
                Console.Write($"Set new name [old: {old.PersonName}]:\n");
                old.PersonName = Console.ReadLine();
                Console.Write($"Set new age [old: {old.PersonAge}]:\n");
                old.PersonAge = int.Parse(Console.ReadLine());
                Console.Write($"Set new gender [old: {old.PersonGender}]:\nMale = 0\nFemale = 1\n");
                old.PersonGender = (Gender)int.Parse(Console.ReadLine());
                Console.WriteLine($"Set new trainer ID [old: {old.TrainerId}]:");
                foreach (var item in rest.Get<Trainer>("/trainer"))
                {
                    Console.WriteLine($"Trainer id: {item.TrainerId}" +
                                      $"\nTrainer name: {item.TrainerName}");
                }
                old.TrainerId = int.Parse(Console.ReadLine());
                rest.Put(old, $"/person/{id}");
            }
            else if (entity == "Workout")
            {
                Workout old = rest.Get<Workout>(id, "/workout");
                Console.Write($"New Workout's type: [old: {old.MuscleTypes}]:\n" +
                    $"Chest = 0\n" +
                    $"Back = 1\n" +
                    $"Shoulders = 2\n" +
                    $"Legs = 3\n" +
                    $"Biceps = 4\n" +
                    $"Triceps = 5\n");
                MuscleTypes muscle = (MuscleTypes)int.Parse(Console.ReadLine());

                Console.Write($"Enter new date: [old: {old.WorkoutDay}]:\n");
                old.WorkoutDay = DateTime.Parse(Console.ReadLine()).Date;

                Console.WriteLine($"New Workout's lift duration (hour):\n");
                old.WorkoutTime_Weights = double.Parse(Console.ReadLine());

                Console.WriteLine($"New Workout's cardio duration (hour):\n");
                old.WorkoutTime_Cardio = double.Parse(Console.ReadLine());

                Console.WriteLine($"New Workout's difficulty:" +
                    $"\nEasy = 0\nMedium = 1\nHard = 2\nExtreme = 3\n");
                old.WorkoutDifficulty = (WorkoutDifficulty)int.Parse(Console.ReadLine());

                rest.Put(old, $"/workout/{id}");
            }
            else { }

            Console.WriteLine($"{entity.ToUpper()} UPDATED!");
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        static void Delete(string entity)
        {
            Console.Write($"Enter a {entity}'s id to delete: ");
            int id = int.Parse(Console.ReadLine());


            rest.Delete(id, "/" + entity.ToLower());

            Console.WriteLine($"\n{entity.ToUpper()} DELETED!");
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
        #endregion

        #region non-crud

        static void GetPeopleWithMonthMemberships()
        {
            List<Person> person = rest.Get<Person>("/person/monthmemberships");
            foreach (var item in person)
            {
                Console.WriteLine($"Person ID: {item.PersonId}" +
                                  $"\nPerson name: {item.PersonName}" +
                                  $"\nPerson age: {item.PersonAge}" +
                                  $"\nPerson gender: {item.PersonGender}{line}{line}");
            }
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
        static void GetPopularTrainers()
        {
            List<Trainer> trainers = rest.Get<Trainer>("/trainer/popular");
            foreach (var item in trainers)
            {
                Console.WriteLine($"Person ID: {item.TrainerId}" +
                                  $"\nTrainer name: {item.TrainerName}" +
                                  $"\nTrainer age: {item.TrainerAge}" +
                                  $"\nTrainer gender: {item.TrainerGender}{line}{line}");
            }
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
        static void GetPersonWithMostWorkouts()
        {
            List<Person> people = rest.Get<Person>("/person/mostworkouts");
            foreach (var item in people)
            {
                Console.WriteLine($"Person ID: {item.PersonId}" +
                                  $"\nPerson name: {item.PersonName}" +
                                  $"\nPerson age: {item.PersonAge}" +
                                  $"\nPerson gender: {item.PersonGender}" +
                                  $"\nPerson count: {item.Workouts.Count()}" +
                                  $"\nPerson workout: {string.Join("\n", item.Workouts)} {line}{line}");
            }
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
        static void GetPeopleWithExtremeWorkouts()
        {
            List<Person> person = rest.Get<Person>("/person/extremeworkouts");
            foreach (var item in person)
            {
                Console.WriteLine($"Person ID: {item.PersonId}" +
                                  $"\nPerson name: {item.PersonName}" +
                                  $"\nPerson age: {item.PersonAge}" +
                                  $"\nPerson gender: {item.PersonGender}{line}{line}");
            }
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
        static void GetPersonsTrainerWithRedColourMembership()
        {
            List<Trainer> trainers = rest.Get<Trainer>("/trainer/clientswithredmembership");
            foreach (var item in trainers)
            {
                Console.WriteLine($"Person ID: {item.TrainerId}" +
                                  $"\nTrainer name: {item.TrainerName}" +
                                  $"\nTrainer age: {item.TrainerAge}" +
                                  $"\nTrainer gender: {item.TrainerGender}{line}{line}");
            }
            Console.Write("\nPress Enter to continue...");
            Console.ReadLine();
        }
        

        #endregion
        static void Main(string[] args)
        {
            rest = new RestService("http://localhost:22315", "/swagger/index.html");


            var membershipSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Membership"))
                .Add("Create", () => Create("Membership"))
                .Add("Delete", () => Delete("Membership"))
                .Add("Update", () => Update("Membership"))
                .Add("Return", ConsoleMenu.Close);

            var workoutSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Workout"))
                .Add("Create", () => Create("Workout"))
                .Add("Delete", () => Delete("Workout"))
                .Add("Update", () => Update("Workout"))
                .Add("Return", ConsoleMenu.Close);

            var personSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Person"))
                .Add("Create", () => Create("Person"))
                .Add("Delete", () => Delete("Person"))
                .Add("Update", () => Update("Person"))
                .Add("Return", ConsoleMenu.Close);

            var trainerSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List", () => List("Trainer"))
                .Add("Create", () => Create("Trainer"))
                .Add("Delete", () => Delete("Trainer"))
                .Add("Update", () => Update("Trainer"))
                .Add("Return", ConsoleMenu.Close);

            var nonCrudSubMenu = new ConsoleMenu(args, level: 1)
                .Add("List every person with month memberships\n", () => GetPeopleWithMonthMemberships())
                .Add("List every trainer with more than 3 clients\n", () => GetPopularTrainers())
                .Add("Person who has the most workouts done so far\n", () => GetPersonWithMostWorkouts())
                .Add("List every person with extreme workouts\n", () => GetPeopleWithExtremeWorkouts())
                .Add("List every persons trainer, with red colour membership\n", () => GetPersonsTrainerWithRedColourMembership())
                .Add("Return", ConsoleMenu.Close);

            var menu = new ConsoleMenu(args, level: 0)
                .Add("Memberships", () => membershipSubMenu.Show())
                .Add("Workouts", () => workoutSubMenu.Show())
                .Add("People", () => personSubMenu.Show())
                .Add("Trainers", () => trainerSubMenu.Show())
                .Add("NonCrud", () => nonCrudSubMenu.Show())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    
    }
}
