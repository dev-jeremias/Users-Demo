using System;
using System.Collections.Generic;
using Users_Demo.DAL;
using Users_Demo.DAL.Models;

namespace Users_Demo.Tests
{
    public class Utilities
    {
        public static void InitializeDbForTests(UsersDemoContext db)
        {
            db.Users.AddRange(AddUsers());
            db.University.AddRange(AddUniversities());
            db.SaveChanges();
        }

        public static void ReinitializeDbForTests(UsersDemoContext db)
        {
            
            db.Users.RemoveRange(db.Users);
            db.University.RemoveRange(db.University);
            InitializeDbForTests(db);
        }

        public static List<Users> AddUsers()
        {
            return new List<Users>()
            {
                new Users(){ FirstName = "TestF1", LastName = "TestL1", IsDeleted = false, DateOfBirth = DateTime.UtcNow, IsActive = true },
                new Users(){ FirstName = "TestF2", LastName = "TestL2", IsDeleted = false, DateOfBirth = DateTime.UtcNow, IsActive = true },
                new Users(){ FirstName = "TestF3", LastName = "TestL2", IsDeleted = false, DateOfBirth = DateTime.UtcNow, IsActive = true },
            };
        }

        public static List<DAL.Models.University> AddUniversities()
        {
            return  new List<DAL.Models.University>()
            {
                new DAL.Models.University(){ Id = 1, IsDeleted = false, Name = "Test1", IsActive = true },
                new DAL.Models.University(){ Id = 2, IsDeleted = false, Name = "Test2", IsActive = true }
            };
        }
    }
}
