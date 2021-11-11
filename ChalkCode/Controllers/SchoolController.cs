using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace ChalkCode.Controllers
{
    [ApiController]
    public class SchoolController
    {
        [Route("/classes")]
        [HttpGet]
        public HashSet<StudentClass> GetClasses()
        {
            return null; // school.classesInTheSchool;
        }
        
        [Route("/classes")]
        [HttpPost]
        public void AddClasses([FromBody] JsonContent body)
        {
            
            // school.addNewClasses(int, int)
        }
    }
}