using Newtonsoft.Json.Linq;

namespace Discord.Webhook.HookRequest
{

	public class DiscordEmbedField
	{

		public DiscordEmbedField(string Name, string Value, bool Line = true)
		{
			JObject jobject = new JObject();
			jobject.Add("name", Name);
			jobject.Add("value", Value);
			jobject.Add("inline", Line);
			this.JsonData = jobject;
		}

		public JObject JsonData { get; private set; }
	}
}
