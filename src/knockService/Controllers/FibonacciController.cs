using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace knockService.Controllers
{
	[Route("api/[controller]")]
    public class FibonacciController:Controller
    {
		// GET api/fibonacci
		[HttpGet]
		public long Get([CustomConstraint.RequiredFromQuery]long n)
		{
			if (n > 92 || n < -92)
			{
				throw new ArgumentOutOfRangeException("n", "Input parameter is out of the range of valid values -93 to 93.");
			}
			else
			{
				return Calculate(n);
			}
		}

		private long Calculate(long n)
		{			
			long a = 0;
			long b = 1;
			long result = 0;
			if (n == 0)
			{
				result = 0;
			}
			else if (n > 0)
			{
				result = FibonacciPositive(n, a, b);
			}
			else // in case of the input n is minus value
			{
				result = FibonacciPositive(n * -1, a, b);

				//shift the positive (n*-1) 1 bit right, then 1 bit left, to check is odd or even
				//if the n is even, return the minus value.
				if ((((n * -1) >> 1) << 1) == (n * -1))
				{
					result = result * -1;
				}
			}

			return result;
			
		}

		/// <summary>
		/// Calculate the Fibonacci number 
		/// </summary>
		/// <param name="n">The input argument n</param>
		/// <param name="a">The seed value for F(0)=0</param>
		/// <param name="b">The seed value for F(1)=1</param>
		/// <returns></returns>
		private long FibonacciPositive(long n, long a, long b)
		{
			long data = a;
			for (int i = 0; i < n; i++)
			{
				long temp = data;
				data = b;
				b = temp + b;
			}
			return data;
		}
	}
}
