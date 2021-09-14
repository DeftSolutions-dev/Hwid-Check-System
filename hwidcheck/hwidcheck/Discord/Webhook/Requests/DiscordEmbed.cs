using Newtonsoft.Json.Linq;

namespace Discord.Webhook.HookRequest
{
	public class DiscordEmbed
	{
		public DiscordEmbed(string Title = "", string Description = "", int Color = 0, string ImageUrl = "", string FooterText = "", string FooterIconUrl = "", DiscordEmbedField[] Fields = null)
		{
			JObject jobject = new JObject();
			jobject.Add("title", Title);
			jobject.Add("description", Description);
			jobject.Add("color", Color);
			JObject jobject2 = new JObject();
			jobject2.Add("url", ImageUrl);
			JObject jobject3 = new JObject();
			jobject3.Add("text", FooterText);
			jobject3.Add("icon_url", FooterIconUrl);
			jobject.Add("image", jobject2);
			jobject.Add("footer", jobject3);
			if (Fields != null)
			{
				JArray jarray = new JArray();
				foreach (DiscordEmbedField discordEmbedField in Fields)
				{
					jarray.Add(discordEmbedField.JsonData);
				}
				jobject.Add("fields", jarray);
			}
			this.JsonData = jobject;
		}

		public JObject JsonData { get; private set; }
	}
}
