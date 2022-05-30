using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Copper.commands;

public class TestCommands : BaseCommandModule
{
	[Command("createchannel")]
	public async Task CreateChannelAsync(CommandContext ctx, [RemainingText] string name)
	{
		if (string.IsNullOrEmpty(name))
		{
			await ctx.RespondAsync("provide a channel name");
			return;
		}

		var channel = await ctx.Guild.CreateTextChannelAsync(name);
		await ctx.RespondAsync("done!");
	}
}