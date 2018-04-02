using System;

public abstract class NonPlayableCharacter : Character {

    private DialogBox _dialog;

    public bool isDialogOpen = false;

    protected abstract void OnDialogClosed();

    public DialogBox StartDialog(DialogLine[] dialogLines)
    {
        if (!isDialogOpen)
        {
            DialogBox dialog = GameObjectProvider.instance.dialogBox;
            DialogBox _dialog = Instantiate(dialog);
            _dialog.Init(dialogLines, picture);
            isDialogOpen = true;
            _dialog.dialogClosing += DialogClosed;
        }
        return _dialog;
    }

    private void DialogClosed(object sender, EventArgs args)
    {
        DialogBox dialog = (DialogBox)sender;
        dialog.dialogClosing -= DialogClosed;
        isDialogOpen = false;
        OnDialogClosed();
    }
}
