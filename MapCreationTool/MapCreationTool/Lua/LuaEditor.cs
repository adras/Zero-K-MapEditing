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
		T GetValue(string input);
		string GetValueString(T input);
	}

	internal class IntProvider : IValueProvider<int?>
	{
		public object GetValue(string input)
		{
			return GetValue(input);
		}
		int? IValueProvider<int?>.GetValue(string input)
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

		public string GetValueString(object input)
		{
			return GetValueString(input);
		}

	}

	internal class DoubleProvider : IValueProvider<double?>, IValueProvider
	{
		public object GetValue(string input)
		{
			return GetValue(input);
		}

		double? IValueProvider<double?>.GetValue(string input)
		{
			double result;
			if (!double.TryParse(input, out result))
				return null;

			return result;
		}
		public string GetValueString(double? input)
		{
			return input?.ToString(CultureInfo.InvariantCulture);
		}

		public string GetValueString(object input)
		{
			return GetValueString(input);
		}


	}


	internal class StringProvider : IValueProvider<string>, IValueProvider
	{
		public object GetValue(string input)
		{
			return GetValue(input);
		}

		string IValueProvider<string>.GetValue(string input)
		{
			return input;
		}

		public string GetValueString(string input)
		{
			return $"\"input\"";
		}

		public string GetValueString(object input)
		{
			return GetValueString(input);
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


		public T GetValue<T>(string key) where T : struct
		{
			Type valueType = typeof(T);
			if (!providers.ContainsKey(valueType))
				throw new NotSupportedException($"Reading types of: {typeof(T).Name} is not supported");

			string stringValue = GetValueFromContent(key);

			T value = (T)providers[valueType].GetValue(stringValue);
			return value;
		}

		public void SetValue<T>(string key, T value) where T : struct
		{
			Type valueType = typeof(T);
			if (!providers.ContainsKey(valueType))
				throw new NotSupportedException($"Reading types of: {typeof(T).Name} is not supported");

			string valueString = providers[valueType].GetValueString(value);
			SetValueInContent(key, valueString);
		}


		private string GetValueFromContent(string key)
		{
			int keyStartIdx = fileContent.IndexOf(key);
			if (keyStartIdx == -1)
				return null;

			string subContent = fileContent.Substring(keyStartIdx);

			int startValueIdx = subContent.IndexOf("=");
			int endValueIdx = subContent.IndexOf("\n");

			if (startValueIdx == -1 || endValueIdx == -1)
				return null;

			string rawContent = subContent.Substring(startValueIdx, endValueIdx - startValueIdx);
			// Remove =, \n and whitespaces at beginning and end
			string result = rawContent.Trim('=', '\n').TrimStart().TrimEnd();
			return result;
		}

		private void SetValueInContent(string key, string value)
		{
			int keyStartIdx = fileContent.IndexOf(key);
			if (keyStartIdx == -1)
				return;

			string subContent = fileContent.Substring(keyStartIdx);

			int startValueIdx = subContent.IndexOf("=");
			int endValueIdx = subContent.IndexOf("\n");

			if (startValueIdx == -1 || endValueIdx == -1)
				return;

			string rawContent = subContent.Substring(startValueIdx, endValueIdx - startValueIdx);

		}

	}
}
