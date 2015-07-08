using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerEngine
{
  public class Parser
  {
    public History History { get { return this._token.History; } }

    private Scanner _lex;
    private Token _token;

    public Parser(Scanner scanner)
    {
      _lex = scanner;
      _token = new Token(_lex);
    }

    public Ast Parse()
    {
      Ast code = null;

      try
      {
        code = Program();
      }
      catch (Exception e)
      {
        throw new Exception("parser error :: " + e.Message);
      }

      return code;
    }


    // [構文] プログラム
    private Ast Program()
    {
      _token.Next();
      var code = Expression();

      if (code == null)
        return null;

      switch (_token.Value)
      {
        case TokenType.EOS:
          break;

        default:
          throw new Exception("parser error :: grammer error");
      }

      return code;
    }

    // [構文] 式
    private Ast Expression()
    {
      Ast result = null;
      Ast code = Term();

      switch (_token.Value)
      {
        case TokenType.EOS:    // 空のプログラム
          result = code;
          break;

        case TokenType.PLUS:
          result = Expression_Deep(code);
          break;

        case TokenType.MINUS:
          result = Expression_Deep(code);
          break;

        default:
          throw new Exception("parser error :: grammer error");
      }
      return result;
    }

    // [構文] 式-2
    private Ast Expression_Deep(Ast code_L)
    {
      AstExpr result = null;

      while ((_token.Value == TokenType.PLUS) || (_token.Value == TokenType.MINUS))
      {
        var op = _token.GetOperator();
        _token.Next();

        var code_R = Term();
        result = new AstExpr(code_L, op, code_R);
      }
      return result;
    }


    // [構文] 項
    private Ast Term()
    {
      Ast code = Factor();

      switch (_token.Value)
      {
        case TokenType.MULTI:
          code = Term_Deep(code);
          break;

        case TokenType.DIVIDE:
          code = Term_Deep(code);
          break;
      }
      return code;
    }

    // [構文] 項-2
    private Ast Term_Deep(Ast code_L)
    {
      AstExpr result = null;

      while ((_token.Value == TokenType.MULTI) || (_token.Value == TokenType.DIVIDE))
      {
        var op = _token.GetOperator();
        _token.Next();

        var code_R = Term();
        result = new AstExpr(code_L, op, code_R);
      }
      return result;
    }


    // [構文] 因子
    private Ast Factor()
    {
      Ast code = null;

      switch (_token.Value)
      {
        case TokenType.EOS:
          break;

        case TokenType.STRING:
          code = new AstString((string)_lex.Val);
          _token.Next();
          break;

        case TokenType.INT:
          code = new AstInt((int)_lex.Val);
          _token.Next();
          break;

        case TokenType.MINUS:
          _token.Next();
          var f_ret = Factor();
          code = new AstMinus(f_ret);
          break;

        case TokenType.APER:
          _token.Next();
          code = Expression();

          if (_token.Value != TokenType.OPER)
            throw new Exception("parser error :: grammer error :: not ')' mark ");

          _token.Next();
          break;

        // <SYMBOL> | <SYMBOL = Expression> | 関数呼出し
        case TokenType.VALIABLE:

          var sym = new AstValiable((string)_lex.Val);
          _token.Next();

          if (_token.Value == TokenType.EQUAL)
          {
            _token.Next();

            var assign = Expression();
            code = new AstAssign(sym, assign);
          }
          else
          {
            code = sym;
          }
          break;

        default:
          throw new Exception("parser error :: grammer error");
      }
      return code;
    }


  }
}
