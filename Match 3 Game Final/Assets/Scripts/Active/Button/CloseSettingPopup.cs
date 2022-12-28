public class CloseSettingPopup : RxButton
{
    public override void OnClickAsync()
    {
        BoardManager.instance.openSettingPopup.Value = false;
    }
}

