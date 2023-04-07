# ScriptsJoelsAudioPlayer

Plans:

	- Find a way to update playback position text while dragging the progress bar knob.
		- When initially attempting to do this, I tried using FixedUpdate every 0.25 seconds in order to prevent using a division every frame.
		- This was still partially laggy and very unclean looking, as the text updated quite slowly compared to the rest of the program.

	- Fix button scrollbar adjustment error :(

	- Change how audio files are ordered in the All Audio Files playlist.
		- Current version alphabetizes. 

	- Fix bug where OnAudioEnd doesn't properly execute when audio has been paused for a little while