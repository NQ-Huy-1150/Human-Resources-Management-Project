using layout.repository;
using System.Data;

namespace layout.service
{
    public class PositionService
    {
        PositionRepository repo = new PositionRepository();

        public DataTable getAllPositionName()
        {
            return repo.getAllPositionName();
        }
        public float getBaseSalary(int id)
        {
            return repo.getBaseSalaryFromPositionId(id);
        }
        public string getPositionName(int id)
        {
            return repo.getPositionNameFromPositionId(id);
        }
        // Thêm vào trong class PositionService
        public DataTable getAllPosition()
        {
            return repo.getAllPosition();
        }

        public bool insertPosition(string name, float salary)
        {
            return repo.insertPosition(name, salary);
        }

        public bool updatePosition(int id, string name, float salary)
        {
            return repo.updatePosition(id, name, salary);
        }

        public bool deletePosition(int id)
        {
            // Có thể thêm logic kiểm tra nghiệp vụ tại đây trước khi xóa
            return repo.deletePosition(id);
        }
    }
}
