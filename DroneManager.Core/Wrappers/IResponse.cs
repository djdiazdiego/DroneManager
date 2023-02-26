using System.Collections.Generic;

namespace DroneManager.Core.Wrappers
{
    public interface IResponse : IBaseResponse
    {
        /// <summary>
        /// Error list
        /// </summary>
        List<string> Errors { get; }

        /// <summary>
        /// Response data
        /// </summary>
        object Data { get; }
    }
}
