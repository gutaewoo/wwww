﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using TheTwitch.Commons.ComboSystem;

namespace TheTwitch
{
    class TwitchCombo : ComboProvider
    {
        public int BlueTrinketLevel;
        public bool AutoBuyBlueTrinket;

        public TwitchCombo(float targetSelectorRange, IEnumerable<Skill> skills, Orbwalking.Orbwalker orbwalker) : base(targetSelectorRange, skills, orbwalker)
        {
        }

        public TwitchCombo(float targetSelectorRange, Orbwalking.Orbwalker orbwalker, params Skill[] skills) : base(targetSelectorRange, orbwalker, skills)
        {
        }

        public override void Update()
        {
            base.Update();

            if (AutoBuyBlueTrinket && ObjectManager.Player.Level >= BlueTrinketLevel && ObjectManager.Player.InFountain() && ObjectManager.Player.InventoryItems.Any(item => item.Id == ItemId.Warding_Totem_Trinket))
            {
                ObjectManager.Player.BuyItem(ItemId.Scrying_Orb_Trinket);
            }
        }

        public override bool ShouldBeDead(LeagueSharp.Obj_AI_Base target, float additionalSpellDamage = 0f)
        {
            return base.ShouldBeDead(target, TwitchE.GetRemainingPoisonDamageMinusRegeneration(target));
        }
    }
}
