﻿using Data.ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DatabaseLayer
{
    public interface IAnswerAccess
    {
        List<Answer> CreateAnswer(List<Answer> answers);
    }
}
