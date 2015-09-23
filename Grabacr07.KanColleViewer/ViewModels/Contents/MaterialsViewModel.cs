using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleViewer.Models;
using Grabacr07.KanColleWrapper;
using Livet;

namespace Grabacr07.KanColleViewer.ViewModels.Contents
{
	public class MaterialsViewModel : ViewModel
	{
        public Materials Model { get; private set; }

        public ICollection<MaterialViewModel> Values { get; private set; }

		#region SelectedItem1 変更通知プロパティ

		private MaterialViewModel _SelectedItem1;

		public MaterialViewModel SelectedItem1
		{
			get { return this._SelectedItem1; }
			set
			{
                if (this._SelectedItem1 != value)
				{
					this._SelectedItem1 = value;
					this.RaisePropertyChanged();
                    if (value != null)
                    {
                        KanColleViewer.Models.Settings.Current.DisplayMaterial1 = value.Key;
                    }
                    
				}
			}
		}

		#endregion

		#region SelectedItem2 変更通知プロパティ

		private MaterialViewModel _SelectedItem2;

		public MaterialViewModel SelectedItem2
		{
			get { return this._SelectedItem2; }
			set
			{
				if (this._SelectedItem2 != value)
				{
					this._SelectedItem2 = value;
					this.RaisePropertyChanged();
                    if (value != null)
                    {
                        KanColleViewer.Models.Settings.Current.DisplayMaterial2 = value.Key;
                    }
				}
			}
		}

		#endregion


        #region SelectedItem3 変更通知プロパティ

        private MaterialViewModel _SelectedItem3;

        public MaterialViewModel SelectedItem3
        {
            get { return this._SelectedItem3; }
            set
            {
                if (this._SelectedItem3 != value)
                {
                    this._SelectedItem3 = value;
                    this.RaisePropertyChanged();
                    if (value != null)
                    {
                        KanColleViewer.Models.Settings.Current.DisplayMaterial3 = value.Key;
                    }
                }
            }
        }

        #endregion



        #region SelectedItem4 変更通知プロパティ

        private MaterialViewModel _SelectedItem4;

        public MaterialViewModel SelectedItem4
        {
            get { return this._SelectedItem4; }
            set
            {
                if (this._SelectedItem4 != value)
                {
                    this._SelectedItem4 = value;
                    this.RaisePropertyChanged();
                    if (value != null)
                    {
                        KanColleViewer.Models.Settings.Current.DisplayMaterial4 = value.Key;
                    }
                }
            }
        }

        #endregion



        #region SelectedItem4 変更通知プロパティ

        private MaterialViewModel _SelectedItem5;

        public MaterialViewModel SelectedItem5
        {
            get { return this._SelectedItem5; }
            set
            {
                if (this._SelectedItem5 != value)
                {
                    this._SelectedItem5 = value;
                    this.RaisePropertyChanged();
                    if (value != null)
                    {
                        KanColleViewer.Models.Settings.Current.DisplayMaterial5 = value.Key;
                    }
                }
            }
        }

        #endregion



        #region SelectedItem4 変更通知プロパティ

        private MaterialViewModel _SelectedItem6;

        public MaterialViewModel SelectedItem6
        {
            get { return this._SelectedItem6; }
            set
            {
                if (this._SelectedItem6 != value)
                {
                    this._SelectedItem6 = value;
                    this.RaisePropertyChanged();
                    if (value != null)
                    {
                        KanColleViewer.Models.Settings.Current.DisplayMaterial6 = value.Key;
                    }
                }
            }
        }

        #endregion


		public MaterialsViewModel()
		{
			this.Model = KanColleClient.Current.Homeport.Materials;
            
			var fuel = new MaterialViewModel("fuel", "燃料");
			var ammunition = new MaterialViewModel("ammunition", "弾薬");
            var steel = new MaterialViewModel("steel", "鋼鉄");
			var bauxite = new MaterialViewModel("bauxite", "ボーキサイト");
            var develop = new MaterialViewModel("develop", "開発資材");
            var repair = new MaterialViewModel("repair", "高速修復材");
            var build = new MaterialViewModel("build", "高速建造材");
            var improvement = new MaterialViewModel("improvement", "改修資材");
			
            this.Values = new List<MaterialViewModel>
			{
				fuel,
				ammunition,
				steel,
				bauxite,
				develop,
				repair,
				build,
				improvement,
			};

            this._SelectedItem1 = this.Values.FirstOrDefault(x => x.Key == KanColleViewer.Models.Settings.Current.DisplayMaterial1) ?? fuel;
            this._SelectedItem2 = this.Values.FirstOrDefault(x => x.Key == KanColleViewer.Models.Settings.Current.DisplayMaterial2) ?? ammunition;
            this._SelectedItem3 = this.Values.FirstOrDefault(x => x.Key == KanColleViewer.Models.Settings.Current.DisplayMaterial3) ?? steel;
            this._SelectedItem4 = this.Values.FirstOrDefault(x => x.Key == KanColleViewer.Models.Settings.Current.DisplayMaterial4) ?? bauxite;
            this._SelectedItem5 = this.Values.FirstOrDefault(x => x.Key == KanColleViewer.Models.Settings.Current.DisplayMaterial5) ?? develop;
            this._SelectedItem6 = this.Values.FirstOrDefault(x => x.Key == KanColleViewer.Models.Settings.Current.DisplayMaterial6) ?? repair;

            this.Model.PropertyChanged += (sender, args) =>
            {
                //args.PropertyName <--- Fuel, Ammunition, etc
                fuel.Value = this.Model.Fuel;
                ammunition.Value = this.Model.Ammunition;
                steel.Value = this.Model.Steel;
                bauxite.Value = this.Model.Bauxite;
                develop.Value = this.Model.DevelopmentMaterials;
                repair.Value = this.Model.InstantRepairMaterials;
                build.Value = this.Model.InstantBuildMaterials;
                improvement.Value = this.Model.ImprovementMaterials;
            };
		}

		public class MaterialViewModel : ViewModel
		{
            public string Key;
            private string _Display;

            public string Display
            {
                get { return this._Display; }
                set
                {
                    if (this._Display != value)
                    {
                        this._Display = value;
                        this.RaisePropertyChanged();
                    }
                }
            }

			#region Value 変更通知プロパティ

			private int _Value;

			public int Value
			{
				get { return this._Value; }
				set
				{
					if (this._Value != value)
					{
						this._Value = value;
						this.RaisePropertyChanged();
					}
				}
			}

			#endregion

			public MaterialViewModel(string key, string display)
			{
				this.Key = key;
				this.Display = display;
			}
		}
	}
}
