using System.Collections.Generic;
using System.Linq;
using Users_Demo.DAL.Models;

namespace Users_Demo.Test.UniversityTest
{
    public class FakeUniversity
    {
        public static IQueryable<University> GetSampleUniversities(bool hasData)
        {
            if (hasData == false)
                return new List<University>().AsQueryable();

            return new List<University>
            {
                new University
                {
                    Id = 1,
                    Name = "Testing1",
                    IsActive = true,
                    IsDeleted = false
                },
                new University
                {
                    Id = 2,
                    Name = "Testing2",
                    IsActive = true,
                    IsDeleted = false
                }
            }.AsQueryable();
        }

        public static University GetSampleUniversity(bool hasData)
        {
            if (hasData == false)
                return new University();

            return new University
            {
                Id = 3,
                Name = "Testing3",
                IsActive = true,
                IsDeleted = false
            };
        }
    }
}
