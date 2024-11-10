using GDWeave.Godot;
using GDWeave.Godot.Variants;
using GDWeave.Modding;

namespace TitleAPI;

public class PlayerLabelPatch : IScriptMod {
    public bool ShouldRun(string path) => path == "res://Scenes/Entities/Player/player_label.gdc";

    public IEnumerable<Token> Modify(string path, IEnumerable<Token> tokens) {
        var topOfFile = new MultiTokenWaiter([
            t => t.Type is TokenType.Newline,
            t => t.Type is TokenType.Newline,
        ], allowPartialMatch: true);
        
        // Wait for the line after 'label' after any reference to "_update_title"
        var waiter = new MultiTokenWaiter([
            t => t is IdentifierToken {Name: "_update_title"},
            t => t.Type is TokenType.Newline,
            t => t is IdentifierToken {Name: "label"},
            t => t.Type is TokenType.Newline,
        ], allowPartialMatch: true);
        
        int skip = 0;
        foreach (var token in tokens) {
            if (skip-- > 0) continue;
            if (topOfFile.Check(token)) {
                yield return token;

                // onready var title_api = get_node("/root/TitleAPI")
                yield return new Token(TokenType.PrOnready);
                yield return new Token(TokenType.PrVar);
                yield return new IdentifierToken("title_api");
                
                yield return new Token(TokenType.OpAssign);
                
                yield return new IdentifierToken("get_node");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("/root/TitleAPI"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline);

                // func _ready():
                yield return new Token(TokenType.PrFunction);
                yield return new IdentifierToken("_ready");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Colon);
                yield return new Token(TokenType.Newline, 1);

                // title_api.connect("titles_updated", self, "_update_title")
                yield return new IdentifierToken("title_api");
                yield return new Token(TokenType.Period);
                yield return new IdentifierToken("connect");
                yield return new Token(TokenType.ParenthesisOpen);
                yield return new ConstantToken(new StringVariant("titles_updated"));
                yield return new Token(TokenType.Comma);
                yield return new Token(TokenType.Self);
                yield return new Token(TokenType.Comma);
                yield return new ConstantToken(new StringVariant("_update_title"));
                yield return new Token(TokenType.ParenthesisClose);
                yield return new Token(TokenType.Newline);
            } else if (waiter.Check(token)) {
                // Found our match, return the original newline
                yield return token;
                
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
