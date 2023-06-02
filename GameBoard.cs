using System;
using System.IO.Ports;
using System.Text;

namespace GameBoard
{
    public class GameBoardControl
    {
        string Arduino_Port = "COM6";
        static SerialPort _serialPort;
        string Status = "";

        public GameBoardControl()
        {
        }

        public bool Connect()
        {
            if (_serialPort == null)
            {
                _serialPort = new SerialPort
                {
                    PortName = Arduino_Port,
                    BaudRate = 115200,
                    DtrEnable = true,
                    RtsEnable = true
                };
            }
            try
            {
                if (!_serialPort.IsOpen)
                {
                    _serialPort.Open();
                    if (GetACK())
                    {
                        Status = "Success";
                        return true;
                    }
                }
                Status = "Error";
                return false;

            }
            catch
            {
                //_serialPort.Dispose();
                _serialPort = null;
                Status = "Not Connected";
                return false;
            }
        }

        bool GetACK()
        {
            bool response;
            int inVal = 0;
            int port_timeout = 0;

            try
            {
                if (_serialPort != null && _serialPort.IsOpen)
                {
                    while (_serialPort.BytesToRead == 0)
                    {
                        //Wait until bootup and response
                        System.Threading.Thread.Sleep(10);
                        port_timeout++;
                        if (port_timeout >= 500)  //5 seconds
                        {
                            //throw error
                            _serialPort.Close();
                            _serialPort = null;
                            Status = "Timeout";
                            response = false;
                            break;
                        }
                    }

                    if (_serialPort != null)
                    {
                        if (_serialPort.BytesToRead > 0)
                        {
                            inVal = _serialPort.ReadChar();

                            if (inVal != 6)   //ACK
                            {
                                //throw error
                                //_serialPort.Close();
                                //_serialPort = null;
                                Status = "NAK";
                            }

                            //if (Flush())
                            //{
                                response = true;
                                Status = "Success";
                            //}
                            //else
                            //{
                            //    response = false;
                            //    Status = "Not connected";
                            //}
                        }
                        else
                        {
                            response = false;
                            Status = "Nothing to read";
                        }
                    }
                    else
                    {
                        response = false;
                        Status = "Not connected";
                    }
                    return response;
                }
                else
                {
                    Status = "Not connected";
                    return false;
                }
            }
            catch
            {
                Status = "Not connected";
                return false;
            }
        }

        bool Flush()
        {
            int response = 0;

            try
            {
                while (_serialPort.BytesToRead > 0)
                {
                    response = _serialPort.ReadChar();
                    //throw away extra chars
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string LastActionStatus()
        {
            return Status;
        }

        public bool IsOpen()
        {
            if (_serialPort != null)
            {
                return _serialPort.IsOpen;
            }
            else
            {
                return false;
            }
        }

        public bool Disconnect()
        {
            if (_serialPort != null && _serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = null;
            }
            return (true);
        }

        public string GetBoard_Retry()
        {
            string response = "";

            response = GetBoard();

            while (Status != "Success")
            {
                response = GetBoard();
            }

            return response;
        }

        string GetBoard()
        {
            byte[] Board = new byte[400];
            int port_timeout = 0;
            int response;

            try
            {
                Connect();
                if (IsOpen())
                {
                    if (Flush())
                    {
                        _serialPort.Write("(4)");
                        System.Threading.Thread.Sleep(2300);

                        while (_serialPort.BytesToRead < 336)   //Wait for response
                        {
                            //Wait until bootup and response
                            System.Threading.Thread.Sleep(25);
                            port_timeout++;
                            if (port_timeout >= 200)  //5 seconds
                            {
                                //throw error
                                _serialPort.Close();
                                _serialPort = null;
                                Status = "Timeout";
                                return "";
                            }
                        }

                        if (_serialPort != null)
                        {
                            if (_serialPort.BytesToRead == 336)
                            {
                                response = _serialPort.ReadByte();
                                if (response == 6) //ACK
                                {
                                    //Clear next 2 bytes
                                    response = _serialPort.ReadByte();    //CHR(13)
                                    response = _serialPort.ReadByte();    //CHR(10)
                                }
                                else
                                {
                                    Status = "NAK";
                                    Flush();
                                    return "";
                                }

                                while (_serialPort.BytesToRead > 0)
                                {
                                    //read JSON
                                    _serialPort.Read(Board, 0, _serialPort.BytesToRead);
                                }
                                Status = "Success";
                                return Encoding.UTF8.GetString(Board).Trim();
                            }
                            else
                            {
                                Status = "Nothing to read";
                                return "";
                            }
                        }
                        else
                        {
                            Status = "Not connected";
                            return "";
                        }
                    }
                    {
                        Status = "Not connected";
                        return "";
                    }

                }
                else
                {
                    Status = "Not connected";
                    return "";
                }
            }
            catch
            {
                Status = "Not connected";
                return "";
            }
        }

        public bool LightOn_Retry()
        {
            bool response = false;

            response = LightOn();

            while (Status != "Success")
            {
                response = LightOn();
            }

            return response;
        }

        bool LightOn()
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(1)");

                    return GetACK();
                }
                else
                {
                    Status = "Not connected";
                    return false;
                }
            }
            else
            {
                Status = "Not connected";
                return false;
            }


        }

        public bool LightOff_Retry()
        {
            bool response = false;

            response = LightOff();

            while (Status != "Success")
            {
                response = LightOff();
            }

            return response;
        }

        public bool LightOff()
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(0)");

                    return GetACK();
                }
                else
                {
                    Status = "Not connected";
                    return false;
                }
            }
            else
            {
                Status = "Not connected";
                return false;
            }
        }

        public bool BlueWins_Retry(int _startCol, int _numCols)
        {
            bool response = false;

            response = BlueWins(_startCol, _numCols);

            while (Status != "Success")
            {
                response = BlueWins(_startCol, _numCols);
            }

            return response;
        }

        bool BlueWins(int _startCol, int _numCols)
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(2" + _startCol.ToString() + _numCols.ToString() + ")");
                    return GetACK();
                }
                else
                {
                    Status = "Not connected";
                    return false;
                }
            }
            else
            {
                Status = "Not connected";
                return false;
            }
        }

        public bool RedWins_Retry(int _startCol, int _numCols)
        {
            bool response = false;

            response = RedWins(_startCol, _numCols);

            while (Status != "Success")
            {
                response = RedWins(_startCol, _numCols);
            }

            return response;
        }

        bool RedWins(int _startCol, int _numCols)
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(3" + _startCol.ToString() + _numCols.ToString() + ")");
                    return GetACK();
                }
                else
                {
                    Status = "Not connected";
                    return false;
                }
            }
            else
            {
                Status = "Not connected";
                return false;
            }

        }
    }
}
