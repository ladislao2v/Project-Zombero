public class HandsPresenter : GunPresenter
{
    private Hands _handsModel;

    public override void Init(Gun gun)
    {
        _handsModel = (Hands)gun;

        base.Init(gun);

        _handsModel.Kicked += OnKicked;
    }

    private void OnDisable()
    {
        _handsModel.Kicked -= OnKicked;
    }

    private void OnKicked()
    {
        //OnShot(new Bullet(0, 0, 0));
    }
}
