// meta-description: Base template for YAT commands extension.

using _BINDINGS_NAMESPACE_;
using YAT.Attributes;
using YAT.Enums;
using YAT.Interfaces;
using YAT.Types;

[Extension("_CLASS_", "Lorem ipsum dolor sit amet.", "[b]Usage[/b]: _CLASS_")]
public partial class _CLASS_ : IExtension
{
	public CommandResult Execute(CommandData data)
	{
		data.Terminal.Print("_CLASS_ is not yet implemented!", EPrintType.Error);

		return CommandResult.Failure;
	}
}
