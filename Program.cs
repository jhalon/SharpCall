//
// Author: Jack Halon (@jack_halon)
// Project: SharpCall (https://github.com/jhalon/SharpCall)
// License: BSD 3-Clause
//

using System;
using System.Runtime.InteropServices;

using static SharpCall.Native;
using static SharpCall.Syscalls;

namespace SharpCall
{
    class Program
    {
        static void Main(string[] args)
        {
            Microsoft.Win32.SafeHandles.SafeFileHandle fileHandle;
            UNICODE_STRING filename = new UNICODE_STRING();
            RtlInitUnicodeString(ref filename, @"\??\C:\Users\User\Desktop\test.txt");
            IntPtr objectName = Marshal.AllocHGlobal(Marshal.SizeOf(filename));
            Marshal.StructureToPtr(filename, objectName, true);

            OBJECT_ATTRIBUTES FileObjectAttributes = new OBJECT_ATTRIBUTES
            {
                Length = (int)Marshal.SizeOf(typeof(OBJECT_ATTRIBUTES)),
                RootDirectory = IntPtr.Zero,
                ObjectName = objectName,
                Attributes = 0x00000040, // OBJ_CASE_INSENSITIVE
                SecurityDescriptor = IntPtr.Zero,
                SecurityQualityOfService = IntPtr.Zero
            };

            IO_STATUS_BLOCK IoStatusBlock = new IO_STATUS_BLOCK();
            long allocationSize = 0;

            var status = NTCreateFile(
                out fileHandle,
                FileAccess.FILE_GENERIC_WRITE,
                ref FileObjectAttributes,
                ref IoStatusBlock,
                ref allocationSize,
                FileAttributes.Normal,
                FileShare.Write,
                CreationDisposition.FILE_OVERWRITE_IF,
                CreateOption.FILE_SYNCHRONOUS_IO_NONALERT,
                IntPtr.Zero,
                0);
        }
    }
}
