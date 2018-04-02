public class PNJTest : NonPlayableCharacter, IInteractable {

    private Player _player;

    public void OnInteract(Player player)
    {
        _player = player;
        DialogLine[] texts = { new DialogLine("Bien ouej, tu es capable d'interagir avec un PNJ!", this)
                , new DialogLine("Je peux répondre !", _player)
                , new DialogLine("Et maintenant tu peux sauter 10 fois, parce que c'est comme ça", this)};
        StartDialog(texts);
    }

    protected override void OnDialogClosed()
    {
        _player.has10Jumps = true;
    }
}
