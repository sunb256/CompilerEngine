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

      // AND･OR の処理
      switch (_op)
      {
        case Operator.AND:
          result = code_L.And(_code_R);
          break;

        case Operator.OR:
          result = code_L.Or(_code_R);
          break;
      }


      var code_R = _code_R.Run();

      // 演算処理
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

        case Operator.LT:
          result = code_L.Lt(code_R);
          break;

        case Operator.GT:
          result = code_L.Gt(code_R);
          break;

        case Operator.EQ:
          result = code_L.Eq(code_R);
          break;

        case Operator.NE:
          result = code_L.Ne(code_R);
          break;

        case Operator.LE:
          result = code_L.Le(code_R);
          break;

        case Operator.GE:
          result = code_L.Ge(code_R);
          break;
      }
      return result;
    }

  }
}
