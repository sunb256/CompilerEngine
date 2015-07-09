using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstFuncCall]")]
  public class AstFuncCall : Ast
  {
    protected AstValiable _symbol;
    protected List<Ast> _codeList;

    public AstFuncCall(AstValiable symbol, List<Ast> codeList)
    {
      this._symbol = symbol;
      this._codeList = codeList;
    }

    public override Ast Run()
    {
      Ast code = Engine.GetFunc(this._symbol.Name);

      if (code == null)
        throw new Exception(String.Format("{0} is not exist in AstFuncCall", this._symbol.Name));

      if (!(code is AstFunc))
        throw new Exception(String.Format("{0} is not AstFunc", this._symbol.Name));

      var func = (AstFunc)code;

      var param = new List<Ast>();
      foreach (var c in this._codeList)
      {
        var ret = c.Run();
        param.Add(ret);
      }

      return func.Call(param);
    }

  }
}
