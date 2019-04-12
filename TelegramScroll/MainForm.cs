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
                var pattern = new DwordPattern("8B 40 04 89 4D E0 89 45 E8");
                var scanResult = scanner.Find(pattern);
                if (!scanResult.Found) throw new Exception("something broke");
                _address = scanResult.ReadAddress + pattern.GetBytes().Count;
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
                // xor eax, eax; nop
                _processSharp.Memory.Write(_address, new byte[] {0x90, 0x90});
                checkBox.Text = "disable";
            }
            else
            {
                // mov eax, dword [ebp+8]
                _processSharp.Memory.Write(_address, new byte[] {0x75, 0x79});
                checkBox.Text = "enable";
            }
        }
    }
}