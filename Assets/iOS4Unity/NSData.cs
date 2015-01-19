﻿using System;
using System.Runtime.InteropServices;

namespace iOS4Unity
{
    public class NSData : NSObject
    {
        private static readonly IntPtr _classHandle;

        static NSData()
        {
            _classHandle = ObjC.GetClass("NSData");
        }

        public override IntPtr ClassHandle 
        {
            get { return _classHandle; }
        }

        internal NSData(IntPtr handle) : base(handle) { }

        public unsafe static NSData FromArray(byte[] buffer)
        {
            if (buffer.Length == 0)
            {
                return NSData.FromBytes(IntPtr.Zero, 0);
            }
            fixed (void* ptr = &buffer[0])
            {
                return NSData.FromBytes((IntPtr)ptr, (uint)buffer.Length);
            }
        }

        public static NSData FromBytes(IntPtr bytes, uint size)
        {
            return new NSData(ObjC.MessageSendIntPtr(_classHandle, "dataWithBytes:length:", bytes, size));
        }

        public static NSData FromData(NSData source)
        {
            return new NSData(ObjC.MessageSendIntPtr(_classHandle, "dataWithData:", source.Handle));
        }

        public static NSData FromBytesNoCopy(IntPtr bytes, uint size)
        {
            return new NSData(ObjC.MessageSendIntPtr(_classHandle, "dataWithBytesNoCopy:length:", bytes, size));
        }

        public static NSData FromBytesNoCopy(IntPtr bytes, uint size, bool freeWhenDone)
        {
            return new NSData(ObjC.MessageSendIntPtr(_classHandle, "dataWithBytesNoCopy:length:freeWhenDone:", bytes, size, freeWhenDone));
        }

        public static NSData FromFile(string path, NSDataReadingOptions mask, out NSError error)
        {
            IntPtr errorHandle;
            var data = new NSData(ObjC.MessageSendIntPtr(_classHandle, "dataWithContentsOfFile:options:error:", path, (uint)mask, out errorHandle));
            error = errorHandle == IntPtr.Zero ? null : new NSError(errorHandle);
            return data;
        }

        public static NSData FromFile(string path)
        {
            return new NSData(ObjC.MessageSendIntPtr(_classHandle, "dataWithContentsOfFile:", path));
        }

        public static NSData FromUrl(string url)
        {
            return new NSData(ObjC.MessageSendIntPtr_NSUrl(_classHandle, "dataWithContentsOfURL:", url));
        }

        public static NSData FromUrl(string url, NSDataReadingOptions mask, out NSError error)
        {
            IntPtr errorHandle;
            var data = new NSData(ObjC.MessageSendIntPtr_NSUrl(_classHandle, "dataWithContentsOfURL:options:error:", url, (uint)mask, out errorHandle));
            error = errorHandle == IntPtr.Zero ? null : new NSError(errorHandle);
            return data;
        }

        public IntPtr Bytes
        {
            get { return ObjC.MessageSendIntPtr(Handle, "bytes"); }
        }

        public uint Length
        {
            get { return ObjC.MessageSendUInt(Handle, "length"); }
            set { throw new NotImplementedException("Not available on NSData, only available on NSMutableData"); }
        }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || (long)index > (long)((ulong)this.Length))
                {
                    throw new ArgumentException("idx");
                }
                return Marshal.ReadByte(new IntPtr(Bytes.ToInt64() + (long)index));
            }
            set
            {
                throw new NotImplementedException("NSData arrays can not be modified, use an NSMutableData instead");
            }
        }
    }

    [Flags]
    public enum NSDataReadingOptions : uint
    {
        Mapped = 1,
        Uncached = 2,
        Coordinated = 4,
        MappedAlways = 8
    }
}