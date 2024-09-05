using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class ItemShopView : MonoBehaviour
{
    [Header("Цена")]
    [SerializeField] private int _price;
    [Header("Data")]
    [SerializeField] private SkinPlane _skinPlane;
    [Header("UI")]
    [SerializeField] private Text _priceView;
    [SerializeField] private GameObject _purchase;
    [SerializeField] private GameObject _purchased;
    [SerializeField] private GameObject _selected;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _purchasedButton;
    [SerializeField] private Button _selectedButton;

    public ReactiveCommand OnChangeStateItemCommand = new();

    public void UpdateItemUI()
    {
        if (ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin == _skinPlane.Name)
            TurnButtons(false, false, true);
        else if (ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenSkins.Contains(_skinPlane.Name))
            TurnButtons(false, true, false);
        else
            TurnButtons(true, false, false);
    }

    private void Start()
    {
        _priceView.text = _price.ToString();
        _purchaseButton.onClick.AddListener(() => { OnPurchase(); });
        _purchasedButton.onClick.AddListener(() => { OnSelect(); });
        UpdateItemUI();
        ManagerUniRx.AddObjectDisposable(OnChangeStateItemCommand);
    }

    private void OnPurchase()
    {
        AudioManager.Instance.PlayClickButton();
        if (WalletManager.TryPurchase(_price))
        {
            ContainerSaveerPlayerPrefs.Instance.SaveerData.OpenSkins += _skinPlane.Name + ",";
            OnChangeStateItemCommand.Execute();
        }
        else
            AudioManager.Instance.PlayPurchaseError();
    }

    private void OnSelect()
    {
        AudioManager.Instance.PlayClickButton();
        ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin = _skinPlane.Name;
        OnChangeStateItemCommand.Execute();
    }

    private void TurnButtons(bool isPurchase, bool isPurchased, bool isSelected)
    {
        _purchase.SetActive(isPurchase);
        _purchased.SetActive(isPurchased);
        _selected.SetActive(isSelected);
    }

    private void OnDestroy()
    {
        ManagerUniRx.Dispose(OnChangeStateItemCommand);
    }
}
