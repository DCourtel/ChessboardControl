using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SUT = ChessboardControl.ChessSquare;

namespace UnitTest_Chessboard_Control
{
    [TestClass]
    public class UnitTest_ChessSquare
    {
        #region Constructor

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_Should_Throw_ArgumentNullException()
        {
            //  Arrange
            new SUT(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        [DataRow("")]
        [DataRow("e")]
        [DataRow("2")]
        [DataRow("e2e4")]
        public void Constructor_Should_Throw_ArgumentException(string input)
        {
            //  Arrange
            new SUT(input);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        [DataRow("i2")]
        [DataRow("22")]
        [DataRow("aa")]
        [DataRow("a9")]
        [DataRow("b0")]
        public void Constructor_Should_Throw_ArgumentOutOfRangeException(string input)
        {
            //  Arrange
            new SUT(input);
        }

        #endregion Constructor

        #region Properties

        [TestMethod]
        [DataRow(ChessboardControl.ChessFile.a, ChessboardControl.ChessRank._2, "a2")]
        [DataRow(ChessboardControl.ChessFile.b, ChessboardControl.ChessRank._3, "b3")]
        [DataRow(ChessboardControl.ChessFile.c, ChessboardControl.ChessRank._4, "c4")]
        [DataRow(ChessboardControl.ChessFile.d, ChessboardControl.ChessRank._5, "d5")]
        [DataRow(ChessboardControl.ChessFile.e, ChessboardControl.ChessRank._6, "e6")]
        [DataRow(ChessboardControl.ChessFile.f, ChessboardControl.ChessRank._7, "f7")]
        [DataRow(ChessboardControl.ChessFile.g, ChessboardControl.ChessRank._8, "g8")]
        [DataRow(ChessboardControl.ChessFile.h, ChessboardControl.ChessRank._1, "h1")]
        public void AlgebraicNotation_Should_Return_CorrectValues(ChessboardControl.ChessFile file, ChessboardControl.ChessRank rank, string expectedValue)
        {
            //	Arrange
            SUT chessSquare;
            string actual;

            //	Act
            chessSquare = new SUT(file, rank);
            actual = chessSquare.AlgebraicNotation;

            //	Assert
            Assert.AreEqual(expectedValue, actual);
        }

        [TestMethod]
        [DataRow("a8", ChessboardControl.ChessColor.White)]
        [DataRow("b7", ChessboardControl.ChessColor.White)]
        [DataRow("c6", ChessboardControl.ChessColor.White)]
        [DataRow("d5", ChessboardControl.ChessColor.White)]
        [DataRow("e4", ChessboardControl.ChessColor.White)]
        [DataRow("f3", ChessboardControl.ChessColor.White)]
        [DataRow("g2", ChessboardControl.ChessColor.White)]
        [DataRow("h1", ChessboardControl.ChessColor.White)]
        [DataRow("h8", ChessboardControl.ChessColor.Black)]
        [DataRow("g7", ChessboardControl.ChessColor.Black)]
        [DataRow("f6", ChessboardControl.ChessColor.Black)]
        [DataRow("e5", ChessboardControl.ChessColor.Black)]
        [DataRow("d4", ChessboardControl.ChessColor.Black)]
        [DataRow("c3", ChessboardControl.ChessColor.Black)]
        [DataRow("b2", ChessboardControl.ChessColor.Black)]
        [DataRow("a1", ChessboardControl.ChessColor.Black)]
        public void Color_Should_Return_CorrectValues(string pgn, ChessboardControl.ChessColor expected)
        {
            //	Arrange
            SUT chessSquare;

            //	Act
            chessSquare = new SUT(pgn);

            //	Assert
            Assert.AreEqual(expected, chessSquare.Color);
        }

        #endregion

        #region Methods

        [TestMethod]
        public void Clone_Should_ReturnProperlyInitializedClass()
        {
            //	Arrange
            ChessboardControl.ChessFile originalFile = ChessboardControl.ChessFile.e;
            ChessboardControl.ChessRank originalRank = ChessboardControl.ChessRank._2;
            SUT originalSquare;
            SUT clonedSquare;

            //	Act
            originalSquare = new SUT(originalFile, originalRank);
            clonedSquare = originalSquare.Clone();
            originalSquare.File = ChessboardControl.ChessFile.a;
            originalSquare.Rank = ChessboardControl.ChessRank._4;

            //	Assert
            Assert.AreEqual(originalFile, clonedSquare.File);
            Assert.AreEqual(originalRank, clonedSquare.Rank);
        }

        #endregion Methods

        #region Operators

        [TestMethod]
        public void Equal_Operator_Should_Return_CorrectValues()
        {
            //	Arrange
            SUT firstSquare;
            SUT secondSquare;
            SUT nullFirstSquare = null;
            SUT nullSecondSquare = null;

            //  Act
            firstSquare = new SUT("e2");
            secondSquare = new SUT("e2");

            //	Assert
            Assert.IsTrue(firstSquare == firstSquare);
            Assert.IsTrue(nullFirstSquare == nullSecondSquare);
            Assert.IsFalse(nullFirstSquare == secondSquare);
            Assert.IsFalse(firstSquare == nullSecondSquare);
            Assert.IsTrue(firstSquare == secondSquare);
        }

        #endregion
    }
}
