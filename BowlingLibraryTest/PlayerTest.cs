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
            Assert.That(player.Info.Value[0].Shots[1], Is.EqualTo(Shot.Empty));
        }

        [Test]
        public void TestBowlWithTooManyPins()
        {
            Assert.Throws<AmountKnockedDownException>(() => player.Bowl(11));
        }

        [Test]
        public void TestZeroPinsOnFrame()
        {
            player.Bowl(0);
            player.Bowl(0);
            Frame first = player.Info.Value[0];
            Assert.That(first.Score, Is.EqualTo(0));
            Shot[] shots = {new(0) {Result = '-'}, new(0) {Result = '-'}};
            Assert.That(player.Info.Value[0].Shots, Is.EqualTo(shots));
        }

        [Test]
        public void TestSomePinsOnFrame()
        {
            player.Bowl(2);
            player.Bowl(3);
            Frame first = player.Info.Value[0];
            Assert.That(first.Score, Is.EqualTo(5));
            Shot[] shots = {new(2) {Result = '2'}, new(3) {Result = '3'}};
            Assert.That(player.Info.Value[0].Shots, Is.EqualTo(shots));
        }

        [Test]
        public void TestInvaidPinSum()
        {
            player.Bowl(2);
            Assert.Throws<AmountKnockedDownException>(() => player.Bowl(9));
        }

        [Test]
        public void TestSpare()
        {
            player.Bowl(3);
            player.Bowl(7);
            Frame first = player.Info.Value[0];
            Assert.That(first.Score, Is.EqualTo(0));
            Shot[] shots = {new(3) {Result = '3'}, new(7) {Result = '/'}};
            Assert.That(player.Info.Value[0].Shots, Is.EqualTo(shots));
        }

        [Test]
        public void TestSpareScoring()
        {
            player.Bowl(3);
            player.Bowl(7);
            player.Bowl(2);
            Frame first = player.Info.Value[0];
            Assert.That(first.Score, Is.EqualTo(12));
        }

        [Test]
        public void TestStrikeScoring()
        {
            player.Bowl(10);
            player.Bowl(2);
            player.Bowl(3);
            Frame first = player.Info.Value[0];
            Assert.That(first.Score, Is.EqualTo(15));
        }

        [Test]
        public void TestBasicScoringInTenthFrame()
        {
            player.Bowl(10);
            player.Bowl(1);
            player.Bowl(5);
            Assert.That(player.Info.Value[0].Score, Is.EqualTo(16));
            Assert.That(player.Info.Value[1].Score, Is.EqualTo(22));
            player.Bowl(6);
            player.Bowl(4);
            player.Bowl(0);
            Assert.That(player.Info.Value[2].Score, Is.EqualTo(32));
            player.Bowl(0);
            Assert.That(player.Info.Value[3].Score, Is.EqualTo(32));
            player.Bowl(3);
            player.Bowl(2);
            Assert.That(player.Info.Value[4].Score, Is.EqualTo(37));
            player.Bowl(10);
            player.Bowl(7);
            player.Bowl(3);
            Assert.That(player.Info.Value[5].Score, Is.EqualTo(57));
            player.Bowl(1);
            Assert.That(player.Info.Value[6].Score, Is.EqualTo(68));
            player.Bowl(1);
            Assert.That(player.Info.Value[7].Score, Is.EqualTo(70));
            player.Bowl(2);
            player.Bowl(4);
            Assert.That(player.Info.Value[8].Score, Is.EqualTo(76));
            player.Bowl(6);
            player.Bowl(0);
            Assert.That(player.Info.Value[9].Score, Is.EqualTo(82));
        }
    }
}