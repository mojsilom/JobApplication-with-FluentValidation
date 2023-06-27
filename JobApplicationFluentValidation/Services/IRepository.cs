using Microsoft.AspNetCore.Mvc;
using JobApplicationFluentValidation.Models;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationFluentValidation.Services
{
    public interface IRepository
    {
        Task<IEnumerable<Models.JobApplication>> GetApplicants();

        Task<Models.JobApplication?> GetApplicantById(int applicantID);
        Task CreateApplicant(Models.JobApplication applicant);

        Task DeleteApplicant(int id);
        Task<bool> SaveChangesAsync();
       
    }
}
