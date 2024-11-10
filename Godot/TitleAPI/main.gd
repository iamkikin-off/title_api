extends Node

signal titles_updated

var plr_titles = {} # The dictionary where titles go.
var prefix = "[TitleAPI] " # Prefix of the mod

func _ready():
	# Register creator titles.
	_register_creator_titles()
	
	# -- DEBUG --
	print(prefix + "Loaded!")
	# -- /DEBUG --

# Get the title of a player. Returns null if they have no custom one.
func get_title(player_id):
	return plr_titles.get(player_id)

# Register a new title for a player
func register_title(id, title):
	plr_titles[id] = title
	emit_signal("titles_updated")

	# -- DEBUG --
	print(prefix + "Successfully set title to: " + title + ", for: " + str(id))
	print(prefix + "" + str(id) + " has a title: " + plr_titles[id])
	# -- /DEBUG --

func deregister_title(player_id):
	plr_titles.erase(player_id)
	emit_signal("titles_updated")
	
	# -- DEBUG --
	print(prefix + "Successfully removed title for: " + str(player_id))
	# -- /DEBUG --

func resolve_name(player_id, name):
	var players_title = plr_titles.get(player_id)
	if players_title == null: 
		# If player doesn't have a title, return just their name.
		return name
	
	# If player has a title, then return it from the dictionary.
	print(prefix + "Successfully resolved.")
	return players_title + "\n" + name
		
func _register_creator_titles():
	register_title(76561199157842767, "[rainbow][KIKIN][/rainbow]") # [IAMKIKIN] - IamKikin - Purple
	register_title(76561198123766303, "[color=#913BED][LOAF][/color]") # [LOAF] - Nowaha - Purple
	
	print(prefix + "Successfully set creator titles!")
