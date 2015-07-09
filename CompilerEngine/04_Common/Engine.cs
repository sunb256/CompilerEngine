using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  public static class Engine
  {
    public static Scanner Lex { get; private set; }
    public static Parser Yacc { get; private set; }
    public static string Source { get; private set; }
 
    public static Dictionary<string, Ast> Symbols { get; private set; }

    private static void init(string src)
    {
      Engine.Source = src;
      Engine.Symbols = new Dictionary<string, Ast>();

      Engine.Lex = new Scanner(Engine.Source);
      Engine.Yacc = new Parser(Engine.Lex);

      Engine.init_BuildinFunc();
    }

    private static void init_BuildinFunc()
    {
      Engine.SetValue("print", new AstFunc_Print());
      Engine.SetValue("max", new AstFunc_Max());
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
          // コードをAstに解析
          result = Engine.Yacc.Parse();
        }
        catch (Exception ex)
        {
          return String.Format("[Result] NG >>> {0}\n\n{1}", ex.Message, Yacc.History.Dump());
        }

        // Astを実行
        if (result != null)
          ret += result.Run().ToString() + "\n";
        

      } while (result != null);

      return ret;
    }


    // ---------------------------
    // - valiable
    // ---------------------------

    public static bool HasValue(string key)
    {
      return Engine.Symbols.ContainsKey(key) ? true : false;
    }

    public static void SetValue(string key, Ast value)
    {
      Engine.Symbols[key] = value;
    }

    // 変数取得
    public static Ast GetValue(AstValiable key)
    {
      if (!Engine.HasValue(key.Name))
        throw new Exception(String.Format("'{0}' is not found in GetValue", key));

      return Engine.Symbols[key.Name];
    }

    // 関数取得
    public static Ast GetFunc(string key)
    {
      if (!Engine.HasValue(key))
        throw new Exception(String.Format("'{0}' is not found in GetFunc", key));

      return Engine.Symbols[key];
    }

  }
}
