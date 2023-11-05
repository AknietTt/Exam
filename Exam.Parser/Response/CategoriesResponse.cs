using Exam.Parser.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Parser.Response {
    public class CategoriesResponse {
        public List<CategoryRefDto> Categories { get; set; }
    }
}
