using System;
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
		/// 熟練度による制空能力ボーナス最小値を計算します。
		/// </summary>
		/// <param name="slotItem"></param>
		/// <returns></returns>
		public static int CalcMinAdeptBonusAirSuperiorityPotential(this SlotItem slotItem) {
            if (slotItem.Info.Type == SlotItemType.艦上戦闘機) {
                if (slotItem.Adept == 1) {
                    return 1;
                } else if (slotItem.Adept == 2) {
                    return 4;
                } else if (slotItem.Adept == 3) {
                    return 6;
                } else if (slotItem.Adept == 4) {
                    return 11;
                } else if (slotItem.Adept == 5) {
                    return 16;
                } else if (slotItem.Adept == 6) {
                    return 17;
                } else if (slotItem.Adept == 7) {
                    return 25;
                } else {
                    return 0;
                }
            } else {
                return 0; // 艦戦以外は簡単に吹き飛ぶので最小値としては計算に入れない
            }
        }

		/// <summary>
		/// 熟練度による制空能力ボーナス最大値を計算します。
		/// </summary>
		/// <param name="slotItem"></param>
		/// <returns></returns>
		public static int CalcMaxAdeptBonusAirSuperiorityPotential(this SlotItem slotItem) {
            if (slotItem.Info.Type == SlotItemType.艦上戦闘機) {
                if (slotItem.Adept == 1) {
                    return 2;
                } else if (slotItem.Adept == 2) {
                    return 5;
                } else if (slotItem.Adept == 3) {
                    return 8;
                } else if (slotItem.Adept == 4) {
                    return 12;
                } else if (slotItem.Adept == 5) {
                    return 18;
                } else if (slotItem.Adept == 6) {
                    return 18;
                } else if (slotItem.Adept == 7) {
                    return 26;
                } else {
                    return 1;
                }
            } else {
                // 艦戦以外はよくわからないので暫定的に1次関数＆切り上げ
                if (slotItem.Info.Type == SlotItemType.艦上攻撃機) {
                    return (int)Math.Ceiling(3 * slotItem.Adept / 7d);
                } else if (slotItem.Info.Type == SlotItemType.艦上爆撃機) {
                    return (int)Math.Ceiling(3 * slotItem.Adept / 7d);
                } else if (slotItem.Info.Type == SlotItemType.水上爆撃機) {
                    return (int)Math.Ceiling(9 * slotItem.Adept / 7d);
                } else {
                    return 0;
                }
            }
        }
			
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
				.Select(x => x.Item.CalcAirSuperiorityPotential(x.Current)
							+ x.Item.CalcMinAdeptBonusAirSuperiorityPotential())
				.Sum();
		}

		/// <summary>
		/// 指定した艦の制空能力の最大値を計算します。
		/// </summary>
		public static int CalcMaxAirSuperiorityPotential(this Ship ship)
		{
            return ship.EquippedSlots
				.Select(x => x.Item.CalcAirSuperiorityPotential(x.Current)
							+ x.Item.CalcMaxAdeptBonusAirSuperiorityPotential())
				.Sum();
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
