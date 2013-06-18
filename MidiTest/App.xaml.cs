using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace MidiTest
{
	/// <summary>
	/// App.xaml の相互作用ロジック
	/// </summary>
	public partial class App : Application
	{
	}

	public class ViewModelBase:INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public void NotifyPropertyChanged(string propertyName)
		{
			if(PropertyChanged!=null) PropertyChanged(this,new PropertyChangedEventArgs(propertyName));
		}
	}
}
