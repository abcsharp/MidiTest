using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MidiTest
{
	[StructLayout(LayoutKind.Sequential,CharSet=CharSet.Unicode)]
	public struct MIDIOUTCAPS
	{
		public ushort wMid;
		public ushort wPid;
		public uint vDriverVersion;
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=32)]
		public string szPname;
		public ushort wTechnology;
		public ushort wVoices;
		public ushort wNotes;
		public ushort wChannelMask;
		public uint dwSupport;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct MIDIHDR
	{
		public IntPtr lpData;
		public uint dwBufferLength;
		public uint dwBytesRecorded;
		public IntPtr dwUser;
		public uint dwFlags;
		public IntPtr lpNext;
		public IntPtr reserved;
		public uint dwOffset;
		[MarshalAs(UnmanagedType.ByValArray,SizeConst=4)]
		public IntPtr[] dwReserved;
	}
	
	public static class Interop
	{	
		[DllImport("winmm.dll",EntryPoint="midiOutGetNumDevs")]
		public static extern uint midiOutGetNumDevs();
		[DllImport("winmm.dll",EntryPoint="midiOutGetDevCapsW",CharSet=CharSet.Unicode)]
		public static extern uint midiOutGetDevCaps(uint uDeviceID,ref MIDIOUTCAPS lpMidiOutCaps,uint cbMidiOutCaps);
		[DllImport("winmm.dll",EntryPoint="midiOutOpen")]
		public static extern uint midiOutOpen(ref IntPtr lphmo,uint uDeviceID,Delegate dwCallback,uint dwCallbackInstance,uint dwFlags);
		[DllImport("winmm.dll",EntryPoint="midiOutClose")]
		public static extern uint midiOutClose(IntPtr hmo);
		[DllImport("winmm.dll",EntryPoint="midiOutShortMsg")]
		public static extern uint midiOutShortMsg(IntPtr hmo,uint dwMsg);
		[DllImport("winmm.dll",EntryPoint="midiOutLongMsg")]
		public static extern uint midiOutLongMsg(IntPtr hmo,ref MIDIHDR lpMidiOutHdr,uint cbMidiOutHdr);
		[DllImport("winmm.dll",EntryPoint="midiOutPrepareHeader")]  
		public static extern uint midiOutPrepareHeader(IntPtr hmo,ref MIDIHDR lpMidiOutHdr,uint cbMidiOutHdr);
		[DllImport("winmm.dll",EntryPoint="midiOutUnprepareHeader")]  
		public static extern uint midiOutUnprepareHeader(IntPtr hmo,ref MIDIHDR lpMidiOutHdr,uint cbMidiOutHdr);
	}

	public enum CALLBACK:uint
	{
		TYPEMASK=0x00070000,
		NULL=0x00000000,
		WINDOW=0x00010000,
		TASK=0x00020000,
		FUNCTION=0x00030000,
		THREAD=TASK,
		EVENT=0x00050000
	}

	public enum MMSYSERR:uint
	{
		NOERROR=0,                  
		ERROR=1,
		BADDEVICEID=2,
		NOTENABLED=3,
		ALLOCATED=4,
		INVALHANDLE=5,
		NODRIVER=6,
		NOMEM=7,
		NOTSUPPORTED=8,
		BADERRNUM=9,
		INVALFLAG=10,
		INVALPARAM=11,
		HANDLEBUSY=12,
		INVALIDALIAS=13,
		BADDB=14,
		KEYNOTFOUND=15,
		READERROR=16,
		WRITEERROR=17,
		DELETEERROR=18,
		VALNOTFOUND=19,
		NODRIVERCB=20,
		MOREDATA=21
	}

	public enum MIDIERR:uint
	{
		UNPREPARED=64,
		STILLPLAYING=64+1,
		NOMAP=64+2,
		NOTREADY=64+3,
		NODEVICE=64+4,
		INVALIDSETUP=64+5,
		BADOPENMODE=64+6,
		DONT_CONTINUE=64+7
	}
}