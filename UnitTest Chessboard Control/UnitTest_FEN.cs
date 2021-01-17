using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessboardControl;
using SUT = ChessboardControl.FEN;

namespace UnitTest_Chessboard_Control
{
    [TestClass]
    public class UnitTest_FEN
    {
        [TestMethod]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "KQkq", "-", "0", "1", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("   rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "KQkq", "-", "0", "1", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("  rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR   ", "w ", " KQkq", "   -  ", " 0  ", "  1   ", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "b", "KQkq", "-", "0", "1", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "KQk", "-", "0", "1", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide )]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "KQ", "-", "0", "1", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.None)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "KQq", "-", "0", "1", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "Kkq", "-", "0", "1", ChessCastling.KingSide , ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "Qkq", "-", "0", "1", ChessCastling.QueenSide , ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "kq", "-", "0", "1", ChessCastling.None, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "q", "-", "0", "1", ChessCastling.None, ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "k", "-", "0", "1", ChessCastling.None, ChessCastling.KingSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "Kk", "-", "0", "1", ChessCastling.KingSide, ChessCastling.KingSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "Qq", "-", "0", "1", ChessCastling.QueenSide, ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "K", "-", "0", "1", ChessCastling.KingSide, ChessCastling.None)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "Q", "-", "0", "1", ChessCastling.QueenSide, ChessCastling.None )]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "KQkq", "g6", "4", "18", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr", "pppppppp", "8", "8", "8", "8", "PPPPPPPP", "RNBQKBNR", "w", "KQkq", "b3", "22", "134", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        public void Constructor_Should_Initialize(string rank1, string rank2, string rank3, string rank4, string rank5, string rank6, string rank7,string rank8, string turn, string castling,
            string enpassant, string halfMove, string moves , ChessCastling expectedCastlingForWhite, ChessCastling expectedCastlingForBlack)
        {
            //	Arrange
            SUT fenObj = new SUT($"{rank1}/{rank2}/{rank3}/{rank4}/{rank5}/{rank6}/{rank7}/{rank8} {turn} {castling} {enpassant} {halfMove} {moves}");

            //	Assert
            Assert.AreEqual(rank1.Trim(), fenObj.Ranks[0]);
            Assert.AreEqual(rank2.Trim(), fenObj.Ranks[1]);
            Assert.AreEqual(rank3.Trim(), fenObj.Ranks[2]);
            Assert.AreEqual(rank4.Trim(), fenObj.Ranks[3]);
            Assert.AreEqual(rank5.Trim(), fenObj.Ranks[4]);
            Assert.AreEqual(rank6.Trim(), fenObj.Ranks[5]);
            Assert.AreEqual(rank7.Trim(), fenObj.Ranks[6]);
            Assert.AreEqual(rank8.Trim(), fenObj.Ranks[7]);

            Assert.AreEqual(expectedCastlingForWhite, fenObj.CastlingForWhite);
            Assert.AreEqual(expectedCastlingForBlack, fenObj.CastlingForBlack);
            Assert.AreEqual(enpassant.Trim(), fenObj.EnPassant);
            Assert.AreEqual (int.Parse( halfMove), fenObj.HalfMoveCount);
            Assert.AreEqual(int.Parse(moves), fenObj.MoveCount);
            Assert.AreEqual(turn.Trim() == "w"? ChessColor.White: ChessColor.Black, fenObj.Turn);
        }

        [TestMethod]
        [DataRow(ChessPieceKind.None, ChessColor.White, "")]
        [DataRow(ChessPieceKind.Rook, ChessColor.White, "R")]
        [DataRow(ChessPieceKind.Knight, ChessColor.White, "N")]
        [DataRow(ChessPieceKind.Bishop, ChessColor.White, "B")]
        [DataRow(ChessPieceKind.Queen, ChessColor.White, "Q")]
        [DataRow(ChessPieceKind.King, ChessColor.White, "K")]
        [DataRow(ChessPieceKind.Rook, ChessColor.White, "R")]
        [DataRow(ChessPieceKind.None, ChessColor.Black, "")]
        [DataRow(ChessPieceKind.Knight, ChessColor.Black, "n")]
        [DataRow(ChessPieceKind.Bishop, ChessColor.Black, "b")]
        [DataRow(ChessPieceKind.Queen, ChessColor.Black, "q")]
        [DataRow(ChessPieceKind.King, ChessColor.Black, "k")]
        public void ChessPieceToFEN_Should_ReturnCorrectFENPiece(ChessPieceKind pieceKind, ChessColor pieceColor, string expectedFEN)
        {
            //	Assert
            Assert.AreEqual(expectedFEN, SUT.ChessPieceToFEN(new ChessPiece(pieceKind, pieceColor)));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void ChessPieceToFEN_Should_ThrowArgumentNullException_WhenPieceIsNull()
        {
            SUT.ChessPieceToFEN(null);
        }

        [TestMethod]
        [DataRow('p', ChessPieceKind.Pawn)]
        [DataRow('r', ChessPieceKind.Rook)]
        [DataRow('n', ChessPieceKind.Knight)]
        [DataRow('b', ChessPieceKind.Bishop)]
        [DataRow('q', ChessPieceKind.Queen)]
        [DataRow('k', ChessPieceKind.King)]
        [DataRow('P', ChessPieceKind.Pawn)]
        [DataRow('R', ChessPieceKind.Rook)]
        [DataRow('N', ChessPieceKind.Knight)]
        [DataRow('B', ChessPieceKind.Bishop)]
        [DataRow('Q', ChessPieceKind.Queen)]
        [DataRow('K', ChessPieceKind.King)]
        public void FenToChessPiece_Should_Return_ChessPieceKind(char piece, ChessPieceKind expectedResult)
        {
            //	Assert
            Assert.AreEqual(expectedResult, SUT.FenToChessPiece(piece));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentOutOfRangeException))]
        public void FenToChessPiece_Should_ThrowArgumentOutOfRangeException()
        {
            //	Act
            SUT.FenToChessPiece('v');
        }
    }
}
