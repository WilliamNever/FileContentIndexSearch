using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearchByIndex.Core.Consts
{
    public static class EnviConst
    {
        private const string IndexFolder = "Indexes";
        private const string TmpWorkingFolder = "TempWorking";
        public static string IndexesFolderPath { get => Path.Combine(AppFolder, IndexFolder); }
        public static string TmpWorkingFolderPath { get => Path.Combine(AppFolder, TmpWorkingFolder); }
        public static string AppFolder { get => Environment.CurrentDirectory; }
        public static string EnvironmentNewLine { get => Environment.NewLine; }
        public static string SpecNewLine1 { get => "\n"; }
    }
}
