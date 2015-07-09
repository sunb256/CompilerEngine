using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstIf]")]
  public class AstIf : Ast
  {
    protected Ast _if_condition;
    protected Ast _if_body;
    protected Ast _el_body;

    public AstIf(Ast if_condition, Ast if_body, Ast el_body)
    {
      this._if_condition = if_condition;
      this._if_body = if_body;
      this._el_body = el_body;
    }

    public override Ast Run()
    {
      Ast result = null;
      Ast code = null;
      AstBool b = (AstBool)_if_condition.Run();

      if (b.isTrue())
        code = _if_body;
      else
        code = _el_body;

      result = code.Run();
      return result;
    }

  }
}
