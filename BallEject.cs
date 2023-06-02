using Pololu.Usc;
using Pololu.UsbWrapper;

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace BallEject
{
    public class EjectServoControl
    {
        bool Connected = false;
        string Status = "";
        bool Ejecting = false;

        public EjectServoControl()  //Constructor
        {
        }

        public bool Eject()
        {
            try
            {
                Ejecting = true;
                Col1Open();
                System.Threading.Thread.Sleep(3000);
                Col1Close();
                Col2Open();
                System.Threading.Thread.Sleep(3000);
                Col2Close();
                Col3Open();
                System.Threading.Thread.Sleep(3000);
                Col3Close();
                Col4Open();
                System.Threading.Thread.Sleep(3000);
                Col4Close();
                Col5Open();
                System.Threading.Thread.Sleep(3000);
                Col5Close();
                Col6Open();
                System.Threading.Thread.Sleep(3000);
                Col6Close();
                Col7Open();
                System.Threading.Thread.Sleep(3000);
                Col7Close();
                System.Threading.Thread.Sleep(200);
                BallEject_Off();
                Status = "Success";
                Ejecting = false;
                return true;
            }
            catch
            {
                Status = "Error";
                return false;
            }

        }

        public bool PinsUp()
        {
            try
            {
                Col1Close();
                Col2Close();
                Col3Close();
                Col4Close();
                Col5Close();
                Col6Close();
                Col7Close();
                System.Threading.Thread.Sleep(200);
                BallEject_Off();
                Status = "Success";
                return true;
            }
            catch
            {
                Status = "Error";
                return false;
            }
        }

        public bool PinsDown()
        {
            try
            {
                Col1Open();
                Col2Open();
                Col3Open();
                Col4Open();
                Col5Open();
                Col6Open();
                Col7Open();
                System.Threading.Thread.Sleep(200);
                BallEject_Off();
                Status = "Success";
                return true;
            }
            catch
            {
                Status = "Error";
                return false;
            }
        }

        public bool IsOpen()
        {
            return Connected;
        }

        public string LastActionStatus()
        {
            return Status;
        }

        public bool isEjecting()
        {
            return Ejecting;
        }

        #region PIN CONTROL

        void Col1Open()
        {
            TrySetTarget(0, 5952);
        }

        void Col1Close()
        {
            TrySetTarget(0, 4288);
        }

        void Col2Open()
        {
            TrySetTarget(1, 6272);
        }

        void Col2Close()
        {
            TrySetTarget(1, 4224);
        }

        void Col3Open()
        {
            TrySetTarget(2, 7680);
        }

        void Col3Close()
        {
            TrySetTarget(2, 5824);
        }

        void Col4Open()
        {
            TrySetTarget(3, 8000);
        }

        void Col4Close()
        {
            TrySetTarget(3, 5888);
        }

        void Col5Open()
        {
            TrySetTarget(4, 7900);
        }

        void Col5Close()
        {
            TrySetTarget(4, 5000);
        }

        void Col6Open()
        {
            TrySetTarget(5, 7900);
        }

        void Col6Close()
        {
            TrySetTarget(5, 4352);
        }

        void Col7Open()
        {
            TrySetTarget(6, 6976);
        }

        void Col7Close()
        {
            TrySetTarget(6, 5312);
        }

        void BallEject_Off()
        {
            TrySetTarget(0, 0);
            TrySetTarget(1, 0);
            TrySetTarget(2, 0);
            TrySetTarget(3, 0);
            TrySetTarget(4, 0);
            TrySetTarget(5, 0);
            TrySetTarget(6, 0);
        }
        #endregion

        /// <summary>
        /// Attempts to set the target (width of pulses sent) of a channel.
        /// </summary>
        /// <param name="channel">Channel number from 0 to 23.</param>
        /// <param name="target">
        ///   Target, in units of quarter microseconds.  For typical servos,
        ///   6000 is neutral and the acceptable range is 4000-8000.
        /// </param>
        public void TrySetTarget(Byte channel, UInt16 target)
        {
            using (Usc device = connectToDevice())  // Find a device and temporarily connect.
            {
                device.setTarget(channel, target);

                // device.Dispose() is called automatically when the "using" block ends,
                // allowing other functions and processes to use the device.
            }
        }

        /// <summary>
        /// Connects to a Maestro using native USB and returns the Usc object
        /// representing that connection.  When you are done with the
        /// connection, you should close it using the Dispose() method so that
        /// other processes or functions can connect to the device later.  The
        /// "using" statement can do this automatically for you.
        /// </summary>
        Usc connectToDevice()
        {
            // Get a list of all connected devices of this type.
            List<DeviceListItem> connectedDevices = Usc.getConnectedDevices();
            foreach (DeviceListItem dli in connectedDevices)
            {
                // If you have multiple devices connected and want to select a particular
                // device by serial number, you could simply add a line like this:
                if (dli.serialNumber == "00281341")
                {
                    Usc device = new Usc(dli); // Connect to the device.
                    Connected = true;
                    return device;             // Return the device.
                }
            }
            Connected = false;
            throw new Exception("Could not find device.  Make sure it is plugged in to USB " +
                "and check your Device Manager (Windows) or run lsusb (Linux).");

        }

    }
}
