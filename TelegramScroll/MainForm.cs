using System;
using System.Linq;
using System.Windows.Forms;
using Process.NET;
using Process.NET.Memory;
using Process.NET.Patterns;

namespace TelegramScroll
{
    public partial class MainForm : Form
    {
        private IntPtr _address;
        private ProcessSharp _processSharp;

        public MainForm()
        {
            InitializeComponent();
        }

        private void OnLoad(object sender, EventArgs e)
        {
            try
            {
                var telegramProcess = System.Diagnostics.Process.GetProcessesByName("Telegram").FirstOrDefault();
                if (telegramProcess == null) throw new Exception("telegram isn't running");
                _processSharp = new ProcessSharp(telegramProcess, MemoryType.Local);
                _processSharp.Memory = new ExternalProcessMemory(_processSharp.Handle);
                var scanner = new PatternScanner(_processSharp.ModuleFactory.MainModule);
                // function 013A5A40 takes new msg count as parameter, which is compared to current unread msg count,
                // then overwrites it; in-between, it's compared against 1; nop the jnz after the cmp to keep scrolling
                var pattern = new DwordPattern("?? ?? 8A 9F 0D 01 00 00");
                var scanResult = scanner.Find(pattern);
                if (!scanResult.Found) throw new Exception("something broke");
                _address = scanResult.ReadAddress;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void OnCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            if (checkBox.Checked)
            {
                // nop; nop
                _processSharp.Memory.Write(_address, new byte[] {0x90, 0x90});
                checkBox.Text = "disable";
            }
            else
            {
                // jnz 164bf23
                _processSharp.Memory.Write(_address, new byte[] {0x75, 0x79});
                checkBox.Text = "enable";
            }
        }
    }
}