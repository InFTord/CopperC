using System.Reflection;
using Copper.commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.EventArgs;
using Microsoft.Extensions.Logging;

namespace Copper;

internal sealed class Program
{
	private ProgramConfig Config { get; }
	public DiscordClient Discord { get; }
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
			MinimumLogLevel = LogLevel.Trace,
			ShardId = shardId,
			ShardCount = Config.ShardCount
		};
		Discord = new DiscordClient(dcfg);

		var cncfg = new CommandsNextConfiguration
		{
			StringPrefixes = Config.Prefixes,
			EnableMentionPrefix = true,
			CaseSensitive = false,
			UseDefaultCommandHandler = true
		};

		CommandsService = Discord.UseCommandsNext(cncfg);
		CommandsService.RegisterCommands(typeof(Copper).GetTypeInfo().Assembly);

		var slcfg = new SlashCommandsConfiguration();

		SlashService = Discord.UseSlashCommands(slcfg);
		SlashService.RegisterCommands<slashcommands>(Config.GuildId);
		SlashService.SlashCommandErrored += SlashService_CommandErrored;
	}

	private async Task SlashService_CommandErrored(SlashCommandsExtension sender, SlashCommandErrorEventArgs e)
	{
		var embed = new DiscordEmbedBuilder
		{
			Title = "Error!",
			Color = DiscordColor.Red
		};

		switch (e.Exception.GetBaseException())
		{
			case SlashExecutionChecksFailedException:
				embed.Description = "You dont have permissions to use this command!";
				break;
			default:
				embed.AddField("Error message:", $"{Formatter.InlineCode(e.Exception.Message)}");
				break;
		}

		

		await e.Context.CreateResponseAsync(embed);
	}

	public async Task RunAsync()
	{
		var act = new DiscordActivity("balls", ActivityType.ListeningTo);
		await Discord.ConnectAsync(act, UserStatus.DoNotDisturb).ConfigureAwait(false);
	}
}