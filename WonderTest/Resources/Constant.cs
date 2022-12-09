
namespace GameAnalytics.Enumerations
{
    public class Constant
    {
        public const string SuccessResponse = "Success";
        public const string FromEmail = "info@system.com";
        public const string SmtpHost = "smtp@gmail.com";
        public const string FromPassword = "test";
        public const string SenderName = "testName";
        public const string CurrentProgress = "Current {0} Progress";
        public const string EmailResponse = "Emails are being send to the Users";
        public const string EmailSentResponse = "Email Sent";

        #region SPs
        public const string SP_CreateUpdateGameData = "Usp_CreateUpdateGameData";
        public const string SP_CreateUpdateUsers = "Usp_CreateUpdateUsers";
        public const string SP_GetGameAnalytics = "Usp_GetGameAnalytics";
        public const string SP_GetAllUser = "Usp_GetAllUser";
        #endregion
    }
}
