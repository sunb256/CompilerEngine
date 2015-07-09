using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstFunc_Max]")]
  public class AstFunc_Max : AstFunc
  {
    public AstFunc_Max()
    {
      base.argsCnt = 2;
    }

    public override Ast Exec(List<Ast> parameter)
    {
      Ast code_1 = parameter[0];
      Ast code_2 = parameter[1];

      if (code_1.GetType().Name != "AstInt")
        throw new Exception("param1 not int in Max Func");

      if (code_2.GetType().Name != "AstInt")
        throw new Exception("param2 not int in Max Func");

      int max = Math.Max(((AstInt)code_1).Value, ((AstInt)code_2).Value);
      return new AstInt(max);
    }


    public override string ToString()
    {
      return "<Print>";
    }
  }
}
