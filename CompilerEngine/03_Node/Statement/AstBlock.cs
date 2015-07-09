using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstBlock]")]
  public class AstBlock : Ast
  {
    protected List<Ast> _codeList;

    public AstBlock(List<Ast> codeList)
    {
      this._codeList = codeList;
    }

    public override Ast Run()
    {
      Ast code = null;

      if (this._codeList.Count <= 0)
        return code;

      // block文を実行
      foreach (var c in this._codeList)
      {
        code = c.Run();
      }

      return code;
    }

  }
}
