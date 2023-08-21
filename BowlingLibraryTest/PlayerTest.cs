using BowlingLibrary;
using BowlingLibrary.Models;
using BowlingLibrary.Exceptions;
namespace BowlingLibraryTest
{
    public class PlayerTest
    {
        private Player player;
        [SetUp]
        public void Setup()
        {
            player = new("Zak Merrigan");
        }

        [Test]
        public void TestScore()
        {
            Assert.That(player.Score, Is.EqualTo(0));
        }

        [Test]
        public void TestBowlWithNegativePins()
        {
            Assert.Throws<AmountKnockedDownException>(() => player.Bowl(-1));
        }

        [Test]
        public void TestBowlWithZeroPins()
        {
            player.Bowl(0);
            Frame first = player.Info.Value[0];
            Assert.That(first.Score, Is.EqualTo(0));
            Shot shot = first.Shots[0];
            Assert.That(shot.PinsKnockedDown, Is.EqualTo(0));
            Assert.That(shot.Result, Is.EqualTo('-'));
        }

        [Test]
        public void TestBowlWithPins()
        {
            player.Bowl(2);
            Frame first = player.Info.Value[0];
            Assert.That(first.Score, Is.EqualTo(0));
            Shot shot = first.Shots[0];
            Assert.That(shot.PinsKnockedDown, Is.EqualTo(2));
            Assert.That(shot.Result, Is.EqualTo('2'));
        }

        [Test]
        public void TestBowlWithStrike()
        {
            player.Bowl(10);
            Frame first = player.Info.Value[0];
            Assert.That(first.Score, Is.EqualTo(0));
            Shot shot = first.Shots[0];
            Assert.That(shot.PinsKnockedDown, Is.EqualTo(10));
            Assert.That(shot.Result, Is.EqualTo('X'));
        }

        [Test]
        public void VerifyEmptyAfterStrike()
        {
            player.Bowl(10);
            Frame first = player.Info.Value[0];
            Shot shot = first.Shots[1];
            
        }

        [Test]
        public void TestBowlWithTooManyPins()
        {
            Assert.Throws<AmountKnockedDownException>(() => player.Bowl(11));
        }
    }
}