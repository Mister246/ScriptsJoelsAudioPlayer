# ScriptsJoelsAudioPlayer

Plans:

	- Find a way to update playback position text while dragging the progress bar knob.
		- When initially attempting to do this, I tried using FixedUpdate every 0.25 seconds in order to prevent using a division every frame.
		- This was still partially laggy and very unclean looking, as the text updated quite slowly compared to the rest of the program.

	- Looping audio function.
		- BUG: When loop is disabled in the middle of playing audio, it starts a coroutine in order to ensure the audio stops playing when it finishes.
			- However, since the coroutine is being started from the script attached to the Loop Option Button game object, if the user clicks off the drop down menu, the game object disables, prevent the coroutine from finishing.

	- Automatically play next audio function.