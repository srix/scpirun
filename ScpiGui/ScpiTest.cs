using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Net;

namespace ScpiTest
{
    public struct ScpiCommand
    {
        public ScpiCommandType Type;
        public string Cmd;//Stores the exact command that will be sent to target
        public string Str;//Stores the string line read from the file
        //for well formed scpi files str and cmd will be same.
        //However for sfxscpi std and for comment lines they will differ.
        //For sfxscpi STD cmd will be stripped str and for comment limes cmd will be null.
    }

    public enum ScpiCommandType
    {
        SET=0,
        QUERY,
        WAIT,
        INVALID
    }

    class ScpiTest
    {
        private TextParser parser;
        private TcpClient socket;
        private IOutput output;

        string targetIP;
        int port;
        public ScpiTest(TextParser txtParser, IOutput outPutUI, string targetIP, int port)
        {
            parser = txtParser;

            output = outPutUI;

            this.targetIP = targetIP;
            this.port = port;
            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Tcp);
            socket = new TcpClient();
           
            
        }

      
        
        public void Run()
        {
            if (ConnectToInstrument() == true)
            {
                ScpiCommand scpiCmd=new ScpiCommand();
                bool status = true;
                while (true==status && parser.GetNextLine(ref scpiCmd)==true)
                {
                    switch (scpiCmd.Type)
                    {
                        case ScpiCommandType.SET:
                            output.ShowCommand(scpiCmd);
                            status=SendToInstrument(scpiCmd.Cmd);
                            break;
                        case ScpiCommandType.QUERY:
                            output.ShowCommand(scpiCmd);
                            status=SendToInstrument(scpiCmd.Cmd);
                            string replyStr="";
                            if (true == status)
                            {
                                status = ReadFromInstrument(ref replyStr);
                            }
                            output.ShowResult(replyStr);
                            break;
                        case ScpiCommandType.WAIT:
                            output.ShowWait(scpiCmd);
                            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
                            break;
                    }
                    
                }
                DisconnectFromInstrument();

            }

           
        }

        private bool ConnectToInstrument()
        {
            bool status = true;
            try
            {
                socket.Connect(IPAddress.Parse(targetIP), port);
            }
            catch (Exception e)
            {
                status = false;
                throw e;
                
            }

            return status;
        }

        private bool SendToInstrument(string cmdStr)
        {
            bool status = true;
            try
            {
                Stream stm = socket.GetStream();

                ASCIIEncoding asen = new ASCIIEncoding();
                byte[] ba = asen.GetBytes(cmdStr);
                //Add the termintaion character
                byte[] term ={ 0x0a};// { 0x0d, 0x0a };
                byte[] buf = new byte[asen.GetByteCount(cmdStr) + term.GetLength(0)];
                Array.Copy(ba, buf, asen.GetByteCount(cmdStr));
                Array.Copy(term, 0, buf, asen.GetByteCount(cmdStr), term.GetLength(0));


                stm.Write(buf, 0, buf.Length);
            }

            catch (Exception e)
            {
                output.ShowError(e.Message);
                status = false;
            }
            return status;

        }

        private bool ReadFromInstrument(ref string str)
        {
             bool status = true;
            try
            {
            Stream stm = socket.GetStream();
            byte[] bb = new byte[100];
            int k = stm.Read(bb, 0, 100);

            str = System.Text.ASCIIEncoding.ASCII.GetString(bb,0,k);
            str = str.Remove(str.IndexOf("\0"));
                str = str.Trim();
            }
                
           catch (Exception e)
            {
                output.ShowError(e.Message);
                status = false;
            }
            return status;
        }
        
        private void DisconnectFromInstrument()
        {
            socket.Close();
        }



    }
}
