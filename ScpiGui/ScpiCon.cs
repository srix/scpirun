using System;
using System.Collections.Generic;
using System.Text;

namespace ScpiTest
{
    class ScpiCon:IOutput
    {
        private ConsoleColor consoleFgColor;

        public ScpiCon()
        {
            consoleFgColor = Console.ForegroundColor;
        }
        public void ShowCommand(ScpiCommand scpiCmd)
        {
            Console.WriteLine(scpiCmd.Str);

        }
        public void ShowResult(string result)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ForegroundColor = consoleFgColor;
        }
        public void ShowError(string errorStr)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: " + errorStr);
            Console.ForegroundColor = consoleFgColor;
        }

        public void ShowWait(ScpiCommand scpiCmd)
        {
            string cmd = scpiCmd.Str;
            cmd=cmd.Remove(0, 2);
            cmd.Trim();
            int wait = Convert.ToInt32(cmd);
            Console.Write(scpiCmd.Str + " ");
            for (int i = 0; i < wait; i++)
            {
                //when the output of scpugui is directed to text file
                //Invalid handle error appears when drawtextprogressbar is first called
                //And scpigui stops there.
                drawDotProgressBar(i + 1, wait);
                System.Threading.Thread.Sleep(1000);                
            }
            Console.WriteLine();

        }

        /// <summary>
        /// Draw a progress bar at the current cursor position.
        /// Be careful not to Console.WriteLine or anything whilst using this to show progress!
        /// </summary>
        /// <param name="progress">The position of the bar</param>
        /// <param name="total">The amount it counts</param>

        private void drawTextProgressBar(int progress, int total)
        {
            Console.CursorLeft = 0;
            Console.Write(":w ");
            int OFFSET = 3;

            //draw empty progress bar
            Console.CursorLeft = OFFSET;
            Console.Write("["); //start
            Console.CursorLeft = 32+OFFSET;
            Console.Write("]"); //end
            Console.CursorLeft = 1 + OFFSET;
            float onechunk = 30.0f / total;

            //draw filled part
            int position = 1 + OFFSET;
            for (int i = 0; i < onechunk * progress; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = position; i < 31+OFFSET; i++)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.CursorLeft = position++;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
           // Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess
        }

        private void drawDotProgressBar(int progress, int total)
        {
            Console.Write(".");
        }
    }
}
