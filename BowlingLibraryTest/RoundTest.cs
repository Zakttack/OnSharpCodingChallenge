using BowlingLibrary;
using BowlingLibrary.Exceptions;
using BowlingLibrary.Models;

namespace BowlingLibraryTest
{
    public class RoundTest
    {
        private Round round;
        [SetUp]
        public void Setup()
        {
            round = new Round();
        }

        [Test]
        public void TestAddPlayer1()
        {
            round.AddPlayer("Zak Merrigan");
            Assert.That(round.PlayerCount, Is.EqualTo(1));
        }

        [Test]
        public void TestAddPlayer2()
        {
            round.AddPlayer("Zak Ray Merrigan");
            Assert.Throws<PlayerAlreadyExistsException>(() => round.AddPlayer("Zak Ray Merrigan"));
        }

        [Test]
        public void TestEnumeratePlayer()
        {
            round.AddPlayer("Zak Ray Merrigan");
            round.AddPlayer("Steve Ray Merrigan");
            round.AddPlayer("Mirna Merrigan");
            Player[] expected = new Player[]{new("Zak Ray Merrigan"), new("Steve Ray Merrigan"),
            new("Mirna Merrigan")};
            Player[] actual = round.ToArray();
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}