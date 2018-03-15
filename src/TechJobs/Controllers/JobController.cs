using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TechJobs.Data;
using TechJobs.Models;
using TechJobs.ViewModels;
using System.Linq;


namespace TechJobs.Controllers
{
    public class JobController : Controller
    {
        

        // Our reference to the data store
        private static JobData jobData;
        private static Job job; 

        static JobController()
        {
            jobData = JobData.GetInstance();
            job = new Job();
        }


         public IActionResult Index(int id)
        {
            // TODO #1 - get the Job with the given ID and pass it into the view

         
            if (id != 99999)
            {
                job = jobData.Find(id);
            }
  

            List<Job> jobList = new List<Job>();
            jobList.Add(job);

            SearchJobsViewModel jobViewModel = new SearchJobsViewModel();
            jobViewModel.Jobs = jobList;

            jobViewModel.Title = "Specific Job";

            jobData.Jobs.Add(job);

            return View(jobViewModel);

            
        }


        public IActionResult New()
        {
            NewJobViewModel newJobViewModel = new NewJobViewModel();
            return View(newJobViewModel);
        }

        [HttpPost]
        public IActionResult New(NewJobViewModel newJobViewModel)
        {
            // TODO #6 - Validate the ViewModel and if valid, create a 
            // new Job and add it to the JobData data store. Then
            // redirect to the Job detail (Index) action/view for the new Job.

           
            job.Name = newJobViewModel.Name;
            if (!ModelState.IsValid)
            {
                return View(newJobViewModel);
            }

            job.Employer = jobData.Employers.Find(newJobViewModel.EmployerID);
            string empVal = job.Employer.Value;

            job.Location = jobData.Locations.Find(newJobViewModel.LocationID);
            string locVal = job.Location.Value;

            job.CoreCompetency = jobData.CoreCompetencies.Find(newJobViewModel.CoreCompetencyID);
            string ccVal = job.CoreCompetency.Value;

            job.PositionType = jobData.PositionTypes.Find(newJobViewModel.PositionTypeID);
            string posTypVal = job.PositionType.Value;

            string jobDataRow = job.Name + ',' + empVal + ',' + locVal + ',' +
                                                 ccVal + ',' + posTypVal;
                                          //       + '\n';


            jobDataExporter.writeJobDataRow(jobDataRow);
        
            return Redirect("/Job?id=99999");

            
        }


    }
}
