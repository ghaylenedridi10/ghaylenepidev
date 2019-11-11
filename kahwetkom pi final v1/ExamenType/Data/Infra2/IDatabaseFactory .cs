﻿using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Data.Infra2
{
    public interface IDatabaseFactory : IDisposable
    {
        ExamenContext DataContext { get; }
    }

}
