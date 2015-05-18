using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models;
using Livet;
using Livet.EventListeners;

namespace Grabacr07.KanColleViewer.ViewModels.Catalogs
{
	public class ShipViewModel : ViewModel
	{
		public int Index { get; private set; }
		public Ship Ship { get; private set; }
        public List<SlotItemViewModel> SlotItems { get; private set; }

		#region Area 変更通知プロパティ

		private ISallyArea _Area;
		public ISallyArea Area
		{
			get { return this._Area; }
			set
			{
				if (this._Area != value)
				{
					this._Area = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion
		

		public ShipViewModel(int index, Ship ship)
		{
			this.Index = index;
			this.Ship = ship;
            this.SlotItems = ship.EquippedSlots.Select(i => new SlotItemViewModel(i)).ToList();

			this.Update(ship);
			this.CompositeDisposable.Add(new PropertyChangedEventListener(ship)
			{
				"SallyArea", (sender, args) => this.Update(ship),
			});
			this.CompositeDisposable.Add(new PropertyChangedEventListener(ResourceServiceWrapper.Current)
			{
				(sender, args) => this.RaisePropertyChanged("Area"),
			});
		}

		private void Update(Ship ship)
		{
			this.Area = SallyArea.Get(ship);
		}
	}
}
