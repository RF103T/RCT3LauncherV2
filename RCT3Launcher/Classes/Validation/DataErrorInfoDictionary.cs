using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher.Class.Validation
{
	class DataErrorInfoDictionary : Dictionary<string, string>
	{
		public new string this[string key]
		{
			get
			{
				if (ContainsKey(key))
					return base[key];
				return string.Empty;
			}
			set
			{
				if (ContainsKey(key))
					base[key] = value;
				else
					base.Add(key, value);
			}
		}

		public new void Add(string key, string value)
		{
			if (!ContainsKey(key))
				base.Add(key, value);
			else
				base[key] = value;
		}
	}
}
