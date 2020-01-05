﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRManagement.Models
{
    /// <summary>
    /// Model for company
    /// </summary>
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}
