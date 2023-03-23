# ScriptsJoelsAudioPlayer

Plans:

	- Find a way to update playback position text while dragging the progress bar knob.
		- When initially attempting to do this, I tried using FixedUpdate every 0.25 seconds in order to prevent using a division every frame.
		- This was still partially laggy and very unclean looking, as the text updated quite slowly compared to the rest of the program.

	- Volume slider?

	- Rewrite song loading so that previous audio file and next audio file are known.
		- Current version is kind of a mess.

	- Add keyboard controls.
		- Space to pause and unpause.
		- Left and right keys to play next song.

	- Shuffle functionality.