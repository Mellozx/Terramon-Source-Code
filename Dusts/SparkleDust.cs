using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Terramon.Dusts
{
	public class SparkleDust : ModDust
	{
		public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;
			dust.frame = new Rectangle(0, 0, 26, 26);
			dust.color = new Color(87, 178, 217);
			dust.scale = 0f;
			dust.noLight = true;
		}

		public override bool Update(Dust dust)
		{
			float light = 0.35f * dust.scale;
			Lighting.AddLight(dust.position, light, light, light);

			dust.velocity.Y = -3f;
			dust.position += dust.velocity;
			if (dust.scale < 1f) dust.scale += 0.05f;
			if (dust.scale > 0.6f) dust.active = false;
			return false;
		}
	}
}