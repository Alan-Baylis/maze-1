using UnityEngine;
using System.Collections;

public static class EventController {

	public delegate void NamedEvent(string eventName);
	public static event NamedEvent OnNamedEvent;

	public static void Event (string eventName) {
		if (OnNamedEvent != null) {
			OnNamedEvent(eventName);
		}
	}
}
