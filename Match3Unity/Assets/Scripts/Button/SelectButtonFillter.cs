//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;
//using UniRx;
//using System.Linq;

//public enum FilterState
//{
//    DEFAULT,
//    ALL,
//    D,
//    C,
//    B,
//    A,
//    S
//}

//public class SelectButtonFillter : RxSelect, IPointerEnterHandler, IPointerExitHandler
//{
//    private Button button;


//    [HideInInspector]
//    public bool isPlayActive = false;
//    public bool isClick;
//    public FilterState filterState;
//    public static SelectButtonFillter Instance;

//    WalletService walletService = WalletService.Instance;


//    //protected override void Awake()
//    //{
//    //    clickButtonHighlight(FilterState.ALL);
//    //}

//    // Start is called before the first frame update
//    void Start()
//    {
//        Instance = this;

//        button = gameObject.GetComponent<Button>();
//        //clickButtonHighlight(walletService.selectFilter.Value);

//        button.OnClickAsObservable()
//        .Subscribe(_ =>
//        {
//            switch (filterState)
//            {
//                case FilterState.ALL:
//                    walletService.selectFilter.Value = FilterState.ALL;
//                    MarketManager.Instance.ShowMyNFT(FilterState.ALL);
//                    break;
//                case FilterState.D:
//                    walletService.selectFilter.Value = FilterState.D;
//                    MarketManager.Instance.ShowMyNFT(FilterState.D);

//                    break;
//                case FilterState.C:
//                    walletService.selectFilter.Value = FilterState.C;
//                    MarketManager.Instance.ShowMyNFT(FilterState.C);

//                    break;
//                case FilterState.B:
//                    walletService.selectFilter.Value = FilterState.B;
//                    MarketManager.Instance.ShowMyNFT(FilterState.B);

//                    break;
//                case FilterState.A:
//                    walletService.selectFilter.Value = FilterState.A;
//                    MarketManager.Instance.ShowMyNFT(FilterState.A);

//                    break;
//                case FilterState.S:
//                    walletService.selectFilter.Value = FilterState.S;
//                    MarketManager.Instance.ShowMyNFT(FilterState.S);
//                    break;
//            }          
//        });

//        walletService.selectFilter
//            .Subscribe(_ =>
//            {
//                clickButtonHighlight(_);
//            });
//    }

//    protected override void subscribeIsSelect(bool isSelect)
//    {
//        selectHighlightObject.SetActive(isSelect);
//    }

//    public void OnPointerEnter(PointerEventData eventData)
//    {
//        if (isPlayActive == false)
//            isSelect.Value = true;
//    }
//    public void OnPointerExit(PointerEventData eventData)
//    {
//        if (isClick == false)
//            isSelect.Value = false;
//    }

//    public void clickButtonHighlight(FilterState state) 
//    {     
//        isClick = false;
//        isSelect.Value = false;
//        if (filterState == FilterState.DEFAULT)
//            return;
//        if (filterState == state)
//        {
//            isClick = true;
//            isSelect.Value = true;
//        }
//    }
    
//}
