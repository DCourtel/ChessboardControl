namespace ChessboardControl
{
    public class BoardCoordinates
    {
        public BoardCoordinates(XCoordinate letter, YCoordinate digit)
        {
            Letter = letter;
            Digit = digit;
        }

        public XCoordinate Letter { get; set; }
        public YCoordinate Digit { get; set; }

        /// <summary>
        /// Gets whether the coordinates is on the board.
        /// </summary>
        /// <returns></returns>
        public bool IsOnBoard
        {
            get { return (Letter != XCoordinate.None && Digit != YCoordinate.None); }
        }

        /// <summary>
        /// Returns the coordinates in an human readable format.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">Thrown if the coordinates are outside of the board.
        /// Use <see cref="IsOnBoard"/> to check if the coordinates are valid.</exception>
        public override string ToString()
        {
            if(!IsOnBoard) { throw new System.ArgumentOutOfRangeException(); }
            
            return $"{Letter}{Digit.ToString().Substring(1)}";
        }
    }
}
