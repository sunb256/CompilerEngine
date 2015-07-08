using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstBool]")]
  public class AstBool : Ast
  {
    private bool _value;

    public AstBool(bool value)
    {
      _value = value;
    }

    public override string ToString()
    {
      return _value.ToString();
    }

    public bool isTrue()
    {
      return _value == true ? true : false;
    }
  }
}
