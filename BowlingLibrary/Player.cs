using System.Xml.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BowlingLibrary.Exceptions;
using BowlingLibrary.Models;

namespace BowlingLibrary
{
    public class Player
    {
        private int frameNumber;
        private int shotNumber;
        public Player(string name)
        {
            Info = new(name, new Frame[10]);
            frameNumber = 0;
            shotNumber = 0;
        }

        public Player(string name, Frame[] frames)
        {
            Info = new(name, frames);
            frameNumber = 0;
            shotNumber = 0;
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
                throw new AmountKnockedDownException(pinsKnockedDown, shotNumber);
            }
            if (Info.Value[frameNumber] == null)
            {
                Info.Value[frameNumber] = new(frameNumber == 9);
            }
            if (pinsKnockedDown == 0)
            {
                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                {
                    Result = '-'
                };
            }
            else
            {
                switch (shotNumber)
                {
                    case 0:
                        if (pinsKnockedDown > 10)
                        {
                            throw new AmountKnockedDownException(pinsKnockedDown, shotNumber);
                        }
                        else if (pinsKnockedDown == 10)
                        {
                            Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                            {
                                Result = 'X'
                            };
                        }
                        else
                        {
                            Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                            {
                                Result = (char)(48 + pinsKnockedDown)
                            };
                        }
                        break;
                    case 1:
                        Shot previousShot = Info.Value[frameNumber].Shots[shotNumber - 1];
                        if (frameNumber == 9 && previousShot.Result == 'X')
                        {
                            if (pinsKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, shotNumber);
                            }
                            else if (pinsKnockedDown == 10)
                            {
                                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                                {
                                    Result = 'X'
                                };
                            }
                            else
                            {
                                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        else if (previousShot.Result == 'X')
                        {
                            Info.Value[frameNumber].Shots[shotNumber] = new();
                        }
                        else
                        {
                            int totalKnockedDown = previousShot.PinsKnockedDown + pinsKnockedDown;
                            if (totalKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, shotNumber);
                            }
                            else if (totalKnockedDown == 10)
                            {
                                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                                {
                                    Result = '/'
                                };
                            }
                            else
                            {
                                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        shotNumber++;
                        break;
                    case 2:
                        if (frameNumber == 9 && Info.Value[frameNumber].Shots[0].Result != 'X' && 
                            Info.Value[frameNumber].Shots[1].Result != 'X' && 
                            Info.Value[frameNumber].Shots[1].Result != '/')
                        {
                            Info.Value[frameNumber].Shots[shotNumber] = new();
                        }
                        else if (frameNumber == 9 && (Info.Value[frameNumber].Shots[1].Result == 'X' ||
                            Info.Value[frameNumber].Shots[1].Result == '/'))
                        {
                            if (pinsKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, shotNumber);
                            }
                            else if (pinsKnockedDown == 10)
                            {
                                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                                {
                                    Result = 'X'
                                };
                            }
                            else
                            {
                                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        else if (frameNumber == 9)
                        {
                            int totalKnockedDown = Info.Value[frameNumber].Shots[shotNumber - 1].PinsKnockedDown + pinsKnockedDown;
                            if (totalKnockedDown > 10)
                            {
                                throw new AmountKnockedDownException(pinsKnockedDown, shotNumber);
                            }
                            else if (totalKnockedDown == 10)
                            {
                                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                                {
                                    Result = '/'
                                };
                            }
                            else
                            {
                                Info.Value[frameNumber].Shots[shotNumber] = new(pinsKnockedDown)
                                {
                                    Result = (char)(48 + pinsKnockedDown)
                                };
                            }
                        }
                        break;
                }
            }
            if (shotNumber < Info.Value[frameNumber].Shots.Length)
            {
                shotNumber++;
            }
            else
            {
                frameNumber++;
                shotNumber = 0;
            }
            UpdateScore();
        }

        private void UpdateScore()
        {
            int f = 0;
            try
            {
                while (f < frameNumber && f <= 8)
                {
                    Info.Value[f].Score = f == 0 ? 0 : Info.Value[f - 1].Score;
                    if (Info.Value[f].Shots[0].Result == 'X')
                    {
                        Queue<Shot> shots = new();
                        int f1 = f + 1;
                        int s = 0;
                        while (shots.Count < 2)
                        {
                            if (Info.Value[f1].Shots[s].Result != ' ')
                            {
                                shots.Enqueue(Info.Value[f1].Shots[s]);
                            }
                            if (s < Info.Value[f1].Shots.Length)
                            {
                                s++;
                            }
                            else
                            {
                                f1++;
                                s = 0;
                            }
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
                if (f == 9)
                {
                    Info.Value[f].Score = Info.Value[f-1].Score;
                    foreach (Shot shot in Info.Value[f].Shots)
                    {
                        Info.Value[f].Score += shot.PinsKnockedDown;
                    }
                }
            }
        }
    }
}