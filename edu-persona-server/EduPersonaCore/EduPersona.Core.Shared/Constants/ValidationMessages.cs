namespace EduPersona.Core.Shared.Constants
{
    public static class Messages
    {
        public static string AccessTokenGenerateError => "Your session has expired or invalid. Please try again.";
        public const string LoginSuccessfully = "Login successfully.";
        public const string RefreshTokenExpired = "Refresh token expired.";
        public const string RequestSuccessful = "Request successful.";
        public static readonly Func<string, string> SuccessfullyMessage = (entityName) => $"{entityName} successfully.";

        public static readonly Func<string, string> UpdateSuccessfullyMessage = (entityName) => $"{entityName} updated successfully.";

        public const string ProfileCompletedSuccessful = "Profile completed successful.";

        public class ErrorMessage
        {
            public static string AtLeastOneSkillMessage = "At least one skill is required.";

            public static Func<IEnumerable<int>, string> InvalidSkillIdsMessage =
                (skillIds) => $"Invalid Skill Id(s): {string.Join(", ", skillIds)}";

            public static readonly Func<string, string> NotExistMessage = (entityName) => $"{entityName} does not exist.";

            public static readonly Func<string, string> NotFoundMessage = (entityName) => $"{entityName} not found.";
            public static string DesignationProfessionMismatch = "Selected designation does not belong to the selected profession.";

            public static string SameCurrentAndTargetDesignation = "Current designation and target designation must be different.";

            public static string DuplicateSkillNotAllowed = "Duplicate skills are not allowed.";
        }

        public class ModelStateMessage
        {
            public const string CurrentDesignationRequired = "Current Designation is Required.";
            public const string TargetDesignationRequired = "Target Designation is Required.";
            public const string ProfessionRequired = "Profession is Required.";
            public const string SkillRequired = "Skill is Required.";
        }
    }
}
