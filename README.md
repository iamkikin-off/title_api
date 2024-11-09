# Title API | IamKikin & Nowaha

In the WebFishing update 1.09, the developer & contributors got "Titles".

A Title is a text above your head that says **"[LAMEDEV]" "[CONTRIBUTOR]"** with fun little colors.

<img src="https://media.discordapp.net/attachments/771319805349199922/1304643434937847829/image.png?ex=67302361&is=672ed1e1&hm=61b650e1fcb33df7f793449f8724656f6957084503c63f27601a963899ca4940&=&format=webp&quality=lossless" width="220"/>

# Developer Usage

```gdscript
onready var title_api = get_node_or_null("/root/TitleAPI")

func _ready:
    if title_api != null:
        _name = title_api.register_title(STEAM_ID, TITLE) # Register your own Title
```

### Example
```gdscript
onready var title_api = get_node_or_null("/root/TitleAPI")

func _ready():
    if title_api != null:
        _name = title_api.register_title(76561198123766302, "[color=#913BED][LOAF][/color]")
```

```gdscript
onready var title_api = get_node_or_null("/root/TitleAPI")

func _ready():
    if title_api != null:
        _name = title_api.register_title(76561199157842765, "[color=#D57EEC][KIKIN][/color]")
```

> Report any bugs to IamKikin on [WEBFISHING Modding Community Discord](https://discord.gg/HzhCPxeCKY)
> Mady by IamKikin & Nowaha
