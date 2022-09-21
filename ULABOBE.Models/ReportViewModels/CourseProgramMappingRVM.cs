using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Models.ReportViewModels
{
    public class CourseProgramMappingRVM
    {
        //Select CPM.Id, C.CourseCode, CLO.CLOCode, CCLO.Description, 
        //Ins.Name, CPM.PLoSelectedIDNames, CPM.GSSelectedIDNames, CPM.PSSelectedIDNames, 
        //CPM.SDGSelectedIDNames, CPM.ARSelectedIDNames, CPM.ApprovedBy, CPM.ApprovedDate, CPM.ApprovedIp, CPM.IsApproved
        //    From dbo.CourseProgramMappings AS CPM
        //    Inner Join CourseLearnings AS CLO on CPM.CourseLearningId= CLO.Id
        //    Left Join CourseHistories AS CHis on CPM.CourseHistoryId= CHis.Id
        //    Left Join Instructors As Ins on CHis.InstructorId= Ins.Id
        //    Left Join Courses As C on CHis.CourseId= C.Id
        //    Left Join CourseClos CCLO on CLO.Id= CCLO.CLOId
        //    Where CPM.IsDeleted= 0 And Ins.Id= 165
        //public CourseProgramMappingRVM(CourseProgramMapping aCourseProgramMapping )
        //{
        //    CourseCode = aCourseProgramMapping.CourseHistory.Course.CourseCode;
        //    CLOName = aCourseProgramMapping.CourseLearning.CLOCode;
        //    Instructor = aCourseProgramMapping.CourseHistory.Instructor.Name;
        //    PLOSelectedName = aCourseProgramMapping.PLoSelectedIDNames;
        //    PSSelectedName = aCourseProgramMapping.PSSelectedIDNames;

        //}
        public int Id { get; set; }
        public string CourseCode { get; set; }
        public string CLOCode { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string InstructorId { get; set; }
        public string PLoSelectedIDNames { get; set; }
        public string GSSelectedIDNames { get; set; }
        public string PSSelectedIDNames { get; set; }
        public string SDGSelectedIDNames { get; set; }
        public string ARSelectedIDNames { get; set; }

        //public string ApprovedBy { get; set; }
        public bool IsApproved { get; set; }

    }



}
