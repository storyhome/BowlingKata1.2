using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace BowlingKata1._2
{
    public class Games
    {
        private int itsCurrentFrame = 0;
        private bool firstThrowInFrame = true;
        private Scorer itsScorer = new Scorer();

        public int Score()
        {

            return scoreForFrame(itsCurrentFrame);
        }


        public void add(int pins)
        {
            itsScorer.addThrow(pins);
            adjustCurrentFrame(pins);
        }

        private void adjustCurrentFrame(int pins)
        {
            if (lastBallInFrame(pins))
                advanceFrame();
            else
                firstThrowInFrame = false;
        }

        private Boolean lastBallInFrame(int pins)
        {
            return strike(pins) || firstThrowInFrame;
        }

        private Boolean strike(int pins)
        {
            return (firstThrowInFrame && pins == 10);
        }

        private void advanceFrame()
        {
            itsCurrentFrame = Math.Min(10, itsCurrentFrame + 1);

        }

        public int scoreForFrame(int theFrame)
        {
            return itsScorer.scoreForFrame(theFrame);
            
        }


        public class Scorer
        {

            private int ball;
            private int[] itsThrows = new int[21];
            private int itsCurrentThrow = 0;

            public void addThrow(int pins)
            {
                itsThrows[itsCurrentThrow++] = pins;
            }

            public int scoreForFrame(int theFrame)
            {
                int ball = 0;
                int score = 0;

                
                for (int currentFrame = 0; currentFrame < theFrame; currentFrame++)
                {
                    if (strike())
                    {
                        score += 10 + nextTwoBallsForStrike();

                        ball++;
                    }
                    else if (spare())
                    {
                        score += 10 + nextBallForSpare();
                        ball += 2;
                    }
                    else
                    {
                        score += twoBallsInFrame();
                        ball += 2;
                    }
                    
                }
                return score;
            }

            private Boolean strike()
            {
                return itsThrows[ball] == 10;
            }

            private bool spare()
            {
                return (itsThrows[ball] + itsThrows[ball + 1]) == 10;
            }

            private int nextTwoBallsForStrike()
            {
                return itsThrows[ball + 1] + itsThrows[ball + 2];
            }

            private int nextBallForSpare()
            {
                return itsThrows[ball + 2];
            }

            private int twoBallsInFrame()
            {
                return itsThrows[ball] + itsThrows[ball + 1];
            }
        }
    }


}
