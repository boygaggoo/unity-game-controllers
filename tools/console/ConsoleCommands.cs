using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConsoleCommands : MonoBehaviour {
	public List<string> response = new List<string>();
	
	public List<string> Execute(string _command) {
		string[] words = _command.Split(' '); 
		string command = words[0];

		// Clear list of old messages
		response.Clear();

		try {
			switch(command) {
				case "help":
					return Help();
				case "example":
					return Example();
				case "exampleArgs":
					return ExampleArgs();
				default:
					response.Add(command+": unrecognized command");
					return response;
			}
		} catch(System.IndexOutOfRangeException) {
			response.Add("Invalid arguments (check help)");
			return response;
		}
	}

	/**	Lists all available commands
	 *	@return: List {string} All available commands
	 */
	public List<string> Help() {
		response.Add("Available commands:");
		response.Add("- help [shows this menu]");
		response.Add("- example [shows example command]");
		response.Add("- exampleArgs {number} [shows example command using arguments]");
		
		return response;
	}

	/**	Example command with arguments
	 *	@params: {string} [0, 1]
	 */
	public List<string> ExampleArgs(string arg) {
		try {
			switch (arg) {
				case "1":
					response.Add("Example command with argument 1.");
					break;
				case "0":
					response.Add("Example command with argument 0.");
					break;
				default:
					response.Add("Invalid argument (should be number 1 or 0)");
					break;
			}

			return response;
		} catch (System.IndexOutOfRangeException) {
			response.Add("Invalid argument (should be only 1 or 0)");
			return response;
		}
	}

	// Example command
	public List<string> Example() {
		// Do your stuff here..

		// How to print text to console
		response.Add("This is example command.");

		return response;
	}
}
