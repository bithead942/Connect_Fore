using System;
using System.IO.Ports;
using System.Text;

namespace BallDispenser
{
    public class BallDispenserControl
    {
        string Arduino_Port = "COM7";
        static SerialPort _serialPort;
        string Status = "";

        public BallDispenserControl()
        {
        }  //Constructor

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


        #region 0_Light_Off
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
        #endregion

        #region 1_Red_Dispense
        public bool RedDispense_Retry()
        {
            bool response = false;

            response = RedDispense();

            while (Status != "Success")
            {
                response = RedDispense();
            }

            return response;
        }

        bool RedDispense()
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
        #endregion

        #region 2_Red_Open
        public bool RedOpen_Retry()
        {
            bool response = false;

            response = RedOpen();

            while (Status != "Success")
            {
                response = RedOpen();
            }

            return response;
        }

        bool RedOpen()
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(2)");

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
        #endregion

        #region 3_Red_Close
        public bool RedClose_Retry()
        {
            bool response = false;

            response = RedClose();

            while (Status != "Success")
            {
                response = RedClose();
            }

            return response;
        }

        bool RedClose()
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(3)");

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
        #endregion

        #region 4_Blue_Dispense
        public bool BlueDispense_Retry()
        {
            bool response = false;

            response = BlueDispense();

            while (Status != "Success")
            {
                response = BlueDispense();
            }

            return response;
        }

        bool BlueDispense()
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(4)");

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
        #endregion


        #region 5_Blue_Open
        public bool BlueOpen_Retry()
        {
            bool response = false;

            response = BlueOpen();

            while (Status != "Success")
            {
                response = BlueOpen();
            }

            return response;
        }

        bool BlueOpen()
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(5)");

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
        #endregion

        #region 6_Blue_Close
        public bool BlueClose_Retry()
        {
            bool response = false;

            response = BlueClose();

            while (Status != "Success")
            {
                response = BlueClose();
            }

            return response;
        }

        bool BlueClose()
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(6)");

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
        #endregion
    }

}
