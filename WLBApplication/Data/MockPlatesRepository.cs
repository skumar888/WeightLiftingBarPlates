using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WLPBlatesManager.Model
{
    public class MockPlatesRepository : IPlatesRepository
    {

        List<Plate> plateRepo = new List<Plate>();

        public MockPlatesRepository()
        {
            plateRepo.Add(new Plate { Name = "2.5LB", weight = 2.5 });
            plateRepo.Add(new Plate { Name = "5LB", weight = 5 });
            plateRepo.Add(new Plate { Name = "10LB", weight = 10 });
            plateRepo.Add(new Plate { Name = "25LB", weight = 25 });
            plateRepo.Add(new Plate { Name = "35LB", weight = 35 });
            plateRepo.Add(new Plate { Name = "45LB", weight = 45 });
        }

        public async Task<IEnumerable<Plate>> GetAllPlates()
        {
            await Task.CompletedTask;
            return  plateRepo;
        }

        public async Task<Plate> GetPlate(double weight)
        {
            await Task.CompletedTask;
            return plateRepo.Where(w => w.weight == weight).FirstOrDefault();
        }

    }
}
