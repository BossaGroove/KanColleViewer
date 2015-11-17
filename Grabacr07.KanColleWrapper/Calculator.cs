﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Internal;
using Grabacr07.KanColleWrapper.Models;

namespace Grabacr07.KanColleWrapper
{

	internal static class Calculator
	{
        /// <summary>
        /// 装備と搭載数を指定して、スロット単位の制空能力を計算します。
        /// </summary>
        /// <param name="slotItem">対空能力を持つ装備。</param>
        /// <param name="onslot">搭載数。</param>
        /// <returns></returns>
        public static int CalcAirSuperiorityPotential(this SlotItem slotItem, int onslot)
        {
            if (slotItem.Info.IsAirSuperiorityFighter)
            {
                return (int)(slotItem.Info.AA * Math.Sqrt(onslot));
            }

            return 0;
        }
        /// <summary>
        /// 指定した艦の制空能力を計算します。
        /// </summary>
        public static int CalcAirSuperiorityPotential(this Ship ship)
        {
            return ship.EquippedSlots
                .Select(x => x.Item.CalcAirSuperiorityPotential(x.Current))
                .Sum();
        }



        /// <summary>
        /// 指定した艦の制空能力の最小値を計算します。
        /// </summary>
        public static int CalcMinAirSuperiorityPotential(this Ship ship)
        {
            return ship.EquippedSlots
                .Select(x => (x.Item.Info.Type == SlotItemType.艦上戦闘機
                                ? x.Item.CalcAirSuperiorityPotential(x.Current)
                                : 0)
                             + x.Item.CalcMinAirecraftAdeptBonus(x.Current))
                .Select(x => (int)x)
                .Sum();

        }

        /// <summary>
        /// 指定した艦の制空能力の最大値を計算します。
        /// </summary>
        public static int CalcMaxAirSuperiorityPotential(this Ship ship)
        {
            return ship.EquippedSlots
               .Select(x => x.Item.CalcAirSuperiorityPotential(x.Current)
                           + x.Item.CalcMaxAirecraftAdeptBonus(x.Current))
               .Select(x => (int)x)
               .Sum();

        }

        /// <summary>
        /// 熟練度による制空能力ボーナス最小値を計算します。
        /// </summary>
        /// <param name="slotItem">対空能力を持つ装備。</param>
        /// <param name="onslot">搭載数。</param>
        /// <returns></returns>
        private static double CalcMinAirecraftAdeptBonus(this SlotItem slotItem, int onslot)
        {
            if (onslot < 1) return 0;
            return slotItem.Info.Type == SlotItemType.艦上戦闘機
                ? slotItem.CalcAirecraftAdeptBonusOfType() + slotItem.CalcMinInternalAirecraftAdeptBonus()
                : 0; // 艦戦以外は簡単に吹き飛ぶので最小値としては計算に入れない
        }

      /// <summary>
		/// 熟練度による制空能力ボーナス最大値を計算します。
		/// </summary>
		/// <param name="slotItem">対空能力を持つ装備。</param>
		/// <param name="onslot">搭載数。</param>
		/// <returns></returns>
		private static double CalcMaxAirecraftAdeptBonus(this SlotItem slotItem, int onslot) {
            if (onslot < 1) return 0;
			return slotItem.CalcAirecraftAdeptBonusOfType() + slotItem.CalcMaxInternalAirecraftAdeptBonus();
        }
		
        /// <summary>
		/// 各表記熟練度に対応した機種別熟練度ボーナスを計算します。
		/// </summary>
		/// <param name="slotItem"></param>
		/// <returns></returns>
		private static int CalcAirecraftAdeptBonusOfType(this SlotItem slotItem) {
            if (slotItem.Info.Type == SlotItemType.艦上戦闘機) {
                if (slotItem.Adept == 1) {
                    return 0;
                } else if (slotItem.Adept == 2) {
                    return 2;
                } else if (slotItem.Adept == 3) {
                    return 5;
                } else if (slotItem.Adept == 4) {
                    return 9;
                } else if (slotItem.Adept == 5) {
                    return 14;
                } else if (slotItem.Adept == 6) {
                    return 14;
                } else if (slotItem.Adept == 7) {
                    return 22;
                } else {
                    return 0;
                }
            } else if (slotItem.Info.Type == SlotItemType.水上爆撃機) {
                if (slotItem.Adept == 1) {
                    return 0;
                } else if (slotItem.Adept == 2) {
                    return 1;
                } else if (slotItem.Adept == 3) {
                    return 1;
                } else if (slotItem.Adept == 4) {
                    return 1;
                } else if (slotItem.Adept == 5) {
                    return 3;
                } else if (slotItem.Adept == 6) {
                    return 3;
                } else if (slotItem.Adept == 7) {
                    return 6;
                } else {
                    return 0;
                }
            } else {
                return 0;
            }
        }

        /// <summary>
        /// 各表記熟練度に対応した艦載機内部熟練度ボーナスの最小値を計算します。
        /// </summary>
        /// <param name="slotItem"></param>
        /// <returns></returns>
        private static double CalcMinInternalAirecraftAdeptBonus(this SlotItem slotItem)
        {
            return slotItem.Info.IsAirSuperiorityFighter
                ? Math.Sqrt((slotItem.Adept != 0 ? (slotItem.Adept - 1) * 15 + 10 : 0) / 10d)
                : 0;
        }

        /// <summary>
        /// 各表記熟練度に対応した艦載機内部熟練度ボーナスの最大値を計算します。
        /// </summary>
        /// <param name="slotItem"></param>
        /// <returns></returns>
        private static double CalcMaxInternalAirecraftAdeptBonus(this SlotItem slotItem)
        {
            if (!slotItem.Info.IsAirSuperiorityFighter)
                return 0;
            switch (slotItem.Adept)
            {
                case 0:
                    return Math.Sqrt(9d / 10);
                case 7:
                    return Math.Sqrt(120d / 10);
                default:
                    return Math.Sqrt((slotItem.Adept * 15 + 9) / 10d);
            }
        }


		public static double CalcViewRange(this Fleet fleet)
		{
			return ViewRangeCalcLogic.Get(KanColleClient.Current.Settings.ViewRangeCalcType).Calc(fleet.Ships);
		}

		public static bool IsHeavilyDamage(this LimitedValue hp)
		{
			return (hp.Current / (double)hp.Maximum) <= 0.25;
		}

		/// <summary>
		/// 現在のシーケンスから護衛退避した艦娘を除きます。
		/// </summary>
		public static IEnumerable<Ship> WithoutEvacuated(this IEnumerable<Ship> ships)
		{
			return ships.Where(ship => !ship.Situation.HasFlag(ShipSituation.Evacuation) && !ship.Situation.HasFlag(ShipSituation.Tow));
		}
	}
}
