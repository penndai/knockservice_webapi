using knockService.Helper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace knockService.Controllers
{
	[Route("api/[Controller]")]
	public class TriangleTypeController : Controller
	{
		//Get api/triangletype
		[HttpGet]
		public Task<JsonResult> WhatShapeIsThisAsync(
			[CustomConstraint.RequiredFromQuery] int a,
			[CustomConstraint.RequiredFromQuery] int b,
			[CustomConstraint.RequiredFromQuery] int c)
		{
			return Task.Run(() =>
			{
				return Json(WhatShapeIsThis(a, b, c));
			});
		}

		private string WhatShapeIsThis(int a, int b, int c)
		{
			if (a <= 0 || b <= 0 || c <= 0 || ((b + c) <= a) || ((a + c) <= b) || ((a + b) <= c))
			{
				throw new ExceptionHandler.TriangleException("Error");
			}
			else
			{
				// hashset ignores the int value already exists in the set.
				HashSet<int> lines = new HashSet<int>();
				lines.Add(a);
				lines.Add(b);
				lines.Add(c);

				return lines.Count == 1 ? Enum.GetName(typeof(TriangleType), TriangleType.Equilateral) : lines.Count == 2 ? Enum.GetName(typeof(TriangleType), TriangleType.Isosceles) : Enum.GetName(typeof(TriangleType), TriangleType.Scalene);
			}
		}
	}
}
