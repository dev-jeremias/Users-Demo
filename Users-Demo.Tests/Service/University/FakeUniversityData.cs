using System.Collections.Generic;
using System.Linq;

namespace Users_Demo.Tests.Service.University
{
    public class FakeUniversityData
    {
        public static IQueryable<DAL.Models.University> GetSampleUniversities(bool hasData)
        {
            if (hasData == false)
                return new List<DAL.Models.University>().AsQueryable();

            return new List<DAL.Models.University>
            {
                new DAL.Models.University
                {
                    Id = 1,
                    Name = "Testing1",
                    IsActive = true,
                    IsDeleted = false
                },
                new DAL.Models.University
                {
                    Id = 2,
                    Name = "Testing2",
                    IsActive = true,
                    IsDeleted = false
                }
            }.AsQueryable();
        }

        public static DAL.Models.University GetSampleUniversity(bool hasData)
        {
            if (hasData == false)
                return new DAL.Models.University();

            return new DAL.Models.University
            {
                Id = 3,
                Name = "Testing3",
                IsActive = true,
                IsDeleted = false
            };
        }
    }
}
