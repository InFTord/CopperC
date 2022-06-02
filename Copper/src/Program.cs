using System.Text;
using Newtonsoft.Json;

namespace Copper
{
	internal sealed class Copper
	{
		private static CancellationTokenSource CancelTokenSource { get; } = new();
		private static CancellationToken CancelToken => CancelTokenSource.Token;
		private static List<Program> Shards { get; } = new();

		public static void Main()
		{
			MainAsync().GetAwaiter().GetResult();
		}

		private static async Task MainAsync()
		{
			var cfg = new ProgramConfig();
			string json;

			if (!File.Exists("config.json"))
			{
				json = JsonConvert.SerializeObject(cfg);
				await File.WriteAllTextAsync("config.json", json, new UTF8Encoding(false));
				Console.WriteLine("well you forgor to create a config dumbass");
				Console.ReadKey();
			}

			json = await File.ReadAllTextAsync("config.json", new UTF8Encoding(false));
			cfg =  JsonConvert.DeserializeObject<ProgramConfig>(json);

			var tskl = new List<Task>();
			for (var i = 0; i < cfg.ShardCount; i++)
			{
				var bot = new Program(cfg, i);
				Shards.Add(bot);
				tskl.Add(bot.RunAsync());
				await Task.Delay(7500).ConfigureAwait(false);
			}

			await Task.WhenAll(tskl).ConfigureAwait(false);

			try
			{
				await Task.Delay(-1, CancelToken).ConfigureAwait(false);
			}
			catch (Exception)
			{
				// ignored
			}
		}
	}
}