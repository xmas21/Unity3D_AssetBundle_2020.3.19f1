using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameManager : MonoBehaviour
{
    [SerializeField] [Header("���J���� ���s")] Button instantiateObj_Btn;

    [SerializeField] [Header("���| �r��")] string addressName_Str;

    GameObject asset_Obj;   // ���|����
    bool isLoadSucc;        // �O�_���\���J

    void Start()
    {
        Initialize();
        Onclick();
    }

    /// <summary>
    /// ��l��
    /// </summary>
    void Initialize()
    {
        instantiateObj_Btn.interactable = false;
        isLoadSucc = false;

        // ���B�[�����w�W�٪�����
        // �[��������|�^�Ǥ@�ӱa�� AsyncOperationHandle<GameObject> ���ƥ� OnAssetObjLoaded
        Addressables.LoadAssetAsync<GameObject>(addressName_Str).Completed += OnAssetObjLoaded;
    }

    /// <summary>
    /// ���sĲ�o
    /// </summary>
    void Onclick()
    {
        instantiateObj_Btn.onClick.AddListener(CreateObjBtn);
    }

    /// <summary>
    /// ���J����
    /// </summary>
    /// <param name="asyncOperationHandle"></param>
    void OnAssetObjLoaded(AsyncOperationHandle<GameObject> _asyncOperationHandle)
    {
        instantiateObj_Btn.interactable = true;
        isLoadSucc = true;

        // ���J�n������s�� Asset_Obj ���ݳQ�ϥΡC
        asset_Obj = _asyncOperationHandle.Result;
    }

    /// <summary>
    /// �ͦ�����
    /// </summary>
    void CreateObjBtn()
    {
        Vector3 pos = new Vector3(Random.Range(3, -3), Random.Range(6, 2), Random.Range(3, -3));
        Instantiate(asset_Obj, pos, Quaternion.identity);
    }
}
