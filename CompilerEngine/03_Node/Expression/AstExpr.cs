using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstExpr]")]
  public class AstExpr : Ast
  {
    protected Ast _code_L;
    protected Operator _op;
    protected Ast _code_R;

    public AstExpr(Ast code_L, Operator op, Ast code_R)
    {
      this._code_L = code_L;
      this._op = op;
      this._code_R = code_R;
    }

    public override Ast Run()
    {
      Ast result = null;

      var code_L = _code_L.Run();
      var code_R = _code_R.Run();

      switch (_op)
      {
        case Operator.PLUS:
          result = code_L.Add(code_R);
          break;

        case Operator.MINUS:
          result = code_L.Sub(code_R);
          break;

        case Operator.MULTI:
          result = code_L.Mul(code_R);
          break;

        case Operator.DIVIDE:
          result = code_L.Div(code_R);
          break;
      }
      return result;
    }

  }
}
