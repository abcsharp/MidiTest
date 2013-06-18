using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MidiTest
{
	[ValueConversion(typeof(bool),typeof(bool))]
	public class Inverter:IValueConverter
	{
		public object Convert(object value,Type targetType,object parameter,CultureInfo culture)
		{
			if(targetType!=typeof(bool)) throw new ArgumentException("変換先の型が違います。");
			return !((bool)value);
		}

		public object ConvertBack(object value,Type targetType,object parameter,CultureInfo culture)
		{
			if(targetType!=typeof(bool)) throw new ArgumentException("変換先の型が違います。");
			return !((bool)value);
		}
	}

	[ValueConversion(typeof(bool),typeof(string))]
	public class StateToStringConverter:IValueConverter
	{
		public object Convert(object value,Type targetType,object parameter,CultureInfo culture)
		{
			return (bool)value?"Close":"Open";
		}

		public object ConvertBack(object value,Type targetType,object parameter,CultureInfo culture)
		{
			if(targetType!=typeof(bool)) throw new ArgumentException("変換先の型が違います。");
			var str=(value as string).ToLower();
			return str!=null?str.StartsWith("close")?true:false:false;
		}
	}
}
