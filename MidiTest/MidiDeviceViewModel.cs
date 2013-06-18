using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiTest
{
	public class MidiDeviceViewModel:ViewModelBase,IDisposable
	{
		private IntPtr handle;

		public string Name{get;private set;}
		public uint DeviceID{get;private set;}

		public bool IsDeviceOpened
		{
			get
			{
				return handle!=IntPtr.Zero;
			}
		}

		private MidiDeviceViewModel(string name,uint deviceID)
		{
			handle=IntPtr.Zero;
			Name=name;
			DeviceID=deviceID;
		}

		~MidiDeviceViewModel()
		{
			Dispose();
		}

		public static IEnumerable<MidiDeviceViewModel> Create()
		{
			foreach(uint deviceID in Enumerable.Range(-1,(int)Interop.midiOutGetNumDevs()+1)){
				var midiOutCaps=new MIDIOUTCAPS();
				Interop.midiOutGetDevCaps(deviceID,ref midiOutCaps,84u);
				yield return new MidiDeviceViewModel(midiOutCaps.szPname,deviceID);
			}
		}

		public override string ToString()
		{
			return Name;
		}

		public bool Open()
		{
			if(handle==IntPtr.Zero)
				return Interop.midiOutOpen(ref handle,DeviceID,null,0,(uint)CALLBACK.NULL)==(uint)MMSYSERR.NOERROR;
			else throw new InvalidOperationException("デバイスを閉じていない状態でOpenメソッドを呼び出すことはできません。");
		}

		public void Close()
		{
			if(handle==IntPtr.Zero) throw new InvalidOperationException("デバイスを開いていない状態でCloseメソッドを呼び出すことはできません。");
			else{
				Interop.midiOutClose(handle);
				handle=IntPtr.Zero;
			}
		}

		public bool SendMessage(byte channel,MessageType messageType,byte dataByte1,byte dataByte2)
		{
			var message=new byte[]{(byte)(((byte)messageType)<<4|channel),dataByte1,dataByte2,0};
			return Interop.midiOutShortMsg(handle,BitConverter.ToUInt32(message,0))==(uint)MMSYSERR.NOERROR;
		}

		public void Dispose()
		{
			if(IsDeviceOpened) Close();
		}
	}
}
