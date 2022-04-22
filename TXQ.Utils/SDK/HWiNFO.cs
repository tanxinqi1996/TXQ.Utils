using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TXQ.Utils.Tool;

namespace TXQ.Utils.SDK
{
    public static class HWiNFO
    {
        static HWiNFO()
        {
            Init();
        }
        public static void Init()
        {
            MMF = MemoryMappedFile.OpenExisting(@"Global\HWiNFO_SENS_SM2", MemoryMappedFileRights.Read);
            accessor = MMF.CreateViewAccessor(0L, Marshal.SizeOf(typeof(HWiNFO_MEMORY)), MemoryMappedFileAccess.Read);
            accessor.Read(0L, out HWiNFOMemory);
            numReadingElements = HWiNFOMemory.dwNumReadingElements;
            offsetReadingSection = HWiNFOMemory.dwOffsetOfReadingSection;
            sizeReadingSection = HWiNFOMemory.dwSizeOfReadingElement;
            var list = new List<SensorInfo>();
            for (uint num = 0U; num < numReadingElements; num += 1U)
            {
                using MemoryMappedViewStream memoryMappedViewStream = MMF.CreateViewStream(offsetReadingSection + num * sizeReadingSection, sizeReadingSection, MemoryMappedFileAccess.Read);
                byte[] array = new byte[sizeReadingSection];
                memoryMappedViewStream.Read(array, 0, (int)sizeReadingSection);
                GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                SensorInfo Result = (SensorInfo)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(SensorInfo));
                Result.Index = num;
                list.Add(Result);
                gchandle.Free();
            }
            AllSensors = list;
        }

        public static void Close()
        {
            if (MMF != null)
            {
                MMF.Dispose();
            }
        }

        public static IEnumerable<SensorInfo> AllSensors;

        private static MemoryMappedFile MMF;

        private static MemoryMappedViewAccessor accessor;

        private static uint numReadingElements;

        private static uint offsetReadingSection;

        private static uint sizeReadingSection;

        private static HWiNFO_MEMORY HWiNFOMemory;


        /// <summary>
        /// 传感器类型
        /// </summary>
        public enum SensorType
        {
            None = 0,
            Temperature = 1,
            Voltage = 2,
            Fan = 3,
            Currten = 4,
            Power = 5,
            Clock = 6,
            Usage = 7,
            Other = 8
        }



        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct SensorInfo
        {
            public readonly SensorType SensorType;

            private readonly uint SensorIndex;

            private readonly uint dwReadingID;


            /// <summary>
            /// 型号
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public readonly string Model;


            /// <summary>
            /// 
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public readonly string Model_LocalLanguange;

            /// <summary>
            /// 单位
            /// </summary>
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            public readonly string Unit;

            /// <summary>
            /// 值
            /// </summary>
            public double Value;

            /// <summary>
            /// 最小值
            /// </summary>
            public double ValueMin;

            /// <summary>
            /// 最大值
            /// </summary>
            public double ValueMax;

            /// <summary>
            /// 平均值
            /// </summary>
            public double ValueAvg;



            internal uint Index;

            /// <summary>
            /// 重新读取
            /// </summary>
            public void ReInit()
            {
                using var MemStr = MMF.CreateViewStream(offsetReadingSection + Index * sizeReadingSection, sizeReadingSection, MemoryMappedFileAccess.Read);
                byte[] array = new byte[sizeReadingSection];
                MemStr.Read(array, 0, (int)sizeReadingSection);
                GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                SensorInfo Result = (SensorInfo)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(SensorInfo));
                Value = Result.Value;
                ValueMin = Result.ValueMin;
                ValueMax = Result.ValueMax;
                ValueAvg = Result.ValueAvg;
                gchandle.Free();

            }

        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HWiNFO_MEMORY
        {
            public uint dwSignature;

            public uint dwVersion;

            public uint dwRevision;

            public long poll_time;

            public uint dwOffsetOfSensorSection;

            public uint dwSizeOfSensorElement;

            public uint dwNumSensorElements;

            public uint dwOffsetOfReadingSection;

            public uint dwSizeOfReadingElement;

            public uint dwNumReadingElements;
        }
    }

}
