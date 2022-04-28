using System;
using System.Runtime.InteropServices;

namespace TXQ.Utils.Model
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Time
    {
        public short year;
        public short month;
        public short dayOfWeek;
        public short day;
        public short hour;
        public short minute;
        public short second;
        public short milliseconds;

        public override bool Equals(object obj)
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(Time left, Time right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Time left, Time right)
        {
            return !(left == right);
        }
    }
}


