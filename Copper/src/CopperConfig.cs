using Newtonsoft.Json;

namespace Copper
{
	public sealed class ProgramConfig
	{
		[JsonProperty("token")]
		public string Token { get; private set; } = string.Empty;

		[JsonProperty("prefixes")] public string[] Prefixes { get; private set; } = { "c!", "c+"};

		[JsonProperty("shardcount")] public int ShardCount { get; private set; } = 1;

		[JsonProperty("guildId")] public ulong GuildId { get; private set; }
	}
}