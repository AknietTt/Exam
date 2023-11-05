using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam.Parser {
    public class Context {
        public IStrategy Strategy { get; set; }

        public Context(IStrategy strategy) {
            Strategy = strategy;
        }

        public void ExecuteStrategy() {
            Strategy.Execute();
        }

        public void ExecuteStrategy(IStrategy strategy) {
            Strategy = strategy;
            Strategy.Execute();
        }
    }
}
