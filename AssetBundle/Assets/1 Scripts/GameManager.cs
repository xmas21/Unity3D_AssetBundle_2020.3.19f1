using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Header("載入物體 按鈕")] Button instantiateObj_Btn;

    [SerializeField] [Header("路徑 字串")] string addressName_Str;

    GameObject asset_Obj;   // 路徑物件
    bool isLoadSucc;        // 是否成功載入

    void Start()
    {
        Initialize();
        Onclick();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    void Initialize()
    {
        instantiateObj_Btn.interactable = false;
        isLoadSucc = false;

        // 異步加載指定名稱的物件
        // 加載完之後會回傳一個帶有 AsyncOperationHandle<GameObject> 的事件給 OnAssetObjLoaded
        Addressables.LoadAssetAsync<GameObject>(addressName_Str).Completed += OnAssetObjLoaded;
    }

    /// <summary>
    /// 按鈕觸發
    /// </summary>
    void Onclick()
    {
        instantiateObj_Btn.onClick.AddListener(CreateObjBtn);
    }

    /// <summary>
    /// 載入物件
    /// </summary>
    /// <param name="asyncOperationHandle"></param>
    void OnAssetObjLoaded(AsyncOperationHandle<GameObject> _asyncOperationHandle)
    {
        instantiateObj_Btn.interactable = true;
        isLoadSucc = true;

        // 載入好的物件存到 Asset_Obj 等待被使用。
        asset_Obj = _asyncOperationHandle.Result;
    }

    /// <summary>
    /// 生成物件
    /// </summary>
    void CreateObjBtn()
    {
        Vector3 pos = new Vector3(Random.Range(3, -3), Random.Range(6, 2), Random.Range(3, -3));
        Instantiate(asset_Obj, pos, Quaternion.identity);
    }
}
