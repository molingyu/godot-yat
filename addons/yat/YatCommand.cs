public partial interface IYatCommand
{
	/// <summary>
	/// Gets the name of the CLI.
	/// </summary>
	public string Name { get; }
	/// <summary>
	/// Gets the description of the CLI command.
	/// </summary>
	public string Description { get; }
	/// <summary>
	/// Gets the usage information for the command line interface.
	/// </summary>
	public string Usage { get; }
	/// <summary>
	/// Gets the aliases for this command.
	/// </summary>
	public string[] Aliases { get; }
	/// <summary>
	/// Executes the YAT command with the given arguments.
	/// </summary>
	/// <param name="yat">The YAT instance to execute the command on.</param>
	/// <param name="args">The arguments to pass to the command.</param>
	public void Execute(YAT yat, params string[] args);
}
