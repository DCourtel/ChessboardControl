using System.Collections.Generic;

namespace ChessboardControl
{
    internal class BoardState
    {
        public ChessMove Move { get; set; }
        public Dictionary<ChessColor, int> Kings { get; set; }
        public ChessColor Turn { get; set; }
        public Dictionary<ChessColor, ChessCastling> Castling { get; set; }
        public int EP_Square { get; set; }
        public int Half_MoveCount { get; set; }
        public int MoveCount { get; set; }
    }
}
