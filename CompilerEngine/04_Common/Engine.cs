using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  public static class Engine
  {
    public static Dictionary<string, AstValiable> Symbols { get; private set; }
    public static Dictionary<string, Ast> Valiables { get; private set; }

    public static Scanner Lex { get; private set; }
    public static Parser Yacc { get; private set; }
    public static string Source { get; private set; }

    private static void init(string src)
    {
      Engine.Source = src;
      Engine.Symbols = new Dictionary<string, AstValiable>();
      Engine.Valiables = new Dictionary<string, Ast>();

      Engine.Lex = new Scanner(Engine.Source);
      Engine.Yacc = new Parser(Engine.Lex);
    }

    public static string Run(string src)
    {
      string ret = "";
      Ast result = null;

      Engine.init(src);

      do
      {
        try
        {
          result = Engine.Yacc.Parse();
        }
        catch (Exception ex)
        {
          return String.Format("[Result] NG >>> {0}\n\n{1}", ex.Message, Yacc.History.Dump());
        }

        if (result != null)
          ret += result.Run().ToString() + "\n";
        

      } while (result != null);

      return ret;
    }


    // ---------------------------
    // - valiable
    // ---------------------------

    public static bool HasValue(AstValiable key)
    {
      return Engine.Valiables.ContainsKey(key.Name) ? true : false;
    }

    public static void SetValue(AstValiable key, Ast value)
    {
      Engine.Valiables[key.Name] = value;
    }

    public static Ast GetValue(AstValiable key)
    {
      return Engine.Valiables[key.Name];
    }

  }
}
