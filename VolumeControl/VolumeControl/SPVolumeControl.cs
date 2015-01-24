using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using CoreAudioApi;
using System.Windows.Controls;

namespace Jisons
{
    public class SPVolumeControl
    {
        private static SPVolumeControl instance;
        public static SPVolumeControl Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SPVolumeControl();
                }
                return instance;
            }
        }

        private static int PIDCurrent { get; set; }
        private static List<SimpleAudioVolume> AudioCurrentList { get; set; }

        private static SimpleAudioVolume AudioCurrent { get; set; }
        private static AudioEndpointVolume AudioSystem { get; set; }
        private static MMDevice DeviceSystem { get; set; }

        /// <summary>
        /// 当前程序音量，默认加载时为 1 
        /// </summary>
        public float VolumeCurrent
        {
            get
            {
                if (AudioCurrent == null)
                {
                    return 1f;
                }
                return AudioCurrent.MasterVolume;
            }
            set
            {
                SetCurrentVolume(value);
            }
        }

        public float VolumeSystem
        {
            get
            {
                return AudioSystem.MasterVolumeLevelScalar;
            }
            set
            {
                SetSystemVolume(value);
            }
        }

        private SPVolumeControl()
        {
            PIDCurrent = Process.GetCurrentProcess().Id;
            FindEquipment();

            //需要使用一次系统声音，才能在音量控制中加载
            MediaElement initelement = new MediaElement();
        }

        private void FindEquipment()
        {
            try
            {
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                DeviceSystem = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eCommunications);
                var activeList = DeviceSystem.AudioSessionManager;
                var activeCount = activeList.Sessions.Count;
                AudioCurrentList = new List<SimpleAudioVolume>();
                for (int i = 0; i < activeCount; i++)
                {
                    var dre = Process.GetCurrentProcess();
                    var valumInfo = activeList.Sessions[i];
                    if (PIDCurrent == valumInfo.ProcessID)
                    {
                        AudioCurrent = valumInfo.SimpleAudioVolume;
                    }
                    AudioCurrentList.Add(valumInfo.SimpleAudioVolume);
                }
                AudioSystem = DeviceSystem.AudioEndpointVolume;
            }
            catch { }
        }

        public void SetSystemVolume(float volume)
        {
            AudioSystem.MasterVolumeLevelScalar = volume;
        }

        public void SetCurrentVolume(float volume)
        {
            try
            {
                MMDeviceEnumerator DevEnum = new MMDeviceEnumerator();
                DeviceSystem = DevEnum.GetDefaultAudioEndpoint(EDataFlow.eRender, ERole.eCommunications);
                var activeList = DeviceSystem.AudioSessionManager;
                var activeCount = activeList.Sessions.Count;
                AudioCurrentList = new List<SimpleAudioVolume>();
                for (int i = 0; i < activeCount; i++)
                {
                    var dre = Process.GetCurrentProcess();
                    var valumInfo = activeList.Sessions[i];
                    if (PIDCurrent == valumInfo.ProcessID)
                    {
                        AudioCurrent = valumInfo.SimpleAudioVolume;
                        AudioCurrent.MasterVolume = volume;
                    }
                    AudioCurrentList.Add(valumInfo.SimpleAudioVolume);
                }
                AudioSystem = DeviceSystem.AudioEndpointVolume;
            }
            catch { }
        }
    }

}