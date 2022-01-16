using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.MapConverter
{
	/// <summary>
	/// 
	/// </summary>
	class MapCompilerSetting
	{
		public string MAP_CONV_EXE_PATH = @"Tools\PyMapConv\pymapconv.exe";

		public readonly string identifier;
		public readonly string settingValue;
		public readonly string custom;

		// Use this overload for custom settings which are not composed out of identifier, and setting value
		public MapCompilerSetting(string custom)
		{
			this.custom = custom;
		}

		public MapCompilerSetting(string identifier, string settingValue)
		{
			this.identifier = identifier;
			this.settingValue = settingValue;
		}

		public override string ToString()
		{
			string result;
			if (string.IsNullOrEmpty(custom))
			{
				result = $"{identifier} {settingValue}";
			}
			else
			{
				result = $"{custom}";
			}
			return result;
		}
	}

	class MapCompilerSettings
	{
		List<MapCompilerSetting> settings;
		public MapCompilerSettings()
		{
			settings = new List<MapCompilerSetting>();
		}

		public void AddSetting(MapCompilerSetting setting)
		{
			settings.Add(setting);
		}

		public void Clear()
		{
			settings.Clear();
		}

		public bool Remove(MapCompilerSetting setting)
		{
			bool result = settings.Remove(setting);
			return result;
		}

		public bool Remove(string identifier)
		{
			MapCompilerSetting? setting = settings.FirstOrDefault(s => s.identifier == identifier);
			if (setting == null)
				return false;

			bool result = settings.Remove(setting);
			return result;
		}

		public string GenerateParameterString()
		{
			StringBuilder sb = new StringBuilder();
			foreach (MapCompilerSetting setting in settings)
			{
				// Using ToString overload
				sb.Append(setting);
			}
			string result = sb.ToString();
			return result;
		}
	}

	class MapCompiler
	{
		public static void Compile(List<MapCompilerSetting> settings)
		{
			// Spring Map Format (SMF) compiler parameters
			// Not all parameter descriptions added, see springrts_smf_compiler\pymapconf.py for descriptions
			// OutFile -o, --outfile <output mapname.smf> (required) The name of the created map file. Should end in .smf. A tilefile (extension .smt) is also created, this name may contain spaces',
			// DiffuseMap -t --intex <texturemap.bmp> (required) Input bitmap to use for the map. Sizes must be multiples of 1024. Xsize and Ysize are determined from this file; xsize = intex width / 8, ysize = height / 8. Don\'t use Alpha unless you know what you are doing.',
			// HeightMap -a --heightmap help='|HEIGHT MAP| <heightmap file> (required) Input heightmap to use for the map, this should be 16 bit greyscale PNG image or a 16bit intel byte order single channel .raw image. Must be xsize*64+1 by ysize*64+1',
			// MetalMap -m --metalmap help='|METAL MAP| <metalmap.bmp> Metal map to use, red channel is amount of metal. Resized to xsize / 2 by ysize / 2.',
			// MaxHeight -x --maxheight
			// MinHeight -n --minheight
			// GeoventDecal -g --geoventfile
			// Compression -c --compress
			// Invert -i --invert
			// Lowpass -l --lowpass
			// Featureplacement -k --featureplacement
			// Featurelist -j --featurelist
			// FeatureMap -f --featuremap
			// GrassMap -r --grassmap
			// TypeMap -y --typemap
			// MiniMap -p --minimap
			// JustSmf -s --justsmf
			// nvxdoptions -v --nvdxt_options
			// HighresFilter --highresheightmapfilter
			// Dirty -c --dirty
			// Decompile -d --decompile
			// SkipTexture -s --skiptexture

			// Mandatory: --outfile, --intex, --heightmap, --metalmap, --maxheight, --minheight
		}
	}
}
