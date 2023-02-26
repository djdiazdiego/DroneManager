using System.Collections.Generic;

namespace DroneManager.Core.Wrappers
{
    public interface IValidationResponse : IBaseResponse
    {
        /// <summary>
        /// Error dictionary
        /// </summary>
        Dictionary<string, string> Errors { get; }
    }
}
