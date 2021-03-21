using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SUT = ChessboardControl.ChessPiece;

namespace UnitTest_Chessboard_Control
{
    [TestClass]
    public class UnitTest_ChessPiece
    {
        #region Methods

        [TestMethod]
        public void Clone_Should_ReturnProperlyInitializedClass()
        {
            //	Arrange
            ChessboardControl.ChessPieceKind originalPieceKind = ChessboardControl.ChessPieceKind.Pawn;
            ChessboardControl.ChessColor originalColor = ChessboardControl.ChessColor.Black;
            SUT originalPiece;
            SUT clonedPiece;

            //	Act
            originalPiece = new SUT(originalPieceKind, originalColor);
            clonedPiece = originalPiece.Clone();
            originalPiece.Kind = ChessboardControl.ChessPieceKind.Rook;
            originalPiece.Color = ChessboardControl.ChessColor.White;

            //	Assert
            Assert.AreEqual(originalPieceKind, clonedPiece.Kind);
            Assert.AreEqual(originalColor, clonedPiece.Color);
        }

        #endregion Methods

        #region Operators

        [TestMethod]
        public void Equal_Operator_Should_Return_CorrectValues()
        {
            //	Arrange
            SUT firstPiece;
            SUT secondPiece;
            SUT nullFirstPiece = null;
            SUT nullSecondPiece = null;

            //  Act
            firstPiece = new SUT(ChessboardControl.ChessPieceKind.Pawn, ChessboardControl.ChessColor.White);
            secondPiece = new SUT(ChessboardControl.ChessPieceKind.Pawn, ChessboardControl.ChessColor.White);

            //	Assert
            Assert.IsTrue(firstPiece == firstPiece);
            Assert.IsTrue(nullFirstPiece == nullSecondPiece);
            Assert.IsFalse(nullFirstPiece == secondPiece);
            Assert.IsFalse(firstPiece == nullSecondPiece);
            Assert.IsTrue(firstPiece == secondPiece);
        }

        #endregion
    }
}
