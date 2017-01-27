using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace knockService.Controllers
{
	[Route("api/[Controller]/{id?}")]
	public class ReverseWordsController : Controller
	{
		//Get api/reversewords
		[HttpGet]
		public JsonResult Get(string sentence)
		{
			if (sentence != null)
			{
				string[] strings = sentence.Split(' ');

				for(int i=0;i<strings.Length;i++)
				{
					strings[i] = ReverseGraphemeClusters(strings[i]);
				}

				sentence = string.Join(" ", strings);

				return Json(sentence);				
			}
			else
			{				
				return Json("");
			}
		}

		private IEnumerable<string> GraphemeClusters(string s)
		{
			var enumerator = StringInfo.GetTextElementEnumerator(s);
			while (enumerator.MoveNext())
			{
				yield return (string)enumerator.Current;
			}
		}
		private string ReverseGraphemeClusters(string s)
		{
			return string.Join("", GraphemeClusters(s).Reverse().ToArray());
		}
	}
}
