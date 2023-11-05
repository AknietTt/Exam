using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Parser.DTOs {
    public class LeagueRefDto {
        public int Id { get; set; }
        public CategoryDto Category { get; set; }
        public string Name { get; set; }
        public int UserCount { get; set; }
    }
}
