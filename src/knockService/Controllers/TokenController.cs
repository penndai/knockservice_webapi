using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace knockService.Controllers
{
	[Route("api/[Controller]")]
    public class TokenController:Controller
    {
		[HttpGet]
		public Guid WhatIsYourToken()
		{
			return Guid.Parse("3ce150bf-cc72-4f1b-9e69-6195a28654eb");
		}
    }
}
