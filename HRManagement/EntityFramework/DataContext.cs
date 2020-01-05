using HRManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagement.EntityFramework
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>().HasData(new Company { Id = 1, Name = "Marta Company" });

            modelBuilder.Entity<JobOffer>().HasData(
                new JobOffer() { CompanyNameId = 1, Id = 1, ContractType = "fullt-ime", Description = "elo",
                JobTitle = "dentist", Location = "Warsaw", Salary = 10});

            modelBuilder.Entity<JobApplication>().HasData(
                new JobApplication() { JobOfferId = 1, ContactAgreement = false, CvUrl = "https://ale.com", EmailAddress = "a@b.c",
                FirstName = "Marta", LastName = "Elo", PhoneNumber = "1421412", Id = 1});
        }
    }
}
