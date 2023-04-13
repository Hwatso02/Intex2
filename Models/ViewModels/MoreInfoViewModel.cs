using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex2.Models.ViewModels
{
    public class MoreInfoViewModel
    {
        public long Id { get; set; }
        public string Sex { get; set; }
        public string Depth { get; set; }
        public string AgeAtDeath { get; set; }
        public string TextileFunction { get; set; }
        public string Color { get; set; }
        public string TextileStructure { get; set; }
        public string Component { get; set; }
        public string Material { get; set; }
        public string Direction { get; set; }
        public string Count { get; set; }
        public string Ply { get; set; }
        public string HairColor { get; set; }
        public string HeadDirection { get; set; }
        public TestBurialsViewModel BurialInfo { get; set; }
    }
}


