using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessboardControl;
using SUT = ChessboardControl.Board;

namespace UnitTest_Chessboard_Control
{
    [TestClass]
    public class UnitTest_Board
    {
        [TestMethod]
        public void Constructor_Should_InitializeWithInitialPosition()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            string fen = board.GetFEN();

            //	Assert
            Assert.AreEqual(SUT.DEFAULT_FEN_POSITION, fen);
        }

        [TestMethod]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "w", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("r1bqkbnr/pp1npppp/2p5/8/3PN3/8/PPP2PPP/R1BQKBNR w KQkq - 1 5", "w", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("r1bqkb1r/pppp1ppp/2n2n2/4p3/4P3/2N2N2/PPPP1PPP/R1BQKB1R w KQkq - 4 4", "w", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("rnbqkb1r/ppp2ppp/4pn2/3p2B1/3PP3/2N5/PPP2PPP/R2QKBNR b KQkq - 0 5", "b", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPPKPPP/RNBQ1BNR b kq - 1 2", "b", ChessCastling.None, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("2rq1rk1/ppp2ppp/n3bn2/1B1pp1B1/1b1PP3/2N2N2/PPP1QPPP/R3K2R w KQ - 0 1", "w", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.None)]
        [DataRow("2rq1rk1/ppp2ppp/n3bn2/1B1pp1B1/1b1PP3/2N2N2/PPP1QPPP/2KRR3 w - - 0 1", "w", ChessCastling.None, ChessCastling.None)]
        public void Constructor_Should_InitializeWithGivenPosition(string expectedFen,
            string expectedSide,
            ChessCastling expectedCastlingForWhite,
            ChessCastling expectedCastlingForBlack)
        {
            //	Arrange
            SUT board = new SUT(expectedFen);

            //	Act
            string fen = board.GetFEN();

            //	Assert
            Assert.AreEqual(expectedFen, fen);
            Assert.AreEqual(expectedSide, board.Turn == ChessColor.White ? "w" : "b");
            Assert.AreEqual(expectedCastlingForWhite, board.GetCastlingRights(ChessColor.White));
            Assert.AreEqual(expectedCastlingForBlack, board.GetCastlingRights(ChessColor.Black));
        }


        [TestMethod]
        public void Clear_Should_ResetTheState()
        {
            //	Arrange
            SUT board = new SUT("rnbqkbnr/pppp1ppp/8/4p3/4P3/8/PPPPKPPP/RNBQ1BNR b kq - 1 2");

            //	Act
            board.Clear();

            //	Assert
            Assert.AreEqual("8/8/8/8/8/8/8/8 w - - 0 1", board.GetFEN());
            Assert.AreEqual(ChessColor.White, board.Turn);
            Assert.AreEqual(ChessCastling.None, board.GetCastlingRights(ChessColor.White));
            Assert.AreEqual(ChessCastling.None, board.GetCastlingRights(ChessColor.Black));
        }

        [TestMethod]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [DataRow("rnbqk2r/pppp1ppp/5n2/2b1p3/2B1P3/5N2/PPPP1PPP/RNBQK2R w KQkq - 0 1")]
        [DataRow("rnbqk2r/pppp1ppp/5n2/2b1p3/2B1P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 0 1")]
        [DataRow("1n3rk1/pppp1ppp/5n2/1qb1p3/P1B1P3/5N2/2PP1PPP/R1BQ1K2 w - - 0 1")]
        [DataRow("r1bqkbnr/1ppp1ppp/p1B5/4p3/4P3/5N2/PPPP1PPP/RNBQK2R b KQkq - 0 4")]
        [DataRow("rnbqkbnr/1p1p1ppp/p3p3/8/3NP3/8/PPP2PPP/RNBQKB1R w KQkq - 0 5")]
        public void LoadFEN_Should_InitializeTheBoard(string expectedFEN)
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.Clear();
            board.LoadFEN(expectedFEN);

            //	Assert
            Assert.AreEqual(expectedFEN, board.GetFEN());
        }

        [TestMethod]
        public void Reset_Should_ResetAndSetupInitialPosition()
        {
            //	Arrange
            SUT board = new SUT("2rq1rk1/ppp2ppp/n3bn2/1B1pp1B1/1b1PP3/2N2N2/PPP1QPPP/2KRR3 w - - 0 1");

            //	Act
            board.Reset();

            //	Assert
            Assert.AreEqual(SUT.DEFAULT_FEN_POSITION, board.GetFEN());
            Assert.AreEqual(ChessColor.White, board.Turn);
            Assert.AreEqual(ChessCastling.KingSide | ChessCastling.QueenSide, board.GetCastlingRights(ChessColor.White));
            Assert.AreEqual(ChessCastling.KingSide | ChessCastling.QueenSide, board.GetCastlingRights(ChessColor.Black));
        }

        [TestMethod]
        public void GetPieceAt_Should_ReturnsThePiece()
        {
            //	Arrange
            SUT board = new SUT();

            //	Assert
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Rook, ChessColor.White).Equals(board.GetPieceAt(new ChessSquare("a1"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Knight, ChessColor.White).Equals(board.GetPieceAt(new ChessSquare("b1"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Bishop, ChessColor.White).Equals(board.GetPieceAt(new ChessSquare("c1"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Queen, ChessColor.White).Equals(board.GetPieceAt(new ChessSquare("d1"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.King, ChessColor.White).Equals(board.GetPieceAt(new ChessSquare("e1"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Bishop, ChessColor.White).Equals(board.GetPieceAt(new ChessSquare("f1"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Knight, ChessColor.White).Equals(board.GetPieceAt(new ChessSquare("g1"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Rook, ChessColor.White).Equals(board.GetPieceAt(new ChessSquare("h1"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Rook, ChessColor.Black).Equals(board.GetPieceAt(new ChessSquare("a8"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Knight, ChessColor.Black).Equals(board.GetPieceAt(new ChessSquare("b8"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Bishop, ChessColor.Black).Equals(board.GetPieceAt(new ChessSquare("c8"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Queen, ChessColor.Black).Equals(board.GetPieceAt(new ChessSquare("d8"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.King, ChessColor.Black).Equals(board.GetPieceAt(new ChessSquare("e8"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Bishop, ChessColor.Black).Equals(board.GetPieceAt(new ChessSquare("f8"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Knight, ChessColor.Black).Equals(board.GetPieceAt(new ChessSquare("g8"))));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Rook, ChessColor.Black).Equals(board.GetPieceAt(new ChessSquare("h8"))));
        }

        [TestMethod]
        [DataRow(ChessPieceKind.Rook, ChessColor.White, "e4")]
        [DataRow(ChessPieceKind.Rook, ChessColor.Black, "e5")]
        public void PutPiece_Should_PutThePieceOnTheBoard(ChessPieceKind kindOfPiece, ChessColor pieceColor, string square)
        {
            //	Arrange
            SUT board = new SUT();
            ChessPiece piece = new ChessPiece(kindOfPiece, pieceColor);

            //	Act
            board.PutPiece(piece, square);

            //	Assert
            Assert.IsTrue(piece.Equals(board.GetPieceAt(new ChessSquare(square))));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void PutPiece_Should_ThrowException_When_PieceIsNull()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.PutPiece(null, "a1");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void PutPiece_Should_ThrowException_When_SquareIsNull()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.PutPiece(new ChessPiece( ChessPieceKind.Rook, ChessColor.Black), null);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void PutPiece_Should_ThrowException_When_SquareIsOutsideOfTheBoard()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.PutPiece(new ChessPiece(ChessPieceKind.Rook, ChessColor.Black), "a9");
        }


        [TestMethod]
        [DataRow("e2", ChessPieceKind.Pawn , ChessColor.White)]
        [DataRow("a1", ChessPieceKind.Rook, ChessColor.White)]
        [DataRow("b1", ChessPieceKind.Knight, ChessColor.White)]
        [DataRow("c1", ChessPieceKind.Bishop, ChessColor.White)]
        [DataRow("d1", ChessPieceKind.Queen, ChessColor.White)]
        [DataRow("e1", ChessPieceKind.King, ChessColor.White)]
        [DataRow("a7", ChessPieceKind.Pawn, ChessColor.Black)]
        [DataRow("a8", ChessPieceKind.Rook, ChessColor.Black)]
        public void RemovePieceAt_Should_RemoveThePiece(string square, ChessPieceKind expectedPiece, ChessColor expectedColor)
        {
            //	Arrange
            SUT board = new SUT();
            ChessPiece actualPiece;

            //	Act
            actualPiece = board.RemovePieceAt(new ChessSquare(square));

            //	Assert
            Assert.AreEqual(expectedPiece, actualPiece.Kind);
            Assert.AreEqual(expectedColor, actualPiece.Color);
        }


        [TestMethod]
        public void RemovePieceAt_Should_ReturnsNull()
        {
            //	Arrange
            SUT board = new SUT();

            //	Assert
            Assert.IsNull(board.RemovePieceAt(new ChessSquare("e4")));
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void RemovePieceAt_Should_ThrowArgumentNullException()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.RemovePieceAt(null);

            //	Assert

        }

        [TestMethod]
        public void Ascii_Should_ReturnsCorrectFormat()
        {
            //	Arrange
            SUT board = new SUT();
            string actual;

            //	Act
            actual = board.Ascii();

            //	Assert
            Assert.AreEqual("   +------------------------+\n" +
                " 8 | r  n  b  q  k  b  n  r |\n" +
                " 7 | p  p  p  p  p  p  p  p |\n" +
                " 6 | .  .  .  .  .  .  .  . |\n" +
                " 5 | .  .  .  .  .  .  .  . |\n" +
                " 4 | .  .  .  .  .  .  .  . |\n" +
                " 3 | .  .  .  .  .  .  .  . |\n" +
                " 2 | P  P  P  P  P  P  P  P |\n" +
                " 1 | R  N  B  Q  K  B  N  R |\n" +
                "   +------------------------+\n" +
                "     a  b  c  d  e  f  g  h\n", actual);
        }

        [TestMethod]
        public void GetLegalMoves_Should_ReturnsAllLegalMoves()
        {
            //	Arrange
            SUT board = new SUT();
            List<ChessMove> legalMoves;

            //	Act
            legalMoves = board.GetLegalMoves();

            //	Assert
            Assert.AreEqual(20, legalMoves.Count);
        }

        [TestMethod]
        public void IsMoveLegal_Should_Return_False_When_NoPieceOnFromSquare()
        {
            //	Arrange
            var superBoard = new Board();
            ChessMove actualResult;

            //	Act
            superBoard.Reset();
            actualResult = superBoard.GetMoveValidity(
                new ChessSquare(ChessFile.e, ChessRank._4),
                new ChessSquare(ChessFile.e, ChessRank._5));

            //	Assert
            Assert.IsFalse(actualResult.IsValid);
            Assert.AreEqual(ChessMoveRejectedReason.NoPieceOnTheSquare, actualResult.IllegalReason);
        }

        [TestMethod]
        public void IsMoveLegal_Should_Return_False_When_NotRightTurn()
        {
            //	Arrange
            var superBoard = new Board();
            ChessMove actualResult;

            //	Act
            superBoard.Reset();
            actualResult = superBoard.GetMoveValidity(
                new ChessSquare(ChessFile.e, ChessRank._7),
                new ChessSquare(ChessFile.e, ChessRank._5));

            //	Assert
            Assert.IsFalse(actualResult.IsValid);
            Assert.AreEqual(ChessMoveRejectedReason.NotYourTurn, actualResult.IllegalReason);
        }


        //  Pawn - Legal Moves

        [TestMethod]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "a2", "a3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "b2", "b3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "c2", "c3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "d2", "d3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "e2", "e3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "f2", "f3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "g2", "g3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "h2", "h3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "a2", "a4", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "b2", "b4", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "c2", "c4", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "d2", "d4", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "e2", "e4", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "f2", "f4", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "g2", "g4", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "h2", "h4", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a7", "a6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b7", "b6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c7", "c6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d7", "d6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e7", "e6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f7", "f6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g7", "g6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h7", "h6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a7", "a5", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b7", "b5", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c7", "c5", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d7", "d5", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e7", "e5", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f7", "f5", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g7", "g5", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h7", "h5", ChessMoveType.Big_Pawn)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "a3", "a4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "b3", "b4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "c3", "c4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "d3", "d4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "e3", "e4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "f3", "f4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "g3", "g4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "h3", "h4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "a4", "a5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "b4", "b5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "c4", "c5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "d4", "d5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "e4", "e5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "f4", "f5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "g4", "g5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "h4", "h5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/PPPPPPPP/8/8/8/RNBQKBNR w KQkq - 0 1", "a5", "a6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/PPPPPPPP/8/8/8/RNBQKBNR w KQkq - 0 1", "b5", "b6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/PPPPPPPP/8/8/8/RNBQKBNR w KQkq - 0 1", "c5", "c6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/PPPPPPPP/8/8/8/RNBQKBNR w KQkq - 0 1", "d5", "d6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/PPPPPPPP/8/8/8/RNBQKBNR w KQkq - 0 1", "e5", "e6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/PPPPPPPP/8/8/8/RNBQKBNR w KQkq - 0 1", "f5", "f6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/PPPPPPPP/8/8/8/RNBQKBNR w KQkq - 0 1", "g5", "g6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/pppppppp/8/PPPPPPPP/8/8/8/RNBQKBNR w KQkq - 0 1", "h5", "h6", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a6", "a5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b6", "b5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c6", "c5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d6", "d5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e6", "e5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f6", "f5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g6", "g5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h6", "h5", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a5", "a4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b5", "b4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c5", "c4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d5", "d4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e5", "e4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f5", "f4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g5", "g4", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h4", "h3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a4", "a3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b4", "b3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c4", "c3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d4", "d3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e4", "e3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f4", "f3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g4", "g3", ChessMoveType.Normal)]
        [DataRow("rnbqkbnr/8/8/8/pppppppp/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h4", "h3", ChessMoveType.Normal)]
        public void IsMoveLegal_Should_Returns_True_When_PawnMoveIsLegal(
            string fen,
            string from,
            string to,
            ChessMoveType moveType)
        {
            //	Arrange
            var superBoard = new Board();
            ChessMove actualResult;

            //	Act
            superBoard.LoadFEN(fen);
            actualResult = superBoard.GetMoveValidity(
                new ChessSquare(from),
                new ChessSquare(to));

            //	Assert
            Assert.IsTrue(actualResult.IsValid);
            Assert.AreEqual(ChessPieceKind.Pawn, actualResult.MovingPiece);
            Assert.AreEqual(moveType, actualResult.MoveKind);
        }

        //  Pawn - Legal Captures

        [TestMethod]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "a6", "b7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "b6", "c7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "c6", "d7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "d6", "e7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "e6", "f7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "f6", "g7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "g6", "h7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "b6", "a7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "c6", "b7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "d6", "c7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "e6", "d7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "f6", "e7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "g6", "f7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppppp/PPPPPPPP/8/8/8/8/RNBQKBNR w KQkq - 0 1", "h6", "g7", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "a7", "b8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Knight)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "b7", "c8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Bishop)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "c7", "d8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Queen)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "e7", "f8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Bishop)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "f7", "g8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Knight)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "g7", "h8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Rook)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "b7", "a8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Rook)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "c7", "b8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Knight)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "d7", "c8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Bishop)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "e7", "d8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Queen)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "g7", "f8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Bishop)]
        [DataRow("rnbq1bnr/PPPPPPPP/8/4k3/8/8/8/RNBQKBNR w KQ - 0 1", "h7", "g8", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Knight)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a3", "b2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b3", "c2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c3", "d2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d3", "e2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e3", "f2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f3", "g2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g3", "h2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b3", "a2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c3", "b2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d3", "c2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e3", "d2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f3", "e2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g3", "f2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/8/8/pppppppp/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h3", "g2", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "a2", "b1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Knight)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "b2", "c1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Bishop)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "c2", "d1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Queen)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "e2", "f1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Bishop)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "f2", "g1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Knight)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "g2", "h1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Rook)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "b2", "a1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Rook)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "c2", "b1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Knight)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "d2", "c1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Bishop)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "e2", "d1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Queen)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "g2", "f1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Bishop)]
        [DataRow("rnbqkbnr/8/8/4K3/8/8/pppppppp/RNBQ1BNR b kq - 0 1", "h2", "g1", ChessMoveType.Capture | ChessMoveType.Promotion, ChessPieceKind.Knight)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "a4", "b5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "b4", "c5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "c4", "d5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "d4", "e5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "e4", "f5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "f4", "g5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "g4", "h5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "b4", "a5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "c4", "b5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "d4", "c5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "e4", "d5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "f4", "e5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "g4", "f5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "h4", "g5", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "a5", "b4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "b5", "c4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "c5", "d4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "d5", "e4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "e5", "f4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "f5", "g4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "g5", "h4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "b5", "a4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "c5", "b4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "d5", "c4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "e5", "d4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "f5", "e4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "g5", "f4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/8/8/pppppppp/PPPPPPPP/8/8/RNBQKBNR b KQkq - 0 1", "h5", "g4", ChessMoveType.Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/p1pppppp/8/Pp6/8/8/1PPPPPPP/RNBQKBNR w KQkq b6 0 1", "a5", "b6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/1ppppppp/8/pP6/8/8/P1PPPPPP/RNBQKBNR w KQkq a6 0 1", "b5", "a6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pp1ppppp/8/1Pp5/8/8/P1PPPPPP/RNBQKBNR w KQkq c6 0 1", "b5", "c6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/p1pppppp/8/1pP5/8/8/PP1PPPPP/RNBQKBNR w KQkq b6 0 1", "c5", "b6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppp1pppp/8/2Pp4/8/8/PP1PPPPP/RNBQKBNR w KQkq d6 0 1", "c5", "d6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pp1ppppp/8/2pP4/8/8/PPP1PPPP/RNBQKBNR w KQkq c6 0 1", "d5", "c6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppp1ppp/8/3Pp3/8/8/PPP1PPPP/RNBQKBNR w KQkq e6 0 1", "d5", "e6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppp1pppp/8/3pP3/8/8/PPPP1PPP/RNBQKBNR w KQkq d6 0 1", "e5", "d6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppppp1pp/8/4Pp2/8/8/PPPP1PPP/RNBQKBNR w KQkq f6 0 1", "e5", "f6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppp1ppp/8/4pP2/8/8/PPPPP1PP/RNBQKBNR w KQkq e6 0 1", "f5", "e6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppp1p/8/5Pp1/8/8/PPPPP1PP/RNBQKBNR w KQkq g6 0 1", "f5", "g6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppppp1pp/8/5pP1/8/8/PPPPPP1P/RNBQKBNR w KQkq f6 0 1", "g5", "f6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppppppp1/8/6Pp/8/8/PPPPPP1P/RNBQKBNR w KQkq h6 0 1", "g5", "h6", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/1ppppppp/8/8/pP6/8/P1PPPPPP/RNBQKBNR b KQkq b3 0 1", "a4", "b3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/p1pppppp/8/8/Pp6/8/1PPPPPPP/RNBQKBNR b KQkq a3 0 1", "b4", "a3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/p1pppppp/8/8/1pP5/8/PP1PPPPP/RNBQKBNR b KQkq c3 0 1", "b4", "c3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pp1ppppp/8/8/1Pp5/8/P1PPPPPP/RNBQKBNR b KQkq b3 0 1", "c4", "b3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pp1ppppp/8/8/2pP4/8/PPP1PPPP/RNBQKBNR b KQkq d3 0 1", "c4", "d3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppp1pppp/8/8/2Pp4/8/PP1PPPPP/RNBQKBNR b KQkq c3 0 1", "d4", "c3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppp1pppp/8/8/3pP3/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", "d4", "e3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppp1ppp/8/8/3Pp3/8/PPP1PPPP/RNBQKBNR b KQkq d3 0 1", "e4", "d3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppp1ppp/8/8/4pP2/8/PPPPP1PP/RNBQKBNR b KQkq f3 0 1", "e4", "f3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppppp1pp/8/8/4Pp2/8/PPPP1PPP/RNBQKBNR b KQkq e3 0 1", "f4", "e3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppppp1pp/8/8/5pP1/8/PPPPPP1P/RNBQKBNR b KQkq g3 0 1", "f4", "g3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppp1p/8/8/5Pp1/8/PPPPP1PP/RNBQKBNR b KQkq f3 0 1", "g4", "f3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/pppppp1p/8/8/6pP/8/PPPPPPP1/RNBQKBNR b KQkq h3 0 1", "g4", "h3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        [DataRow("rnbqkbnr/ppppppp1/8/8/6Pp/8/PPPPPP1P/RNBQKBNR b KQkq g3 0 1", "h4", "g3", ChessMoveType.EP_Capture, ChessPieceKind.Pawn)]
        public void IsMoveLegal_Should_Returns_True_When_PawnCaptureIsLegal(
            string fen,
            string from,
            string to,
            ChessMoveType moveType,
            ChessPieceKind capturedPiece)
        {
            //	Arrange
            var superBoard = new Board();
            ChessMove actualResult;

            //	Act
            superBoard.LoadFEN(fen);
            actualResult = superBoard.GetMoveValidity(
                new ChessSquare(from),
                new ChessSquare(to));

            //	Assert
            Assert.IsTrue(actualResult.IsValid);
            Assert.AreEqual(ChessPieceKind.Pawn, actualResult.MovingPiece);
            Assert.AreEqual(capturedPiece, actualResult.CapturedPiece);
            Assert.AreEqual(moveType, actualResult.MoveKind);
        }

        //  Pawn - Illegal Moves

        [TestMethod]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "a2", "a5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "b2", "b5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "c2", "c5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "d2", "d5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "e2", "e5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "f2", "f5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "g2", "g5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "h2", "h5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "a2", "a6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "b2", "b6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "c2", "c6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "d2", "d6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "e2", "e6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "f2", "f6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "g2", "g6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "h2", "h6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "a3", "a5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "b3", "b5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "c3", "c5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "d3", "d5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "e3", "e5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "f3", "f5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "g3", "g5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "h3", "h5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "a3", "a6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "b3", "b6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "c3", "c6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "d3", "d6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "e3", "e6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "f3", "f6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "g3", "g6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/PPPPPPPP/8/RNBQKBNR w KQkq - 0 1", "h3", "h6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "a4", "a6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "b4", "b6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "c4", "c6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "d4", "d6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "e4", "e6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "f4", "f6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "g4", "g6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "h4", "h6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "a4", "a3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "b4", "b3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "c4", "c3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "d4", "d3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "e4", "e3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "f4", "f3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "g4", "g3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "h4", "h3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "a4", "a2", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "b4", "b2", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "c4", "c2", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "d4", "d2", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "e4", "e2", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "f4", "f2", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "g4", "g2", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/PPPPPPPP/8/8/RNBQKBNR w KQkq - 0 1", "h4", "h2", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a7", "a4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b7", "b4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c7", "c4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d7", "d4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e7", "e4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f7", "f4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g7", "g4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h7", "h4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a7", "a3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b7", "b3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c7", "c3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d7", "d3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e7", "e3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f7", "f3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g7", "g3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h7", "h3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a6", "a4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b6", "b4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c6", "c4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d6", "d4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e6", "e4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f6", "f4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g6", "g4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h6", "h4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a6", "a3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b6", "b3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c6", "c3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d6", "d3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e6", "e3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f6", "f3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g6", "g3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/pppppppp/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h6", "h3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a5", "a3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b5", "b3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c5", "c3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d5", "d3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e5", "e3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f5", "f3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g5", "g3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h5", "h3", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a5", "a6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b5", "b6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c5", "c6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d5", "d6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e5", "e6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f5", "f6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g5", "g6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h5", "h6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "a5", "a7", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "b5", "b7", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "c5", "c7", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "d5", "d7", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "e5", "e7", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "f5", "f7", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "g5", "g7", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/8/8/pppppppp/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "h5", "h7", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "a2", "c4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "a2", "d5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "a2", "e6", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "h2", "f4", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "h2", "e5", ChessMoveRejectedReason.NotMovingLikeThis)]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "h2", "d6", ChessMoveRejectedReason.NotMovingLikeThis)]
        public void IsMoveLegal_Should_Returns_False_When_PawnMoveIsIllegal(
            string fen,
            string from,
            string to,
            ChessMoveRejectedReason rejectedReason)
        {
            //	Arrange
            var superBoard = new Board();
            ChessMove actualResult;

            //	Act
            superBoard.LoadFEN(fen);
            actualResult = superBoard.GetMoveValidity(
                new ChessSquare(from),
                new ChessSquare(to));

            //	Assert
            Assert.IsFalse(actualResult.IsValid);
            Assert.AreEqual(ChessPieceKind.Pawn, actualResult.MovingPiece);
            Assert.AreEqual(rejectedReason, actualResult.IllegalReason);
        }
    }
}
