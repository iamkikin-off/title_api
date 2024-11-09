extends Node

var titles = {
	# 76561199157842767: "[color=#D57EEC][KIKIN][/color]",
	76561198123766303: "[color=#913BED][LOAF][/color]"
}

func _ready():
	var YourSteamID = Network.STEAM_ID
	
	print("[Kikin's Title API] Loaded! - " + str(YourSteamID))
	register_title(76561199157842767, "[color=#D57EEC][KIKIN][/color]")
	

func register_title(id, title):
	titles[id] = title
	
func resolve_name(player_id, name):
	print(player_id)
	print(titles.get(player_id))
	var title = titles.get(player_id)
	if title == null: 
		return name
	return title + "\n" + name
		
