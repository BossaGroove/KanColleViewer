using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Livet;

namespace Grabacr07.KanColleWrapper.Models
{
	public class ShipSlot : NotificationObject
	{
		public SlotItem Item { get; private set; }

		public int Maximum { get; private set; }

		public bool Equipped
		{
			get { return this.Item != null && this.Item.RawData.api_slotitem_id != -1; }            
		}

		#region Current �ύX�ʒm�v���p�e�B

		private int _Current;

		public int Current
		{
			get { return this._Current; }
			set
			{
				if (this._Current != value)
				{
					this._Current = value;
					this.RaisePropertyChanged();
				}
			}
		}

		#endregion

		public ShipSlot(SlotItem item, int maximum, int current)
		{
            if (item == null) {
                this.Item = SlotItem.Dummy;
            }
            else
            {
                this.Item = item;
            }            
			this.Maximum = maximum;
			this.Current = current;
		}
	}
}