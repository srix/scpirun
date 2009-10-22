using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ScpiTest
{




#if  CONSOLE
    class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            string fileName = "";
            string target = "";
            int port = 49152;
            ParseStd std = ParseStd.SFXSCPI;

            Console.WriteLine("starting\n");
            ScpiCon scpiCon = new ScpiCon();

            if (ParseCommandLine(args, ref fileName, ref target, ref port, ref std) == true)
            {
                try
                {
                    Console.WriteLine("Loading Scpi File " + fileName);
                    TextParser parser = new TextParser(fileName, std);
                    Console.WriteLine("Executing Scpi commands");
                    ScpiTest scpiTest = new ScpiTest(parser, scpiCon, target, port);
                    scpiTest.Run();
                    Console.WriteLine("Exciting ScpiCon");
                }
                catch (Exception e)
                {
                    scpiCon.ShowError(e.Message);
                }
            }
            else
            {
                scpiCon.ShowError("Invalid command line parameter or option");
                string str = "scpicon /f [filename] /t [target machine] ";
                str += "/p [port, default=49152] /s [0-SCPI, 1-SFXSCPI]";
                Console.WriteLine(str);

            }
            

        }

        static private bool ParseCommandLine(string[] args, ref string fileName, ref string target,
                                           ref int port, ref ParseStd standard)
        {

            bool status = false; ;
            for (int i = 0; i < args.GetUpperBound(0); i++)
            {
                if ("/f" == args[i].ToLower())
                {
                    fileName = args[++i];
                    status = true;
                }
                else if ("/t" == args[i].ToLower())
                {
                    target = args[++i];
                    status = true;
                }
                else if ("/p" == args[i].ToLower())
                {
                    System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[0-9]*");
                    if (rgx.IsMatch(args[i]) == true)
                    {
                        port = Convert.ToInt32(args[++i]);
                        status = true;
                    }
                    else
                    {
                        status = false;
                        break;
                    }
                }
                else if ("/s" == args[i].ToLower())
                {
                    System.Text.RegularExpressions.Regex rgx = new System.Text.RegularExpressions.Regex("[0-1]");
                    if (rgx.IsMatch(args[i]) == true)
                    {
                        standard = (ParseStd)Convert.ToInt32(args[++i]);
                        status = true;
                    }
                    else
                    {
                        status = false;
                        break;
                    }
                }
                else
                {
                    status = false;
                    break;
                }

            }
                        
            return status;
        }
    }

#else
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new formScpiGUI());

        }
    }
#endif

}
