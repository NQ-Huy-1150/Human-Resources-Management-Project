using layout.repository;
using System.Data;

namespace layout.service
{
    public class PositionService
    {
        private readonly PositionRepository repo = new PositionRepository();

        public DataTable getAllPositionName()
        {
            return repo.getAllPositionName();
        }
        public float getBaseSalary(int id)
        {
            return repo.getBaseSalaryFromPositionId(id);
        }
    }
}
