using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terramon.Pokemon.FirstGeneration.Normal._caughtForms;
using Terramon.UI;
using Terraria;
using Terraria.ModLoader.IO;

namespace Terramon.Players
{
    public sealed partial class TerramonPlayer
    {
        private const string
            PKM_PARTY_SLOTS_TAG = "PartySlotList",
            PKM_PARTY_SLOT_TAG = "Slot",
            PKM_PARTY_SLOT_INDEX = "Index",
            PKM_PARTY_CAUGHT_POKEBALL = "PKM";


        private void SaveParty(TagCompound terramonTag)
        {
            TagCompound partyTag = new TagCompound();
            VanillaItemSlotWrapper[] slots = TerramonMod.Instance.PartySlots.Slots;

            for (int i = 0; i < slots.Length; i++)
            {
                Item item = slots[i].Item;

                if (!(item?.modItem is PokeballCaught))
                    continue;

                TagCompound slotTag = new TagCompound()
                {
                    [PKM_PARTY_SLOT_INDEX] = i,
                    [PKM_PARTY_CAUGHT_POKEBALL] = item
                };

                partyTag.Add(PKM_PARTY_SLOT_TAG + i, slotTag);
            }

            terramonTag.Add(PKM_PARTY_SLOTS_TAG, partyTag);
        }

        private void LoadParty(TagCompound terramonTag)
        {
            if (!terramonTag.ContainsKey(PKM_PARTY_SLOTS_TAG))
                return;

            TagCompound partyTag = terramonTag.GetCompound(PKM_PARTY_SLOTS_TAG);
            VanillaItemSlotWrapper[] slots = TerramonMod.Instance.PartySlots.Slots;

            for (int i = 0; i < slots.Length; i++)
            {
                if (!partyTag.ContainsKey(PKM_PARTY_SLOT_TAG + i))
                    slots[i].Item.TurnToAir();
                else
                {
                    TagCompound slotTag = partyTag.GetCompound(PKM_PARTY_SLOT_TAG + i);
                    int slotIndex = slotTag.GetInt(PKM_PARTY_SLOT_INDEX);

                    slots[slotIndex].Item = slotTag.Get<Item>(PKM_PARTY_CAUGHT_POKEBALL);
                }
            }
        }
    }
}
