﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Model
{
    public class BaseSearchModel
    {
        public int Skip { get; set; }
        public int Take { get; set; }
    }
}
