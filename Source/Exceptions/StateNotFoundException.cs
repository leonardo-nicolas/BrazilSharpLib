using System;
using System.Diagnostics;

namespace BrazilSharp.Exceptions
{
    /// <summary>Represents error when user set a invalid, unknown or non existents state.</summary>
    [DebuggerStepThrough]
    public class StateNotFoundException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="StateNotFoundException"/> class.</summary>
        public StateNotFoundException() { }
        
        /// <summary>Initializes a new instance of the <see cref="StateNotFoundException"/> class with a specified error message.</summary>
        /// <param name="message">The message that describes the error.</param>
        public StateNotFoundException(string message) : base(message) { }
        
        /// <summary>Initializes a new instance of the <see cref="StateNotFoundException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="inner">The exception that is the cause of the current exception, or a <see langword="null"/> reference (<see langword="Nothing"/> in Visual Basic) if no inner exception is specified.</param>
        public StateNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}