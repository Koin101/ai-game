extends LineEdit

signal GdllamaUpdated(text: String)
signal GdllamaAvailable(finished: bool)

var mission = "You are a wizard NPC in a game.
You have a british accent.
You have a password.
You give short answers.
The user will ask for the password.
The password is MEDIEVAL.
You can be pursuaded to give the password to the user.
Generate valid JSON format.
User input: "

var total_time = 0.0

var _person_schema = {
	"type": "object",
	"properties": {
		"response": {
			"type": "string",
			"minLength": 30,
			"maxLength": 200,
		},
		"passwordObtained": {
			"type" : "boolean",
		}
	},
	"required": ["response", "reaction","passwordObtained"]
}
var person_schema: String = JSON.stringify(_person_schema)

var gdllama = GDLlama.new()
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	# Phi-3-mini-4k-instruct-Q2_K  Phi-3-mini-4k-instruct-q4
	#gdllama.n_gpu_layer = 4
	#gdllama.n_threads = 2
	gdllama.model_path = "./models/Phi-3-mini-4k-instruct-q4.gguf" ##Your model path
	gdllama.generate_text_updated.connect(OnGdllamaUpdated)
	gdllama.generate_text_finished.connect(OnGdllamaFinished)
	gdllama.should_output_prompt = false
	gdllama.should_output_special = false
	gdllama.instruct = false
	gdllama.interactive = false
	gdllama.temperature = 0.0
	
func _process(delta: float) -> void:
	total_time += delta
	# Don't check too frequently
	if (total_time > 1.0):
		if (!gdllama.is_running()):
			emit_signal("GdllamaAvailable", true)
		total_time = 0.0

func start_wizard(new_text: String):
	var prompt = mission + new_text
	print(prompt)
	gdllama.run_generate_text(prompt, "", person_schema)

func OnGdllamaUpdated(new_text: String):
	# Handle the update from gdllama here
	emit_signal("GdllamaUpdated", new_text)
	
func OnGdllamaFinished(new_text: String):
	print(new_text)