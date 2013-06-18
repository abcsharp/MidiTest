using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiTest
{
	public class MainWindowViewModel:ViewModelBase
	{
		private byte dataByte1=0,dataByte2=0,selectedChannel=0;
		private MidiDeviceViewModel selectedDevice=null;
		private MessageTypeViewModel selectedMessageType=null;
		private bool started=false;

		public IEnumerable<MidiDeviceViewModel> Devices{get;private set;}
		public IEnumerable<ChannelViewModel> Channels{get;private set;}
		public IEnumerable<MessageTypeViewModel> MessageTypes{get;private set;}
		public Command OpenCommand{get;private set;}
		public Command SendMessageCommand{get;private set;}

		public bool Started
		{
			get
			{
				return started;
			}
			set
			{
				started=value;
				NotifyPropertyChanged("Started");
			}
		}

		public MidiDeviceViewModel SelectedDevice
		{
			get
			{
				return selectedDevice;
			}
			set
			{
				selectedDevice=value;
				NotifyPropertyChanged("SelectedDevice");
			}
		}

		public byte SelectedChannel
		{
			get
			{
				return selectedChannel;
			}
			set
			{
				selectedChannel=value;
			}
		}

		public MessageTypeViewModel SelectedMessageType
		{
			get
			{
				return selectedMessageType;
			}
			set
			{
				selectedMessageType=value;
				NotifyPropertyChanged("SelectedMessageType");
			}
		}

		public byte DataByte1
		{
			get
			{
				return dataByte1;
			}
			set
			{
				dataByte1=value;
				NotifyPropertyChanged("DataByte1");
			}
		}

		public byte DataByte2
		{
			get
			{
				return dataByte2;
			}
			set
			{
				dataByte2=value;
				NotifyPropertyChanged("DataByte2");
			}
		}

		public MainWindowViewModel()
		{
			Devices=MidiDeviceViewModel.Create().ToArray();
			SelectedDevice=Devices.FirstOrDefault();
			Channels=ChannelViewModel.Create().ToArray();
			MessageTypes=MessageTypeViewModel.Create().ToArray();
			SelectedMessageType=MessageTypes.FirstOrDefault();
			OpenCommand=new Command(MidiOutOpen,CanMidiOutOpen);
			SendMessageCommand=new Command(SendMessage,CanSendMessage);
		}

		private void MidiOutOpen()
		{
			if(SelectedDevice.IsDeviceOpened) SelectedDevice.Close();
			else Started=SelectedDevice.Open();
			Started=SelectedDevice.IsDeviceOpened;
		}

		private bool CanMidiOutOpen()
		{
			return Devices.Count()>0;
		}

		private void SendMessage()
		{
			SelectedDevice.SendMessage(SelectedChannel,SelectedMessageType.MessageType,DataByte1,DataByte2);
		}

		private bool CanSendMessage()
		{
			return Started;
		}
	}
}
