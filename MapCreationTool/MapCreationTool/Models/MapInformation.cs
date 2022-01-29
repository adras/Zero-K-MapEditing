using MapCreationTool.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MapCreationTool.Models
{
	public class MapInformation
	{
		private const string AUTHOR = "author";
		private const string VERSION = "version";
		private const string SHORTNAME = "shortname";
		private const string NAME = "name";
		private const string MAX_METAL = "maxMetal";
		private const string MAP_HARDNESS = "maphardness";
		private const string GRAVITY = "gravity";
		private const string EXTRACTOR_RADIUS = "extractorRadius";
		private const string DESCRIPTION = "description";
		private LuaEditor editor;

		public MapInformation()
		{
			editor = new LuaEditor();
		}

		public string Name { get; set; }
		
		public string ShortName { get; set; }
		
		public string Description { get; set; }
		
		public string Author { get; set; }
		
		public string Version { get; set; }
		
		public int MapHardness { get; set; }
		
		public int Gravity { get; set; }
		
		public double MaxMetal { get; set; }
		
		public double ExtractorRadius { get; set; }

		/*
		// SMF values are a comment in template, therefore it's not implemented right now
		public int MinHeight { get; set; }
		public int MaxHeight { get; set; }
		*/

		// ProjectSettings is a horrible dependency
		public static MapInformation LoadFrom(string mapInfoPath)
		{
			MapInformation result = new MapInformation();
			bool loadResult = result.editor.Load(mapInfoPath);
			if (loadResult)
			{
				result.GetValues();
			}

			return result;
		}
		internal void SaveAs(string mapInfoPath)
		{
			SetValues();
			editor.Save(mapInfoPath);
		}

		private void GetValues()
		{
			Author = editor.GetValue<string>(AUTHOR);
			Description = editor.GetValue<string>(DESCRIPTION);
			ExtractorRadius = editor.GetValue<double>(EXTRACTOR_RADIUS);
			Gravity = editor.GetValue<int>(GRAVITY);
			MapHardness = editor.GetValue<int>(MAP_HARDNESS);
			MaxMetal = editor.GetValue<double>(MAX_METAL);
			Name = editor.GetValue<string>(NAME);
			ShortName = editor.GetValue<string>(SHORTNAME);
			Version = editor.GetValue<string>(VERSION);
		}
		private void SetValues()
		{
			editor.SetValue(AUTHOR, Author);
			editor.SetValue(DESCRIPTION, Description);
			editor.SetValue(EXTRACTOR_RADIUS, ExtractorRadius);
			editor.SetValue(GRAVITY, Gravity);
			editor.SetValue(MAP_HARDNESS, MapHardness);
			editor.SetValue(MAX_METAL, MaxMetal);
			editor.SetValue(NAME, Name);
			editor.SetValue(SHORTNAME, ShortName);
			editor.SetValue(VERSION, Version);
		}


	}
}
