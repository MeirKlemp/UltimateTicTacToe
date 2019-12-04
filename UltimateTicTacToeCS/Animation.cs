using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimateTicTacToeCS
{
    public class Animation
    {
        public static bool Paused { get; set; }
        public static int Ticks => (Paused ? pauseTime : TickCount) - ticksPaused;

        private static int ticksPaused;
        private static int pauseTime;
        private static int startTick = Environment.TickCount;
        private static int TickCount => Environment.TickCount - startTick;

        public static void Pause()
        {
            if (!Paused)
            {
                pauseTime = TickCount;
                Paused = true;
            }
        }

        public static void Resume()
        {
            if (Paused)
            {
                ticksPaused += TickCount - pauseTime;
                Paused = false;
            }
        }

        public int Duration { get; private set; }
        public float Value => Started ? (Options.UseAnimations ? 1 - (float)(end - Math.Min(end, OwnTicks)) / Duration : 1) : 0;
        public float ReversedValue => 1 - Value;
        public bool Started => end > 0;
        public bool Finished => Ticks >= end;
        public bool Running => Started && !Finished;
        public bool Stopped { get; private set; }
        private int OwnTicks => (Stopped ? startStop : Ticks) - stopTicks;

        private int end;
        private int stopTicks;
        private int startStop;

        public Animation(int duration)
        {
            Duration = duration;
        }

        public void Start()
        {
            end = OwnTicks + Duration;
        }

        public void Stop()
        {
            if (!Stopped)
            {
                startStop = Ticks;
                Stopped = true;
            }
        }

        public void Continue()
        {
            if (Stopped)
            {
                stopTicks += Ticks - startStop;
                Stopped = false;
            }
        }

        public void Reset()
        {
            end = 0;
        }
    }
}
