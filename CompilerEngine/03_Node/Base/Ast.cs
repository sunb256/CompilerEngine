using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[Ast]")]
  public class Ast
  {
    public Ast()
    {
    }

    public virtual Ast Run()
    {
      return this;
    }


    public virtual Ast Add(Ast code)
    {
      return null;
    }

    public virtual Ast Sub(Ast code)
    {
      return null;
    }

    public virtual Ast Mul(Ast code)
    {
      return null;
    }

    public virtual Ast Div(Ast code)
    {
      return null;
    }
  }



}
