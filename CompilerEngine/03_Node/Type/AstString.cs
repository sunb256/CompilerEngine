using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstString]")]
  public class AstString : Ast
  {
    private string _str;

    public AstString(string str)
    {
      _str = str;
    }

    public override string ToString()
    {
      return _str;
    }

    public override Ast Add(Ast code)
    {
      if (code.GetType().Name != "AstString")
      {
        throw new Exception("grammer error :: not string!");
      }
      var s = (AstString)code;

      return new AstString(this.ToString() + s.ToString());
    }
  }
}
