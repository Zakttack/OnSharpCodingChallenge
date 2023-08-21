using BowlingLibrary.Exceptions;
using BowlingLibrary.Models;

namespace BowlingLibrary
{
    public class Player
    {
        private Turn turn;
        private bool hasExtraShot;
        public Player(string name)
        {
            Info = new(name, new Frame[10]);
            turn = new();
            hasExtraShot = false;
        }

        public Player(string name, Frame[] frames)
        {
            Info = new(name, frames);
            turn = new();
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
                int i = 9;
                while (i > 0)
                {
                    try
                    {
                        return Info.Value[i].Score;
                    }
                    catch(SystemException)
                    {
                        i--;
                    }
                }
                return 0;
            }
        }

        public void Bowl(int pinsKnockedDown)
        {
            if (pinsKnockedDown < 0)
            {
                throw new AmountKnockedDownException(pinsKnockedDown, turn.ShotNumber);
            }
            if (Info.Value[turn.FrameNumber] == null)
            {
                Info.Value[turn.FrameNumber] = new(turn.FrameNumber == 9);
            }
            if (pinsKnockedDown == 0)
            {
                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                {
                    Result = '-'
                };
                if (turn.FrameNumber == 9 && turn.ShotNumber == 1 && !hasExtraShot)
                {
                    turn++;
                    Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = Shot.Empty;
                }
            }
            else
            {
                switch (turn.ShotNumber)
                {
                    case 0:
                        if (pinsKnockedDown > 10)
                        {
                            throw new AmountKnockedDownException(pinsKnockedDown, turn.ShotNumber);
                        }
                        else if (pinsKnockedDown == 10)
                        {
                            Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                            {
                                Result = 'X'
                            };
                            if (turn.FrameNumber != 9)
                            {
                                turn++;
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = Shot.Empty;
                            }
                        }
                        else
                        {
                            Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                            {
                                Result = (char)(48 + pinsKnockedDown)
                            };
                        }
                        break;
                    case 1:
                        Shot previousShot = Info.Value[turn.FrameNumber].Shots[turn.ShotNumber - 1];
                        if (turn.FrameNumber == 9 && previousShot.Result == 'X')
                        {
                            hasExtraShot = true;
                            if (pinsKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, turn.ShotNumber);
                            }
                            else if (pinsKnockedDown == 10)
                            {
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = 'X'
                                };
                            }
                            else
                            {
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
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
                                throw new AmountKnockedDownException(pinsKnockedDown, turn.ShotNumber);
                            }
                            else if (totalKnockedDown == 10)
                            {
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = '/'
                                };
                                hasExtraShot = turn.FrameNumber == 9;
                            }
                            else
                            {
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                                if (turn.FrameNumber == 9 && !hasExtraShot)
                                {
                                    turn++;
                                    Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = Shot.Empty;
                                }
                            }
                        }
                        break;
                    case 2:
                        if (Info.Value[turn.FrameNumber].Shots[1].Result == 'X' || Info.Value[turn.FrameNumber].Shots[1].Result == '/')
                        {
                            if (pinsKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, turn.ShotNumber);
                            }
                            else if (pinsKnockedDown == 10)
                            {
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = 'X'
                                };
                            }
                            else
                            {
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        else
                        {
                            int totalKnockedDown = Info.Value[turn.FrameNumber].Shots[turn.ShotNumber - 1].PinsKnockedDown + pinsKnockedDown;
                            if (totalKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, turn.ShotNumber);
                            }
                            else if (totalKnockedDown == 10)
                            {
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = '/'
                                };
                            }
                            else
                            {
                                Info.Value[turn.FrameNumber].Shots[turn.ShotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        break;
                }
            }
            turn++;
            UpdateScore();
        }

        private void UpdateScore()
        {
            int f = 0;
            try
            {
                while (f < turn.FrameNumber && f <= 8)
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
    }
}