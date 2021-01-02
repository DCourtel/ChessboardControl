using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ChessboardControl;
using SUT = ChessboardControl.BoardCoordinates;

namespace UnitTest_Chessboard_Control
{
    /// <summary>
    /// Description résumée pour UnitTest1
    /// </summary>
    [TestClass]
    public class UnitTest_BoadCoordinates
    {
        [TestMethod]
        [DataRow(XCoordinate.a, YCoordinate._1)]
        [DataRow(XCoordinate.a, YCoordinate._2)]
        [DataRow(XCoordinate.a, YCoordinate._3)]
        [DataRow(XCoordinate.a, YCoordinate._4)]
        [DataRow(XCoordinate.a, YCoordinate._5)]
        [DataRow(XCoordinate.a, YCoordinate._6)]
        [DataRow(XCoordinate.a, YCoordinate._7)]
        [DataRow(XCoordinate.a, YCoordinate._8)]
        [DataRow(XCoordinate.b, YCoordinate._1)]
        [DataRow(XCoordinate.b, YCoordinate._2)]
        [DataRow(XCoordinate.b, YCoordinate._3)]
        [DataRow(XCoordinate.b, YCoordinate._4)]
        [DataRow(XCoordinate.b, YCoordinate._5)]
        [DataRow(XCoordinate.b, YCoordinate._6)]
        [DataRow(XCoordinate.b, YCoordinate._7)]
        [DataRow(XCoordinate.b, YCoordinate._8)]
        [DataRow(XCoordinate.c, YCoordinate._1)]
        [DataRow(XCoordinate.c, YCoordinate._2)]
        [DataRow(XCoordinate.c, YCoordinate._3)]
        [DataRow(XCoordinate.c, YCoordinate._4)]
        [DataRow(XCoordinate.c, YCoordinate._5)]
        [DataRow(XCoordinate.c, YCoordinate._6)]
        [DataRow(XCoordinate.c, YCoordinate._7)]
        [DataRow(XCoordinate.c, YCoordinate._8)]
        [DataRow(XCoordinate.d, YCoordinate._1)]
        [DataRow(XCoordinate.d, YCoordinate._2)]
        [DataRow(XCoordinate.d, YCoordinate._3)]
        [DataRow(XCoordinate.d, YCoordinate._4)]
        [DataRow(XCoordinate.d, YCoordinate._5)]
        [DataRow(XCoordinate.d, YCoordinate._6)]
        [DataRow(XCoordinate.d, YCoordinate._7)]
        [DataRow(XCoordinate.d, YCoordinate._8)]
        [DataRow(XCoordinate.e, YCoordinate._1)]
        [DataRow(XCoordinate.e, YCoordinate._2)]
        [DataRow(XCoordinate.e, YCoordinate._3)]
        [DataRow(XCoordinate.e, YCoordinate._4)]
        [DataRow(XCoordinate.e, YCoordinate._5)]
        [DataRow(XCoordinate.e, YCoordinate._6)]
        [DataRow(XCoordinate.e, YCoordinate._7)]
        [DataRow(XCoordinate.e, YCoordinate._8)]
        [DataRow(XCoordinate.f, YCoordinate._1)]
        [DataRow(XCoordinate.f, YCoordinate._2)]
        [DataRow(XCoordinate.f, YCoordinate._3)]
        [DataRow(XCoordinate.f, YCoordinate._4)]
        [DataRow(XCoordinate.f, YCoordinate._5)]
        [DataRow(XCoordinate.f, YCoordinate._6)]
        [DataRow(XCoordinate.f, YCoordinate._7)]
        [DataRow(XCoordinate.f, YCoordinate._8)]
        [DataRow(XCoordinate.g, YCoordinate._1)]
        [DataRow(XCoordinate.g, YCoordinate._2)]
        [DataRow(XCoordinate.g, YCoordinate._3)]
        [DataRow(XCoordinate.g, YCoordinate._4)]
        [DataRow(XCoordinate.g, YCoordinate._5)]
        [DataRow(XCoordinate.g, YCoordinate._6)]
        [DataRow(XCoordinate.g, YCoordinate._7)]
        [DataRow(XCoordinate.g, YCoordinate._8)]
        [DataRow(XCoordinate.h, YCoordinate._1)]
        [DataRow(XCoordinate.h, YCoordinate._2)]
        [DataRow(XCoordinate.h, YCoordinate._3)]
        [DataRow(XCoordinate.h, YCoordinate._4)]
        [DataRow(XCoordinate.h, YCoordinate._5)]
        [DataRow(XCoordinate.h, YCoordinate._6)]
        [DataRow(XCoordinate.h, YCoordinate._7)]
        [DataRow(XCoordinate.h, YCoordinate._8)]
        public void IsOnBoard_Should_Return_true(XCoordinate letter, YCoordinate digit)
        {
            //	Arrange
            SUT boardCoordinates;

            //	Act
            boardCoordinates = new SUT(letter, digit);

            //	Assert
            Assert.IsTrue(boardCoordinates.IsOnBoard);
        }

