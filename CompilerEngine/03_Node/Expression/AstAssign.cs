using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstAssign]")]
  public class AstAssign : AstExpr
  {
    public AstAssign(Ast code_L, Ast code_R)
      : base(code_L, Operator.EQUAL, code_R)
    {
    }

    public override Ast Run()
    {
      var sym_L = (AstValiable)this._code_L;
      var code_R = this._code_R.Run();

      Engine.SetValue(sym_L, code_R);

      return code_R;
    }

  }
}
