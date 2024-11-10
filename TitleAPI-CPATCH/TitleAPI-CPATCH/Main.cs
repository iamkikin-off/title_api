using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace TitleAPI_CPATCH;

public class Main : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player_label.gdc";

    // Returns a list of tokens for the new script, with the input being the original script's tokens
    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        // Wait for any newline after any reference to "_ready"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_update_title"},
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "label"},
            t => t.Type is TokenType.Newline,
        ], allowPartialMatch: true);
        
        // Loop through all tokens in the script
        int skip = 0;
        foreach (var token in tokens) {
            if (skip-- > 0) continue;
            if (waiter.Check(token)) {
                // Found our match, return the original newline
                yield return token;

                // var title_api = get_node("/root/TitleAPI")
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("title_api");
                
                yield return new Token(TokenType.OpAssign);
                
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/TitleAPI"));
                yield return new Token(TokenType.ParenthesisClose);
                
                // New Line
                yield return new Token(TokenType.Newline, 1);
                
                // _name = title_api.resolve_name(player_id, _name)
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
