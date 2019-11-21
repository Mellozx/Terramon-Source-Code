using System;
using Terramon.Pokemon;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Terramon.Items.MiscItems
{
    public class RareCandyUniversal : GlobalNPC
    {
        public override void NPCLoot(NPC npc)
        {
            if (Main.rand.Next(100) < 3)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"));
            if (Main.rand.Next(100) < 2 && Main.hardMode == true)
                Item.NewItem(npc.getRect(), mod.ItemType("SuperCandy"));
            if (npc.type == NPCID.KingSlime)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 6);
            if (npc.type == NPCID.QueenBee)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 5);
            if (npc.type == NPCID.EyeofCthulhu)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 7);
            if (Array.IndexOf(new int[] { NPCID.EaterofWorldsBody, NPCID.EaterofWorldsHead, NPCID.EaterofWorldsTail }, npc.type) > -1 && npc.boss)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 8);
            if (npc.type == NPCID.BrainofCthulhu)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 8);
            if (npc.type == NPCID.SkeletronHead)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 10);
            if (npc.type == NPCID.WallofFlesh)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 15);
            if (npc.type == NPCID.Retinazer)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 5);
            if (npc.type == NPCID.Spazmatism)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 5);
            if (npc.type == NPCID.SkeletronPrime)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 9);
            if (npc.type == NPCID.TheDestroyer)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 9);
            if (npc.type == NPCID.Plantera)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 8);
            if (npc.type == NPCID.Golem)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 8);
            if (npc.type == NPCID.DukeFishron)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 14);
            if (npc.type == NPCID.CultistBoss)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 17);
            if (npc.type == NPCID.MoonLordHead)
                Item.NewItem(npc.getRect(), mod.ItemType("RareCandy"), 40);
        }

        public override bool? CanHitNPC(NPC npc, NPC target)
        {
            if (target.modNPC != null && target.modNPC is ParentPokemonNPC)
            {
                return false;
            }
                return null;
        }
    }
}