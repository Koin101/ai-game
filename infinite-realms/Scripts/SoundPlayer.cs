using Godot;
using System;

public partial class SoundPlayer : AudioStreamPlayer
{
	private static readonly System.Collections.Generic.Dictionary<String, AudioStreamPlayer> _sounds = new();
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var sounds = this.FindChildren("*", "AudioStreamPlayer");
		foreach (var sound in sounds)
		{
			_sounds.Add(sound.Name, (AudioStreamPlayer)sound);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public static void Play(string name) 
	{
		_sounds[name].Play();
	}

	public static void Stop(string name)
	{
		_sounds[name].Stop();
	}

	public static void StopAll()
	{
		foreach (var sound in _sounds.Keys)
		{
			_sounds[sound].Stop();
		}
	}
}
