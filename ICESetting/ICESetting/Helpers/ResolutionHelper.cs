using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;


namespace Jisons
{

    /// <summary> 分辨率信息 </summary>
    public class Devmode
    {
        /// <summary> 颜色质量,如32位,24位 </summary>
        public int BitsPerPel { get; set; }

        /// <summary> 刷新频率,如75Hz </summary>
        public int DisplayFrequency { get; set; }

        /// <summary> 分辨率宽度，如1024 </summary>
        public int PelsWidth { get; set; }

        /// <summary> 分辨率高度,如768 </summary>
        public int PelsHeight { get; set; }

        public static implicit operator Devmode(SystemDevmode value)
        {  
            return new Devmode() { BitsPerPel = value.BitsPerPel, DisplayFrequency = value.DisplayFrequency, PelsWidth = value.PelsWidth, PelsHeight = value.PelsHeight };
        }

        public override bool Equals(object obj)
        {
            var value = obj as Devmode;
            if (value != null)
            {
                return value.PelsHeight == this.PelsHeight && value.PelsWidth == this.PelsWidth;
            }

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.PelsHeight.GetHashCode() ^ this.PelsWidth.GetHashCode();
        }

    }

    /// <summary> 设置分辨率 </summary>
    public static class ResolutionHelper
    {

        /// <summary> 获取当前分辨率信息 </summary>
        /// <returns></returns>
        public static Devmode GetCurrentDevmode()
        {
            return SystemResolution.Instance.GetResolution();
        }

        /// <summary> 获取系统所支持的分辨率 </summary>
        /// <param name="minwidth"> 最小宽度 </param>
        /// <param name="minheight"> 最小高度 </param>
        /// <returns></returns>
        public static List<Devmode> GetSettingDevmodes(int minwidth = 800, int minheight = 600)
        {
            return SystemResolution.Instance.GetAllResolution().Where(i => i.PelsHeight >= minheight && i.PelsWidth >= minwidth).Distinct(Equality<SystemDevmode>.CreateComparer(p => p.PelsHeight + p.PelsWidth)).Select<SystemDevmode, Devmode>((s) => (Devmode)s).ToList();
        }

        /// <summary> 设置当前分辨率 </summary>
        /// <param name="devmode"></param>
        /// <param name="DisplayFrequency"> 刷新频率，默认 60Hz </param>
        /// <returns></returns>
        public static bool SetCurrentDevmode(this Devmode devmode, int DisplayFrequency = 60)
        {
            return SystemResolution.Instance.SetResolution(devmode.PelsWidth, devmode.PelsHeight, DisplayFrequency, devmode.BitsPerPel);
        }

    }

    #region 系统分辨率设置

    internal enum DMDO
    {
        DEFAULT = 0,
        D90 = 1,
        D180 = 2,
        D270 = 3
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct SystemDevmode
    {
        internal const int DM_DISPLAYFREQUENCY = 0x400000;
        internal const int DM_PELSWIDTH = 0x80000;
        internal const int DM_PELSHEIGHT = 0x100000;
        private const int CCHDEVICENAME = 32;
        private const int CCHFORMNAME = 32;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHDEVICENAME)]
        internal string DeviceName;
        internal short SpecVersion;
        internal short DriverVersion;
        internal short Size;
        internal short DriverExtra;
        internal int Fields;

        internal int PositionX;
        internal int PositionY;
        internal DMDO DisplayOrientation;
        internal int DisplayFixedOutput;

