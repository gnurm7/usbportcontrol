
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace usbportcontrol
{
    public partial class Form1 : Form
    {
        private NotifyIcon notifyIcon;

        string devconPath = "C:\\Users\\HP\\Desktop\\devcon64.exe";

        public Form1()
        {
            InitializeComponent();

        }

        private void EnablePortsMenuItem_Click(object sender, EventArgs e)
        {
            EnableAllUSBPorts();
        }

        private void DisablePortsMenuItem_Click(object sender, EventArgs e)
        {
            DisableAllUSBPorts();
        }

        private void DisableStorageDevicesMenuItem_Click(object sender, EventArgs e)
        {
            DisableUSBStorageDevices();
        }

        private void RemoveAllDevicesMenuItem_Click(object sender, EventArgs e)
        {
            RemoveAllUSBDevices();
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void RunAsAdminOnCMD(string command)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                WorkingDirectory = devconPath,
                Arguments = "/C " + command,
                Verb = "runas",
            };
            process.StartInfo = startInfo;
            process.Start();
        }

        private void EnableAllUSBPorts()
        {
            try
            {
                RunAsAdminOnCMD(devconPath + " rescan"); // Enable USB power
                RunAsAdminOnCMD(devconPath + " enable USB\\*"); // Enable USB ports
                RunAsAdminOnCMD(devconPath + " enable USBSTOR\\*"); // Enable USB storage devices
                RunAsAdminOnCMD(devconPath + " enable remove usb\\*");
                MessageBox.Show("All USB ports have been enabled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, "An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableAllUSBPorts()
        {
            try
            {
                RunAsAdminOnCMD(devconPath + " disable USB\\*");

                MessageBox.Show("All USB ports have been disabled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, "An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisableUSBStorageDevices()
        {
            try
            {
                RunAsAdminOnCMD(devconPath + " disable USBSTOR\\*");

                MessageBox.Show("USB storage devices have been disabled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, "An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RemoveAllUSBDevices()
        {
            try
            {
                RunAsAdminOnCMD(devconPath + " remove usb\\*");

                MessageBox.Show("All USB devices have been removed. Press the button to open and restart your computer.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(null, "An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void notify_button_Click(object sender, EventArgs e)
        {
            NotifyIcon notifyIcon = new NotifyIcon();

            Icon icon = Icon.ExtractAssociatedIcon(@"C:\Users\HP\Desktop\images.ico");
            notifyIcon.Icon = icon;
            notifyIcon.Text = " ";
            notifyIcon.Visible = true;
            notifyIcon.BalloonTipTitle = "USB Port Control";
            notifyIcon.BalloonTipText = "Click the button you want to use";
            notifyIcon.ShowBalloonTip(300);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
