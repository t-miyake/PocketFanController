using System;
using System.Linq;
using System.Diagnostics;
using Microsoft.Win32;

namespace PocketFanController
{
    public sealed class Model
    {
        // Singleton instance.
        public static Model Instance { get; } = new Model();
        private Model(){}

        public int CurrentState { get; set; } = 0;

        public void GetCurrentStatus()
        {
            var state0 = new[] {40, 60, 75};
            var state1 = new[] {99, 99, 10};
            var state2 = new[] {99, 10, 99};
            var state3 = new[] {10, 99, 99};
            var states = new[] {state0, state1, state2, state3};

            var t0 = ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0");
            var t1 = ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1");
            var t2 = ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2");
            var current = new[] {t0, t1, t2};

            foreach (var s in states.Select((value, index) => new {Value = value, Index = index}))
            {
                if (s.Value.SequenceEqual(current))
                {
                    CurrentState = s.Index;
                }
            }
        }

        public void SetDefault()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 40);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 60);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 75);
            RestartService();
        }

        public void SetLow()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 10);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 99);
            RestartService();
        }

        public void SetMiddle()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 10);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 99);
            RestartService();
        }

        public void SetFast()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 10);
            RestartService();
        }

        public void WriteReg(string subKey, string keyName, int value)
        {
            var key = Registry.LocalMachine.CreateSubKey(subKey);
            key?.SetValue(keyName, value, RegistryValueKind.DWord);
            key?.Close();
        }

        public int ReadReg(string subKey, string keyName)
        {
            try
            {
                var key = Registry.LocalMachine.OpenSubKey(subKey);
                if (key != null) return (int)key.GetValue(keyName);
            }
            catch
            {
                return 0;

            }
            return 0;
        }

        public void RestartService()
        {
            //サービスを停止したり再起動できないので、exeをkillする
            var oldProcessesprocess = Process.GetProcessesByName("FanMonitorService");
            foreach (var p in oldProcessesprocess)
            {
                p.Kill();
            }

            //サービスのスタートがアプリ側からできないので、コマンドプロンプトからスタートさせる
            var cmd = new Process
            {
                StartInfo =
                {
                    FileName = Environment.GetEnvironmentVariable("ComSpec"),
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    Arguments = @"/c net start ""Fan Monitor"""
                }
            };
            cmd.Start();
        }
    }
}