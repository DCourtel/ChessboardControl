using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessboardControl;
using SUT = ChessboardControl.Chessboard;

namespace UnitTest_Chessboard_Control
{
    [TestClass]
    public class UnitTest_Board
    {
        [TestMethod]
        public void Constructor_Should_Create_An_Empty_Board()
        {
            //  Arrange
            Chessboard board;

            //  Act
            board = new SUT();

            //  Assert
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._3)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._4)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._5)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._6)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._8)));

            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._3)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._4)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._5)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._6)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._8)));

            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._3)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._4)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._5)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._6)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._8)));

            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._3)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._4)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._5)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._6)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._8)));

            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._3)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._4)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._5)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._6)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._8)));

            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._3)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._4)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._5)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._6)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._8)));

            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._3)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._4)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._5)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._6)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._8)));

            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._3)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._4)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._5)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._6)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.None, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._8)));
        }

        [TestMethod]
        [DataRow(XCoordinate.a, YCoordinate._1, ChessPiece.BlackBishop)]
        [DataRow(XCoordinate.a, YCoordinate._8, ChessPiece.WhiteBishop)]
        [DataRow(XCoordinate.h, YCoordinate._1, ChessPiece.BlackKnight)]
        [DataRow(XCoordinate.h, YCoordinate._8, ChessPiece.WhiteKnight)]
        [DataRow(XCoordinate.d, YCoordinate._4, ChessPiece.WhitePawn)]
        [DataRow(XCoordinate.e, YCoordinate._4, ChessPiece.WhitePawn)]
        [DataRow(XCoordinate.d, YCoordinate._5, ChessPiece.BlackPawn)]
        [DataRow(XCoordinate.e, YCoordinate._5, ChessPiece.BlackPawn)]
        public void Set_Should_Set_Piece(XCoordinate letter, YCoordinate digit, ChessPiece expectedPiece)
        {
            //	Arrange
            SUT board = new SUT();
            ChessPiece actualPiece;

            //	Act
            board.SetPieceAt(new BoardCoordinates(letter, digit), expectedPiece);
            actualPiece = board.GetPieceAt(new BoardCoordinates(letter, digit));

            //	Assert
            Assert.AreEqual(expectedPiece, actualPiece);
        }

        [TestMethod]
        public void SetupInitialPosition_Should_Add_Pieces()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.SetupInitialPosition();

            //	Assert
            Assert.AreEqual(ChessPiece.BlackRook, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._8)));
            Assert.AreEqual(ChessPiece.BlackKnight, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._8)));
            Assert.AreEqual(ChessPiece.BlackBishop, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._8)));
            Assert.AreEqual(ChessPiece.BlackQueen, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._8)));
            Assert.AreEqual(ChessPiece.BlackKing, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._8)));
            Assert.AreEqual(ChessPiece.BlackBishop, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._8)));
            Assert.AreEqual(ChessPiece.BlackKnight, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._8)));
            Assert.AreEqual(ChessPiece.BlackRook, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._8)));
            Assert.AreEqual(ChessPiece.BlackPawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.BlackPawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.BlackPawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.BlackPawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.BlackPawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.BlackPawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.BlackPawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._7)));
            Assert.AreEqual(ChessPiece.BlackPawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._7)));

            Assert.AreEqual(ChessPiece.WhiteRook, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.WhiteKnight, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.WhiteBishop, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.WhiteQueen, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.WhiteKing, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.WhiteBishop, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.WhiteKnight, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.WhiteRook, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._1)));
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.a, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.b, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.c, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.d, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.f, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.g, YCoordinate._2)));
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.h, YCoordinate._2)));
        }

        [TestMethod]
        [ExpectedException(typeof(ChessboardControl.Exceptions.InvalidCoordinatesException), AllowDerivedTypes = false)]
        public void MovePiece_Should_Throw_InvalideCoordinateException()
        {
            //	Arrange
            SUT board;

            //	Act
            board = new SUT();
            board.SetupInitialPosition();
            board.MovePiece(new BoardCoordinates(XCoordinate.e, YCoordinate._4), new BoardCoordinates(XCoordinate.e, YCoordinate._5));
        }

        [TestMethod]
        public void MovePiece_Should_Move_The_Piece()
        {
            //	Arrange
            SUT board;

            //	Act
            board = new SUT();
            board.SetupInitialPosition();
            board.MovePiece(new BoardCoordinates(XCoordinate.e, YCoordinate._2), new BoardCoordinates(XCoordinate.e, YCoordinate._4));

            //  Assert
            Assert.AreEqual(ChessPiece.WhitePawn, board.GetPieceAt(new BoardCoordinates(XCoordinate.e, YCoordinate._4)));
        }

        [TestMethod]
        public void MovePiece_Should_Move_Return_Captured_Piece()
        {
            //	Arrange
            SUT board;
            ChessPiece capturedPiece;

            //	Act
            board = new SUT();
            board.SetupInitialPosition();
            board.MovePiece(new BoardCoordinates(XCoordinate.e, YCoordinate._2), new BoardCoordinates(XCoordinate.e, YCoordinate._4));
            board.MovePiece(new BoardCoordinates(XCoordinate.d, YCoordinate._7), new BoardCoordinates(XCoordinate.d, YCoordinate._5));
            capturedPiece = board.MovePiece(new BoardCoordinates(XCoordinate.e, YCoordinate._4), new BoardCoordinates(XCoordinate.d, YCoordinate._5));

            //  Assert
            Assert.AreEqual(ChessPiece.BlackPawn, capturedPiece);
        }

        [TestMethod]
        public void MovePiece_Should_Move_Return_No_Piece()
        {
            //	Arrange
            SUT board;
            ChessPiece capturedPiece;

            //	Act
            board = new SUT();
            board.SetupInitialPosition();
            capturedPiece = board.MovePiece(new BoardCoordinates(XCoordinate.e, YCoordinate._2), new BoardCoordinates(XCoordinate.e, YCoordinate._4));

            //  Assert
            Assert.AreEqual(ChessPiece.None, capturedPiece);
        }
    }
}
