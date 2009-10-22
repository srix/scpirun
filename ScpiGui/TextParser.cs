using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;

namespace ScpiTest
{
    public enum ParseStd
    {
        SFXSCPI,
        SCPI
    }
    class TextParser
    {
        private string scpiFileName;
        private FileStream fs;
        private StreamReader fileReader;
        private ISource source;
        private ParseStd parseStd;

        private delegate bool ReadNextLineDelegate(ref string readStr);
        ReadNextLineDelegate ReadNextLine;

        public TextParser(string fileName,ParseStd parseStd)
        {
            scpiFileName = fileName;
            this.parseStd = parseStd;
            OpenFile();
            //Assign delegate
            ReadNextLine = ReadNextLineFromFile; 
        }

        public TextParser(ISource source, ParseStd parseStd)
        {
            this.source = source;
            this.parseStd = parseStd;
            //Assign delegate
            ReadNextLine = ReadNextLineFromList; 

        }
        
        private void OpenFile() 
        {

            try
            {
                fs = new FileStream(scpiFileName, FileMode.Open,FileAccess.Read);
                
                               
                fileReader = new StreamReader(fs);
            }
            catch (Exception e)
            {
                throw e;
                //output.ShowError("File open error" + e.Message);
                //status= false;
            }
     
        }

         ~TextParser()
        {
            if (fs != null)
            {
                fs.Close();
            }
        }

        public  bool GetNextLine(ref ScpiCommand scpiCmd)
        {
            bool status=true;

            string readStr="";
            if (ReadNextLine(ref readStr)==true)
            {
                if (parseStd == ParseStd.SFXSCPI)
                {
                    readStr=readStr.Trim();
                    if ("r:"==readStr)
                    {
                        scpiCmd.Type = ScpiCommandType.QUERY;
                        scpiCmd.Cmd = "";
                    }
                    else if (readStr.StartsWith("s: ",true,CultureInfo.CurrentCulture)==true)
                    {
                        scpiCmd.Type = ScpiCommandType.SET;
                        scpiCmd.Cmd = readStr.Remove(readStr.IndexOf("s: ", StringComparison.OrdinalIgnoreCase), 3);
                    }
                    else if (readStr.StartsWith("w: ", true, CultureInfo.CurrentCulture) == true)
                    {
                        scpiCmd.Type = ScpiCommandType.WAIT;
                        scpiCmd.Cmd = "";
                    }
                    else //default
                    {
                        scpiCmd.Type = ScpiCommandType.INVALID;
                        scpiCmd.Cmd = "";

                    }
                    scpiCmd.Str = readStr;
                }
                else//ParseStd.SCPI
                {
                    readStr.Trim();
                    if ((readStr.StartsWith(":")==true) &&
                        (readStr.EndsWith("?") == true))
                    {
                        scpiCmd.Type = ScpiCommandType.QUERY;
                    }
                    else if (readStr.StartsWith(":w ") == true)
                    {
                        scpiCmd.Type = ScpiCommandType.WAIT;
                    }
                    else if ((readStr.StartsWith(":")==true) &&
                        (readStr.EndsWith("?") == false))
                    {
                        scpiCmd.Type = ScpiCommandType.SET;
                    }
                    else //default
                    {
                        scpiCmd.Type = ScpiCommandType.INVALID;

                    }
                    scpiCmd.Cmd = readStr;
                }

            }
            else
            {
                status = false;
            }
            
            return status;
        }

        private bool ReadNextLineFromFile(ref string readStr)
        {
            bool status = true;
            if (fileReader.EndOfStream == false)
            {
                readStr = "";
                try
                {
                    readStr = fileReader.ReadLine();

                }
                catch (Exception e)
                {
                    status = false;
                    throw e;

                }
            }
            else
            {
                status = false;
            }
            return status;
        }

        private bool ReadNextLineFromList(ref string readStr)
        {
           
         return   source.GetNextLine(ref readStr);

        }
                
    }
}
