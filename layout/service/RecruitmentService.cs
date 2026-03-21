using layout.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Deployment.Internal;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.service
{
    public class RecruitmentService
    {
        RecruimentRepository repo = new RecruimentRepository();
        public void getCreateRecruitment(Recruitment recruitment)
        {
            repo.createRecruitment(recruitment);
        }
        public DataTable fetchAllRecruitment()
        {
            return repo.getAllRecruitment();
        }
        public DataTable fetchById(int id)
        {
            return repo.findById(id);
        }
        public void getDelete(int id)
        {
            repo.deleteRecruitment(id);
        }
        public void getUpdate(Recruitment re)
        {
            repo.updateRecruitment(re);
        }
        public string getPosition(int id)
        {
            return repo.getPositionByRecruitmentId(id);
        }
        public DataTable fetchAllJobs()
        {
            return repo.getAllJobs();
        }
    }
}
