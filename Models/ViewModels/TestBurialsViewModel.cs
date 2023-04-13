using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models.ViewModels
{
    public class TestBurialsViewModel
    {
        public long Id { get; set; }
        public string Sex { get; set; }
        public string Depth { get; set; }
        public string AgeAtDeath { get; set; }
        public string TextileFunction { get; set; }
        public string Color { get; set; }
        public string TextileStructure { get; set; }
    }
}
