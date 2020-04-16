//
// Author: Jack Halon (@jack_halon)
// Project: SharpCall (https://github.com/jhalon/SharpCall)
// License: BSD 3-Clause
//

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

using static SharpCall.Native;

namespace SharpCall
{
    class Syscalls
    {

        static byte[] bNtCreateFile =
        {
            0x4C, 0x8B, 0xD1,               // mov r10, rcx
            0xB8, 0x55, 0x00, 0x00, 0x00,   // mov eax, 0x55 (NtCreateFile Syscall)
            0x0F, 0x05,                     // syscall
            0xC3                            // ret
        };

        public static NTSTATUS NTCreateFile(
            out Microsoft.Win32.SafeHandles.SafeFileHandle FileHandle,
            FileAccess DesiredAcces,
            ref OBJECT_ATTRIBUTES ObjectAttributes,
            ref IO_STATUS_BLOCK IoStatusBlock,
            ref long AllocationSize,
            FileAttributes FileAttributes,
            FileShare ShareAccess,
            CreationDisposition CreateDisposition,
            CreateOption CreateOptions,
            IntPtr EaBuffer,
            uint EaLength)
        {
            byte[] syscall = bNtCreateFile;

            unsafe
            {
                fixed (byte* ptr = syscall)
                {
                    IntPtr memoryAddress = (IntPtr)ptr;

                    if (!VirtualProtect(memoryAddress, (UIntPtr)syscall.Length, (uint)AllocationProtect.PAGE_EXECUTE_READWRITE, out uint lpflOldProtect))
                    {
                        throw new Win32Exception();
                    }

                    Delegates.NtCreateFile assembledFunction = (Delegates.NtCreateFile)Marshal.GetDelegateForFunctionPointer(memoryAddress, typeof(Delegates.NtCreateFile));

                    return (NTSTATUS)assembledFunction(out FileHandle,
                        DesiredAcces,
                        ref ObjectAttributes,
                        ref IoStatusBlock,
                        ref AllocationSize,
                        FileAttributes,
                        ShareAccess,
                        CreateDisposition,
                        CreateOptions,
                        EaBuffer,
                        EaLength);
                }
            }
        }

        public struct Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.StdCall)]
            public delegate NTSTATUS NtCreateFile(
                out Microsoft.Win32.SafeHandles.SafeFileHandle FileHandle,
                FileAccess DesiredAcces,
                ref OBJECT_ATTRIBUTES ObjectAttributes,
                ref IO_STATUS_BLOCK IoStatusBlock,
                ref long AllocationSize,
                FileAttributes FileAttributes,
                FileShare ShareAccess,
                CreationDisposition CreateDisposition,
                CreateOption CreateOptions,
                IntPtr EaBuffer,
                uint EaLength
                );
        }
    }
}








