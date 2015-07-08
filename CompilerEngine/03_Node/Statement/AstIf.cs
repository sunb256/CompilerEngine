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
    protected Ast _main_body;
    protected Ast _else_body;

    public AstIf(Ast if_condition, Ast main_body, Ast else_body)
    {
      this._if_condition = if_condition;
      this._main_body = main_body;
      this._else_body = else_body;
    }

    public override Ast Run()
    {
      Ast result = null;
      Ast code = null;
      AstBool b = (AstBool)_if_condition.Run();

      if (b.isTrue())
        code = _main_body;
      else
        code = _else_body;

      result = code.Run();
      return result;
    }

  }
}
