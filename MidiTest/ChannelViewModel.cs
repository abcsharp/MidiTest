using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiTest
{
	public class ChannelViewModel
	{
		public string Name{get;private set;}
		public byte Channel{get;private set;}

		private ChannelViewModel(string name,byte channel)
		{
			Name=name;
			Channel=channel;
		}

		public static IEnumerable<ChannelViewModel> Create()
		{
			return from channel in Enumerable.Range(0,16)
				   select new ChannelViewModel((channel+1).ToString(),(byte)channel);
		}

		public override string ToString()
		{
			return Name;
		}
	}
}
