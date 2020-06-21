﻿using DataStore;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ui.Common;
using Ui.Common.Interfaces;
using System.IO;

namespace Libra
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuBuilder.Default
                .DetectMenuOn<MainMenu>()
                .AddDefaultExit()
                .Build()
                .Process().Wait();
        }
    }
}
