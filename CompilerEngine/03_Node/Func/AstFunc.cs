using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstFunc]")]
  public class AstFunc : Ast
  {
    protected List<Ast> _codeList;
    protected int argsCnt = 0;

    public AstFunc()
    {
    }

    public Ast Call(List<Ast> parameter)
    {
      // 引数チェック
      if (parameter == null)
      {
        if (argsCnt != 0)
        {
          throw new Exception("Func error :: param not different");
        }
      }
      else
      {
        if (parameter.Count != argsCnt)
        {
          throw new Exception("Func error :: param not different");
        }
      }

      return Exec(parameter);
    }

    // virtual
    public virtual Ast Exec(List<Ast> parameter)
    {
      throw new Exception("grammer error :: AstFunc.Exec error!");
    }

  }
}
