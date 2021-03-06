﻿using System;
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
            
			var fuel = new MaterialViewModel("fuel", KanColleViewer.Properties.Resources.Homeport_Fuel_Short);
            var ammunition = new MaterialViewModel("ammunition", KanColleViewer.Properties.Resources.Homeport_Ammunition_Short);
            var steel = new MaterialViewModel("steel", KanColleViewer.Properties.Resources.Homeport_Steel_Short);
            var bauxite = new MaterialViewModel("bauxite", KanColleViewer.Properties.Resources.Homeport_Bauxite_Short);
            var develop = new MaterialViewModel("develop", KanColleViewer.Properties.Resources.Homeport_DevelopmentMaterials_Short);
            var repair = new MaterialViewModel("repair", KanColleViewer.Properties.Resources.Homeport_InstantRepair_Short);
            var build = new MaterialViewModel("build", KanColleViewer.Properties.Resources.Homeport_InstantBuild_Short);
            var improvement = new MaterialViewModel("improvement", KanColleViewer.Properties.Resources.Homeport_ImprovementMaterial_Short);
            var blank = new MaterialViewModel("blank", "");

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
                blank
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
                fuel.Value = this.Model.Fuel.ToString();
                ammunition.Value = this.Model.Ammunition.ToString();
                steel.Value = this.Model.Steel.ToString();
                bauxite.Value = this.Model.Bauxite.ToString();
                develop.Value = this.Model.DevelopmentMaterials.ToString();
                repair.Value = this.Model.InstantRepairMaterials.ToString();
                build.Value = this.Model.InstantBuildMaterials.ToString();
                improvement.Value = this.Model.ImprovementMaterials.ToString();
            };

            KanColleClient.Current.Translations.PropertyChanged += (sender, args) =>
            {
                fuel.Display = KanColleViewer.Properties.Resources.Homeport_Fuel_Short;
                ammunition.Display = KanColleViewer.Properties.Resources.Homeport_Ammunition_Short;
                steel.Display = KanColleViewer.Properties.Resources.Homeport_Steel_Short;
                bauxite.Display = KanColleViewer.Properties.Resources.Homeport_Bauxite_Short;
                develop.Display = KanColleViewer.Properties.Resources.Homeport_DevelopmentMaterials_Short;
                repair.Display = KanColleViewer.Properties.Resources.Homeport_InstantRepair_Short;
                build.Display = KanColleViewer.Properties.Resources.Homeport_InstantBuild_Short;
                improvement.Display = KanColleViewer.Properties.Resources.Homeport_ImprovementMaterial_Short;
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

            private string _Value;

            public string Value
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
                this.Value = "";
			}
		}
	}
}
