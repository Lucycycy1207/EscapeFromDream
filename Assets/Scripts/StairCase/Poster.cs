using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Poster : MonoBehaviour
{
    [SerializeField] private Color normalColor;
    [SerializeField] private Color abnormalColor;

    [SerializeField] float possibleOfTruePoster = 0.2f;
    [SerializeField] private bool[] posterSet;
    public bool[] visitedPoster { get; set; }

    Dictionary<MeshRenderer, bool> posterDic = new Dictionary<MeshRenderer, bool>();

    private void Awake()
    {
        int childNum = this.transform.childCount;
        visitedPoster = new bool[childNum];
        Debug.Log($"childNum = {childNum}");
        for (int i = 0; i < childNum; i++)
        {
            posterDic.Add(transform.GetChild(i).GetComponent<MeshRenderer>(), false);

            Debug.Log($"posters[{i}] = {transform.GetChild(i).name}");
            visitedPoster[i] = false;
        }
    }


    public bool getPosterValue(int index)
    {
        return posterSet[index];
    }
    public void RandomPoster(int index)
    {
        
        float value = Random.value;
        posterSet[index] = (value <= possibleOfTruePoster) ? true : false;

        Debug.Log($"set poster[{index}] to {posterSet[index]}");
    }
    public void ModifyPoster(int index, bool value)
    {
        posterSet[index] = value;
    }

    void PrintDictionary()
    {
        Debug.Log("Printing Dictionary:");

        foreach (var poster in posterDic)
        {
            Debug.Log($"Key: {poster.Key.name}, Value: {poster.Value}");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int index = 0;

        List<MeshRenderer> keys = new List<MeshRenderer>(posterDic.Keys);
        foreach (MeshRenderer key in keys)
        {
            //Debug.Log($"{index}: {posterSet[index]}");
            posterDic[key] = posterSet[index++];
            
            key.material.color = posterDic[key] ? normalColor : abnormalColor;
        }
    }
}
