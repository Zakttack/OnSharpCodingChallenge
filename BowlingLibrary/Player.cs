using BowlingLibrary.Exceptions;
using BowlingLibrary.Models;

namespace BowlingLibrary
{
    public class Player
    {
        private bool hasExtraShot;
        public Player(string name)
        {
            Info = new(name, new Frame[10]);
            hasExtraShot = false;
        }

        public Player(string name, Frame[] frames)
        {
            Info = new(name, frames);
            hasExtraShot = false;
        }

        public KeyValuePair<string,Frame[]> Info
        {
            get;
            private set;
        }

        public int Score
        {
            get
            {
                for (int f = 9; f >= 0; f--)
                {
                    if (Info.Value[f] != null)
                    {
                        return Info.Value[f].Score;
                    }
                }
                return 0;
            }
        }

        public Turn TurnStatus
        {
            get
            {
                for (int f = 0; f < Info.Value.Length; f++)
                {
                    if (Info.Value[f] == null)
                    {
                        return new(f);
                    }
                    for (int s = 0; s < Info.Value[f].Shots.Length; s++)
                    {
                        if (Info.Value[f].Shots[s] == null)
                        {
                            return new(f,s);
                        }
                    }
                }
                return Turn.Complete;
            }
        }

        public void Bowl(int pinsKnockedDown)
        {
            if (pinsKnockedDown < 0)
            {
                throw new AmountKnockedDownException(pinsKnockedDown, TurnStatus.ShotNumber);
            }
            if (Info.Value[TurnStatus.FrameNumber] == null)
            {
                Info.Value[TurnStatus.FrameNumber] = new(TurnStatus.FrameNumber == 9);
            }
            if (pinsKnockedDown == 0)
            {
                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                {
                    Result = '-'
                };
                if (TurnStatus.FrameNumber == 9 && TurnStatus.ShotNumber == 1 && !hasExtraShot)
                {
                    EndFrame();
                }
            }
            else
            {
                switch (TurnStatus.ShotNumber)
                {
                    case 0:
                        if (pinsKnockedDown > 10)
                        {
                            throw new AmountKnockedDownException(pinsKnockedDown, TurnStatus.ShotNumber);
                        }
                        else if (pinsKnockedDown == 10)
                        {
                            Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                            {
                                Result = 'X'
                            };
                            if (TurnStatus.FrameNumber != 9)
                            {
                                EndFrame();
                            }
                            else
                            {
                                hasExtraShot = true;
                            }
                        }
                        else
                        {
                            Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                            {
                                Result = (char)(48 + pinsKnockedDown)
                            };
                        }
                        break;
                    case 1:
                        Shot previousShot = Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber - 1];
                        if (TurnStatus.FrameNumber == 9 && previousShot.Result == 'X')
                        {
                            hasExtraShot = true;
                            if (pinsKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, TurnStatus.ShotNumber);
                            }
                            else if (pinsKnockedDown == 10)
                            {
                                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = 'X'
                                };
                            }
                            else
                            {
                                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        else
                        {
                            int totalKnockedDown = previousShot.PinsKnockedDown + pinsKnockedDown;
                            if (totalKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, TurnStatus.ShotNumber);
                            }
                            else if (totalKnockedDown == 10)
                            {
                                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = '/'
                                };
                                hasExtraShot = TurnStatus.FrameNumber == 9;
                            }
                            else
                            {
                                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                                if (TurnStatus.FrameNumber == 9 && TurnStatus.ShotNumber == 1 && !hasExtraShot)
                                {
                                    EndFrame();
                                }
                            }
                        }
                        break;
                    case 2:
                        if (Info.Value[TurnStatus.FrameNumber].Shots[1].Result == 'X' || Info.Value[TurnStatus.FrameNumber].Shots[1].Result == '/')
                        {
                            if (pinsKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, TurnStatus.ShotNumber);
                            }
                            else if (pinsKnockedDown == 10)
                            {
                                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = 'X'
                                };
                            }
                            else
                            {
                                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        else
                        {
                            int totalKnockedDown = Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber - 1].PinsKnockedDown + pinsKnockedDown;
                            if (totalKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, TurnStatus.ShotNumber);
                            }
                            else if (totalKnockedDown == 10)
                            {
                                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = '/'
                                };
                            }
                            else
                            {
                                Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        break;
                }
            }
            UpdateScore();
        }

        private void EndFrame()
        {
            Info.Value[TurnStatus.FrameNumber].Shots[TurnStatus.ShotNumber] = Shot.Empty;
        }

        private void UpdateScore()
        {
            int f = 0;
            try
            {
                while (f < TurnStatus.FrameNumber && f <= 8)
                {
                    Info.Value[f].Score = f == 0 ? 0 : Info.Value[f - 1].Score;
                    if (Info.Value[f].Shots[0].Result == 'X')
                    {
                        Queue<Shot> shots = new();
                        Turn t = new(f + 1);
                        while (shots.Count < 2)
                        {
                            if (Info.Value[t.FrameNumber].Shots[t.ShotNumber].Result != ' ')
                            {
                                shots.Enqueue(Info.Value[t.FrameNumber].Shots[t.ShotNumber]);
                            }
                            t++;
                        }
                        Info.Value[f].Score += 10;
                        foreach (Shot shot in shots)
                        {
                            Info.Value[f].Score += shot.PinsKnockedDown;
                        }
                    }
                    else if (Info.Value[f].Shots[1].Result == '/')
                    {
                        Info.Value[f].Score += 10 + Info.Value[f + 1].Shots[0].PinsKnockedDown;
                    }
                    else
                    {
                        foreach (Shot shot in Info.Value[f].Shots)
                        {
                            Info.Value[f].Score += shot.PinsKnockedDown;
                        }
                    }
                    f++;
                }
            }
            catch (SystemException)
            {
                Info.Value[f].Score = f == 0 ? 0 : Info.Value[f - 1].Score;
            }
            finally
            {
                if (f == 9 && Info.Value[f] != null)
                {
                    Info.Value[f].Score = Info.Value[f-1].Score;
                    foreach (Shot shot in Info.Value[f].Shots)
                    {
                        try
                        {
                            Info.Value[f].Score += shot.PinsKnockedDown;
                        }
                        catch (NullReferenceException)
                        {
                            Info.Value[f].Score += 0;
                        }
                    }
                }
            }
        }

        public static bool operator==(Player a, Player b)
        {
            try
            {
                return a.Equals(b);
            }
            catch (NullReferenceException)
            {
                try
                {
                    return b.Equals(a);
                }
                catch (NullReferenceException)
                {
                    return true;
                }
            }
        }

        public static bool operator!=(Player a, Player b)
        {
            try
            {
                return !a.Equals(b);
            }
            catch (NullReferenceException)
            {
                try
                {
                    return !b.Equals(a);
                }
                catch (NullReferenceException)
                {
                    return false;
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is not Player)
            {
                return false;
            }
            Player other = (Player)obj;
            return Info.Key == other.Info.Key;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}