using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;

public class ShopView : MonoBehaviour
{
    [SerializeField] private List<ItemShopView> _itemsShop;
    [SerializeField] private Button _back;
    [SerializeField] private TextMeshProUGUI _countMoney;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void Start()
    {
        _back.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlayClickButton();
            SetActive(false);
        });
        UpdateViewMoney();

        foreach (var item in _itemsShop)
        {
            foreach (var itemTemp in _itemsShop)
                item.OnChangeStateItemCommand.Subscribe(_ =>
                {
                    itemTemp.UpdateItemUI();
                    UpdateViewMoney();
                });
        }
    }

    private void UpdateViewMoney()
        => _countMoney.text = ContainerSaveerPlayerPrefs.Instance.SaveerData.Money.ToString();
}
