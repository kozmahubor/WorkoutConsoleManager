using IUE7VU_HFT_2022231.Repository;
using System;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IUE7VUDbContext cntxt = new IUE7VUDbContext();

            foreach (var item in cntxt.Persons)
            {
                Console.WriteLine($"Név: {item.PersonName}, edzője: {item.Trainer.TrainerName}");
            }
        }
    }
}
