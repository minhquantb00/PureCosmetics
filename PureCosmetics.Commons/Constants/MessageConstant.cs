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
        /// Represents the message displayed when a user is updated successfully.
        /// </summary>
        public const string USER_UPDATED = "User updated successfully.";

        /// <summary>
        /// Represents the message displayed when a user is deleted successfully.
        /// </summary>
        public const string USER_DELETED = "User deleted successfully.";

        /// <summary>
        /// Represents the error message used to indicate validation failures.
        /// </summary>
        public const string VALIDATION_ERROR = "Validation errors.";

        /// <summary>
        /// Represents the message displayed when a user is not authenticated.
        /// </summary>
        public const string UN_AUTHENTICATED = "UnAuthenticated user";

        /// <summary>
        /// Represents the message displayed when a user is not authorized to perform an action.
        /// </summary>
        public const string UN_AUTHORIZED = "Unauthorized user";

        /// <summary>
        /// Represents the error message used when the provided username or email is invalid.
        /// </summary>
        public const string INVALID_USERNAME_OR_EMAIL = "Invalid user name or email";

        /// <summary>
        /// Reporesents the error message used when the provided password is invalid.
        /// </summary>
        public const string INVALID_PASSWORD = "Invalid password";

        /// <summary>
        /// Represents the message displayed when a user logs in successfully.
        /// </summary>
        public const string LOGIN_SUCCESS = "Login successful.";
    }

    /// <summary>
    /// Provides constant string messages related to role operations, such as validation errors and status
    /// User created: QuanTM
    /// Created date: 2025/12/19
    /// Last modified date: 2025/12/19
    /// </summary>
    public class MessageConstantForRole
    {
        /// <summary>
        /// Represents an error message indicating that a role with the same code already exists.
        /// </summary>
        public const string ROLE_CODE_ALREADY_EXISTS = "Role code already exists.";

        /// <summary>
        /// Represents the message displayed when a role is created successfully.
        /// </summary>
        public const string ROLE_CREATED_SUCCESSFULLY = "Role created successfully.";

        /// <summary>
        /// Represents the error message used when role creation fails.
        /// </summary>
        public const string ROLE_CREATION_FAILED = "Role creation failed.";

        /// <summary>
        /// Represents the message displayed when a role is updated successfully.
        /// </summary>
        public const string ROLE_UPDATED_SUCCESSFULLY = "Role updated successfully.";

        /// <summary>
        /// Represents the message displayed when a role is deleted successfully.
        /// </summary>
        public const string ROLE_DELETED_SUCCESSFULLY = "Role deleted successfully.";


        /// <summary>
        /// Represents the error message used when a role is not found.
        /// </summary>
        public const string ROLE_NOT_FOUND = "Role not found.";
    }
}
