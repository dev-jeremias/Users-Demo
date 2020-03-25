using System;
using System.Collections.Generic;
using System.Linq;
using Users_Demo.DAL.Models;

namespace Users_Demo.Test.UsersTest
{
    public class FakeUser
    {
        public static IQueryable<Users> GetSampleUsers(bool hasData)
        {
            if (hasData == false)
                return new List<Users>().AsQueryable();

            return new List<Users>
            {
                new Users
                {
                    Id = 1,
                    FirstName = "TestingF1",
                    LastName = "TestingL1",
                    DateOfBirth = DateTime.UtcNow,
                    IsActive = true,
                    IsDeleted = false
                },
                new Users
                {
                    Id = 2,
                    FirstName = "TestingF2",
                    LastName = "TestingL2",
                    DateOfBirth = DateTime.UtcNow,
                    IsActive = true,
                    IsDeleted = false
                }
            }.AsQueryable();
        }

        public static Users GetSampleUser(bool hasData)
        {
            if (hasData == false)
                return new Users();

            return new Users
            {
                Id = 3,
                FirstName = "TestingF3",
                LastName = "TestingL3",
                DateOfBirth = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
        }
    }
}
