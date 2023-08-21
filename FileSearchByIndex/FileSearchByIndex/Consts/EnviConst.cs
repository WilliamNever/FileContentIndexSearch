using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Consts
{
    public static class EnviConst
    {
        private const string IndexFolder = "Indexes";
        public static string IndexesFolderPath { get => Path.Combine(Environment.CurrentDirectory, IndexFolder); }
    }
}
