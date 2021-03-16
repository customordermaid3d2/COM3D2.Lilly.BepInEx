using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Lilly.Plugin
{
    class MyUtill
    {
		/// <summary>
		/// 닷넷 3.5에선 컴파일 안되서 직접 복사 수정
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="separator"></param>
		/// <param name="values"></param>
		/// <returns></returns>
		public static string Join<T>(string separator, IEnumerable<T> values)
		{
			if (values == null)
			{
				return "";
			}
			if (separator == null)
			{
				separator = string.Empty;
			}
			string result;
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					result = string.Empty;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (enumerator.Current != null)
					{
						T t = enumerator.Current;
						string text = t.ToString();
						if (text != null)
						{
							stringBuilder.Append(text);
						}
					}
					while (enumerator.MoveNext())
					{
						stringBuilder.Append(separator);
						if (enumerator.Current != null)
						{
							T t = enumerator.Current;
							string text2 = t.ToString();
							if (text2 != null)
							{
								stringBuilder.Append(text2);
							}
						}
					}
					result = stringBuilder.ToString();
				}
			}
			return result;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="methodBase"></param>
		/// <returns></returns>
		public static string GetClassMethodName(System.Reflection.MethodBase methodBase)
        {
			return methodBase.ReflectedType.Name+"."+methodBase.Name+":" ;
        }
	}
}
