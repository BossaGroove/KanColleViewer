using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Grabacr07.KanColleWrapper.Models;

namespace Grabacr07.KanColleViewer.Views.Controls
{
    public class SlotItemTooltip : Control
    {
        static SlotItemTooltip()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SlotItemTooltip), new FrameworkPropertyMetadata(typeof(SlotItemTooltip)));
        }

        #region EqName

        public string EqName
        {
            get { return (string)this.GetValue(EqNameProperty); }
            set { this.SetValue(EqNameProperty, value); }
        }
        public static readonly DependencyProperty EqNameProperty =
            DependencyProperty.Register("EqName", typeof(string), typeof(SlotItemTooltip), new UIPropertyMetadata(""));

        #endregion

        #region UntranslatedName

        public string UntranslatedName
        {
            get { return (string)this.GetValue(UntranslatedNameProperty); }
            set { this.SetValue(UntranslatedNameProperty, value); }
        }
        public static readonly DependencyProperty UntranslatedNameProperty =
            DependencyProperty.Register("UntranslatedName", typeof(string), typeof(SlotItemTooltip), new UIPropertyMetadata(""));

        #endregion

        #region LevelText

        public string LevelText
        {
            get { return (string)this.GetValue(LevelTextProperty); }
            set { this.SetValue(LevelTextProperty, value); }
        }
        public static readonly DependencyProperty LevelTextProperty =
            DependencyProperty.Register("LevelText", typeof(string), typeof(SlotItemTooltip), new UIPropertyMetadata(""));

        #endregion

        #region AdeptText

        public string AdeptText
        {
            get { return (string)this.GetValue(AdeptTextProperty); }
            set { this.SetValue(AdeptTextProperty, value); }
        }
        public static readonly DependencyProperty AdeptTextProperty =
            DependencyProperty.Register("AdeptText", typeof(string), typeof(SlotItemTooltip), new UIPropertyMetadata(""));

        #endregion

        #region IsNumerable

        public bool IsNumerable
        {
            get { return (bool)this.GetValue(IsNumerableProperty); }
            set { this.SetValue(IsNumerableProperty, value); }
        }
        public static readonly DependencyProperty IsNumerableProperty =
            DependencyProperty.Register("IsNumerable", typeof(bool), typeof(SlotItemTooltip), new UIPropertyMetadata(false));

        #endregion

        #region ShowHeader

        public bool ShowHeader
        {
            get { return (bool)this.GetValue(ShowHeaderProperty); }
            set { this.SetValue(ShowHeaderProperty, value); }
        }
        public static readonly DependencyProperty ShowHeaderProperty =
            DependencyProperty.Register("ShowHeader", typeof(bool), typeof(SlotItemTooltip), new UIPropertyMetadata(false));

        #endregion

        #region Current

        public int Current
        {
            get { return (int)this.GetValue(CurrentProperty); }
            set { this.SetValue(CurrentProperty, value); }
        }
        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(int), typeof(SlotItemTooltip), new UIPropertyMetadata(0));

        #endregion

        #region Maximum

        public int Maximum
        {
            get { return (int)this.GetValue(MaximumProperty); }
            set { this.SetValue(MaximumProperty, value); }
        }
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register("Maximum", typeof(int), typeof(SlotItemTooltip), new UIPropertyMetadata(0));

        #endregion

        #region AllStats

        public string AllStats
        {
            get { return (string)this.GetValue(AllStatsProperty); }
            set { this.SetValue(AllStatsProperty, value); }
        }
        public static readonly DependencyProperty AllStatsProperty =
            DependencyProperty.Register("AllStats", typeof(string), typeof(SlotItemTooltip), new UIPropertyMetadata(""));

        #endregion

    }
}
