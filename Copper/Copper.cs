using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using Microsoft.Extensions.Logging;

namespace Copper;

internal sealed class Program
{
	private ProgramConfig Config { get; }
	public DiscordClient Discord { get; }
	private CommandsNextExtension CommandsService { get; }

	public Program(ProgramConfig cfg, int shardId)
	{
		Config = cfg;

		var dcfg = new DiscordConfiguration
		{
			Token = Config.Token,
			TokenType = TokenType.Bot,
			Intents = DiscordIntents.All,
			MinimumLogLevel = LogLevel.Trace,
			ShardId = shardId,
			ShardCount = Config.ShardCount
		};
		Discord = new DiscordClient(dcfg);
		
		var cncfg = new CommandsNext
	}

	public async Task RunAsync()
	{
		var act = new DiscordActivity("balls", ActivityType.ListeningTo);
		await Discord.ConnectAsync(act, UserStatus.DoNotDisturb).ConfigureAwait(false);
	}
}