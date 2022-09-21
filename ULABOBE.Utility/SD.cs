using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ULABOBE.Utility
{
    public static class SD
    {
        public const string Proc_UserType_Create = "usp_CreateUserType";
        public const string Proc_UserType_Get = "usp_GetUserType";
        public const string Proc_UserType_GetAll = "usp_GetUserTypes";
        public const string Proc_UserType_Update = "usp_UpdateUserType";
        public const string Proc_UserType_Delete = "usp_DeleteUserType";

        public const string Proc_Designation_Create = "usp_CreateDesignation";
        public const string Proc_Designation_Get = "usp_GetDesignation";
        public const string Proc_Designation_GetAll = "usp_GetDesignations";
        public const string Proc_Designation_Update = "usp_UpdateDesignation";
        public const string Proc_Designation_Delete = "usp_DeleteDesignation";

        public const string Proc_CourseOutline_GetWithParam = "usp_GetCourseOutLineWithParameter";
        public const string Proc_CourseClassDocument_GetWithParam = "usp_GetCourseClassDocumentWithParameter";

        public const string Role_User_Indi = "Individual";
        public const string Role_SuperAdmin = "Super-Admin";
        public const string Role_Administration = "Administration";
        public const string Role_Employee = "Employee";
        public const string Role_Faculty = "Faculty";


        public const string Proc_CourseProMap_Get = "usp_CourseProgramMapping";
        public const string Proc_CourseProMap_GetAll = "usp_CourseProgramMappings";
        public const string Proc_CourseProMap_ViewGetAll = "usp_RptViewCourseProgramMappings";


    }
}
