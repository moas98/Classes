﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Training_Courses.Models.Helper
{
    public interface IErrorClass
    {
        string ErrorCode { get; set; }
        string ErrorMessage { get; set; }
        string ErrorProp { get; set; }

        void LoadError(string ErrorCode);
    }
}
