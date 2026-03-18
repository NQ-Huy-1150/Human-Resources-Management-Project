using layout.repository;
using System;
using System.Collections.Generic;
using System.Data;
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
    }
}
