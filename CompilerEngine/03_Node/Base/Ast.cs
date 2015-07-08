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

    public virtual Ast And(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Or(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Add(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Sub(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Mul(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Div(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Lt(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Gt(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Eq(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Ne(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Le(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

    public virtual Ast Ge(Ast code)
    {
      throw new Exception("grammer error :: Ast.run error!");
    }

  }



}
