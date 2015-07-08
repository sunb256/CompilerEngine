using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  public class Token
  {
    public TokenType Prev { get; private set; }
    public TokenType Value { get; private set; }
    public History History{ get; private set; }

    private Scanner _scanner;

    public Token(Scanner scanner)
    {
      _scanner = scanner;

      this.Prev = TokenType.NULL;
      this.Value = TokenType.NULL;

      this.History = new History();
    }

    public Operator GetOperator()
    {
      switch (this.Value)
      {
        case TokenType.PLUS:
          return Operator.PLUS;
        case TokenType.MINUS:
          return Operator.MINUS;
        case TokenType.MULTI:
          return Operator.MULTI;
        case TokenType.DIVIDE:
          return Operator.DIVIDE;
        default:
          return Operator.NULL;
      }
    }

    public void Next()
    {
      this.Prev = _scanner.Type;

      var ret = _scanner.Next();
      if (ret == Scanner.ScanResult.OK)
      {
        this.Value = _scanner.Type;
        this.History.Add(_scanner);
      }
      else if (ret == Scanner.ScanResult.EOF)
      {
        this.Value = TokenType.EOS;
        this.History.Add(_scanner);
      }
      else
      {
        throw new Exception("grammer error!");
      }

    }



  }
}
