public class OpenSettingPopup : RxButton
{
    public override void OnClickAsync()
    {
        BoardManager.instance.openSettingPopup.Value = true;
    }
}

