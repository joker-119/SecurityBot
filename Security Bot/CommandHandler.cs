using System.Reflection;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace Security_Bot
{
	public class CommandHandler
	{
		private readonly DiscordSocketClient _client;
		private readonly CommandService _commands;

		public CommandHandler(DiscordSocketClient client, CommandService commands)
		{
			_commands = commands;
			_client = client;
		}

		public async Task InstallCommandsAsync()
		{
			_client.MessageReceived += HandleCommandAsync;

			await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);
		}

		private async Task HandleCommandAsync(SocketMessage messageParam)
		{
			if (!(messageParam is SocketUserMessage message)) return;

			int argPos = 0;

			if (!(message.HasCharPrefix('~', ref argPos) ||
			      message.HasMentionPrefix(_client.CurrentUser, ref argPos)) || message.Author.IsBot)
				return;
			
			SocketGuildUser sender = (SocketGuildUser)message.Author;
			bool isAllowed = false;
			foreach (SocketRole role in sender.Roles)
				if (role.Id == 1234567890 || role.Id == 1234567890)
					isAllowed = true;

			if (!isAllowed) return;
			
			SocketCommandContext context = new SocketCommandContext(_client, message);

			await _commands.ExecuteAsync(context, argPos, null);
		}
	}
}