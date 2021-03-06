﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChessboardControl;
using SUT = ChessboardControl.Board;

namespace UnitTest_Chessboard_Control
{
    [TestClass]
    public class UnitTest_Board
    {
        private static List<string> GetHalfMovesFromPGN(string pgn)
        {
            //  1.e4 e5 2.f4 exf4 3.Bc4 d5 4.Bxd5 Qh4+ 5.Kf1 g5 6.Nc3 Ne7
            var moves = new List<string>();
            var halfMoves = pgn.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var halfMove in halfMoves)
            {
                var index = halfMove.IndexOf('.');
                if (index != -1)
                {
                    moves.Add(halfMove.Substring(index + 1));
                }
                else
                {
                    moves.Add(halfMove);
                }
            }

            return moves;
        }

        
        public TestContext TestContext { get; set; }

        #region Constructors

        [TestMethod]
        public void Constructor_Should_InitializeWithInitialPosition()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            string fen = board.GetFEN();

            //	Assert
            Assert.AreEqual(SUT.INITIAL_FEN_POSITION, fen);
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

        #endregion Constructors

        [TestMethod]
        public void Ascii_Should_ReturnCorrectFormat()
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
        public void Ascii_Should_ReturnEmptyBoard()
        {
            //	Arrange
            SUT board = new SUT();
            string actual;

            //	Act
            board.Clear();
            actual = board.Ascii();

            //	Assert
            Assert.AreEqual("   +------------------------+\n" +
                " 8 | .  .  .  .  .  .  .  . |\n" +
                " 7 | .  .  .  .  .  .  .  . |\n" +
                " 6 | .  .  .  .  .  .  .  . |\n" +
                " 5 | .  .  .  .  .  .  .  . |\n" +
                " 4 | .  .  .  .  .  .  .  . |\n" +
                " 3 | .  .  .  .  .  .  .  . |\n" +
                " 2 | .  .  .  .  .  .  .  . |\n" +
                " 1 | .  .  .  .  .  .  .  . |\n" +
                "   +------------------------+\n" +
                "     a  b  c  d  e  f  g  h\n", actual);
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
        [DataRow("r3k2r/pppp1ppp/2n2n2/4p3/3PP3/2N2N2/PPP2PPP/R3K2R b KQkq - 0 4", ChessCastling.KingSide | ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("r3k2r/pppp1ppp/2n2n2/4p3/3PP3/2N2N2/PPP2PPP/R3K2R b Qkq - 0 4", ChessCastling.QueenSide, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("r3k2r/pppp1ppp/2n2n2/4p3/3PP3/2N2N2/PPP2PPP/R3K2R b kq - 0 4", ChessCastling.None, ChessCastling.KingSide | ChessCastling.QueenSide)]
        [DataRow("r3k2r/pppp1ppp/2n2n2/4p3/3PP3/2N2N2/PPP2PPP/R3K2R b q - 0 4", ChessCastling.None, ChessCastling.QueenSide)]
        [DataRow("r3k2r/pppp1ppp/2n2n2/4p3/3PP3/2N2N2/PPP2PPP/R3K2R b - - 0 4", ChessCastling.None, ChessCastling.None)]
        public void GetCastlingRights_Should_ReturnCorrectValues(string fenString, ChessCastling expectedForWhite, ChessCastling expectedForBlack)
        {
            //	Arrange
            SUT board;
            ChessCastling actualWhite;
            ChessCastling actualBlack;

            //	Act
            board = new SUT(fenString);
            actualWhite = board.GetCastlingRights(ChessColor.White);
            actualBlack = board.GetCastlingRights(ChessColor.Black);

            //	Assert
            Assert.AreEqual(expectedForWhite, actualWhite);
            Assert.AreEqual(expectedForBlack, actualBlack);
        }

        [TestMethod]
        [DataRow("rnbqkbnr/pppppppp/Q1Q5/8/Q1p5/8/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "a4", "c4", "Qa4xc4")]
        [DataRow("rnbqkbnr/pppppppp/Q1Q5/8/Q1p5/8/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "a6", "c4", "Qa6xc4")]
        [DataRow("rnbqkbnr/pppppppp/Q1Q5/8/Q1p5/8/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "c6", "c4", "Qc6xc4")]
        [DataRow("rnbqkbnr/pppppppp/3Q4/2Q5/Q2p4/8/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "a4", "d4", "Qaxd4")]
        [DataRow("rnbqkbnr/pppppppp/3Q4/2Q5/Q2p4/8/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "c5", "d4", "Qcxd4")]
        [DataRow("rnbqkbnr/pppppppp/3Q4/2Q5/Q2p4/8/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "d6", "d4", "Qdxd4")]
        [DataRow("rnbqkbnr/pppppppp/8/1N3N2/3p4/1N3N2/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "b5", "d4", "Nb5xd4")]
        [DataRow("rnbqkbnr/pppppppp/8/1N3N2/3p4/1N3N2/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "b3", "d4", "Nb3xd4")]
        [DataRow("rnbqkbnr/pppppppp/8/1N3N2/3p4/1N3N2/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "f5", "d4", "Nf5xd4")]
        [DataRow("rnbqkbnr/pppppppp/8/1N3N2/3p4/1N3N2/PPPPPPPP/RNB1KBNR w KQkq - 0 1", "f3", "d4", "Nf3xd4")]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "g1", "f3", "Nf3")]
        public void GetDisambiguator_Should_ReturnCorrectValue(string FENPosition, string from, string to, string expectedSAN)
        {
            //	Arrange
            SUT board = new SUT(FENPosition);
            ChessMove actualMove;

            //	Act
            actualMove = board.GetMoveValidity(new ChessSquare(from), new ChessSquare(to));

            //	Assert
            Assert.AreEqual(expectedSAN, actualMove.ToSAN);
        }

        [TestMethod]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [DataRow("rnbqkb1r/pp2pppp/5n2/3p4/2PP4/2N5/PP3PPP/R1BQKBNR b KQkq - 2 5")]
        [DataRow("rnbqk1nr/p1pp1ppp/8/1pP1p3/1b2P3/8/PP1P1PPP/RNBQKBNR w KQkq b6 0 4")]
        [DataRow("rnbqkbnr/pppp1p1p/8/4p3/B3P1pP/8/PPPP1PP1/RNBQK1NR b KQkq h3 0 4")]
        public void GetFEN_Should_Return_CorrectFenString(string expectedFEN)
        {
            //	Arrange
            SUT board;

            //	Act
            board = new SUT(expectedFEN);

            //	Assert
            Assert.AreEqual(expectedFEN, board.GetFEN());
        }

        [TestMethod]
        [DataRow("4k3/8/8/8/4K3/8/8/8 w - - 0 1", "e4", "d5,e5,f5,d4,f4,d3,e3,f3")]
        [DataRow("8/K7/8/8/4k3/8/8/8 b - - 0 1", "e4", "d5,e5,f5,d4,f4,d3,e3,f3")]
        [DataRow("8/K3N3/8/8/4k3/8/8/4N3 b - - 0 1", "e4", "e5,d4,f4,e3")]
        [DataRow("8/k3n3/8/8/4K3/8/8/4n3 w - - 0 1", "e4", "e5,d4,f4,e3")]
        [DataRow("8/8/8/8/4Q3/8/8/8 w - - 0 1", "e4", "a8,b7,c6,d5,f3,g2,h1,a4,b4,c4,d4,f4,g4,h4,h7,g6,f5,b1,c2,d3,e1,e2,e3,e5,e6,e7,e8")]
        [DataRow("8/8/8/8/4q3/8/8/8 b - - 0 1", "e4", "a8,b7,c6,d5,f3,g2,h1,a4,b4,c4,d4,f4,g4,h4,h7,g6,f5,b1,c2,d3,e1,e2,e3,e5,e6,e7,e8")]
        [DataRow("8/8/2P3P1/8/P3q2P/8/2P3P1/8 b - - 0 1", "e4", "c6,g6,a4,h4,c2,g2,d5,f3,b4,c4,d4,f4,g4,f5,d3,e1,e2,e3,e5,e6,e7,e8")]
        [DataRow("8/8/2p3p1/8/p3Q2p/8/2p3p1/8 w - - 0 1", "e4", "c6,g6,a4,h4,c2,g2,d5,f3,b4,c4,d4,f4,g4,f5,d3,e1,e2,e3,e5,e6,e7,e8")]
        [DataRow("8/4P3/8/8/P3r2P/8/8/4P3 b - - 0 1", "e4", "a4,b4,c4,d4,f4,g4,h4,e7,e6,e5,e3,e2,e1")]
        [DataRow("8/4p3/8/8/p3R2p/8/8/4p3 w - - 0 1", "e4", "a4,b4,c4,d4,f4,g4,h4,e7,e6,e5,e3,e2,e1")]
        [DataRow("8/7p/2p5/8/4B3/8/6p1/1p6 w - - 0 1", "e4", "c6,d5,f3,g2,b1,c2,d3,f5,g6,h7")]
        [DataRow("8/7P/2P5/8/4b3/8/6P1/1P6 b - - 0 1", "e4", "c6,d5,f3,g2,b1,c2,d3,f5,g6,h7")]
        [DataRow("8/8/8/8/4n3/8/8/8 b - - 0 1", "e4", "d6,f6,g5,g3,d2,f2,c3,c5")]
        [DataRow("8/8/8/8/4N3/8/8/8 w - - 0 1", "e4", "d6,f6,g5,g3,d2,f2,c3,c5")]
        public void GetLegalMoves_Should_ReturnAllLegalMoves(string fen, string from, string moves)
        {
            //	Arrange
            SUT board = new SUT(fen);
            List<ChessMove> legalMoves;
            List<string> expectedMoves = new List<string>();

            //	Act
            foreach (string move in moves.Split(new char[] { ',' }))
            {
                expectedMoves.Add(move);
            }
            legalMoves = board.GetLegalMoves();

            //	Assert
            Assert.AreEqual(expectedMoves.Count, legalMoves.Count);
            foreach (ChessMove legalMove in legalMoves)
            {
                if (from == legalMove.From.AlgebraicNotation)
                {
                    bool found = false;
                    foreach (string expectedMove in expectedMoves)
                    {
                        if (expectedMove == legalMove.To.AlgebraicNotation) { found = true; break; }
                    }
                    Assert.IsTrue(found);
                }
            }
        }

        [TestMethod]
        public void GetLegalMoves_Should_ReturnAllLegalMovesForInitialPosition()
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
        public void GetLegalMoves_Should_ReturnEmptyList()
        {
            //	Arrange
            SUT board = new SUT("8/4p3/p3k3/P2R1R2/4K1p1/6P1/8/8 b - - 0 1");
            List<ChessMove> legalMoves;

            //	Act
            legalMoves = board.GetLegalMoves();

            //	Assert
            Assert.AreEqual(0, legalMoves.Count);
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
        [ExpectedException(typeof(ArgumentException))]
        public void LoadFEN_Should_ThrowArgumentException()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.Clear();
            board.LoadFEN("rnbqkbnr/1p1p1ppp/p3p3/NP3/8/PPP2PPP/RNBQKB1R w KQkq - 0 5");
        }

        [TestMethod]
        public void Move_Should_Play_The_Game()
        {
            //	Arrange
            var testFile = new System.IO.FileInfo(@"..\..\..\..\Test-Materials\Games\Alekhine.pgn");

            //	Act
            using (var reader = testFile.OpenText())
            {
                var board = new Board();
                while (!reader.EndOfStream)
                {
                    var game = reader.ReadLine();
                    var halfMoves = GetHalfMovesFromPGN(game);
                    board.Reset();
                    foreach (var halfMove in halfMoves)
                    {
                        var currentMove = board.SANToMove(halfMove);
                        Assert.IsTrue(currentMove.IsValid);
                        board.Move(currentMove);
                    }
                }
            }
        }

        [TestMethod]
        [DataRow("r2qk2r/ppbn1pp1/2p1p3/2P2b1p/1P1PpP2/4P3/P2NB1PP/R1BQ1RK1 b kq f3 0 12", "g5")]
        [DataRow("r2qk2r/ppbn1pp1/2p1p3/2P2b1p/1P1PpP2/4P3/P2NB1PP/R1BQ1RK1 b kq f3 0 12", "Nb8")]
        [DataRow("r2qk2r/ppbn1pp1/2p1p3/2P2b1p/1P1PpP2/4P3/P2NB1PP/R1BQ1RK1 b kq f3 0 12", "exf3")]
        public void Move_Should_ResetEnPassant(string fen, string nextMove)
        {
            //  Arrange            
            var board = new SUT(fen);

            //  Act
            board.Move(board.SANToMove(nextMove));
            fen = board.GetFEN();

            //  Assert
            Assert.AreEqual("-", new FEN(fen).EnPassant);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Move_Should_ThrowException_When_FromIsNull()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act            
            board.Move(null, new ChessSquare("e4"));
        }

        [TestMethod]
        [ExpectedException(typeof(IllegalMoveException))]
        public void Move_Should_ThrowException_When_MoveIsInvalid()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            ChessMove invalidMove = board.GetMoveValidity(new ChessSquare("e2"), new ChessSquare("e5"));
            board.Move(invalidMove);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Move_Should_ThrowException_When_ToIsNull()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act            
            board.Move(new ChessSquare("e2"), null);
        }

        [TestMethod]
        public void Reset_Should_ResetAndSetupInitialPosition()
        {
            //	Arrange
            SUT board = new SUT("2rq1rk1/ppp2ppp/n3bn2/1B1pp1B1/1b1PP3/2N2N2/PPP1QPPP/2KRR3 w - - 0 1");

            //	Act
            board.Reset();

            //	Assert
            Assert.AreEqual(SUT.INITIAL_FEN_POSITION, board.GetFEN());
            Assert.AreEqual(ChessColor.White, board.Turn);
            Assert.AreEqual(ChessCastling.KingSide | ChessCastling.QueenSide, board.GetCastlingRights(ChessColor.White));
            Assert.AreEqual(ChessCastling.KingSide | ChessCastling.QueenSide, board.GetCastlingRights(ChessColor.Black));
        }

        [TestMethod]
        public void GetPieceAt_Should_ReturnThePiece()
        {
            //	Arrange
            SUT board = new SUT();

            //	Assert
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Rook, ChessColor.White) == board.GetPieceAt(new ChessSquare("a1")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Knight, ChessColor.White) == board.GetPieceAt(new ChessSquare("b1")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Bishop, ChessColor.White) == board.GetPieceAt(new ChessSquare("c1")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Queen, ChessColor.White) == board.GetPieceAt(new ChessSquare("d1")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.King, ChessColor.White) == board.GetPieceAt(new ChessSquare("e1")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Bishop, ChessColor.White) == board.GetPieceAt(new ChessSquare("f1")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Knight, ChessColor.White) == board.GetPieceAt(new ChessSquare("g1")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Rook, ChessColor.White) == board.GetPieceAt(new ChessSquare("h1")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Rook, ChessColor.Black) == board.GetPieceAt(new ChessSquare("a8")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Knight, ChessColor.Black) == board.GetPieceAt(new ChessSquare("b8")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Bishop, ChessColor.Black) == board.GetPieceAt(new ChessSquare("c8")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Queen, ChessColor.Black) == board.GetPieceAt(new ChessSquare("d8")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.King, ChessColor.Black) == board.GetPieceAt(new ChessSquare("e8")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Bishop, ChessColor.Black) == board.GetPieceAt(new ChessSquare("f8")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Knight, ChessColor.Black) == board.GetPieceAt(new ChessSquare("g8")));
            Assert.IsTrue(new ChessPiece(ChessPieceKind.Rook, ChessColor.Black) == board.GetPieceAt(new ChessSquare("h8")));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetPieceAt_Should_ThrowArgumentNullExecption()
        {
            //	Arrange
            SUT board = new SUT();

            //  Act
            board.GetPieceAt(null);
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
            board.PutPiece(piece, new ChessSquare(square));

            //	Assert
            Assert.IsTrue(piece == board.GetPieceAt(new ChessSquare(square)));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PutPiece_Should_ThrowException_When_PieceIsNull()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.PutPiece(null, new ChessSquare("a1"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PutPiece_Should_ThrowException_When_SquareIsNull()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.PutPiece(new ChessPiece(ChessPieceKind.Rook, ChessColor.Black), null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PutPiece_Should_ThrowException_When_ThereIsTwoKingOfTheSameColor()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.PutPiece(new ChessPiece(ChessPieceKind.King, ChessColor.Black), new ChessSquare("e5"));
        }

        [TestMethod]
        [DataRow("e2", ChessPieceKind.Pawn, ChessColor.White)]
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
        public void RemovePieceAt_Should_ReturnNull()
        {
            //	Arrange
            SUT board = new SUT();

            //	Assert
            Assert.IsNull(board.RemovePieceAt(new ChessSquare("e4")));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void RemovePieceAt_Should_ThrowArgumentNullException()
        {
            //	Arrange
            SUT board = new SUT();

            //	Act
            board.RemovePieceAt(null);

            //	Assert

        }

        [TestMethod]
        public void LegalMoves_Should_ReturnKingSideCastle()
        {
            //	Arrange            
            SUT board = new SUT("rnbqk2r/pppp1ppp/5n2/4p3/8/b7/8/4K2R w Kkq - 0 1");
            List<ChessMove> legalMoves;

            //	Act
            legalMoves = board.GetLegalMoves();

            //	Assert
            Assert.AreEqual(14, legalMoves.Count);
            bool found = false;
            foreach (ChessMove chessMove in legalMoves)
            {
                if (chessMove.MoveKind == ChessMoveType.KSide_Castle) { found = true; }
            }
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void LegalMoves_Should_ReturnQueenSideCastle()
        {
            //	Arrange            
            SUT board = new SUT("rnbqk2r/pppp1ppp/3b1n2/4p3/8/8/8/R3K3 w Qkq - 0 1");
            List<ChessMove> legalMoves;

            //	Act
            legalMoves = board.GetLegalMoves();

            //	Assert
            Assert.AreEqual(15, legalMoves.Count);
            bool found = false;
            foreach (ChessMove chessMove in legalMoves)
            {
                if (chessMove.MoveKind == ChessMoveType.QSide_Castle) { found = true; }
            }
            Assert.IsTrue(found);
        }

        [TestMethod]
        public void GetMoveHistory_Should_Return_AllTheMoves()
        {
            //	Arrange
            SUT board = new SUT();
            List<ChessMove> playedMoves = new List<ChessMove>();
            ChessMove[] moveHistory;
            int moveCount = 0;
            List<ChessMove> legalMoves = board.GetLegalMoves();
            System.Random randomizer = new System.Random(System.DateTime.Now.Second);

            //	Act
            do
            {
                var moveIndex = randomizer.Next(0, legalMoves.Count);
                board.Move(legalMoves[moveIndex]);
                playedMoves.Add(legalMoves[moveIndex]);

                legalMoves = board.GetLegalMoves();
                moveCount++;
            } while (legalMoves.Count > 0 && moveCount < 30);
            moveHistory = board.GetMoveHistory();

            //	Assert
            Assert.IsNotNull(moveHistory);
            Assert.AreEqual(playedMoves.Count, moveHistory.Length);
            for (int i = 0; i < moveHistory.Length; i++)
            {
                Assert.AreEqual(moveHistory[i].CapturedPiece, playedMoves[i].CapturedPiece);
                Assert.IsTrue(moveHistory[i].From == playedMoves[i].From);
                Assert.IsTrue(moveHistory[i].To == playedMoves[i].To);
                Assert.AreEqual(moveHistory[i].IsValid, playedMoves[i].IsValid);
                Assert.AreEqual(moveHistory[i].MoveKind, playedMoves[i].MoveKind);
                Assert.AreEqual(moveHistory[i].MovingPiece.Kind, playedMoves[i].MovingPiece.Kind);
                Assert.AreEqual(moveHistory[i].PromotedTo, playedMoves[i].PromotedTo);
                Assert.AreEqual(moveHistory[i].IllegalReason, playedMoves[i].IllegalReason);
            }
        }

        [TestMethod]
        public void GetMoveValidity_Should_Not_ThrowException_When_MoveIsInvalid()
        {
            //	Arrange
            var superBoard = new Board();
            ChessMove actualResult;

            //	Act
            superBoard.Reset();
            actualResult = superBoard.GetMoveValidity(
                new ChessSquare(ChessFile.e, ChessRank._2),
                new ChessSquare(ChessFile.e, ChessRank._5));

            //	Assert
            Assert.IsFalse(actualResult.IsValid);
        }

        [TestMethod]
        public void GetMoveValidity_Should_Return_False_When_NoPieceOnFromSquare()
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
        public void GetMoveValidity_Should_Return_False_When_NotRightTurn()
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

        [TestMethod]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")]
        [DataRow("1K6/8/8/2Pk4/8/8/8/8 w - - 0 1")]
        [DataRow("1K6/8/6B1/3k4/8/8/5B2/8 w - - 0 1")]
        [DataRow("1K6/8/6N1/3k4/8/8/5N2/8 w - - 0 1")]
        [DataRow("1K6/8/8/3k4/8/8/5R2/8 w - - 0 1")]
        [DataRow("1K6/8/8/3k4/8/8/5Q2/8 w - - 0 1")]
        public void InsufficientMaterial_Should_ReturnFalse(string fen)
        {
            //	Arrange
            SUT board = new SUT(fen);

            //	Assert
            Assert.IsFalse(board.IsDrawByInsufficientMaterial);
        }

        [TestMethod]
        [DataRow("1K6/8/8/3k4/8/8/8/8 w - - 0 1")]
        [DataRow("1K6/8/8/3k4/8/8/N7/8 w - - 0 1")]
        [DataRow("1K6/8/8/3k4/8/8/B7/8 w - - 0 1")]
        [DataRow("1K6/5b2/8/3k4/8/8/8/3B4 w - - 0 1")]
        public void InsufficientMaterial_Should_ReturnTrue(string fen)
        {
            //	Arrange
            SUT board = new SUT(fen);

            //	Assert
            Assert.IsTrue(board.IsDrawByInsufficientMaterial);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "O-O")]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1", "O-O-O")]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "O-O")]
        [DataRow("rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR b KQkq - 0 1", "O-O-O")]
        public void SANToMove_Should_ThrowArgumentException(string initialFEN, string san)
        {
            //	Arrange
            SUT board;

            //	Act
            board = new SUT(initialFEN);
            board.SANToMove(san);
        }
    }
}
