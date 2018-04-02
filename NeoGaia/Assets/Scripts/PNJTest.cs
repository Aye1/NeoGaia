using System;
using UnityEngine;

public class PNJTest : MonoBehaviour, IInteractable {

    public DialogBox dialog;
    private bool isDialogOpen;
    private Player _player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnInteract(Player player)
    {
        _player = player;
        if (!isDialogOpen)
        {
            DialogBox newDialog = Instantiate(dialog);
            string[] texts = { "Bien ouej, tu es capable d'interagir avec un PNJ!"
                    , "Ceci est ma seconde phrase"
                    , "Et maintenant tu peux sauter 10 fois, parce que c'est comme ça"};
            newDialog.Init(texts);
            isDialogOpen = true;
            newDialog.dialogClosing += DialogClosed;
        }
    }

    private void DialogClosed(object sender, EventArgs args)
    {
        DialogBox dialog = (DialogBox)sender;
        dialog.dialogClosing -= DialogClosed;
        isDialogOpen = false;
        _player.has10Jumps = true;
    }
}
