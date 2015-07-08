using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  [DebuggerDisplay("[AstValiable] : {this.Name}")]
  public class AstValiable : Ast
  {
    public string Name {get; private set; }

    public AstValiable(string name)
    {
      this.Name = name;
    }

    public override Ast Run()
    {
      var code = Engine.GetValue(this);
      if (code == null)
        throw new Exception("Valiable error :: " + this.Name);

      return code;
    }

    public override string ToString()
    {
      return this.Name;
    }

  }
}
