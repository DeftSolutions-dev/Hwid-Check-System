using System.Globalization;
using System.Windows.Media;

namespace Discord.Utils
{
    public class DiscordConverter
	{
		public static int ColorToHex(Color SourceColor)
        {
			return int.Parse(SourceColor.R.ToString("X2") + SourceColor.G.ToString("X2") + SourceColor.B.ToString("X2"), NumberStyles.HexNumber);
		}
	}
}