        [TestMethod]
        [DataRow(XCoordinate.None, YCoordinate.None)]
        [DataRow(XCoordinate.None, YCoordinate._1)]
        [DataRow(XCoordinate.a, YCoordinate.None)]
        public void IsOnBoard_Should_Return_false(XCoordinate letter, YCoordinate digit)
        {
            //	Arrange
            SUT boardCoordinates;

            //	Act
            boardCoordinates = new SUT(letter, digit);

            //	Assert
            Assert.IsFalse(boardCoordinates.IsOnBoard);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException), AllowDerivedTypes = false)]
        [DataRow(XCoordinate.None, YCoordinate.None)]
        [DataRow(XCoordinate.None, YCoordinate._1)]
        [DataRow(XCoordinate.a, YCoordinate.None)]
        public void ToString_Should_Throw_ArgumentOutOfRangeException(XCoordinate letter, YCoordinate digit)
        {
            //	Arrange
            SUT boardCoordinates;

            //	Act
            boardCoordinates = new SUT(letter, digit);
            boardCoordinates.ToString();
        }

        [TestMethod]
        [DataRow(XCoordinate.a, YCoordinate._1, "a1")]
        [DataRow(XCoordinate.a, YCoordinate._2, "a2")]
        [DataRow(XCoordinate.a, YCoordinate._3, "a3")]
        [DataRow(XCoordinate.a, YCoordinate._4, "a4")]
        [DataRow(XCoordinate.a, YCoordinate._5, "a5")]
        [DataRow(XCoordinate.a, YCoordinate._6, "a6")]
        [DataRow(XCoordinate.a, YCoordinate._7, "a7")]
        [DataRow(XCoordinate.a, YCoordinate._8, "a8")]
        [DataRow(XCoordinate.b, YCoordinate._1, "b1")]
        [DataRow(XCoordinate.b, YCoordinate._2, "b2")]
        [DataRow(XCoordinate.b, YCoordinate._3, "b3")]
        [DataRow(XCoordinate.b, YCoordinate._4, "b4")]
        [DataRow(XCoordinate.b, YCoordinate._5, "b5")]
        [DataRow(XCoordinate.b, YCoordinate._6, "b6")]
        [DataRow(XCoordinate.b, YCoordinate._7, "b7")]
        [DataRow(XCoordinate.b, YCoordinate._8, "b8")]
        [DataRow(XCoordinate.c, YCoordinate._1, "c1")]
        [DataRow(XCoordinate.c, YCoordinate._2, "c2")]
        [DataRow(XCoordinate.c, YCoordinate._3, "c3")]
        [DataRow(XCoordinate.c, YCoordinate._4, "c4")]
        [DataRow(XCoordinate.c, YCoordinate._5, "c5")]
        [DataRow(XCoordinate.c, YCoordinate._6, "c6")]
        [DataRow(XCoordinate.c, YCoordinate._7, "c7")]
        [DataRow(XCoordinate.c, YCoordinate._8, "c8")]
        [DataRow(XCoordinate.d, YCoordinate._1, "d1")]
        [DataRow(XCoordinate.d, YCoordinate._2, "d2")]
        [DataRow(XCoordinate.d, YCoordinate._3, "d3")]
        [DataRow(XCoordinate.d, YCoordinate._4, "d4")]
        [DataRow(XCoordinate.d, YCoordinate._5, "d5")]
        [DataRow(XCoordinate.d, YCoordinate._6, "d6")]
        [DataRow(XCoordinate.d, YCoordinate._7, "d7")]
        [DataRow(XCoordinate.d, YCoordinate._8, "d8")]
        [DataRow(XCoordinate.e, YCoordinate._1, "e1")]
        [DataRow(XCoordinate.e, YCoordinate._2, "e2")]
        [DataRow(XCoordinate.e, YCoordinate._3, "e3")]
        [DataRow(XCoordinate.e, YCoordinate._4, "e4")]
        [DataRow(XCoordinate.e, YCoordinate._5, "e5")]
        [DataRow(XCoordinate.e, YCoordinate._6, "e6")]
        [DataRow(XCoordinate.e, YCoordinate._7, "e7")]
        [DataRow(XCoordinate.e, YCoordinate._8, "e8")]
        [DataRow(XCoordinate.f, YCoordinate._1, "f1")]
        [DataRow(XCoordinate.f, YCoordinate._2, "f2")]
        [DataRow(XCoordinate.f, YCoordinate._3, "f3")]
        [DataRow(XCoordinate.f, YCoordinate._4, "f4")]
        [DataRow(XCoordinate.f, YCoordinate._5, "f5")]
        [DataRow(XCoordinate.f, YCoordinate._6, "f6")]
        [DataRow(XCoordinate.f, YCoordinate._7, "f7")]
        [DataRow(XCoordinate.f, YCoordinate._8, "f8")]
        [DataRow(XCoordinate.g, YCoordinate._1, "g1")]
        [DataRow(XCoordinate.g, YCoordinate._2, "g2")]
        [DataRow(XCoordinate.g, YCoordinate._3, "g3")]
        [DataRow(XCoordinate.g, YCoordinate._4, "g4")]
        [DataRow(XCoordinate.g, YCoordinate._5, "g5")]
        [DataRow(XCoordinate.g, YCoordinate._6, "g6")]
        [DataRow(XCoordinate.g, YCoordinate._7, "g7")]
        [DataRow(XCoordinate.g, YCoordinate._8, "g8")]
        [DataRow(XCoordinate.h, YCoordinate._1, "h1")]
        [DataRow(XCoordinate.h, YCoordinate._2, "h2")]
        [DataRow(XCoordinate.h, YCoordinate._3, "h3")]
        [DataRow(XCoordinate.h, YCoordinate._4, "h4")]
        [DataRow(XCoordinate.h, YCoordinate._5, "h5")]
        [DataRow(XCoordinate.h, YCoordinate._6, "h6")]
        [DataRow(XCoordinate.h, YCoordinate._7, "h7")]
        [DataRow(XCoordinate.h, YCoordinate._8, "h8")]
        public void ToString_Should_Return_CorrectValue(XCoordinate letter, YCoordinate digit, string expectedValue)
        {
            //	Arrange
            SUT boardCoordinates;

            //	Act
            boardCoordinates = new SUT(letter, digit);

            //	Assert
            Assert.AreEqual(expectedValue,boardCoordinates.ToString());
        }

    }
}
