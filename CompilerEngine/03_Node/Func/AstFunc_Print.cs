using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstFunc_Print]")]
  public class AstFunc_Print : AstFunc
  {
    public AstFunc_Print()
    {
      base.argsCnt = 1;
    }

    public override Ast Exec(List<Ast> parameter)
    {
      Ast code = parameter[0];
      Console.WriteLine(code.Run().ToString());

      return null;
    }


    public override string ToString()
    {
      return "<Print>";
    }
  }
}
