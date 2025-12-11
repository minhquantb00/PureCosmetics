using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Constants
{
    /// <summary>
    /// Provides constant string messages related to user operations, such as validation errors and status
    /// notifications.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>Use these constants to standardize user-facing messages throughout the application, ensuring
    /// consistency in error reporting and status updates.</remarks>
    public static class MessageConstantForUser
    {
        /// <summary>
        /// Represents an error message indicating that a user with the same phone number or email address already
        /// exists.
        /// </summary>
        public const string ALREADY_EXIST_EMAIL_OR_PHONENUMBER = "User with the same PhoneNumber or Email already exists.";

        /// <summary>
        /// Represents the error message used when a user object is null.
        /// </summary>
        public const string USER_IS_NULL = "User is null.";

        /// <summary>
        /// Represents the message displayed when a user is created successfully.
        /// </summary>
        public const string USER_CREATED = "User created successfully.";

        /// <summary>
        /// Represents the error message used to indicate validation failures.
        /// </summary>
        public const string VALIDATION_ERROR = "Validation errors.";
    }
}
