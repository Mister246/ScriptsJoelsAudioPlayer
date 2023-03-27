# ScriptsJoelsAudioPlayer

Plans:

	- Find a way to update playback position text while dragging the progress bar knob.
		- When initially attempting to do this, I tried using FixedUpdate every 0.25 seconds in order to prevent using a division every frame.
		- This was still partially laggy and very unclean looking, as the text updated quite slowly compared to the rest of the program.

	- Add buttons to move back and forth through playlist.

	- Add keyboard controls.
		- Space to pause and unpause.
		- Left and right keys to play next song.

	- Playlist that contains all audio files saved in the audio player.

	- Add support for other file types (at least .wav and .mp3)