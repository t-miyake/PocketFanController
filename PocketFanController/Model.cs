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

        public void GetCurrentStatus()
        {
            var state0 = new[] {40, 60, 75};
            var state1 = new[] {99, 99, 10};
            var state2 = new[] {99, 10, 99};
            var state3 = new[] {10, 99, 99};
            var state4 = new[] {99, 99, 85};
            var states = new[] {state0, state1, state2, state3,state4};

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

        public void SetSlow()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 10);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 99);
            RestartService();
        }

        public void SetFast()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 10);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 99);
            RestartService();
        }

        public void SetFastest()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 10);
            RestartService();
        }

        public void SetSlowest()
        {
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t0", 99);
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t1", 99);
            //熱くなりすぎたらファンを全開にする。
            WriteReg(@"SYSTEM\CurrentControlSet\Services\wfan0109", "t2", 85);
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