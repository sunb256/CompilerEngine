using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstMinus]")]
  public class AstMinus : Ast
  {
    private Ast _code;

    public AstMinus(Ast code)
    {
      _code = code;
    }

    public override Ast Run()
    {
      AstInt tmp = (AstInt)_code.Run();

      var result = new AstInt(tmp.Value * -1);
      return result;
    }

  }
}
