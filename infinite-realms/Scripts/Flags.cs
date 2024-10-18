using Godot;
using System;
using System.Collections.Generic;

public static class Flags
{
	private readonly static Dictionary<String, bool> _flags = new();

	public static void SetFlag(String flag)
	{
		_flags[flag] = true;
	}

	public static void ClearFlag(String flag)
	{
        _flags[flag] = false;
    }

	public static bool GetFlag(String flag)
	{
		return _flags.ContainsKey(flag) && _flags[flag];
	}


}
