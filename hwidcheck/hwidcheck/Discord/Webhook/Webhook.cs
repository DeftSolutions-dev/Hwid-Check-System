using System.Net;
using Discord.Webhook.HookRequest;

namespace Discord.Webhook
{
    public class DiscordWebhook
	{
		public string HookUrl { get; set; }

		public void Hook(DiscordHook HookRequest)
		{
			new WebClient
			{
				Headers =
				{
					{
						"Content-Type",
						"multipart/form-data; boundary=" + HookRequest.Boundary
					}
				}
			}.UploadData(this.HookUrl, HookRequest.Body.ToArray());
		}
	}
}
