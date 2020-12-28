using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RCT3Launcher.Utils
{
	class ArrayUtils
	{
		/// <summary>
		/// 报告指定的 System.Byte[] 在此实例中的第一个匹配项的索引。
		/// </summary>
		/// <param name="srcBytes">被执行查找的 System.Byte[]。</param>
		/// <param name="searchBytes">要查找的 System.Byte[]。</param>
		/// <returns>如果找到该字节数组，则为 searchBytes 的索引位置；如果未找到该字节数组，则为 -1。如果 searchBytes 为 null 或者长度为0，则返回值为 -1。</returns>
		public static int IndexOf(byte[] srcBytes, byte[] searchBytes, int offset = 0)
		{
			if (offset == -1) { return -1; }
			if (srcBytes == null) { return -1; }
			if (searchBytes == null) { return -1; }
			if (srcBytes.Length == 0) { return -1; }
			if (searchBytes.Length == 0) { return -1; }
			if (srcBytes.Length < searchBytes.Length) { return -1; }
			for (var i = offset; i < srcBytes.Length - searchBytes.Length; i++)
			{
				if (srcBytes[i] != searchBytes[0]) continue;
				if (searchBytes.Length == 1) { return i; }
				var flag = true;
				for (var j = 1; j < searchBytes.Length; j++)
				{
					if (srcBytes[i + j] != searchBytes[j])
					{
						flag = false;
						break;
					}
				}
				if (flag) { return i; }
			}
			return -1;
		}
	}
}
