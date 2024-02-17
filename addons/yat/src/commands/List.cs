using System;
using System.Linq;
using System.Reflection;
using System.Text;
using YAT.Attributes;
using YAT.Interfaces;
using YAT.Scenes;
using YAT.Types;

namespace YAT.Commands;

[Command("list", "List all available commands", "[b]Usage[/b]: list", "lc")]
public sealed class List : ICommand
{
	public CommandResult Execute(CommandData data)
	{
		var sb = new StringBuilder();

		sb.AppendLine("Available commands:");

		foreach (var command in RegisteredCommands.Registered)
		{
			if (command.Value.GetCustomAttribute<CommandAttribute>() is not CommandAttribute attribute) continue;

			var description = command.Value.GetCustomAttribute<DescriptionAttribute>();

			// Skip aliases
			if (attribute.Aliases.Contains(command.Key)) continue;

			sb.Append($"[b]{command.Key}[/b] - ");
			sb.Append(description?.Description ?? attribute.Description);
			sb.AppendLine();
		}

		data.Terminal.Print(sb.ToString());

		return ICommand.Success();
	}
}
