using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiTest
{
	public enum MessageType:byte
	{
		NoteOn=0x9,ControlChange=0xB,PitchBend=0xE,MidiClock=0xF
	}

	public class MessageTypeViewModel:ViewModelBase
	{
		public string Name{get;private set;}
		public MessageType MessageType{get;private set;}

		private MessageTypeViewModel(string name,MessageType messageType)
		{
			Name=name;
			MessageType=messageType;
		}

		public static IEnumerable<MessageTypeViewModel> Create()
		{
			return from value in Enum.GetValues(typeof(MessageType)).Cast<MessageType>()
				   let name=Enum.GetName(typeof(MessageType),value)
				   select new MessageTypeViewModel(name,value);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
