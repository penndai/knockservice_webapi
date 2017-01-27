using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace knockService.ExceptionHandler
{
	public class TriangleException : InvalidOperationException
	{
		public TriangleException()
		{

		}

		public TriangleException(string message) : base(message)
		{

		}

		public TriangleException(string message, Exception inner) : base(message, inner)
		{

		}
	}
}
