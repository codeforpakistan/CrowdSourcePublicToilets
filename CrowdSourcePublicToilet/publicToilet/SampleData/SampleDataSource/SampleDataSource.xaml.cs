﻿//      *********    DO NOT MODIFY THIS FILE     *********
//      This file is regenerated by a design tool. Making
//      changes to this file can cause errors.
namespace Expression.Blend.SampleData.SampleDataSource
{
	using System; 
	using System.ComponentModel;

// To significantly reduce the sample data footprint in your production application, you can set
// the DISABLE_SAMPLE_DATA conditional compilation constant and disable sample data at runtime.
#if DISABLE_SAMPLE_DATA
	internal class SampleDataSource { }
#else

	public class SampleDataSource : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		public SampleDataSource()
		{
			try
			{
				Uri resourceUri = new Uri("/publicToilet;component/SampleData/SampleDataSource/SampleDataSource.xaml", UriKind.RelativeOrAbsolute);
				System.Windows.Application.LoadComponent(this, resourceUri);
			}
			catch
			{
			}
		}

		private ItemCollection _Collection = new ItemCollection();

		public ItemCollection Collection
		{
			get
			{
				return this._Collection;
			}
		}
	}

	public class Item : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		private string _Town = string.Empty;

		public string Town
		{
			get
			{
				return this._Town;
			}

			set
			{
				if (this._Town != value)
				{
					this._Town = value;
					this.OnPropertyChanged("Town");
				}
			}
		}

		private string _Near_Famous = string.Empty;

		public string Near_Famous
		{
			get
			{
				return this._Near_Famous;
			}

			set
			{
				if (this._Near_Famous != value)
				{
					this._Near_Famous = value;
					this.OnPropertyChanged("Near_Famous");
				}
			}
		}

		private string _Distance = string.Empty;

		public string Distance
		{
			get
			{
				return this._Distance;
			}

			set
			{
				if (this._Distance != value)
				{
					this._Distance = value;
					this.OnPropertyChanged("Distance");
				}
			}
		}

		private System.Windows.Media.ImageSource _ToiletImage = null;

		public System.Windows.Media.ImageSource ToiletImage
		{
			get
			{
				return this._ToiletImage;
			}

			set
			{
				if (this._ToiletImage != value)
				{
					this._ToiletImage = value;
					this.OnPropertyChanged("ToiletImage");
				}
			}
		}
	}

	public class ItemCollection : System.Collections.ObjectModel.ObservableCollection<Item>
	{ 
	}
#endif
}
