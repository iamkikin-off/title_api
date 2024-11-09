using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace ColorfulNames;

public class Main : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player_label.gdc";
    
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_update_title"},
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "label"},
            t => t.Type is TokenType.Newline,
        ], allowPartialMatch: true);
        
        int skip = 0;
        foreach (var token in tokens) {
            if (skip-- > 0) continue;
            if (waiter.Check(token)) {
                yield return token;

                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("title_api");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("get_node");
                
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/TitleAPI"));
                yield return new Token(TokenType.ParenthesisClose);

                yield return new Token(TokenType.Newline, 1);
                
                yield return new IdentifierToken("_name");
                yield return new Token(TokenType.OpAssign);
                yield return new IdentifierToken("title_api");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("resolve_name");
                
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new IdentifierToken("player_id");
                yield return new Token(TokenType.Comma);
                yield return new IdentifierToken("_name");
                yield return new Token(TokenType.ParenthesisClose);
                
                yield return new Token(TokenType.Newline, 1);
                
                skip = 22;
            } else {
                yield return token;
            }
        }
    }
}
