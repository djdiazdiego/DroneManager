using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace DroneManager.Core.Exceptions
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidationException() : base("One or more validation failures have occurred.") =>
            Errors = new Dictionary<string, string>();

        /// <summary>
        /// Validation errors.
        /// </summary>
        public Dictionary<string, string> Errors { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="failures"></param>
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            foreach (var failure in failures)
                Errors.Add(failure.PropertyName, failure.ErrorMessage);
        }

    }
}
