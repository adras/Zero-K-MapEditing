using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapCreationTool.Lua
{
	internal interface IValueProvider
	{
		object GetValue(string input);

		string GetValueString(object input);
	}

	internal interface IValueProvider<T> : IValueProvider
	{
		//T GetValue(string input);
		string GetValueString(T input);
	}

	internal class IntProvider : IValueProvider<int?>
	{
		object IValueProvider.GetValue(string input)
		{
			return GetValue(input);
		}

		public int? GetValue(string input)
		{
			int result;
			if (!int.TryParse(input, out result))
				return null;

			return result;
		}

		public string GetValueString(int? input)
		{
			return input?.ToString();
		}

		string IValueProvider.GetValueString(object input)
		{
			return GetValueString((int?)input);
		}

	}

	internal class DoubleProvider : IValueProvider<double?>
	{
		object IValueProvider.GetValue(string input)
		{
			return GetValue(input);
		}

		public double? GetValue(string input)
		{
			double result;
			if (!double.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out result))
				return null;

			return result;
		}
		public string GetValueString(double? input)
		{
			return input?.ToString(CultureInfo.InvariantCulture);
		}

		string IValueProvider.GetValueString(object input)
		{
			return GetValueString((double?)input);
		}
	}

	internal class BoolProvider : IValueProvider<bool?>
	{
		object IValueProvider.GetValue(string input)
		{
			return GetValue(input);
		}

		public bool? GetValue(string input)
		{
			bool result;
			if (!bool.TryParse(input, out result))
				return null;

			return result;
		}
		public string GetValueString(bool? input)
		{
			return input?.ToString().ToLower();
		}

		string IValueProvider.GetValueString(object input)
		{
			return GetValueString((bool?)input);
		}
	}


	internal class StringProvider : IValueProvider<string>, IValueProvider
	{
		object IValueProvider.GetValue(string input)
		{
			return GetValue(input);
		}

		public string GetValue(string input)
		{
			string result = input.Trim('"');
			return result;
		}

		public string GetValueString(string input)
		{
			return $"\"{input}\"";
		}

		string IValueProvider.GetValueString(object input)
		{
			return GetValueString((string)input);
		}
	}

	public class LuaEditor
	{
		string fileContent;
		Dictionary<Type, IValueProvider> providers;

		public LuaEditor()
		{
			providers = new Dictionary<Type, IValueProvider>();
			providers.Add(typeof(int), new IntProvider());
			providers.Add(typeof(string), new StringProvider());
			providers.Add(typeof(double), new DoubleProvider());
			providers.Add(typeof(bool), new BoolProvider());
		}

		public bool Load(string path)
		{
			try
			{
				using (StreamReader sr = new StreamReader(path))
				{
					fileContent = sr.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error reading file: {path} - {ex.ToString()}");
				return false;
			}

			return true;
		}

		public bool Save(string path)
		{
			try
			{
				using (StreamWriter sw = new StreamWriter(path))
				{
					sw.Write(fileContent);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine($"Error saving file: {path} - {ex.ToString()}");
				return false;
			}
			return true;
		}


		public T GetValue<T>(string key)
		{
			Type valueType = typeof(T);
			if (!providers.ContainsKey(valueType))
				throw new NotSupportedException($"Reading types of: {typeof(T).Name} is not supported");

			string stringValue = GetValueFromContent(key);
			if (stringValue == null)
			{
				throw new InvalidOperationException($"Could not read value for key: {key}. Value not found ");
			}

			object valueObject = providers[valueType].GetValue(stringValue);
			if (valueObject == null)
			{
				// Check when this happens and improve exception message
				// It's related to an attempt to read the wrong type. e.g. read int, but value was double
				throw new InvalidOperationException($"Could not read value '{stringValue}' of {key} as type: {valueType.Name}");
			}

			T value = (T)valueObject;
			return value;
		}

		public void SetValue<T>(string key, T value)
		{
			Type valueType = typeof(T);
			if (!providers.ContainsKey(valueType))
				throw new NotSupportedException($"Reading types of: {typeof(T).Name} is not supported");

			string valueString = providers[valueType].GetValueString(value);
			// We should be right after the = sign, also include a whitespace
			SetValueInContent(key, $" {valueString}");
		}


		private string GetValueFromContent(string key)
		{
			int keyStartIdx = fileContent.IndexOf(key);
			if (keyStartIdx == -1)
				return null;

			string subContent = fileContent.Substring(keyStartIdx);

			int startValueIdx = subContent.IndexOf("=");
			int endValueIdx = subContent.IndexOf(',');

			if (startValueIdx == -1 || endValueIdx == -1)
				return null;

			string rawContent = subContent.Substring(startValueIdx, endValueIdx - startValueIdx);
			// Remove =, \n and whitespaces at beginning and end
			string result = rawContent.Trim('=').TrimStart().TrimEnd();
			return result;
		}

		private void SetValueInContent(string key, string value)
		{
			int keyStartIdx = fileContent.IndexOf(key);
			if (keyStartIdx == -1)
				return;

			string subContent = fileContent.Substring(keyStartIdx);

			int startValueIdx = subContent.IndexOf("=");
			int endValueIdx = subContent.IndexOf(',');

			if (startValueIdx == -1 || endValueIdx == -1)
				return;

			//string rawContent = subContent.Substring(startValueIdx + 1, endValueIdx - startValueIdx-1);

			string beforeValue = fileContent.Substring(0, keyStartIdx + startValueIdx + 1);

			string afterValue = fileContent.Substring(keyStartIdx + endValueIdx);

			string result = $"{beforeValue}{value}{afterValue}";
			fileContent = result;
		}

	}
}
