using System;

namespace DroneManager.Core.Settings
{
    public class SqlServerSettings
    {
        /// <summary>
        /// Migrations assembly name
        /// </summary>
        public MigrationsAssembly MigrationsAssemblyName { get; set; }

        /// <summary>
        /// The maximum number of retry attempts
        /// </summary>
        public int MaxRetryCount { get; set; }

        /// <summary>
        /// The maximum delay between retries
        /// </summary>
        public TimeSpan MaxRetryDelay { get; set; }

        /// <summary>
        /// Additional SQL error numbers that should be considered transient
        /// </summary>
        public int[] ErrorNumbersToAdd { get; set; }

        public class MigrationsAssembly
        {
            /// <summary>
            /// Project assembly name
            /// </summary>
            public string Project { get; set; }

            /// <summary>
            /// Identity assembly name
            /// </summary>
            public string Identity { get; set; }
        }
    }
}
