using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstWhile]")]
  public class AstWhile : Ast
  {
    protected Ast _cond;
    protected Ast _body;

    public AstWhile(Ast cond, Ast body)
    {
      this._cond = cond;
      this._body = body;
    }

    public override Ast Run()
    {
      Ast code = null;

      while (true)
      {
        var ret = (AstBool)_cond.Run();
        if (!ret.isTrue())
        {
          break;
        }

        code = _body.Run();
      }

      return code;
    }

  }
}
