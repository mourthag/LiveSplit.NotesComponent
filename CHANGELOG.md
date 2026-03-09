# Changelog

## v1.2
* Fixed unnamed note sections (without `!!!` header) not displaying correctly — mapping is now computed at runtime based on split position relative to the nearest named anchor
* Fixed color changes in the settings dialog not taking effect until layout reload — colors are now read live on every frame
* Fixed default text color being hardcoded to white — now configurable via settings
* Added "Default Text Color" and "Default Background Color" controls to the Colors settings panel
* Added optional debug logging (toggle in settings) — writes a `notes_debug.log` next to the notes file
* Fixed CS0436 build warning caused by namespace collision with `LiveSplit.View`

## v1.1
* Fixed some memory leaks
* Fixed window resizing
* Moved to VS Code
* Moved to VS Code C# Dev Kit
* Added some new syntax (see example notes)
* Added `%%%` comments
* Leading and trailing spaces are now removed
* Follow-up notes (unnamed sections after a `!!!` header) can now be used
