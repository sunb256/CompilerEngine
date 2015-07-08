using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstNot]")]
  public class AstNot : Ast
  {
    private Ast _code;

    public AstNot(Ast code)
    {
      _code = code;
    }

    public override Ast Run()
    {
      var ret = _code.Run();

      if (ret.GetType().Name != "AstBool")
      {
        throw new Exception("grammer error :: can't apply not object!");
      }

      return (AstBool)ret;
    }
  }
}
