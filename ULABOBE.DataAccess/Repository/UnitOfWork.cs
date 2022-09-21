using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULABOBE.DataAccess.Data;
using ULABOBE.DataAccess.Repository.IRepository;

namespace ULABOBE.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            School = new SchoolRepository(_db);
            Department = new DepartmentRepository(_db);
            Program = new ProgramRepository(_db);
            UserType = new UserTypeRepository(_db);
            Designation = new DesignationRepository(_db);
            Instructor = new InstructorRepository(_db);
            LevelTerm = new LevelTermRepository(_db);
            SessionYear = new SessionYearRepository(_db);
            Course = new CourseRepository(_db);
            CourseType = new CourseTypeRepository(_db);
            CourseHistory = new CourseHistoryRepository(_db);
            CourseLearning = new CourseLearningRepository(_db);
            ProgramLearning = new ProgramLearningRepository(_db);
            CourseLearningType = new CourseLearningTypeRepository(_db);
            CourseCLO = new CourseCLORepository(_db);
            ProgramPLO = new ProgramPLORepository(_db);
            Correlation = new CorrelationRepository(_db);
            MappingCourseProgramLO = new MappingCourseProgramLORepository(_db);
            GenericSkillType = new GenericSkillTypeRepository(_db);
            GenericSkill = new GenericSkillRepository(_db);
            CourseGenericSkill = new CourseGenericSkillRepository(_db);
            ProfessionalSkill = new ProfessionalSkillRepository(_db);
            ProgramProfessionalSkill = new ProgramProfessionalSkillRepository(_db);
            LearningAssessmentRubric = new LearningAssessmentRubricRepository(_db);
            DepartmentLAR = new DepartmentLARRepository(_db);
            SDGContribution = new SDGContributionRepository(_db);
            DepartmentSDGContribution = new DepartmentSDGContributionRepository(_db);
            CourseContent = new CourseContentRepository(_db);
            Semester = new SemesterRepository(_db);
            //CourseContentCLO = new CourseContentCLORepository(_db);
            MasterSetup = new MasterSetupRepository(_db);
            WeekDay = new WeekDayRepository(_db);
            Schedule = new ScheduleRepository(_db);
            Time = new TimeRepository(_db);
            Section = new SectionRepository(_db);
            DepartmentGSkill = new DepartmentGSkillRepository(_db);
            CourseProgramMapping = new CourseProgramMappingRepository(_db);
            LearningResourceType = new LearningResourceTypeRepository(_db);
            CourseLearningResource = new CourseLearningResourceRepository(_db);
            CoursePolicyType = new CoursePolicyTypeRepository(_db);
            CoursePolicyProcedure = new CoursePolicyProcedureRepository(_db);
            AssessmentType = new AssessmentTypeRepository(_db);
            AssessmentTechniqueWeightage = new AssessmentTechniqueWeightageRepository(_db);
            BloomsCategory = new BloomsCategoryRepository(_db);
            AssessmentPattern = new AssessmentPatternRepository(_db);
            UserInfoCheck = new UserInfoCheckRepository(_db);
            CourseOutline = new CourseOutlineRepository(_db);
            ProgramCatalog = new ProgramCatalogRepository(_db);
            CourseClassDocument = new CourseClassDocumentRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public ISchoolRepository School { get; private set; }
        public IDepartmentRepository Department { get; private set; }
        public IProgramRepository Program { get; private set; }
        public IUserTypeRepository UserType { get; set; }
        public IDesignationRepository Designation { get; set; }
        public IInstructorRepository Instructor { get; set; }
        public ILevelTermRepository LevelTerm { get; set; }
        public ISessionYearRepository SessionYear { get; set; }
        public ICourseRepository Course { get; set; }
        public ICourseTypeRepository CourseType { get; set; }
        public ICourseHistoryRepository CourseHistory { get; set; }
        public ICourseLearningRepository CourseLearning { get; set; }
        public IProgramLearningRepository ProgramLearning { get; set; }
        public ICourseLearningTypeRepository CourseLearningType { get; set; }
        public ICourseCLORepository CourseCLO { get; set; }
        public IProgramPLORepository ProgramPLO { get; set; }
        public ICorrelationRepository Correlation { get; set; }
        public IMappingCourseProgramLORepository MappingCourseProgramLO { get; set; }
        public IGenericSkillTypeRepository GenericSkillType { get; set; }
        public IGenericSkillRepository GenericSkill { get; set; }
        public ICourseGenericSkillRepository CourseGenericSkill { get; set; }
        public IProfessionalSkillRepository ProfessionalSkill { get; set; }
        public IProgramProfessionalSkillRepository ProgramProfessionalSkill { get; set; }
        public ILearningAssessmentRubricRepository LearningAssessmentRubric { get; set; }
        public IDepartmentLARRepository DepartmentLAR { get; set; }
        public ISDGContributionRepository SDGContribution { get; set; }
        public IDepartmentSDGContributionRepository DepartmentSDGContribution { get; set; }
        public ICourseContentRepository CourseContent { get; set; }
        public ISemesterRepository Semester { get; set; }

        public IMasterSetupRepository MasterSetup { get; set; }
        //public ICourseContentCLORepository CourseContentCLO { get; set; }
        public IWeekDayRepository WeekDay { get; set; }
        public IScheduleRepository Schedule { get; set; }
        public ITimeRepository Time { get; set; }
        public ISectionRepository Section { get; set; }
        public IDepartmentGSkillRepository DepartmentGSkill { get; set; }
        public ICourseProgramMappingRepository CourseProgramMapping { get; set; }
        public ICoursePolicyTypeRepository CoursePolicyType { get; set; }
        public ICoursePolicyProcedureRepository CoursePolicyProcedure { get; set; }
        public ILearningResourceTypeRepository LearningResourceType { get; set; }
        public ICourseLearningResourceRepository CourseLearningResource { get; set; }
        public IAssessmentTypeRepository AssessmentType { get; set; }
        public IAssessmentTechniqueWeightageRepository AssessmentTechniqueWeightage { get; set; }
        public IBloomsCategoryRepository BloomsCategory { get; set; }
        public IAssessmentPatternRepository AssessmentPattern { get; set; }
        public IUserInfoCheckRepository UserInfoCheck { get; set; }
        public ICourseOutlineRepository CourseOutline { get; set; }
        public IProgramCatalogRepository ProgramCatalog { get; set; }
        public ICourseClassDocumentRepository CourseClassDocument { get; set; }
        public ISP_Call SP_Call { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
