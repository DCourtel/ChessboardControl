namespace ChessboardControl
{
    public class FENValidationResult
    {
        public FENValidationResult() { }

        public FENValidationResult(string errMessage)
        {
            this.ErrorMessage = errMessage;
        }

        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
