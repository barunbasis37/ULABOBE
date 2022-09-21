using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ISchoolRepository School { get; }
        IDepartmentRepository Department { get; }
        IProgramRepository Program { get; }
        IUserTypeRepository UserType { get; }
        IDesignationRepository Designation { get; }
        IInstructorRepository Instructor { get; }
        ILevelTermRepository LevelTerm { get; }
        ISessionYearRepository SessionYear { get; }
        ICourseRepository Course { get; }
        ICourseTypeRepository CourseType { get; }
        ICourseHistoryRepository CourseHistory { get; }
        ICourseLearningTypeRepository CourseLearningType { get; }
        ICourseLearningRepository CourseLearning { get; }
        IProgramLearningRepository ProgramLearning { get; }
        ICourseCLORepository CourseCLO { get; }
        IProgramPLORepository ProgramPLO { get; }
        ICorrelationRepository Correlation { get; }
        IMappingCourseProgramLORepository MappingCourseProgramLO { get; }
        IGenericSkillTypeRepository GenericSkillType { get; }
        IGenericSkillRepository GenericSkill { get; }
        ICourseGenericSkillRepository CourseGenericSkill { get; }
        IProfessionalSkillRepository ProfessionalSkill { get; }
        IProgramProfessionalSkillRepository ProgramProfessionalSkill { get; }
        ILearningAssessmentRubricRepository LearningAssessmentRubric { get; }
        IDepartmentLARRepository DepartmentLAR { get; }
        ISDGContributionRepository SDGContribution { get; }
        IDepartmentSDGContributionRepository DepartmentSDGContribution { get; }
        ICourseContentRepository CourseContent { get; }
        ISemesterRepository Semester { get; }
        IMasterSetupRepository MasterSetup { get; }
        IWeekDayRepository WeekDay { get; }
        IScheduleRepository Schedule { get; }
        ITimeRepository Time { get; }
        ISectionRepository Section { get; }
        IDepartmentGSkillRepository DepartmentGSkill { get; }
        ICourseProgramMappingRepository CourseProgramMapping { get; }
        ILearningResourceTypeRepository LearningResourceType { get; }
        ICourseLearningResourceRepository CourseLearningResource { get; }
        ICoursePolicyTypeRepository CoursePolicyType { get; }
        ICoursePolicyProcedureRepository CoursePolicyProcedure { get; }
        IAssessmentTypeRepository AssessmentType { get; }
        IAssessmentTechniqueWeightageRepository AssessmentTechniqueWeightage { get; }
        IBloomsCategoryRepository BloomsCategory { get; }
        IAssessmentPatternRepository AssessmentPattern { get; }
        IUserInfoCheckRepository UserInfoCheck { get; }
        ICourseOutlineRepository CourseOutline { get; }
        IProgramCatalogRepository ProgramCatalog { get; set; }
        ICourseClassDocumentRepository CourseClassDocument { get; }
        //ICourseContentCLORepository CourseContentCLO { get; }
        ISP_Call SP_Call { get; }

        void Save();
    }
}
