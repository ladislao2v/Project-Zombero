using UnityEngine;
using UnityEngine.UI;

public class HealthPresenter : MonoBehaviour, IPresenter
{
    [SerializeField] private Text _text;

    private Health _healthModel;

    public void Init(Health healthModel)
    {
        _healthModel = healthModel;    
    }


    private void Start()
    {
        _text = GetComponent<Text>();
    }

    public void OnEnable()
    {
        _healthModel.HealthChanged += OnHealthChanged;
    }

    public void OnDisable()
    {
        _healthModel.HealthChanged -= OnHealthChanged;
    }

    public void SetAmount(int health)
    {
        _text.text = $"{health}";
    }

    private void OnHealthChanged()
    {
        SetAmount(_healthModel.Value);
    }
}
