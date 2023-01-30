using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KT2CG.Render
{
	public static class HtmlExtensions
	{
		public static string ReplaceRangesWithSpans(this string text)
		{
			return text.Replace("△", "<span class='range1' ></span>")
				.Replace("◯", "<span class='range2' ></span>")
				.Replace("□", "<span class='range3' ></span>")
				.Replace("⬠", "<span class='range6' ></span>");
		}

		public static string ReplaceLineBreaksWithBreaks(this string text)
		{
			return text.Replace(Environment.NewLine, "<br />");
		}

		public static string BoldFromLineBreakToColon(this string text)
		{
			return Regex.Replace(text, @"<br />(.*?)\:", "<br /><b>$1</b>:<br />");
		
		}
	}
}