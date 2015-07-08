using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstInt] : {this.Value}")]
  public class AstInt : Ast
  {
    public int Value { get; private set; }

    public AstInt(int val)
    {
      this.Value = val;
    }

    public override string ToString()
    {
      return this.Value.ToString();
    }


    public override Ast Add(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;
      
      return new AstInt(val1 + val2);
    }

    public override Ast Sub(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return new AstInt(val1 - val2);
    }

    public override Ast Mul(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return new AstInt(val1 * val2);
    }

    public override Ast Div(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return new AstInt(val1 / val2);
    }

    public override Ast Lt(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return (val1 < val2) ? new AstBool(true) : new AstBool(false);
    }

    public override Ast Gt(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return (val1 > val2) ? new AstBool(true) : new AstBool(false);
    }

    public override Ast Eq(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return (val1 == val2) ? new AstBool(true) : new AstBool(false);
    }

    public override Ast Ne(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return (val1 != val2) ? new AstBool(true) : new AstBool(false);
    }

    public override Ast Le(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return (val1 <= val2) ? new AstBool(true) : new AstBool(false);
    }

    public override Ast Ge(Ast code)
    {
      var val1 = this.Value;
      var val2 = ((AstInt)code).Value;

      return (val1 >= val2) ? new AstBool(true) : new AstBool(false);
    }

  }
}
