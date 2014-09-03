using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour {
  public GUISkin guiskin;
  private Rect windowRect = new Rect(Screen.width / 2 - 300, Screen.height / 2 - 200, 600, 400);
  private bool consoleEnabled = false; 
  public bool printInUnityConsole = false;    // Show output also in unity console

  [HideInInspector]
  private string consoleInput = "";
  private List<string> consoleMessages = new List<string>();
  private Vector2 scrollableArea = Vector2.zero;

  void OnGUI() {
    GUI.skin = guiskin;

    if (consoleEnabled) {
      windowRect = GUI.Window (1, windowRect, ConsoleWindow, "");
      GUI.FocusControl("ConsoleInputControl");
    }

    Event e = Event.current;
    if (e.keyCode == KeyCode.BackQuote && e.type == EventType.KeyUp) {
      ToggleConsole();
    }

    if (e.keyCode == KeyCode.Return) {
      ExecuteCommand(consoleInput);
    }
  }

  void ToggleConsole() {
    consoleEnabled = !consoleEnabled;
  }

  void ExecuteCommand(string _command) {
    if (consoleInput != "") {
      Message("$> "+consoleInput);
      List<string> response = new List<string>();
      response = gameObject.GetComponent<ConsoleCommands>().Execute(_command);
      response.ForEach(delegate(string _message) {
        Message(_message);
      });
      consoleInput = "";
      scrollableArea.y = Mathf.Infinity;
    }
  }

  void Update() {
    if (consoleInput == "`") {
      consoleInput = "";
    }

    if (consoleInput.IndexOf("`") != -1) {
      consoleInput = consoleInput.TrimEnd('`');
    }
  }

  void ConsoleWindow(int windowID) {
    GUI.DragWindow(new Rect(0, 0, 600, 40));

    GUI.Label(new Rect(20, 10, 200, 30), "Console");

    /* Console messages */
    scrollableArea = GUI.BeginScrollView(new Rect(20, 40, 560, 300), scrollableArea, new Rect(0, 0, 400, (18 * consoleMessages.Count)+10 ));
    DrawMessages();
    GUI.EndScrollView();

    GUI.SetNextControlName("ConsoleInputControl");
    consoleInput = GUI.TextField(new Rect(20, 350, 460, 30), consoleInput, 50);
    GUI.Label(new Rect(30, 349, 40, 30), "$>", "ConsoleRibbon");
    if (GUI.Button(new Rect(490, 350, 90, 30), "Send", "ButtonStyle")) {
      ExecuteCommand(consoleInput);
    }
  }

  /** @desc: Draws all messages from messages list
   *  @params: none
   */
  void DrawMessages() {
    foreach (string _message in consoleMessages) {
      GUILayout.BeginHorizontal();
      if (_message[0] == '$') {
        GUI.color = Color.gray;
      }
      GUILayout.Label(_message, "ConsoleMessage");
      GUI.color = Color.white;
      GUILayout.EndHorizontal();
    }
  }

  /** @desc: Creates a standard message in console
   *  @params: {string} message text
   */
  void Message(string _text) {
    consoleMessages.Add(_text);

    if (printInUnityConsole) {
      print(_text);
    }
  }

  void Start() {
    Message("Type `help` for command list.");
  }
}
