using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Discord.Webhook.HookRequest
{
	public class DiscordHookBuilder
	{
		private DiscordHookBuilder(string Nickname, string AvatarUrl)
		{
			this._bound = "------------------------" + DateTime.Now.Ticks.ToString("x");
			this._nick = Nickname;
			this._avatar = AvatarUrl;
			this._json = new JObject();
			this.Embeds = new List<DiscordEmbed>();
		}

		public static DiscordHookBuilder Create(string Nickname = null, string AvatarUrl = null)
		{
			return new DiscordHookBuilder(Nickname, AvatarUrl);
		}

		public FileInfo FileUpload { get; set; }

		public List<DiscordEmbed> Embeds { get; private set; }

		public string Message { get; set; }

		public bool UseTTS { get; set; }

		public DiscordHook Build()
		{
			MemoryStream memoryStream = new MemoryStream();
			byte[] bytes = Encoding.UTF8.GetBytes("--" + this._bound + "\r\n");
			memoryStream.Write(bytes, 0, bytes.Length);
			if (this.FileUpload != null)
			{
				string s = "Content-Disposition: form-data; name=\"file\"; filename=\"" + this.FileUpload.Name + "\"\r\nContent-Type: application/octet-stream\r\n\r\n";
				byte[] bytes2 = Encoding.UTF8.GetBytes(s);
				memoryStream.Write(bytes2, 0, bytes2.Length);
				byte[] array = File.ReadAllBytes(this.FileUpload.FullName);
				memoryStream.Write(array, 0, array.Length);
				string s2 = "\r\n--" + this._bound + "\r\n";
				byte[] bytes3 = Encoding.UTF8.GetBytes(s2);
				memoryStream.Write(bytes3, 0, bytes3.Length);
			}
			this._json.Add("username", this._nick);
			this._json.Add("avatar_url", this._avatar);
			this._json.Add("content", this.Message);
			this._json.Add("tts", this.UseTTS);
			JArray jarray = new JArray();
			foreach (DiscordEmbed discordEmbed in this.Embeds)
			{
				jarray.Add(discordEmbed.JsonData);
			}
			if (jarray.Count > 0)
			{
				this._json.Add("embeds", jarray);
			}
			string s3 = string.Concat(new string[]
			{
				"Content-Disposition: form-data; name=\"payload_json\"\r\nContent-Type: application/json\r\n\r\n",
				this._json.ToString(0, Array.Empty<JsonConverter>()),
				"\r\n--",
				this._bound,
				"--"
			});
			byte[] bytes4 = Encoding.UTF8.GetBytes(s3);
			memoryStream.Write(bytes4, 0, bytes4.Length);
			return new DiscordHook(memoryStream, this._bound);
		}

		private string _bound;

		private string _nick;

		private string _avatar;

		private JObject _json;
	}
}
