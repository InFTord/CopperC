using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using DSharpPlus.SlashCommands.Attributes;

namespace Copper.commands;

public class slashcommands : ApplicationCommandModule
{
	#region Before / After

	public override Task<bool> BeforeSlashExecutionAsync(InteractionContext ctx)
	{
		Console.WriteLine($"Before executing the slash command '{ctx.CommandName}'");
		return Task.FromResult(true);
	}

	public override Task AfterSlashExecutionAsync(InteractionContext ctx)
	{
		Console.WriteLine($"After executing the slash command '{ctx.CommandName}'");
		return Task.FromResult(true);
	}

	#endregion

	[SlashCommand("createChannel", "create channel")]
	[SlashRequirePermissions(Permissions.ManageChannels)]
	public async Task CreateGuildChannelSlash(InteractionContext ctx, [Option("name", "channel name")] string name)
	{
		await ctx.Guild.CreateTextChannelAsync(name);
		await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder().WithContent($"Created a channel with name ``{name}``"));
	}
}