using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ULABOBE.Models;

namespace ULABOBE.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<School> Schools { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ProgramData> Programs { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<UserInfoCheck> UserInfoChecks { get; set; }
        public DbSet<CourseType> CourseTypes { get; set; }
        public DbSet<Session> SessionYears { get; set; }
        public DbSet<Term> LevelTerms { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseHistory> CourseHistories { get; set; }
        public DbSet<CourseLearning> CourseLearnings { get; set; }
        public DbSet<CourseLearningType> CourseLearningTypes { get; set; }
        public DbSet<CourseCLO> CourseClos { get; set; }
        public DbSet<ProgramLearning> ProgramLearnings { get; set; }
        public DbSet<ProgramPLO> ProgramPLOs { get; set; }
        public DbSet<Correlation> Correlations { get; set; }
        public DbSet<MappingCourseProgramLO> MappingCourseProgramLos { get; set; }
        public DbSet<GenericSkillType> GenericSkillTypes { get; set; }
        public DbSet<GenericSkill> GenericSkills { get; set; }
        public DbSet<CourseGenericSkill> CourseGenericSkills { get; set; }
        public DbSet<ProfessionalSkill> ProfessionalSkills { get; set; }
        public DbSet<ProgramProfessionalSkill> ProgramProfessionalSkills { get; set; }
        public DbSet<LearningAssessmentRubric> LearningAssessmentRubrics { get; set; }
        public DbSet<DepartmentLAR> DepartmentLars { get; set; }
        public DbSet<SDGContribution> SDGContributions { get; set; }
        public DbSet<DepartmentSDGContribution> DepartmentSdgContributions { get; set; }
        public DbSet<CourseContent> CourseContents { get; set; }
        public DbSet<MasterSetup> MasterSetups { get; set; }
        //public DbSet<CourseContentCLO> CourseContentCLOs { get; set; }
        public DbSet<Semester> Semesters { get; set; }
        public DbSet<WeekDay> WeekDays { get; set; }
        public DbSet<Time> Times { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<CourseProgramMapping> CourseProgramMappings { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<DepartmentGSkill> DepartmentGSkills { get; set; }
        public DbSet<LearningResourceType> LearningResourceTypes { get; set; }
        public DbSet<CourseLearningResource> CourseLearningResources { get; set; }
        public DbSet<CoursePolicyType> CoursePolicyTypes { get; set; }
        public DbSet<CoursePolicyProcedure> CoursePolicyProcedures { get; set; }
        public DbSet<AssessmentType> AssessmentTypes { get; set; }
        public DbSet<AssessmentTechniqueWeightage> AssessmentTechniqueWeightages { get; set; }
        public DbSet<BloomsCategory> BloomsCategories { get; set; }
        public DbSet<AssessmentPattern> AssessmentPatterns { get; set; }
        public DbSet<CourseOutline> CourseOutlines { get; set; }
        public DbSet<ProgramCatalog> ProgramCatalogs { get; set; }
        public DbSet<CourseClassDocument> CourseClassDocuments { get; set; }

        //public DbQuery<> DbQuery { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //}


    }
}
