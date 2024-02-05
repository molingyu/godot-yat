using System.Collections.Generic;
using System.Text;
using Godot;
using Godot.Collections;
using YAT.Scenes.BaseTerminal;
using YAT.Types;
using static YAT.Scenes.BaseTerminal.BaseTerminal;

namespace YAT.Helpers;

public static class Scene
{
	public static bool PrintChildren(BaseTerminal terminal, string path)
	{
		Node node = GetFromPathOrDefault(path, terminal.SelectedNode.Current, out path);

		if (node is null)
		{
			terminal.Output.Error(Messages.InvalidNodePath(path));
			return false;
		}

		var children = node.GetChildren();

		if (children.Count == 0)
		{
			terminal.Print($"Node '{path}' has no children.", PrintType.Warning);
			terminal.Output.Warning(Messages.NodeHasNoChildren(path));
			return true;
		}

		StringBuilder sb = new();
		sb.AppendLine($"Found {children.Count} children of '{node.Name}' node:");

		foreach (Node child in children)
		{
			sb.Append($"[{child.Name}] ({child.GetType().Name}) - {child.GetPath()}");
			sb.AppendLine();
		}

		terminal.Print(sb);

		return true;
	}

	/// <summary>
	/// Retrieves a Node from a given path or returns a default Node if the path is "." or "./".
	/// </summary>
	/// <param name="path">The path to the Node.</param>
	/// <param name="defaultNode">The default Node to return if the path is "." or "./".</param>
	/// <param name="newPath">The updated path of the retrieved Node.</param>
	/// <returns>The retrieved Node if it is a valid instance; otherwise, null.</returns>
	public static Node GetFromPathOrDefault(string path, Node defaultNode, out string newPath)
	{
		Node node = (path == "." || path == "./")
			? defaultNode
			: defaultNode.GetNodeOrNull(path);

		newPath = node?.GetPath();
		newPath = newPath is null ? path : newPath;

		if (GodotObject.IsInstanceValid(node)) return node;

		return null;
	}

	public static IEnumerator<Dictionary> GetNodeMethods(Node node)
	{
		if (!GodotObject.IsInstanceValid(node)) return null;

		return node.GetMethodList().GetEnumerator();
	}

	public static bool TryFindNodeMethodInfo(Node node, string methodName, out NodeMethodInfo info)
	{
		info = default;

		if (!GodotObject.IsInstanceValid(node)) return false;

		var method = GetNodeMethods(node);

		if (method is null) return false;

		while (method.MoveNext())
		{
			if (!method.Current.TryGetValue("name", out var value) || (string)value != methodName) continue;

			info = GetNodeMethodInfo(method.Current);

			return true;
		}

		return true;
	}

	public static NodeMethodInfo GetNodeMethodInfo(Dictionary method)
	{
		var name = method.TryGetValue("name", out var value)
			? value.AsStringName()
			: (StringName)string.Empty;
		var arguments = method.TryGetValue("args", out var args)
			? args.AsGodotArray<Godot.Collections.Dictionary<string, Variant>>()
			: new Array<Godot.Collections.Dictionary<string, Variant>>();
		var defaultArguments = method.TryGetValue("default_args", out Variant defaultArgs)
			? defaultArgs
			: new Array<Variant>();
		var flags = method.TryGetValue("flags", out var f)
			? (MethodFlags)(int)f
			: MethodFlags.Default;
		var id = method.TryGetValue("id", out var i) ? (int)i : 0;
		var @return = method.TryGetValue("return", out var r)
			? r.AsGodotDictionary<string, Variant>()
			: new Godot.Collections.Dictionary<string, Variant>();

		return new(
			name,
			arguments,
			defaultArguments.AsGodotArray<Variant>(),
			flags,
			id,
			@return
		);
	}
}
