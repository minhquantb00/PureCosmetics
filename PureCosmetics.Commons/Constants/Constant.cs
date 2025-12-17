using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureCosmetics.Commons.Constants
{
    /// <summary>
    /// Provides application-wide constant values for roles, connection strings, authentication secrets, and default API
    /// routing patterns.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>This class contains string constants commonly used throughout the application to ensure
    /// consistency and reduce the risk of typographical errors. The constants include role identifiers, default
    /// connection string names, authentication secret keys, and standard route templates for API controllers and
    /// actions. All members are static and can be accessed without instantiating the class.</remarks>
    public static class Constant
    {
        /// <summary>
        /// Represents the role name assigned to customer users.
        /// </summary>
        public const string ROLE_CUSTOMER = "ROLE_CUSTOMER";

        /// <summary>
        /// Represents the name of the default database connection string.
        /// </summary>
        public const string DEFAULT_CONNECTION = "DefaultConnection";

        /// <summary>
        /// Represents the configuration key name used to retrieve the authentication secret.
        /// </summary>
        public const string AUTH_SECRET = "AuthSecret";

        /// <summary>
        /// Represents the default route template for API controllers, using the controller and action names in the URL.
        /// </summary>
        /// <remarks>This constant can be used to configure routing in ASP.NET Core applications, ensuring
        /// that API endpoints follow the conventional pattern of 'api/{controller}/{action}'.</remarks>
        public const string DEFAULT_CONTROLLER_ROUTE = "api/[controller]/[action]";

        /// <summary>
        /// Represents the default route template for API controllers that does not include an action segment.
        /// </summary>
        /// <remarks>This constant is typically used when configuring routing for ASP.NET Core
        /// controllers, allowing requests to be matched to controller endpoints without specifying an action in the
        /// URL. The placeholder '[controller]' is replaced with the controller's name at runtime.</remarks>
        public const string DEFAULT_CONTROLLER_ROUTE_WITHOUT_ACTION = "api/[controller]";

        /// <summary>
        /// Represents the default route template for actions, typically used in routing configurations to indicate
        /// where the action name should be substituted.
        /// </summary>
        /// <remarks>This constant is commonly used in frameworks or libraries that support attribute
        /// routing, allowing developers to specify routes dynamically based on action names.</remarks>
        public const string DEFAULT_ACTION_ROUTE = "[action]";
    }

    /// <summary>
    /// Provides constant string values representing user role identifiers used for access control within the
    /// application.
    /// User create: QuanTM
    /// Created date: 2025/12/11
    /// Last modified date: 2025/12/11
    /// </summary>
    /// <remarks>Use these constants to assign or check user roles in authentication and authorization
    /// scenarios. The values are intended to standardize role names across the system and reduce errors from hard-coded
    /// strings.</remarks>
    public class Roles
    {
        /// <summary>
        /// Represents the role name for administrative users.
        /// </summary>
        public const string ROLE_ADMIN = "ROLE_ADMIN";

        /// <summary>
        /// Represents the role name assigned to staff users.
        /// </summary>
        public const string ROLE_STAFF = "ROLE_STAFF";

        /// <summary>
        /// Represents the role name assigned to customer users.
        /// </summary>
        /// <remarks>Use this constant when assigning or checking user roles to ensure consistency
        /// throughout the application.</remarks>
        public const string ROLE_CUSTOMER = "ROLE_CUSTOMER";

        /// <summary>
        /// Represents the role name for an affiliate user.
        /// </summary>
        public const string ROLE_AFFILIATE = "ROLE_AFFILIATE";
    }
}
