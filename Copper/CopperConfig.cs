using Newtonsoft.Json;

namespace Copper;

public sealed class ProgramConfig
{
	[JsonProperty("token")]
	public string Token { get; private set; } = "OTY5ODQyNTIwODA2ODA1NTA0.GkyNww.EK6OzMdRSvfJEYeQKZRfoMX2vFATVdUzByukAA";

	[JsonProperty("prefixes")] public string[] Prefixes { get; private set; } = { "c!", "c+" };

	[JsonProperty("shardcount")] public int ShardCount { get; private set; } = 1;
}