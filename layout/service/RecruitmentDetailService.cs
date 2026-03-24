using layout.domain;
using layout.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.service
{
    public class RecruitmentDetailService
    {
        RecruitmentDetailRepository repo = new RecruitmentDetailRepository();
        public int getCreateRecruitment(Candidate candidate)
        {
            return repo.createRecruitmentDetail(candidate);
        }
        public DataTable fetchAllRecruitment(int id)
        {
            return repo.getAllRecruitmentDetailByRecruitId(id);
        }
        public DataTable fetchById(int id)
        {
            return repo.findById(id);
        }
        public void getDelete(int id)
        {
            repo.deleteRecruitmentDetail(id);
        }
        public void getUpdateDetail(Candidate candidate)
        {
            repo.updateRecruitmentDetail(candidate);
        }
        public void getUpdateStatus(int id, string status)
        {
            repo.updateRecruitmentDetailStatus(id, status);
        }
        public string findByLookupId(string id)
        {
            return repo.getStatusFromLookUpId(id);
        }
    }
}
