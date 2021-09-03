﻿using System;
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
                // why do they keep changing this specific function
                var pattern = new DwordPattern("EB 7E 85 DB 75 58");
                var scanResult = scanner.Find(pattern);
                if (!scanResult.Found) throw new Exception("something broke");
                _address = scanResult.ReadAddress + 2;
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
                // xor edi, edi; nop; nop
                _processSharp.Memory.Write(_address, new byte[] {0x31, 0xdb, 0x90, 0x90});
                checkBox.Text = "disable";
            }
            else
            {
                // test edi, edi; jnz 0078bfa5
                _processSharp.Memory.Write(_address, new byte[] {0x85, 0xdb, 0x75, 0x58});
                checkBox.Text = "enable";
            }
        }
    }
}