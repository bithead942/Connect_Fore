using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace Robot
{
    public class RobotControl
    {
        string Arduino_Port = "COM9";
        static SerialPort _serialPort;
        string Status = "";

        public RobotControl()
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
                        if (port_timeout >= 600)  //2 seconds
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
                                _serialPort.Close();
                                _serialPort = null;
                                Status = "NAK";
                            }
                            response = true;
                            Status = "Success";
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
            if (IsOpen())
            {
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = null;
                Status = "Success";
            }
            else
            {
                Status = "Already Closed";
            }
            return (true);
        }

        #region "0 Light Off"
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

        #region "1 Dispense Blue Ball"
        public bool DispenseBlue()
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

        #region "2 Blue Open"
        public bool BlueOpen()
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

        #region "3 Blue Close"
        public bool BlueClose()
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

        #region "4 Laser On"
        public bool LaserOn()
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

        #region "5 Laser Off"
        public bool LaserOff()
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

        #region "6 Launcher On"
        public bool LauncherOn(String _Speed)
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(6" + _Speed + ")");

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

        #region "7 Launcher Off"
        public bool LauncherOff()
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(7)");

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

        #region "8 Aim"
        public bool Aim_Retry(String _TargetHole)
        {
            bool response = false;

            Status = "";

            while (Status != "Success")
            {
                response = Aim(_TargetHole);
            }

            return response;
        }

        bool Aim(String _TargetHole)
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(8" + _TargetHole + ")");
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

        #region "9 Aim + Fire"
        public bool AimFire_Retry(String _TargetHole)
        {
            String _Speed = "";
            bool response = false;

            switch (_TargetHole)
            {
                case "1":
                    _Speed = "135";
                    break;
                case "2":
                    _Speed = "132";
                    break;
                case "3":
                    _Speed = "130";
                    break;
                case "4":
                    _Speed = "125";
                    break;
                case "5":
                    _Speed = "130";
                    break;
                case "6":
                    _Speed = "132";
                    break;
                case "7":
                    _Speed = "135";
                    break;
                default:
                    _Speed = "130";
                    break;
            }


            Status = "";

            while (Status != "Success")
            {
                response = AimFire(_TargetHole, _Speed);
            }

            return response;
        }

        bool AimFire(String _TargetHole, String _Speed)
        {
            Connect();
            if (IsOpen())
            {
                if (Flush())
                {
                    _serialPort.Write("(9" + _TargetHole + _Speed + ")");
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
