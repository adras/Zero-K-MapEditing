# Lua files
It's pretty complicated to load and edit .lua files.

To simplify parsing/editing a few decisions will be made.

It also seems like there are no .lua libraries which help to resolve all issues

# Relua
* Ignores comments in file
* Although the parser is on a very low level which is nice, it's too tedious to use

# LsonLib
* Does it allow to keep the sourcecode at the end of mapinfo.lua?
* Does it support comments?

# Custom parser/editor
For now, we'll use the following design:
The following approach should be easy to implement

There's one major drawback however: If a key exists multiple times in the file, e.g. "name" appears in multiple structures
it will only be possible to edit the first occurence. Fixing that by allowing indices will crash when one file doesn't have a definition, but another file has it.
For that a proper context would be necessary. E.g. a path like: "startboxes.name", "player.name" etc, but this requires recursive parsing
But for now, we can hopefully ignore this feature.

Create an editor, which reads the entire file.

public class LuaEditor
{
	public int GetIntValue(string key)
	public string GetStringValue(string key)
	public double GetDoubleValue(string key)
	{
		// searches for the given key, and returns everything between the = and , symbols
	}

	public void ReplaceIntValue(string key, int value)
	public void ReplaceDoubleValue(string key, double value)
	public void ReplaceStringValue(string key, string value)
	{
		// Searches for the given key, and replaces everything between the = and , symbols with the given number
		// In case of string, quotes will be automatically added to the given value
	}

	public void Load(string path)
	{
		// Loads the given file into memory
		// A simple: string contents = File.ReadAll should be sufficient
	}

	public void Save(string path)
	{
		// Writes the current data into the given path
		// A simple: File.Write (contents) should be sufficient
	}
}