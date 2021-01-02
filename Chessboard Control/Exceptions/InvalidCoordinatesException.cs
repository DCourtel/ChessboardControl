using System;

namespace ChessboardControl.Exceptions
{
    public class InvalidCoordinatesException : Exception
    {
        public InvalidCoordinatesException(string parameterName)
        {
            ParameterName = parameterName;
        }

        public InvalidCoordinatesException(string parameterName, string message)
        {
            ParameterName = parameterName;
            Message = message;
        }

        public string ParameterName { get; } = string.Empty;

        public override string Message { get; } = string.Empty;
    }
}
