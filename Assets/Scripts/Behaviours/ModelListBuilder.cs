using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModelListBuilder : MonoBehaviour
{
    [SerializeField]
    private GameObject m_GridContainer;
    [SerializeField]
    private GameObject m_GridItemPrefab;

    // Start is called before the first frame update
    private void Awake(){

        if (m_GridContainer != null){
            StartCoroutine(GenerateModelList());
        }
        else 
        {
            Debug.LogWarning(name + ": No Container Set");
        }
    }

    private IEnumerator GenerateModelList(){
        yield return new WaitUntil(() => ModelManager.Instance != null);

        foreach(ModelManager.ModelData model in ModelManager.Instance.Models){
            Button button = Instantiate(m_GridItemPrefab, m_GridContainer.transform).GetComponent<Button>();
            button.onClick.AddListener(()=>{
                ModelManager.Instance.LoadModel(model.Name);
            });
            button.GetComponentInChildren<Text>().text = model.Name;
            button.GetComponentInChildren<RawImage>().texture = model.Icon;
        }
    }
}