        internal short Color;
        internal short Duplex;
        internal short YResolution;
        internal short TTOption;
        internal short Collate;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = CCHFORMNAME)]
        internal string FormName;
        internal short LogPixels;

        /// <summary> 颜色质量,如32位,24位 </summary>
        public int BitsPerPel;

        /// <summary> 分辨率宽度，如1024 </summary>
        public int PelsWidth;

        /// <summary> 分辨率高度,如768 </summary>
        public int PelsHeight;

        internal int DisplayFlags;

        /// <summary> 刷新频率,如75Hz </summary>
        public int DisplayFrequency;

        internal int ICMMethod;
        internal int ICMIntent;
        internal int MediaType;
        internal int DitherType;
        internal int Reserved1;
        internal int Reserved2;
        internal int PanningWidth;
        internal int PanningHeight;
    }

    internal class SystemResolution
    {
        private static SystemResolution instance;
        internal static SystemResolution Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SystemResolution();
                }
                return instance;
            }
        }

        private const int CDS_UPDATEREGISTRY = 0x01;
        private const int CDS_TEST = 0x02;
        private const int DISP_CHANGE_SUCCESSFUL = 0;
        private const int DISP_CHANGE_RESTART = 1;
        private const int DISP_CHANGE_FAILED = -1;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int ChangeDisplaySettings([In] ref SystemDevmode lpDevMode, int dwFlags);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool EnumDisplaySettings(string lpszDeviceName, Int32 iModeNum, ref   SystemDevmode lpDevMode);

        private SystemResolution()
        { }

        //设置分辨率,width宽,height高,displayFrequency刷新频率,设置成功返回true,否则false
        //调用方式： setResolution(1024, 768, 75);
        internal bool SetResolution(int width, int height, int displayFrequency)
        {
            bool ret = false;
            long RetVal = 0;
            SystemDevmode dm = new SystemDevmode();
            dm.Size = (short)Marshal.SizeOf(typeof(SystemDevmode));
            dm.PelsWidth = width;
            dm.PelsHeight = height;
            dm.DisplayFrequency = displayFrequency;
            dm.Fields = SystemDevmode.DM_PELSWIDTH | SystemDevmode.DM_PELSHEIGHT | SystemDevmode.DM_DISPLAYFREQUENCY;
            RetVal = ChangeDisplaySettings(ref dm, CDS_TEST);
            if (RetVal == 0)
            {
                RetVal = ChangeDisplaySettings(ref dm, 0);
                ret = true;
            }
            return ret;
        }

        //设置分辨率,width宽,height高,displayFrequency刷新频率,bitsPerPel颜色位数，设置成功返回true,否则false
        //调用方式： setResolution(1024, 768, 75, 32);
        internal bool SetResolution(int width, int height, int displayFrequency, int bitsPerPel)
        {
            bool ret = false;
            long RetVal = 0;
            var dm = new SystemDevmode();
            dm.Size = (short)Marshal.SizeOf(typeof(SystemDevmode));
            dm.PelsWidth = width;
            dm.PelsHeight = height;
            dm.DisplayFrequency = displayFrequency;
            dm.BitsPerPel = bitsPerPel;
            dm.Fields = SystemDevmode.DM_PELSWIDTH | SystemDevmode.DM_PELSHEIGHT | SystemDevmode.DM_DISPLAYFREQUENCY;
            RetVal = ChangeDisplaySettings(ref   dm, CDS_TEST);
            if (RetVal == 0)
            {
                RetVal = ChangeDisplaySettings(ref dm, 0);
                ret = true;
            }
            return ret;
        }

        //返回当前图形模式信息
        internal SystemDevmode GetResolution()
        {
            var dm = new SystemDevmode();
            dm.Size = (short)Marshal.SizeOf(typeof(SystemDevmode));
            bool mybool;
            mybool = EnumDisplaySettings(null, -1, ref dm);
            return dm;
        }

        //返回所有支持图形模式
        internal List<SystemDevmode> GetAllResolution()
        {
            var allMode = new List<SystemDevmode>();
            var dm = new SystemDevmode();
            dm.Size = (short)Marshal.SizeOf(typeof(SystemDevmode));
            int index = 0;
            while (EnumDisplaySettings(null, index, ref dm))
            {
                //Windows 系统默认最小 800 * 600
                if (dm.PelsWidth >= 800 && dm.PelsHeight >= 600)
                {
                    allMode.Add(dm);
                }
                index++;
            }

            allMode.Reverse();

            return allMode;
        }

    }

    #endregion

}
