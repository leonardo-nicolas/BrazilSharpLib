using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace BrazilSharp.Exceptions
{
    /// <summary>Represents error when user set a invalid, unknown or non existents state.</summary>
    [DebuggerStepThrough]
    [Serializable]
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

        /// <summary>Initializes a new instance of the <see cref="StateNotFoundException"/> class with serialized data.</summary>
        /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
        protected StateNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}