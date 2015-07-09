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
      var code = Statement();

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

    // [構文] 文
    private Ast Statement()
    {
      Ast result = null;

      switch (_token.Value)
      {
        case TokenType.IF:
          result = If_Statement();
          break;

        case TokenType.WHILE:
          result = While_Statement();
          break;

        case TokenType.BLOCK_APER:
          result = Block_Statement();
          break;

        default:
          result = Expression();
          break;
      }

      return result;
    }



    // [構文] if文
    private Ast If_Statement()
    {
      Ast result = null;

      // (
      _token.Next();
      if (_token.Value != TokenType.APER)
        throw new Exception("parser error :: grammer error not '(' in If_Statement");

      // 式
      _token.Next();
      Ast if_cond = Expression();

      // )
      if (_token.Value != TokenType.OPER)
        throw new Exception("parser error :: grammer error not ')' in If_Statement");

      // 文
      _token.Next();
      Ast stmt_if = Statement();
      Ast stmt_el = null;

      // else 文
      if (_token.Value == TokenType.ELSE)
      {
        _token.Next();
        stmt_el = Statement();
      }

      result = new AstIf(if_cond, stmt_if, stmt_el);
      return result;
    }


    // [構文] While文
    private Ast While_Statement()
    {
      Ast result = null;

      // (
      _token.Next();
      if (_token.Value != TokenType.APER)
        throw new Exception("parser error :: grammer error not '(' in If_Statement");

      // 式
      _token.Next();
      Ast while_cond = Expression();

      // )
      if (_token.Value != TokenType.OPER)
        throw new Exception("parser error :: grammer error not ')' in If_Statement");

      // 文
      _token.Next();
      Ast stmt = Statement();

      result = new AstWhile(while_cond, stmt);
      return result;
    }


    // [構文] Block文
    private Ast Block_Statement()
    {
      Ast result = null;
      var codeList = new List<Ast>();

      _token.Next();

      // }
      while (_token.Value != TokenType.BLOCK_OPER) 
      {
        // 文を取得
        Ast code = Statement();
        
        if (_token.Value != TokenType.EOS)
        {
          throw new Exception("parser error :: grammer error not ';' in Block_Statement");
        }

        // 文を格納
        _token.Next();
        codeList.Add(code);
      }

      _token.Next();
      result = new AstBlock(codeList);
      return result;
    }


    // [構文] 式
    private Ast Expression()
    {
      Ast result = null;
      Ast code = SimpleExpr();

      switch (_token.Value)
      {
        case TokenType.LT: // <
        case TokenType.GT: // >
        case TokenType.EQ: // ==
        case TokenType.NE: // !=
        case TokenType.LE: // <=
        case TokenType.GE: // >=
          result = Expression_Deep(code);
          break;

        default:
          result = code;
          break;
      }
      return result;
    }

    // [構文] 式-2
    private Ast Expression_Deep(Ast code_L)
    {
      AstExpr result = null;

      while ((_token.Value == TokenType.LT) ||  // <
             (_token.Value == TokenType.GT) ||  // >
             (_token.Value == TokenType.EQ) ||  // ==
             (_token.Value == TokenType.NE) ||  // !=
             (_token.Value == TokenType.LE) ||  // <=
             (_token.Value == TokenType.GE))    // >=
      {
        var op = _token.GetOperator();
        _token.Next();

        var code_R = SimpleExpr();
        result = new AstExpr(code_L, op, code_R);
      }
      return result;
    }


    // [構文] Simple式
    private Ast SimpleExpr()
    {
      Ast result = null;
      Ast code = Term();

      switch (_token.Value)
      {
        case TokenType.EOS:
          result = code;
          break;

        case TokenType.PLUS:
          result = SimpleExpr_Deep(code);
          break;

        case TokenType.MINUS:
          result = SimpleExpr_Deep(code);
          break;

        case TokenType.OR:
          result = SimpleExpr_Deep(code);
          break;

        default:
          result = code;
          break;
      }
      return result;
    }

    // [構文] Simple式-2
    private Ast SimpleExpr_Deep(Ast code_L)
    {
      AstExpr result = null;

      while ((_token.Value == TokenType.PLUS) || (_token.Value == TokenType.MINUS) || (_token.Value == TokenType.OR))
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

        case TokenType.AND:
          code = Term_Deep(code);
          break;
      }
      return code;
    }

    // [構文] 項-2
    private Ast Term_Deep(Ast code_L)
    {
      AstExpr result = null;

      while ((_token.Value == TokenType.MULTI) || (_token.Value == TokenType.DIVIDE) || (_token.Value == TokenType.AND))
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

        case TokenType.TRUE:
          code = new AstBool(true);
          _token.Next();
          break;

        case TokenType.FALSE:
          code = new AstBool(false);
          _token.Next();
          break;

        case TokenType.INT:
          code = new AstInt((int)_lex.Val);
          _token.Next();
          break;

        case TokenType.MINUS:
          _token.Next();
          var f1_ret = Factor();
          code = new AstMinus(f1_ret);
          break;

        case TokenType.EX:
          _token.Next();
          var f2_ret = Factor();
          code = new AstNot(f2_ret);
          break;

        case TokenType.APER:
          _token.Next();
          code = SimpleExpr();

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

            var assign = SimpleExpr();
            code = new AstAssign(sym, assign);
          }
          else
          {
            code = sym;
          }
          break;

        default:
          throw new Exception("parser error :: grammer error in Factor");
      }
      return code;
    }


  }
}
