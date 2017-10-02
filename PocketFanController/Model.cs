using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using Microsoft.Win32;
using OpenHardwareMonitor.Hardware;

namespace PocketFanController
{
    public sealed class Model
    {
        // Singleton instance.
        public static Model Instance { get; } = new Model();

        private Model(){}

        public int CurrentState { get; set; } = 0;

        public int ManualMargin { get; set; }
        public int ManualT0 { get; set; }
        public int ManualT1 { get; set; }
        public int ManualT2 { get; set; }

        public void GetManualConfigs()
        {
            ManualMargin = ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedmargin") == 0
                ? 5
                : ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedmargin");
            ManualT0 = ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt0") == 0
                ? 40
                : ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt0");
            ManualT1 = ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt1") == 0
                ? 60
                : ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt1");
            ManualT2 = ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt2") == 0
                ? 75
                : ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt2");
        }

        public void GetCurrentStatus()
        {
            var state0 = new[] {40, 60, 75};
            var state1 = new[] {99, 99, 10};
            var state2 = new[] {99, 10, 99};
            var state3 = new[] {10, 99, 99};
            var state4 = new[] {99, 99, 85};
            var states = new[] {state0, state1, state2, state3, state4};

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

            if (CurrentState != 5)
            {
                if (current.SequenceEqual(states[CurrentState])) return;
                CurrentState = 5;
            }
        }

        public void SetDefault()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "margin", 5);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 40);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 60);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 75);
            RestartService();
        }

        public void SetSlow()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "margin", 5);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 10);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 99);
            RestartService();
        }

        public void SetFast()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "margin", 5);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 10);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 99);
            RestartService();
        }

        public void SetFastest()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "margin", 5);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 10);
            RestartService();
        }

        public void SetSlowest()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "margin", 5);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 99);
            //熱くなりすぎたらファンを全開にする。
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 85);
            RestartService();
        }

        public void SetManual()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "margin",
                ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedmargin"));
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0",
                ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt0"));
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1",
                ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt1"));
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2",
                ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt2"));
            RestartService();
        }

        public void SaveManualConfig(int margin, int t0, int t1, int t2)
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedmargin", margin);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt0", t0);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt1", t1);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "savedt2", t2);
        }

        public void SaveLastState() => WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "laststate",CurrentState);

        public void LoadAndResetLastState()
        {
            var lastState = ReadReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "laststate");
            switch (lastState)
            {
                case 0:
                    SetDefault();
                    break;
                case 1:
                    SetFastest();
                    break;
                case 2:
                    SetFast();
                    break;
                case 3:
                    SetSlow();
                    break;
                case 4:
                    SetSlowest();
                    break;
                case 5:
                    SetManual();
                    break;
                default:
                    SetDefault();
                    break;
            }
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
            var oldProcesses = Process.GetProcessesByName("FanMonitorService");
            foreach (var p in oldProcesses)
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

        public string GetCpuTemp()
        {
            var computer = new Computer
            {
                MainboardEnabled = false,
                CPUEnabled = true,
                RAMEnabled = false,
                GPUEnabled = false,
                FanControllerEnabled = false,
                HDDEnabled = false
            };

            computer.Open();

            var temps = new List<string>();

            foreach (var item in computer.Hardware)
            {
                if (item.HardwareType != HardwareType.CPU) continue;
                item.Update();
                temps.AddRange(from sensor in item.Sensors where sensor.SensorType == SensorType.Temperature where sensor.Value != null select sensor.Value.Value.ToString(CultureInfo.CurrentCulture));
            }

            computer.Close();

            //最初に取得できるのが、現在の温度。
            return temps[0];
        }

    }
}