namespace Nop.Core.Domain.Security
{
    /// <summary>
    /// Represents default values related to robots.txt
    /// </summary>
    public partial class RobotsTxtDefaults
    {
        /// <summary>
        /// Gets a system name of 'administrators' customer role
        /// </summary>
        public static string RobotsCustomFileName => "robots.custom.txt";

        /// <summary>
        /// Gets a system name of 'administrators' customer role
        /// </summary>
        public static string RobotsAdditionsFileName => "robots.additions.txt";
    }
}
