using NUnit.Framework;
using System;
using IUE7VU_HFT_2022231.Models;
using IUE7VU_HFT_2022231.Repository;
using IUE7VU_HFT_2022231.Logic;
using Moq;
using System.Collections.Generic;
using System.Linq;
using static IUE7VU_HFT_2022231.Models.Enum;

namespace IUE7VU_HFT_2022231.Test
{
    [TestFixture]
    public class IUE7VULogicTester
    {
        PersonLogic personLogic;
        TrainerLogic trainerLogic;
        MembershipLogic membershipLogic;
        WorkoutLogic workoutLogic;
        Mock<IRepository<Person>> mockPersonRepo;
        Mock<IRepository<Trainer>> mockTrainerRepo;
        Mock<IRepository<Membership>> mockMembershipRepo;
        Mock<IRepository<Workout>> mockWorkoutRepo;

        [SetUp]
        public void Init()
        {
            mockPersonRepo = new Mock<IRepository<Person>>();
            mockTrainerRepo = new Mock<IRepository<Trainer>>();
            mockMembershipRepo = new Mock<IRepository<Membership>>();
            mockWorkoutRepo = new Mock<IRepository<Workout>>();

            personLogic = new PersonLogic(mockPersonRepo.Object);
            trainerLogic = new TrainerLogic(mockTrainerRepo.Object);
            membershipLogic = new MembershipLogic(mockMembershipRepo.Object, mockPersonRepo.Object);
            workoutLogic = new WorkoutLogic(mockWorkoutRepo.Object, mockPersonRepo.Object);
        }
        [Test]
        public void GetPeopleWithMonthMembershipsTest()
        {
            //arrange
            var person = new List<Person>()
            {
                new Person()
                {
                    PersonId = 1,
                    PersonName = "Sanyi",
                    Memberships = new Membership
                    {
                        MembershipType = MembershipTypes.Monthly,
                    }
                },
                new Person()
                {
                    PersonId = 2,
                    PersonName = "Karcsi",
                    Memberships = new Membership
                    {
                        MembershipType = MembershipTypes.Daily,
                    }
                },
            };
            mockPersonRepo.Setup(p => p.ReadAll()).Returns(person.AsQueryable());
            var expected = new List<Person>() { person[0] };

            //act
            var actual = personLogic.GetPeopleWithMonthMemberships();

            //assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetPopularTrainersTest()
        {
            var trainers = new List<Trainer>()
            {
                new Trainer()
                {
                    TrainerId = 1,
                    Clients = new List<Person>() { new Person(), new Person() }
                },
                new Trainer()
                {
                    TrainerId = 2,
                    Clients = new List<Person>() { new Person() }
                }
            };
            mockTrainerRepo.Setup(t => t.ReadAll()).Returns(trainers.AsQueryable());

            var expected = new List<Trainer>() { trainers[0] };
            var actual = trainerLogic.GetPopularTrainers();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetPersonWithMostWorkoutsTest()
        {
            var person = new List<Person>()
            {
                new Person()
                {
                    PersonId= 1,
                    PersonName = "Atilla",
                    Workouts = new List<Workout>() { new Workout(), new Workout(), new Workout() }
                },
                
                new Person()
                {
                    PersonId= 1,
                    PersonName = "Atilla",
                    Workouts = new List<Workout>() { new Workout() }
                },

                new Person()
                {
                    PersonId= 1,
                    PersonName = "Atilla",
                    Workouts = new List<Workout>() { new Workout(), new Workout() }
                },

            };
            mockPersonRepo.Setup(p => p.ReadAll()).Returns(person.AsQueryable);

            var expected = new List<Person>() { person[0] };
            var actual = personLogic.GetPersonWithMostWorkouts();
            
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetPeopleWithExtremeWorkoutsTest()
        {
            var person = new List<Person>()
            {
                new Person()
                {
                    PersonId = 1,
                    PersonName = "Hunor",
                    Workouts= new List<Workout>() 
                    {
                        new Workout()
                        {
                            WorkoutId = 1,
                            WorkoutDifficulty = WorkoutDifficulty.Extreme,
                        },

                        new Workout()
                        {
                            WorkoutId = 2,
                            WorkoutDifficulty= WorkoutDifficulty.Easy,
                        }
                    }
                },

                new Person()
                {
                    PersonId = 5,
                    PersonName = "Reni",
                    Workouts= new List<Workout>()
                    {
                        new Workout()
                        {
                            WorkoutId = 3,
                            WorkoutDifficulty = WorkoutDifficulty.Hard,
                        },

                        new Workout()
                        {
                            WorkoutId = 4,
                            WorkoutDifficulty= WorkoutDifficulty.Medium,
                        }
                    }
                }
            };
            mockPersonRepo.Setup(p => p.ReadAll()).Returns(person.AsQueryable());

            var expected = new List<Person>() { person[0] };
            var actual = personLogic.GetPeopleWithExtremeWorkouts();

            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void GetPersonsTrainerWithRedColourMembershipTest()
        {
            var trainer = new List<Trainer>()
            {
                new Trainer()
                {
                    TrainerId = 1,
                    TrainerName = "Antalovics",
                    Clients = new List<Person>() 
                    { 
                        new Person()
                        {
                            PersonId = 1,
                            PersonName = "Hubor",
                            Memberships = new Membership()
                            {
                                MembershipId = 1,
                                MembershipTicketColour = MembershipTicketColours.Red,
                            },
                        },
                        new Person()
                        {
                            PersonId = 2,
                            PersonName = "Laci",
                            Memberships = new Membership()
                            {
                                MembershipId = 2,
                                MembershipTicketColour = MembershipTicketColours.Green,
                            },
                        },
                    },
                },
                new Trainer()
                {
                    TrainerId = 2,
                    TrainerName = "Kitti",
                    Clients = new List<Person>()
                    {
                        new Person()
                        {
                            PersonId = 3,
                            PersonName = "Zsolti",
                            Memberships = new Membership()
                            {
                                MembershipId = 3,
                                MembershipTicketColour = MembershipTicketColours.Blue,
                            },
                        },
                        new Person()
                        {
                            PersonId = 4,
                            PersonName = "Reni",
                            Memberships = new Membership()
                            {
                                MembershipId = 4,
                                MembershipTicketColour = MembershipTicketColours.Pink,
                            },
                        },
                    },
                },
            };
            mockTrainerRepo.Setup(t => t.ReadAll()).Returns(trainer.AsQueryable());

            var expected = new List<Trainer>() { trainer[0] };
            var actual = trainerLogic.GetPersonsTrainerWithRedColourMembership();

            Assert.AreEqual(expected, actual);
        }

        //------TRAINER--------------------------------------------
        [Test]
        public void CreateTrainerWithCorrectNameTest()
        {
            var trainer = new Trainer()
            {
                TrainerName = "Antal"
            };
            trainerLogic.Create(trainer);
            mockTrainerRepo.Verify(t => t.Create(trainer), Times.Once);
        }
        [Test]
        public void CreateTrainerWithInCorrectDataTest()
        {
            var trainer = new Trainer()
            {
                TrainerName = "",
                TrainerAge = 10
            };
            Assert.Catch<ArgumentException>(() => trainerLogic.Create(trainer));

            mockTrainerRepo.Verify(t => t.Create(trainer), Times.Never);
        }
        [Test]
        public void CreateTrainerWithCorrectDataTest()
        {
            var trainer = new Trainer()
            {
                TrainerId = 1,
                TrainerName = "Antal",
                TrainerAge = 29,
                TrainerGender = 0
            };
            trainerLogic.Create(trainer);
            mockTrainerRepo.Verify(t => t.Create(trainer), Times.Once);
        }

        //------PERSON--------------------------------------------
        [Test]
        public void CreatePersonWithCorrectNameTest()
        {
            var person = new Person()
            {
                PersonName = "Hunor"
            };
            personLogic.Create(person);
            mockPersonRepo.Verify(p => p.Create(person), Times.Once);
        }
        [Test]
        public void CreatePersonWithInCorrectNameTest()
        {
            var person = new Person()
            {
                PersonName = ""
            };
            Assert.Catch<ArgumentException>(() => personLogic.Create(person));
            mockPersonRepo.Verify(p => p.Create(person), Times.Never);
        }
        [Test]
        public void CreatePersonWithCorrectDatasTest()
        {
            var person = new Person()
            {
                PersonId = 1,
                PersonName = "Hunor",
                PersonAge = 22,
                PersonGender = 0
            };
            personLogic.Create(person);
            mockPersonRepo.Verify(p => p.Create(person), Times.Once);
        }

        //------WORKOUT--------------------------------------------
        [Test]
        public void CreateWorkoutWithCorrectBodypartTest()
        {
            var person = new Person()
            {
                PersonId = 1,
            };
            mockPersonRepo.Setup(p => p.Read(1)).Returns(person);

            var workout = new Workout()
            {
                PersonId = 1,
                MuscleTypes = 0,
            };
            workoutLogic.Create(workout, workout.PersonId);
            mockWorkoutRepo.Verify(p => p.Create(workout), Times.Once);
        }
        [Test]
        public void CreatWorkoutWithInCorrectBodypart()
        {
            var person = new Person()
            {
                PersonId = 1,
            };
            mockPersonRepo.Setup(p => p.Read(1)).Returns(person);

            var workout = new Workout()
            {
                PersonId = 1,
                MuscleTypes = (Models.Enum.MuscleTypes)9,
            };
            Assert.Catch<ArgumentException>(() => workoutLogic.Create(workout, workout.PersonId));
            mockWorkoutRepo.Verify(w => w.Create(workout), Times.Never);
        }

        //------MEMBERSHIP--------------------------------------------
        [Test]
        public void CreateMembershipWithCorrectColourTest()
        {
            var person = new Person()
            {
                PersonId = 1,
            };
            mockPersonRepo.Setup(p => p.Read(1)).Returns(person);
            var membership = new Membership()
            {
                PersonId = 1,
                MembershipTicketColour = (Models.Enum.MembershipTicketColours)3,
            };
            membershipLogic.Create(membership, membership.PersonId);
            mockMembershipRepo.Verify(p => p.Create(membership), Times.Once);
        }
        [Test]
        public void CreateMembershipWithInCorrectColourTest()
        {
            var person = new Person()
            {
                PersonId = 1,
            };
            mockPersonRepo.Setup(p => p.Read(1)).Returns(person);
            var membership = new Membership()
            {
                PersonId = 1,
                MembershipTicketColour = (Models.Enum.MembershipTicketColours)5,
            };
            Assert.Catch<ArgumentException>(() => membershipLogic.Create(membership, membership.PersonId));
            mockMembershipRepo.Verify(m => m.Create(membership), Times.Never);
        }
        
    }
}
