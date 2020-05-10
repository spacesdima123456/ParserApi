﻿using Microsoft.AspNetCore.Mvc;

namespace ParserApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StartController : ControllerBase
    {
        [HttpGet]
        public string Get()=> "Parse API start";
    }
}


