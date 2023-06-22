using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CabBooking.Models
{
    public class ResponseType
    {
        public static string Success { get { return "success"; } }
        public static string Warning { get { return "warning"; } }
        public static string Error { get { return "error"; } }
        public static string Info { get { return "info"; } }
    }

    public class MessageStream
    {
        public static string NoDataFound { get { return "No Data Found."; } }
        public static string DataExist { get { return "Data Allready Exist."; } }
        public static string AllFieldsMandatory { get { return "All fields are required."; } }
        public static string UserNotFound { get { return "Invalid login id entered. Please enter valid login id."; } }

        public static string InvalidOrganisation { get { return "Invalid organisation."; } }
        public static string UserIdNotFound { get { return "User id not found."; } }

        public static string InvalidUserId { get { return "Invalid user id."; } }

        public static string ProfileDataNotFound { get { return "Profile detail not found."; } }

        public static string OTPNotVerified { get { return "Mobile could not Verify."; } }
        public static string OTPNotValid { get { return "Entered OTP is invalid. Please enter valid OTP."; } }
        public static string OTPSentMailAndMobileSucess { get { return "An OTP has been sent on your registered Mobile No. and Email ID. Enter that OTP in respective field to login into your account."; } }
        public static string OTPRequired { get { return "Please Enter OTP."; } }

        public static string UnRegisteredUser { get { return "Please enter valid Registered Mobile No."; } }
        public static string UserAlreadyRegistered { get { return "Mobile No. that you have entered is already registered."; } }
        public static string ValidMobileNoRequired { get { return "Please enter valid Mobile No."; } }
        public static string SessionExpireMessage { get { return "Oh! It seems your session is expired."; } }
        public static string SomethingWentWrong { get { return "Sorry! Unable to process, Something went wrong."; } }
        public static string InvalidCaptcha { get { return "Invalid Captcha Entered. Please Enter Valid Captcha."; } }
        public static string EmailLink { get { return "An Email verification link has been send to your registered email Id. Link is valid for next 24 hours."; } }
        public static string EmailLinkExpire { get { return "This Link is expired please go back and try with different link."; } }
        public static string SessionExpire { get { return "Your session has expired and you have been Logged Out!"; } }
        public static string InvalidUser { get { return "Invalid User ID or Password is entered. Please enter correct details."; } }

        public static string InvalidUserDetail { get { return "Invalid Login ID is entered. Please enter correct details."; } }

        public static string PasswordChanged { get { return "Password Changed Successfully."; } }

        public static string IncorrectOldPassword { get { return "Old password is incorrect. Please enter correct old password."; } }

        public static string InvalidCurrentPassword { get { return "Please Enter Correct Current Password."; } }

        public static string OTPSentMobileSucess { get { return "An OTP has been sent on your registered Mobile No. and Email Id Kindly fill and verify."; } }

        public static string OTPSentEmailSucess { get { return "An OTP has been sent on your registered E-Mail Id. Kindly fill and verify it to change password."; } }

        public static string UsernameInvalid { get { return "Invalid User ID Entered. Please Enter Valid User ID"; } }
        public static string PasswordInvalid { get { return "Invalid Password Entered. Please Enter Valid Password."; } }
        public static string UnAuthorizedUser { get { return "You Are not a Authorized User."; } }

        public static string InvalidEmailForForgetPassword { get { return "Incorrect Registered Email ID is entered. Please enter correct details"; } }

        public static string InvalidEmailMobileForForgetPassword { get { return "Incorrect Registered Email ID and Mobile No. Please enter correct details"; } }

        public static string InvalidMobileForForgetPassword { get { return "Incorrect Registered Mobile No is entered. Please enter correct details"; } }

        public static string ProfileUpdateSuccess { get { return "Profile updated successfully"; } }

        public static string OpeningImportReadingNotFound { get { return "Opening Import Reading Not Found"; } }

        public static string OpeningExportReadingNotFound { get { return "Opening Export Reading Not Found"; } }

        public static string SubstationMeteringDateOver { get { return "Add date is over"; } }

        public static string EditDateOver { get { return "Edit date is over"; } }

        public static string PreMonthSubMeteringNotFound { get { return "Please Fill Previous Month Entry."; } }

        public static string MeteringStatusNotFound { get { return "Please Fill First Metering Status Format !"; } }

        public static string ReportLevelNotSelected { get { return "Please Select Report Level !"; } }

        public static string AllFieldRequired { get { return "Please Input All Fields."; } }

        public static string mobileNoNotFound { get { return "Registration Mobile No. Not Found."; } }


    }
}