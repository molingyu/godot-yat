<div align="center">
	<h3>YAT</h1>
	<p>Yet Another Terminal</p>
</div>

Terminal for Godot 4 allowing you to add your own commands to cheat, debug, or anything else.

## Prerequisites

-   [.NET SDK 7](https://dotnet.microsoft.com/en-us/download)
-   [.NET enabled Godot](https://godotengine.org/download/windows/)

## Usage

### Keybindings

To use this extension, you need to create these keybindings in your project:

-   `yat_toggle` - Toggles the state of the overlay.
-   `yat_history_previous` - Displays the previous command from history.
-   `yat_history_next` - Displays the next command from history.

### Builtin commands

> More information about each command can be found in their manuals.

| Command                            | Alias        | Description                        |
| ---------------------------------- | ------------ | ---------------------------------- |
| cls                                | clear        | Clears the console.                |
| man <command_name>                 | -            | Displays the manual for a command. |
| quit                               | -            | Quits the game.                    |
| echo <text>                        | -            | Displays the given text.           |
| restart                            | reboot       | Restarts the level.                |

### Creating commands

To create a command, you need to create C# file and implement `IYatCommand` interface.

As an example, let's look at `Cls` command:

```csharp
public partial class Cls : IYatCommand
{
	public string Name => "cls";

	public string Description => "Clears the console.";

	public string Usage => "cls";

	public string[] Aliases => new string[] { "clear" };

	public void Execute(string[] args, YAT yat)
	{
		yat.Cli.Clear();
	}
}
```

#### Adding command to YAT

To add a command to the YAT all you have to do is call `AddCommand` method on YAT instance:

```csharp
GetNode<YAT>("/root/YAT").AddCommand(new Cls());
```

#### Creating custom windows

Lorem ipsum dolor sit amet.

## License

Licensed under [MIT license](./LICENSE).
