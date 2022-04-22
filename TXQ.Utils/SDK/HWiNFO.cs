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
        public static void Init()
        {
            try
            {
                mmf = MemoryMappedFile.OpenExisting("Global\\HWiNFO_SENS_SM2", MemoryMappedFileRights.Read);
                accessor = mmf.CreateViewAccessor(0L, (long)Marshal.SizeOf(typeof(HWiNFO.HWiNFO_MEMORY)), MemoryMappedFileAccess.Read);
                accessor.Read(0L, out HWiNFOMemory);
                numReadingElements = HWiNFOMemory.dwNumReadingElements;
                offsetReadingSection = HWiNFOMemory.dwOffsetOfReadingSection;
                sizeReadingSection = HWiNFOMemory.dwSizeOfReadingElement;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occured while opening the HWiNFO shared memory! - " + ex.Message);
            }
            var list = new List<HWiNFOResult>();
            for (uint num = 0U; num < numReadingElements; num += 1U)
            {
                using MemoryMappedViewStream memoryMappedViewStream = mmf.CreateViewStream(offsetReadingSection + num * sizeReadingSection, sizeReadingSection, MemoryMappedFileAccess.Read);
                byte[] array = new byte[sizeReadingSection];
                memoryMappedViewStream.Read(array, 0, (int)sizeReadingSection);
                GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                HWiNFOResult Result = (HWiNFOResult)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(HWiNFOResult));
                Result.Index = num;
                list.Add(Result);
                gchandle.Free();
            }
            AllSensors = list;
        }

        public static void Close()
        {
            if (mmf != null)
            {
                mmf.Dispose();
            }
        }

        public static IEnumerable<HWiNFOResult> AllSensors;

        private static MemoryMappedFile mmf;

        private static MemoryMappedViewAccessor accessor;

        private static uint numReadingElements;

        private static uint offsetReadingSection;

        private static uint sizeReadingSection;

        private static HWiNFO_MEMORY HWiNFOMemory;

        public enum SensorType
        {
            SENSOR_TYPE_NONE = 0,
            SENSOR_TYPE_TEMP = 1,
            SENSOR_TYPE_VOLT = 2,
            SENSOR_TYPE_FAN = 3,
            SENSOR_TYPE_CURRENT = 4,
            SENSOR_TYPE_POWER = 5,
            SENSOR_TYPE_CLOCK = 6,
            SENSOR_TYPE_USAGE = 7,
            SENSOR_TYPE_OTHER = 8
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct HWiNFOResult
        {
            public SensorType SensorType;

            private uint SensorIndex;

            private uint dwReadingID;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            public string Model;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
            private string Model_LocalLanguange;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
            private string szUnit;

            public double Value;

            public double ValueMin;

            public double ValueMax;

            public double ValueAvg;

            internal uint Index { get; set; }

            public void ReInit()
            {

                using MemoryMappedViewStream memoryMappedViewStream = mmf.CreateViewStream(offsetReadingSection + Index * sizeReadingSection, sizeReadingSection, MemoryMappedFileAccess.Read);
                byte[] array = new byte[sizeReadingSection];
                memoryMappedViewStream.Read(array, 0, (int)sizeReadingSection);
                GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
                HWiNFOResult Result = (HWiNFOResult)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(HWiNFOResult));
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
