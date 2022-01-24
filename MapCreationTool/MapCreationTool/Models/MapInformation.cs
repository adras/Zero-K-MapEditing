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
		internal LuaEditor editor;

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
			result.editor.Load(mapInfoPath);
			result.ReadValues();

			return result;
		}

		private void ReadValues()
		{
			Author = editor.GetValue<string>("author");
			Description = editor.GetValue<string>("description");
			ExtractorRadius = editor.GetValue<double>("extractorRadius");
			Gravity = editor.GetValue<int>("gravity");
			MapHardness = editor.GetValue<int>("maphardness");
			MaxMetal = editor.GetValue<double>("maxMetal");
			Name = editor.GetValue<string>("name");
			ShortName = editor.GetValue<string>("shortname");
			Version = editor.GetValue<string>("version");
		}

	}
}
