using System.Reflection;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.SlashCommands;
using Microsoft.Extensions.Logging;
#pragma warning disable CS0618

namespace Copper
{
	internal sealed class Program
	{
		private ProgramConfig Config { get; }
		private DiscordClient Discord { get; }

		private CommandsNextExtension CommandsService { get; }
		private SlashCommandsExtension SlashService { get; }

		public Program(ProgramConfig cfg, int shardId)
		{
			Config = cfg;

			var dcfg = new DiscordConfiguration
			{
				Token = Config.Token,
				TokenType = TokenType.Bot,
				Intents = DiscordIntents.All,
				MinimumLogLevel = LogLevel.Information,
				ShardId = shardId,
				ShardCount = Config.ShardCount
			};
			Discord = new DiscordClient(dcfg);
		
			Discord.Ready += Discord_Ready;
		
			var cncfg = new CommandsNextConfiguration
			{
				StringPrefixes = Config.Prefixes,
				EnableMentionPrefix = true,
				CaseSensitive = false,
				UseDefaultCommandHandler = true
			};

			CommandsService = Discord.UseCommandsNext(cncfg);
			CommandsService.RegisterCommands(typeof(Copper).GetTypeInfo().Assembly);
			
		}

		private static Task Discord_Ready(DiscordClient client, ReadyEventArgs e) => Task.CompletedTask;


		public async Task RunAsync()
		{
			var act = new DiscordActivity("balls", ActivityType.ListeningTo);
			await Discord.ConnectAsync(act, UserStatus.Online).ConfigureAwait(false);
		}
	
	
	}
}